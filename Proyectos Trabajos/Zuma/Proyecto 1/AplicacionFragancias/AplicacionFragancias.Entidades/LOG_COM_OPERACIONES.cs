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
    
    public partial class LOG_COM_OPERACIONES
    {
        public LOG_COM_OPERACIONES()
        {
            this.COM_CAT_TIPO_OPERACION = new HashSet<COM_CAT_TIPO_OPERACION>();
        }
    
        public int IDLOGOPERACIONES { get; set; }
        public string TIPOOPERACION { get; set; }
        public string DESCRIPCION { get; set; }
    
        public virtual ICollection<COM_CAT_TIPO_OPERACION> COM_CAT_TIPO_OPERACION { get; set; }
    }
}