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
    public partial class SisArbolMenu
    
    {
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SisArbolMenu()
        {
            this.SisArbolMenu1 = new HashSet<SisArbolMenu>();
            this.SisTipoUsuarioPorMenu = new HashSet<SisTipoUsuarioPorMenu>();
        }
    
    	[DataMember]
        public int IdMenu { get; set; }
    
    	[DataMember]
        public Nullable<int> IdMenuPadre { get; set; }
    
    	[DataMember]
        public string NombreMenu { get; set; }
    
    	[DataMember]
        public string Controller { get; set; }
    
    	[DataMember]
        public string Method { get; set; }
    
    	[DataMember]
        public string Url { get; set; }
    
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SisArbolMenu> SisArbolMenu1 { get; set; }
        public virtual SisArbolMenu SisArbolMenu2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SisTipoUsuarioPorMenu> SisTipoUsuarioPorMenu { get; set; }
    }
}