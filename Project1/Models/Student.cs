using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project1.Models
{
    public class Student
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        public string Address { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Group_Id { get; set; }

        public Student()
        {
            Id = Guid.NewGuid().ToString();
            Birthday = DateTime.MinValue;
        }
    }
}