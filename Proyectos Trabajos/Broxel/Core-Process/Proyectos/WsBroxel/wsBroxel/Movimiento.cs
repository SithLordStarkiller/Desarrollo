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
    
    public partial class Movimiento
    {
        public Movimiento()
        {
            this.LogMensajes = new HashSet<LogMensajes>();
        }
    
        public int idMovimiento { get; set; }
        public string Tarjeta { get; set; }
        public string NombreTarjeta { get; set; }
        public string FechaExpira { get; set; }
        public Nullable<decimal> Monto { get; set; }
        public string NoReferencia { get; set; }
        public string NombreReferencia { get; set; }
        public Nullable<int> idLote { get; set; }
        public Nullable<bool> Autorizado { get; set; }
        public string NoAutorizacion { get; set; }
        public string MensajeError { get; set; }
        public string MensajeRespuesta { get; set; }
        public Nullable<int> idUsuario { get; set; }
        public Nullable<int> idComercio { get; set; }
        public Nullable<bool> Status { get; set; }
        public string UsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaHoraCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaHoraModificacion { get; set; }
        public Nullable<bool> ActivoLote { get; set; }
        public string CVC { get; set; }
        public bool RegControl { get; set; }
        public string telefono { get; set; }
        public string tipo_telefono { get; set; }
        public string exp_01 { get; set; }
        public string exp_02 { get; set; }
        public string email { get; set; }
        public Nullable<int> TipoTransaccion { get; set; }
        public Nullable<int> SubTipoTransaccion { get; set; }
        public string NumCuenta { get; set; }
    
        public virtual Comercio Comercio { get; set; }
        public virtual ICollection<LogMensajes> LogMensajes { get; set; }
    }
}
