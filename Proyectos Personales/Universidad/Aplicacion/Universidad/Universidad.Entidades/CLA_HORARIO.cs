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
    public partial class CLA_HORARIO
    
    {
    
    	[DataMember]
        public short IDHORARIO { get; set; }
    
    
    	[DataMember]
        public Nullable<short> IDHORARIOMATERIA { get; set; }
    
    
    	[DataMember]
        public Nullable<int> IDCLASE { get; set; }
    
    
        public virtual CLA_CLASE CLA_CLASE { get; set; }
        public virtual MAT_HORARIO_POR_MATERIA MAT_HORARIO_POR_MATERIA { get; set; }
    }
}
