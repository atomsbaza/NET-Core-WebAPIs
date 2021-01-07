using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace True_Test_WebAPIs.Models
{
    public class Worker
    {
        public int Msg_id { get; set; }
        public string Sender { get; set; }
        public string Msg { get; set; }
        public DateTime Received_Time { get; set; }
    }
}
