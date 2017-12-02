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
    /// 
    /// </summary>
    [RoutePrefix("api/Jerarquias")]
    public class JerarquiasController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("GetJerarquias")]
        [HttpGet]
        [AllowAnonymous]
        public ResponseDto<List<BeJerarquia>> Get()
        {
            return new Jerarquias().GetJerarquias();
        }
    }
}
