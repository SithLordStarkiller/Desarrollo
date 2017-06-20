namespace Suncorp.Models.Models
{
    using System.Runtime.Serialization;
    using System;

    [Serializable]
    [DataContract]
    public class UsUsuariosModel
    {
        [DataMember]
        public int idUsuarios { get; set; }
        [DataMember]
        public int? IdTipoUsuario { get; set; }
        [DataMember]
        public int? IdNivelUsuario { get; set; }
        [DataMember]
        public int? IdEstatusUsuario { get; set; }
        [DataMember]
        public string Usuario { get; set; }
        [DataMember]
        public string Contrasena { get; set; }
        [DataMember]
        public bool Borrado { get; set; }
    }
}
