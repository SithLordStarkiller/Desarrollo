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
    
    public partial class CAT_SE_COND_CONEXION
    {
        public CAT_SE_COND_CONEXION()
        {
            this.CAT_PRODUCTO = new HashSet<CAT_PRODUCTO>();
        }
    
        public int Cve_Conductor_Conex { get; set; }
        public string Dx_Dsc_Conductor_Conex { get; set; }
        public string Atributo_1 { get; set; }
        public string Atributo_2 { get; set; }
        public string Atributo_3 { get; set; }
        public string Atributo_4 { get; set; }
        public string Atributo_5 { get; set; }
    
        public virtual ICollection<CAT_PRODUCTO> CAT_PRODUCTO { get; set; }
    }
}
