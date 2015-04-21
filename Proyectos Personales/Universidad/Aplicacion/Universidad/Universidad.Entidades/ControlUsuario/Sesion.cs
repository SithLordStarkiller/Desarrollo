using System.Runtime.Serialization;

namespace Universidad.Entidades.ControlUsuario
{
    [DataContract]
    public class Sesion
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
