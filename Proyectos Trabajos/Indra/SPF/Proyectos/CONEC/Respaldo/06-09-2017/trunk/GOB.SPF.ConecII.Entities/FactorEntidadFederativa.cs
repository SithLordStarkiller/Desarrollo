using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class FactorEntidadFederativa : Request.RequestBase
    {

        public FactorEntidadFederativa()
        {
            Clasificacion = new ClasificacionFactor();
            Factor = new Factor();
            Estado = new Estado();
        }

        public int Identificador { get; set; }
        public int IdClasificadorFactor { get; set; }
        public int IdFactor { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicial { get; set; }

        public DateTime FechaFinal { get; set; }

        public bool Activo { get; set; }
        public List<ClasificacionFactor> ClasificacionesFactor { get; set; }
        public List<Factor> Factores { get; set; }
        public List<Estado> Estados { get; set; }

        public ClasificacionFactor Clasificacion { get; set; }

        public Factor Factor { get; set; }

        public Estado Estado { get; set; }
    }
}