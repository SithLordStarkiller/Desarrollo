using System;
using System.ComponentModel.DataAnnotations;

namespace PubliPayments.Entidades
{
    public class FiltrosDashboard
    {
        [Key]
        public String Delegacion { get; set; }
        public String Estado{ get; set; }
        public String Despacho { get; set; }
        public String Supervisor { get; set; }
        public String Gestor { get; set; }        
    }
}