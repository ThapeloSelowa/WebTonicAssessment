using Microsoft.EntityFrameworkCore;
using SharedModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTonicAssessmentAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<StudentRecord> StudentRecords { get; set; }
        public DbSet<Error_log> ErrorLogs { get; set; }
        public DbSet<Audit> Audits { get; set; }
    }
}
