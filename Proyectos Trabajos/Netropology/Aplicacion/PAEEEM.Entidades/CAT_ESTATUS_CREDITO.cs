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
    
    public partial class CAT_ESTATUS_CREDITO
    {
        public CAT_ESTATUS_CREDITO()
        {
            this.ACCION_ESTATUS = new HashSet<ACCION_ESTATUS>();
            this.CRE_Credito = new HashSet<CRE_Credito>();
            this.MOTIVOS_RECHAZOS_CANCELACIONES = new HashSet<MOTIVOS_RECHAZOS_CANCELACIONES>();
        }
    
        public byte Cve_Estatus_Credito { get; set; }
        public string Dx_Estatus_Credito { get; set; }
        public Nullable<System.DateTime> Dt_Fecha_Estatus_Credito { get; set; }
    
        public virtual ICollection<ACCION_ESTATUS> ACCION_ESTATUS { get; set; }
        public virtual ICollection<CRE_Credito> CRE_Credito { get; set; }
        public virtual ICollection<MOTIVOS_RECHAZOS_CANCELACIONES> MOTIVOS_RECHAZOS_CANCELACIONES { get; set; }
    }
}
