namespace ExamenEdenred.Controller.Usuarios
{
    using System.Threading.Tasks;
    using Entities.Entities;
    using SvcUsuarios;
    using Entities.Models;

    public class ControllerUsuarios
    {
        private readonly UsuariosClient _servicio;

        public ControllerUsuarios(Session session)
        {
            var configServicios = new ServiceController.ServiceController();
            _servicio = new UsuariosClient(configServicios.ObtenBasicHttpBinding(), configServicios.ObtenEndpointAddress(session, @"Usuarios/", "Usuarios.svc"));
        }

        public Task<UsUsuarios> ExisteUsuario()
        {
            return Task.Run(() => _servicio.ExisteUsuarioAsync(1));
        }

        public Task<bool> GuardaArchivo(string texto)
        {
            return Task.Run(() => _servicio.GuardaArchivoAsync(texto));
        }
    }
}
