namespace ExamenEdenred.Controller.Personas
{
    using System.Threading.Tasks;
    using SvcPersonas;
    using Entities.Models;

    public class ControllerPersonas
    {
        private readonly PersonasClient _servicio;

        public ControllerPersonas(Session session)
        {
            var configServicios = new ServiceController.ServiceController();
            _servicio = new PersonasClient(configServicios.ObtenBasicHttpBinding(), configServicios.ObtenEndpointAddress(session, @"Usuarios/", "Usuarios.svc"));
        }

        public Task<bool> ExisteUsuario(int idPersona)
        {
            return Task.Run(() => _servicio.EliminaPersonaAsync(idPersona));
        }
    }
}
