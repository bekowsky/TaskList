using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskList.Models
{
    public class Friend
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte Image { get; set; }
        public DateTime Online { get; set; }
        public bool IsAssept { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public Friend()
        {
            Messages = new List<Message>();
        }
    }

}