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
    
    public partial class COM_CAT_ESTATUS_PRODUCTO
    {
        public COM_CAT_ESTATUS_PRODUCTO()
        {
            this.COM_PRODUCTOS_PEDIDOS = new HashSet<COM_PRODUCTOS_PEDIDOS>();
        }
    
        public short IDESTAUSPRODUCTO { get; set; }
        public string ESTATUSPRODUCTO { get; set; }
        public string DESCRIPCION { get; set; }
        public bool BORRADO { get; set; }
    
        public virtual ICollection<COM_PRODUCTOS_PEDIDOS> COM_PRODUCTOS_PEDIDOS { get; set; }
    }
}
