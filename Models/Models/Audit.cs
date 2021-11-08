using System;
using System.Collections.Generic;
using System.Text;

namespace SharedModels.Models
{
    public class Audit
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public int UserId { get; set; }
        public string Client { get; set; }
        public string Change { get; set; }
        public DateTime Change_Date { get; set; }
    }
}
