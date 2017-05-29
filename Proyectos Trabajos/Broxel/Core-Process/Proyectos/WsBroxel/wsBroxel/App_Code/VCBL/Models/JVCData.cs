using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsBroxel.App_Code.VCBL.Models
{

    /// <summary>
    /// Datos de tarjeta Virtual para JSON
    /// </summary>
    public class JVCData
    {
        /// <summary>
        /// Numero de tarjeta 16 posiciones
        /// </summary>
        public string Tarjeta { set; get; }
        /// <summary>
        /// Fecha de vencimiento MM/YY
        /// </summary>
        public string FechaVencimiento { set; get; }
        /// <summary>
        /// CVV
        /// </summary>
        public string Cvv { set; get; }
        /// <summary>
        /// Nombre del tarjetahabiente
        /// </summary>
        public string Nombre { set; get; }
        /// <summary>
        /// Clabe interbancaria de la cuenta
        /// </summary>
        public string Clabe { set; get; }
        /// <summary>
        /// Fecha de consulta de la información.
        /// </summary>
        public string FechaConsulta { set; get; }
    }
}