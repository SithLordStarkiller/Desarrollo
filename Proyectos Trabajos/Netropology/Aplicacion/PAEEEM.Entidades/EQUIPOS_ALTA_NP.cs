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
    
    public partial class EQUIPOS_ALTA_NP
    {
        public int ID_ALTA { get; set; }
        public string No_RPU { get; set; }
        public int Cve_Producto { get; set; }
        public Nullable<int> No_Cantidad { get; set; }
        public Nullable<decimal> Mt_Precio_Unitario { get; set; }
        public Nullable<decimal> Mt_Precio_Unitario_Sin_IVA { get; set; }
        public Nullable<decimal> Mt_Total { get; set; }
        public Nullable<decimal> Mt_Gastos_Instalacion_Mano_Obra { get; set; }
        public string CapacidadSistema { get; set; }
        public string Grupo { get; set; }
        public Nullable<int> IdGrupo { get; set; }
        public Nullable<decimal> Incentivo { get; set; }
        public Nullable<int> Secuencia_Intento { get; set; }
        public string Adicionado_Por { get; set; }
        public Nullable<System.DateTime> Fecha_Adicion { get; set; }
    }
}