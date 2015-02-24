using System.Collections.Generic;
using Newtonsoft.Json;
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

            var lista = JsonConvert.DeserializeObject<List<MenuSistemaE>>(e.Result);

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

            var lista = JsonConvert.DeserializeObject<List<SIS_AADM_ARBOLMENUS>>(e.Result);

            MenuArbolFinalizado(lista);
        }

        #endregion

        #region TraeMenuArbolWadm

        public delegate void TraeArbolMenuWadmArgs(List<SIS_WADM_ARBOLMENU> lista);

        public event TraeArbolMenuWadmArgs TraeArbolMenuWadmFinalizado;

        public void TraeArbolMenuWadm(US_USUARIOS usuario)
        {
            _servicio.TraeArbolMenuWadmCompleted += _servicio_TraeArbolMenuWadmCompleted;
            _servicio.TraeArbolMenuWadm(usuario);
        }

        void _servicio_TraeArbolMenuWadmCompleted(object sender, TraeArbolMenuWadmCompletedEventArgs e)
        {
            if (e.Result == null) return;

            var lista = JsonConvert.DeserializeObject<List<SIS_WADM_ARBOLMENU>>(e.Result);
            
            _servicio.TraeArbolMenuWadmCompleted -= _servicio_TraeArbolMenuWadmCompleted;

            TraeArbolMenuWadmFinalizado(lista);
        }

        #endregion
    }
}
