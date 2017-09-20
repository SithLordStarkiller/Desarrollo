namespace GOB.SPF.ConecII.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// Representa una excepcion
    /// </summary>
    public class UiException: Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UiException()
           : base()
        {
        }

        public UiException(string message)
            : base(message)
        {
        }

        public UiException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}