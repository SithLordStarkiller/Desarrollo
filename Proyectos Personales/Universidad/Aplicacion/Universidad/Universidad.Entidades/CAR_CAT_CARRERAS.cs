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
    public partial class CAR_CAT_CARRERAS
    
    {
        public CAR_CAT_CARRERAS()
        {
            this.CLA_CLASE = new HashSet<CLA_CLASE>();
            this.MAT_CAT_MATERIAS = new HashSet<MAT_CAT_MATERIAS>();
        }
    
    
    	[DataMember]
        public short IDCARRERA { get; set; }
    
    
    	[DataMember]
        public Nullable<short> IDESPECIALIDAD { get; set; }
    
    
    	[DataMember]
        public string NOMBRECARRERA { get; set; }
    
    
        public virtual CAR_CAT_ESPECIALIDAD CAR_CAT_ESPECIALIDAD { get; set; }
        public virtual ICollection<CLA_CLASE> CLA_CLASE { get; set; }
        public virtual ICollection<MAT_CAT_MATERIAS> MAT_CAT_MATERIAS { get; set; }
    }
}
