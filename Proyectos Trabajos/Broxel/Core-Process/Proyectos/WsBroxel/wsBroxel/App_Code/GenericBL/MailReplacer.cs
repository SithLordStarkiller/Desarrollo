using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsBroxel.App_Code.GenericBL
{
    /// <summary>
    /// Clase de persistencia para reemplazar etiquetas en correo
    /// </summary>
    public class MailReplacer
    {
        /// <summary>
        /// Etiqueta
        /// </summary>
        public string Tag { set; get; }
        /// <summary>
        /// Valor de la etiqueta
        /// </summary>
        public string Value { set; get; }
    }
}