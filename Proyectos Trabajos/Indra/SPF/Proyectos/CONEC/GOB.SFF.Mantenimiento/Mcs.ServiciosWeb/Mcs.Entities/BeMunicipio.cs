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
    public class BeMunicipio
    {
        public int IdPais { get; set; }
        public int IdEstado { get; set; }
        public int IdMunicipio { get; set; }
        public string Descripcion { get; set; }
        public bool Vigente { get; set; }
    }
}
