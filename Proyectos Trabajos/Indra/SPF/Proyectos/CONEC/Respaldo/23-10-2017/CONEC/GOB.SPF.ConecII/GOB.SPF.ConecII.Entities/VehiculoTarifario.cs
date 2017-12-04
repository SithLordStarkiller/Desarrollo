using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class VehiculoTarifario : Request.RequestBase
    {
        public int Identificador { get; set; }
        public string NombreGpoTarifario { get; set; }
        public string DescGpoTarifario { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public bool Activo { get; set; }
    }
}
