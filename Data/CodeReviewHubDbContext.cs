using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class CodeReviewHubDbContext : DbContext
    {
        public CodeReviewHubDbContext(DbContextOptions<CodeReviewHubDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CodePublication> CodePublications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Publications)
                .WithOne(p => p.Creator)
                .HasForeignKey(p => p.CreatorId);
        }
    }
}