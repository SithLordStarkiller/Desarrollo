namespace GOB.SPF.Conec.Services.Controllers
{
    using ConecII.Entities.Request;
    using ConecII.Business;
    using ConecII.Entities;
    using System;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using System.Web.Http;
    using business = ConecII.Business;

    [RoutePrefix("api/Solicitud")]
    public class SolicitudController : ApiController
    {
        #region Clientes

        [HttpPost, Route("Cliente/ObtenerTodos")]
        public async Task<IHttpActionResult> ClienteObtenerTodos(RequestCliente entity)
        {
            var result = new ResultPage<Cliente>();
            try
            {
                Parameter(entity);
                var business = new ClienteBusiness();
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

        [HttpPost, Route("Cliente/ObtenerPorId")]
        public async Task<IHttpActionResult> ClienteObtenerPorId(RequestCliente entity)
        {
            var result = new ResultPage<Cliente>();
            try
            {
                Parameter(entity);
                var business = new ClienteBusiness();
                result.Entity = business.ObtenerPorId(entity.Item.Identificador);
                result.Success = result.Entity.Identificador.Equals(entity.Item.Identificador);
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

        [HttpPost, Route("Cliente/ObtenerTodosPorRazonSocial")]
        public async Task<IHttpActionResult> ObtenerTodosPorRazonSocial(RequestCliente entity)
        {
            var result = new ResultPage<Cliente>();
            try
            {
                Parameter(entity);
                var business = new ClienteBusiness();
                result.List.AddRange(business.ObtenerPorRazonSocial(entity.Item.RazonSocial));
                result.Page.CurrentPage = 0;
                result.Page.Pages = 0;
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

        [HttpPost, Route("Cliente/ObtenerTodosPorNombreCorto")]
        public async Task<IHttpActionResult> ObtenerTodosPorNombreCorto(RequestCliente entity)
        {
            var result = new ResultPage<Cliente>();
            try
            {
                Parameter(entity);
                var business = new ClienteBusiness();
                result.List.AddRange(business.ObtenerPorNombreCorto(entity.Item.NombreCorto));
                result.Page.CurrentPage = 0;
                result.Page.Pages = 0;
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

        [HttpPost, Route("Cliente/ObtenerPorCriterio")]
        public async Task<IHttpActionResult> ClienteObtenerPorCriterio(RequestCliente entity)
        {
            var result = new ResultPage<Cliente>();
            try
            {
                Parameter(entity);
                var business = new ClienteBusiness();
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

        [HttpPost, Route("Cliente/Guardar")]
        public IHttpActionResult ClienteGuardar(RequestCliente entity)
        {
            var result = new ResultPage<Cliente>();
            try
            {
                Parameter(entity);
                var business = new ClienteBusiness();
                result.Success = business.Guardar(entity.Item) > 0;
            }
            catch (SqlException e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Errors[0].Message}\n{e.StackTrace}", EventLogEntryType.Error);
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
        [HttpPost, Route("Cliente/ObtenerDomicilioFiscal")]
        public async Task<IHttpActionResult> ClienteObtenerDomicilioFiscal(RequestCliente entity)
        {
            Parameter(entity);
            var result = new ResultPage<DomicilioFiscal>();
            try
            {
                var business = new DomicilioFiscalBusiness();
                result.Entity = business.ObtenerDomicilioFiscal(entity.Item);
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

        [HttpPost, Route("Cliente/CambiarEstatus")]
        public async Task<IHttpActionResult> ClienteCambiarEstatus(RequestCliente entity)
        {
            Parameter(entity);

            var result = new ResultPage<Cliente>();
            try
            {
                var business = new ClienteBusiness();
                result.Success = business.CambiarEstatus(entity.Item);
            }
            catch (SqlException e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
            }
            catch (ConecException ex)
            {
                result.Errors.Add(new Error { Code = 1000, Message = ex.Message });
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
            }

            return Ok(result);
        }

        #endregion

        #region Instalaciones 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost, Route("Instalacion/ObtenerTodos")]
        public IHttpActionResult InstalacionnObtener(Paging paging)
        {
            var result = new ResultPage<Instalacion>();
            try
            {
                var business = new InstalacionBusiness();
                result.List.AddRange(business.ObtenerTodos(paging));
                result.Page.CurrentPage = paging.CurrentPage;
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

        [HttpPost, Route("Instalacion/ObtenerPorCriterio")]
        public IHttpActionResult InstalacionObtenerPorCriterio(RequestInstalacion entity)
        {
            var result = new ResultPage<Instalacion>();
            try
            {
                var business = new InstalacionBusiness();
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

        [HttpPost, Route("Instalacion/ObtenerPorId")]
        public async Task<IHttpActionResult> InstalacionObtenerPorId(RequestInstalacion entity)
        {
            var result = new ResultPage<Instalacion>();
            try
            {
                Parameter(entity);
                var business = new InstalacionBusiness();
                result.Entity = business.ObtenerPorId(entity.Item.Identificador);
                result.Success = result.Entity != null;
            }
            catch (SqlException e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
            }
            catch (ConecException e)
            {
                result.Errors.Add(new Error { Code = 1000, Message = e.Message });
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
            }

            return Ok(result);
        }


        [HttpPost, Route("Instalacion/CambiarEstatus")]
        public async Task<IHttpActionResult> InstalacionCambiarEstatus(RequestInstalacion entity)
        {
            var result = new ResultPage<Instalacion>();
            try
            {
                var business = new InstalacionBusiness();
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


        [HttpPost, Route("Instalacion/Save")]
        public IHttpActionResult InstalacionGuardar(RequestInstalacion entity)
        {
            var result = new ResultPage<Instalacion>();
            try
            {
                var business = new InstalacionBusiness();
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

        private static void Parameter(RequestBase entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
        }
        #endregion

        #region SOLICITUDES

        [HttpPost]
        [Route("Solicitudes/Obtener")]
        public async Task<IHttpActionResult> SolicitudesObtener(RequestSolicitudes entity)
        {
            ResultPage<Solicitudes> result = new ResultPage<Solicitudes>();
            try
            {
                business.SolicitudesBusiness business = new business.SolicitudesBusiness();
                result.List.AddRange(business.Obtener(entity.Paging));
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
        [Route("Solicitudes/ObtenerTodos")]
        public async Task<IHttpActionResult> SolicitudesObtenerTodos(RequestSolicitudes entity)
        {
            ResultPage<Solicitudes> result = new ResultPage<Solicitudes>();
            try
            {
                business.SolicitudesBusiness business = new business.SolicitudesBusiness();
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
        [Route("Solicitudes/ObtenerPorCriterio")]
        public async Task<IHttpActionResult> SolicitudesObtenerPorCriterio(RequestSolicitudes entity)
        {
            ResultPage<Solicitudes> result = new ResultPage<Solicitudes>();
            try
            {
                business.SolicitudesBusiness business = new business.SolicitudesBusiness();
                result.List.AddRange(business.ObtenerPorCriterio(/*entity.Paging, */entity.Item));
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
        [Route("Solicitudes/ObtenerId")]
        public async Task<IHttpActionResult> SolicitudesObtenerId(RequestSolicitudes entity)
        {
            ResultPage<Solicitudes> result = new ResultPage<Solicitudes>();
            try
            {
                business.SolicitudesBusiness business = new business.SolicitudesBusiness();
                result.List.AddRange(business.ObtenerPorId(entity.Item));
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
        [Route("Solicitudes/Save")]
        public async Task<IHttpActionResult> SolicitudesGuardar(RequestSolicitudes entity)
        {
            ResultPage<Solicitudes> result = new ResultPage<Solicitudes>();
            try
            {
                business.SolicitudesBusiness business = new business.SolicitudesBusiness();
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
        [Route("Solicitudes/CambiarEstatus")]
        public async Task<IHttpActionResult> SolicitudesCambiarEstatus(RequestSolicitudes entity)
        {
            ResultPage<Solicitudes> result = new ResultPage<Solicitudes>();
            try
            {
                business.SolicitudesBusiness business = new business.SolicitudesBusiness();
                result.Success = business.CambiarEstatus(entity.Item);
            }
            //catch (SqlException e)
            //{
            //    EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
            //}
            //catch (Exception e)
            //{
            //    EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
            //}
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

        #endregion
    }
}
