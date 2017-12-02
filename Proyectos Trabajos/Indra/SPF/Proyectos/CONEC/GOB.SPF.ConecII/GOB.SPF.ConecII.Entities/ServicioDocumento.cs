using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class ServicioDocumento:Amatzin.Documento
    {
        public int Identificador { get; set; }
        public int IdServicio { get; set; }
        public string Observaciones { get; set; }
    }
}
