using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcs.Entities
{
    public class BeEstacion
    {
        public int IdEstacion { get; set; }
        public string Estacion { get; set; }

        public bool Vigente { get; set; }
    }
}
