using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class Estado:Request.RequestBase
    {
        public int Identificador { get; set; }
        public int IdPais { get; set; }
        public string Nombre { get; set; }

        public bool IsActive { get; set; }
    }
}
