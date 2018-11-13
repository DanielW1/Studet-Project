using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ParkAndRide.App.ConfigureJSON;
using ParkAndRide.App.EMail;
using ParkAndRide.App.Requests;
using ParkAndRide.Models;
using ParkAndRide.Models.App;
using ParkAndRide.Models.Config;

namespace ParkAndRide.Controllers
{
    public class HomeController : Controller
    {
        ParkAndRideContext db = new ParkAndRideContext();
        private readonly IOptions<GoogleApiConfig> config;
        private readonly IOptions<SMTPSession> configSmtp;

        public HomeController(IOptions<GoogleApiConfig> config, IOptions<SMTPSession> configSmtp)
        {
            this.config = config;
            this.configSmtp = configSmtp;
            ConfigureOption.Config = config;
            ConfigureOption.ConfigSMTP = configSmtp;
        }

        public IActionResult Index()
        {
          // int result = GoogleMapsClient.GetParkAndRideApiAsync().Result;
            var parkings = (from e in db.Parking select e).ToList();
         //   var temp = db.Parking.Include(x=> x.ParkingPlace);
            
            List<ParkingExtensions> exParkings = ParkingExtensions.takeParkingExtensionsWithNumberPlaces(parkings);

            return View(exParkings);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        [HttpPost]
        public IActionResult Contact(MailModel mail)
        {
            IMailModel email = new MailModel(mail);
            email.sendEmail();
            

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

       
    }
}
