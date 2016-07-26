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

        #region ProCatMarca

        public List<ProCatMarca> GetListProCatMarca()
        {
            return new EntitiesAccess().GetListProCatMarca();
        }

        public ProCatMarca NewRegProCatMarca(ProCatMarca reg)
        {
            return new EntitiesAccess().NewRegProCatMarca(reg);
        }

        public bool UpdateRegProCatMarca(ProCatMarca reg)
        {
            return new EntitiesAccess().UpdateRegProCatMarca(reg);
        }

        public bool DeleteRegProCatMarca(ProCatMarca reg)
        {
            return new EntitiesAccess().DeleteRegProCatMarca(reg);
        }

        #endregion

        #region ProCatModelo

        public List<ProCatModelo> GetListProCatModelo()
        {
            return new EntitiesAccess().GetListProCatModelo();
        }

        public ProCatModelo NewRegProCatModelo(ProCatModelo reg)
        {
            return new EntitiesAccess().NewRegProCatModelo(reg);
        }

        public bool UpdateRegProCatModelo(ProCatModelo reg)
        {
            return new EntitiesAccess().UpdateRegProCatModelo(reg);
        }

        public bool DeleteRegProCatModelo(ProCatModelo reg)
        {
            return new EntitiesAccess().DeleteRegProCatModelo(reg);
        }

        #endregion

        #region ProDiviciones

        public List<ProDiviciones> GetListProCatDiviciones()
        {
            return new EntitiesAccess().GetListProCatDiviciones();
        }

        public ProDiviciones NewRegProDiviciones(ProDiviciones reg)
        {
            return new EntitiesAccess().NewRegProDiviciones(reg);
        }

        public bool UpdateRegProDiviciones(ProDiviciones reg)
        {
            return new EntitiesAccess().UpdateRegProDiviciones(reg);
        }

        public bool DeleteRegProDiviciones(ProDiviciones reg)
        {
            return new EntitiesAccess().DeleteRegProDiviciones(reg);
        }

        #endregion
    }
}
