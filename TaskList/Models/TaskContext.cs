using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TaskList.Models
{
    public class TaskContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Row> Rows { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().HasMany(c => c.Users)
                .WithMany(s => s.Projects)
                .Map(t => t.MapLeftKey("ProjectId")
                .MapRightKey("UserId")
                .ToTable("ProjectUser"));
        }
    }
}