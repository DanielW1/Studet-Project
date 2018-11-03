using System;
using System.Collections.Generic;

namespace ParkAndRide.Models
{
    public partial class User
    {
        public User()
        {
            Account = new HashSet<Account>();
            AdditionInfo = new HashSet<AdditionInfo>();
            Card = new HashSet<Card>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Sex { get; set; }
        public DateTime Birthday { get; set; }
        public string Place { get; set; }
        public string Street { get; set; }
        public string NumberOfBuilding { get; set; }
        public string NumberOfFlat { get; set; }
        public string PostCode { get; set; }
        public int? Pesel { get; set; }
        public string EMail { get; set; }
        public string Phone { get; set; }

        public ICollection<Account> Account { get; set; }
        public ICollection<AdditionInfo> AdditionInfo { get; set; }
        public ICollection<Card> Card { get; set; }
    }
}
