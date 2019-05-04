using Auth.Models;
using Auth.Models.Entities;
using Auth.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auth.Controllers
{
    [Authorize(Roles ="Admin,Manager")]
    public class DepartmentController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Department
        [Authorize]
        public ActionResult Index()
        {
            EmployeeViewModel EVM = new EmployeeViewModel();
            EVM.Departments = context.Departments.ToList();
            return View(EVM);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddDepartment(Department department)
        {
            EmployeeViewModel EVM = new EmployeeViewModel();
            EVM.Departments = context.Departments.ToList();
            if (ModelState.IsValid)
            {
                context.Departments.Add(department);
                context.SaveChanges();
                return PartialView("_PartialDepartmentRaw", department);
            }
            return View(EVM);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult EditDepartment(int id)
        {
            Department dept = context.Departments.FirstOrDefault(e => e.Id == id);
            return View("AddDepartment", dept);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult EditDepartment(Department department)
        {
            Department oldDept = context.Departments.FirstOrDefault(d => d.Id == department.Id);
            if (ModelState.IsValid)
            {
                if (oldDept != null)
                {
                    oldDept.Department_Name = department.Department_Name;
                    context.SaveChanges();
                    return JavaScript("window.location=`/Department/Index`");
                }
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteDepartment(int id)
        {
            Department department = context.Departments.FirstOrDefault(d => d.Id == id);
            context.Departments.Remove(department);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
