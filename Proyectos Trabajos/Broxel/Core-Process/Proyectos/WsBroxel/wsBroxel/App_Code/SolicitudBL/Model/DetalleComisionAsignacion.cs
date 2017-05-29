using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wsBroxel.App_Code.SolicitudBL.Model
{
    /// <summary>
    /// Detalle de las comisiones por el uso de red de pagos.
    /// </summary>
    public class DetalleComisionAsignacion
    {
        /// <summary>
        /// Id del detalle comisión.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Valor al que se descontara del monto.
        /// </summary>
        public decimal Valor { get; set; }
        /// <summary>
        /// Tipo de aplicación del descuento, si es monto o porcentaje.
        /// </summary>
        public string TipoAplica { get; set; }
        /// <summary>
        /// Id del comercio.
        /// </summary>
        public int idComercio { get; set; }
        /// <summary>
        /// Nombre del comercio.
        /// </summary>
        public string NombreComercio { get; set; }
    }
}
