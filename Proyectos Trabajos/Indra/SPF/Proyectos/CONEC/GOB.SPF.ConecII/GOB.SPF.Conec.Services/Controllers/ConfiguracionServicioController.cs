namespace GOB.SPF.Conec.Services.Controllers
{
    using ConecII.Entities.DTO;
    using ConecII.Entities.Request;
    using GOB.SPF.Conec.Services.Models;
    using GOB.SPF.Conec.Services.Validators;
    using GOB.SPF.ConecII.Entities;
    using System;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using business = ConecII.Business;

    /// <summary>
    /// Servicio de catálogos
    /// </summary>
    [RoutePrefix("api/ConfiguracionServicio")]
    public class ConfiguracionServicioController : ApiController
    {
        [HttpPost]
        [Route("ConfiguracionServicio/ObtenerTodoPaginadoDDD")]
        public IHttpActionResult ConfiguracionServicioObtenerPaginado(RequestConfiguracionServicio entity)
        {
            ResultPage<ConfiguracionServicioDTO> result = new ResultPage<ConfiguracionServicioDTO>();

            try
            {
                business.ConfiguracionServicioBussines bussiness = new business.ConfiguracionServicioBussines();
                result.List.AddRange(bussiness.ObtenerTodosPaginados(entity.Paging));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = bussiness.Pages;
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
        [Route("ConfiguracionServicio/ObtenerPorId")]
        public IHttpActionResult ConfiguracionServicioObtenerPorIdConfiguracionServicio(int idConfiguracionServicio)
        {
            ResultPage<ConfiguracionServicioDTO> result = new ResultPage<ConfiguracionServicioDTO>();

            try
            {
                business.ConfiguracionServicioBussines bussiness = new business.ConfiguracionServicioBussines();
                result.List.AddRange(bussiness.ObtenerTodosPorIdConfiguracionServicio(idConfiguracionServicio));
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
        [Route("ConfiguracionServicio/ObtenerPorIdTipoServicioIdCentroCosto")]
        public IHttpActionResult ConfiguracionServicioObtenerPorIdTipoServicioIdCentroCosto(int idTipoServicio, string idCentroCosto)
        {
            ResultPage<ConfiguracionServicioDTO> result = new ResultPage<ConfiguracionServicioDTO>();
            try
            {
                business.ConfiguracionServicioBussines bussiness = new business.ConfiguracionServicioBussines();
                result.List.AddRange(bussiness.ObtenerTodosPorIdTipoServicioIdCentroCosto(idTipoServicio, idCentroCosto));
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
        [Route("ConfiguracionServicio/InsertarConfiguracionesDDDD")]
        public IHttpActionResult ConfiguracionServicioInsertarConfiguraciones(  RequestConfiguracionServicio entity)
        {
            int result = 0;
            try
            {
                business.ConfiguracionServicioBussines business = new business.ConfiguracionServicioBussines();
                result = business.Guardar(entity.ListInsertar);
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

        #region CONFIGURACION_SOLICITUD_SERVICIO
        [HttpPost]
        [Route("CampoServicio/ObtenerPorIdTipoServicio")]
        public IHttpActionResult CampoServiciosObtenerPorIdTipoServicio (RequestTipoServicio entity)
        {
            ResultPage<ConfiguracionCampoServicio> result = new ResultPage<ConfiguracionCampoServicio>();
            result.Success = false;
            try
            {
                business.CampoServicioBusiness business = new business.CampoServicioBusiness();
                result.Entity = business.ObtenerPorId(entity.Item.Identificador);
                result.Success = true;
            }
            catch (SqlException e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                result.Errors.Add(new Error { Message = e.Message });
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                result.Errors.Add(new Error { Message = e.Message });
            }

            return Ok(result);
        }

        #endregion

    }
}