using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Mcs.BussinessLogic.Catalogos;
using Mcs.Core.Common;
using  Mcs.Entities;
namespace Mcs.ServiceData.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("api/GrupoTarifario")]
    public class GrupoTarifarioController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("GetGrupoTarifarios")]
        //[AcceptVerbs("POST","GET")]
        [HttpGet]
        [AllowAnonymous]
        public ResponseDto<List<BeGrupoTarifario>> Get()
        {
            return new GrupoTarifario().GetGrupoTarifario();
        }
    }
}
