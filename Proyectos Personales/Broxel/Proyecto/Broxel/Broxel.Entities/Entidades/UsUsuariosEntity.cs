namespace Broxel.Entities.Entidades
{
    using System.Runtime.Serialization;

    [DataContract]
    public class UsUsuariosEntity
    {
        [DataMember]
        public int IdUsuario { get; set; }

        [DataMember]
        public int? IdEstatus { get; set; }

        [DataMember]
        public string Usuario { get; set; }

        [DataMember]
        public string Contrasena { get; set; }

        public UsUsuariosEntity()
        {
        }

        public UsUsuariosEntity(int idUsuario, int idEstatus, string usuario, string contrasena)
        {
            IdUsuario = idUsuario;
            IdEstatus = idEstatus;
            Usuario = usuario;
            Contrasena = contrasena;
        }
    }
}
