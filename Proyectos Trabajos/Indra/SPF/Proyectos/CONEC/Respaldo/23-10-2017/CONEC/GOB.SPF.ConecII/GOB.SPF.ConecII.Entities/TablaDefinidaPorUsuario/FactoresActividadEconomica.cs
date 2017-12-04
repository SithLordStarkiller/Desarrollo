using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities.TablaDefinidaPorUsuario
{
    public class FactoresActividadEconomica
    {
        public int IdClasificadorFactor { get; set; }
        public int IdFactor { get; set; }
        public int IdFraccion { get; set; }
        public string Descripcion { get; set; }
    }
}
