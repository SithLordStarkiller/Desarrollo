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
    
    public partial class CAT_TECNOLOGIA
    {
        public CAT_TECNOLOGIA()
        {
            this.ABE_UNIDAD_TECNOLOGIA = new HashSet<ABE_UNIDAD_TECNOLOGIA>();
            this.ABE_UNIDAD_TECNOLOGIA1 = new HashSet<ABE_UNIDAD_TECNOLOGIA>();
            this.CAT_CAPACIDAD_SUSTITUCION = new HashSet<CAT_CAPACIDAD_SUSTITUCION>();
            this.CAT_COMBINACION_TECNOLOGIAS = new HashSet<CAT_COMBINACION_TECNOLOGIAS>();
            this.CAT_PRODUCTO = new HashSet<CAT_PRODUCTO>();
            this.CAT_PRODUCTO_CAPACIDAD = new HashSet<CAT_PRODUCTO_CAPACIDAD>();
            this.CAT_TARIFAS_X_TECNOLOGIA = new HashSet<CAT_TARIFAS_X_TECNOLOGIA>();
            this.CAT_TIPO_PRODUCTO = new HashSet<CAT_TIPO_PRODUCTO>();
            this.CRE_CREDITO_EQUIPOS_BAJA = new HashSet<CRE_CREDITO_EQUIPOS_BAJA>();
            this.FACTOR_DEGRADACION = new HashSet<FACTOR_DEGRADACION>();
            this.K_CENTRO_DISP_TECNOLOGIA = new HashSet<K_CENTRO_DISP_TECNOLOGIA>();
            this.K_CREDITO_SUSTITUCION = new HashSet<K_CREDITO_SUSTITUCION>();
            this.K_PROG_TECNOLOGIA = new HashSet<K_PROG_TECNOLOGIA>();
            this.K_TECNOLOGIA_RESIDUO_MATERIAL = new HashSet<K_TECNOLOGIA_RESIDUO_MATERIAL>();
            this.Load_LayOut = new HashSet<Load_LayOut>();
            this.SISTEMA_ARREGLO = new HashSet<SISTEMA_ARREGLO>();
        }
    
        public int Cve_Tecnologia { get; set; }
        public string Dx_Nombre_General { get; set; }
        public string Dx_Nombre_Particular { get; set; }
        public System.DateTime Dt_Fecha_Tecnologoia { get; set; }
        public Nullable<int> Cve_Tipo_Tecnologia { get; set; }
        public string Dx_Cve_CC { get; set; }
        public Nullable<int> Cve_Esquema { get; set; }
        public Nullable<int> Cve_Gasto { get; set; }
        public string Cve_Tipo_Movimiento { get; set; }
        public Nullable<int> Cve_Equipos_Baja { get; set; }
        public Nullable<int> Cve_Equipos_Alta { get; set; }
        public Nullable<int> Cve_Chatarrizacion { get; set; }
        public Nullable<decimal> Monto_Chatarrizacion { get; set; }
        public Nullable<int> Cve_Factor_Sustitucion { get; set; }
        public Nullable<int> Cve_Incentivo { get; set; }
        public Nullable<decimal> Monto_Incentivo { get; set; }
        public Nullable<int> Combina_Tecnologias { get; set; }
        public Nullable<int> Estatus { get; set; }
        public string Adicionado_Por { get; set; }
        public Nullable<int> Cve_Plantilla { get; set; }
        public Nullable<int> Cve_Leyenda { get; set; }
        public Nullable<byte> NumeroGrupos { get; set; }
    
        public virtual ICollection<ABE_UNIDAD_TECNOLOGIA> ABE_UNIDAD_TECNOLOGIA { get; set; }
        public virtual ICollection<ABE_UNIDAD_TECNOLOGIA> ABE_UNIDAD_TECNOLOGIA1 { get; set; }
        public virtual ICollection<CAT_CAPACIDAD_SUSTITUCION> CAT_CAPACIDAD_SUSTITUCION { get; set; }
        public virtual ICollection<CAT_COMBINACION_TECNOLOGIAS> CAT_COMBINACION_TECNOLOGIAS { get; set; }
        public virtual CAT_EQUIPOS_BAJA_ALTA CAT_EQUIPOS_BAJA_ALTA { get; set; }
        public virtual CAT_EQUIPOS_BAJA_ALTA CAT_EQUIPOS_BAJA_ALTA1 { get; set; }
        public virtual CAT_FACTOR_SUSTITUCION CAT_FACTOR_SUSTITUCION { get; set; }
        public virtual CAT_PLANTILLA CAT_PLANTILLA { get; set; }
        public virtual ICollection<CAT_PRODUCTO> CAT_PRODUCTO { get; set; }
        public virtual ICollection<CAT_PRODUCTO_CAPACIDAD> CAT_PRODUCTO_CAPACIDAD { get; set; }
        public virtual ICollection<CAT_TARIFAS_X_TECNOLOGIA> CAT_TARIFAS_X_TECNOLOGIA { get; set; }
        public virtual CAT_TIPO_MOVIMIENTO CAT_TIPO_MOVIMIENTO { get; set; }
        public virtual CAT_TIPO_TECNOLOGIA CAT_TIPO_TECNOLOGIA { get; set; }
        public virtual ICollection<CAT_TIPO_PRODUCTO> CAT_TIPO_PRODUCTO { get; set; }
        public virtual ICollection<CRE_CREDITO_EQUIPOS_BAJA> CRE_CREDITO_EQUIPOS_BAJA { get; set; }
        public virtual ICollection<FACTOR_DEGRADACION> FACTOR_DEGRADACION { get; set; }
        public virtual ICollection<K_CENTRO_DISP_TECNOLOGIA> K_CENTRO_DISP_TECNOLOGIA { get; set; }
        public virtual ICollection<K_CREDITO_SUSTITUCION> K_CREDITO_SUSTITUCION { get; set; }
        public virtual ICollection<K_PROG_TECNOLOGIA> K_PROG_TECNOLOGIA { get; set; }
        public virtual ICollection<K_TECNOLOGIA_RESIDUO_MATERIAL> K_TECNOLOGIA_RESIDUO_MATERIAL { get; set; }
        public virtual ICollection<Load_LayOut> Load_LayOut { get; set; }
        public virtual ICollection<SISTEMA_ARREGLO> SISTEMA_ARREGLO { get; set; }
    }
}