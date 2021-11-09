using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SharedModels.Models;
using WebTonicAssessment.ApiAccessMethods;

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
                                            rowString = rowString.Remove(rowString.Length - 1);
                                        }

                                        if (rowString.StartsWith('\"'))
                                        {
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
                                        student.Client = "WebTonic";
                                        student.CapturedByUserId = 0;
                                       
                                        studentsRecords.Add(student);
                                    }
                                }
                            }
                    }
                    HttpContext.Session.SetString("studentsRecords", JsonConvert.SerializeObject(studentsRecords));
                }
                catch (Exception ex)
                {
                    var error = new Error_log
                    {
                        Section = ex.Source,
                        Method = ex.TargetSite.Name,
                        Message = ex.Message,
                        Date_Stamp = DateTime.Now,
                        Computer = System.Environment.MachineName
                    };
                    ErrorLogsControllerAccessMethods.CreateErrorLog(error);
                }

            }

        }

        public void OnPostSaveToDatabase()
        {           
            var data = HttpContext.Session.GetString("studentsRecords");
            if (data != null)
            {
                List<StudentRecord> studentsRecords = JsonConvert.DeserializeObject<List<StudentRecord>>(data);
                var results = StudentRecordsControllerMethods.CreateBulkStudentRecords(studentsRecords);
                if (results.Count > 0)
                {
                    //Logging an audit for the user addition of students records
                    Audit audit = new Audit();
                    audit.Client = "WebTonic";
                    audit.UserEmail = "";
                    audit.UserId = 1;
                    audit.Change = $"{audit.UserEmail} added {studentsRecords.Count} new students records.";
                    AuditControllerAccessMethods.CreateAudit(audit);

                   //Destroying students records saved in the session
                    HttpContext.Session.Remove("studentsRecords");
                }
                else
                { 
                    //Display data not saved message
                }
                
            }
            else
            {
                //Return no records found to save
            }
        }
    }
}
