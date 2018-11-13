using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ParkAndRide.Models;
using ParkAndRide.App.ActionModel;
using ParkAndRide.Models.App;
using ParkAndRide.App.Requests;
using ParkAndRide.App.Utilities;
using ParkAndRide.App.ConfigureJSON;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Newtonsoft.Json;

public class ParkingController : Controller
{
    private readonly IOptions<GoogleApiConfig> config;

    public ParkingController(IOptions<GoogleApiConfig> config)
    {
        this.config = config;
        ConfigureOption.Config = config;
    }

    ParkAndRideContext db =new ParkAndRideContext();

    public IActionResult List()
    {
       
        var parkings= (from e in db.Parking select e).ToList();

        List<ParkingExtensions> exParkings = ParkingExtensions.takeParkingExtensionsWithNumberPlaces(parkings);

        string origin = String.Format("{0},{1}", parkings[0].GpsLat, parkings[0].GpsLng);
        string destination = String.Format("{0},{1}", parkings[1].GpsLat, parkings[1].GpsLng);

       var temp =  GoogleMapsClient.GetDistanceMatrixAsync(origin, destination, config.Value.GoogleApiKey).Result;

        return View(exParkings);
    }

    public IEnumerable<ParkingExtensions> mobileList()
    {

        var parkings = ((from e in db.Parking select e)).ToList();
        List<ParkingExtensions> exParkings = ParkingExtensions.takeParkingExtensionsWithNumberPlaces(parkings);

        return exParkings;
    }

    [HttpGet]
    public IActionResult BestRoad()
    {
        List<ParkingExtensions> parkings = new List<ParkingExtensions>();
        return View(parkings);
    }

    [HttpPost]
    public IActionResult BestRoad(List<Parking>parkings)
    {
        Location userLocation;
        List<ParkingExtensions> result;

            userLocation = new Location() {lat=parkings[parkings.Count-1].GpsLat, lng = parkings[parkings.Count-1].GpsLng };

        if (userLocation.lat != null && userLocation.lat != "" &&
            userLocation.lng != null && userLocation.lng != "")
        {
            var dbParkings = (from e in db.Parking select e).ToList();

            List<ParkingExtensions> exParkings = ParkingExtensions.takeParkingExtensionsWithNumberPlaces(dbParkings);

            result = ParkingAction.bestParkings(exParkings, userLocation, 3, DistanceType.DURATION);
        }
        else
        {
            result = new List<ParkingExtensions>();
        }
  
        return View(result);
    }

    public async Task<IActionResult> Statistic(string name)
    {
        List<StatisticModel> statistics = new List<StatisticModel>();
        var conn = db.Database.GetDbConnection();
        string result = null;
        try
        {
            await conn.OpenAsync();
            using (var command = conn.CreateCommand())
            {
                string query = "SELECT * FROM FN_free_places_statistic('P+R Metro M³ociny')";
                command.CommandText = query;
                DbDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        result = reader.ToString();

                        statistics.Add(new StatisticModel()
                        {
                            DayOfWeek = reader.GetInt32(0),
                            Hour = reader.GetInt32(1),
                            Minutes = reader.GetInt32(2),
                            AvgNumberOfFreePlaces = reader.GetInt32(3)
                        });

                        //var r = Serialize(reader);
                      //  var json = JsonConvert.DeserializeObject(reader.ToString());
                        Console.WriteLine();

                    }
                }
                reader.Dispose();
            }
        }
        finally
        {
            conn.Close();
        }
        return View(statistics);

    }

    public IActionResult About()
    {
        return View();
    }
    public IActionResult Contact()
    {
        return View();
    }
    public IActionResult Error()
    {
        return View();
    }
}