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
    
    public partial class ControlsCuentaAsociada
    {
        public int Id { get; set; }
        public int IdRegla { get; set; }
        public int IdCuentaAsociada { get; set; }
        public bool EstatusControl { get; set; }
        public bool EstatusPadre { get; set; }
        public int IdProducto { get; set; }
        public Nullable<int> Metodo { get; set; }
    
        public virtual Producto Producto { get; set; }
        public virtual CuentaAsociadaFamily CuentaAsociadaFamily { get; set; }
    }
}