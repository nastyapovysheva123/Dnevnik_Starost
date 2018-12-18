using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project1.Models
{
    public class Group
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public Group()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}