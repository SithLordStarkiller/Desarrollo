using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcs.Entities
{
    /// <summary>
    /// Entidad del la tabla Jerarquias de la BD de Personal.
    /// </summary>
    public class BeJerarquia
    {
        /// <summary>
        /// Clave de la Jerarquia.
        /// </summary>
        public int IdJerarquia { get; set; }
        /// <summary>
        /// Nombre de la Jerarquia.
        /// </summary>
        public string Jerarquia { get; set; }
        /// <summary>
        /// Nivel 
        /// </summary>
        public int Nivel { get; set; }
        /// <summary>
        /// Indica si esta activa la Jerarquia.
        /// </summary>
        public bool Vigente { get; set; }

    }
}
