using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsBroxel.App_Code.RequestResponses
{
    /// <summary>
    /// Respuesta del preautorizador
    /// </summary>
    public class PreAutResponse
    {
        /// <summary>
        /// Identificador de la transacción realizada, cero en caso de error
        /// </summary>
        public int IdTransaccion { get; set; }
        /// <summary>
        /// Mensaje de error en caso de que IdTransaccion sea cero.
        /// </summary>
        public string Message { get; set; }
    }
}