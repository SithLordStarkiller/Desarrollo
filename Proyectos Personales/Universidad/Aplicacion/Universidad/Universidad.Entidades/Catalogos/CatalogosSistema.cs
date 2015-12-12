namespace Universidad.Entidades.Catalogos
{
    using System.Runtime.Serialization;

    [DataContract]
    public class CatalogosSistema
    {
        [DataMember]
        public string NombreTabla { get; set; }
    }
}
