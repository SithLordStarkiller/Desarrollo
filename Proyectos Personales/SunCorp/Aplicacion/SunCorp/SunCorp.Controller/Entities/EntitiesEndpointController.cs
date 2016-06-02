namespace SunCorp.Controller.Entities
{
    using Controller;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using SunCorp.Entities.Generic;
    using SunCorp.Entities;
    using System.Web.Mvc;

    public class EntitiesEndpointController
    {

        private readonly SvcServicesEntities.EntitiesServerClient _servicio;

        public EntitiesEndpointController(UserSession sesion)
        {
            var configServicios = new ClientEndpointController();
            _servicio = new SvcServicesEntities.EntitiesServerClient(configServicios.ObtenBasicHttpBinding(), configServicios.ObtenEndpointAddress(sesion, @"Services/", "EntitiesServer.svc"));
        }

        [HttpPost]
        public Task<UsUsuarios> GetUsUsuario(UserSession user)
        {
            return Task.Run(() => _servicio.GetUsUsuariosAsync(user));
        }

        public Task<List<UsZona>> GetListUsZonasUser(UsUsuarios user)
        {
            return Task.Run(() => _servicio.GetListUsZonasUserAsync(user));
        }

        public Task<UsTipoUsuario> GetTypeUser(UsUsuarios user)
        {
            return Task.Run(() => _servicio.GetTypeUserAsync(user));
        }
    }
}
