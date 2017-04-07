using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;
using wsServices01800.Formiik;
using PubliPayments.Entidades.Originacion;

namespace wsServices01800
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "Iservices" in both code and config file together.
    [ServiceContract]
    public interface Iservices
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "FlexibleUpdateWorkOrder"
            )]
        FlexibleUpdateResponse FlexibleUpdateWorkOrder(Stream r);

        //Expone SendWorkOrderToClient
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Xml,
            ResponseFormat = WebMessageFormat.Xml,
            UriTemplate = "SendWorkOrderToClient"
            )]

        Stream SendWorkOrderToClient(Stream respuesta);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Xml,
            ResponseFormat = WebMessageFormat.Xml,
            UriTemplate = "ValidateUserSimpleReturnGroup"
            )]
        Stream ValidateUserSimpleReturnGroup(Stream streamUser);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Xml,
            ResponseFormat = WebMessageFormat.Xml,
            UriTemplate = "ValidateUserForDeviceReturnGroup"
            )]
        Stream ValidateUserForDeviceReturnGroup(Stream streamUserDevice);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Xml,
            ResponseFormat = WebMessageFormat.Xml,
            UriTemplate = "GetUserCatalog"
            )]
        Stream GetUserCatalog(Stream streamUserDevice);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "ObtenerDocumentosExcepcion"
            )]
        string ObtenerDocumentosExcepcion(DocumentosOrigacion modelo);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Xml,
            ResponseFormat = WebMessageFormat.Xml,
            UriTemplate = "GenerarDocSolicitudCreditoSimple"
            //BodyStyle = WebMessageBodyStyle.WrappedRequest
            )]
        //string GenerarDocSolicitudCreditoSimple(SolicitudInscripcionCreditoSimpleModel solicitud);
        string GenerarDocSolicitudCreditoSimple(Stream streamuser);
        //string GenerarDocSolicitudCreditoSimple(string texto);
        
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "RegenerarDocumentos"
            //BodyStyle = WebMessageBodyStyle.WrappedRequest
            )]
        //string GenerarDocSolicitudCreditoSimple(SolicitudInscripcionCreditoSimpleModel solicitud);
        string RegenerarDocumentos(Stream streamuser);
        //string GenerarDocSolicitudCreditoSimple(string texto);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "RecepcionRefNumCredito"
            //BodyStyle = WebMessageBodyStyle.WrappedRequest
            )]
        int RecepcionRefNumCredito(EReferenciaNumeroCredito modelo);


        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "ListaRefNumCredito"
            //BodyStyle = WebMessageBodyStyle.WrappedRequest
            )]
        string ListaRefNumCredito();
    }
}
