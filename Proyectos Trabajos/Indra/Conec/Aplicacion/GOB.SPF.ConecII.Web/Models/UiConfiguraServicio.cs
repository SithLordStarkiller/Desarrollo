using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiConfiguraServicio
    {
        public List<UiTiposPago> TipoPago { get; set; }
        public List<UiArea> Areas { get; set; }
        public List<UiTiposServicio> Servicios { get; set; }
        public List<UiActividad> Actividades { get; set; }
        public List<UiRegimenFiscal> RegimenFiscal { get; set; }
    }
}