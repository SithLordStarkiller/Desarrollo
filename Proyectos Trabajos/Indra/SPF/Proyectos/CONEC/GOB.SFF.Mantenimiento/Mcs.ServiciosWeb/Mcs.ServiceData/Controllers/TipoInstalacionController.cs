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
    /// Api Service que controla el/las TiposInstalcion. 
    /// </summary>
    [RoutePrefix("api/TiposInstalacion")]
    public class TipoInstalacionController : ApiController
    {
        /// <summary>
        /// Obtiene todas los Tipos de instalación.
        /// </summary>
        /// <returns>Devuelve un objeto genérico que contiene la respuesta de la solicitud.
        /// Este esta compuesto por lo siguente:
        /// Success: Indica si la respuesta fue correcta.
        /// TotalRows: Total de registros obtenidos.
        /// Message: Mensaje de error o informativo que devuelve el servicio.
        /// Value: Contiene el resultado de la solicitud; en este caso una lista de todas los Tipos de instalación.
        /// </returns>
        [Route("GetTiposInstalacion")]
        [HttpGet]
        [AllowAnonymous]
        public ResponseDto<List<BeTipoInstalacion>> Get()
        {
            return new BussinessLogic.Catalogos.Mcs.TipoInstalacion().GetTiposInstalacion();
        }
    }
}
