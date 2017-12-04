using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class Municipio
    {

        public int Identificador { get; set; }

        public string Nombre { get; set; }

        public Estado Estado { get; set; }

        public bool isActive { get; set; }
        public bool IsActive { get; set; }
    }
}
