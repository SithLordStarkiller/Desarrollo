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
    public class InstitucionesController : BaseController<RequestInstituciones, Instituciones>
    {

        InstitucionesBusiness mAdministraInstituciones;

        public InstitucionesController()
        {
            mAdministraInstituciones = new InstitucionesBusiness();
            mBusiness = mAdministraInstituciones;
        }

        [HttpPost]
        [Route("api/Instituciones/ObtenerTodos")]
        public async Task<IHttpActionResult> InstitucionesObtenerTodos(RequestInstituciones entity)
        {
            return Ok(ObtenerTodos(entity));
        }

        [HttpPost]
        [Route("api/Instituciones/ObtenerPorCriterio")]
        public async Task<IHttpActionResult> InstitucionesObtenerPorCriterio(RequestInstituciones entity)
        {
            return Ok(ObtenerPorCriterio(entity));
        }

        [HttpPost]
        [Route("api/Instituciones/ObtenerId/{id}")]
        public async Task<IHttpActionResult> InstitucionesObtenerPorId(RequestInstituciones entity)
        {
            return Ok(ObtenerPorId(entity));
        }

        [HttpPost]
        [Route("api/Instituciones/Save")]
        public async Task<IHttpActionResult> InstitucionesGuardar(RequestInstituciones entity)
        {
            return Ok(Guardar(entity));
        }

        [HttpPost]
        [Route("api/Instituciones/CambiarEstatus")]
        public async Task<IHttpActionResult> InstitucionesCambiarEstatus(RequestInstituciones entity)
        {
            return Ok(CambiarEstatus(entity));
        }


    }
}