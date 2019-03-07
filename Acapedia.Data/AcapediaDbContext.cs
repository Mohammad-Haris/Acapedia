using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Acapedia.Data.Models;

namespace Acapedia.Data
{
    public class AcapediaDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Contribution> Contribution { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Discipline> Discipline { get; set; }
        public DbSet<University> University { get; set; }
        public DbSet<UniversityDiscipline> UniversityDiscipline { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        
        public AcapediaDbContext (DbContextOptions<AcapediaDbContext> options)
            : base(options)
        {
        }
    }
}
