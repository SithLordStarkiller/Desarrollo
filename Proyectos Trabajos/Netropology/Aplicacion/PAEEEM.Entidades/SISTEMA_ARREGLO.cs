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
    
    public partial class SISTEMA_ARREGLO
    {
        public SISTEMA_ARREGLO()
        {
            this.CAT_CAPACIDAD_SUSTITUCION = new HashSet<CAT_CAPACIDAD_SUSTITUCION>();
            this.K_CREDITO_SUSTITUCION = new HashSet<K_CREDITO_SUSTITUCION>();
            this.K_CREDITO_SUSTITUCION1 = new HashSet<K_CREDITO_SUSTITUCION>();
        }
    
        public byte IdSistemaArreglo { get; set; }
        public string SistemaArreglo { get; set; }
        public Nullable<decimal> Potencia { get; set; }
        public int Cve_Tecnologia { get; set; }
        public int Ft_Tipo_Producto { get; set; }
        public Nullable<bool> Estatus { get; set; }
        public Nullable<System.DateTime> FechaAdicion { get; set; }
        public string AdicionadoPor { get; set; }
    
        public virtual ICollection<CAT_CAPACIDAD_SUSTITUCION> CAT_CAPACIDAD_SUSTITUCION { get; set; }
        public virtual CAT_TECNOLOGIA CAT_TECNOLOGIA { get; set; }
        public virtual CAT_TIPO_PRODUCTO CAT_TIPO_PRODUCTO { get; set; }
        public virtual ICollection<K_CREDITO_SUSTITUCION> K_CREDITO_SUSTITUCION { get; set; }
        public virtual ICollection<K_CREDITO_SUSTITUCION> K_CREDITO_SUSTITUCION1 { get; set; }
    }
}