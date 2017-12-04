using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities.TablaDefinidaPorUsuario
{
    public class FactoresEntidadFederativa
    {
        public int IdClasificadorFactor { get; set; }
        public int IdFactor { get; set; }
        public string Descripcion { get; set; }
        public int IdEntidFed { get; set; }
        public DateTime FechaInicial { get; set; }
    }
}
