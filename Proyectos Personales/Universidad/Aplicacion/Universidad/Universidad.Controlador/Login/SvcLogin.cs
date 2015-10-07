namespace Universidad.Controlador.Login
{
    using System.Threading.Tasks;
    using Universidad.Controlador.SvcLogin;
    using Entidades;
    using Entidades.ControlUsuario;

    public class SvcLogin
    {
        private readonly S_LoginClient _servicio;

        public SvcLogin(Sesion sesion)
        {
            var configServicios = new Controlador.ControladorServicios();
            _servicio = new S_LoginClient(configServicios.ObtenBasicHttpBinding(), configServicios.ObtenEndpointAddress(sesion, @"Login_S/", "S_Login.svc"));
        }

        public Task PruebaServicioCompleto()
        {
            return Task.Run(() => _servicio.FuncionaAsync());
        }

        public Task<US_USUARIOS> LoginAdministrativo(string usuario, string contrasena)
        {
            return Task.Run(() => _servicio.LoginAdministradorAsync(usuario, contrasena));
        }

        public Task<PER_PERSONAS> ObtenNombreCompleto(US_USUARIOS usuario)
        {
            return Task.Run(() => _servicio.ObtenPersonaAsync(usuario));
        }
    }
}
