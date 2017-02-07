namespace Broxel.Controller.ControladorUsuarios
{
    using System.Web.Configuration;
    using ControladorWcf;
    //using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;

    public class ControladorUsuarios
    {
        private readonly string _urlSrver = WebConfigurationManager.AppSettings["ServerURL"];

        private readonly UsuariosWcf.UsuariosWcfClient _servicio;

        public ControladorUsuarios()
        {

            var configServicios = new ControladorWcfClient(_urlSrver);
            _servicio = new UsuariosWcf.UsuariosWcfClient(configServicios.ObtenBasicHttpBinding(),
                configServicios.ObtenEndpointAddress(@"Usuarios/", "UsuariosWcf.svc"));
        }

        #region Metodos Usuarios

        public Task<USUSUARIOS> ObtenUsUsuarionPorLogin(string usuario, string contrasena)
        {
            return Task.Run(() => _servicio.ObtenUsUsuarionPorLoginAsync(usuario, contrasena));
        }

        #endregion
    }
}