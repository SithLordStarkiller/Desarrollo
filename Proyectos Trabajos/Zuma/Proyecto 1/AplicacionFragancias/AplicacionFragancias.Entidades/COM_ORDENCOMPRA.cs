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
    
    public partial class COM_ORDENCOMPRA
    {
        public COM_ORDENCOMPRA()
        {
            this.COM_PRODUCTOS = new HashSet<COM_PRODUCTOS>();
        }
    
        public int IDORDENCOMPRA { get; set; }
        public Nullable<short> IDALAMACENES { get; set; }
        public Nullable<int> IDESTATUSCOMPRA { get; set; }
        public System.DateTime FECHAENTREGA { get; set; }
        public System.DateTime FECHAPEDIDO { get; set; }
        public decimal CANTIDADTOTAL { get; set; }
        public bool ENTREGAFRACCIONARIA { get; set; }
    
        public virtual ALM_CAT_ALMECENES ALM_CAT_ALMECENES { get; set; }
        public virtual COM_ESTATUS_COMPRA COM_ESTATUS_COMPRA { get; set; }
        public virtual ICollection<COM_PRODUCTOS> COM_PRODUCTOS { get; set; }
    }
}
