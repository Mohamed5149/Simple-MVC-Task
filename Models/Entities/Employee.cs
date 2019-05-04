using Auth.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Auth.Models.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [UIHint("string")]
        [Display(Name="Full Name")]
        public string Name { get; set; }

        [Range(10, 100)]
        public int Age { get; set; }

        public Gender Gender { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public virtual Department Department { get; set; }

        [ForeignKey("Department")]
        public int FK_Department_id { get; set; }
    }
}