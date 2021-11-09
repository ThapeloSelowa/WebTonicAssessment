using Microsoft.AspNetCore.Mvc;
using SharedModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTonicAssessmentAPI.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebTonicAssessmentAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuditsController : ControllerBase
    {
        public ApplicationDbContext _context { get; set; }

        public AuditsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAllAudits")]
        public ActionResult<List<Audit>> GetAllAudits()
        {
            try
            {
                var audits = _context.Audits;
                if (audits == null)
                {
                    return NotFound();
                }
                return audits.ToList();
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
        [Route("GetAuditByUserId/{id}")]
        public ActionResult<Audit> GetAuditByUserId(int userid)
        {
            try
            {
                var audit = _context.Audits.Where(x => x.UserId == userid).FirstOrDefault();
                if (audit == null)
                {
                    return NotFound();
                }
                return audit;
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
        [Route("GetAuditByClient/{client}")]
        public ActionResult<Audit> GetAuditByClient(string client)
        {
            try
            {
                var audit = _context.Audits.Where(x => x.Client == client).FirstOrDefault();
                if (audit == null)
                {
                    return NotFound();
                }
                return audit;
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
        [Route("GetAuditByUserEmail/{id}")]
        public ActionResult<Audit> GetAuditByUserEmail(string useremail)
        {
            try
            {
                var audit = _context.Audits.Where(x => x.UserEmail == useremail).FirstOrDefault();
                if (audit == null)
                {
                    return NotFound();
                }
                return audit;
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

        [HttpPost]
        [Route("PostAudit")]
        public ActionResult<Audit> PostAudit(Audit audit)
        {
            try
            {
                _context.Audits.Add(audit);
                _context.SaveChanges();
                return CreatedAtAction("GetAuditById", new { id = audit.Id }, audit);
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
