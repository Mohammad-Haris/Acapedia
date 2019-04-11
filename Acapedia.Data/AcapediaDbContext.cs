using Acapedia.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Acapedia.Data
{
    public class AcapediaDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Country> Country
        {
            get; set;
        }
        public DbSet<Discipline> Discipline
        {
            get; set;
        }
        public DbSet<WebsiteLink> WebsiteLink
        {
            get; set;
        }
        public DbSet<ApplicationUser> ApplicationUser
        {
            get; set;
        }

        public AcapediaDbContext (DbContextOptions<AcapediaDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Discipline>()
                .HasIndex(disc => disc.DisciplineName).IsUnique();

            modelBuilder.Entity<WebsiteLink>()
                .HasIndex(link => new
                {
                    link.LinkCountryName,
                    link.LinkDisciplineId
                });
        }
    }
}
