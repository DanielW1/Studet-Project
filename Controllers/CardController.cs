using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ParkAndRide.Models;

namespace ParkAndRide.Controllers
{
    public class CardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IEnumerable<Card> GetFullfilledCard(int id)
        {
            List<Card> cards = new List<Card>();
            using(ParkAndRideContext db = new ParkAndRideContext())
            {
              cards = db.Card.Include(x=> x.Parking).Where(x => (x.UserId == id && x.DataTo != null))
                    .OrderByDescending(x=> x.DataFrom).ToList();
            }

            return cards;
        }
        [HttpGet]
        public IEnumerable<Card> GetOpenCard(int id)
        {
            List<Card> cards = new List<Card>();
            using (ParkAndRideContext db = new ParkAndRideContext())
            {
                cards = db.Card.Include(x => x.Parking).Where(x => (x.UserId == id && x.DataTo == null)).ToList();
            }

            return cards;
        }

        [HttpPost]
        public IActionResult Post()
        {

            Card card = null;
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body, System.Text.Encoding.UTF8))
                {
                    string res = reader.ReadToEndAsync().Result;
                    card = JsonConvert.DeserializeObject<Card>(res);

                    using (ParkAndRideContext db = new ParkAndRideContext())
                    {
                       List<Card> cards = db.Card.Include(x => x.Parking).Where(x => (x.UserId == card.UserId && x.DataTo == null)).ToList();
                        if (cards.Count == 0)
                        {
                            db.Card.Add(card);
                            db.SaveChanges();
                        }
                        else
                        {
                            return StatusCode(409, "Masz już otwarte karty");
                        }
                    }
                }
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
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
            return Created("", card);
        }

        [HttpPut]
        public IActionResult Put()
        {
            Card card = null;
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body, System.Text.Encoding.UTF8))
                {
                    string res = reader.ReadToEndAsync().Result;
                    card = JsonConvert.DeserializeObject<Card>(res);

                    using (ParkAndRideContext db = new ParkAndRideContext())
                    {
                        db.Card.Update(card);
                        db.SaveChanges();
                    }
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
    }
}