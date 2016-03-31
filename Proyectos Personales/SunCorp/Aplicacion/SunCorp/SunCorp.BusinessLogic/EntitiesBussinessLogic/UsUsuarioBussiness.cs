using SunCorp.Entities.Generic;

namespace SunCorp.BusinessLogic.EntitiesBussinessLogic
{
    using Entities.Entities;
    using DataAccessSqlServer.EntitiesDataAccesss;

    public class UsUsuarioBussiness
    {
        public UsUsuarios GetUsUsuarios(UserSession session)
        {
            return new UsUsuariosAccess().GetUsUsuario(session);
        }
    }
}
