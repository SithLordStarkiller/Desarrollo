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
    public class PartesDocumentoController : BaseController<RequestPartesDocumento, PartesDocumento>
    {

        PartesDocumentoBusiness mAdministraPartesDocumento;

        public PartesDocumentoController()
        {
            mAdministraPartesDocumento = new PartesDocumentoBusiness();
            mBusiness = mAdministraPartesDocumento;
        }

        [HttpPost]
        [Route("api/PartesDocumento/ObtenerTodos")]
        public async Task<IHttpActionResult> PartesDocumentoObtenerTodos(RequestPartesDocumento entity)
        {
            return Ok(ObtenerTodos(entity));
        }

        [HttpPost]
        [Route("api/PartesDocumento/ObtenerPorCriterio")]
        public async Task<IHttpActionResult> PartesDocumentoObtenerPorCriterio(RequestPartesDocumento entity)
        {
            return Ok(ObtenerPorCriterio(entity));
        }

        [HttpPost]
        [Route("api/PartesDocumento/ObtenerPorId")]
        public async Task<IHttpActionResult> PartesDocumentoObtenerPorId(RequestPartesDocumento entity)
        {
            return Ok(ObtenerPorId(entity));
        }

        [HttpPost]
        [Route("api/PartesDocumento/Save")]
        public async Task<IHttpActionResult> PartesDocumentoGuardar(RequestPartesDocumento entity)
        {
            return Ok(Guardar(entity));
        }

        [HttpPost]
        [Route("api/PartesDocumento/CambiarEstatus")]
        public async Task<IHttpActionResult> PartesDocumentoCambiarEstatus(RequestPartesDocumento entity)
        {
            return Ok(CambiarEstatus(entity));
        }

        [HttpPost]
        [Route("api/PartesDocumento/ObtenerPorIdTipoDocumento")]
        public async Task<IHttpActionResult> ObtenerPorIdTipoDocumento(RequestPartesDocumento entity)
        {
            ResultPage<PartesDocumento> result = new ResultPage<PartesDocumento>();
            try
            {
                result.Entity = mAdministraPartesDocumento.ObtenerPorIdTipoDocumento(entity.Item);
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