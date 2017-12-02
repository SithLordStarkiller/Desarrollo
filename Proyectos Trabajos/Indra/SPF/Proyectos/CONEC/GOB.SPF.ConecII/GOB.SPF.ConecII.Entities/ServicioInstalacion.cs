using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class ServicioInstalacion : Instalacion
    {
        public bool Seleccionado { get; set; }
        public CotizacionDetalle CotizacionDetalle { get; set; }
        public string DireccionCompleta { get; set; }
        public Factor FactorActividad { get; set; }
        public Factor FactorDistancia { get; set; }
        public Factor FactorCriminalidad { get; set; }
    }
}
