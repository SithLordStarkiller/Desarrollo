using System.Runtime.Serialization;

namespace Universidad.Entidades.ControlUsuario
{
    [DataContract]
    public class MenuSistemaE
    {
        [DataMember]
        public int IdMenuHijo { get; set; }

        [DataMember]
        public int IdMenuPadre { get; set; }

        [DataMember]
        public string NombreNodo { get; set; }

        [DataMember]
        public string RutaNodo { get; set; }

        [DataMember]
        public int IdTipoUsuario { get; set; }

        [DataMember]
        public string TipoUsuario { get; set; }

        [DataMember]
        public int IdNivelUsuario { get; set; }

        [DataMember]
        public string NivelUsuario { get; set; }

        [DataMember]
        public int IdTabPage { get; set; }

        [DataMember]
        public string RutaTapPage { get; set; }

        [DataMember]
        public string NombreTabPage { get; set; }
    }
}
