namespace GOB.SPF.Conec.Services.Controllers
{
    using ConecII.Entities.DTO;
    using ConecII.Entities.Request;
    using GOB.SPF.Conec.Services.Models;
    using GOB.SPF.Conec.Services.Validators;
    using ConecII.Business;
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
    [RoutePrefix("api/Catalog")]
    public class CatalogController : ApiController
    {

        #region DIVISION

       
        [HttpPost]
        [Route("Divisiones/ObtenerTodos")]
        public IHttpActionResult DivisionObtener(RequestDivision entity)
        {
            ResultPage<Division> result = new ResultPage<Division>();
            try
            {                
                business.DivisionBusiness business = new business.DivisionBusiness();
                result.List.AddRange(business.ObtenerTodos(entity.Paging));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
                result.Success = true;                
            }
            catch (SqlException e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
            }
            catch(Exception e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
            }
            
            return Ok(result);
        }

        [HttpPost]
        [Route("Divisiones/ObtenerListado")]
        public IHttpActionResult DivisionObtenerListado()
        {
            ResultPage<Division> result = new ResultPage<Division>();
            try
            {
                business.DivisionBusiness business = new business.DivisionBusiness();
                result.List.AddRange(business.ObtenerListado());
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
        [Route("Divisiones/ObtenerPorCriterio")]
        public IHttpActionResult DivisionObtenerPorCriterio(RequestDivision entity)
        {
            ResultPage<Division> result = new ResultPage<Division>();
            try
            {
                business.DivisionBusiness business = new business.DivisionBusiness();
                result.List.AddRange(business.ObtenerPorCriterio(entity.Paging,entity.Item));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("Divisiones/ObtenerPorId/{id}")]
        public async Task<IHttpActionResult> DivisionObtenerPorId(RequestDivision entity)
        {
            ResultPage<Division> result = new ResultPage<Division>();
            try
            {
                business.DivisionBusiness business = new business.DivisionBusiness();
                result.Entity = business.ObtenerPorId(entity.Item.Identificador);
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("Divisiones/CambiarEstatus")]
        public async Task<IHttpActionResult> DivisionCambiarEstatus(RequestDivision entity)
        {
            ResultPage<Division> result = new ResultPage<Division>();
            try
            {
                business.DivisionBusiness business = new business.DivisionBusiness();
                result.Success = business.CambiarEstatus(entity.Item) > 0;
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
        [Route("Divisiones/Save")]
        public IHttpActionResult DivisionGuardar(RequestDivision entity)
        {
            ResultPage<Division> result = new ResultPage<Division>();
            try
            {
                business.DivisionBusiness business = new business.DivisionBusiness();
                result.Success = business.Guardar(entity.Item) > 0;
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



        #endregion

        #region PERIODO

        [HttpPost]
        [Route("Periodos/ObtenerTodos")]
        public async Task<IHttpActionResult> PeriodoObtener(RequestPeriodo entity)
        {
            ResultPage<Periodo> result = new ResultPage<Periodo>();
            try
            {
                business.PeriodosBusiness business = new business.PeriodosBusiness();
                result.List.AddRange(business.ObtenerTodos(entity.Paging));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("Periodos/Save")]
        public async Task<IHttpActionResult> PeriodoGuardar(RequestPeriodo entity)
        {
            ResultPage<Periodo> result = new ResultPage<Periodo>();
            try
            {
                business.PeriodosBusiness business = new business.PeriodosBusiness();
                result.Success = business.Guardar(entity.Item) > 0;
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
        [Route("Periodos/ObtenerPorId/{id}")]
        public async Task<IHttpActionResult> PeriodoObtenerPorId(RequestPeriodo entity)
        {
            ResultPage<Periodo> result = new ResultPage<Periodo>();
            try
            {
                business.PeriodosBusiness business = new business.PeriodosBusiness();
                result.Entity = business.ObtenerPorId(entity.Item.Identificador);
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
        [Route("Periodos/CambiarEstatus")]
        public async Task<IHttpActionResult> PeriodoCambiarEstatus(RequestPeriodo entity)
        {
            ResultPage<Periodo> result = new ResultPage<Periodo>();
            try
            {
                business.PeriodosBusiness business = new business.PeriodosBusiness();
                result.Success = business.CambiarEstatus(entity.Item) > 0;
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
        [Route("Periodos/ObtenerPorCriterio")]
        public async Task<IHttpActionResult> PeriodoObtenerPorCriterio(RequestPeriodo entity)
        {
            ResultPage<Periodo> result = new ResultPage<Periodo>();
            try
            {
                business.PeriodosBusiness business = new business.PeriodosBusiness();
                result.List.AddRange(business.ObtenerPorCriterio(entity.Paging, entity.Item));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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

        #endregion

        #region TIPOS_SERVICIOS

        [HttpPost]
        [Route("TiposServicios/ObtenerTodos")]
        public async Task<IHttpActionResult> TipoServicioObtenerTodos(RequestTipoServicio entity)
        {
            ResultPage<TipoServicio> result = new ResultPage<TipoServicio>();
            try
            {
                business.TiposServicioBusiness business = new business.TiposServicioBusiness();
                result.List.AddRange(business.ObtenerTodos(entity.Paging));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("TiposServicios/ObtenerPorCriterio")]
        public async Task<IHttpActionResult> TipoServicioObtenerPorCriterio(RequestTipoServicio entity)
        {
            ResultPage<TipoServicio> result = new ResultPage<TipoServicio>();
            try
            {
                business.TiposServicioBusiness business = new business.TiposServicioBusiness();
                result.List.AddRange(business.ObtenerPorCriterio(entity.Paging, entity.Item));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("TiposServicios/ObtenerId/{id}")]
        public async Task<IHttpActionResult> TipoServicioObtenerId(RequestTipoServicio entity)
        {
            ResultPage<TipoServicio> result = new ResultPage<TipoServicio>();
            try
            {
                business.TiposServicioBusiness business = new business.TiposServicioBusiness();
                result.Entity = business.ObtenerPorId(entity.Item.Identificador);
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
        [Route("TiposServicios/Save")]
        public async Task<IHttpActionResult> TipoServicioGuardar(RequestTipoServicio entity)
        {
            ResultPage<TipoServicio> result = new ResultPage<TipoServicio>();
            try
            {
                business.TiposServicioBusiness business = new business.TiposServicioBusiness();
                result.Success = business.Guardar(entity.Item);
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
        [Route("TiposServicios/CambiarEstatus")]
        public async Task<IHttpActionResult> TipoServicioCambiarEstatus(RequestTipoServicio entity)
        {
            ResultPage<TipoServicio> result = new ResultPage<TipoServicio>();
            try
            {
                business.TiposServicioBusiness business = new business.TiposServicioBusiness();
                result.Success = business.CambiarEstatus(entity.Item);
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

        #endregion

        #region GRUPOS

        [HttpPost]
        [Route("Grupo/ObtenerTodos")]
        public IHttpActionResult GrupoObtenerTodos(RequestGrupo entity)
        {
            ResultPage<Grupo> result = new ResultPage<Grupo>();
            try
            {
                business.GruposBusiness business = new business.GruposBusiness();
                result.List.AddRange(business.ObtenerTodos(entity.Paging));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("Grupo/ObtenerPorCriterio")]
        public IHttpActionResult GrupoObtenerPorCriterio(RequestGrupo entity)
        {

            ResultPage<Grupo> result = new ResultPage<Grupo>();
            try
            {
                business.GruposBusiness business = new business.GruposBusiness();
                result.List.AddRange(business.ObtenerPorCriterio(entity.Paging, entity.Item));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("Grupo/ObtenerId/{id}")]
        public IHttpActionResult GrupoObtener(RequestGrupo entity)
        {
            ResultPage<Grupo> result = new ResultPage<Grupo>();
            try
            {
                business.GruposBusiness business = new business.GruposBusiness();
                result.Entity = business.ObtenerPorId(entity.Item.Identificador);
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
        [Route("Grupo/Save")]
        public IHttpActionResult GrupoGuadar(RequestGrupo entity)
        {
            ResultPage<Grupo> result = new ResultPage<Grupo>();
            try
            {
                business.GruposBusiness business = new business.GruposBusiness();
                result.Success = business.Guardar(entity.Item);                
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
        [Route("Grupo/CambiarEstatus")]
        public IHttpActionResult GrupoCambiarEstatus(RequestGrupo entity)
        {
            ResultPage<Grupo> result = new ResultPage<Grupo>();
            try
            {
                business.GruposBusiness business = new business.GruposBusiness();
                result.Success = business.CambiarEstatus(entity.Item);
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
        [Route("Grupos/ObtenerIdDivision/")]
        public IHttpActionResult GrupoObtenerPorIdDivision(RequestGrupo entity)
        {
            ResultPage<Grupo> result = new ResultPage<Grupo>();
            try
            {
                business.GruposBusiness business = new business.GruposBusiness();
                result.List.AddRange(business.ObtenerPorIdDivision(entity.Item.IdDivision));
                result.Page.Pages = business.Pages;
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

        #endregion

        #region CUOTAS

        [HttpPost]
        [Route("Cuotas/ObtenerTodos")]
        public async Task<IHttpActionResult> CuotaObtenerTodos(RequestCuota entity)
        {
            ResultPage<Cuota> result = new ResultPage<Cuota>();
            try
            {
                business.CuotasBusiness business = new business.CuotasBusiness();
                result.List.AddRange(business.ObtenerTodos(entity.Paging));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("Cuotas/ObtenerPorCriterio")]
        public async Task<IHttpActionResult> CuotaObtenerPorCriterio(RequestCuota entity)
        {
            ResultPage<Cuota> result = new ResultPage<Cuota>();
            try
            {
                business.CuotasBusiness business = new business.CuotasBusiness();
                result.List.AddRange(business.ObtenerPorCriterio(entity.Paging,entity.Item));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("Cuotas/ObtenerId/{id}")]
        public async Task<IHttpActionResult> CuotaObtenerPorId(RequestCuota entity)
        {
            ResultPage<Cuota> result = new ResultPage<Cuota>();
            try
            {
                business.CuotasBusiness business = new business.CuotasBusiness();
                result.Entity = business.ObtenerPorId(entity.Item.Identificador);
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
        [Route("Cuotas/Save")]
        public async Task<IHttpActionResult> CuotaGuardar(RequestCuota entity)
        {
            ResultPage<Cuota> result = new ResultPage<Cuota>();
            try
            {
                business.CuotasBusiness business = new business.CuotasBusiness();
                result.Success = business.Guardar(entity.Item);
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
        [Route("Cuotas/CambiarEstatus")]
        public async Task<IHttpActionResult> CuotaCambiarEstatus(RequestCuota entity)
        {
            ResultPage<Cuota> result = new ResultPage<Cuota>();
            try
            {
                business.CuotasBusiness business = new business.CuotasBusiness();
                result.Success = business.CambiarEstatus(entity.Item);
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

        #endregion

        #region TIPOS_DOCUMENTO

        [HttpPost]
        [Route("TipoDocumento/ObtenerTodos")]
        public async Task<IHttpActionResult> TipoDocumentoObtenerTodos(RequestTipoDocumento entity)
        {
            ResultPage<TipoDocumento> result = new ResultPage<TipoDocumento>();
            try
            {
                business.TiposDocumentoBusiness business = new business.TiposDocumentoBusiness();
                result.List.AddRange(business.ObtenerTodos(entity.Paging));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("TipoDocumento/ObtenerPorCriterio")]
        public async Task<IHttpActionResult> TipoDocumentoObtenerPorCriterio(RequestTipoDocumento entity)
        {
            ResultPage<TipoDocumento> result = new ResultPage<TipoDocumento>();
            try
            {
                business.TiposDocumentoBusiness business = new business.TiposDocumentoBusiness();
                result.List.AddRange(business.ObtenerPorCriterio(entity.Paging,entity.Item));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("TipoDocumento/ObtenerId/{id}")]
        public async Task<IHttpActionResult> TipoDocumentoObtenerPorId(RequestTipoDocumento entity)
        {
            ResultPage<Cuota> result = new ResultPage<Cuota>();
            try
            {
                business.CuotasBusiness business = new business.CuotasBusiness();
                result.Entity = business.ObtenerPorId(entity.Item.Identificador);
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
        [Route("TipoDocumento/Save")]
        public async Task<IHttpActionResult> TipoDocumentoGuardar(RequestTipoDocumento entity)
        {
            ResultPage<TipoDocumento> result = new ResultPage<TipoDocumento>();
            try
            {
                business.TiposDocumentoBusiness business = new business.TiposDocumentoBusiness();
                result.Success = business.Guardar(entity.Item);
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
        [Route("TipoDocumento/CambiarEstatus")]
        public async Task<IHttpActionResult> TipoDocumentoCambiarEstatus(RequestTipoDocumento entity)
        {
            ResultPage<TipoDocumento> result = new ResultPage<TipoDocumento>();
            try
            {
                business.TiposDocumentoBusiness business = new business.TiposDocumentoBusiness();
                result.Success = business.CambiarEstatus(entity.Item);
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

        #endregion

        #region FRACCIONES

        [HttpPost]
        [Route("Fracciones/ObtenerTodos")]
        public async Task<IHttpActionResult> FraccionObtenerTodos(RequestFraccion entity)
        {
            ResultPage<Fraccion> result = new ResultPage<Fraccion>();
            try
            {
                business.FraccionesBusiness business = new business.FraccionesBusiness();
                result.List.AddRange(business.ObtenerTodos(entity.Paging));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("Fracciones/ObtenerPorCriterio")]
        public async Task<IHttpActionResult> FraccionObtenerPorCriterio(RequestFraccion entity)
        {
            ResultPage<Fraccion> result = new ResultPage<Fraccion>();
            try
            {
                business.FraccionesBusiness business = new business.FraccionesBusiness();
                result.List.AddRange(business.ObtenerPorCriterio(entity.Paging, entity.Item));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("Fracciones/ObtenerId/{id}")]
        public async Task<IHttpActionResult> FraccionObtenerPorId(RequestFraccion entity)
        {
            ResultPage<Fraccion> result = new ResultPage<Fraccion>();
            try
            {
                business.FraccionesBusiness business = new business.FraccionesBusiness();
                result.Entity = business.ObtenerPorId(entity.Item.Identificador);
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("Fracciones/Save")]
        public async Task<IHttpActionResult> FraccionGuardar(RequestFraccion entity)
        {
            ResultPage<Fraccion> result = new ResultPage<Fraccion>();
            try
            {
                business.FraccionesBusiness business = new business.FraccionesBusiness();
                result.Success = business.Guardar(entity.Item);
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
        [Route("Fracciones/CambiarEstatus")]
        public async Task<IHttpActionResult> FraccionCambiarEstatus(RequestFraccion entity)
        {
            ResultPage<Fraccion> result = new ResultPage<Fraccion>();
            try
            {
                business.FraccionesBusiness business = new business.FraccionesBusiness();
                result.Success = business.CambiarEstatus(entity.Item);
            }
            catch (SqlException e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                result.Errors.Add(new Error { Code = e.ErrorCode, Message = ResourceExceptionMessage.MensajeErrorAplicacion });
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
                result.Errors.Add(new Error { Code = System.Runtime.InteropServices.Marshal.GetExceptionCode(), Message = ResourceExceptionMessage.MensajeErrorAplicacion });
            }

            return Ok(result);
        }

        #endregion FRACCIONES

        #region FACTOR_ENTIDAD_FEDERATIVA

        [HttpPost]
        [Route("FactoresEntidadFederativa/ObtenerTodos")]
        public async Task<IHttpActionResult> FactorEntidadFederativaObtenerTodos(RequestFactorEntidadFederativa entity)
        {
            ResultPage<FactorEntidadFederativaDTO> result = new ResultPage<FactorEntidadFederativaDTO>();
            try
            {
                business.FactoresEntidadFederativaBusiness business = new business.FactoresEntidadFederativaBusiness();
                result.List.AddRange(business.ObtenerTodos(entity.Paging));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("FactoresEntidadFederativa/ObtenerPorCriterio")]
        public async Task<IHttpActionResult> FactorEntidadFederativaObtenerPorCriterio(RequestFactorEntidadFederativa entity)
        {
            ResultPage<FactorEntidadFederativa> result = new ResultPage<FactorEntidadFederativa>();
            try
            {
                business.FactoresEntidadFederativaBusiness business = new business.FactoresEntidadFederativaBusiness();
                result.List.AddRange(business.ObtenerPorCriterio(entity.Paging, entity.Item));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("FactoresEntidadFederativa/ObtenerId/{id}")]
        public async Task<IHttpActionResult> FactorEntidadFederativaObtenerPorId(RequestFactorEntidadFederativa entity)
        {
            ResultPage<FactorEntidadFederativa> result = new ResultPage<FactorEntidadFederativa>();
            try
            {
                business.FactoresEntidadFederativaBusiness business = new business.FactoresEntidadFederativaBusiness();
                result.Entity = business.ObtenerPorId(entity.Item.Identificador);
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("FactoresEntidadFederativa/Save")]
        public async Task<IHttpActionResult> FactorEntidadFederativaGuadar(RequestFactorEntidadFederativa entity)
        {
            ResultPage<FactorEntidadFederativa> result = new ResultPage<FactorEntidadFederativa>();
            try
            {
                business.FactoresEntidadFederativaBusiness business = new business.FactoresEntidadFederativaBusiness();
                result.Success = business.Guardar(entity.DTO);                
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
        [Route("FactoresEntidadFederativa/CambiarEstatus")]
        public async Task<IHttpActionResult> FactorEntidadFederativaCambiarEstatus(RequestFactorEntidadFederativa entity)
        {
            ResultPage<FactorEntidadFederativa> result = new ResultPage<FactorEntidadFederativa>();
            try
            {
                business.FactoresEntidadFederativaBusiness business = new business.FactoresEntidadFederativaBusiness();
                result.Success = business.CambiarEstatus(entity.Item);
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
        [Route("FactorEntidadFederativa/Obtener/")]
        public async Task<IHttpActionResult> FactorEntidadFederativaObtener(RequestFactorMunicipio entity)
        {
            ResultPage<FactorEntidadFederativaDTO> result = new ResultPage<FactorEntidadFederativaDTO>();
            try
            {
                FactoresEntidadFederativaBusiness business = new FactoresEntidadFederativaBusiness();
                result.Entity = business.Obtener(entity.Paging);
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

        #endregion

        #region FACTORES_MUNICIPIO

        [HttpPost]
        [Route("FactoresMunicipio/ObtenerTodos")]
        public async Task<IHttpActionResult> FactorMunicipioObtenerTodos(RequestFactorMunicipio entity)
        {
            ResultPage<FactorMunicipio> result = new ResultPage<FactorMunicipio>();
            try
            {
                business.FactoresMunicipioBusiness business = new business.FactoresMunicipioBusiness();
                result.List.AddRange(business.ObtenerTodos(entity.Paging));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("FactoresMunicipio/ObtenerPorCriterio")]
        public async Task<IHttpActionResult> FactorMunicipioObtenerPorCriterio(RequestFactorMunicipio entity)
        {
            ResultPage<FactorMunicipio> result = new ResultPage<FactorMunicipio>();
            try
            {
                business.FactoresMunicipioBusiness business = new business.FactoresMunicipioBusiness();
                result.List.AddRange(business.ObtenerPorCriterio(entity.Paging, entity.Item));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("FactoresMunicipio/Obtener/")]
        public async Task<IHttpActionResult> FactorMunicipioObtener(RequestFactorMunicipio entity)
        {
            ResultPage<FactorMunicipioDTO> result = new ResultPage<FactorMunicipioDTO>();
            try
            {
                business.FactoresMunicipioBusiness business = new business.FactoresMunicipioBusiness();
                result.Entity = business.Obtener(entity.Paging);               
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
        [Route("FactoresMunicipio/Save")]
        public async Task<IHttpActionResult> FactorMunicipioGuardar(RequestFactorMunicipio entity)
        {
            ResultPage<FactorMunicipio> result = new ResultPage<FactorMunicipio>();
            try
            {
                business.FactoresMunicipioBusiness business = new business.FactoresMunicipioBusiness();
                result.Success = business.Guardar(entity.FactorMunicipioDTO);                
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
        [Route("FactoresMunicipio/CambiarEstatus")]
        public async Task<IHttpActionResult> FactorMunicipioCambiarEstatus(RequestFactorMunicipio entity)
        {
            ResultPage<FactorMunicipio> result = new ResultPage<FactorMunicipio>();
            try
            {
                business.FactoresMunicipioBusiness business = new business.FactoresMunicipioBusiness();
                result.Success = business.CambiarEstatus(entity.Item);
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

        #endregion

        #region FACTORES_LEY_INGRESO

        [HttpPost]
        [Route("FactoresLeyIngreso/ObtenerTodos")]
        public async Task<IHttpActionResult> FactorLeyIngresoObtenerTodos(RequestFactorLeyIngreso entity)
        {
            ResultPage<FactorLeyIngreso> result = new ResultPage<FactorLeyIngreso>();
            try
            {
                business.FactoresLeyIngresoBusiness business = new business.FactoresLeyIngresoBusiness();
                result.List.AddRange(business.ObtenerTodos(entity.Paging));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("FactoresLeyIngreso/ObtenerPorCriterio")]
        public async Task<IHttpActionResult> FactorLeyIngresoObtenerPorCriterio(RequestFactorLeyIngreso entity)
        {
            ResultPage<FactorLeyIngreso> result = new ResultPage<FactorLeyIngreso>();
            try
            {
                business.FactoresLeyIngresoBusiness business = new business.FactoresLeyIngresoBusiness();
                result.List.AddRange(business.ObtenerPorCriterio(entity.Paging, entity.Item));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("FactoresLeyIngreso/ObtenerId/{id}")]
        public async Task<IHttpActionResult> FactorLeyIngresoObtenerPorId(RequestFactorLeyIngreso entity)
        {
            ResultPage<FactorLeyIngreso> result = new ResultPage<FactorLeyIngreso>();
            try
            {
                business.FactoresLeyIngresoBusiness business = new business.FactoresLeyIngresoBusiness();
                result.Entity = business.ObtenerPorId(entity.Item.Identificador);
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
        [Route("FactoresLeyIngreso/Save")]
        public async Task<IHttpActionResult> FactorLeyIngresoSave(RequestFactorLeyIngreso entity)
        {
            ResultPage<FactorLeyIngreso> result = new ResultPage<FactorLeyIngreso>();
            try
            {
                business.FactoresLeyIngresoBusiness business = new business.FactoresLeyIngresoBusiness();
                result.Success = business.Guardar(entity.Item);
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
        [Route("FactoresLeyIngreso/CambiarEstatus")]
        public async Task<IHttpActionResult> FactorLeyIngresoCambiarEstatus(RequestFactorLeyIngreso entity)
        {
            ResultPage<FactorLeyIngreso> result = new ResultPage<FactorLeyIngreso>();
            try
            {
                business.FactoresLeyIngresoBusiness business = new business.FactoresLeyIngresoBusiness();
                result.Success = business.CambiarEstatus(entity.Item);
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


        #endregion

        #region FACTORES_ACTIVIDAD_ECONOMICA

        [HttpPost]
        [Route("FactoresActividadEconomica/ObtenerTodos")]
        public async Task<IHttpActionResult> FactorActividadEconomicaObtenerTodos(RequestFactorActividadEconomica entity)
        {
            ResultPage<FactorActividadEconomica> result = new ResultPage<FactorActividadEconomica>();
            try
            {
                business.FactoresActividadEconomicaBusiness business = new business.FactoresActividadEconomicaBusiness();
                result.List.AddRange(business.ObtenerTodos(entity.Paging));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("FactoresActividadEconomica/ObtenerPorCriterio")]
        public async Task<IHttpActionResult> FactorActividadEconomicaObtenerPorCriterio(RequestFactorActividadEconomica entity)
        {
            ResultPage<FactorActividadEconomica> result = new ResultPage<FactorActividadEconomica>();
            try
            {
                business.FactoresActividadEconomicaBusiness business = new business.FactoresActividadEconomicaBusiness();
                result.List.AddRange(business.ObtenerPorCriterio(entity.Paging,entity.Item));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("FactoresActividadEconomica/ObtenerId/{id}")]
        public async Task<IHttpActionResult> FactorActividadEconomicaObtenerPorId(RequestFactorActividadEconomica entity)
        {
            ResultPage<FactorActividadEconomica> result = new ResultPage<FactorActividadEconomica>();
            try
            {
                business.FactoresActividadEconomicaBusiness business = new business.FactoresActividadEconomicaBusiness();
                result.Entity = business.ObtenerPorId(entity.Item.Identificador);
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
        [Route("FactoresActividadEconomica/Save")]
        public async Task<IHttpActionResult> FactorActividadEconomicaGuardar(RequestFactorActividadEconomica entity)
        {
            ResultPage<FactorActividadEconomica> result = new ResultPage<FactorActividadEconomica>();
            try
            {
                business.FactoresActividadEconomicaBusiness business = new business.FactoresActividadEconomicaBusiness();
                result.Success = business.Guardar(entity.Item);
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
        [Route("FactoresActividadEconomica/CambiarEstatus")]
        public async Task<IHttpActionResult> FactorActividadEconomicaCambiarEstatus(RequestFactorActividadEconomica entity)
        {
            ResultPage<FactorActividadEconomica> result = new ResultPage<FactorActividadEconomica>();
            try
            {
                business.FactoresActividadEconomicaBusiness business = new business.FactoresActividadEconomicaBusiness();
                result.Success = business.CambiarEstatus(entity.Item);
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

        #endregion

        #region GASTOS_INHERENTES

        [HttpPost]
        [Route("GastosInherentes/ObtenerTodos")]
        public async Task<IHttpActionResult> GastoInherenteObtenerTodos(RequestGastoInherente entity)
        {
            ResultPage<GastoInherente> result = new ResultPage<GastoInherente>();
            try
            {
                business.GastosInherentesBusiness business = new business.GastosInherentesBusiness();
                result.List.AddRange(business.ObtenerTodos(entity.Paging));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("GastosInherentes/ObtenerPorCriterio")]
        public async Task<IHttpActionResult> GastoInherenteObtenerPorCriterio(RequestGastoInherente entity)
        {
            ResultPage<GastoInherente> result = new ResultPage<GastoInherente>();
            try
            {
                business.GastosInherentesBusiness business = new business.GastosInherentesBusiness();
                result.List.AddRange(business.ObtenerPorCriterio(entity.Paging, entity.Item));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("GastosInherentes/ObtenerId/{id}")]
        public async Task<IHttpActionResult> GastoInherenteObtenerPorId(RequestGastoInherente entity)
        {
            ResultPage<GastoInherente> result = new ResultPage<GastoInherente>();
            try
            {
                business.GastosInherentesBusiness business = new business.GastosInherentesBusiness();
                result.Entity = business.ObtenerPorId(entity.Item.Identificador);
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
        [Route("GastosInherentes/Save")]
        public async Task<IHttpActionResult> GastoInherenteGuardar(RequestGastoInherente entity)
        {
            ResultPage<GastoInherente> result = new ResultPage<GastoInherente>();
            try
            {
                business.GastosInherentesBusiness business = new business.GastosInherentesBusiness();
                result.Success = business.Guardar(entity.Item);
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
        [Route("GastosInherentes/CambiarEstatus")]
        public async Task<IHttpActionResult> GastoInherenteCambiarEstatuss(RequestGastoInherente entity)
        {
            ResultPage<GastoInherente> result = new ResultPage<GastoInherente>();
            try
            {
                business.GastosInherentesBusiness business = new business.GastosInherentesBusiness();
                result.Success = business.CambiarEstatus(entity.Item);
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

        #endregion

        #region FACTORES

        [HttpPost]
        [Route("Factores/ObtenerTodos")]
        public async Task<IHttpActionResult> FactorObtenerTodos(RequestFactor entity)
        {
            ResultPage<Factor> result = new ResultPage<Factor>();
            try
            {
                business.FactoresBusiness business = new business.FactoresBusiness();
                result.List.AddRange(business.ObtenerTodos(entity.Paging));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("Factores/ObtenerPorCriterio")]
        public async Task<IHttpActionResult> FactorObtenerPorCriterio(RequestFactor entity)
        {
            ResultPage<Factor> result = new ResultPage<Factor>();
            try
            {
                business.FactoresBusiness business = new business.FactoresBusiness();
                result.List.AddRange(business.ObtenerPorCriterio(entity.Paging, entity.Item));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("Factores/ObtenerId/{id}")]
        public async Task<IHttpActionResult> FactorObtenerPorId(RequestFactor entity)
        {
            ResultPage<Factor> result = new ResultPage<Factor>();
            try
            {
                business.FactoresBusiness business = new business.FactoresBusiness();
                result.Entity = business.ObtenerPorId(entity.Item.Identificador);
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
        [Route("Factores/Save")]
        public async Task<IHttpActionResult> FactorGuardar(RequestFactor entity)
        {
            ResultPage<Factor> result = new ResultPage<Factor>();
            try
            {
                business.FactoresBusiness business = new business.FactoresBusiness();
                result.Success = business.Guardar(entity.Item);
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
        [Route("Factores/CambiarEstatus")]
        public async Task<IHttpActionResult> FactorCambiarEstatus(RequestFactor entity)
        {
            ResultPage<Factor> result = new ResultPage<Factor>();
            try
            {
                business.FactoresBusiness business = new business.FactoresBusiness();
                result.Success = business.CambiarEstatus(entity.Item);
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
        [Route("Factores/FactorObtenerPorClasificacion")]
        public async Task<IHttpActionResult> FactorObtenerPorClasificacion(RequestFactor entity)
        {
            ResultPage<Factor> result = new ResultPage<Factor>();
            try
            {
                business.FactoresBusiness business = new business.FactoresBusiness();
                result.List.AddRange(business.ObtenerPorClasificacion(entity.Item));
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
        #endregion

        #region GRUPOS_TARIFARIOS

        [HttpPost]
        [Route("GruposTarifario/ObtenerTodos")]
        public async Task<IHttpActionResult> GrupoTarifarioObtenerTodos(RequestGrupoTarifario entity)
        {
            ResultPage<GrupoTarifario> result = new ResultPage<GrupoTarifario>();
            try
            {
                business.McsBusiness business = new business.McsBusiness();
                result.List.AddRange(business.ObtenerTarifario(entity.Paging));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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

        #endregion

        #region MEDIDAS_COBRO

        [HttpPost]
        [Route("MedidasCobro/ObtenerTodos")]
        public async Task<IHttpActionResult> MedidaCobroObtenerTodos(RequestMedidaCobro entity)
        {
            ResultPage<MedidaCobro> result = new ResultPage<MedidaCobro>();
            try
            {
                business.MedidasCobroBusiness business = new business.MedidasCobroBusiness();
                result.List.AddRange(business.ObtenerTodos(entity.Paging));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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

        #endregion

        #region DEPENDENCIAS

        [HttpPost]
        [Route("Dependencias/ObtenerTodos")]
        public async Task<IHttpActionResult> DependenciasObtenerTodos(RequestDependencia entity)
        {
            ResultPage<Dependencia> result = new ResultPage<Dependencia>();
            try
            {
                business.DependenciasBusiness business = new business.DependenciasBusiness();
                result.List.AddRange(business.ObtenerTodos(entity.Paging));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("Dependencias/ObtenerPorCriterio")]
        public async Task<IHttpActionResult> DependenciasObtenerPorCriterio(RequestDependencia entity)
        {
            ResultPage<Dependencia> result = new ResultPage<Dependencia>();
            try
            {
                business.DependenciasBusiness business = new business.DependenciasBusiness();
                result.List.AddRange(business.ObtenerPorCriterio(entity.Paging,entity.Item));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("Dependencias/ObtenerId/{id}")]
        public async Task<IHttpActionResult> DependenciasObtenerPorId(RequestDependencia entity)
        {
            ResultPage<Dependencia> result = new ResultPage<Dependencia>();
            try
            {
                business.DependenciasBusiness business = new business.DependenciasBusiness();
                result.Entity = business.ObtenerPorId(entity.Item.Identificador);
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("Dependencias/Save")]
        public async Task<IHttpActionResult> DependenciasGuardar(RequestDependencia entity)
        {
            ResultPage<Dependencia> result = new ResultPage<Dependencia>();
            try
            {
                business.DependenciasBusiness business = new business.DependenciasBusiness();
                result.Success = business.Guardar(entity.Item);
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
        [Route("Dependencias/CambiarEstatus")]
        public async Task<IHttpActionResult> DependenciasCambiarEstatus(RequestDependencia entity)
        {
            ResultPage<Dependencia> result = new ResultPage<Dependencia>();
            try
            {
                business.DependenciasBusiness business = new business.DependenciasBusiness();
                result.Success = business.CambiarEstatus(entity.Item);
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

        #endregion

        #region CLASIFICACION_FACTOR

        [HttpPost]
        [Route("ClasificacionFactor/ObtenerTodos")]
        public async Task<IHttpActionResult> ClasificacionFactorObtenerTodos(RequestClasificacionFactor entity)
        {
            ResultPage<ClasificacionFactor> result = new ResultPage<ClasificacionFactor>();
            try
            {
                business.ClasificacionFactorBusiness business = new business.ClasificacionFactorBusiness();
                result.List.AddRange(business.ObtenerTodos(entity.Paging));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("ClasificacionFactor/ObtenerPorCriterio")]
        public async Task<IHttpActionResult> ClasificacionFactorObtenerPorCriterio(RequestClasificacionFactor entity)
        {
            ResultPage<ClasificacionFactor> result = new ResultPage<ClasificacionFactor>();
            try
            {
                business.ClasificacionFactorBusiness business = new business.ClasificacionFactorBusiness();
                result.List.AddRange(business.ObtenerPorCriterio(entity.Paging, entity.Item));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("ClasificacionFactor/ObtenerId/{id}")]
        public async Task<IHttpActionResult> ClasificacionFactorObtenerPorId(RequestClasificacionFactor entity)
        {
            ResultPage<ClasificacionFactor> result = new ResultPage<ClasificacionFactor>();
            try
            {
                business.ClasificacionFactorBusiness business = new business.ClasificacionFactorBusiness();
                result.Entity = business.ObtenerPorId(entity.Item.Identificador);
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
        [Route("ClasificacionFactor/Save")]
        public async Task<IHttpActionResult> ClasificacionFactorGuardar(RequestClasificacionFactor entity)
        {
            ResultPage<ClasificacionFactor> result = new ResultPage<ClasificacionFactor>();
            try
            {
                business.ClasificacionFactorBusiness business = new business.ClasificacionFactorBusiness();
                result.Success = business.Guardar(entity.Item);
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
        [Route("ClasificacionFactor/CambiarEstatus")]
        public async Task<IHttpActionResult> ClasificacionFactorCambiarEstatus(RequestClasificacionFactor entity)
        {
            ResultPage<ClasificacionFactor> result = new ResultPage<ClasificacionFactor>();
            try
            {
                business.ClasificacionFactorBusiness business = new business.ClasificacionFactorBusiness();
                result.Success = business.CambiarEstatus(entity.Item);
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

        #endregion

        #region ESTADOS
        [HttpPost]
        [Route("Estados/ObtenerTodos")]
        public IHttpActionResult EstadoObtener()
        {
            ResultPage<Estado> result = new ResultPage<Estado>();
            try
            {
                business.McsBusiness business = new business.McsBusiness();
                result.List.AddRange(business.ObtenerEstados());
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
        #endregion

        #region AREAS
        [HttpPost]
        [Route("Areas/ObtenerTodos")]
        public IHttpActionResult AreasObtener()
        {
            ResultPage<Area> result = new ResultPage<Area>();
            try
            {
                business.RepBusiness business = new business.RepBusiness();
                result.List.AddRange(business.ObtenerAreas());
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
        #endregion

        #region ANIOS

        [HttpPost]
        [Route("Anios/ObtenerTodos")]
        public async Task<IHttpActionResult> AniosObtener(RequestAnio entity)
        {
            ResultPage<Anio> result = new ResultPage<Anio>();
            try
            {
                business.AniosBusiness business = new business.AniosBusiness();
                result.List.AddRange(business.ObtenerTodos(entity.Paging));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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

        #endregion

        #region MESES

        [HttpPost]
        [Route("Meses/ObtenerTodos")]
        public async Task<IHttpActionResult> MesesObtenerTodos(RequestMes entity)
        {
            ResultPage<Meses> result = new ResultPage<Meses>();
            try
            {
                business.MesesBusiness business = new business.MesesBusiness();
                result.List.AddRange(business.ObtenerTodos(entity.Paging));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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

        #endregion

        #region REGIMENFISCAL
        [HttpPost]
        [Route("RegimenFiscal/ObtenerTodos")]
        public IHttpActionResult RegimenFiscalesObtener(RequestRegimenFiscal entity)
        {
            ResultPage<RegimenFiscal> result = new ResultPage<RegimenFiscal>();
            try
            {
                business.RegimenFiscalesBusiness business = new business.RegimenFiscalesBusiness();
                result.List.AddRange(business.ObtenerTodos(entity.Paging));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("RegimenFiscal/ObtenerPorCriterio")]
        public async Task<IHttpActionResult> RegimenFiscalObtenerPorCriterio(RequestRegimenFiscal entity)
        {
            ResultPage<RegimenFiscal> result = new ResultPage<RegimenFiscal>();
            try
            {
                business.RegimenFiscalesBusiness business = new business.RegimenFiscalesBusiness();
                result.List.AddRange(business.ObtenerPorCriterio(entity.Paging, entity.Item));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        #endregion

        #region TIPO PAGO
        [HttpPost]
        [Route("TiposPago/ObtenerTodos")]
        public IHttpActionResult TiposPagoObtener(RequestTiposPago entity)
        {
            ResultPage<TiposPago> result = new ResultPage<TiposPago>();
            try
            {
                business.TiposPagoBusiness business = new business.TiposPagoBusiness();
                result.List.AddRange(business.ObtenerTodos(entity.Paging));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("TiposPago/ObtenerPorCriterio")]
        public async Task<IHttpActionResult> TiposPagoObtenerPorCriterio(RequestTiposPago entity)
        {
            ResultPage<TiposPago> result = new ResultPage<TiposPago>();
            try
            {
                business.TiposPagoBusiness business = new business.TiposPagoBusiness();
                result.List.AddRange(business.ObtenerPorCriterio(entity.Paging, entity.Item));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        #endregion

        #region ACTIVIDADES
        [HttpPost]
        [Route("Actividades/ObtenerTodos")]
        public IHttpActionResult ActividadesObtener(RequestActividad entity)
        {
            ResultPage<Actividad> result = new ResultPage<Actividad>();
            try
            {
                business.ActividadesBusiness business = new business.ActividadesBusiness();
                result.List.AddRange(business.ObtenerTodos(entity.Paging));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("TiposPago/ObtenerPorCriterio")]
        public async Task<IHttpActionResult> TiposPagoObtenerPorCriterio(RequestActividad entity)
        {
            ResultPage<Actividad> result = new ResultPage<Actividad>();
            try
            {
                business.ActividadesBusiness business = new business.ActividadesBusiness();
                result.List.AddRange(business.ObtenerPorCriterio(entity.Paging, entity.Item));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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

        #endregion

        #region REFERENCIAS

        [HttpPost]
        [Route("Referencias/ObtenerTodos")]
        public async Task<IHttpActionResult> ReferenciaObtenerTodos(RequestReferencia entity)
        {
            ResultPage<Referencia> result = new ResultPage<Referencia>();
            try
            {
                business.ReferenciasBusiness business = new business.ReferenciasBusiness();
                result.List.AddRange(business.ObtenerTodos(entity.Paging));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("Referencias/ObtenerPorCriterio")]
        public async Task<IHttpActionResult> ReferenciaObtenerPorCriterio(RequestReferencia entity)
        {
            ResultPage<Referencia> result = new ResultPage<Referencia>();
            try
            {
                business.ReferenciasBusiness business = new business.ReferenciasBusiness();
                result.List.AddRange(business.ObtenerPorCriterio(entity.Paging, entity.Item));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("Referencias/ObtenerId/{id}")]
        public async Task<IHttpActionResult> ReferenciaObtenerPorId(RequestReferencia entity)
        {
            ResultPage<Referencia> result = new ResultPage<Referencia>();
            try
            {
                business.ReferenciasBusiness business = new business.ReferenciasBusiness();
                result.Entity = business.ObtenerPorId(entity.Item.Identificador);
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
        [Route("Referencias/Save")]
        public async Task<IHttpActionResult> ReferenciaGuardar(RequestReferencia entity)
        {
            ResultPage<Referencia> result = new ResultPage<Referencia>();
            try
            {
                business.ReferenciasBusiness business = new business.ReferenciasBusiness();
                result.Success = business.Guardar(entity.Item);
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

            return Ok(result); ;
        }

        [HttpPost]
        [Route("Referencias/CambiarEstatus")]
        public async Task<IHttpActionResult> ReferenciaCambiarEstatus(RequestReferencia entity)
        {
            ResultPage<Referencia> result = new ResultPage<Referencia>();
            try
            {
                business.ReferenciasBusiness business = new business.ReferenciasBusiness();
                result.Success = business.CambiarEstatus(entity.Item);
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

        #endregion

        #region JERARQUIAS

        [HttpPost]
        [Route("Jerarquias/ObtenerTodos")]
        public async Task<IHttpActionResult> JerarquiaObtenerTodos(RequestJerarquia entity)
        {
            ResultPage<Jerarquia> result = new ResultPage<Jerarquia>();
            try
            {
                business.RepBusiness business = new business.RepBusiness();
                result.List.AddRange(business.ObtenerJerarquias(entity.Paging));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("Jerarquias/ObtenerId/{id}")]
        public async Task<IHttpActionResult> JerarquiaObtenerPorId(RequestJerarquia entity)
        {
            ResultPage<Referencia> result = new ResultPage<Referencia>();
            try
            {
                business.ReferenciasBusiness business = new business.ReferenciasBusiness();
                result.Entity = business.ObtenerPorId(entity.Item.Identificador);
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

        #endregion

        [HttpPost]
        [Route("Municipios/ObtenerTodos")]
        public IHttpActionResult MunicipiosObtener(RequestEstado request)
        {
            ResultPage<Municipio> result = new ResultPage<Municipio>();
            try
            {
                business.McsBusiness business = new business.McsBusiness();
                result.List.AddRange(business.ObtenerMunipios(request.Item));
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

        #region CONFIGURACION_SERVICIO

        [HttpPost]
        [Route("ConfigServ/ObtenerTodoPaginado")]
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
        [Route("ConfigServ/ObtenerPorId/")]
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
        [Route("ConfigServ/ObtenerPorIdTipoServicioIdCentroCosto/{idTipoServicio}/{idCentroCosto}")]
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
        [Route("ConfigServ/InsertarConfiguraciones")]
        public IHttpActionResult ConfiguracionServicioInsertarConfiguraciones(RequestConfiguracionServicio entity)
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

        #endregion CONFIGURACION_SERVICIO

        #region INTEGRANTES

        [HttpPost]
        [Route("Integrantes/ObtenerTodos")]
        public async Task<IHttpActionResult> IntegrantesObtenerTodos(RequestIntegrante entity)
        {
            ResultPage<Integrante> result = new ResultPage<Integrante>();
            try
            {
                business.RepBusiness business = new business.RepBusiness();
                result.List.AddRange(business.ObtenerIntegrantes(entity.Paging));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("Integrantes/ObtenerPorCriterio")]

        public async Task<IHttpActionResult> IntegrantesObtenerPorCriterio(RequestIntegrante entity)
        {
            ResultPage<Integrante> result = new ResultPage<Integrante>();
            try
            {
                business.RepBusiness business = new business.RepBusiness();
                result.List.AddRange(business.ObtenerIntegrantes(entity.Paging));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        #endregion

        #region FASES
        [HttpPost]
        [Route("Fases/ObtenerTodos")]
        public IHttpActionResult FasesObtener(RequestFase entity)
        {
            ResultPage<Fase> result = new ResultPage<Fase>();
            try
            {
                business.FasesBusiness business = new business.FasesBusiness();
                result.List.AddRange(business.ObtenerTodos(entity.Paging));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        [Route("Fases/ObtenerPorCriterio")]
        public async Task<IHttpActionResult> TiposPagoObtenerPorCriterio(RequestFase entity)
        {
            ResultPage<Fase> result = new ResultPage<Fase>();
            try
            {
                business.FasesBusiness business = new business.FasesBusiness();
                result.List.AddRange(business.ObtenerPorCriterio(entity.Paging, entity.Item));
                result.Page.CurrentPage = entity.Paging.CurrentPage;
                result.Page.Pages = business.Pages;
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
        #endregion


    }
}
