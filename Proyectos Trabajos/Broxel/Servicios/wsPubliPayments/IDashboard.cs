using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using PubliPayments.Entidades;

namespace wsPubliPayments
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IDashboard" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IDashboard
    {
        [OperationContract]
        [WebInvoke(Method = "POST")]
        List<IndicadorDashboard> ObtenerIndicadorJson(string token, List<String> valores, String tipoDashboard, String sUser, int nUser, int nRol, int nDominio,String indicador);

        [OperationContract]
        [WebInvoke(Method = "POST")]
        List<IndicadorDashboard>  ObtenerBloqueIndicadorJson(string token, List<String> valores, String tipoDashboard, String sUser, int nUser,int nRol, int nDominio, int parteTabla);

        [OperationContract]
        [WebInvoke(Method = "POST")]
        List<OpcionesFiltroDashboard> ObtenerListaFiltros(string token, String accion, List<String> valores, String usuario);

        [OperationContract]
        [WebInvoke(Method = "POST")]
        String Login(string valor);
    }
}
