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
    public partial class CLA_CLASE
    
    {
        public CLA_CLASE()
        {
            this.ALU_HORARIO = new HashSet<ALU_HORARIO>();
            this.CLA_HORARIO = new HashSet<CLA_HORARIO>();
        }
    
    
    	[DataMember]
        public int IDCLASE { get; set; }
    
    
    	[DataMember]
        public Nullable<short> IDMATERIA { get; set; }
    
    
    	[DataMember]
        public Nullable<int> IDPROFESOR { get; set; }
    
    
    	[DataMember]
        public Nullable<short> IDCARRERA { get; set; }
    
    
        public virtual ICollection<ALU_HORARIO> ALU_HORARIO { get; set; }
        public virtual CAR_CAT_CARRERAS CAR_CAT_CARRERAS { get; set; }
        public virtual MAT_CAT_MATERIAS MAT_CAT_MATERIAS { get; set; }
        public virtual PRO_PROFESOR PRO_PROFESOR { get; set; }
        public virtual ICollection<CLA_HORARIO> CLA_HORARIO { get; set; }
    }
}
