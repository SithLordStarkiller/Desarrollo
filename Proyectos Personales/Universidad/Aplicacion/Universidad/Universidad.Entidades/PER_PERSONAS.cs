//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Universidad.Entidades
{
    using System.Runtime.Serialization;
    using System;
    using System.Collections.Generic;
    
    
    [DataContract]
    [Serializable]
    public partial class PER_PERSONAS
    
    {
        public PER_PERSONAS()
        {
            this.ALU_ALUMNOS = new HashSet<ALU_ALUMNOS>();
            this.PRO_PROFESOR = new HashSet<PRO_PROFESOR>();
        }
    
    
    	[DataMember]
        public int ID_PERSONA { get; set; }
    
    
    	[DataMember]
        public string ID_PER_LINKID { get; set; }
    
    
    	[DataMember]
        public Nullable<int> IDDIRECCION { get; set; }
    
    
    	[DataMember]
        public Nullable<int> CVE_NACIONALIDAD { get; set; }
    
    
    	[DataMember]
        public Nullable<int> ID_TELEFONOS { get; set; }
    
    
    	[DataMember]
        public Nullable<int> ID_TIPO_PERSONA { get; set; }
    
    
    	[DataMember]
        public Nullable<int> ID_USUARIO { get; set; }
    
    
    	[DataMember]
        public Nullable<int> ID_MEDIOS_ELECTRONICOS { get; set; }
    
    
    	[DataMember]
        public Nullable<int> IDFOTO { get; set; }
    
    
    	[DataMember]
        public string NOMBRE { get; set; }
    
    
    	[DataMember]
        public string A_PATERNO { get; set; }
    
    
    	[DataMember]
        public string A_MATERNO { get; set; }
    
    
    	[DataMember]
        public string NOMBRE_COMPLETO { get; set; }
    
    
    	[DataMember]
        public System.DateTime FECHA_NAC { get; set; }
    
    
    	[DataMember]
        public System.DateTime FECHAINGRESO { get; set; }
    
    
    	[DataMember]
        public string SEXO { get; set; }
    
    
    	[DataMember]
        public string CURP { get; set; }
    
    
    	[DataMember]
        public string RFC { get; set; }
    
    
    	[DataMember]
        public string IMSS { get; set; }
    
    
        public virtual ICollection<ALU_ALUMNOS> ALU_ALUMNOS { get; set; }
        public virtual DIR_DIRECCIONES DIR_DIRECCIONES { get; set; }
        public virtual PER_CAT_NACIONALIDAD PER_CAT_NACIONALIDAD { get; set; }
        public virtual PER_CAT_TELEFONOS PER_CAT_TELEFONOS { get; set; }
        public virtual PER_CAT_TIPO_PERSONA PER_CAT_TIPO_PERSONA { get; set; }
        public virtual PER_FOTOGRAFIA PER_FOTOGRAFIA { get; set; }
        public virtual PER_MEDIOS_ELECTRONICOS PER_MEDIOS_ELECTRONICOS { get; set; }
        public virtual ICollection<PRO_PROFESOR> PRO_PROFESOR { get; set; }
        public virtual US_USUARIOS US_USUARIOS { get; set; }
    }
}
