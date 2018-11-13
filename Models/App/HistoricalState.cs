using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkAndRide.Models.App
{
    public class HistoricalState
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int IdState { get; set; }
        public string Name { get; set; }
        public int NumberOfFreePlace { get; set; }
        public DateTime Date { get; set; }
    }
}
