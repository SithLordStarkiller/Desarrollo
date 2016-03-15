namespace SunCorp.Server.Services
{
    using Entities.Entities;
    using BusinessLogic.EntitiesBussinessLogic;
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EntitiesServer" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select EntitiesServer.svc or EntitiesServer.svc.cs at the Solution Explorer and start debugging.
    public class EntitiesServer : IEntitiesServer
    {
        public UsUsuarios GetUsUsuarios(string user)
        {
            return new UsUsuarioBussiness().GetUsUsuarios(user);
        }
    }
}
