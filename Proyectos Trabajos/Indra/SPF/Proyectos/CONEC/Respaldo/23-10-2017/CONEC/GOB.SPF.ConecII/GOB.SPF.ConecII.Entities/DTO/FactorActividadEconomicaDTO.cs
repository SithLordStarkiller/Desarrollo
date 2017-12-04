using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities.DTO
{
    public class FactorActividadEconomicaDTO
    {
        public FactorActividadEconomicaDTO()
        {
            Clasificaciones = new List<ClasificacionFactor>();
            Factores = new List<Factor>();
            Divisiones = new List<Division>();
            Grupos = new List<Grupo>();
            Fracciones = new List<Fraccion>();
        }
        public int Identificador { get; set; }
        public string Descripcion { get; set; }
        public int IdFraccion { get; set; }
        public int IdClasificacionFactor { get; set; }
        public int IdFactor { get; set; }
        public int IdGrupo { get; set; }
        public int IdDivision { get; set; }
        public ClasificacionFactor Clasificacion { get; set; }
        public List<ClasificacionFactor> Clasificaciones { get; set; }
        public Factor Factor { get; set; }
        public List<Factor> Factores { get; set; }
        public Fraccion Fraccion { get; set; }
        public List<Fraccion> Fracciones { get; set; }
        public Division Division { get; set; }
        public List<Division> Divisiones { get; set; }
        public Grupo Grupo { get; set; }
        public List<Grupo> Grupos { get; set; }
        public bool Activo { get; set; }

    }
}
