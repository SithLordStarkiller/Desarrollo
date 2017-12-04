using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class InstalacionCliente:Instalacion
    {
        public int IdCliente { get; set; }
        public string RazonSocial  { get; set; }
        public string NombreCorto  { get; set; }
        public string Rfc  { get; set; }
    }
}
