using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcs.Entities.Mcs
{
    /// <summary>
    /// Clase de tipo entidad que representa el Tipo de Instalación.
    /// Módulo: MCS
    /// Base de datos: Sicogua.
    /// </summary>
    public class BeTipoInstalacion
    {
        /// <summary>
        /// Clave del tipo de instalación.
        /// </summary>
        public int IdTipoInstalacion { get; set; }
        /// <summary>
        /// Mombre o descripción del tipo de instalación.
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// Indica si el tipo de instalación es vigente.
        /// </summary>
        public bool Vigencia { get; set; }
    }
}
