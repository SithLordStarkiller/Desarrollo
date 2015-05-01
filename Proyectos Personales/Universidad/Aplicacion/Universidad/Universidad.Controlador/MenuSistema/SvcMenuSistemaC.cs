using System.Collections.Generic;
using Universidad.Controlador.SVRMenuSistema;
using Universidad.Entidades;
using Universidad.Entidades.ControlUsuario;

namespace Universidad.Controlador.MenuSistema
{
    public class SvcMenuSistemaC
    {
        #region Propiedades de la clase

        private readonly MenusSistemaSClient _servicio;

        public SvcMenuSistemaC(Sesion sesion)
        {
            var configServicios = new Controlador.ControladorServicios();
            _servicio = new MenusSistemaSClient(configServicios.ObtenBasicHttpBinding(),
                configServicios.ObtenEndpointAddress(sesion, @"MenuSistema/", "MenusSistemaS.svc"));
        }

        #endregion

        #region MenuSistema

        public delegate void MenuSistemaArgs(List<MenuSistemaE> lista);

        public event MenuSistemaArgs MenuSistemaFinalizado;

        public void MenuSistema(US_USUARIOS usuario)
        {
            _servicio.TraerMenusCompleted += _servicio_TraerMenusCompleted;
            _servicio.TraerMenusAsync(usuario);
        }

        private void _servicio_TraerMenusCompleted(object sender, TraerMenusCompletedEventArgs e)
        {
            if (e.Result == null) return;

            var lista = e.Result;

            MenuSistemaFinalizado(lista);
        }

        #endregion

        #region TraeMenuArbol

        public delegate void MenuArbolArgs(List<SIS_AADM_ARBOLMENUS> lista);

        public event MenuArbolArgs MenuArbolFinalizado;

        public void MenuArbol()
        {
            _servicio.TraeArbolCompleted += _servicio_TraeArbolCompleted;
            _servicio.TraeArbolAsync();
        }

        private void _servicio_TraeArbolCompleted(object sender, TraeArbolCompletedEventArgs e)
        {
            if (e.Result == null) return;
            var lista = e.Result;
            MenuArbolFinalizado(lista);
        }

        #endregion

        #region TraeMenuArbolWadmAsyncronous

        public delegate void TraeMenuArbolWadmArgs(List<SIS_WADM_ARBOLMENU> lista);

        public event TraeMenuArbolWadmArgs TraeMenuArbolWadmFinalizado;

        public void TraeMenuArbolWadmAsyncrono(US_USUARIOS usuario)
        {
            _servicio.TraeArbolMenuWadmCompleted +=
                delegate(object sender, TraeArbolMenuWadmCompletedEventArgs e)
                {
                    if (e.Result == null) return;
                    var lista = e.Result;
                    TraeMenuArbolWadmFinalizado(lista);
                };
            _servicio.TraeArbolMenuWadmAsync(usuario);
        }

        #endregion

        #region TraeMenuArbolWadmSynchronous

        public List<SIS_WADM_ARBOLMENU> TraeArbolMenuWadm(US_USUARIOS usuario)
        {
            return _servicio.TraeArbolMenuWadm(usuario);
        }

        #endregion

        #region TraeArbolMenuMvc

        public delegate void TraeArbolMenuMvcArgs(List<SIS_WADM_ARBOLMENU_MVC> lista);

        public event TraeArbolMenuMvcArgs TraeArbolMenuMvcFinalizado;

        public void TraeArbolMenuMvc(US_USUARIOS usuario)
        {
            _servicio.TraeArbolMenuMvcCompleted += _servicio_TraeArbolMenuMvcCompleted;
            _servicio.TraeArbolMenuMvcAsync(usuario);
        }

        private void _servicio_TraeArbolMenuMvcCompleted(object sender, TraeArbolMenuMvcCompletedEventArgs e)
        {
            if (e.Result == null) return;
            var lista = e.Result;
            TraeArbolMenuMvcFinalizado(lista);
        }

        #endregion
    }
}
