using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using VoxU_Backend.Pesistence.Identity.Entities;

namespace VoxU_Backend.Pesistence.Identity.Context
{
    public class IdentityContext : IdentityDbContext
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);

            //Schema definition 
            modelbuilder.HasDefaultSchema("Identity");


            modelbuilder.Entity<ApplicationUser>().ToTable("Users");
            modelbuilder.Entity<IdentityRole>().ToTable("IdentityRole");
            modelbuilder.Entity<IdentityUserLogin<string>>().ToTable("IdentityUserLogin");
            modelbuilder.Entity<IdentityUserRole<string>>().ToTable("IdentityUser");
        }



    }
}
