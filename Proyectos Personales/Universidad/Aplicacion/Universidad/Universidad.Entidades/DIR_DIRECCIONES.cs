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
    public partial class DIR_DIRECCIONES
    
    {
        public DIR_DIRECCIONES()
        {
            this.PER_PERSONAS = new HashSet<PER_PERSONAS>();
        }
    
    
    	[DataMember]
        public int IDDIRECCION { get; set; }
    
    
    	[DataMember]
        public Nullable<int> IDESTADO { get; set; }
    
    
    	[DataMember]
        public Nullable<int> IDMUNICIPIO { get; set; }
    
    
    	[DataMember]
        public Nullable<int> IDCOLONIA { get; set; }
    
    
    	[DataMember]
        public string CALLE { get; set; }
    
    
    	[DataMember]
        public string NOEXT { get; set; }
    
    
    	[DataMember]
        public string NOINT { get; set; }
    
    
    	[DataMember]
        public string REFERENCIAS { get; set; }
    
    
        public virtual DIR_CAT_COLONIAS DIR_CAT_COLONIAS { get; set; }
        public virtual DIR_CAT_ESTADO DIR_CAT_ESTADO { get; set; }
        public virtual ICollection<PER_PERSONAS> PER_PERSONAS { get; set; }
    }
}
