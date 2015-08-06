namespace Universidad.Entidades.Catalogos
{
    using System.Runtime.Serialization;

    [DataContract]
    public class ListasGenerica
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public string Descripcion { get; set; }

        [DataMember]
        public byte[] Binario { get; set; }

        [DataMember]
        public string CampoAdicional { get; set; }
    }
}
