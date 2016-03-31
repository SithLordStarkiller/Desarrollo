namespace SunCorp.Entities.Entities
{
    using System.Runtime.Serialization;

    [DataContract]
    public class UsTipoUsuario
    {
        [DataMember]
        public int IdTipoUsuario { get; set; }

        [DataMember]
        public string TipoUsuario { get; set; }

        [DataMember]
        public string Descripcion { get; set; }
    }
}
