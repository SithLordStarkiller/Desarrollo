namespace SunCorp.Controller.Entities
{
    using Controller;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using SunCorp.Entities.Generic;
    using SunCorp.Entities;

    public class EntitiesEndpointController
    {

        private readonly SvcServicesEntities.EntitiesServerClient _servicio;

        public EntitiesEndpointController(UserSession sesion)
        {
            var configServicios = new ClientEndpointController();
            _servicio = new SvcServicesEntities.EntitiesServerClient(configServicios.ObtenBasicHttpBinding(), configServicios.ObtenEndpointAddress(sesion, @"Services/", "EntitiesServer.svc"));
        }

        #region UsUsuario

        public Task<UsUsuarios> GetUsUsuario(UserSession user)
        {
            return Task.Run(() => _servicio.GetUsUsuariosAsync(user));
        }

        #endregion

        #region UsZona

        public Task<List<UsZona>> GetListZonas()
        {
            return Task.Run(() => _servicio.GetListUsZonaAsync());
        }

        public Task<List<UsZona>> GetListUsZonaPageList(int page, int numRows, bool includeDelete)
        {
            return Task.Run(() => _servicio.GetListUsZonaPageListAsync(page, numRows, includeDelete));
        }

        public Task<UsZona> NewRegUsZona(UsZona zona)
        {
            return Task.Run(() => _servicio.NewRegUsZonaAsync(zona));
        }

        public Task<bool> UpdateRegUsZona(UsZona zona)
        {
            return Task.Run(() => _servicio.UpdateRegUsZonaAsync(zona));
        }

        public Task<bool> DeleteRegUsZona(UsZona zona)
        {
            return Task.Run(() => _servicio.DeleteRegUsZonaAsync(zona));
        }

        public Task<List<UsZona>> GetListUsZonasUser(UsUsuarios user)
        {
            return Task.Run(() => _servicio.GetListUsZonasUserAsync(user));
        }

        #endregion

        #region UsTipoUsuario

        public Task<UsTipoUsuario> GetTypeUser(UsUsuarios user)
        {
            return Task.Run(() => _servicio.GetTypeUserAsync(user));
        }

        #endregion
    }
}
