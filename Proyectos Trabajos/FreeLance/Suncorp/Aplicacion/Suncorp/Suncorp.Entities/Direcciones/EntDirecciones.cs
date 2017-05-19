namespace Suncorp.Entities.Direcciones
{
    using Suncorp.DataAccess;
    using Suncorp.Helpers;
    using Suncorp.Models;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;

    /// <summary>
    /// Clase encargada de la manipulacion de las direcciones
    /// </summary>
    public class EntDirecciones
    {
        private readonly SuncorpEntities _contexto;
        private readonly Logger _logLogger = new Logger(Constantes.UrlLog, Constantes.ConnectionStringTrabajo);

        public EntDirecciones()
        {

        }

        #region Operaciones CRUD DirCatEstados

        /// <summary>
        /// Obtiene una lista de la tabla de DirDirecciones
        /// </summary>
        /// <returns>Retorna una lista con los estados de la republica</returns>
        public async Task<List<DirCatEstados>> ObtenerTablaDirCatEstado()
        {
            try
            {
                using (var aux = new Repositorio<DirCatEstados>())
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
                           MethodBase.GetCurrentMethod().Name, "Error al obtener registro DirCatEstado", e.Message, e, ""));
                throw;
            }
        }

        /// <summary>
        /// Metodo encargado de insertar un registro en DirCatEstados
        /// </summary>
        /// <param name="model">Se requiere un modelo de DirCatEstados para insertar el registro (Es un catalogo por lo que se requiere un IdEstado)</param>
        /// <returns>Retorna un modelo de DirCatEstados Actualizado</returns>
        public async Task<DirCatEstados> InsertarDirCatEstado(DirCatEstados model)
        {
            try
            {
                using (var aux = new Repositorio<DirCatEstados>())
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
                           MethodBase.GetCurrentMethod().Name, "Error al insertar registro DirCatEstado", e.Message, e, ""));
                throw;
            }
        }

        /// <summary>
        /// Metodo encargado de generar una actualizacion a DirCatEstados
        /// </summary>
        /// <param name="model">Un modelo de DirCatEstado Actualizado</param>
        /// <returns>Retorna un booleano si se actualizo el registro</returns>
        public async Task<bool> ActualizarDirCatEstado(DirCatEstados model)
        {
            try
            {
                using (var aux = new Repositorio<DirCatEstados>())
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
                           MethodBase.GetCurrentMethod().Name, "Error al Actualizar registro DirCatEstado", e.Message, e, ""));
                throw;
            }
        }

        /// <summary>
        /// Metodo encargado de eliminar una un registro de DirCatEstados
        /// </summary>
        /// <param name="model">Se requiere el modelo de DirCatEstados a eliminar</param>
        /// <returns>retorna un Booleano si se pudo eliminar el registro</returns>
        public async Task<bool> EliminarDirCatEstado(DirCatEstados model)
        {
            try
            {
                using (var aux = new Repositorio<DirCatEstados>())
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
                           MethodBase.GetCurrentMethod().Name, "Error al Actualizar registro DirCatEstado", e.Message, e, ""));
                throw;
            }
        }
        #endregion

        #region Operaciones DirCatEstados
        #endregion
    }
}
