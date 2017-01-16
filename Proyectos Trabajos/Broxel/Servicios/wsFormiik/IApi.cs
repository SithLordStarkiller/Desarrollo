using System.ServiceModel;
using System.ServiceModel.Web;
using System.IO;

namespace PubliPayments
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name 
    //"IApi" in both code and config file together.
    [ServiceContract(Namespace = Constantes.Namespace)]
    public interface IApi
    {
        //Expone ValidateUserSimple
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Xml,
            ResponseFormat = WebMessageFormat.Xml,
            UriTemplate = "ValidateUserSimple"
            )]
        string ValidateUserSimple(Stream usuario);
        
        //Expone SendWorkOrderToClient
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Xml,
            ResponseFormat = WebMessageFormat.Xml,
            UriTemplate = "SendWorkOrderToClient"
            )]

        Stream SendWorkOrderToClient(Stream respuesta);

        //Expone GetUserCatalog
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Xml,
            ResponseFormat = WebMessageFormat.Xml,
            UriTemplate = "GetUserCatalog"
            )]

        string GetUserCatalog(Stream usuario);

        //Expone SendErrors
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Xml,
            ResponseFormat = WebMessageFormat.Xml,
            UriTemplate = "SendErrors"
            )]

        void SendErrors(Stream errores);

        //Expone ValidateUserForDevice
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Xml,
            ResponseFormat = WebMessageFormat.Xml,
            UriTemplate = "ValidateUserForDevice"
            )]
        string ValidateUserForDevice(Stream usuario);

        //Expone FlexibleUpdateWorkOrder
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Xml,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "FlexibleUpdateWorkOrder"
            )]
        string FlexibleUpdateWorkOrder(Stream updateorder);
        
         //Expone ReceiveStatusNotification
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Xml,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "ReceiveStatusNotification"
            )]
        string ReceiveStatusNotification(Stream notification);

        //Expone ReceiveStatus
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Xml,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "UpdateStatusNotification"
            )]
        Stream UpdateStatusNotification(Stream notification);
      }
}
