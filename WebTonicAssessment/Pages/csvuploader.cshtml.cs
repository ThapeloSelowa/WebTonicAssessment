using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedModels.Models;

namespace WebTonicAssessment.Pages
{
    public class csvuploaderModel : PageModel
    {
        [BindProperty]
        public List<IFormFile> FormFiles { get; set; }
        [BindProperty]
        public List<StudentRecord> studentsRecords { get; set; }
        public void OnGet()
        {
        }

        public void OnPost(List<IFormFile> recordsFiles)
        {
            FormFiles = recordsFiles;

            if (recordsFiles.Count > 0)
            {

                studentsRecords = new List<StudentRecord>();

                try
                {
                    foreach (var formFile in recordsFiles)
                    {
                        if (formFile.FileName.EndsWith(".csv") || formFile.FileName.EndsWith(".CSV"))
                            if (formFile.Length > 0)
                            {
                                var fileName = formFile.FileName;
                                var TempfilePath = Path.GetTempFileName();

                                using (var sreader = new StreamReader(formFile.OpenReadStream()))
                                {
                                    string[] headers = sreader.ReadLine().Split(';');
                                    while (!sreader.EndOfStream)
                                    {
                                        var rowString = sreader.ReadLine();
                                        if (rowString.EndsWith('\"'))
                                        {
                                            rowString = rowString.Remove(rowString.Length-1);
                                        }

                                        if(rowString.StartsWith('\"')){
                                            rowString = rowString.Substring(1);
                                        }
                                        string[] rows = rowString.Split(';');
                                        string studentNo = rows[0].ToString();
                                        string FirstName = rows[1].ToString();
                                        string Surname = rows[2].ToString();
                                        string CourseCode = rows[3].ToString();
                                        string CourseDescription = rows[4].ToString();
                                        string Grade = rows[5].ToString();

                                        //Creating student record
                                        var student = new StudentRecord();
                                        student.StudentNo = Convert.ToInt32(studentNo);
                                        student.FirstName = FirstName;
                                        student.Surname = Surname;
                                        student.CourseCode = CourseCode;
                                        student.CourseDescription = CourseDescription;
                                        student.Grade = Grade;
                                        studentsRecords.Add(student);
                                    }
                                }
                            }
                    }
                }
                catch (Exception ex)
                {

                }

            }

        }
    }
}
