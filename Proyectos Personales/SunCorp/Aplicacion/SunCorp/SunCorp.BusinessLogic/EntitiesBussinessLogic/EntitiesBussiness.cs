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
            return new EntitiesAccess().GetUsUsuario(session).Result;
        }

        #endregion

        #region UsTipoUsuario

        public UsTipoUsuario GetTypeUser(UsUsuarios user)
        {
            return new EntitiesAccess().GetTypeUser(user).Result;
        }

        #endregion

        #region UsZona

        public List<UsZona> GetListUsZona()
        {
            return new EntitiesAccess().GetListUsZona().Result;
        }

        public List<UsZona> GetListUsZonaPageList(int page, int numRows, ref int totalRows, bool includeDelete)
        {
            return null;//new EntitiesAccess().GetListUsZonaPageList(page,numRows, ref totalRows, includeDelete);
        }

        public UsZona NewRegUsZona(UsZona zona)
        {
            return new EntitiesAccess().NewRegUsZona(zona).Result;
        }

        public bool UpdateRegUsZona(UsZona zona)
        {
            return new EntitiesAccess().UpdateRegUsZona(zona).Result;
        }

        public bool DeleteRegUsZona(UsZona zona)
        {
            return new EntitiesAccess().DeleteRegUsZona(zona).Result;
        }

        public List<UsZona> GetListUsZonasUser(UsUsuarios user)
        {
            var listUsUsuariosPorzona = new EntitiesAccess().GetUsUsuarioPorZona(user).Result;
            var listZonas = listUsUsuariosPorzona.Select(item => item.IdZona ?? -1).ToList();

            return new EntitiesAccess().GetListUsZonaUser(listZonas).Result;
        }

        #endregion

        #region ProCatMarca

        public List<ProCatMarca> GetListProCatMarca()
        {
            return new EntitiesAccess().GetListProCatMarca().Result;
        }

        public ProCatMarca NewRegProCatMarca(ProCatMarca reg)
        {
            return new EntitiesAccess().NewRegProCatMarca(reg).Result;
        }

        public bool UpdateRegProCatMarca(ProCatMarca reg)
        {
            return new EntitiesAccess().UpdateRegProCatMarca(reg).Result;
        }

        public bool DeleteRegProCatMarca(ProCatMarca reg)
        {
            return new EntitiesAccess().DeleteRegProCatMarca(reg).Result;
        }

        #endregion

        #region ProCatModelo

        public List<ProCatModelo> GetListProCatModelo()
        {
            return new EntitiesAccess().GetListProCatModelo().Result;
        }

        public ProCatModelo NewRegProCatModelo(ProCatModelo reg)
        {
            return new EntitiesAccess().NewRegProCatModelo(reg).Result;
        }

        public bool UpdateRegProCatModelo(ProCatModelo reg)
        {
            return new EntitiesAccess().UpdateRegProCatModelo(reg).Result;
        }

        public bool DeleteRegProCatModelo(ProCatModelo reg)
        {
            return new EntitiesAccess().DeleteRegProCatModelo(reg).Result;
        }

        #endregion

        #region ProDiviciones

        public List<ProDiviciones> GetListProCatDiviciones()
        {
            return new EntitiesAccess().GetListProCatDiviciones().Result;
        }

        public ProDiviciones NewRegProDiviciones(ProDiviciones reg)
        {
            return new EntitiesAccess().NewRegProDiviciones(reg).Result;
        }

        public bool UpdateRegProDiviciones(ProDiviciones reg)
        {
            return new EntitiesAccess().UpdateRegProDiviciones(reg).Result;
        }

        public bool DeleteRegProDiviciones(ProDiviciones reg)
        {
            return new EntitiesAccess().DeleteRegProDiviciones(reg).Result;
        }

        #endregion
    }
}
