namespace Suncorp.BusinessLogic.Usuarios
{
    using Models;
    using Entities.Usuarios;
    using Helpers;
    using System;
    using System.Threading.Tasks;
    using System.Reflection;
    using Suncorp.Helpers.CustomExcepciones;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Se encarga de manejar la logica de los usuarios
    /// </summary>
    public class LogicUsuarios
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
                return new EntUsuarios().ObtenerTablaUsuario().Result;
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
                return new EntUsuarios().ObteneRegistroUsuario(criterio).Result;
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
                return  new EntUsuarios().ObteneRegistrosUsuarios(criterio).Result;
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
                return new EntUsuarios().InsertarUsuario(model).Result;
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
                return new EntUsuarios().ActualizarUsuario(model).Result;
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
                return new EntUsuarios().EliminarUsuario(model).Result;
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
        /// Obtiene un modelo del usuario con los parametros enviados 
        /// </summary>
        /// <param name="usuario">usuario con el  que se realiza el inicio de sesion</param>
        /// <param name="contrasena">Contraseña que corresponde al usuario</param>
        /// <returns>Retorna un modelo con la informacion del usuario en caso de ser correcto</returns>
        public UsUsuarios ObtenerUsuarioLogin(string usuario, string contrasena)
        {
            try
            {
                return new EntUsuarios().ObtenerUsuarioLogin(usuario, contrasena).Result;
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
            catch (Exception e)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.Preventivo,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Login error", e.Message, e, ""));
                throw;
            }
        }

        #endregion
    }
}
