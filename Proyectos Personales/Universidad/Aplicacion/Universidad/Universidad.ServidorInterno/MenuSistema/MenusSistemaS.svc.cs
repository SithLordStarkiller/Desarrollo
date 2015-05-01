using System.Collections.Generic;
using Universidad.Entidades;
using Universidad.Entidades.ControlUsuario;
using Universidad.LogicaNegocios.MenuSistema;

namespace Universidad.ServidorInterno.MenuSistema
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MenusSistemaS" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MenusSistemaS.svc or MenusSistemaS.svc.cs at the Solution Explorer and start debugging.
    public class MenusSistemaS : IMenusSistemaS
    {
        public List<SIS_AADM_ARBOLMENUS> TraeArbol()
        {
            return new MenuSistemaAdministrativoL().TraeArbol();
        }

        public List<MenuSistemaE> TraerMenus(US_USUARIOS usuario)
        {
            return new MenuSistemaAdministrativoL().TraeMenus(usuario);
        }

        public List<SIS_WADM_ARBOLMENU> TraeArbolMenuWadm(US_USUARIOS usuario)
        {
            return new MenuSistemaAdministrativoL().TraeArbolMenuWadm(usuario);
        }

        public List<SIS_WADM_ARBOLMENU_MVC> TraeArbolMenuMvc(US_USUARIOS usuario)
        {
            return new MenuSistemaAdministrativoL().TraeArbolMenuArbolMvc(usuario);
        }
    }
}
