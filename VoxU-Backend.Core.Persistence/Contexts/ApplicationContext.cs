using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxU_Backend.Core.Domain.Entities;

namespace VoxU_Backend.Core.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Publications> Publications { get; set; }
        public DbSet<SellPublications> SellPublications { get; set; }
        public DbSet<Replies> Replies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Comments>().ToTable("Comments");
            modelBuilder.Entity<Publications>().ToTable("Publications");
            modelBuilder.Entity<SellPublications>().ToTable("SellPublications");
            modelBuilder.Entity<Replies>().ToTable("Replies");

            modelBuilder.Entity<Comments>().HasKey(comment => comment.Id);
            modelBuilder.Entity<Publications>().HasKey(Publications => Publications.Id);
            modelBuilder.Entity<SellPublications>().HasKey(SellPublications => SellPublications.Id);
            modelBuilder.Entity<Replies>().HasKey(r => r.Id);

            modelBuilder.Entity<Publications>()
                .HasMany<Comments>(Publications => Publications.Comments)
                .WithOne(Comments => Comments.Publications)
                .HasForeignKey(p => p.IdPublication);

            modelBuilder.Entity<Comments>()
                .HasMany<Replies>(c => c.replies)
                .WithOne(reply => reply.Comments)
                .HasForeignKey(reply => reply.CommentId);

        }

    }
}
