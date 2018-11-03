using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkAndRide.Models
{
    public class ParkingGeoCoding
    {

        public List<GeoCodingObject> geoCodingObects { get; set; }
        public List<Parking> parkings { get; set; }

        public ParkingGeoCoding(List<GeoCodingObject> geoCodingObjects, List<Parking> parkings)
        {
            this.geoCodingObects = geoCodingObects;
            this.parkings = parkings;
        }
    }
}
