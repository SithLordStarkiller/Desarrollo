namespace SunCorp.WebServerApplication.Services
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

        #region UsZona

        public List<UsZona> GetListUsZona()
        {
            return new EntitiesBussiness().GetListUsZona();
        }

        public List<UsZona> GetListUsZonaPageList(int page, int numRows, ref int totalRows, bool includeDelete)
        {
            return new EntitiesBussiness().GetListUsZonaPageList(page, numRows, ref totalRows, includeDelete);
        }

        public UsZona NewRegUsZona(UsZona zona)
        {
            return new EntitiesBussiness().NewRegUsZona(zona);
        }

        public bool UpdateRegUsZona(UsZona zona)
        {
            return new EntitiesBussiness().UpdateRegUsZona(zona);
        }

        public bool DeleteRegUsZona(UsZona zona)
        {
            return new EntitiesBussiness().DeleteRegUsZona(zona);
        }

        public List<UsZona> GetListUsZonasUser(UsUsuarios user)
        {
            return new EntitiesBussiness().GetListUsZonasUser(user);
        }

        #endregion

        #region ProCatMarca

        public List<ProCatMarca> GetListProCatMarca()
        {
            return new EntitiesBussiness().GetListProCatMarca();
        }

        public ProCatMarca NewRegProCatMarca(ProCatMarca reg)
        {
            return new EntitiesBussiness().NewRegProCatMarca(reg);
        }

        public bool UpdateRegProCatMarca(ProCatMarca reg)
        {
            return new EntitiesBussiness().UpdateRegProCatMarca(reg);
        }

        public bool DeleteRegProCatMarca(ProCatMarca reg)
        {
            return new EntitiesBussiness().DeleteRegProCatMarca(reg);
        }

        #endregion

        #region ProCatModelo

        public List<ProCatModelo> GetListProCatModelo()
        {
            return new EntitiesBussiness().GetListProCatModelo();
        }

        public ProCatModelo NewRegProCatModelo(ProCatModelo reg)
        {
            return new EntitiesBussiness().NewRegProCatModelo(reg);
        }

        public bool UpdateRegProCatModelo(ProCatModelo reg)
        {
            return new EntitiesBussiness().UpdateRegProCatModelo(reg);
        }

        public bool DeleteRegProCatModelo(ProCatModelo reg)
        {
            return new EntitiesBussiness().DeleteRegProCatModelo(reg);
        }

        #endregion

        #region ProDiviciones

        public List<ProDiviciones> GetListProCatDiviciones()
        {
            return new EntitiesBussiness().GetListProCatDiviciones();
        }

        public ProDiviciones NewRegProDiviciones(ProDiviciones reg)
        {
            return new EntitiesBussiness().NewRegProDiviciones(reg);
        }

        public bool UpdateRegProDiviciones(ProDiviciones reg)
        {
            return new EntitiesBussiness().UpdateRegProDiviciones(reg);
        }

        public bool DeleteRegProDiviciones(ProDiviciones reg)
        {
            return new EntitiesBussiness().DeleteRegProDiviciones(reg);
        }

        #endregion
    }
}
