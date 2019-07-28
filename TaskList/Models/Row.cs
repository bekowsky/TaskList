using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskList.Models
{
    public class Row
    {
        public int Id { get; set; }
        
        public  string Text { get; set; }
        public  int Number { get; set; }
        public  bool IsDone { get; set; }
        public int? ProjectId { get; set; }
        public Project Project { get; set; }
    }
}