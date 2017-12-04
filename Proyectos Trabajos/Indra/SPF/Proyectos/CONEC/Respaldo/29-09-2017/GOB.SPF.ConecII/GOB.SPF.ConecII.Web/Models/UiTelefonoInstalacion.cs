using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiTelefonoInstalacion:UiTelefono
    {
        public int Identificador { get; set; }
        public UiInstalacion Instalacion { get; set; }
    }
}