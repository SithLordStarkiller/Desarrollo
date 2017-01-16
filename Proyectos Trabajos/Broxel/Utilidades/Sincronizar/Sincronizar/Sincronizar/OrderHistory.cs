using System.Runtime.Serialization;

namespace Sincronizar
{
    [DataContract]
    internal class OrderHistory
    {
        [DataMember]
        public string ExternalId { get; set; }
        [DataMember]
        public string FormiikId { get; set; }
        [DataMember]
        public string FormatType { get; set; }
        [DataMember]
        public string User { get; set; }
        [DataMember]
        public string Group { get; set; }
        [DataMember]
        public string OrderStatus { get; set; }
        [DataMember]
        public string OrderStatusDate { get; set; }
    }
}
