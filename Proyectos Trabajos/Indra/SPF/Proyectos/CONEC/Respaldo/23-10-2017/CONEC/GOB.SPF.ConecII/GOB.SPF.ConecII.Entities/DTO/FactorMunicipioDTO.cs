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
            Factores = new List<Factor>();
            Estados = new List<Estado>();
            Municipios = new List<Municipio>();
        }

        public int Identificador { get; set; }
        public List<ClasificacionFactor> Clasificaciones { get; set; }
        public List<Factor> Factores { get; set; }
        public List<Estado> Estados { get; set; }
        public List<Municipio> Municipios { get; set; }
        public Factor Factor { get; set; }
        public Estado Estado { get; set; }
        public Municipio Municipio { get; set; }
        public ClasificacionFactor Clasificacion { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
    }
}
