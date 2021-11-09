using Microsoft.AspNetCore.Mvc;
using SharedModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTonicAssessmentAPI.Data;


namespace WebTonicAssessmentAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ErrorLogsController : ControllerBase
    {
        public ApplicationDbContext _context { get; set; }

        public ErrorLogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet][Route("GetAllErrorLogs")]
        public ActionResult<List<Error_log>> GetAllErrorLogs()
        {
            try
            {
                var errors = _context.ErrorLogs;
                if (errors == null)
                {
                    return NotFound();
                }
                return errors.ToList();
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

 
        [HttpGet][Route("GetErrorLogById/{id}")]
        public ActionResult<Error_log> GetErrorLogById(int id)
        {
            try
            {
                var error = _context.ErrorLogs.Where(x => x.ID == id).FirstOrDefault();
                if (error == null)
                {
                    return NotFound();
                }
                return error;
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


        [HttpGet]
        [Route("GetErrorLogsByClientName/{client}")]
        public ActionResult<List<Error_log>> GetErrorLogsByClientName(string client)
        {
            try
            {
                var errors = _context.ErrorLogs.Where(x => x.Client == client);
                if (errors == null)
                {
                    return NotFound();
                }
                return errors.ToList();
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

        [HttpPost][Route("PostErrorLog")]
        public ActionResult<Error_log> PostErrorLog(Error_log error_Log)
        {
            try
            {
                _context.ErrorLogs.Add(error_Log);
                _context.SaveChanges();
                return CreatedAtAction("GetErrorLogById", new { id = error_Log.ID }, error_Log);
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
