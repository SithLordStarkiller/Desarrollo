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
    public partial class LogLogger
    {
    	[DataMember]
        public long IdLog { get; set; }
    	[DataMember]
        public Nullable<int> IdTipoLog { get; set; }
    	[DataMember]
        public string Proyecto { get; set; }
    	[DataMember]
        public string Clase { get; set; }
    	[DataMember]
        public string Metodo { get; set; }
    	[DataMember]
        public string Mensage { get; set; }
    	[DataMember]
        public string Log { get; set; }
    	[DataMember]
        public string Excepcion { get; set; }
    	[DataMember]
        public string Auxiliar { get; set; }
    	[DataMember]
        public System.DateTime FechaCreacion { get; set; }
    
        [DataMember]public virtual LogCatTipoLog LogCatTipoLog { get; set; }
    }
}
