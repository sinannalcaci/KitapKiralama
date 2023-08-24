using KitapKiralama.DataAccess.Concrete.EntityFramework.Mapping;
using KitapKiralama.Entity.Poco;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitapKiralama.DataAccess.Concrete.EntityFramework.Context
{
    public class AppDbContext:IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server =.\SQLAHL; Database = LibraryDb; Trusted_Connection = True; TrustServerCertificate=True");
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new KitapMap());
            modelBuilder.ApplyConfiguration(new KitapTuruMap());
            modelBuilder.ApplyConfiguration(new KiralamaMap());
            

            base.OnModelCreating(modelBuilder);

        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    }
}
