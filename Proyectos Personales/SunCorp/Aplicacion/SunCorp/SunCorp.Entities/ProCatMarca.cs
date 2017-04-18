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
    public partial class ProCatMarca
    
    {
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProCatMarca()
        {
            this.ProCatModelo = new HashSet<ProCatModelo>();
            this.ProProducto = new HashSet<ProProducto>();
        }
    
    	[DataMember]
        public int IdMarca { get; set; }
    
    	[DataMember]
        public string NombreMarca { get; set; }
    
    	[DataMember]
        public string Descripcion { get; set; }
    
    	[DataMember]
        public string Creador { get; set; }
    
    	[DataMember]
        public Nullable<System.DateTime> FechaCreacion { get; set; }
    
    	[DataMember]
        public string Modificado { get; set; }
    
    	[DataMember]
        public Nullable<System.DateTime> FechaModificacion { get; set; }
    
    	[DataMember]
        public Nullable<bool> Borrado { get; set; }
    
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProCatModelo> ProCatModelo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProProducto> ProProducto { get; set; }
    }
}
