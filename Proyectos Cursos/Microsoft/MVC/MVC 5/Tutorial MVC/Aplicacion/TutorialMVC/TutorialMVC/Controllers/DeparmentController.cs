using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutorialMVC.Models;

namespace TutorialMVC.Controllers
{
    public class DeparmentController : Controller
    {
        // GET: Deparment
        public ActionResult Index()
        {
            var employeeContext = new EmployeeContext();
            //var listDeparments = employeeContext.Deparments.ToList();
            var employee = employeeContext.Employees;
            return View(employee);
        }
    }
}