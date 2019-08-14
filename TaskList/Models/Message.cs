using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskList.Models
{
    public class Message
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsSender { get; set; }

        public int? FriendId { get; set; }
        public Friend Friend { get; set; }
    }
}