using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class FactorMunicipio : Request.RequestBase
    {
        public FactorMunicipio()
        {
            Clasificacion = new ClasificacionFactor();
            Factor = new Factor();
            Estado = new Estado();
            Municipio = new Municipio();
        }
        public int Identificador { get; set; }
        public string Descripcion { get; set; }
        public int IdClasificacionFactor { get; set; }
        public int IdGrupo { get; set; }
        public int IdEstado { get; set; }
        public List<ClasificacionFactor> ClasificacionesFactor { get; set; }
        public List<Factor> Factores { get; set; }
        public List<Estado> Estados { get; set; }
        public ClasificacionFactor Clasificacion { get; set; }
        public Factor Factor { get; set; }
        public Estado Estado { get; set; }
        public Municipio Municipio { get; set; }
        public Municipio Municipios { get; set; }
        public string NomEstado { get; set; }
        public string NomMunicipios { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool Activo { get; set; }
        public int IdFactor { get; set; }
        public int IdMunicipio { get; set; }
    }
}