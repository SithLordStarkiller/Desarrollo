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
        //private readonly SuncorpEntities Contexto;
        private readonly Logger _logLogger = new Logger(Constantes.UrlLog, Constantes.ConnectionStringTrabajo);

        public EntDirecciones()
        {

        }

        #region DirCatEstados

        #region CRUD

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

        #region Operaciones
        #endregion

        #endregion

        #region DirCatMunicipios

        #region CRUD

        /// <summary>
        /// Obtiene una lista de la tabla de DirDirecciones
        /// </summary>
        /// <returns>Retorna una lista con los estados de la republica</returns>
        public async Task<List<DirCatMunicipios>> ObtenerTablaDirCatMunicipios()
        {
            try
            {
                using (var aux = new Repositorio<DirCatMunicipios>())
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
                           MethodBase.GetCurrentMethod().Name, "Error al obtener registro DirCatMunicipios", e.Message, e, ""));
                throw;
            }
        }

        /// <summary>
        /// Metodo encargado de insertar un registro en DirCatMunicipios
        /// </summary>
        /// <param name="model">Se requiere un modelo de DirCatMunicipios para insertar el registro (Es un catalogo por lo que se requiere un IdMunicipio)</param>
        /// <returns>Retorna un modelo de DirCatMunicipios Actualizado</returns>
        public async Task<DirCatMunicipios> InsertarDirCatMunicipios(DirCatMunicipios model)
        {
            try
            {
                using (var aux = new Repositorio<DirCatMunicipios>())
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
        /// Metodo encargado de generar una actualizacion a DirCatMunicipios
        /// </summary>
        /// <param name="model">Un modelo de DirCatEstado Actualizado</param>
        /// <returns>Retorna un booleano si se actualizo el registro</returns>
        public async Task<bool> ActualizarDirCatMunicipios(DirCatMunicipios model)
        {
            try
            {
                using (var aux = new Repositorio<DirCatMunicipios>())
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
                           MethodBase.GetCurrentMethod().Name, "Error al Actualizar registro DirCatMunicipios", e.Message, e, ""));
                throw;
            }
        }

        /// <summary>
        /// Metodo encargado de eliminar una un registro de DirCatEstados
        /// </summary>
        /// <param name="model">Se requiere el modelo de DirCatEstados a eliminar</param>
        /// <returns>retorna un Booleano si se pudo eliminar el registro</returns>
        public async Task<bool> EliminarDirCatMunicipios(DirCatMunicipios model)
        {
            try
            {
                using (var aux = new Repositorio<DirCatMunicipios>())
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
                           MethodBase.GetCurrentMethod().Name, "Error al Actualizar registro DirCatMunicipios", e.Message, e, ""));
                throw;
            }
        }
        #endregion

        #region Operaciones
        #endregion

        #endregion

        #region DirCatColonias

        #region CRUD

        /// <summary>
        /// Obtiene una lista de la tabla de DirCatColonias
        /// </summary>
        /// <returns>Retorna una lista con los estados de la republica</returns>
        public async Task<List<DirCatColonias>> ObtenerTablaDirCatColonias()
        {
            try
            {
                using (var aux = new Repositorio<DirCatColonias>())
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
                           MethodBase.GetCurrentMethod().Name, "Error al obtener registro DirCatColonias", e.Message, e, ""));
                throw;
            }
        }

        /// <summary>
        /// Metodo encargado de insertar un registro en DirCatColonias
        /// </summary>
        /// <param name="model">Se requiere un modelo de DirCatMunicipios para insertar el registro (Es un catalogo por lo que se requiere un Id)</param>
        /// <returns>Retorna un modelo de DirCatMunicipios Actualizado</returns>
        public async Task<DirCatColonias> InsertarDirCatColonias(DirCatColonias model)
        {
            try
            {
                using (var aux = new Repositorio<DirCatColonias>())
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
                           MethodBase.GetCurrentMethod().Name, "Error al insertar registro DirCatColonias", e.Message, e, ""));
                throw;
            }
        }

        /// <summary>
        /// Metodo encargado de generar una actualizacion a DirCatColonias
        /// </summary>
        /// <param name="model">Un modelo de DirCatEstado Actualizado</param>
        /// <returns>Retorna un booleano si se actualizo el registro</returns>
        public async Task<bool> ActualizarDirCatColonias(DirCatColonias model)
        {
            try
            {
                using (var aux = new Repositorio<DirCatColonias>())
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
                           MethodBase.GetCurrentMethod().Name, "Error al Actualizar registro DirCatColonias", e.Message, e, ""));
                throw;
            }
        }

        /// <summary>
        /// Metodo encargado de eliminar una un registro de DirCatColonias
        /// </summary>
        /// <param name="model">Se requiere el modelo de DirCatEstados a eliminar</param>
        /// <returns>retorna un Booleano si se pudo eliminar el registro</returns>
        public async Task<bool> EliminarDirCatColonias(DirCatColonias model)
        {
            try
            {
                using (var aux = new Repositorio<DirCatColonias>())
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
                           MethodBase.GetCurrentMethod().Name, "Error al Actualizar registro DirCatColonias", e.Message, e, ""));
                throw;
            }
        }
        #endregion

        #region Operaciones
        #endregion

        #endregion

        #region DirDirecciones

        #region CRUD

        /// <summary>
        /// Obtiene una lista de la tabla de DirDirecciones
        /// </summary>
        /// <returns>Retorna una lista con los estados de la republica</returns>
        public async Task<List<DirDirecciones>> ObtenerTablaDirDirecciones()
        {
            try
            {
                using (var aux = new Repositorio<DirDirecciones>())
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
                           MethodBase.GetCurrentMethod().Name, "Error al obtener registro DirDirecciones", e.Message, e, ""));
                throw;
            }
        }

        /// <summary>
        /// Metodo encargado de insertar un registro en DirDirecciones
        /// </summary>
        /// <param name="model">Se requiere un modelo de DirCatMunicipios para insertar el registro (Es un catalogo por lo que se requiere un Id)</param>
        /// <returns>Retorna un modelo de DirDirecciones Actualizado</returns>
        public async Task<DirDirecciones> InsertarDirDirecciones(DirDirecciones model)
        {
            try
            {
                using (var aux = new Repositorio<DirDirecciones>())
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
                           MethodBase.GetCurrentMethod().Name, "Error al insertar registro DirDirecciones", e.Message, e, ""));
                throw;
            }
        }

        /// <summary>
        /// Metodo encargado de generar una actualizacion a DirDirecciones
        /// </summary>
        /// <param name="model">Un modelo de DirCatEstado Actualizado</param>
        /// <returns>Retorna un booleano si se actualizo el registro</returns>
        public async Task<bool> ActualizarDirDirecciones(DirDirecciones model)
        {
            try
            {
                using (var aux = new Repositorio<DirDirecciones>())
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
                           MethodBase.GetCurrentMethod().Name, "Error al Actualizar registro DirCatColonias", e.Message, e, ""));
                throw;
            }
        }

        /// <summary>
        /// Metodo encargado de eliminar una un registro de DirDirecciones
        /// </summary>
        /// <param name="model">Se requiere el modelo de DirDirecciones a eliminar</param>
        /// <returns>retorna un Booleano si se pudo eliminar el registro</returns>
        public async Task<bool> EliminarDirDirecciones(DirDirecciones model)
        {
            try
            {
                using (var aux = new Repositorio<DirDirecciones>())
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
                           MethodBase.GetCurrentMethod().Name, "Error al Actualizar registro DirDirecciones", e.Message, e, ""));
                throw;
            }
        }
        #endregion

        #region Operaciones
        #endregion

        #endregion
    }
}
