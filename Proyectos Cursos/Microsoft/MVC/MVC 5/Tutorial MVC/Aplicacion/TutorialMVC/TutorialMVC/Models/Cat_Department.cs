using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using TutorialMVC.Models;
namespace TutorialMVC.Models
{
    [Table("Cat_Deparment")]
    public class Cat_Department
    {
        public int IdDeparment { get; set; }
        public string NameDeparment { set; get; }

        public List<Employee> Employees { get; set; }
    }
}