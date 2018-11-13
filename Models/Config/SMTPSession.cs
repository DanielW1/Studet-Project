using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkAndRide.Models.Config
{
    public class SMTPSession
    {
        public string Server { get; set; }
    public int port { get; set; }
    public string senderLogin { get; set; }
    public string senderPassword { get; set; }
    }
}
