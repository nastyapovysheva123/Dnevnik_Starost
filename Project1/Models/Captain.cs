using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project1.Models
{
    public class Captain
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string PassHash { get; set; }
        public string Note { get; set; }
        public string StudentId { get; set; }
        public string GroupId { get; set; }
    }
}