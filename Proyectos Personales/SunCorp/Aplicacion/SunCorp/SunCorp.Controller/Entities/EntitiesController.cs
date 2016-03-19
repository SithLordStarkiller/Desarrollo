namespace SunCorp.Controller.Entities
{
    using Controller;
    using System.Threading.Tasks;
    using SunCorp.Entities.Generic;
    using SunCorp.Entities.Entities;

    public class EntitiesController
    {

        private readonly SvcServicesEntities.EntitiesServerClient _servicio;

        public EntitiesController(UserSession sesion)
        {
            var configServicios = new ClientController();
            _servicio = new SvcServicesEntities.EntitiesServerClient(configServicios.ObtenBasicHttpBinding(), configServicios.ObtenEndpointAddress(sesion, @"Services/", "EntitiesServer.svc"));
        }

        public Task<UsUsuarios> GetUsUsuario(string user, string password)
        {
            return Task.Run(() => _servicio.GetUsUsuariosAsync(user,password));
        }
    }
}
