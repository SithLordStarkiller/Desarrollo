using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Mcs.BussinessLogic.Catalogos;
using Mcs.Core.Common;
using Mcs.Entities;

namespace Mcs.ServiceData.Controllers
{
    /// <summary>
    /// Api Service que controla las Zonas. 
    /// </summary>
    [RoutePrefix("api/Zonas")]
    public class ZonasController : ApiController
    {
        /// <summary>
        /// Obtiene todas las Zonas.
        /// </summary>
        /// <returns>Devuelve un objeto genérico que contiene la respuesta de la solicitud.
        /// Este esta compuesto por lo siguente:
        /// Success: Indica si la respuesta fue correcta.
        /// TotalRows: Total de registros obtenidos.
        /// Message: Mensaje de error o informativo que devuelve el servicio.
        /// Value: Contiene el resultado de la solicitud; en este caso una lista de todas las Zonas.
        /// </returns>
        [Route("GetZonas")]
        [HttpGet]
        public ResponseDto<List<BeZona>> Get()
        {
            return new Zonas().GetZonas();

        }
    }
}
