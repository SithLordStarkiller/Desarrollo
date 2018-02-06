namespace ExamenMvcEf.Helpers.CustomException
{
    using System;

    /// <summary>
    /// Clase que implementa una excepcion personalizada para detectar si un usuario no realizo un acceso
    /// </summary>
    public class DefaultException : Exception
    {
        /// <summary>
        /// Codigo asignado a este error o excepcion
        /// </summary>
        public string CodigoError { get; set; }

        /// <summary>
        /// para almacenar la fecha en que ocuerrio el error
        /// </summary>
        public DateTime Fecha { get; }

        public DefaultException() : base("Excepcion no identificada")
        {
            CodigoError = "ADVNORSERV00001";
            Fecha = DateTime.Now;
        }
    }
}
