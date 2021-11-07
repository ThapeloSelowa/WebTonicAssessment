using System;
using System.Collections.Generic;
using System.Text;

namespace SharedModels.Models
{
   public class StudentRecord
    {
        public int Id { get; set; }
        public int StudentNo { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string CourseCode { get; set; }
        public string CourseDescription { get; set; }
        public string Grade { get; set; }
    }
}
    