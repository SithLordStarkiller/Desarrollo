namespace Universidad.Controlador.Usuarios
{
    using System.Threading.Tasks;
    using SVRUsuarios;
    using Entidades.ControlUsuario;
    using Entidades;

    public class SvcUsuarios
    {
        #region Propiedades de la clase

        private readonly SUsuariosClient _servicio;

        public SvcUsuarios(Sesion sesion)
        {
            var configServicios = new Controlador.ControladorServicios();
            _servicio = new SUsuariosClient(configServicios.ObtenBasicHttpBinding(),
                configServicios.ObtenEndpointAddress(sesion, @"Usuarios/", "SUsuarios.svc"));
        }

        #endregion

        public Task<US_USUARIOS> ObtenUsuario(string usuario)
        {
            return Task.Run(() => _servicio.ObtenUsuarioAsync(usuario));
        }
    }
}
