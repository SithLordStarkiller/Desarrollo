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
    public class EtiquetasController : BaseController<RequestEtiquetas, Etiquetas>
    {

        EtiquetasBusiness mAdministraEtiquetas;

        public EtiquetasController()
        {
            mAdministraEtiquetas = new EtiquetasBusiness();
            mBusiness = mAdministraEtiquetas;
        }

        [HttpPost]
        [Route("api/Etiquetas/ObtenerTodos")]
        public async Task<IHttpActionResult> EtiquetasObtenerTodos(RequestEtiquetas entity)
        {
            return Ok(ObtenerTodos(entity));
        }

        [HttpPost]
        [Route("api/Etiquetas/ObtenerPorCriterio")]
        public async Task<IHttpActionResult> EtiquetasObtenerPorCriterio(RequestEtiquetas entity)
        {
            return Ok(ObtenerPorCriterio(entity));
        }

        [HttpPost]
        [Route("api/Etiquetas/ObtenerId/{id}")]
        public async Task<IHttpActionResult> EtiquetasObtenerPorId(RequestEtiquetas entity)
        {
            return Ok(ObtenerPorId(entity));
        }

        [HttpPost]
        [Route("api/Etiquetas/Save")]
        public async Task<IHttpActionResult> EtiquetasGuardar(RequestEtiquetas entity)
        {
            return Ok(Guardar(entity));
        }

        [HttpPost]
        [Route("api/Etiquetas/CambiarEstatus")]
        public async Task<IHttpActionResult> EtiquetasCambiarEstatus(RequestEtiquetas entity)
        {
            return Ok(CambiarEstatus(entity));
        }


    }
}