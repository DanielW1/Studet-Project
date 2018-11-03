using System;
using System.Collections.Generic;

namespace ParkAndRide.Models
{
    public partial class ParkingAdministrator
    {
        public ParkingAdministrator()
        {
            Parking = new HashSet<Parking>();
        }

        public int AdminId { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public string Street { get; set; }
        public string NumberOfBuilding { get; set; }
        public string NumberOfFlat { get; set; }
        public string PostCode { get; set; }
        public string Nip { get; set; }
        public string Regon { get; set; }
        public string Phone { get; set; }
        public string EMail { get; set; }

        public ICollection<Parking> Parking { get; set; }
    }
}
