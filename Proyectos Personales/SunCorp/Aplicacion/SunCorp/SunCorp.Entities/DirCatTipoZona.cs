//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SunCorp.Entities
{
    
    using System;
    using System.Collections.Generic;
    
    using System.Runtime.Serialization;
    
    [DataContract]
    public partial class DirCatTipoZona
    
    {
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DirCatTipoZona()
        {
            this.DirCatColonia = new HashSet<DirCatColonia>();
        }
    
    	[DataMember]
        public int IdTipoZona { get; set; }
    
    	[DataMember]
        public byte[] TipoZona { get; set; }
    
    	[DataMember]
        public Nullable<bool> Borrado { get; set; }
    
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DirCatColonia> DirCatColonia { get; set; }
    }
}
