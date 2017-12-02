using System.Collections.Generic;
using System.Web.Http;
using Mcs.Core.Common;
using Mcs.BussinessLogic.Catalogos;
using Mcs.Entities;

namespace Mcs.ServiceData.Controllers
{
    /// <summary>
    /// Api Service que controla el/los Asentamiento(s). 
    /// </summary>
    [RoutePrefix("api/Asentamientos")]
    public class AsentamientosController : ApiController
    {
        /// <summary>
        /// Consulta el/los asentamientos de acuerdo a los parametros o filtros indicados.
        /// </summary>
        /// <param name="request">Objeto request que contiene los valores de busqueda.</param>
        /// <returns>Devuelve un objeto genérico que contiene el resultado de la consulta de los asentamientos.</returns>
        [Route("GetAsentamientos")]
        //[AcceptVerbs("POST","GET")]
        [HttpPost]
        [AllowAnonymous]
        public ResponseDto<List<BeAsentamiento>> Get(BeAsentamiento request)
        {
            return new Asentamientos().GetAsentamientos(request);
        }
    }
}
