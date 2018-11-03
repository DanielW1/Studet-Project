using System;
using System.Collections.Generic;

namespace ParkAndRide.Models
{
    public partial class AdditionInfo
    {
        public int InfoId { get; set; }
        public string TypeOfMachine { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int? UserId { get; set; }

        public User User { get; set; }
    }
}
