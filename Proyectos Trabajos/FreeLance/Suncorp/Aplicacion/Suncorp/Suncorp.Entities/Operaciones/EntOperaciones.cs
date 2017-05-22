namespace Suncorp.Entities.Operaciones
{
    using Models;
    using Helpers;
    using DataAccess;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System;
    using System.Reflection;
    
    /// <summary>
    /// Clase encargada del manejo de entidades del modulo de operaciones
    /// </summary>
    public class EntOperaciones
    {
        private readonly Logger _logLogger = new Logger(Constantes.UrlLog, Constantes.ConnectionStringTrabajo);

        public EntOperaciones()
        {

        }

        #region OpeCatZonas

        #region CRUD

        /// <summary>
        /// Obtiene una lista de la tabla de OpeCatZonas
        /// </summary>
        /// <returns>Retorna una lista el modelo de OpeCatZonas</returns>
        public async Task<List<OpeCatZonas>> ObtenerTablaOpeCatZonas()
        {
            try
            {
                using (var aux = new Repositorio<OpeCatZonas>())
                {
                    return await aux.ObtenerTabla();
                }
            }
            catch (Exception e)
            {
                await Task.Factory.StartNew(
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
        public async Task<OpeCatZonas> InsertarOpeCatZonas(OpeCatZonas model)
        {
            try
            {
                using (var aux = new Repositorio<OpeCatZonas>())
                {
                    return await aux.Insertar(model);
                }
            }
            catch (Exception e)
            {
                await Task.Factory.StartNew(
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
        public async Task<bool> ActualizarOpeCatZonas(OpeCatZonas model)
        {
            try
            {
                using (var aux = new Repositorio<OpeCatZonas>())
                {
                    return await aux.Actualizar(model);
                }
            }
            catch (Exception e)
            {
                await Task.Factory.StartNew(
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
        /// <param name="model">Se requiere el modelo de OpeCatZonas a eliminar</param>
        /// <returns>retorna un Booleano si se pudo eliminar el registro</returns>
        public async Task<bool> EliminarOpeCatZonas(OpeCatZonas model)
        {
            try
            {
                using (var aux = new Repositorio<OpeCatZonas>())
                {
                    return await aux.Eliminar(model);
                }
            }
            catch (Exception e)
            {
                await Task.Factory.StartNew(
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
        /// <returns>Retorna una lista el modelo de OpeZonasPorEstados</returns>
        public async Task<List<OpeZonasPorEstados>> ObtenerTablaOpeZonasPorEstados()
        {
            try
            {
                using (var aux = new Repositorio<OpeZonasPorEstados>())
                {
                    return await aux.ObtenerTabla();
                }
            }
            catch (Exception e)
            {
                await Task.Factory.StartNew(
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
        public async Task<OpeZonasPorEstados> InsertarOpeZonasPorEstados(OpeZonasPorEstados model)
        {
            try
            {
                using (var aux = new Repositorio<OpeZonasPorEstados>())
                {
                    return await aux.Insertar(model);
                }
            }
            catch (Exception e)
            {
                await Task.Factory.StartNew(
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
        public async Task<bool> ActualizarOpeZonasPorEstados(OpeZonasPorEstados model)
        {
            try
            {
                using (var aux = new Repositorio<OpeZonasPorEstados>())
                {
                    return await aux.Actualizar(model);
                }
            }
            catch (Exception e)
            {
                await Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.Error,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al Actualizar registro OpeZonasPorEstados", e.Message, e, ""));
                throw;
            }
        }

        /// <summary>
        /// Metodo encargado de eliminar una un registro de OpeZonasPorEstados
        /// </summary>
        /// <param name="model">Se requiere el modelo de OpeZonasPorEstados a eliminar</param>
        /// <returns>retorna un Booleano si se pudo eliminar el registro</returns>
        public async Task<bool> EliminarOpeZonasPorEstados(OpeZonasPorEstados model)
        {
            try
            {
                using (var aux = new Repositorio<OpeZonasPorEstados>())
                {
                    return await aux.Eliminar(model);
                }
            }
            catch (Exception e)
            {
                await Task.Factory.StartNew(
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
