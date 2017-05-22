namespace Suncorp.Services.ServiciosWcf
{
    using Helpers;
    using Models;
    using BusinessLogic.Operaciones;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;

    /// <summary>
    /// Clase encargada de implementar los servicio
    /// </summary>
    public class Operaciones : IOperaciones
    {

        private readonly Logger _logLogger = new Logger(Constantes.UrlLog, Constantes.ConnectionStringTrabajo);

        #region OpeCatZonas

        #region CRUD

        /// <summary>
        /// Obtiene una lista de la tabla de OpeCatZonas
        /// </summary>
        /// <returns>Retorna una lista con los estados de la republica</returns>
        public List<OpeCatZonas> ObtenerTablaOpeCatZonas()
        {
            try
            {
                return new LogicOperaciones().ObtenerTablaOpeCatZonas();
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
        /// <param name="model">Se requiere un modelo de DirDirecciones para insertar el registro</param>
        /// <returns>Retorna un modelo de OpeCatZonas Actualizado</returns>
        public OpeCatZonas InsertarOpeCatZonas(OpeCatZonas model)
        {
            try
            {
                return new LogicOperaciones().InsertarOpeCatZonas(model);
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
        /// <param name="model">Un modelo de DirCatColonias Actualizado</param>
        /// <returns>Retorna un booleano si se actualizo el registro</returns>
        public bool ActualizarOpeCatZonas(OpeCatZonas model)
        {
            try
            {
                return new LogicOperaciones().ActualizarOpeCatZonas(model);
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
        /// Metodo encargado de eliminar una un registro de DirDirecciones
        /// </summary>
        /// <param name="model">Se requiere el modelo de DirCatEstados a eliminar</param>
        /// <returns>retorna un Booleano si se pudo eliminar el registro</returns>
        public bool EliminarOpeCatZonas(OpeCatZonas model)
        {
            try
            {
                return new LogicOperaciones().EliminarOpeCatZonas(model);
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
        /// <returns>Retorna una lista con el modelo OpeZonasPorEstados</returns>
        public List<OpeZonasPorEstados> ObtenerTablaOpeZonasPorEstados()
        {
            try
            {
                return new LogicOperaciones().ObtenerTablaOpeZonasPorEstados();
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
        /// <returns>Retorna un modelo de OpeZonasPorEstados Actualizado</returns>
        public OpeZonasPorEstados InsertarOpeZonasPorEstados(OpeZonasPorEstados model)
        {
            try
            {
                return new LogicOperaciones().InsertarOpeZonasPorEstados(model);
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
        /// Metodo encargado de generar una actualizacion a OpeCatZonas
        /// </summary>
        /// <param name="model">Un modelo de DirCatColonias Actualizado</param>
        /// <returns>Retorna un booleano si se actualizo el registro</returns>
        public bool ActualizarOpeZonasPorEstados(OpeZonasPorEstados model)
        {
            try
            {
                return new LogicOperaciones().ActualizarOpeZonasPorEstados(model);
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
        /// Metodo encargado de eliminar una un registro de DirDirecciones
        /// </summary>
        /// <param name="model">Se requiere el modelo de DirCatEstados a eliminar</param>
        /// <returns>retorna un Booleano si se pudo eliminar el registro</returns>
        public bool EliminarOpeZonasPorEstados(OpeZonasPorEstados model)
        {
            try
            {
                return new LogicOperaciones().EliminarOpeZonasPorEstados(model);
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
