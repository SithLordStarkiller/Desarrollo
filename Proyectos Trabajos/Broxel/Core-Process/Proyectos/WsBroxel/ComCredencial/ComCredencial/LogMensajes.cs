//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ComCredencial
{
    using System;
    using System.Collections.Generic;
    
    public partial class LogMensajes
    {
        public long IdLog { get; set; }
        public Nullable<int> IdMovimiento { get; set; }
        public Nullable<int> IdAnulacion { get; set; }
        public string numCuenta { get; set; }
        public int IdMetodo { get; set; }
        public int IdServicio { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public Nullable<System.DateTime> FechaFin { get; set; }
        public string request { get; set; }
        public string response { get; set; }
    
        public virtual AnulacionN AnulacionN { get; set; }
        public virtual Movimiento Movimiento { get; set; }
    }
}