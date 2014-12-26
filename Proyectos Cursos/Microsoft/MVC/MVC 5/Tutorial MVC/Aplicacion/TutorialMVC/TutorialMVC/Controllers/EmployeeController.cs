using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutorialMVC.Models;

namespace TutorialMVC.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Details(/*int IdEmployee*/)
        {
            var IdEmployee = 1;
            var employeeContext = new EmployeeContext();
            var employee = employeeContext.Employees;//employeeContext.Employees.Single(emp => emp.IdEmployee == IdEmployee);
            
            return View(employee);
        }
    }
}