using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskList.Models
{
    public class LightUser
    {
        public string Name { get; set; }
        public string Id { get; set; }

        public LightUser(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}