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
    
    public partial class K_PROGRAMA_DET_FONDO
    {
        public int Fl_Prog_Proy_Fondo { get; set; }
        public Nullable<byte> ID_Prog_Proy { get; set; }
        public Nullable<int> Cve_Financiador { get; set; }
        public Nullable<decimal> Mt_Monto_Aportado { get; set; }
        public Nullable<System.DateTime> Dt_Fecha_Fondo { get; set; }
    
        public virtual CAT_FINANCIADOR CAT_FINANCIADOR { get; set; }
        public virtual CAT_PROGRAMA CAT_PROGRAMA { get; set; }
    }
}