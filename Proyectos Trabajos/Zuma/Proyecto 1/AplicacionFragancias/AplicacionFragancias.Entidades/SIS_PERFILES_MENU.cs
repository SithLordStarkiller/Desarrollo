//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AplicacionFragancias.Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class SIS_PERFILES_MENU
    {
        public short IDPERFILESMENU { get; set; }
        public Nullable<int> IDMENU { get; set; }
        public Nullable<short> IDPERFIL { get; set; }
    
        public virtual SIS_MENUARBOL SIS_MENUARBOL { get; set; }
        public virtual US_CAT_PERFILES US_CAT_PERFILES { get; set; }
    }
}