using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebTonicAssessment.Pages
{
    public class csvuploaderModel : PageModel
    {
        [BindProperty]
        public List<IFormFile> FormFiles { get; set; }
        public void OnGet()
        {
        }

        public void OnPost(List<IFormFile> recordsFiles)
        {
            FormFiles = recordsFiles;
            return;
            if (recordsFiles.Count > 0) {

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
                                        string[] rows = sreader.ReadLine().Split(';');
                                        string studenNo = rows[0];
                                        string FirstName = rows[1];
                                        string Surname = rows[2];
                                        string CourseCode = rows[3];
                                        string CourseDescription = rows[4];
                                        string Grade = rows[5];
                                    }
                                }
                            }
                    }
                }
                catch (Exception ex) { 
                    
                }
                
            }
            
        }
    }
}
