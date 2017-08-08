namespace Suncorp.ServiceController
{
    using Models;
    using ServiceController.UsuariosWcf;
    using Suncorp.Helpers.CustomExcepciones;
    using System.ServiceModel;
    using System.Threading.Tasks;

    //using Helpers.CustomExcepciones;
    //using System.Threading.Tasks;

    public class UsuariosClient
    {
        private readonly UsuariosWcfClient _servicio;

        private readonly SessionSecurityWcf _session = new SessionSecurityWcf();

        public UsuariosClient(SessionSecurityWcf session)
        {
            _session.UrlServer = session.UrlServer;
            _session.Service = "ServiciosWcf/UsuariosWcf.svc";
            
            var configServicios = new WcfController(_session);
            var endpoint = configServicios.ObtenEndpointAddress();
            var binding = configServicios.ObtenBasicHttpBinding();

            _servicio = new UsuariosWcfClient(binding,endpoint);
        }

        #region Operaciones

        /// <summary>
        /// Metodos wcf para obtener un usuario por contraseña
        /// </summary>
        /// <param name="usuario">usuaio que decea realizar un log in</param>
        /// <param name="contrasena">Contraseña que corresponde al usuario</param>
        /// <returns>Retorna un WcfResponse estandar con el tipo de Objeto UsUsuarios</returns>
        public Task<UsUsuarios> ObtenerUsuarioLogin(string usuario, string contrasena)
        {
            try
            {
                return Task.Run(()=> _servicio.ObtenerUsuarioLoginAsync(usuario, contrasena));
            }
            catch (UserNotFoundException eUnf)
            {
                return null;
            }
        }       

        #endregion 
    }
}
