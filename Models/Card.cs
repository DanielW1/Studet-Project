using System;
using System.Collections.Generic;

namespace ParkAndRide.Models
{
    public partial class Card
    {
        public int CardId { get; set; }
        public DateTime DataFrom { get; set; }
        public DateTime? DataTo { get; set; }
        public int? ParkingId { get; set; }
        public int? UserId { get; set; }

        public Parking Parking { get; set; }
        public User User { get; set; }
    }
}
