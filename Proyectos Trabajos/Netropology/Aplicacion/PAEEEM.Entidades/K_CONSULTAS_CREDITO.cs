//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PAEEEM.Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class K_CONSULTAS_CREDITO
    {
        public int Id_Paquete { get; set; }
        public Nullable<int> Id_Proveedor { get; set; }
        public string Fg_Tipo_Proveedor { get; set; }
        public Nullable<int> Cve_Zona { get; set; }
        public string Id_Codigo_Paquete { get; set; }
        public Nullable<int> Cve_Estatus_Paquete { get; set; }
        public Nullable<System.DateTime> Dt_Fecha_Envio { get; set; }
        public Nullable<System.DateTime> Dt_Fecha_Revision { get; set; }
        public Nullable<System.DateTime> Dt_Fecha_Rechazado { get; set; }
        public Nullable<System.DateTime> Dt_Fecha_Reenvio { get; set; }
        public Nullable<System.DateTime> Dt_Fecha_Aceptado { get; set; }
        public Nullable<System.DateTime> Dt_Fecha_Concluido { get; set; }
        public string Dx_Razon_Rechazo1 { get; set; }
        public string Dx_Razon_Rechazo2 { get; set; }
        public string Dx_Razon_Rechazo3 { get; set; }
        public string Dx_Razon_Rechazo4 { get; set; }
    }
}
