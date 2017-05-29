using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsBroxel.App_Code.RequestResponses
{
    /// <summary>
    /// Clase de retorno para grupos preautorizador
    /// </summary>
    public class GruposPreAut
    {
        /// <summary>
        /// Id de Grupo
        /// </summary>
        public int IdGrupo { set; get; }
        /// <summary>
        /// Descripcion del grupo
        /// </summary>
        public string Descripcion { set; get; }
    }
}