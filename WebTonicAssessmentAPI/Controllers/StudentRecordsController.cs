using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTonicAssessmentAPI.Data;


namespace WebTonicAssessmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentRecordsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentRecordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet][Route("GetAllStudentsRecords")]
        public ActionResult<List<StudentRecord>> GetAllStudentsRecords()
        {
            try
            {
                var students = _context.StudentRecords;
                if (students == null)
                {
                    return NotFound();
                }
                return students.ToList();
            }
            catch (Exception ex)
            {
                _context.ErrorLogs.Add(new Error_log
                {
                    Section = ex.Source,
                    Method = ex.TargetSite.Name,
                    Message = ex.Message,
                    Date_Stamp = DateTime.Now,
                    Computer = System.Environment.MachineName
                });
                _context.SaveChanges();
                return StatusCode(500);
            }


        }

        [HttpGet][Route("GetStudentRecordById/{id}")]
        public ActionResult<StudentRecord> GetStudentRecordById(int id)
        {
            try
            {
                var student = _context.StudentRecords.Where(x => x.Id == id).FirstOrDefault();
                if (student == null)
                {
                    return NotFound();
                }
                return student;
            }
            catch (Exception ex)
            {
                _context.ErrorLogs.Add(new Error_log
                {
                    Section = ex.Source,
                    Method = ex.TargetSite.Name,
                    Message = ex.Message,
                    Date_Stamp = DateTime.Now,
                    Computer = System.Environment.MachineName
                });
                _context.SaveChanges();
                return StatusCode(500);
            }

        }

        [HttpPost][Route("PostStudentRecord")]
        public ActionResult<StudentRecord> PostStudentRecord(StudentRecord student)
        {
            try
            {
                _context.StudentRecords.Add(student);
                _context.SaveChanges();
                return CreatedAtAction("GetStudentById", new { id = student.Id }, student);
            }
            catch (Exception ex)
            {
                _context.ErrorLogs.Add(new Error_log
                {
                    Section = ex.Source,
                    Method = ex.TargetSite.Name,
                    Message = ex.Message,
                    Date_Stamp = DateTime.Now,
                    Computer = System.Environment.MachineName
                });
                _context.SaveChanges();
                return StatusCode(500);
            }
        }

        [HttpPost][Route("PostBulkStudentsRecord")]
        public ActionResult<List<StudentRecord>> PostBulkStudentsRecord(List<StudentRecord> students)
        {
            try
            {
                var processedStudents = new List<StudentRecord>();
                foreach (var student in students) {
                   var savedStudent =  _context.StudentRecords.Add(student);
                    _context.SaveChanges();
                    processedStudents.Add(savedStudent.Entity);
                }
                return processedStudents;
            }
            catch (Exception ex)
            {
                _context.ErrorLogs.Add(new Error_log
                {
                    Section = ex.Source,
                    Method = ex.TargetSite.Name,
                    Message = ex.Message,
                    Date_Stamp = DateTime.Now,
                    Computer = System.Environment.MachineName
                });
                _context.SaveChanges();
                return StatusCode(500);
            }
        }

        [HttpPut][Route("PutStudentRecord")]
        public ActionResult<StudentRecord> PutStudentRecord(int id, StudentRecord student)
        {
            try
            {
                if (id != student.Id)
                {
                    return BadRequest();
                }

                _context.Entry(student).State = EntityState.Modified;

                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentRecordExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return student;
            }
            catch (Exception ex)
            {
                _context.ErrorLogs.Add(new Error_log
                {
                    Section = ex.Source,
                    Method = ex.TargetSite.Name,
                    Message = ex.Message,
                    Date_Stamp = DateTime.Now,
                    Computer = System.Environment.MachineName
                });
                _context.SaveChanges();
                return StatusCode(500);
            }
        }

        private bool StudentRecordExists(int id)
        {
            return _context.StudentRecords.Any(e => e.Id == id);
        }

        // DELETE api/<StudentRecordsController>/5
        [HttpDelete("{id}")]
        public ActionResult<StudentRecord> Delete(int id)
        {
            try
            {
                var student = _context.StudentRecords.Find(id);
                if (student == null)
                {
                    return NotFound();
                }
                _context.StudentRecords.Remove(student);
                _context.SaveChanges();
                return student;
            }
            catch (Exception ex)
            {
                _context.ErrorLogs.Add(new Error_log
                {
                    Section = ex.Source,
                    Method = ex.TargetSite.Name,
                    Message = ex.Message,
                    Date_Stamp = DateTime.Now,
                    Computer = System.Environment.MachineName
                });
                _context.SaveChanges();
                return StatusCode(500);
            }
        }
    }
}
