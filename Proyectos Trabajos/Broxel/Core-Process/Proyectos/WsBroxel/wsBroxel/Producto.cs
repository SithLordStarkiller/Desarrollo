//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace wsBroxel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Producto
    {
        public Producto()
        {
            this.ControlsCuentaAsociada = new HashSet<ControlsCuentaAsociada>();
        }
    
        public int Id { get; set; }
        public string NombreComercio { get; set; }
    
        public virtual ICollection<ControlsCuentaAsociada> ControlsCuentaAsociada { get; set; }
    }
}