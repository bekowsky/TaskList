using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskList.Models
{
    public class User
    {
        public int Id { get; set; }
        [StringLength(12, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        public string Name { get; set; }
        [StringLength(12, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        public string Password { get; set; }
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }
        [Required]
        public bool IsMale { get; set; }
        public string Country { get; set; }
        [StringLength(12, MinimumLength = 6, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
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