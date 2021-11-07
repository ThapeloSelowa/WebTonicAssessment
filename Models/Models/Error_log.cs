using System;
using System.Collections.Generic;
using System.Text;

namespace SharedModels.Models
{
    public class Error_log
    {
        public int ID { get; set; }
        public string Section { get; set; }
        public string Method { get; set; }
        public string Message { get; set; }
        public DateTime Date_Stamp { get; set; }
        public string Computer { get; set; }
        public string Client { get; set; }
        public int UserId { get; set; }
    }
}
