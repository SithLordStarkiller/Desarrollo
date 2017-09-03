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
        
        //public ClasificacionFactor ClasificacionFactor { get; set; }
        //public Factor Factor { get; set; }
        public string Descripcion { get; set; }
        //public Estado Estado { get; set; }

        public ClasificacionFactor Clasificacion { get; set; }

        public Factor Factor { get; set; }

        public Estado Estado { get; set; }

        public DateTime FechaInicial { get; set; }

        public DateTime FechaFinal { get; set; }

        public bool Activo { get; set; }
    }
}