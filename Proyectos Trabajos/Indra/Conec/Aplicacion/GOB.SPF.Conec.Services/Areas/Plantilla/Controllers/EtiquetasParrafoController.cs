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
    public class EtiquetasParrafoController : BaseController<RequestEtiquetasParrafo, EtiquetasParrafo>
    {

        EtiquetasParrafoBusiness mAdministraEtiquetasParrafo;

        public EtiquetasParrafoController()
        {
            mAdministraEtiquetasParrafo = new EtiquetasParrafoBusiness();
            mBusiness = mAdministraEtiquetasParrafo;
        }

        [HttpPost]
        [Route("api/EtiquetasParrafo/ObtenerTodos")]
        public async Task<IHttpActionResult> EtiquetasParrafoObtenerTodos(RequestEtiquetasParrafo entity)
        {
            return Ok(ObtenerTodos(entity));
        }

        [HttpPost]
        [Route("api/EtiquetasParrafo/ObtenerPorCriterio")]
        public async Task<IHttpActionResult> EtiquetasParrafoObtenerPorCriterio(RequestEtiquetasParrafo entity)
        {
            return Ok(ObtenerPorCriterio(entity));
        }

        [HttpPost]
        [Route("api/EtiquetasParrafo/ObtenerId/{id}")]
        public async Task<IHttpActionResult> EtiquetasParrafoObtenerPorId(RequestEtiquetasParrafo entity)
        {
            return Ok(ObtenerPorId(entity));
        }

        [HttpPost]
        [Route("api/EtiquetasParrafo/Save")]
        public async Task<IHttpActionResult> EtiquetasParrafoGuardar(RequestEtiquetasParrafo entity)
        {
            return Ok(Guardar(entity));
        }

        [HttpPost]
        [Route("api/EtiquetasParrafo/CambiarEstatus")]
        public async Task<IHttpActionResult> EtiquetasParrafoCambiarEstatus(RequestEtiquetasParrafo entity)
        {
            return Ok(CambiarEstatus(entity));
        }

        [HttpPost]
        [Route("api/EtiquetasParrafo/ObtenerPorParteDocumento")]
        public async Task<IHttpActionResult> ObtenerPorIdParteDocumento(RequestEtiquetasParrafo entity)
        {
            ResultPage<EtiquetasParrafo> result = new ResultPage<EtiquetasParrafo>();
            try
            {
                result.List = mAdministraEtiquetasParrafo.ObtenerPorIdParteDocumento(entity.Paging, entity.Item);
                result.Success = true;
            }
            catch (SqlException e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
            }

            return Ok(result);
        }


    }
}