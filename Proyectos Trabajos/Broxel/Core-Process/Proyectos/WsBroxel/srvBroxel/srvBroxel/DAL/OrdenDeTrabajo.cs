//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace srvBroxel.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrdenDeTrabajo
    {
        public long Id { get; set; }
        public string Folio { get; set; }
        public string ClaveCliente { get; set; }
        public string UsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string Estado { get; set; }
        public Nullable<System.DateTime> FechaEnvio { get; set; }
        public string UsuarioEnvio { get; set; }
        public string NombreSolicitante { get; set; }
        public string EmailNotificacion { get; set; }
        public string CodigoDeProducto { get; set; }
        public string CodigoServicio { get; set; }
        public Nullable<decimal> PorcentajeBonificacion { get; set; }
        public string CodigoPeriodoBonificacion { get; set; }
        public string CantidadCuotasBonificacion { get; set; }
        public Nullable<System.DateTime> FechaDesde { get; set; }
        public string MarcaTPP { get; set; }
        public string GrupoDeLiquidacion { get; set; }
        public string HabilitaCompra { get; set; }
        public string File { get; set; }
        public string UsuarioGenera { get; set; }
        public Nullable<System.DateTime> FechaGenera { get; set; }
    }
}
