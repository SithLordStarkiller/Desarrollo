using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities.DTO
{
    public class FactorMunicipioDTO
    {
        public FactorMunicipioDTO()
        {
            Clasificaciones = new List<ClasificacionFactor>();
            Estados = new List<Estado>();
        }

        public List<ClasificacionFactor> Clasificaciones { get; set; }

        public List<Estado> Estados { get; set; }

        public List<Municipio> Municipios { get; set; }

        public int IdFactor { get; set; }

        public int IdClasificacion { get; set; }

        public string Descripcion { get; set; }

        public int IdEstado { get; set; }
    }
}
