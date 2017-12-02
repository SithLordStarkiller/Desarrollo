using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiEstatus
    {
        public int Identificador { get; set; }
        public string Nombre { get; set; }
        public UiEntidadNegocio EntidadNegocio { get; set; }
    }
}