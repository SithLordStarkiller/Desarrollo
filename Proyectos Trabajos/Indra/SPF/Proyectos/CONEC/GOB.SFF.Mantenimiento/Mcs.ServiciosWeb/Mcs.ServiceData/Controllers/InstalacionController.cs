using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Mcs.Core.Common;
using Mcs.Entities;
using Mcs.Entities.Mcs;

namespace Mcs.ServiceData.Controllers
{
    /// <summary>
    /// Api Service que controla el/las Instalacion. 
    /// </summary>
    [RoutePrefix("api/Instalacion")]
    public class InstalacionController : ApiController
    {
        /// <summary>
        /// Obtiene todas las instalaciones.
        /// </summary>
        /// <returns>Devuelve un objeto genérico que contiene la respuesta de la solicitud.
        /// Este esta compuesto por lo siguente:
        /// Success: Indica si la respuesta fue correcta.
        /// TotalRows: Total de registros obtenidos.
        /// Message: Mensaje de error o informativo que devuelve el servicio.
        /// Value: Contiene el resultado de la solicitud; en este caso una lista de todas las instalaciones.
        /// </returns>
        [Route("GetInstalacion")]
        [HttpGet]
        [AllowAnonymous]
        public ResponseDto<List<BeInstalacion>> Get()
        {
            return new BussinessLogic.Catalogos.Mcs.Instalacion().GetInstalaciones();
        }
    }
}
