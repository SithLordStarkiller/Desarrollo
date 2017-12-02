using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcs.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class BeArea
    {
        /// <summary>
        /// Clave del centro de costo (area).
        /// </summary>
        public string IdCentroCosto { get; set; }
        /// <summary>
        /// Descripción del centro de costo (área)
        /// </summary>
        public string CcDescripcion { get; set; }
        /// <summary>
        /// Indica si el centro de costo (área).
        /// </summary>
        public bool CcVigente { get; set; }
    }
}
