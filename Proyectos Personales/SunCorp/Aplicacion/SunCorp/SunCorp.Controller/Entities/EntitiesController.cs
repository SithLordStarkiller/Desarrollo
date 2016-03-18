namespace SunCorp.Controller.Entities
{
    using Controller;
    using System.Threading.Tasks;
    using SunCorp.Entities.Generic;

    public class EntitiesController
    {

        private readonly SvcServicesEntities.EntitiesServerClient _servicio;

        public EntitiesController(UserSession sesion)
        {
            var configServicios = new ClientController();
            _servicio = new SvcServicesEntities.EntitiesServerClient(configServicios.ObtenBasicHttpBinding(), configServicios.ObtenEndpointAddress(sesion, @"Login_S/", "S_Login.svc"));
        }

        public Task GetUsUsuario(string user)
        {
            return Task.Run(() => _servicio.GetUsUsuariosAsync(user));
        }
    }
}
