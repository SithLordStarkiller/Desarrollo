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
    
    public partial class DISTRIBUIDORES_ACTIVOS
    {
        public int IdDistribuidoresActivos { get; set; }
        public Nullable<int> Anio { get; set; }
        public Nullable<byte> Mes { get; set; }
        public string DescripcionMes { get; set; }
        public Nullable<int> NoDistActivos { get; set; }
        public Nullable<bool> Estatus { get; set; }
        public Nullable<System.DateTime> FechaAdicion { get; set; }
        public string AdicionadoPor { get; set; }
    }
}
