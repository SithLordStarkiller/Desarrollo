namespace Universidad.LogicaNegocios.MenuSistema
{
    using System.Collections.Generic;
    using System.Linq;
    using AccesoDatos.AdministracionSistema.MenuSistema;
    using Entidades;
    using Entidades.ControlUsuario;

    public class MenuSistemaAdministrativoL
    {
        public List<SIS_AADM_ARBOLMENUS> TraeArbol()
        {
            return new MenuSistemaAdministrativoA().TraeArbolLinq();
        }

        public List<MenuSistemaE> TraeMenus(US_USUARIOS usuario)
        {
            return new MenuSistemaAdministrativoA().TrerMenusLinq(usuario);
        }

        public List<SIS_WADM_ARBOLMENU_MVC> TraeArbolMenuArbolMvc(US_USUARIOS usuario)
        {
            var listaPermisos = new MenuSistemaAdministrativoA().TraeListaMenuPermisosMvcLinq(usuario);

            var listaMenuArbol = new MenuSistemaAdministrativoA().TraeArbolMenuArbolMvcLinq(usuario);

            listaMenuArbol = listaMenuArbol.Where(r => listaPermisos.Any(x => x.IDMENU == r.IDMENU)).ToList();

            return listaMenuArbol;
        }
    }
}
