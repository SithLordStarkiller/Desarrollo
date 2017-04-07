using System;
using System.ComponentModel.DataAnnotations;

namespace PubliPayments.Entidades
{
    public class OpcionesFiltroDashboard
    {
        [Key]
        public String Value { get; set; }
        public String Description { get; set; }
    }
}