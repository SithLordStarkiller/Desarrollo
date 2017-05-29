using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsBroxel.App_Code.SolicitudBL.Model
{
    /// <summary>
    /// Clase de persistencia para obtener la información de una cuenta dada.
    /// </summary>
    public class InfoCuentaSolicitud
    {
        /// <summary>
        /// CLABE interbancaria del cliente corporativo
        /// </summary>
        public string Clabe { set; get; }
        /// <summary>
        /// Producto de la cuenta
        /// </summary>
        public string Producto { set; get; }
        /// <summary>
        /// Clave del cliente corporativo
        /// </summary>
        public string ClaveCliente { set; get; }
        /// <summary>
        /// Numero de cuenta, se usa para obtener la cuenta asociada a la tarjeta virtual
        /// </summary>
        public string Cuenta { set; get; }
    }
}