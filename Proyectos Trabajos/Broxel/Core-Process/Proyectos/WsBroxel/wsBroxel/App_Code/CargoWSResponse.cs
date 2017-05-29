
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsBroxel.App_Code
{
    /// <summary>
    /// Respuesta a cargos ws
    /// </summary>
    public class CargoWSResponse:DispResponse
    {
        /// <summary>
        /// Fecha y Hora de la operacion
        /// </summary>
        public DateTime FechaHoraOperacion { set; get; }
        /// <summary>
        /// Numero de autorización
        /// </summary>
        public string NoAutorizacion { set; get; }
    }
}