namespace SunCorp.Entities.Entities
{
    using System.Runtime.Serialization;

    [DataContract]
    public class UsUsuarios
    {
        [DataMember]
        public int IdUsuario { get; set; }

        [DataMember]
        public string Usuario { get; set; }

        [DataMember]
        public string Contrasena { get; set; }
    }
}
