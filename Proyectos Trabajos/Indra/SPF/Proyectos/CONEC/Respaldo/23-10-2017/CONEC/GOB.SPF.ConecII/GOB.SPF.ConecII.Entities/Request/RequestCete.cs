using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.Entities.Amatzin;

namespace GOB.SPF.ConecII.Entities.Request
{
    public class RequestCete : RequestBase
    {
        public List<Cete> Lista { get; set; }
        public Documento Archivo { get; set; }

        public CeteCriterio Criterio { get; set; }
    }
}
