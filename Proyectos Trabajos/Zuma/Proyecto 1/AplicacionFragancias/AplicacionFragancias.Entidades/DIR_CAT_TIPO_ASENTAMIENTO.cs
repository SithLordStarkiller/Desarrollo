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
    
    public partial class DIR_CAT_TIPO_ASENTAMIENTO
    {
        public DIR_CAT_TIPO_ASENTAMIENTO()
        {
            this.DIR_CAT_COLONIAS = new HashSet<DIR_CAT_COLONIAS>();
        }
    
        public int IDTIPOASENTAMIENTO { get; set; }
        public string TIPOASENTAMIENTO { get; set; }
    
        public virtual ICollection<DIR_CAT_COLONIAS> DIR_CAT_COLONIAS { get; set; }
    }
}
