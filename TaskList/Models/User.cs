using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskList.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsMale { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Key { get; set; }
        public DateTime Date { get; set; }
        public DateTime WasOnline { get; set; }
        public byte Image { get; set; }


        public virtual ICollection<Project> Projects { get; set; }

        public User()
        {
            Projects = new List<Project>();
        }

        public User(string name, string password, string key)
        {
            Name = name;
            Password = password;
            Key = key;
            Projects = new List<Project>();
        }
    }

   
}