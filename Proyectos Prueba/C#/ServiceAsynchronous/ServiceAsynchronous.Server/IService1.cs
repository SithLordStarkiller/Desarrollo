using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ServiceAsynchronous.Server
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginConsulta(int pTotalPosts, AsyncCallback pCallback, object state);

        List<string> EndConsulta(IAsyncResult pResultado);
    }
}
