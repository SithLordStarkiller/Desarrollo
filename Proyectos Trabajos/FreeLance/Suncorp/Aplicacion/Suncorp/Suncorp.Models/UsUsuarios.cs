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
    public partial class UsUsuarios{
    	[DataMember]
        public int idUsuarios { get; set; }
    	[DataMember]
        public Nullable<int> IdTipoUsuario { get; set; }
    	[DataMember]
        public Nullable<int> IdNivelUsuario { get; set; }
    	[DataMember]
        public Nullable<int> IdEstatusUsuario { get; set; }
    	[DataMember]
        public string Usuario { get; set; }
    	[DataMember]
        public string Contrasena { get; set; }
    	[DataMember]
        public bool Borrado { get; set; }
    
        public virtual UsCatNivelUsuario UsCatNivelUsuario { get; set; }
        public virtual UsEstatusUsuario UsEstatusUsuario { get; set; }
    }
}
