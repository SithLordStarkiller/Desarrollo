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
    public class BeInstalacion
    {
        /// <summary>
        /// Id del servicio
        /// </summary>
        public int IdServicio { get; set; }
        /// <summary>
        /// Id de la instalación.
        /// </summary>
        public int IdInstalacion { get; set; }
        /// <summary>
        /// Id de la zona.
        /// </summary>
        public int IdZone { get; set; }
        /// <summary>
        /// Nombre o descripción de la instalación.
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// Indica si la instalación es vigente.
        /// </summary>
        public bool Vigente { get; set; }
        /// <summary>
        /// Indica la Fecha de Inicio de la instalación  
        /// </summary>
        public DateTime FechaInicio { get; set; }
        /// <summary>
        /// Indica la Fecha de Fin de la instalación  
        /// </summary>
        public DateTime FechaFin { get; set; }
    }
}
