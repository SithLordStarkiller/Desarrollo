//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ExamenMvcEf.Models
{
    using System;
    using System.Collections.Generic;
    
    using System.Runtime.Serialization;
    
    
    [Serializable]
    //[DataContract]
    [DataContract(IsReference=true)]
    public partial class Users
    {
    	[DataMember]
        public int IdUser { get; set; }
    	[DataMember]
        public int IdTypeUser { get; set; }
    	[DataMember]
        public string LastName { get; set; }
    	[DataMember]
        public string FirstName { get; set; }
    	[DataMember]
        public string Password { get; set; }
    	[DataMember]
        public string Address { get; set; }
    
        [DataMember]public virtual CatTypeUser CatTypeUser { get; set; }
    }
}
