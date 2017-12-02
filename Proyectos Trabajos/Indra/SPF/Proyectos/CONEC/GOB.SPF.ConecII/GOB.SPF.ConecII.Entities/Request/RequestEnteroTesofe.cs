using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOB.SPF.ConecII.Entities.Amatzin;

namespace GOB.SPF.ConecII.Entities.Request
{
    public class RequestEnteroTesofe : RequestBase
    {
        public List<EnteroTesofe> Lista { get; set; }
        public Documento Archivo { get; set; }
        public EnteroTesofeCriterio Criterio { get; set; }
    }
}
