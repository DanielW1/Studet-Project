using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkAndRide.Models;
using System.Web;


namespace ParkAndRide.Controllers
{
    public class AccountController : Controller
    {
        ParkAndRideContext db = new ParkAndRideContext();
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Account account){
            
            using (ParkAndRideContext db = new ParkAndRideContext()){

                var usr = db.Account.Where(u => u.Login == account.Login && u.Password == account.Password).FirstOrDefault();

                if (usr != null){

                  HttpContext.Session.Set("AccountId",BitConverter.GetBytes(account.AccountId));
                return RedirectToAction("LoggedIn");

                }else{
                    ModelState.AddModelError("", "Zły login lub hasło");
                }
            }

            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Account account)
        {
            if (ModelState.IsValid)
            {
                using (ParkAndRideContext db = new ParkAndRideContext())
                {
                    db.Account.Add(account);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = "Sukces";
            }
            return View();
        }

        public IActionResult LoggedIn(){

            if(HttpContext.Session.ToString() != null){
                ViewBag.sessionId = HttpContext.Session.Id;
                return View();

            }else{

               return RedirectToAction("Login");
            }
        }

        // var usr = db.userAccount.Where(u => u.Username == user.Username && u.Password == user.Password).FirstOrDefault();


    }
}
