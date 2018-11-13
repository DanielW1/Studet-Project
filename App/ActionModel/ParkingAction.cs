using Microsoft.Extensions.Options;
using ParkAndRide.App.ConfigureJSON;
using ParkAndRide.App.Utilities;
using ParkAndRide.Models;
using ParkAndRide.Models.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkAndRide.App.ActionModel
{
    public class ParkingAction
    {

        /*
         * Zwraca najlepsze parkingi dla użytkownika 
         **/
        public static List<ParkingExtensions> bestParkings(List<ParkingExtensions>parkings, Location location, 
            int numberOfBestResult, DistanceType type)
        {
            foreach(ParkingExtensions park in parkings)
            {
                park.distanceFromUserLocation(location, ConfigureOption.Config.Value.GoogleApiKey);
            }

            List<ParkingExtensions> result = new List<ParkingExtensions>();
            parkings = sortParkingsByArgument(parkings, type);

            for(int i=0; i<parkings.Count; i++)
            {
                result.Add(parkings[i]);
                if (result.Count == numberOfBestResult) break;
            }
            return result;
        }

        private static List<ParkingExtensions> sortParkingsByArgument(List<ParkingExtensions> parkings, DistanceType type)
        {

            switch (type)
            {

                case DistanceType.DISTANCE:
                    parkings = parkings.OrderBy(x => x.DistanceToUserPlace).ToList();
                    break;

                case DistanceType.DURATION:
                    parkings = parkings.OrderBy(x => x.DurationToUserPlace).ToList();
                    break;

                default:
                    break;
            }

            return parkings;
        }
    }
}
