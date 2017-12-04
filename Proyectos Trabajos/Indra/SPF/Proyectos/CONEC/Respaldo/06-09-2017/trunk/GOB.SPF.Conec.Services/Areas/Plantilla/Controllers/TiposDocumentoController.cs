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
using GOB.SPF.ConecII.Entities.Request;
using System.Data.SqlClient;
using System.Diagnostics;
using GOB.SPF.ConecII.Business;
using GOB.SPF.Conec.Services;


namespace Cns.Sies.Rest.Mvc.Areas.Plantilla.Controllers
 {
     public class TiposDocumentoController : ApiController
    {

        GOB.SPF.ConecII.Business.Plantilla.TiposDocumentoBusiness mAdministraTiposDocumento;
 
         public TiposDocumentoController()
         {
            mAdministraTiposDocumento = new GOB.SPF.ConecII.Business.Plantilla.TiposDocumentoBusiness();
         }

        [HttpPost]
        [Route("api/TiposDocumento/ObtenerTodos")]
        public async Task<IHttpActionResult> TipoDocumentoObtenerTodos(RequestTiposDocumento entity)
        {
            ResultPage<TiposDocumento> result = new ResultPage<TiposDocumento>();
            try
            {

                result.List.AddRange(mAdministraTiposDocumento.ObtenerTodos(entity.Paging));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = mAdministraTiposDocumento.Pages;
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

        [HttpPost]
        [Route("api/TiposDocumento/ObtenerPorCriterio")]
        public async Task<IHttpActionResult> TipoDocumentoObtenerPorCriterio(RequestTiposDocumento entity)
        {
            ResultPage<TiposDocumento> result = new ResultPage<TiposDocumento>();
            try
            {

                result.List.AddRange(mAdministraTiposDocumento.ObtenerPorCriterio(entity.Paging, entity.Item));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = mAdministraTiposDocumento.Pages;
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

        [HttpPost]
        [Route("api/TiposDocumento/ObtenerId/{id}")]
        public async Task<IHttpActionResult> TipoDocumentoObtenerPorId(RequestTiposDocumento entity)
        {
            ResultPage<TiposDocumento> result = new ResultPage<TiposDocumento>();
            try
            {
                //mAdministraTiposDocumento.CuotasBusiness mAdministraTiposDocumento = new mAdministraTiposDocumento.CuotasBusiness();
                result.Entity = mAdministraTiposDocumento.ObtenerPorId((long)entity.Item.Identificador);
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

        [HttpPost]
        [Route("api/TiposDocumento/Save")]
        public async Task<IHttpActionResult> TipoDocumentoGuardar(RequestTiposDocumento entity)
        {
            ResultPage<TiposDocumento> result = new ResultPage<TiposDocumento>();
            try
            {

                result.Success = mAdministraTiposDocumento.Guardar(entity.Item);
            }
            catch (SqlException e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                result.Errors.Add(new Error { Code = e.ErrorCode, Message = ResourceExceptionMessage.MensajeErrorAplicacion });
            }
            catch (ConecException ex)
            {
                result.Errors.Add(new Error { Code = 1000, Message = ex.Message });
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                result.Errors.Add(new Error { Code = System.Runtime.InteropServices.Marshal.GetExceptionCode(), Message = ResourceExceptionMessage.MensajeErrorAplicacion });
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("api/TiposDocumento/CambiarEstatus")]
        public async Task<IHttpActionResult> TipoDocumentoCambiarEstatus(RequestTiposDocumento entity)
        {
            ResultPage<TiposDocumento> result = new ResultPage<TiposDocumento>();
            try
            {

                result.Success = mAdministraTiposDocumento.CambiarEstatus(entity.Item);
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
