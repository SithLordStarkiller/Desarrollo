using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using PubliPayments.Entidades;
using PubliPayments.Negocios;

namespace wsServiciosExternos
{
    /// <summary>
    /// Summary description for ServiciosExternos
    /// </summary>
    [WebService(Namespace = "https://servicios.publipayments.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ServiciosExternos : System.Web.Services.WebService
    {
        [WebMethod]
        public String Echo()
        {
            return ModelServiciosExternos.Echo();
        }

        [WebMethod]
        public List<PubliPayments.Entidades.UsuarioRelacion> ObtenerRelacionUsuarios(PubliPayments.Entidades.TipoConsulta tipoConsulta, String id)
        {
            return ModelServiciosExternos.ObtenerRelacionUsuarios(tipoConsulta, id);
        }

        [WebMethod]
        public List<int> OrdenesAutorizadas()
        {
            return ModelServiciosExternos.OrdenesAutorizadas();
        }

        [WebMethod]
        public List<PubliPayments.Entidades.ValorRespuesta> Respuestas()
        {
            return ModelServiciosExternos.Respuestas();
        }

        [WebMethod]
        public object[][] Dictamenes()
        {
            return new Dictionary<int, string>().ToList();
        }
    }
}
