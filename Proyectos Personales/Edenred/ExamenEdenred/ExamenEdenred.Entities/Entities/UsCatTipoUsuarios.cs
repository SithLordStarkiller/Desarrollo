namespace ExamenEdenred.Entities.Entities
{
    using System.Runtime.Serialization;

    [DataContract]
    public class UsCatTipoUsuarios
    {
        [DataMember]
        public int IdTipoUsuario { get; set; }

        [DataMember]
        public string TipoUsuario { get; set; }

        [DataMember]
        public string Descripcion { get; set; }
    }
}
