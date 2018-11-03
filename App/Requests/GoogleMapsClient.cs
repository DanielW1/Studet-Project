using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ParkAndRide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace ParkAndRide.App.Requests
{
    public class GoogleMapsClient
    {

        static HttpClient client = new HttpClient();

       public static async Task<GeoCodingObject> GetGeoCodingAsync(string latitude, string longitude, string key)
        {
            GeoCodingObject geoCodingObject = null;
            string path = getGeoCodingPath(latitude, longitude, key);
            try
            {
                HttpResponseMessage response = await client.GetAsync(path);
                string result;
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                
                         geoCodingObject = JsonConvert.DeserializeObject<GeoCodingObject>(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetGeoCodingAsync in GoogleMapsClient Error" + ex.Message);
            }
            return geoCodingObject;
        }
        /*
         * Origin moze byc wartoscia geometryczna latitude oraz longitude oddzielonych przecinkiem "," lub nazwa, adresem
         * Destination moze byc wartoscia geometryczna latitude oraz longitude oddzielonych przecinkiem "," lub nazwa, adresem
         * Mozna rowniez podac wiele wartosci oddzielonych znakiem "|"
         * Funkcja zwraca odleglosc od punktu a do punktu b.
         */
        public static async Task<DistanceMatrixObject> GetDistanceMatrixAsync(string origin, string destination, string key)
        {
            string path = getDistanceMatrixPath(origin, destination, key, "pl");

            DistanceMatrixObject distanceMatrixObject = null;

            try
            {
                HttpResponseMessage response = await client.GetAsync(path);
                string result;
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                    distanceMatrixObject = JsonConvert.DeserializeObject<DistanceMatrixObject>(result);
                }

            }catch(Exception ex)
            {
                Console.WriteLine("GetDistanceMatrixAsync in GoogleMapsClient Error" + ex.Message);
            }
            return distanceMatrixObject;
        }

        public static string getDistanceMatrixPath(string origin, string destination, string key, string language)
        {
            return String.Format("https://maps.googleapis.com/maps/api/distancematrix/json?" +
                "origins={0}&destinations={1}&language={2}&&key={3}", origin, destination,language, key);
        }

        public static string getGeoCodingPath(string latitude, string longitude, string key)
        {
            return String.Format("https://maps.googleapis.com/maps/api/geocode/json?latlng={0},{1}&key={2}", latitude, longitude, key);
        }

    }
}
