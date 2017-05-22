namespace Suncorp.BusinessLogic.Direcciones
{
    using Helpers;
    using Models;
    using Entities.Direcciones;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;

    public class LogicDirecciones
    {
        private readonly Logger _logLogger = new Logger(Constantes.UrlLog, Constantes.ConnectionStringTrabajo);

        public LogicDirecciones()
        {

        }

        #region DirCatEstados

        #region CRUD

        /// <summary>
        /// Obtiene una lista de la tabla de DirDirecciones
        /// </summary>
        /// <returns>Retorna una lista con los estados de la republica</returns>
        public List<DirCatEstados> ObtenerTablaDirCatEstado()
        {
            try
            {
                return new EntDirecciones().ObtenerTablaDirCatEstado().Result;
            }
            catch (Exception e)
            {
                Task.Factory.StartNew(
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
        public DirCatEstados InsertarDirCatEstado(DirCatEstados model)
        {
            try
            {
                return new EntDirecciones().InsertarDirCatEstado(model).Result;
            }
            catch (Exception e)
            {
                Task.Factory.StartNew(
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
        public bool ActualizarDirCatEstado(DirCatEstados model)
        {
            try
            {
                return new EntDirecciones().ActualizarDirCatEstado(model).Result;
            }
            catch (Exception e)
            {
                Task.Factory.StartNew(
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
        public bool EliminarDirCatEstado(DirCatEstados model)
        {
            try
            {
                return new EntDirecciones().EliminarDirCatEstado(model).Result;
            }
            catch (Exception e)
            {
                Task.Factory.StartNew(
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
        /// Obtiene una lista de la tabla de DirCatMunicipios
        /// </summary>
        /// <returns>Retorna una lista con los estados de la republica</returns>
        public List<DirCatMunicipios> ObtenerTablaDirCatMunicipios()
        {
            try
            {
                return new EntDirecciones().ObtenerTablaDirCatMunicipios().Result;
            }
            catch (Exception e)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.Error,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al obtener registro DirCatMunicipios", e.Message, e, ""));
                throw;
            }
        }

        /// <summary>
        /// Metodo encargado de insertar un registro en DirCatEstados
        /// </summary>
        /// <param name="model">Se requiere un modelo de DirCatEstados para insertar el registro (Es un catalogo por lo que se requiere un IdEstado)</param>
        /// <returns>Retorna un modelo de DirCatEstados Actualizado</returns>
        public DirCatMunicipios InsertarDirCatMunicipios(DirCatMunicipios model)
        {
            try
            {
                return new EntDirecciones().InsertarDirCatMunicipios(model).Result;
            }
            catch (Exception e)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.Error,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al insertar registro DirCatMunicipios", e.Message, e, ""));
                throw;
            }
        }

        /// <summary>
        /// Metodo encargado de generar una actualizacion a DirCatEstados
        /// </summary>
        /// <param name="model">Un modelo de DirCatEstado Actualizado</param>
        /// <returns>Retorna un booleano si se actualizo el registro</returns>
        public bool ActualizarDirCatMunicipios(DirCatMunicipios model)
        {
            try
            {
                return new EntDirecciones().ActualizarDirCatMunicipios(model).Result;
            }
            catch (Exception e)
            {
                Task.Factory.StartNew(
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
        public bool EliminarDirCatMunicipios(DirCatMunicipios model)
        {
            try
            {
                return new EntDirecciones().EliminarDirCatMunicipios(model).Result;
            }
            catch (Exception e)
            {
                Task.Factory.StartNew(
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

        #region DirCatMunicipios

        #region CRUD

        /// <summary>
        /// Obtiene una lista de la tabla de DirCatColonias
        /// </summary>
        /// <returns>Retorna una lista con los estados de la republica</returns>
        public List<DirCatColonias> ObtenerTablaDirCatColonias()
        {
            try
            {
                return new EntDirecciones().ObtenerTablaDirCatColonias().Result;
            }
            catch (Exception e)
            {
                Task.Factory.StartNew(
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
        /// <param name="model">Se requiere un modelo de DirCatEstados para insertar el registro (Es un catalogo por lo que se requiere un Id)</param>
        /// <returns>Retorna un modelo de DirCatEstados Actualizado</returns>
        public DirCatColonias InsertarDirCatColonias(DirCatColonias model)
        {
            try
            {
                return new EntDirecciones().InsertarDirCatColonias(model).Result;
            }
            catch (Exception e)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.Error,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al insertar registro DirCatColonias", e.Message, e, ""));
                throw;
            }
        }

        /// <summary>
        /// Metodo encargado de generar una actualizacion a DirCatEstados
        /// </summary>
        /// <param name="model">Un modelo de DirCatEstado Actualizado</param>
        /// <returns>Retorna un booleano si se actualizo el registro</returns>
        public bool ActualizarDirCatColonias(DirCatColonias model)
        {
            try
            {
                return new EntDirecciones().ActualizarDirCatColonias(model).Result;
            }
            catch (Exception e)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.Error,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al Actualizar registro DirCatMunicipios", e.Message, e, ""));
                throw;
            }
        }

        /// <summary>
        /// Metodo encargado de eliminar una un registro de DirCatColonias
        /// </summary>
        /// <param name="model">Se requiere el modelo de DirCatEstados a eliminar</param>
        /// <returns>retorna un Booleano si se pudo eliminar el registro</returns>
        public bool EliminarDirCatColonias(DirCatColonias model)
        {
            try
            {
                return new EntDirecciones().EliminarDirCatColonias(model).Result;
            }
            catch (Exception e)
            {
                Task.Factory.StartNew(
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
        public List<DirDirecciones> ObtenerTablaDirDirecciones()
        {
            try
            {
                return new EntDirecciones().ObtenerTablaDirDirecciones().Result;
            }
            catch (Exception e)
            {
                Task.Factory.StartNew(
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
        /// <param name="model">Se requiere un modelo de DirCatEstados para insertar el registro (Es un catalogo por lo que se requiere un Id)</param>
        /// <returns>Retorna un modelo de DirDirecciones Actualizado</returns>
        public DirDirecciones InsertarDirDirecciones(DirDirecciones model)
        {
            try
            {
                return new EntDirecciones().InsertarDirDirecciones(model).Result;
            }
            catch (Exception e)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.Error,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al insertar registro DirDirecciones", e.Message, e, ""));
                throw;
            }
        }

        /// <summary>
        /// Metodo encargado de generar una actualizacion a DirCatEstados
        /// </summary>
        /// <param name="model">Un modelo de DirDirecciones Actualizado</param>
        /// <returns>Retorna un booleano si se actualizo el registro</returns>
        public bool ActualizarDirDirecciones(DirDirecciones model)
        {
            try
            {
                return new EntDirecciones().ActualizarDirDirecciones(model).Result;
            }
            catch (Exception e)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.Error,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al Actualizar registro DirDirecciones", e.Message, e, ""));
                throw;
            }
        }

        /// <summary>
        /// Metodo encargado de eliminar una un registro de DirCatColonias
        /// </summary>
        /// <param name="model">Se requiere el modelo de DirCatEstados a eliminar</param>
        /// <returns>retorna un Booleano si se pudo eliminar el registro</returns>
        public bool EliminarDirDirecciones(DirDirecciones model)
        {
            try
            {
                return new EntDirecciones().EliminarDirDirecciones(model).Result;
            }
            catch (Exception e)
            {
                Task.Factory.StartNew(
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
