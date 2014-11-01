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
    
    public partial class CAT_ESTADO
    {
        public CAT_ESTADO()
        {
            this.ABE_HORAS_TECNOLOGIA = new HashSet<ABE_HORAS_TECNOLOGIA>();
            this.ABE_REGIONES_MUNICIPIO_FACT_BIO = new HashSet<ABE_REGIONES_MUNICIPIO_FACT_BIO>();
            this.AUX_DATOS_TRAMA = new HashSet<AUX_DATOS_TRAMA>();
            this.CAT_CENTRO_DISP = new HashSet<CAT_CENTRO_DISP>();
            this.CAT_CENTRO_DISP1 = new HashSet<CAT_CENTRO_DISP>();
            this.CAT_CENTRO_DISP_SUCURSAL = new HashSet<CAT_CENTRO_DISP_SUCURSAL>();
            this.CAT_CENTRO_DISP_SUCURSAL1 = new HashSet<CAT_CENTRO_DISP_SUCURSAL>();
            this.CAT_CODIGO_POSTAL = new HashSet<CAT_CODIGO_POSTAL>();
            this.CAT_CODIGO_POSTAL_SEPOMEX = new HashSet<CAT_CODIGO_POSTAL_SEPOMEX>();
            this.CAT_DELEG_MUNICIPIO = new HashSet<CAT_DELEG_MUNICIPIO>();
            this.CAT_PROVEEDOR = new HashSet<CAT_PROVEEDOR>();
            this.CAT_PROVEEDOR1 = new HashSet<CAT_PROVEEDOR>();
            this.CAT_PROVEEDORBRANCH = new HashSet<CAT_PROVEEDORBRANCH>();
            this.CAT_PROVEEDORBRANCH1 = new HashSet<CAT_PROVEEDORBRANCH>();
            this.DIR_Direcciones = new HashSet<DIR_Direcciones>();
            this.K_TARIFA_COSTO = new HashSet<K_TARIFA_COSTO>();
        }
    
        public int Cve_Estado { get; set; }
        public string Dx_Nombre_Estado { get; set; }
        public Nullable<System.DateTime> Dt_Estado { get; set; }
        public string Dx_Cve_CC { get; set; }
        public string Dx_Cve_PM { get; set; }
        public string Dx_Cve_Trama { get; set; }
        public int dx_id_region_tarifa { get; set; }
        public Nullable<int> IDREGION_BIOCLIMA { get; set; }
    
        public virtual ICollection<ABE_HORAS_TECNOLOGIA> ABE_HORAS_TECNOLOGIA { get; set; }
        public virtual ABE_REGIONES_BIOCLIMATICAS ABE_REGIONES_BIOCLIMATICAS { get; set; }
        public virtual ICollection<ABE_REGIONES_MUNICIPIO_FACT_BIO> ABE_REGIONES_MUNICIPIO_FACT_BIO { get; set; }
        public virtual ICollection<AUX_DATOS_TRAMA> AUX_DATOS_TRAMA { get; set; }
        public virtual ICollection<CAT_CENTRO_DISP> CAT_CENTRO_DISP { get; set; }
        public virtual ICollection<CAT_CENTRO_DISP> CAT_CENTRO_DISP1 { get; set; }
        public virtual ICollection<CAT_CENTRO_DISP_SUCURSAL> CAT_CENTRO_DISP_SUCURSAL { get; set; }
        public virtual ICollection<CAT_CENTRO_DISP_SUCURSAL> CAT_CENTRO_DISP_SUCURSAL1 { get; set; }
        public virtual ICollection<CAT_CODIGO_POSTAL> CAT_CODIGO_POSTAL { get; set; }
        public virtual ICollection<CAT_CODIGO_POSTAL_SEPOMEX> CAT_CODIGO_POSTAL_SEPOMEX { get; set; }
        public virtual ICollection<CAT_DELEG_MUNICIPIO> CAT_DELEG_MUNICIPIO { get; set; }
        public virtual ICollection<CAT_PROVEEDOR> CAT_PROVEEDOR { get; set; }
        public virtual ICollection<CAT_PROVEEDOR> CAT_PROVEEDOR1 { get; set; }
        public virtual ICollection<CAT_PROVEEDORBRANCH> CAT_PROVEEDORBRANCH { get; set; }
        public virtual ICollection<CAT_PROVEEDORBRANCH> CAT_PROVEEDORBRANCH1 { get; set; }
        public virtual ICollection<DIR_Direcciones> DIR_Direcciones { get; set; }
        public virtual ICollection<K_TARIFA_COSTO> K_TARIFA_COSTO { get; set; }
    }
}
