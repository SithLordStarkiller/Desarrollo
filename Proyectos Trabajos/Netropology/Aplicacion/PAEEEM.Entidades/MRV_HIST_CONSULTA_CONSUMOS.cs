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
    
    public partial class MRV_HIST_CONSULTA_CONSUMOS
    {
        public int IdConsultaConsumo { get; set; }
        public byte idHistorico { get; set; }
        public Nullable<System.DateTime> FechaPeriodo { get; set; }
        public Nullable<decimal> Consumo { get; set; }
        public Nullable<decimal> Demanda { get; set; }
        public Nullable<decimal> FactorPotencia { get; set; }
        public Nullable<byte> IdValido { get; set; }
        public Nullable<bool> Estatus { get; set; }
        public Nullable<System.DateTime> FechaAdicion { get; set; }
        public string AdicionadoPor { get; set; }
    
        public virtual MRV_CONSULTA_CONSUMOS MRV_CONSULTA_CONSUMOS { get; set; }
    }
}
