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
    public partial class PER_FOTOGRAFIA
    
    {
        public PER_FOTOGRAFIA()
        {
            this.PER_PERSONAS = new HashSet<PER_PERSONAS>();
        }
    
    
    	[DataMember]
        public int IDFOTO { get; set; }
    
    
    	[DataMember]
        public string NOMBRE { get; set; }
    
    
    	[DataMember]
        public string EXTENCION { get; set; }
    
    
    	[DataMember]
        public byte[] FOTOGRAFIA { get; set; }
    
    
    	[DataMember]
        public Nullable<long> LONGITUD { get; set; }
    
    
        public virtual ICollection<PER_PERSONAS> PER_PERSONAS { get; set; }
    }
}
