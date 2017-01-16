using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using PubliPayments.Entidades;

namespace wsServiciosExternos
{
    
    [ServiceContract]
    public interface IServicioIA
    {
        [OperationContract]
        [WebInvoke(Method = "POST")]
        String Echo();

        [OperationContract]
        [WebInvoke(Method = "POST")]
        List<UsuarioRelacion> ObtenerRelacionUsuarios(TipoConsulta tipoConsulta,String id);

        [OperationContract]
        [WebInvoke(Method = "POST")]
        List<int> OrdenesAutorizadas();

        [OperationContract]
        [WebInvoke(Method = "POST")]
        List<ValorRespuesta> Respuestas();

        [OperationContract]
        [WebInvoke(Method = "POST")]
        Dictionary<int,String> Dictamenes();
    }


    
}
