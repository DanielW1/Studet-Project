using ParkAndRide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkAndRide.App.Formatter
{
    public class RequestFormatter
    {

        public static string geometryLocationFormatter(string latitude, string longitude)
        {
            return String.Format("{0},{1}", latitude, longitude);
        }

        public static string geometryMultipleLocationFormatter(List<Location> locations)
        {
            string result = "";

            foreach(Location location in locations)
            {
                result+= String.Format("{0},{1}|", location.lat, location.lng);
            }

            return result.Substring(0, result.Length - 1);
        }
    }
}
