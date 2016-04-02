namespace SunCorp.Controller.Entities
{
    using Controller;
    using System.Threading.Tasks;
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
    }
}
