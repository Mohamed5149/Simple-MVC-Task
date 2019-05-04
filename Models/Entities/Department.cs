using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Auth.Models.Entities
{

    [Table("Department")]
    public class Department
    {
        public int Id { get; set; }

        [Required]
        public string Department_Name { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
