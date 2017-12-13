using System.Runtime.Serialization;

namespace ExamenCsi.Entities
{
    [DataContract]
    public class UsUsuario
    {
        [DataMember]
        public int IdUsuario { get; set; }
        [DataMember]
        public int IdTipoUsuario { get; set; }
        [DataMember]
        public string Usuario { get; set; }
        [DataMember]
        public string Contrasena { get; set; }
    }
}
