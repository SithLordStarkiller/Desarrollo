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
    public partial class ProProducto
    
    {
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProProducto()
        {
            this.ProVendedorTienda = new HashSet<ProVendedorTienda>();
        }
    
    	[DataMember]
        public int IdProducto { get; set; }
    
    	[DataMember]
        public Nullable<int> IdModelo { get; set; }
    
    	[DataMember]
        public Nullable<int> IdMarca { get; set; }
    
    	[DataMember]
        public Nullable<int> IdFamilia { get; set; }
    
    	[DataMember]
        public Nullable<int> IdDivicion { get; set; }
    
    	[DataMember]
        public string NombreProducto { get; set; }
    
    	[DataMember]
        public string Descripcion { get; set; }
    
    	[DataMember]
        public string CodigoBarras { get; set; }
    
    	[DataMember]
        public string Detalles { get; set; }
    
    	[DataMember]
        public Nullable<bool> EsReparable { get; set; }
    
    	[DataMember]
        public Nullable<bool> HaySeries { get; set; }
    
    	[DataMember]
        public Nullable<bool> Borrado { get; set; }
    
    	[DataMember]
        public string Creador { get; set; }
    
    	[DataMember]
        public Nullable<System.DateTime> FechaCreacion { get; set; }
    
    	[DataMember]
        public string ModificadoPor { get; set; }
    
    	[DataMember]
        public Nullable<System.DateTime> FechaModificacion { get; set; }
    
    
        public virtual ProCatMarca ProCatMarca { get; set; }
        public virtual ProCatModelo ProCatModelo { get; set; }
        public virtual ProDiviciones ProDiviciones { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProVendedorTienda> ProVendedorTienda { get; set; }
    }
}
