using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsBroxel.App_Code.RequestResponses
{
    /// <summary>
    /// Respuesta de la api del preautorizador
    /// </summary>
    public class PreAutApiResponse
    {
        /// <summary>
        /// Código de respuesta
        /// </summary>
        public int ProcessingCode { set; get; }
        /// <summary>
        /// Numero de transacción
        /// </summary>
        public int TrackingNumber { set; get; }
        /// <summary>
        /// Descripción en caso de error
        /// </summary>
        public string Descripcion { set; get; }
    }
}