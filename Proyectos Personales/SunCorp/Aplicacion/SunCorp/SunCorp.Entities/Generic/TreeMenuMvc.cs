namespace SunCorp.Entities.Generic
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    public class TreeMenuMvc
    {
        [DataMember]
        public int? IdChild { get; set; }

        [DataMember]
        public int? IdParent { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Controller { get; set; }

        [DataMember]
        public string Method { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public List<TreeMenuMvc> ListChilds { get; set; }
    }
}