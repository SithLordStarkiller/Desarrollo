using System.Linq;
using GOB.SPF.ConecII.Entities.Amatzin;
using DocumentFormat.OpenXml.Office.CustomUI;

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

        [HttpPost, Route("Cliente/ObtenerSolicitudPorIdCliente")]
        public async Task<IHttpActionResult> ObtenerSolicitudPorIdCliente(RequestCliente entity)
        {
            var result = new ResultPage<Cliente>();
            try
            {
                var business = new ClienteBusiness();
                result.Entity = business.ObtenerPorIdClienteSolicitud(entity.Item.Identificador);
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
        
        [HttpPost, Route("Cliente/ObtenerListaSolicitudPorIdCliente")]
        public async Task<IHttpActionResult> ObtenerListaSolicitudPorIdCliente(Solicitud entity)
        {
            var result = new ResultPage<Solicitud>();
            try
            {
                var business = new SolicitudBusisness();
                result.List = business.ObtenerPorIdCliente(entity);
                result.Success = result.List.Any();
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

        #region TIPO_INSTALACIONES

        [HttpPost, Route("TipoInstalacion/ObtenerTodos")]
        public IHttpActionResult TipoInstalacionObtener(RequestTipoInstalacion request)
        {
            var result = new ResultPage<TipoInstalacion>();
            try
            {
                var business = new TipoInstalacionBusiness();
                result.List.AddRange(business.ObtenerTodos(request.Paging));
                result.Success = result.List.Any();
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
        /// Obtiene las todas instalaciones registradas paginado.
        /// </summary>
        /// <param name="entity">Paginado</param>
        /// <returns>Listado de instalaciones.</returns>
        [HttpPost, Route("Instalacion/ObtenerTodos")]
        public IHttpActionResult InstalacionObtener(RequestInstalacion request)
        {
            var result = new ResultPage<Cliente>();
            try
            {
                var business = new InstalacionBusiness();
                result.List.AddRange(business.ObtenerTodos(request.Paging));
                result.Success = result.List.Any();
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

        /// <summary>
        /// Obtiene las instalaciones registradas de acuerdo a los parámetros proporcionados.
        /// </summary>
        /// <param name="entity">Clase Instalacion (Filtros.)</param>
        /// <returns>Listado de instalaciones</returns>
        [HttpPost, Route("Instalacion/ObtenerPorCriterio")]
        public IHttpActionResult InstalacionObtenerPorCriterio(RequestCliente entity)
        {
            var result = new ResultPage<Cliente>();
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

        /// <summary>
        /// Obtiene la instalación de acuerdo al identificador proporcionado
        /// </summary>
        /// <param name="entity">Instalación a buscar</param>
        /// <returns>Instalacion requerida.</returns>
        [HttpPost, Route("Instalacion/ObtenerPorId")]
        public async Task<IHttpActionResult> InstalacionObtenerPorId(RequestInstalacion entity)
        {
            var result = new ResultPage<Instalacion>();
            try
            {
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

        [HttpPost, Route("ServiciosInstalacion/ObtenerPorIdCliente")]
        public async Task<IHttpActionResult> ObtenerInstalacionesPorIdCliente(RequestSolicitud entity)
        {
            var result = new ResultPage<ServicioInstalacion>();
            try
            {
                var business = new ServicioInstalacionBusiness();
                result.List = business.ObtenerInstalacionesPorIdCliente(entity.Item, entity.Item.Servicio.Identificador == 0 ? null : (int?)entity.Item.Servicio.Identificador).ToList();
                result.Success = result.List != null;
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


        [HttpPost, Route("Instalacion/Save")]
        public IHttpActionResult InstalacionGuardar(RequestCliente entity)
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

        [HttpGet, Route("Instalacion/ObtenerPorNombre")]
        public IHttpActionResult InstalacionObtenerPorNombre(string nombre)
        {
            var result = new ResultPage<string>();
            try
            {
                var business = new InstalacionBusiness();
                result.List.AddRange(business.ObtenerPorNombre(nombre));
                result.Success = result.List.Any();
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
        public async Task<IHttpActionResult> SolicitudesObtenerIdCliente(Solicitud entity)
        {
            ResultPage<Solicitud> result = new ResultPage<Solicitud>();
            try
            {
                business.SolicitudBusisness business = new business.SolicitudBusisness();
                result.List.AddRange(business.ObtenerPorIdCliente(entity));
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

        #region Solicitud

        [HttpPost]
        [Route("Solicitud/Guardar")]
        public async Task<IHttpActionResult> SolicitudGuardar(Solicitud solicitud)
        {
            var result = new ResultPage<Solicitud>();
            try
            {
                var solicitudBusisness = new SolicitudBusisness();
                result.Success = await solicitudBusisness.Guardar(solicitud);
                if (result.Success)
                {
                    result.Entity = solicitud;
                }
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

        [HttpPost, Route("Solicitud/ObtenerSolicitudServiciosAcuerdosPorIdSolicitud")]
        public async Task<IHttpActionResult> ObtenerSolicitudServiciosAcuerdosPorIdSolicitud(Solicitud solicitud)
        {
            var result = new ResultPage<Solicitud>();
            try
            {
                //Parameter(solicitud);
                result.Entity = new SolicitudBusisness().ObtenerSolicitudServiciosAcuerdosPorIdSolicitud(solicitud);
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
        [Route("Solicitud/ActualizarSolicitudServicioAcuerdo")]
        public IHttpActionResult ActualizarSolicitudServicioAcuerdo(Solicitud solicitud)
        {
            var result = new ResultPage<Solicitud>();
            try
            {
                result.Success = new SolicitudBusisness().ActualizarSolicitudAcuerdos(solicitud);
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
        [Route("Solicitud/ObtenerPorCriterio")]
        public async Task<IHttpActionResult> SolicitudObtenerPorCriterio(RequestSolicitud entity)
        {
            ResultPage<Solicitud> result = new ResultPage<Solicitud>();
            try
            {
                var business = new SolicitudBusisness();
                result.List.AddRange(business.ObtenerPorCriterio(entity.Paging, entity.Item, entity.DTO.FechaRegistroMin, entity.DTO.FechaRegistroMax));
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

        [AllowAnonymous]
        [HttpPost]
        [Route("Solicitud/ObtenerConDetalleServiciosClientePorIdSolicitud")]
        public async Task<IHttpActionResult> SolicitudObtenerConDetallePorId(RequestSolicitud entity)
        {
            ResultPage<Solicitud> result = new ResultPage<Solicitud>();
            try
            {
                var business = new SolicitudBusisness();
                result.Entity = business.ObtenerSolicitudConDetalle(entity.Item);
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
        [Route("Solicitud/ObtenerServicios")]
        public async Task<IHttpActionResult> SolicitudesServiciosObtener(RequestSolicitud solicitud)
        {
            var result = new ResultPage<Solicitud>();
            try
            {
                var solicitudBusisness = new SolicitudBusisness();
                var list = solicitudBusisness.ObtenerSolicitudesServicios(solicitud.Paging).ToList();
                result.List = list;
                result.Success = list.Any();
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
        [Route("Solicitud/ObtenerServiciosPorCriterio")]
        public async Task<IHttpActionResult> SolicitudesServiciosObtenerPorCriterio(RequestSolicitud solicitud)
        {
            var result = new ResultPage<Solicitud>();
            try
            {
                var solicitudBusisness = new SolicitudBusisness();
                var list = solicitudBusisness.ObtenerSolicitudesServiciosPorCriterio(solicitud.Paging, solicitud.Item).ToList();
                result.List = list;
                result.Success = list.Any();
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

        #endregion

        #region Documentos
        [HttpGet]
        [Route("Documentos/ObtenerPorId")]
        public IHttpActionResult EstadoObtener(long archivoId)
        {
            var result = new ResultPage<Documento>();
            try
            {
                var business = new AmatzinBusiness();
                result.Entity = business.Consultar(archivoId);
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
