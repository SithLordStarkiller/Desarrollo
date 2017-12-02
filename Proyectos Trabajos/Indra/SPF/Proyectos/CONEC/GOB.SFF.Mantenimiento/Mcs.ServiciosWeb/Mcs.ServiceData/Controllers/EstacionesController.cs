using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Mcs.Core.Common;
using Mcs.Entities;
using Mcs.BussinessLogic.Catalogos;
namespace Mcs.ServiceData.Controllers
{
    /// <summary>
    /// Api Service que controla las Estaciones. 
    /// </summary>
    [RoutePrefix("api/Estaciones")]
    public class EstacionesController : ApiController
    {
        /// <summary>
        /// Obtiene todas las Estaciones.
        /// </summary>
        /// <returns>Devuelve un objeto genérico que contiene la respuesta de la solicitud.
        /// Este esta compuesto por lo siguente:
        /// Success: Indica si la respuesta fue correcta.
        /// TotalRows: Total de registros obtenidos.
        /// Message: Mensaje de error o informativo que devuelve el servicio.
        /// Value: Contiene el resultado de la solicitud; en este caso una lista de todas las Estaciones.
        /// </returns>
        [Route("GetEstaciones")]
        [HttpGet]
        [AllowAnonymous]
        public ResponseDto<List<BeEstacion>> Get()
        {
            return new Estaciones().GetEstaciones();
        }
    }
}
