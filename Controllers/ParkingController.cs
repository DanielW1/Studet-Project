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
using ParkAndRide.App.DBFunction;

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

    public IEnumerable<ParkingExtensions> MobileList()
    {

        var parkings = (from e in db.Parking select e).ToList();

        List<ParkingExtensions> exParkings = ParkingExtensions.takeParkingExtensionsWithNumberPlaces(parkings);

        string origin = String.Format("{0},{1}", parkings[0].GpsLat, parkings[0].GpsLng);
        string destination = String.Format("{0},{1}", parkings[1].GpsLat, parkings[1].GpsLng);

        var temp = GoogleMapsClient.GetDistanceMatrixAsync(origin, destination, config.Value.GoogleApiKey).Result;

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

    [HttpGet]
    public IEnumerable<ParkingExtensions> MobileBestRoad(float gpsLat, float gpsLng)
    {
        Location userLocation;
        List<ParkingExtensions> result;

        userLocation = new Location() { lat = gpsLat.ToString(), lng = gpsLng.ToString() };

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

        return result;
    }

    public async Task<IActionResult> Statistic(int id)
    {
        string query = String.Format("SELECT * FROM FN_free_places_statistic({0})",id);
        string query2 = String.Format("SELECT * FROM FN_free_places_statistic_last_week({0})", id);
        List<StatisticModel> statistics =  await DBRequest.getStatisticsModel(query);
        List<StatisticModel> statisticsLastWeek =  await DBRequest.getStatisticsModel(query2);

        List<List<StatisticModel>> result = new List<List<StatisticModel>>();
        result.Add(statistics);
        result.Add(statisticsLastWeek);

        return View(result);

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