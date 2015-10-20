namespace ExamenEdenred.Entities.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Session
    {
        [DataMember]
        public string Conexion { get; set; }

        [DataMember]
        public bool RecordarSesion { get; set; }

        [DataMember]
        public bool RecordarContrasena { get; set; }

        [DataMember]
        public string Usuario { get; set; }

        [DataMember]
        public string Contrasena { get; set; }
    }
}
