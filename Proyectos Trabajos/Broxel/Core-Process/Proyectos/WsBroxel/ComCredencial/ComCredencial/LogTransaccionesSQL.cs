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
    
    public partial class LogTransaccionesSQL
    {
        public int Id { get; set; }
        public string NumCuenta { get; set; }
        public string NumTarjeta { get; set; }
        public string Accion { get; set; }
        public string Resultado { get; set; }
        public string Mensaje { get; set; }
        public string Usuario { get; set; }
        public System.DateTime FechaHora { get; set; }
        public string MetodoEjecucion { get; set; }
        public string WS { get; set; }
        public string NumAutorizacion { get; set; }
    }
}