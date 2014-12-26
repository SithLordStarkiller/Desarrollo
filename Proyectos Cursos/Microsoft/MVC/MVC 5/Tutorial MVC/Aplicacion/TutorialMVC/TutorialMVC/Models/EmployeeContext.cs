using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace TutorialMVC.Models
{
    public class EmployeeContext : DbContext
    {
        public DbSet<Employee> Employees{ get; set; }
        //public DbSet<Cat_Department> Deparments { get; set; }
    }
}