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
    public partial class HOR_CAT_HORAS
    
    {
        public HOR_CAT_HORAS()
        {
            this.HOR_HORAS_POR_DIA = new HashSet<HOR_HORAS_POR_DIA>();
        }
    
    
    	[DataMember]
        public short IDHORA { get; set; }
    
    
    	[DataMember]
        public Nullable<short> IDTURNO { get; set; }
    
    
    	[DataMember]
        public string NOMBREHORA { get; set; }
    
    
    	[DataMember]
        public System.DateTime HORAINICIO { get; set; }
    
    
    	[DataMember]
        public System.DateTime HORATERMINO { get; set; }
    
    
    	[DataMember]
        public string DESCRIPCION { get; set; }
    
    
        public virtual HOR_CAT_TURNO HOR_CAT_TURNO { get; set; }
        public virtual ICollection<HOR_HORAS_POR_DIA> HOR_HORAS_POR_DIA { get; set; }
    }
}
