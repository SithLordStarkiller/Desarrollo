//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Suncorp.Models
{
    using System.Runtime.Serialization;
    using System;
    using System.Collections.Generic;
    
    [DataContract]
    public partial class UsEstatusUsuario{
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UsEstatusUsuario()
        {
            this.UsUsuarios = new HashSet<UsUsuarios>();
        }
    
    	[DataMember]	
        public int IdEstatusUsuario { get; set; }
    	[DataMember]	
        public string EstatusUsuario { get; set; }
    	[DataMember]	
        public string Descripcion { get; set; }
    	[DataMember]	
        public bool Borrado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsUsuarios> UsUsuarios { get; set; }
    }
}
