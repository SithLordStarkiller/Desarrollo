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

        #region Notificaciones

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

        #region ReceptorAlerta

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ReceptoresAlerta/GuardarLista")]
        public IHttpActionResult ReceptoresAlertaGuardarLista(RequestReceptoresAlerta entity)
        {
            var result = new ResultPage<ReceptorAlerta>();
            try
            {
                var business = new ReceptoresAlertaBusiness();
                result.Success = business.GuardarLista(entity.Receptores, entity.Notificacion);
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
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ReceptoresAlerta/ObtenerTodos")]
        public IHttpActionResult ReceptoresAlertaObtenerTodos(RequestReceptoresAlerta entity)
        {
            var result = new ResultPage<ReceptorAlerta>();
            try
            {
                var lista = new ReceptoresAlertaBusiness().ListaReceptorAlertaObtenerTodos(entity.Notificacion);

                result.List = lista;
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

        #endregion

        #region AreasValidadoras

        /// <summary>
        /// Metodo encargado del almacenar las configuraciones de una notificacion
        /// </summary>
        /// <param name="entity">Modelo de una tabla de </param>
        /// <returns>retorna un result page con la informacion del servicio</returns>
        [HttpPost]
        [Route("AreasValidadoras/Guardar")]
        public IHttpActionResult AreasValidadorasGuadar(RequestAreasValidadoras entity)
        {
            var result = new ResultPage<AreasValidadoras>();
            try
            {
                result.Success = new AreasValidadorasBusiness().InsertarTabla(entity.Lista);
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
        [Route("AreasValidadoras/ObtenerTodos")]
        public IHttpActionResult AreasValidadorasObtenerTodos(RequestAreasValidadoras entity)
        {
            var result = new ResultPage<AreasValidadoras>();
            try
            {
                var lista = new AreasValidadorasBusiness().Obtener(entity.Paging);
                result.List = lista.ToList();
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
        [Route("AreasValidadoras/ActualizarTablas")]
        public IHttpActionResult AreasValidadorasActualizar(RequestAreasValidadoras entity)
        {
            var result = new ResultPage<AreasValidadoras>();
            try
            {
                new AreasValidadorasBusiness().ActualizarTabla(entity.Lista);
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
        [Route("AreasValidadoras/ModificarEstatus")]
        public IHttpActionResult AreasValidadorasModificarEstatus(RequestAreasValidadoras entity)
        {
            var result = new ResultPage<AreasValidadoras>();
            try
            {
                new AreasValidadorasBusiness().CambiarEstatus(entity.Item);
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
