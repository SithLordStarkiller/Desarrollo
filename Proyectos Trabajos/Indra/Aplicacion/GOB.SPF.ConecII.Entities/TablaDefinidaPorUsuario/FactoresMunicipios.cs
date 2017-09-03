using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities.TablaDefinidaPorUsuario
{
    public class FactoresMunicipios
    {
        public int IdFactor { get; set; }

        public int IdClasificacion { get; set; }

        public string Descripcion { get; set; }

        public int IdEstado { get; set; }

        public int IdMunicipio { get; set; }
    }
}
