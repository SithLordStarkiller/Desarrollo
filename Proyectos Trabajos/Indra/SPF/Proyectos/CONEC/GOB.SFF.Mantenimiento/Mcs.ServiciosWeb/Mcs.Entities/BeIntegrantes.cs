using System;

namespace Mcs.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class BeIntegrantes
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid IdEmpleado { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ApPaterno { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ApMaterno { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Correo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CorreoPersonal { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //public BeArea Area { get; set; }
        public string IdArea { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Area { get; set; }

        //public BeJerarquia Jerarquia { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int IdJerarquia { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Jerarquia { get; set; }
    }
}
