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
    public partial class MAT_CAT_MATERIAS
    
    {
        public MAT_CAT_MATERIAS()
        {
            this.CLA_CLASE = new HashSet<CLA_CLASE>();
        }
    
    
    	[DataMember]
        public short IDMATERIA { get; set; }
    
    
    	[DataMember]
        public Nullable<short> IDCARRERA { get; set; }
    
    
    	[DataMember]
        public string NOMBREMATERIA { get; set; }
    
    
    	[DataMember]
        public Nullable<decimal> CREDITOS { get; set; }
    
    
        public virtual CAR_CAT_CARRERAS CAR_CAT_CARRERAS { get; set; }
        public virtual ICollection<CLA_CLASE> CLA_CLASE { get; set; }
    }
}
