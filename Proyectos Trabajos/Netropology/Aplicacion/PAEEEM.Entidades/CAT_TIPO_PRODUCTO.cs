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
    
    public partial class CAT_TIPO_PRODUCTO
    {
        public CAT_TIPO_PRODUCTO()
        {
            this.ABE_VALORES_PRODUCTO = new HashSet<ABE_VALORES_PRODUCTO>();
            this.CAT_AHORRO_REFRIGERACION = new HashSet<CAT_AHORRO_REFRIGERACION>();
            this.CAT_PRODUCTO = new HashSet<CAT_PRODUCTO>();
            this.K_CREDITO_SUSTITUCION = new HashSet<K_CREDITO_SUSTITUCION>();
            this.SISTEMA_ARREGLO = new HashSet<SISTEMA_ARREGLO>();
        }
    
        public int Ft_Tipo_Producto { get; set; }
        public Nullable<int> Cve_Tecnologia { get; set; }
        public string Dx_Tipo_Producto { get; set; }
        public Nullable<bool> EQUIPO_ALTA { get; set; }
        public Nullable<bool> EQUIPO_BAJA { get; set; }
        public Nullable<bool> ESTATUS { get; set; }
        public Nullable<System.DateTime> FECHA_ADICION { get; set; }
        public string ADICIONADO_POR { get; set; }
    
        public virtual ICollection<ABE_VALORES_PRODUCTO> ABE_VALORES_PRODUCTO { get; set; }
        public virtual ICollection<CAT_AHORRO_REFRIGERACION> CAT_AHORRO_REFRIGERACION { get; set; }
        public virtual ICollection<CAT_PRODUCTO> CAT_PRODUCTO { get; set; }
        public virtual CAT_TECNOLOGIA CAT_TECNOLOGIA { get; set; }
        public virtual ICollection<K_CREDITO_SUSTITUCION> K_CREDITO_SUSTITUCION { get; set; }
        public virtual ICollection<SISTEMA_ARREGLO> SISTEMA_ARREGLO { get; set; }
    }
}
