using System;
using System.Collections.Generic;

namespace ParkAndRide.Models
{
    public partial class Parking
    {
        public Parking()
        {
            Card = new HashSet<Card>();
            ParkingPlace = new HashSet<ParkingPlace>();
        }

        public Parking(Parking parking)
        {
            this.ParkingId = parking.ParkingId;
            this.Name = parking.Name;
            this.Place = parking.Place;
            this.Street = parking.Street;
            this.Number = parking.Number;
            this.PostCode = parking.PostCode;
            this.Area = parking.Area;
            this.CreationDate = parking.CreationDate;
            this.UndergroundPlace = parking.UndergroundPlace;
            this.Guarded = parking.Guarded;
            this.Admin = parking.Admin;
            this.GpsLat = parking.GpsLat;
            this.GpsLng = parking.GpsLng;
            this.Admin = parking.Admin;
            this.Card = parking.Card;
            this.ParkingPlace = parking.ParkingPlace;
          
        }

        public int ParkingId { get; set; }
        public string Name{get;set;}
        public string Place { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string PostCode { get; set; }
        public int Area { get; set; }
        public DateTime CreationDate { get; set; }
        public int NumberOfFloor { get; set; }
        public int Guarded { get; set; }
        public string UndergroundPlace { get; set; }
        public int? AdminId { get; set; }
        public string GpsLat { get; set; }
        public string GpsLng { get; set; }

        public ParkingAdministrator Admin { get; set; }
        public ICollection<Card> Card { get; set; }
        public ICollection<ParkingPlace> ParkingPlace { get; set; }

        
    }
}
