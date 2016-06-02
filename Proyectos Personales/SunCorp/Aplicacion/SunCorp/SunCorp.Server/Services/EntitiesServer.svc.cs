namespace SunCorp.Server.Services
{
    using Entities.Generic;
    using Entities;
    using System.Collections.Generic;
    using BusinessLogic.EntitiesBussinessLogic;
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EntitiesServer" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select EntitiesServer.svc or EntitiesServer.svc.cs at the Solution Explorer and start debugging.
    public class EntitiesServer : IEntitiesServer
    {
        public UsUsuarios GetUsUsuarios(UserSession session)
        {
            return new EntitiesBussiness().GetUsUsuarios(session);
        }
        public UsTipoUsuario GetTypeUser(UsUsuarios user)
        {
            return new EntitiesBussiness().GetTypeUser(user);
        }

        public List<UsZona> GetListUsZonasUser(UsUsuarios user)
        {
            return new EntitiesBussiness().GetListUsZonasUser(user);
        }
    }
}
