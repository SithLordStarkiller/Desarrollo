using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class RolModulo : Request.RequestBase
    {
        public int Identificador { get; set; }
        public int IdRol { get; set; }
        public int IdModulo { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public bool Activo { get; set; }
    }
}
