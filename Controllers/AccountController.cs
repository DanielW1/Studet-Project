using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkAndRide.Models;
using Newtonsoft.Json;
using System.Web;
using System.IO;
using Microsoft.EntityFrameworkCore;

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


        [HttpPost]
        public  IActionResult Post()
        {
            Account account = null;
            try
            {
                
                using (StreamReader reader = new StreamReader(Request.Body, System.Text.Encoding.UTF8))
                {
                    string res = reader.ReadToEndAsync().Result;
                    var user = JsonConvert.DeserializeObject<User>(res);
                    user.EMail = "";
                    user.Sex = "";
                    user.Birthday = DateTime.Now;
                    user.Place = "Warszawa";
                    user.Street = "Puchatka";
                    user.NumberOfBuilding = "1";
                    user.NumberOfFlat = "1";
                    user.PostCode = "00-640";
                    user.Phone = "801802803";

                   account = JsonConvert.DeserializeObject<Account>(res);
                    account.CreationDate = DateTime.Now;

                        using (ParkAndRideContext db = new ParkAndRideContext())
                        {
                            account.User = user;
                            db.Account.Add(account);
                            db.SaveChanges();
                        }
                        ModelState.Clear();

                }
            }catch(Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                return StatusCode(409);
            }
            catch (JsonReaderException)
            {
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest();

            }
            return Created("",account);
        }

        [HttpPost]
        public IActionResult mobileLogin()
        {
            Account account = null;
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body, System.Text.Encoding.UTF8))
                {
                    string res = reader.ReadToEndAsync().Result;
                    account = JsonConvert.DeserializeObject<Account>(res);

                    var usr = db.Account.Include(d => d.User).
                        Where(u => u.Login == account.Login && u.Password == account.Password).FirstOrDefault();

                    if (usr != null)
                        return Ok(usr);
                    else
                        return Unauthorized();
                }
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                return BadRequest("Wrong body contains");
            }
            catch (JsonReaderException)
            {
                return BadRequest("Wrong body contains");
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult mobilePut()
        {
            Account account = null;
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body, System.Text.Encoding.UTF8))
                {
                    string res = reader.ReadToEndAsync().Result;
                    account = JsonConvert.DeserializeObject<Account>(res);
                }

                using(ParkAndRideContext db = new ParkAndRideContext())
                {
                    db.Account.Update(account);
                    db.SaveChanges();
                }
                return Ok();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                return BadRequest("Wrong body contains");
            }
            catch (JsonReaderException)
            {
                return BadRequest("Wrong body contains");
            }
            catch (Exception e)
            {
                return BadRequest();

            }
        }


        public IActionResult LoggedIn(){

            if(HttpContext.Session.ToString() != null){
                ViewBag.sessionId = HttpContext.Session.Id;
                return View();

            }else{

               return RedirectToAction("Login");
            }
        }

    }

}
