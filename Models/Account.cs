using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ParkAndRide.Models
{
    public partial class Account
    {
        [JsonProperty("accountId")]
        public int AccountId { get; set; }
        [JsonProperty("login")]
        public string Login { get; set; }
        [JsonProperty ("password")]
        public string Password { get; set; }
        [JsonProperty("creationDate")]
        public DateTime CreationDate { get; set; }
        [JsonProperty("isActive")]
        public int IsActive { get; set; }
        [JsonProperty("blocked")]
        public int Blocked { get; set; }
        [JsonProperty("ReasonForBlocking")]
        public string ReasonForBlocking { get; set; }
        [JsonProperty("UserId")]
        public int? UserId { get; set; }

        public User User { get; set; }
    }
}
