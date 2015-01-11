using System.Collections.Generic;

namespace Universidad.Entidades.ControlUsuario
{
    public class MenuSistemaE
    {
        public int IdMenuHijo { get; set; }
        public int IdMenuPadre { get; set; }
        public string NombreNodo { get; set; }
        public string RutaNodo { get; set; }
        public int IdTipoUsuario { get; set; }
        public string TipoUsuario { get; set; }
        public int IdNivelUsuario { get; set; }
        public string NivelUsuario { get; set; }
        public int IdTabPage { get; set; }
        public string RutaTapPage { get; set; }
        public string NombreTabPage { get; set; }
    }
}
