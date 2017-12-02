using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcs.Entities.Mcs
{
    /// <summary>
    /// Clase entidad del catálogo de paises.
    /// </summary>
    public class BePais
    {
        /// <summary>
        /// Clave del país.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del país
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// Indica si el pais es vigente o activo.
        /// </summary>
        public bool Vigente { get; set; }
        /// <summary>
        /// Descripción de la nacionalidad del paía.
        /// </summary>
        //public string Nacionalidad { get; set; }
    }
}
