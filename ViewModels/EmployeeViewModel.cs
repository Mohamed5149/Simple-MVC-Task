using Auth.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auth.ViewModels
{
    public class EmployeeViewModel
    {
        public Employee Employee { get; set; }
        public Department Department { get; set; }
        public List<Department> Departments { get; set; }
        public List<Employee> Employees { get; set; }
    }
}