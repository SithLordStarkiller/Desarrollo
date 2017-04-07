using System;
using System.ComponentModel.DataAnnotations;

namespace PubliPayments.Entidades
{
    public class IndicadorDashboard
    {
        [Key]
        public String FcClave { get; set; }
        public String FcDescripcion { get; set; }
        public long FiParte { get; set; }
        public int? FiValue { get ; set; }
        public String FiPorcentaje { get; set; }
        public long? FiOrden { get; set; }
        

    }
}