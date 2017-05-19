namespace Suncorp.Services.ServiciosWcf
{
    using Helpers;
    using Models;
    using BusinessLogic.Direcciones;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;

    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "DireccionesWcf" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione DireccionesWcf.svc o DireccionesWcf.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class DireccionesWcf : IDireccionesWcf
    {
        private readonly Logger _logLogger = new Logger(Constantes.UrlLog, Constantes.ConnectionStringTrabajo);

        #region Operaciones CRUD DirCatEstados

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

        #region Operaciones DirCatEstados
        #endregion
    }
}
