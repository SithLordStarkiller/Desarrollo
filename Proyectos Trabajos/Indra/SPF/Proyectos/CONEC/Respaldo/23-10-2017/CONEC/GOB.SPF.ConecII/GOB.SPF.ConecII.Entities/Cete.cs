using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOB.SPF.ConecII.Entities
{
    public class Cete : TEntity
    {
	    public DateTime Fecha { get; set; }
        public decimal TasaRendimiento { get; set; }
    }
}
