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
            this.COM_PRODUCTOS_PEDIDOS = new HashSet<COM_PRODUCTOS_PEDIDOS>();
        }
    
        public string NOORDENCOMPRA { get; set; }
        public Nullable<int> IDESTATUSCOMPRA { get; set; }
        public Nullable<int> IDIMPUESTO { get; set; }
        public Nullable<int> IDMONEDA { get; set; }
        public string CVEPROVEEDOR { get; set; }
        public Nullable<short> IDCONDICIONESPAGO { get; set; }
        public System.DateTime FECHAORDENCOMPRA { get; set; }
        public System.DateTime FECHAPEDIDO { get; set; }
        public System.DateTime FECHAENTREGA { get; set; }
        public bool ENTREGAFRACCIONARIA { get; set; }
        public decimal SUBTOTAL { get; set; }
        public decimal TOTAL { get; set; }
        public bool BORRADO { get; set; }
    
        public virtual COM_CAT_ESTATUS_COMPRA COM_CAT_ESTATUS_COMPRA { get; set; }
        public virtual COM_PROVEEDORES COM_PROVEEDORES { get; set; }
        public virtual FAC_CAT_IMPUESTO FAC_CAT_IMPUESTO { get; set; }
        public virtual FAC_CAT_MONEDA FAC_CAT_MONEDA { get; set; }
        public virtual FAC_CAT_CONDICIONES_PAGO FAC_CAT_CONDICIONES_PAGO { get; set; }
        public virtual ICollection<COM_PRODUCTOS_PEDIDOS> COM_PRODUCTOS_PEDIDOS { get; set; }
    }
}
