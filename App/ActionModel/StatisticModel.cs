using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkAndRide.App.ActionModel
{
    public class StatisticModel
    {
        public int DayOfWeek { get; set; }
        public int Hour { get; set; }
        public int Minutes { get; set; }
        public int AvgNumberOfFreePlaces { get; set; }
    }
}
