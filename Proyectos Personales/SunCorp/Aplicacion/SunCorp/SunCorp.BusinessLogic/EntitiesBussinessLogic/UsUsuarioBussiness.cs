using System.Linq;

namespace SunCorp.BusinessLogic.EntitiesBussinessLogic
{
    using Entities;
    using Entities.Generic;
    using System.Collections.Generic;
    using DataAccessSqlServer.EntitiesDataAccesss;

    public class UsUsuarioBussiness
    {
        public UsUsuarios GetUsUsuarios(UserSession session)
        {
            return new UsUsuariosAccess().GetUsUsuarioLinQ(session);
        }

        public List<UsZona> GetListUsZonasUser(UsUsuarios user)
        {
            var listUsUsuariosPorzona = new UsUsuariosAccess().GetUsUsuarioPorZona(user);
            var listZonas = listUsUsuariosPorzona.Select(item => (int) item.IdZona).ToList();

            return new UsUsuariosAccess().GetListUsZonaUserLinq(listZonas);
        }
    }
}
