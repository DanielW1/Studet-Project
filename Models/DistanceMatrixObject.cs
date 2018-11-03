using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkAndRide.Models
{
    public class DistanceMatrixObject
    {
        [JsonProperty("destination_addresses")]
        public string[] DestinationAddresses { get; set; }
        [JsonProperty("origin_addresses")]
        public string[] OriginAddresses { get; set; }
        [JsonProperty("rows")]
        public Row[] Rows { get; set; } 
        [JsonProperty("status")]
        public string Status { get; set; }

    }

    public class Row
    {
        public Element[] Elements { get; set; }

    }

    public class Element
    {

            [JsonProperty("distance")]
            public Distance Distance { get; set; }
            [JsonProperty("duration")]
            public Duration Duration { get; set; }
            public string Status { get; set; }

    }

    public class Distance
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("value")]
        public int Value { get; set; }
    }

    public class Duration
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("value")]
        public int Value { get; set; }

    }


}
