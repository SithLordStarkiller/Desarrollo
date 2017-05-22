namespace Suncorp.BusinessLogic.Operaciones
{
    using Entities.Operaciones;
    using Helpers;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;

    /// <summary>
    /// Clase encargada del manejo de la logca de negocios del modulo de operaciones
    /// </summary>
    public class LogicOperaciones
    {
        private readonly Logger _logLogger = new Logger(Constantes.UrlLog, Constantes.ConnectionStringTrabajo);

        public LogicOperaciones()
        {

        }

        #region OpeCatZonas

        #region CRUD

        /// <summary>
        /// Obtiene una lista de la tabla de OpeCatZonas
        /// </summary>
        /// <returns>Retorna una lista con OpeCatZonas</returns>
        public List<OpeCatZonas> ObtenerTablaOpeCatZonas()
        {
            try
            {
                return new EntOperaciones().ObtenerTablaOpeCatZonas().Result;
            }
            catch (Exception e)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.Error,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al obtener registro OpeCatZonas", e.Message, e, ""));
                throw;
            }
        }

        /// <summary>
        /// Metodo encargado de insertar un registro en OpeCatZonas
        /// </summary>
        /// <param name="model">Se requiere un modelo de OpeCatZonas para insertar el registro</param>
        /// <returns>Retorna un modelo de OpeCatZonas Actualizado</returns>
        public OpeCatZonas InsertarOpeCatZonas(OpeCatZonas model)
        {
            try
            {
                return new EntOperaciones().InsertarOpeCatZonas(model).Result;
            }
            catch (Exception e)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.Error,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al insertar registro OpeCatZonas", e.Message, e, ""));
                throw;
            }
        }

        /// <summary>
        /// Metodo encargado de generar una actualizacion a OpeCatZonas
        /// </summary>
        /// <param name="model">Un modelo de OpeCatZonas Actualizado</param>
        /// <returns>Retorna un booleano si se actualizo el registro</returns>
        public bool ActualizarOpeCatZonas(OpeCatZonas model)
        {
            try
            {
                return new EntOperaciones().ActualizarOpeCatZonas(model).Result;
            }
            catch (Exception e)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.Error,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al Actualizar registro OpeCatZonas", e.Message, e, ""));
                throw;
            }
        }

        /// <summary>
        /// Metodo encargado de eliminar una un registro de OpeCatZonas
        /// </summary>
        /// <param name="model">Se requiere el modelo de DirCatEstados a eliminar</param>
        /// <returns>retorna un Booleano si se pudo eliminar el registro</returns>
        public bool EliminarOpeCatZonas(OpeCatZonas model)
        {
            try
            {
                return new EntOperaciones().EliminarOpeCatZonas(model).Result;
            }
            catch (Exception e)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.Error,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al Actualizar registro OpeCatZonas", e.Message, e, ""));
                throw;
            }
        }
        #endregion

        #region Operaciones
        #endregion

        #endregion

        #region OpeZonasPorEstados

        #region CRUD

        /// <summary>
        /// Obtiene una lista de la tabla de OpeZonasPorEstados
        /// </summary>
        /// <returns>Retorna una lista con OpeZonasPorEstados</returns>
        public List<OpeZonasPorEstados> ObtenerTablaOpeZonasPorEstados()
        {
            try
            {
                return new EntOperaciones().ObtenerTablaOpeZonasPorEstados().Result;
            }
            catch (Exception e)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.Error,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al obtener registro OpeZonasPorEstados", e.Message, e, ""));
                throw;
            }
        }

        /// <summary>
        /// Metodo encargado de insertar un registro en OpeZonasPorEstados
        /// </summary>
        /// <param name="model">Se requiere un modelo de OpeZonasPorEstados para insertar el registro</param>
        /// <returns>Retorna un modelo de OpeCatZonas Actualizado</returns>
        public OpeZonasPorEstados InsertarOpeZonasPorEstados(OpeZonasPorEstados model)
        {
            try
            {
                return new EntOperaciones().InsertarOpeZonasPorEstados(model).Result;
            }
            catch (Exception e)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.Error,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al insertar registro OpeZonasPorEstados", e.Message, e, ""));
                throw;
            }
        }

        /// <summary>
        /// Metodo encargado de generar una actualizacion a OpeZonasPorEstados
        /// </summary>
        /// <param name="model">Un modelo de OpeZonasPorEstados Actualizado</param>
        /// <returns>Retorna un booleano si se actualizo el registro</returns>
        public bool ActualizarOpeZonasPorEstados(OpeZonasPorEstados model)
        {
            try
            {
                return new EntOperaciones().ActualizarOpeZonasPorEstados(model).Result;
            }
            catch (Exception e)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.Error,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al Actualizar registro OpeZonasPorEstados", e.Message, e, ""));
                throw;
            }
        }

        /// <summary>
        /// Metodo encargado de eliminar una un registro de OpeCatZonas
        /// </summary>
        /// <param name="model">Se requiere el modelo de DirCatEstados a eliminar</param>
        /// <returns>retorna un Booleano si se pudo eliminar el registro</returns>
        public bool EliminarOpeZonasPorEstados(OpeZonasPorEstados model)
        {
            try
            {
                return new EntOperaciones().EliminarOpeZonasPorEstados(model).Result;
            }
            catch (Exception e)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.Error,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al Actualizar registro OpeZonasPorEstados", e.Message, e, ""));
                throw;
            }
        }
        #endregion

        #region Operaciones
        #endregion

        #endregion
    }
}
