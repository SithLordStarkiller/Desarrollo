//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace wsServices01800
{
    using System;
    using System.Collections.Generic;
    
    public partial class PrecalificacionRequest
    {
        public long idPrecalificacion { get; set; }
        public long idOrden { get; set; }
        public string nss { get; set; }
        public string pen_alim { get; set; }
        public string usuario { get; set; }
        public string nombre_oficina { get; set; }
        public string oficina { get; set; }
    
        public virtual PrecalificacionResponse PrecalificacionResponse { get; set; }
    }
}
