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
    
    public partial class MRV_CONSULTA_CONSUMOS
    {
        public MRV_CONSULTA_CONSUMOS()
        {
            this.MRV_HIST_CONSULTA_CONSUMOS = new HashSet<MRV_HIST_CONSULTA_CONSUMOS>();
        }
    
        public int IdConsultaConsumo { get; set; }
        public string No_Credito { get; set; }
        public Nullable<System.DateTime> FechaConsumo { get; set; }
        public Nullable<bool> Estatus { get; set; }
        public Nullable<System.DateTime> FechaAdicion { get; set; }
        public string AdicionadoPor { get; set; }
    
        public virtual ICollection<MRV_HIST_CONSULTA_CONSUMOS> MRV_HIST_CONSULTA_CONSUMOS { get; set; }
        public virtual CRE_Credito CRE_Credito { get; set; }
    }
}