using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsBroxel.App_Code.SolicitudBL.Model
{
    /// <summary>
    /// Modelo para Json cambio de contraseña
    /// </summary>
    public class CambiaContrasenaRequest
    {
        /// <summary>
        /// HandShake = Br0x3l6789
        /// </summary>
        public string Hd { set; get; }
        /// <summary>
        /// Fecha de generación de la peticion
        /// </summary>
        public string Fecha { set; get; }
        /// <summary>
        /// Nuevo password a setear, encriptado
        /// </summary>
        public string Pwd { set; get; }
        /// <summary>
        /// Id de usuario en IdBroxelOnline, idSecure
        /// </summary>
        public int IdUser { set; get; }
        /// <summary>
        /// Id App
        /// </summary>
        public int IdApp { set; get; }
    }
}