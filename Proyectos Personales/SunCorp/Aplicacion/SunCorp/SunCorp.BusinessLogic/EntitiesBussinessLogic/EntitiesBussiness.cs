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

        #region UsZona

        public List<UsZona> GetListUsZona()
        {
            return new EntitiesAccess().GetListUsZona();
        }

        public List<UsZona> GetListUsZonaPageList(int page, int numRows, ref int totalRows, bool includeDelete)
        {
            return new EntitiesAccess().GetListUsZonaPageList(page,numRows, ref totalRows, includeDelete);
        }

        public UsZona NewRegUsZona(UsZona zona)
        {
            return new EntitiesAccess().NewRegUsZona(zona);
        }

        public bool UpdateRegUsZona(UsZona zona)
        {
            return new EntitiesAccess().UpdateRegUsZona(zona);
        }

        public bool DeleteRegUsZona(UsZona zona)
        {
            return new EntitiesAccess().DeleteRegUsZona(zona);
        }

        public List<UsZona> GetListUsZonasUser(UsUsuarios user)
        {
            var listUsUsuariosPorzona = new EntitiesAccess().GetUsUsuarioPorZona(user);
            var listZonas = listUsUsuariosPorzona.Select(item => (int) item.IdZona).ToList();

            return new EntitiesAccess().GetListUsZonaUserLinq(listZonas);
        }

        #endregion
    }
}
