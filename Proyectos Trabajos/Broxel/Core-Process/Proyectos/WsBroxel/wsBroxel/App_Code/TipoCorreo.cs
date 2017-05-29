using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsBroxel
{
    public enum TipoCorreo
    {
        /// <summary>
        /// Transferencia SPEI
        /// </summary>
        SPEI = 1,
        /// <summary>
        /// Transferencia tarjeta a tarjeta broxel
        /// </summary>
        C2C,
        /// <summary>
        /// Transferencia entre mis tarjetas favoritas
        /// </summary>
        Favoritas,
        /// <summary>
        /// Transferencia entre mis tarjetas
        /// </summary>
        MisTarjetas,
        /// <summary>
        /// Transferencia a otras tarjetas broxel
        /// </summary>
        OtrasTarjetas,
        /// <summary>
        /// Transferencia con el flujo Anterior
        /// </summary>
        Anterior
    }
}