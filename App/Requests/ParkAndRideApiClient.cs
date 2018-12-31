using Newtonsoft.Json;
using ParkAndRide.Models.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ParkAndRide.App.Requests
{
    public class ParkAndRideApiClient
    {
        static HttpClient client = new HttpClient();
        public static async Task<int> GetParkAndRideApiAsync()
        {
            string result = "";
            string path = "https://parkandrideapi.azurewebsites.net/api/Parking/getNumberOfFreePlaces/";
            try
            {
                HttpResponseMessage response = await client.GetAsync(path);

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetGeoCodingAsync in GoogleMapsClient Error" + ex.Message);
            }
            return int.Parse(result);
        }


        public static async Task<List<HistoricalState>> GetAllParkAndRideApiAsync()
        {
            string result = "";
            List<HistoricalState> parkings = null;
            string path = "https://parkandrideapi.azurewebsites.net/api/AllParking";
            try
            {
                HttpResponseMessage response = await client.GetAsync(path);

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                    parkings = JsonConvert.DeserializeObject<List<HistoricalState>>(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetGeoCodingAsync in GoogleMapsClient Error" + ex.Message);
  
            }
            return parkings;
        }
    }
}
