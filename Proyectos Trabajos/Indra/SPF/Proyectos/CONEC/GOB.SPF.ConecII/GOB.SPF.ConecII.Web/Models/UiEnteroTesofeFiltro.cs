using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiEnteroTesofeFiltro
    {
        public DateTime? FechaPresentacionInicial { get; set; }
        public DateTime? FechaPresentacionFinal { get; set; }
        public string RazonSocial { get; set; }
        public string LlavePago { get; set; }
        public long? NumeroOperacion { get; set; }
        public string RFC { get; set; }
        public int? ClaveReferenciaDPA { get; set; }
        public DateTime? FechaCargaInicial { get; set; }
        public DateTime? FechaCargaFinal { get; set; }
    }
}