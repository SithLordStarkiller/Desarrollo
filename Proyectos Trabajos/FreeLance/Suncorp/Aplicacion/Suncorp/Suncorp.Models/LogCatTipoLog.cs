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
    public partial class LogCatTipoLog{
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LogCatTipoLog()
        {
            this.LogLogger = new HashSet<LogLogger>();
        }
    
    	[DataMember]	
        public int IdTipoLog { get; set; }
    	[DataMember]	
        public string TipoLog { get; set; }
    	[DataMember]	
        public string Descripcion { get; set; }
    	[DataMember]	
        public Nullable<bool> Borrado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LogLogger> LogLogger { get; set; }
    }
}
