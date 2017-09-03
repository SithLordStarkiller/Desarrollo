namespace GOB.SPF.Conec.Services.Controllers
{
    using System;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Web.Http;
    using System.Linq;

    using ConecII.Entities;
    using ConecII.Business;
    using ConecII.Entities.Request;

    /// <summary>
    /// Controlador encargado de almacenar la peticiones de los procesos del modulo de configuracion
    /// </summary>
    [RoutePrefix("api/Configuration")]
    public class ConfiguracionController : ApiController
    {
        #region Notificaciones y alertas

        /// <summary>
        /// Metodo encargado del almacenar las configuraciones de una notificacion
        /// </summary>
        /// <param name="entity">Modelo de una tabla de </param>
        /// <returns>retorna un result page con la informacion del servicio</returns>
        [HttpPost]
        [Route("Notificaciones/Guardar")]
        public IHttpActionResult NotificacionesGuardar(RequestNotificaciones entity)
        {
            var result = new ResultPage<Notificaciones>();
            try
            {
                var business = new NotificacionesBusiness();
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

        /// <summary>
        /// Metodo encargado de obtener la configuraciones de una notificacion
        /// </summary>
        /// <param name="entity">Modelo de una tabla de </param>
        /// <returns>retorna un result page con la informacion del servicio</returns>
        [HttpPost]
        [Route("Notificaciones/ObtenerPorId")]
        public IHttpActionResult NotificacionesObtenerPorId(RequestNotificaciones entity)
        {
            var result = new ResultPage<Notificaciones>();
            try
            {
                var notificacion = new NotificacionesBusiness().ObtenerPorId(Convert.ToInt64(entity.Item.IdNotificacion));
                result.Entity = notificacion;
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

        /// <summary>
        /// Metodo encargado de obtener una lista de configuraciones de notificaciones
        /// </summary>
        /// <param name="entity">Modelo de una tabla de </param>
        /// <returns>retorna un result page con la informacion del servicio</returns>
        [HttpPost]
        [Route("Notificaciones/ObtenerTodos")]
        public IHttpActionResult NotificacionesObtenerTodos(RequestNotificaciones entity)
        {
            var result = new ResultPage<Notificaciones>();
            try
            {
                var notificacion = new NotificacionesBusiness().ObtenerTodos(entity.Paging);
                result.List = notificacion.ToList();
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
