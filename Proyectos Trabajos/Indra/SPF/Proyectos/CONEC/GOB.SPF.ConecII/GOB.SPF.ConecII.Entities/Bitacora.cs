using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class Bitacora
    {
        public int IdUsuario { get; set; }
        public string IP { get; set; }
        public string MAC { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Activo { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public string Path { get; set; }
    }
}
