namespace SunCorp.Server.Services
{
    using System.Collections.Generic;
    using System.ServiceModel;
    using Entities.Generic;

    [ServiceContract]
    public interface ICatalogsServer
    {
        [OperationContract]
        List<GenericTable> GetListCatalogsSystem();
    }
}
