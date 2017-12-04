using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities.Response
{
    public class ResponseFactorEntidadFederativa
    {
        public List<ClasificacionFactor> LClasificacionFactor { get; set; }
        public List<MedidaCobro> LMedidaCobro { get; set; }
        public List<TipoServicio> LTipoServicio { get; set; }
    }
}
