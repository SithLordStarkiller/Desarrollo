namespace ExamenEdenred.Entities.Entities
{
    using System.Runtime.Serialization;

    [DataContract]
    public class UsUsuarios
    {
        [DataMember]
        public int? IdUsuario { get; set; }

        [DataMember]
        public int? IdTipoUsuario { get; set; }

        [DataMember]
        public int? IdEstatusUsuario { get; set; }

        [DataMember]
        public int? IdNivelUsuario { get; set; }

        [DataMember]
        public int? IdHistorial { get; set; }

        [DataMember]
        public string Usuario { get; set; }

        [DataMember]
        public string Contrasena { get; set; }
    }
}