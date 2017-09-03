using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class FactorMunicipio : TEntity
    {
        public override int Identificador { get; set; }
        public string DescFactMpio { get; set; }
        public int IdClasificadorFactor { get; set; }
        public int IdGrupo { get; set; }
        public int IdEntidFed { get; set; }
        public string DescEntidFed { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool Activo { get; set; }
        public int IdFactor { get; set; }

        public int IdMunicipio { get; set; }
    }
}