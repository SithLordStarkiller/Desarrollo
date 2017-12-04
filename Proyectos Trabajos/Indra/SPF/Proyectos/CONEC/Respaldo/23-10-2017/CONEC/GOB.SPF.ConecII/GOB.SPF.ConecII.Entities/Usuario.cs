using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class Usuario : Request.RequestBase
    {
        public int Identificador { get; set; }
        public int IdPersona { get; set; }
        public int IdExterno { get; set; }
        public string Login { get; set; }
        public byte Password { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public bool Activo { get; set; }
    }
}
