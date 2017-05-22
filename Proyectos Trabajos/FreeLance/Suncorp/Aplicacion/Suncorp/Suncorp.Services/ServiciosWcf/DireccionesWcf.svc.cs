namespace Suncorp.Services.ServiciosWcf
{
    using Helpers;
    using Models;
    using BusinessLogic.Direcciones;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;


    public class DireccionesWcf : IDireccionesWcf
    {
        private readonly Logger _logLogger = new Logger(Constantes.UrlLog, Constantes.ConnectionStringTrabajo);

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
                return new LogicDirecciones().ObtenerTablaDirCatEstado();
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
                return new LogicDirecciones().InsertarDirCatEstado(model);
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
                return new LogicDirecciones().ActualizarDirCatEstado(model);
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
                return new LogicDirecciones().EliminarDirCatEstado(model);
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
        /// Obtiene una lista de la tabla de DirCatEstados
        /// </summary>
        /// <returns>Retorna una lista con los estados de la republica</returns>
        public List<DirCatMunicipios> ObtenerTablaDirCatMunicipios()
        {
            try
            {
                return new LogicDirecciones().ObtenerTablaDirCatMunicipios();
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
        /// Metodo encargado de insertar un registro en DirCatMunicipios
        /// </summary>
        /// <param name="model">Se requiere un modelo de DirCatMunicipios para insertar el registro (Es un catalogo por lo que se requiere un IdEstado)</param>
        /// <returns>Retorna un modelo de DirCatEstados Actualizado</returns>
        public DirCatMunicipios InsertarDirCatMunicipios(DirCatMunicipios model)
        {
            try
            {
                return new LogicDirecciones().InsertarDirCatMunicipios(model);
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
        /// Metodo encargado de generar una actualizacion a DirCatMunicipios
        /// </summary>
        /// <param name="model">Un modelo de DirCatMunicipios Actualizado</param>
        /// <returns>Retorna un booleano si se actualizo el registro</returns>
        public bool ActualizarDirCatMunicipios(DirCatMunicipios model)
        {
            try
            {
                return new LogicDirecciones().ActualizarDirCatMunicipios(model);
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
        /// Metodo encargado de eliminar una un registro de DirCatMunicipios
        /// </summary>
        /// <param name="model">Se requiere el modelo de DirCatEstados a eliminar</param>
        /// <returns>retorna un Booleano si se pudo eliminar el registro</returns>
        public bool EliminarDirCatMunicipios(DirCatMunicipios model)
        {
            try
            {
                return new LogicDirecciones().EliminarDirCatMunicipios(model);
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

        #region DirCatColonias

        #region CRUD

        /// <summary>
        /// Obtiene una lista de la tabla de DirCatEstados
        /// </summary>
        /// <returns>Retorna una lista con los estados de la republica</returns>
        public List<DirCatColonias> ObtenerTablaDirCatColonias()
        {
            try
            {
                return new LogicDirecciones().ObtenerTablaDirCatColonias();
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
        /// <param name="model">Se requiere un modelo de DirCatMunicipios para insertar el registro (Es un catalogo por lo que se requiere un Id)</param>
        /// <returns>Retorna un modelo de DirCatEstados Actualizado</returns>
        public DirCatColonias InsertarDirCatColonias(DirCatColonias model)
        {
            try
            {
                return new LogicDirecciones().InsertarDirCatColonias(model);
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
        /// Metodo encargado de generar una actualizacion a DirCatColonias
        /// </summary>
        /// <param name="model">Un modelo de DirCatColonias Actualizado</param>
        /// <returns>Retorna un booleano si se actualizo el registro</returns>
        public bool ActualizarDirCatColonias(DirCatColonias model)
        {
            try
            {
                return new LogicDirecciones().ActualizarDirCatColonias(model);
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

        /// <summary>
        /// Metodo encargado de eliminar una un registro de DirCatColonias
        /// </summary>
        /// <param name="model">Se requiere el modelo de DirCatEstados a eliminar</param>
        /// <returns>retorna un Booleano si se pudo eliminar el registro</returns>
        public bool EliminarDirCatColonias(DirCatColonias model)
        {
            try
            {
                return new LogicDirecciones().EliminarDirCatColonias(model);
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
        /// Obtiene una lista de la tabla de DirCatEstados
        /// </summary>
        /// <returns>Retorna una lista con los estados de la republica</returns>
        public List<DirDirecciones> ObtenerTablaDirDirecciones()
        {
            try
            {
                return new LogicDirecciones().ObtenerTablaDirDirecciones();
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
        /// <param name="model">Se requiere un modelo de DirDirecciones para insertar el registro (Es un catalogo por lo que se requiere un Id)</param>
        /// <returns>Retorna un modelo de DirCatEstados Actualizado</returns>
        public DirDirecciones InsertarDirDirecciones(DirDirecciones model)
        {
            try
            {
                return new LogicDirecciones().InsertarDirDirecciones(model);
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
        /// Metodo encargado de generar una actualizacion a DirDirecciones
        /// </summary>
        /// <param name="model">Un modelo de DirCatColonias Actualizado</param>
        /// <returns>Retorna un booleano si se actualizo el registro</returns>
        public bool ActualizarDirDirecciones(DirDirecciones model)
        {
            try
            {
                return new LogicDirecciones().ActualizarDirDirecciones(model);
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
        /// Metodo encargado de eliminar una un registro de DirDirecciones
        /// </summary>
        /// <param name="model">Se requiere el modelo de DirCatEstados a eliminar</param>
        /// <returns>retorna un Booleano si se pudo eliminar el registro</returns>
        public bool EliminarDirDirecciones(DirDirecciones model)
        {
            try
            {
                return new LogicDirecciones().EliminarDirDirecciones(model);
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
