using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsBroxel.App_Code.GenericBL
{
    /// <summary>
    /// Clase de persistencia para conocer el tipo de correo
    /// </summary>
    public class MailSelector
    {
        /// <summary>
        /// Tipo de correo
        /// </summary>
        public int Type { set; get; }
        /// <summary>
        /// booleano para activar la aparicion del disclaimer
        /// </summary>
        public bool Disclamer { set; get; }
        /// <summary>
        /// Fecha de vencimiento para disclaimer
        /// </summary>
        public DateTime FechaLimite { set; get; }
    }
}