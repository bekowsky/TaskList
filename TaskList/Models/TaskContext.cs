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
            Database.SetInitializer<TaskContext>(null);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>().HasMany(c => c.Users)
                .WithMany(s => s.Projects)
                .Map(t => t.MapLeftKey("Project_Id")
                .MapRightKey("User_Id")
                .ToTable("UserProjects"));
        }

        public bool IsCorrect(string name, string password)
        {
            foreach (User user in Users)
                if (user.Name == name && user.Password == password)
                    return true;
            return false;
        }

        public User FindByName(string name)
        {
            foreach (User user in Users)
                if (user.Name == name)
                    return user;
            return null;
        }
        public Project FindByKey(string key)
        {
            foreach (Project project in Projects)
                if (project.Key == key)
                    return project;
            return null;
        }

    }
}