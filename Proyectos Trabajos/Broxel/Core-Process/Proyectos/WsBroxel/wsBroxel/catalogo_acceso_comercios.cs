//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace wsBroxel
{
    using System;
    using System.Collections.Generic;
    
    public partial class catalogo_acceso_comercios
    {
        public int id { get; set; }
        public string usuario { get; set; }
        public Nullable<int> comercio { get; set; }
        public string password { get; set; }
        public string afiliacion { get; set; }
        public string Nombre { get; set; }
        public string AppellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> FechaHoraCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaHoraModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string Tipo { get; set; }
        public string Matriz { get; set; }
        public string ClaveCliente { get; set; }
        public Nullable<sbyte> accesoMTC { get; set; }
        public Nullable<sbyte> accesoPayments { get; set; }
    }
}
