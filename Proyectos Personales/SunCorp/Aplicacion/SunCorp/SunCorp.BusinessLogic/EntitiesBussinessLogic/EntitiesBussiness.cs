namespace SunCorp.BusinessLogic.EntitiesBussinessLogic
{
    using Entities;
    using Entities.Generic;
    using System.Linq;
    using System.Collections.Generic;
    using DataAccessSqlServer.EntitiesDataAccesss;

    public class EntitiesBussiness
    {
        #region UsUsuarios
        
        public UsUsuarios GetUsUsuarios(UserSession session)
        {
            return new EntitiesAccess().GetUsUsuario(session);
        }

        #endregion

        #region UsTipoUsuario

        public UsTipoUsuario GetTypeUser(UsUsuarios user)
        {
            return new EntitiesAccess().GetTypeUser(user);
        }

        #endregion

        #region UsZonas

        public List<UsZona> GetListUsZonasUser(UsUsuarios user)
        {
            var listUsUsuariosPorzona = new EntitiesAccess().GetUsUsuarioPorZona(user);
            var listZonas = listUsUsuariosPorzona.Select(item => (int) item.IdZona).ToList();

            return new EntitiesAccess().GetListUsZonaUserLinq(listZonas);
        }

        #endregion
    }
}
