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
    
    public partial class CAT_SE_COND_CONEXION_MARCA
    {
        public CAT_SE_COND_CONEXION_MARCA()
        {
            this.CAT_PRODUCTO = new HashSet<CAT_PRODUCTO>();
        }
    
        public int Cve_Marca { get; set; }
        public string Dx_Nombre_Marca { get; set; }
    
        public virtual ICollection<CAT_PRODUCTO> CAT_PRODUCTO { get; set; }
    }
}
