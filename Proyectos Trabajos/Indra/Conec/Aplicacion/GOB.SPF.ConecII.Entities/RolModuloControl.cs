using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class RolModuloControl : Request.RequestBase
    {
        public int Identificador { get; set; }
        public int IdRolModulo { get; set; }
        public int IdControl { get; set; }
        public bool Captura { get; set; }
        public bool Consulta { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public bool Activo { get; set; }
    }
}
