namespace ExamenMvcEf.Models
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    [DataContract(IsReference = true)]
    public class Paginador
    {
        [DataMember]
        public int PaginaActual { get; set; }
        [DataMember]
        public int TotalRegistros { get; set; }
        [DataMember]
        public int RegistrosPorPaguna { get; set; }
    }
}
