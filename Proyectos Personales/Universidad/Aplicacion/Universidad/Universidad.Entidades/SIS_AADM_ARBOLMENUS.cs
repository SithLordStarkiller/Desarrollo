//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Universidad.Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class SIS_AADM_ARBOLMENUS
    {
        public SIS_AADM_ARBOLMENUS()
        {
            this.SIS_AADM_APLICACIONES = new HashSet<SIS_AADM_APLICACIONES>();
        }
    
        public int IDMENU { get; set; }
        public string NOMBRENODO { get; set; }
        public string RUTA { get; set; }
        public int IDMENUPADRE { get; set; }
    
        public virtual ICollection<SIS_AADM_APLICACIONES> SIS_AADM_APLICACIONES { get; set; }
    }
}