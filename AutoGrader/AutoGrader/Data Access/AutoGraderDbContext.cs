using System;
using AutoGrader.Models.Assignment;
using Microsoft.EntityFrameworkCore;

namespace AutoGrader.DataAccess
{
    public class AutoGraderDbContext : DbContext
    {
        public AutoGraderDbContext(DbContextOptions<AutoGraderDbContext> contextOptions) : base(contextOptions) { }

        public DbSet<Assignment> Assignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
