using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.Models
{
    public class UiExterno : UiEntity
    {
        public int Identificador { get; set; }
        public int IdCliente { get; set; }
        public int IdTipoContacto { get; set; }
        public string TipoContacto { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Cargo { get; set; }
        public bool Activo { get; set; }
    }
}