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
    
    public partial class TransferenciasSolicitud
    {
        public long Id { get; set; }
        public string Folio { get; set; }
        public string UsuarioCreacion { get; set; }
        public System.DateTime FechaHoraCreacion { get; set; }
        public string Origen { get; set; }
        public string UsuarioAprobacion { get; set; }
        public string UsuarioEjecucion { get; set; }
        public System.DateTime FechaHoraEjecucion { get; set; }
        public System.DateTime FechaHoraAprobacion { get; set; }
        public string Tipo { get; set; }
        public string ClaveCliente { get; set; }
        public string Producto { get; set; }
        public string Estado { get; set; }
        public string Cliente { get; set; }
        public string RfcCliente { get; set; }
        public string Solicitante { get; set; }
        public string AreaSolicitante { get; set; }
        public string EmailNotificacion { get; set; }
        public Nullable<decimal> MontoTotal { get; set; }
        public Nullable<int> TotalDeCuentas { get; set; }
        public Nullable<int> CuentasRepetidas { get; set; }
        public Nullable<decimal> ValorEstimado { get; set; }
        public string UsuarioVerificacion { get; set; }
        public Nullable<System.DateTime> FechaHoraVerificacion { get; set; }
        public bool TransfSaldoTotal { get; set; }
        public string ConceptoTransferencia { get; set; }
    }
}
