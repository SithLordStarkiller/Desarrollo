using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities.DTO
{
    public class FactorEntidadFederativaDTO
    {
        public FactorEntidadFederativaDTO()
        {
            Clasificaciones = new List<ClasificacionFactor>();
            Estados = new List<Estado>();
            Factores = new List<Factor>();
        }

        public List<ClasificacionFactor> Clasificaciones { get; set; }
        public List<Factor> Factores { get; set; }

        public List<Estado> Estados { get; set; }

        public Factor Factor { get; set; }

        public ClasificacionFactor Clasificacion { get; set; }

        public string Descripcion { get; set; }

    }
}
