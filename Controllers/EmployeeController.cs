using Auth.Models;
using Auth.Models.Entities;
using Auth.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auth.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Employee
        public ActionResult Index()
        {
            EmployeeViewModel EVM = new EmployeeViewModel();
            EVM.Departments = context.Departments.ToList();
            EVM.Employees = context.Employees.Include(e => e.Department).ToList();
            return View(EVM);
        }

        //ADD: Add Employee
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult AddEmployee(Employee employee)
        {
            EmployeeViewModel EVM = new EmployeeViewModel();
            EVM.Departments = context.Departments.ToList();
            EVM.Employees = context.Employees.ToList();
            if (ModelState.IsValid)
            {
                context.Employees.Add(employee);
                context.SaveChanges();
                return PartialView("_PartialEmployeeRaw",employee);
            }
            EVM.Employee = employee;
            ViewBag.action = "Fire";
            return PartialView("_PartialAddEmployeeModal",EVM);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult EditEmployee(int id)
        {
            Employee emp = context.Employees.FirstOrDefault(e => e.Id == id);
            EmployeeViewModel EVM = new EmployeeViewModel();
            EVM.Employee = emp;
            EVM.Departments = context.Departments.ToList();
            EVM.Employees = context.Employees.ToList();
            return View("AddEmployee", EVM);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult EditEmployee(Employee employee)
        {
            Employee old = context.Employees.FirstOrDefault(e => e.Id == employee.Id);
            if (ModelState.IsValid)
            {
                if (old != null)
                {
                    old.Name = employee.Name;
                    old.Age = employee.Age;
                    old.Email = employee.Email;
                    context.SaveChanges();
                    return JavaScript("window.location=`/Employee/Index`");
                }
            }
            EmployeeViewModel EVM = new EmployeeViewModel();
            EVM.Departments = context.Departments.ToList();
            EVM.Employees = context.Employees.ToList();
            EVM.Employee = employee;
            return View("AddEmployee", EVM);
        }

        [HttpGet]
        public ActionResult ShowEmployee(int id)
        {
            Employee emp = context.Employees.FirstOrDefault(e => e.Id == id);
            return View(emp);
        }

        [Authorize(Roles = "Admin,Manager")]
        public ActionResult DeleteEmployee(int id)
        {
            Employee emp = context.Employees.FirstOrDefault(e => e.Id == id);
            if (emp != null)
            {
                context.Employees.Remove(emp);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
