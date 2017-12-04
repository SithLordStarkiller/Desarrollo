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
    public class ContratantesController : BaseController<RequestContratantes, Contratantes>
    {
        ContratantesBusiness mAdministraContratantes;
        
        public ContratantesController()
        {
            mAdministraContratantes = new ContratantesBusiness();
            mBusiness = mAdministraContratantes;
        }

        [HttpPost]
        [Route("api/Contratantes/ObtenerTodos")]
        public async Task<IHttpActionResult> ContratantesObtenerTodos(RequestContratantes entity)
        {
            return Ok(ObtenerTodos(entity));
        }

        [HttpPost]
        [Route("api/Contratantes/ObtenerPorCriterio")]
        public async Task<IHttpActionResult> ContratantesObtenerPorCriterio(RequestContratantes entity)
        {
            return Ok(ObtenerPorCriterio(entity));
        }

        [HttpPost]
        [Route("api/Contratantes/ObtenerId/{id}")]
        public async Task<IHttpActionResult> ContratantesObtenerPorId(RequestContratantes entity)
        {
            return Ok(ObtenerPorId(entity));
        }

        [HttpPost]
        [Route("api/Contratantes/Save")]
        public async Task<IHttpActionResult> ContratantesGuardar(RequestContratantes entity)
        {
            return Ok(Guardar(entity));
        }

        [HttpPost]
        [Route("api/Contratantes/CambiarEstatus")]
        public async Task<IHttpActionResult> ContratantesCambiarEstatus(RequestContratantes entity)
        {
            return Ok(CambiarEstatus(entity));
        }

        
    }
}