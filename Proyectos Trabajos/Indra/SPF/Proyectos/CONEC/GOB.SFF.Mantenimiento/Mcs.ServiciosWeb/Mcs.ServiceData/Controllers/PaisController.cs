using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Mcs.BussinessLogic.Catalogos;
using Mcs.BussinessLogic.Catalogos.Mcs;
using Mcs.Core.Common;
using Mcs.Entities.Mcs;
namespace Mcs.ServiceData.Controllers
{
    /// <summary>
    /// Clase controlador del catálogo de paises (Sicogua).
    /// </summary>
    [RoutePrefix("api/Paises")]
    public class PaisController : ApiController
    {
        /// <summary>
        /// Devuelve el/los paises del catálogo de paises.
        /// </summary>
        /// <returns>Objeto que contiene el resultado de la solicitud (mensaje, error en caso de aberlo y/o una lista de los paises).</returns>
        [Route("GetPaises")]
        [HttpGet]
        public ResponseDto<List<BePais>> GetAll()
        {
            return new Pais().GetAllPaises();
        }

        /// <summary>
        /// Devuelve el pais.
        /// </summary>
        /// <param name="id">Clave del pais.</param>
        /// <returns>Objeto que contiene el resultado de la solicitud (mensaje, error en caso de aberlo y/o una endidad del pais).</returns>
        [Route("GetPaisesPorId/{id}")]
        [HttpGet]
        public ResponseDto<BePais> GetPaisesById(int id)
        {
            return new Pais().GetPaisById(id);
        }
    }
}
