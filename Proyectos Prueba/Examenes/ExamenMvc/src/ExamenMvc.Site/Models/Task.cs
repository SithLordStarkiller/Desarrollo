namespace ExamenMvc.Site.Models
{
    using System;

    namespace ExamenMVC.Models
    {
        public partial class Task
        {
            public Decimal TaskId { get; set; }
            public Decimal EmployeeId { get; set; }
            public string TaskName { get; set; }
        }
    }

}
