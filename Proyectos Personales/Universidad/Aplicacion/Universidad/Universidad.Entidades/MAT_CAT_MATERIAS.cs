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
            this.CAL_CALIFICACIONES = new HashSet<CAL_CALIFICACIONES>();
            this.CLA_CLASE = new HashSet<CLA_CLASE>();
            this.MAT_ARBOL_MATERIA = new HashSet<MAT_ARBOL_MATERIA>();
            this.MAT_MATERIAS_POR_CARRERA = new HashSet<MAT_MATERIAS_POR_CARRERA>();
        }
    
    
    	[DataMember]
        public short IDMATERIA { get; set; }
    
    
    	[DataMember]
        public string NOMBREMATERIA { get; set; }
    
    
    	[DataMember]
        public decimal CREDITOS { get; set; }
    
    
    	[DataMember]
        public Nullable<bool> OPTATIVA { get; set; }
    
    
        public virtual ICollection<CAL_CALIFICACIONES> CAL_CALIFICACIONES { get; set; }
        public virtual ICollection<CLA_CLASE> CLA_CLASE { get; set; }
        public virtual ICollection<MAT_ARBOL_MATERIA> MAT_ARBOL_MATERIA { get; set; }
        public virtual ICollection<MAT_MATERIAS_POR_CARRERA> MAT_MATERIAS_POR_CARRERA { get; set; }
    }
}
