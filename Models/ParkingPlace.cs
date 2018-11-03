using System;
using System.Collections.Generic;

namespace ParkAndRide.Models
{
    public partial class ParkingPlace
    {
        public int PlaceId { get; set; }
        public string TypeOfPlace { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }
        public int? ParkingId { get; set; }

        public Parking Parking { get; set; }
    }
}
