using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsBroxel.App_Code.VCBL.Models
{
    /// <summary>
    /// Datos de tarjeta virtual (publicos)
    /// </summary>
    public class VCardData
    {
        /// <summary>
        /// Identificador de transacción
        /// </summary>
        public long IdTran { set; get; }
        /// <summary>
        ///  Track de información 1
        /// </summary>
        public string Track1 { set; get; }
        /// <summary>
        ///  Track de información 2
        /// </summary>
        public string Track2 { set; get; }
    }
}