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
    
    public partial class AccionesTarjetaAsociada
    {
        public AccionesTarjetaAsociada()
        {
            this.ConfigTarjetaAsociada = new HashSet<ConfigTarjetaAsociada>();
        }
    
        public int Id { get; set; }
        public string Accion { get; set; }
    
        public virtual ICollection<ConfigTarjetaAsociada> ConfigTarjetaAsociada { get; set; }
    }
}
