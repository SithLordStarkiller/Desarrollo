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
    
    public partial class RenominacionSolicitudes
    {
        public long Id { get; set; }
        public string Folio { get; set; }
        public string ClaveCliente { get; set; }
        public string Producto { get; set; }
        public string Cliente { get; set; }
        public string RFC { get; set; }
        public string Solicitante { get; set; }
        public string AreaSolicitante { get; set; }
        public string Email { get; set; }
        public string UsuarioCreacion { get; set; }
        public System.DateTime FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string UsuarioEjecucion { get; set; }
        public Nullable<System.DateTime> FechaEjecucion { get; set; }
        public string Estado { get; set; }
        public string Tipo { get; set; }
        public string UsuarioVerificacion { get; set; }
        public Nullable<System.DateTime> FechaVerificacion { get; set; }
        public string UsuarioAprobacion { get; set; }
        public Nullable<System.DateTime> FechaAprobacion { get; set; }
        public Nullable<decimal> TotalCuentas { get; set; }
        public Nullable<decimal> CuentasRepetidas { get; set; }
    }
}
