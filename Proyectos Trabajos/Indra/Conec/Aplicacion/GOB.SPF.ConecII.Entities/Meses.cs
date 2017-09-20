using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class Meses : TEntity
    {
        public override int Identificador { get; set; }
        public string Nombre { get; set; }
        public string DescMes { get; set; }
    }
}
