using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class Asistente : Genericos.SubRegistro<int>
    {
        public int Identificador { get; set; }

        public Guid IdPersona { get; set; }

        public bool Activo { get; set; }
    }
}
