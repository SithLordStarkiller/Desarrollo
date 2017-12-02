using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Mcs.Core.Common;
using Mcs.Entities;
namespace Mcs.ServiceData.Controllers
{
    /// <summary>
    /// Api Service que controla el/los Integrante(s). 
    /// </summary>
    [RoutePrefix("api/Integrantes")]
    public class IntegrantesController : ApiController
    {

            /// <summary>
            /// Obtiene todos los Integrantes.
            /// </summary>
            /// <returns>Devuelve un objeto genérico que contiene la respuesta de la solicitud.
            /// Este esta compuesto por lo siguente:
            /// Success: Indica si la respuesta fue correcta.
            /// TotalRows: Total de registros obtenidos.
            /// Message: Mensaje de error o informativo que devuelve el servicio.
            /// Value: Contiene el resultado de la solicitud; en este caso una lista de todas las Áreas (Centro de Costos).
            /// </returns>
            [Route("GetIntegrantes")]
            [HttpGet]
            [AllowAnonymous]
            public ResponseDto<List<BeIntegrantes>> Get()
            {
                return new BussinessLogic.Rep.Integrantes().GetIntegrantes();
            }
        
    }
}
