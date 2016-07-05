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

        public Task<List<UsZona>> GetListUsZonaPageList(int page, int numRows, ref int totalRows, bool includeDelete)
        {
            var aux = totalRows;
            var obj = Task.Run(() => _servicio.GetListUsZonaPageList(page, numRows, ref aux, includeDelete));
            totalRows = aux;
            return obj;
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

        #region ProCatMarca

        public Task<List<ProCatMarca>> GetListProCatMarca()
        {
            return Task.Run(() => _servicio.GetListProCatMarcaAsync());
        }

        public Task<ProCatMarca> NewRegProCatMarca(ProCatMarca reg)
        {
            return Task.Run(() => _servicio.NewRegProCatMarcaAsync(reg));
        }

        public Task<bool> UpdateRegProCatMarca(ProCatMarca reg)
        {
            return Task.Run(() => _servicio.UpdateRegProCatMarcaAsync(reg));
        }

        public Task<bool> DeleteRegProCatMarca(ProCatMarca reg)
        {
            return Task.Run(()=>_servicio.DeleteRegProCatMarcaAsync(reg));
        }

        #endregion

        #region ProCatModelo

        public Task<List<ProCatModelo>> GetListProCatModelo()
        {
            return Task.Run(() => _servicio.GetListProCatModeloAsync());
        }

        public Task<ProCatModelo> NewRegProCatModelo(ProCatModelo reg)
        {
            return Task.Run(() => _servicio.NewRegProCatModeloAsync(reg));
        }

        public Task<bool> UpdateRegProCatModelo(ProCatModelo reg)
        {
            return Task.Run(() => _servicio.UpdateRegProCatModeloAsync(reg));
        }

        public Task<bool> DeleteRegProCatModelo(ProCatModelo reg)
        {
            return Task.Run(() => _servicio.DeleteRegProCatModeloAsync(reg));
        }

        #endregion
    }
}
