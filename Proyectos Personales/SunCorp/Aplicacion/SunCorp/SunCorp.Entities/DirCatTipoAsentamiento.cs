//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SunCorp.Entities
{
    
    using System;
    using System.Collections.Generic;
    
    using System.Runtime.Serialization;
    
    [DataContract]
    public partial class DirCatTipoAsentamiento
    
    {
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DirCatTipoAsentamiento()
        {
            this.DirCatColonia = new HashSet<DirCatColonia>();
        }
    
    	[DataMember]
        public int IdTipoAsentmiento { get; set; }
    
    	[DataMember]
        public string TipoAsentamiento { get; set; }
    
    	[DataMember]
        public Nullable<bool> Borrado { get; set; }
    
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DirCatColonia> DirCatColonia { get; set; }
    }
}