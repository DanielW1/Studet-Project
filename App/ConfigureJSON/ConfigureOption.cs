using Microsoft.Extensions.Options;
using ParkAndRide.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkAndRide.App.ConfigureJSON
{
    public class ConfigureOption
    {
        public static IOptions<GoogleApiConfig> Config { get; set; }

       
    }
}
