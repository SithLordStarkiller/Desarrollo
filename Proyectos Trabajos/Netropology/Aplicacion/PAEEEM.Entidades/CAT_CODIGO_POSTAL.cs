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
    
    public partial class CAT_CODIGO_POSTAL
    {
        public int Cve_CP { get; set; }
        public string Codigo_Postal { get; set; }
        public string Dx_Colonia { get; set; }
        public string Dx_Tipo_Colonia { get; set; }
        public Nullable<int> Cve_Deleg_Municipio { get; set; }
        public int Cve_Estado { get; set; }
    
        public virtual CAT_DELEG_MUNICIPIO CAT_DELEG_MUNICIPIO { get; set; }
        public virtual CAT_ESTADO CAT_ESTADO { get; set; }
    }
}
