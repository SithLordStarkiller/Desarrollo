namespace SunCorp.Entities.Generic
{
    using System.Runtime.Serialization;

    [DataContract]
    public class GenericTable
    {
        [DataMember]
        public int IdTable { get; set; }

        [DataMember]
        public string TableName { get; set; }

        [DataMember]
        public string TableDescription { get; set; }

        [DataMember]
        public bool Deleted { get; set; }

    }
}
