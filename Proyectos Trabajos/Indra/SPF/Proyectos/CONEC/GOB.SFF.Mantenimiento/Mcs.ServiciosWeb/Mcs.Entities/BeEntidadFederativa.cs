using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcs.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class BeEntidadFederativa
    {
        public int IdPais { get; set; }
        public int IdEstado { get; set; }
        public string Estado { get; set; }
        public bool Vigente { get; set; }
    }
}
