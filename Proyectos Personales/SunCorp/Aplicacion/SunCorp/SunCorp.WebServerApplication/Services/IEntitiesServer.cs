namespace SunCorp.WebServerApplication.Services
{
    using Entities.Generic;
    using System.Collections.Generic;
    using System.ServiceModel;
    using Entities;

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEntitiesServer" in both code and config file together.

    [ServiceContract]
    public interface IEntitiesServer
    {
        [OperationContract]
        UsUsuarios GetUsUsuarios(UserSession session);

        [OperationContract]
        List<UsZona> GetListUsZona();

        [OperationContract]
        List<UsZona> GetListUsZonaPageList(int page, int numRows, ref int totalRows, bool includeDelete);

        #region UsZona

        [OperationContract]
        List<UsZona> GetListUsZonasUser(UsUsuarios user);

        [OperationContract]
        UsZona NewRegUsZona(UsZona zona);

        [OperationContract]
        bool UpdateRegUsZona(UsZona zona);

        [OperationContract]
        bool DeleteRegUsZona(UsZona zona);

        [OperationContract]
        UsTipoUsuario GetTypeUser(UsUsuarios user);

        #endregion

        #region ProCatMarca

        [OperationContract]
        List<ProCatMarca> GetListProCatMarca();

        [OperationContract]
        ProCatMarca NewRegProCatMarca(ProCatMarca reg);

        [OperationContract]
        bool UpdateRegProCatMarca(ProCatMarca reg);

        [OperationContract]
        bool DeleteRegProCatMarca(ProCatMarca reg);

        #endregion

        #region ProCatModelo

        [OperationContract]
        List<ProCatModelo> GetListProCatModelo();

        [OperationContract]
        ProCatModelo NewRegProCatModelo(ProCatModelo reg);

        [OperationContract]
        bool UpdateRegProCatModelo(ProCatModelo reg);

        [OperationContract]
        bool DeleteRegProCatModelo(ProCatModelo reg);

        #endregion

        #region ProDiviciones

        [OperationContract]
        List<ProDiviciones> GetListProCatDiviciones();

        [OperationContract]
        ProDiviciones NewRegProDiviciones(ProDiviciones reg);

        [OperationContract]
        bool UpdateRegProDiviciones(ProDiviciones reg);

        [OperationContract]
        bool DeleteRegProDiviciones(ProDiviciones reg);

        #endregion
    }
}
