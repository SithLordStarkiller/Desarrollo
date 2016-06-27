﻿namespace SunCorp.Server.Services
{
    using System.Collections.Generic;
    using Entities.Generic;
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
