using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkAndRide.Models
{
    public class GeoCodingObject
    {
        [JsonProperty("status")]
        public string status { get; set; }
        [JsonProperty("results")]
        public Results[] results { get; set; }

    }

    public class Results
    {
        [JsonProperty("formatted_address")]
        public string FormattedAddress { get; set; }
        [JsonProperty("geometry")]
        public Geometry geometry { get; set; }
        [JsonProperty("types")]
        public string[] Types { get; set; }
        [JsonProperty("address_components")]
        public AddressComponent[] addressComponents { get; set; }
    }

    public class Geometry
    {
        public string location_type { get; set; }
        public Location location { get; set; }
    }

    public class Location
    {
        public string lat { get; set; }
        public string lng { get; set; }
    }

    
    public class AddressComponent
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public string[] types { get; set; }
    }
}
