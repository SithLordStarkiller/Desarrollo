namespace SunCorp.Controller.Catalogs
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using SunCorp.Entities.Generic;
    using Controller;

    using System.Web.Mvc;

    public class CatalogsEndPointController
    {

        private readonly SvcServicesCatalogs.CatalogsServerClient _servicio;

        public CatalogsEndPointController(UserSession sesion)
        {
            var configServicios = new ClientEndpointController();
            _servicio = new SvcServicesCatalogs.CatalogsServerClient(configServicios.ObtenBasicHttpBinding(), configServicios.ObtenEndpointAddress(sesion, @"Services/", "CatalogsServer.svc"));
        }

        [HttpPost]
        public Task<List<GenericTable>> GetListCatalogsSystem()
        {
            return Task.Run(() => _servicio.GetListCatalogsSystemAsync());
        }
    }
}