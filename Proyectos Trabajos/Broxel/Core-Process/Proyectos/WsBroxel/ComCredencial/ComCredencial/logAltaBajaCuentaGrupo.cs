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
    
    public partial class logAltaBajaCuentaGrupo
    {
        public int id { get; set; }
        public Nullable<int> idUsuario { get; set; }
        public string cuenta { get; set; }
        public Nullable<sbyte> accion { get; set; }
        public Nullable<int> idGrupo { get; set; }
        public System.DateTime fechaCreacion { get; set; }
        public Nullable<int> processingCode { get; set; }
        public Nullable<int> trackingNumber { get; set; }
        public string descripcion { get; set; }
        public Nullable<System.DateTime> fechaRespuesta { get; set; }
    }
}