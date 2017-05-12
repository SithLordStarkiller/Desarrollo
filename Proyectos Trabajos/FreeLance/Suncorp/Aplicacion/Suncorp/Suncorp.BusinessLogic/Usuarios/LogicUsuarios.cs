namespace Suncorp.BusinessLogic.Usuarios
{
    using Models;
    using Entities.Usuarios;
    using Helpers;
    using System;
    using System.Threading.Tasks;
    using System.Reflection;
    using Suncorp.Helpers.CustomExcepciones;

    /// <summary>
    /// Se encarga de manejar la logica de los usuarios
    /// </summary>
    public class LogicUsuarios
    {
        private readonly Logger _logLogger = new Logger(Constantes.UrlLog, Constantes.ConnectionStringTrabajo);

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
    }
}
