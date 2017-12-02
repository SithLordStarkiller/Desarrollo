using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.Interfaces;

namespace GOB.SPF.ConecII.Entities
{
    public class RolModulo : IRolModulo
    {
        public int Id { get; set; }
        public IRol Rol { get; set; }
        public IModulo Modulo { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime? FechaFinal { get; set; }
        public bool Activo { get; set; }
    }
}
