using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityService
{
    public class SchoolSystemContext: DbContext
    {
        public SchoolSystemContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Wellinton Connectionstring
            //optionsBuilder.UseSqlServer(@"Server=.;Database=SchoolSystem;Trusted_Connection=True;TrustServerCertificate=True;");

            //ken Connectionstring
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=SchoolSystem;User ID=sa;Password=kensindy;TrustServerCertificate=true;");

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<TestResult> TestResults { get; set; }
    }
}
