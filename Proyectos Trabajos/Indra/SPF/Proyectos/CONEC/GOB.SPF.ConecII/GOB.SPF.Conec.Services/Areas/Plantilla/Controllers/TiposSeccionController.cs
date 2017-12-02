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
    public class TiposSeccionController : BaseController<RequestTiposSeccion, TiposSeccion>
    {

        TiposSeccionBusiness mAdministraTiposSeccion;

        public TiposSeccionController()
        {
            mAdministraTiposSeccion = new TiposSeccionBusiness();
            mBusiness = mAdministraTiposSeccion;
        }

        [HttpPost]
        [Route("api/TiposSeccion/ObtenerTodos")]
        public async Task<IHttpActionResult> TiposSeccionObtenerTodos(RequestTiposSeccion entity)
        {
            return Ok(ObtenerTodos(entity));
        }

        [HttpPost]
        [Route("api/TiposSeccion/ObtenerPorCriterio")]
        public async Task<IHttpActionResult> TiposSeccionObtenerPorCriterio(RequestTiposSeccion entity)
        {
            return Ok(ObtenerPorCriterio(entity));
        }

        [HttpPost]
        [Route("api/TiposSeccion/ObtenerId/{id}")]
        public async Task<IHttpActionResult> TiposSeccionObtenerPorId(RequestTiposSeccion entity)
        {
            return Ok(ObtenerPorId(entity));
        }

        [HttpPost]
        [Route("api/TiposSeccion/Save")]
        public async Task<IHttpActionResult> TiposSeccionGuardar(RequestTiposSeccion entity)
        {
            return Ok(Guardar(entity));
        }

        [HttpPost]
        [Route("api/TiposSeccion/CambiarEstatus")]
        public async Task<IHttpActionResult> TiposSeccionCambiarEstatus(RequestTiposSeccion entity)
        {
            return Ok(CambiarEstatus(entity));
        }

        [HttpPost]
        [Route("api/TiposSeccion/DropDownList")]
        public async Task<IHttpActionResult> TiposSeccionObtenerDropDownList(RequestTiposSeccion entity)
        {
            return Ok(ObtenerDropDownList(entity));
        }
    }
}