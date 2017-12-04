using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class Area : Request.RequestBase
    {
        public int Identificador { get; set; }

        public string IdCentroCosto { get; set; }

        public string Nombre { get; set; }

        public bool IsActive { get; set; }
    }
}
