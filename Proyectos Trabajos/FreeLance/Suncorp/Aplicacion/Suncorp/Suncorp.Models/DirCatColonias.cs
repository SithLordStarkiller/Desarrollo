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
    public partial class DirCatColonias{
    	[DataMember]	
        public short IdColonia { get; set; }
    	[DataMember]	
        public short IdEstado { get; set; }
    	[DataMember]	
        public short IdMunicipio { get; set; }
    	[DataMember]	
        public string NombreColonia { get; set; }
    	[DataMember]	
        public string CodigoPostal { get; set; }
    	[DataMember]	
        public string ClaveCepomexColonia { get; set; }
    	[DataMember]	
        public bool Borrado { get; set; }
    
        public virtual DirCatEstados DirCatEstados { get; set; }
        public virtual DirCatMunicipios DirCatMunicipios { get; set; }
    }
}
