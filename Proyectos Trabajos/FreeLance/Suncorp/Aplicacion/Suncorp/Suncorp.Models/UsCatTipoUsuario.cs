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
    public partial class UsCatTipoUsuario{
    	[DataMember]
        public int IdTipoUsuario { get; set; }
    	[DataMember]
        public string TipoUsuario { get; set; }
    	[DataMember]
        public string Descripcion { get; set; }
    	[DataMember]
        public bool Borrado { get; set; }
    }
}