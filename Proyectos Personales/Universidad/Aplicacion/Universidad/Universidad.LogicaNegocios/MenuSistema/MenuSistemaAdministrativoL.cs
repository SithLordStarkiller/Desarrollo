using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universidad.AccesoDatos.AdministracionSistema.MenuSistema;
using Universidad.Entidades;
using Universidad.Entidades.ControlUsuario;


namespace Universidad.LogicaNegocios.MenuSistema
{
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
    }
}
