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
    
    public partial class CAT_CAPACIDAD_SUSTITUCION
    {
        public CAT_CAPACIDAD_SUSTITUCION()
        {
            this.CAT_PRODUCTO = new HashSet<CAT_PRODUCTO>();
            this.CRE_CREDITO_EQUIPOS_BAJA = new HashSet<CRE_CREDITO_EQUIPOS_BAJA>();
            this.K_CREDITO_SUSTITUCION = new HashSet<K_CREDITO_SUSTITUCION>();
            this.K_PRODUCTO_CHARACTERS = new HashSet<K_PRODUCTO_CHARACTERS>();
        }
    
        public int Cve_Capacidad_Sust { get; set; }
        public Nullable<int> Cve_Tecnologia { get; set; }
        public string Dx_Capacidad { get; set; }
        public Nullable<double> No_Capacidad { get; set; }
        public string Dx_Unidades { get; set; }
        public Nullable<System.DateTime> Dt_Fe_Alta { get; set; }
        public Nullable<byte> IdSistemaArreglo { get; set; }
        public Nullable<bool> Estatus { get; set; }
        public Nullable<int> Ft_Tipo_Producto { get; set; }
    
        public virtual CAT_TECNOLOGIA CAT_TECNOLOGIA { get; set; }
        public virtual SISTEMA_ARREGLO SISTEMA_ARREGLO { get; set; }
        public virtual ICollection<CAT_PRODUCTO> CAT_PRODUCTO { get; set; }
        public virtual ICollection<CRE_CREDITO_EQUIPOS_BAJA> CRE_CREDITO_EQUIPOS_BAJA { get; set; }
        public virtual ICollection<K_CREDITO_SUSTITUCION> K_CREDITO_SUSTITUCION { get; set; }
        public virtual ICollection<K_PRODUCTO_CHARACTERS> K_PRODUCTO_CHARACTERS { get; set; }
    }
}