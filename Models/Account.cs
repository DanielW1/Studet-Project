using System;
using System.Collections.Generic;

namespace ParkAndRide.Models
{
    public partial class Account
    {
        public int AccountId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate { get; set; }
        public int IsActive { get; set; }
        public int Blocked { get; set; }
        public string ReasonForBlocking { get; set; }
        public int? UserId { get; set; }

        public User User { get; set; }
    }
}
