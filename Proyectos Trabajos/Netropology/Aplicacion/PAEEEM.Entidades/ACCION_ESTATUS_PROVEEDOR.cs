//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PAEEEM.Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class ACCION_ESTATUS_PROVEEDOR
    {
        public byte ID { get; set; }
        public byte ID_Acciones { get; set; }
        public int Cve_Estatus_Proveedor { get; set; }
        public bool Estatus { get; set; }
        public string Adicionado_Por { get; set; }
        public System.DateTime Fecha_Adicion { get; set; }
        public string Modificado_Por { get; set; }
        public Nullable<System.DateTime> Fecha_Modificacion { get; set; }
    
        public virtual CAT_ACCIONES CAT_ACCIONES { get; set; }
        public virtual CAT_ESTATUS_PROVEEDOR CAT_ESTATUS_PROVEEDOR { get; set; }
    }
}
