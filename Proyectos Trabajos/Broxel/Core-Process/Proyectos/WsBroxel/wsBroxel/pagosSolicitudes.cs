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
    
    public partial class pagosSolicitudes
    {
        public long id { get; set; }
        public string folio { get; set; }
        public string cliente { get; set; }
        public string claveCliente { get; set; }
        public string rfc { get; set; }
        public string solicitante { get; set; }
        public string areaSolicitante { get; set; }
        public string email { get; set; }
        public Nullable<double> montoPrincipal { get; set; }
        public string producto { get; set; }
        public string usuarioCreacion { get; set; }
        public System.DateTime fechaCreacion { get; set; }
        public string usuarioModificacion { get; set; }
        public Nullable<System.DateTime> fechaModificacion { get; set; }
        public string usuarioEjecucion { get; set; }
        public Nullable<System.DateTime> fechaEjecucion { get; set; }
        public string estado { get; set; }
        public string tipo { get; set; }
        public string usuarioVerificacion { get; set; }
        public Nullable<System.DateTime> fechaVerificacion { get; set; }
        public string usuarioAprobacion { get; set; }
        public Nullable<System.DateTime> fechaAprobacion { get; set; }
        public Nullable<decimal> total_cuentas { get; set; }
        public Nullable<decimal> cuentas_repetidas { get; set; }
        public Nullable<decimal> valor_estimado { get; set; }
    }
}
