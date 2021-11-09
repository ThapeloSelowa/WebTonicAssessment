using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedModels.Models;
using WebTonicAssessment.ApiAccessMethods;

namespace WebTonicAssessment.Pages
{
    public class ViewStudentRecordsModel : PageModel
    {
        [BindProperty]
        public List<StudentRecord> studentsRecords { get; set; }
        public void OnGet()
        {
            studentsRecords = StudentRecordsControllerMethods.GetAllStudentsRecords();
        }
    }
}
