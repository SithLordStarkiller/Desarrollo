using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Amatzin.Entities.Request
{
    public class RequestArchivo : RequestBase
    {

        public string Nombre { get; set; }
        public string Directorio { get; set; }
        public string Referencia { get; set; }
    }
}
