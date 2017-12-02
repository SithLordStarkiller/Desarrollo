using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using GOB.SPF.ConecII.Entities.Plantilla;
using GOB.SPF.ConecII.Business.Plantilla;
using GOB.SPF.ConecII.Entities.DTO;
using System.Threading.Tasks;
using GOB.SPF.ConecII.Entities;
using System.Data.SqlClient;
using System.Diagnostics;
using GOB.SPF.Conec.Services;
using GOB.SPF.ConecII.Business;
using GOB.SPF.Conec.Services.Controllers;

namespace Cns.Sies.Rest.Mvc.Areas.Plantilla.Controllers
{
    public class ParrafosController : BaseController<RequestParrafos, Parrafos>
    {
        ParrafosBusiness mAdministraParrafos;

        public ParrafosController()
        {
            mAdministraParrafos = new ParrafosBusiness();
            mBusiness = mAdministraParrafos;
        }

        [HttpPost]
        [Route("api/Parrafos/ObtenerTodos")]
        public async Task<IHttpActionResult> ParrafosObtenerTodos(RequestParrafos entity)
        {
            return Ok(ObtenerTodos(entity));
        }

        [HttpPost]
        [Route("api/Parrafos/ObtenerPorCriterio")]
        public async Task<IHttpActionResult> ParrafosObtenerPorCriterio(RequestParrafos entity)
        {
            return Ok(ObtenerPorCriterio(entity));
        }

        [HttpPost]
        [Route("api/Parrafos/ObtenerId/{id}")]
        public async Task<IHttpActionResult> ParrafosObtenerPorId(RequestParrafos entity)
        {
            return Ok(ObtenerPorId(entity));
        }

        [HttpPost]
        [Route("api/Parrafos/Save")]
        public async Task<IHttpActionResult> ParrafosGuardar(RequestParrafos entity)
        {
            return Ok(Guardar(entity));
        }
        
        [HttpPost]
        [Route("api/Parrafos/CambiarEstatus")]
        public async Task<IHttpActionResult> ParrafosCambiarEstatus(RequestParrafos entity)
        {
            return Ok(CambiarEstatus(entity));
        }

        [HttpPost]
        [Route("api/Parrafos/DropDownList")]
        public async Task<IHttpActionResult> ParrafosObtenerDropDownList(RequestParrafos entity)
        {
            return Ok(ObtenerDropDownList(entity));
        }

        [HttpPost]
        [Route("api/Parrafos/DropDownListCriterio")]
        public async Task<IHttpActionResult> ParrafosObtenerDropDownListCriterio(RequestParrafos entity)
        {
            return Ok(ObtenerDropDownListCriterio(entity));
        }


    }
}