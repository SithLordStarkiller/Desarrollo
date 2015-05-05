using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Universidad.Entidades.ControlUsuario
{
    public class MenuSisWadmE
    {
        [DataMember]
        public int? IdMenuHijo { get; set; }

        [DataMember]
        public int? IdMenuPadre { get; set; }

        [DataMember]
        public int? IdTipoUsurio { get; set; }

        [DataMember]
        public int? IdNivelUsuario { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public string Controller { get; set; }

        [DataMember]
        public string Method { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public List<MenuSisWadmE> Hijos { get; set; }
    }
}
