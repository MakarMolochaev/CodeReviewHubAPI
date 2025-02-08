using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CodePublication>()
                .HasOne(cp => cp.Creator)
                .WithMany(u => u.Publications)
                .HasForeignKey(cp => cp.CreatorId)
                .OnDelete(DeleteBehavior.Cascade);






            modelBuilder.Entity<User>(eb =>
            {
                eb.HasKey(u => u.Id);
                eb.Property(u => u.Id)
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("GEN_RANDOM_UUID()")
                    .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Throw);
            });







            modelBuilder.Entity<CodePublication>(eb =>
            {
                eb.HasKey(u => u.Id);
                eb.Property(u => u.Id)
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("GEN_RANDOM_UUID()")
                    .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Throw);
            });

            modelBuilder.Entity<CodePublication>()
                .Property(b => b.RatedUsers)
                .HasDefaultValueSql("ARRAY[]::uuid[]");
            
            modelBuilder.Entity<CodePublication>()
                .Property(b => b.Rating)
                .IsRequired();
            /*
            modelBuilder.Entity<CodePublication>()
            .Property(u => u.RatedUsers)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(g => Guid.Parse(g)).ToList());
            */


            modelBuilder.Entity<Comment>(eb =>
            {
                eb.HasKey(u => u.Id);
                eb.Property(u => u.Id)
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("GEN_RANDOM_UUID()")
                    .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Throw);
            });

            modelBuilder.Entity<CodePublication>()
                .Property(b => b.RatedUsers)
                .HasDefaultValueSql("ARRAY[]::uuid[]");
        }
    }
}