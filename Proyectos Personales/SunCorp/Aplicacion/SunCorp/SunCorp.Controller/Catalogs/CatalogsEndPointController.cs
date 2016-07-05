namespace SunCorp.Controller.Catalogs
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using SunCorp.Entities.Generic;
    using Controller;
    using SunCorp.Entities;

    public class CatalogsEndPointController
    {

        private readonly SvcServicesCatalogs.CatalogsServerClient _servicio;

        public CatalogsEndPointController(UserSession sesion)
        {
            var configServicios = new ClientEndpointController();
            _servicio = new SvcServicesCatalogs.CatalogsServerClient(configServicios.ObtenBasicHttpBinding(), configServicios.ObtenEndpointAddress(sesion, @"Services/", "CatalogsServer.svc"));
        }

        public Task<List<GenericTable>> GetListCatalogsSystem()
        {
            return Task.Run(() => _servicio.GetListCatalogsSystemAsync());
        }

        public Task<List<SisArbolMenu>> GetListMenuForUserType(UsUsuarios user)
        {
            return Task.Run(() => _servicio.GetListMenuForUserTypeAsync(user));
        }

        #region ListCatalogProducts

        public Task<List<GenericTable>> GetListCatalogsProducts()
        {
            return Task.Run(() => _servicio.GetListCatalogsProductsAsync());
        }

        #endregion

    }
}