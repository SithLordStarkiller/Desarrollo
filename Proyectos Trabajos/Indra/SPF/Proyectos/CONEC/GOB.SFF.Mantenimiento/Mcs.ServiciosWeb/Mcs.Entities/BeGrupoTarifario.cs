using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcs.Entities
{
    /// <summary>
    /// Entidad del la tabla Grupo Tarifario de la BD de Cove.
    /// </summary>
    public class BeGrupoTarifario
    {
        /// <summary>
        /// Número consecutivo del grupo tarifario.
        /// </summary>
        public int IdGrupoTarifario { get; set; }
        /// <summary>
        /// Descripción o nombre del grupo tarifario.
        /// </summary>
        public string GrupoTarifario { get; set; }
        /// <summary>
        /// Nivel del grupo tarifario.
        /// </summary>
        public int Nivel { get; set; }
        /// <summary>
        /// Indica si el grupo tarifario esta activo.
        /// </summary>
        public bool Vigente { get; set; }
    }
}
