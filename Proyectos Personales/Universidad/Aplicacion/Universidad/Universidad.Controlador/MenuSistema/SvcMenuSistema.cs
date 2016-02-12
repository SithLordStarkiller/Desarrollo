namespace Universidad.Controlador.MenuSistema
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Universidad.Controlador.SvcMenuSistema;
    using Entidades;
    using Entidades.ControlUsuario;

    public class SvcMenuSistema
    {
        private readonly MenusSistemaSClient _servicio;

        public SvcMenuSistema(Sesion sesion)
        {
            var configServicios = new Controlador.ControladorServicios();
            _servicio = new MenusSistemaSClient(configServicios.ObtenBasicHttpBinding(),
                configServicios.ObtenEndpointAddress(sesion, @"MenuSistema/", "MenusSistemaS.svc"));
        }

        public Task MenuSistema(US_USUARIOS usuario)
        {
            return Task.Run(() => _servicio.TraerMenusAsync(usuario));
        }

        public Task MenuArbol()
        {
            return Task.Run(() => _servicio.TraeArbolAsync());
        }

        public Task<List<SIS_WADM_ARBOLMENU_MVC>> TraeArbolMenuMvc(US_USUARIOS usuario)
        {
            return Task.Run(() => _servicio.TraeArbolMenuMvcAsync(usuario).Result.ToList());
        }
    }
}
