using ParkAndRide.App.Requests;
using ParkAndRide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkAndRide.App.ActionModel
{
    public class BackgroundWorker
    {

        public static void getCurrentDateFromParkAndRideApi()
        {
            try
            {
                var result = ParkAndRideApiClient.GetAllParkAndRideApiAsync().Result;

                using (ParkAndRideContext db = new ParkAndRideContext())
                {
                    foreach (var item in result)
                    {
                        db.HistoricalState.Add(item);
                        db.SaveChanges();
                    }

                }
            }catch(Exception ex)
            {
                Console.WriteLine("BackgroundWorker.getCurrentDateFromParkAndRideApi " + ex.Message);
            }

        }
    }
}
