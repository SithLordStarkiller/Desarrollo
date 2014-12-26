using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TutorialMVC.Models
{
    [Table("Emp_Employee")]
    public class Employee
    {   
        public int IdEmployee { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public int IdDeparment { get; set; }
    }
}