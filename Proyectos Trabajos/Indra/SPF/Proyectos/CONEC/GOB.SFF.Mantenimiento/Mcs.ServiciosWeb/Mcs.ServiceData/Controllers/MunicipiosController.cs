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
    /// Api Service que controla los Municipios. 
    /// </summary>
    [RoutePrefix("api/Municipios")]
    public class MunicipiosController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //[Route("GetMunicipios")]
        //public ResponseDto<List<BeMunicipio>> Get()
        //{
        //    Municipios municipios = new Municipios();
        //    var result = municipios.GetMunicipios();
        //    return result;
        //}

        /// <summary>
        /// Devuelve el/los municipios por entidad federativa.
        /// </summary>
        /// <param name="idEstado">Clave de la entidad federativa</param>
        /// <returns>Objeto con el/los municipios.</returns>
        [Route("GetMunicipiosPorEstado/{idEstado}")]
        [HttpGet]
        public ResponseDto<List<BeMunicipio>> Get(int idEstado)
        {
            Municipios municipios = new Municipios();
            var result = municipios.GetMunicipios(idEstado);
            return result;
        }
    }
}
