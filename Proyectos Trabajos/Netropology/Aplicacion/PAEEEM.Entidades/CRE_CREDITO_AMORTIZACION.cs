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
    
    public partial class CRE_CREDITO_AMORTIZACION
    {
        public string RPU { get; set; }
        public int No_Pago { get; set; }
        public Nullable<System.DateTime> Dt_Fecha { get; set; }
        public Nullable<int> No_Dias_Periodo { get; set; }
        public Nullable<decimal> Mt_Capital { get; set; }
        public Nullable<decimal> Mt_Interes { get; set; }
        public Nullable<decimal> Mt_IVA { get; set; }
        public Nullable<decimal> Mt_Pago { get; set; }
        public Nullable<decimal> Mt_Amortizacion { get; set; }
        public Nullable<decimal> Mt_Saldo { get; set; }
        public Nullable<System.DateTime> Dt_Fecha_Credito_Amortización { get; set; }
    }
}