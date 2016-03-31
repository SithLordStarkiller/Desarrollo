﻿namespace SunCorp.Server.Services
{
    using Entities.Generic;
    using System.ServiceModel;
    using Entities.Entities;

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEntitiesServer" in both code and config file together.

    [ServiceContract]
    public interface IEntitiesServer
    {
        [OperationContract]
        UsUsuarios GetUsUsuarios(UserSession session);
    }
}
