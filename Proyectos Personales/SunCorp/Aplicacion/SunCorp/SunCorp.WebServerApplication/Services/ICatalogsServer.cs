namespace SunCorp.WebServerApplication.Services
{
    using System.Collections.Generic;
    using Entities.Generic;
    using System.ServiceModel;
    using Entities;

    [ServiceContract]
    public interface ICatalogsServer
    {
        [OperationContract]
        List<GenericTable> GetListCatalogsSystem();

        [OperationContract]
        List<SisArbolMenu> GetListMenuForUserType(UsUsuarios user);
    }
}
