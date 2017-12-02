using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class Recibo : TEntity
    {
        public DateTime FechaEmision { get; set; }
        public string Folio { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }            
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }
    }
}
