using System.Collections.Generic;
using System.ServiceModel;
using Universidad.Entidades;
using Universidad.Entidades.ControlUsuario;

namespace Universidad.ServidorInterno.MenuSistema
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMenusSistemaS" in both code and config file together.
    [ServiceContract]
    public interface IMenusSistemaS
    {
        [OperationContract]
        List<SIS_AADM_ARBOLMENUS> TraeArbol();

        [OperationContract]
        List<MenuSistemaE> TraerMenus(US_USUARIOS usuario);

        [OperationContract]
        List<SIS_WADM_ARBOLMENU_MVC> TraeArbolMenuMvc(US_USUARIOS usuario);
    }
}
