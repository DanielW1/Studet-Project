using ParkAndRide.App.Formatter;
using ParkAndRide.App.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkAndRide.Models.App
{
    public class ParkingExtensions:Parking
    {
        public ParkingExtensions():base()
        {
           
        }

        public ParkingExtensions(Parking parking):base(parking)
        {
            
        }
        public int NumberOfPlaces { get; set; }
        public int NumberOfFreePlaces { get; set; }
        public int DurationToUserPlace { get; set; }
        public int DistanceToUserPlace { get; set; }

        public static List<ParkingExtensions> takeParkingExtensionsWithNumberPlaces(List<Parking> parking)
        {

            ParkAndRideContext db = new ParkAndRideContext();
            
                var parkingExtension = (from e in db.ParkingPlace
                                        group e by e.ParkingId
                                   into newgroup
                                        select new { key = newgroup.Key, number = newgroup.Count() }).ToList();

            var freeParkingPlaces = ParkAndRideApiClient.GetAllParkAndRideApiAsync().Result;


            List<ParkingExtensions> exPark = new List<ParkingExtensions>();
            foreach (Parking park in parking)
            {
                var obj = parkingExtension.Find(x => x.key == park.ParkingId);
                ParkingExtensions exParking;
                exParking = obj != null ? new ParkingExtensions(park) { NumberOfPlaces = obj.number } : new ParkingExtensions(park);

                var result = freeParkingPlaces.Find(x => x.Name == park.Name);

                exParking.NumberOfFreePlaces = result.NumberOfFreePlace;
                exPark.Add(exParking);

            }

            return exPark;
        }
        /*
         * Pobiera jako parametr wspolrzedne urzytkownika korzystajacego z aplikacji oraz klucz API
         * ustawia obecna droge liczona w czasie oraz dystancie od wybranego parkingu "this", zwraca obiekt z API Google
         * 
         * */
        public DistanceMatrixObject distanceFromUserLocation(Location location, string path)
        {
            string origin = RequestFormatter.geometryLocationFormatter(location.lat, location.lng);
            string destination = RequestFormatter.geometryLocationFormatter(this.GpsLat, this.GpsLng);
            DistanceMatrixObject result = GoogleMapsClient.GetDistanceMatrixAsync(origin, destination, path).Result;
            this.DurationToUserPlace = result.Rows[0].Elements[0].Duration.Value;
            this.DistanceToUserPlace = result.Rows[0].Elements[0].Distance.Value;
            return result;
        }

        public void setDistanceFromUserLoactionAsDistance(Location location, string path)
        {
            DistanceMatrixObject result = distanceFromUserLocation(location, path);
            int distance = result.Rows[0].Elements[0].Distance.Value;
            this.DistanceToUserPlace = distance;
        }

        public void setDistanceFromUserLoactionAsTime(Location location, string path)
        {
            DistanceMatrixObject result = distanceFromUserLocation(location, path);
            int duration = result.Rows[0].Elements[0].Duration.Value;
            this.DurationToUserPlace = duration;
        }
    }

}
