namespace Suncorp.Services.ServiciosWcf
{
    using BusinessLogic.Usuarios;
    using Helpers;
    using Models;
    using Helpers.CustomExcepciones;
    using System;
    using System.Threading.Tasks;
    using System.Reflection;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Clase con los servicios WCF de los usuarios
    /// </summary>
    public class UsuariosWcf : IUsuariosWcf
    {
        private readonly Logger _logLogger = new Logger(Constantes.UrlLog, Constantes.ConnectionStringTrabajo);

        #region Operaciones CRUD

        /// <summary>
        /// Obtiene una lista de la tabla de Ususuarios
        /// </summary>
        /// <returns>Retorna el modelo con el IdUsuario actualizado</returns>
        public List<UsUsuarios> ObtenerTablaUsuario()
        {
            try
            {
                return new LogicUsuarios().ObtenerTablaUsuario();
            }
            catch (Exception e)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.Error,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al obtener registro usuario", e.Message, e, ""));
                throw;
            }
        }

        /// <summary>
        /// Retorna un registro de UsUsuario por un criterio
        /// </summary>
        /// <param name="criterio">Criterio por el cual se va a filtrar el registro</param>
        /// <returns>Retorna un registro de UsUsuario</returns>
        public UsUsuarios ObteneRegistroUsuario(Expression<Func<UsUsuarios, bool>> criterio)
        {
            try
            {
                return new LogicUsuarios().ObteneRegistroUsuario(criterio);
            }
            catch (Exception e)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.Error,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al obtener registro usuario", e.Message, e, criterio.ToString()));
                throw;
            }
        }

        /// <summary>
        /// Retorna una lista de registros de UsUsuario por un criterio
        /// </summary>
        /// <param name="criterio">Criterio por el cual se va a filtrar el registro</param>
        /// <returns>Retorna lista de registros de UsUsuario</returns>
        public List<UsUsuarios> ObteneRegistrosUsuarios(Expression<Func<UsUsuarios, bool>> criterio)
        {
            try
            {
                return new LogicUsuarios().ObteneRegistrosUsuarios(criterio);
            }
            catch (Exception e)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.Error,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al obtener registro usuario", e.Message, e, criterio.ToString()));
                throw;
            }
        }

        /// <summary>
        /// Inserta en la tabla de usuarios
        /// </summary>
        /// <param name="model">Modelo UsUsuario con los campos que se van a modificar</param>
        /// <returns>Retorna el modelo con el IdUsuario actualizado</returns>
        public UsUsuarios InsertarUsuario(UsUsuarios model)
        {
            try
            {
                return new LogicUsuarios().InsertarUsuario(model);
            }
            catch (Exception e)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.Error,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al actualizar usuario", e.Message, e, "Error al insertar: " + model.ToString()));
                throw;
            }
        }

        /// <summary>
        /// Actualiza en la tabla de usuarios
        /// </summary>
        /// <param name="model">Campos que se van a modificar</param>
        /// <returns>retorna un booleano si se completo correctamente ela operacion</returns>
        public bool ActualizarUsuario(UsUsuarios model)
        {
            try
            {
                return new LogicUsuarios().ActualizarUsuario(model);
            }
            catch (Exception e)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.Error,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al actualizar usuario", e.Message, e, "Modelo al actualizar: " + model.ToString()));
                throw;
            }
        }

        /// <summary>
        /// Elimina en la tabla de usuarios
        /// </summary>
        /// <param name="model">Campos que se van a modificar</param>
        /// <returns>retorna un booleano si se completo correctamente ela operacion</returns>
        public bool EliminarUsuario(UsUsuarios model)
        {
            try
            {
                return new LogicUsuarios().EliminarUsuario(model);
            }
            catch (Exception e)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.Error,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al actualizar usuario", e.Message, e, "Modelo al actualizar: " + model.ToString()));
                throw;
            }
        }

        #endregion

        #region Operaciones

        /// <summary>
        /// Metodos wcf para obtener un usuario por contraseña
        /// </summary>
        /// <param name="usuario">usuaio que decea realizar un log in</param>
        /// <param name="contrasena">Contraseña que corresponde al usuario</param>
        /// <returns>Retorna un modelo con la el registro del usuario</returns>
        public UsUsuarios ObtenerUsuarioLogin(string usuario, string contrasena)
        {
            try
            {
                return new LogicUsuarios().ObtenerUsuarioLogin(usuario, contrasena);
            }
            catch (UserNotFindException e)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.Preventivo,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Login error", e.Message, e, "Usuario: " + e.Usuario + " Contrasena: " + e.Contrasena));
                throw;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion
    }
}
