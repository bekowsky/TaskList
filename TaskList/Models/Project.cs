using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskList.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Describtion { get; set; }
        public bool IsDone { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Creator { get; set; }

        public string ReadPermission { get; set; }
        public string ChangePermission { get; set; }
        public string SettingsPermission { get; set; }

        public virtual ICollection<Row> Rows { get; set; }
        public virtual ICollection<User> Users { get; set; }

        public Project()
        {
           Rows = new List<Row>();
            Users = new List<User>();
        }
    }
}