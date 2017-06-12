namespace Suncorp.Services.WebService
{
    using BusinessLogic.Usuarios;
    using Models;
    using Models.Models;
    using Helpers;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Web.Services;
    using System.Xml.Serialization;

    /// <summary>
    /// Descripción breve de UsuariosWs
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class UsuariosWs : WebService
    {

        private readonly Logger _logLogger = new Logger(Constantes.UrlLog, Constantes.ConnectionStringTrabajo);

        #region Operaciones CRUD

        /// <summary>
        /// Obtiene una lista de la tabla de Ususuarios
        /// </summary>
        /// <returns>Retorna el modelo con el IdUsuario actualizado</returns>
        [WebMethod]
        //[XmlInclude(typeof(ListUsUsuarioResponse))]
        public ListUsUsuarioResponse ObtenerTablaUsuario()
        {
            try
            {
                return new ListUsUsuarioResponse
                {
                    ProcesoCorrecto = true,
                    Mensage = "Correcto",
                    FechaEjecucion = DateTime.Now
                    //ListaEntidades = new LogicUsuarios().ObtenerTablaUsuario().ToListModel(UsUsuariosModel)
                };

                return null;
            }
            catch (Exception e)
            {
                Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.Error,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Error al obtener registro usuario", e.Message, e, ""));

                return new ListUsUsuarioResponse
                {
                    ProcesoCorrecto = false,
                    Mensage = "Error: " + e.Message
                    ,
                    FechaEjecucion = DateTime.Now
                };

                return null;
            }
        }

        /// <summary>
        /// Inserta en la tabla de usuarios
        /// </summary>
        /// <param name="model">Modelo UsUsuario con los campos que se van a modificar</param>
        /// <returns>Retorna el modelo con el IdUsuario actualizado</returns>
        //public UsUsuarios InsertarUsuario(UsUsuarios model)
        //{
        //    try
        //    {
        //        return new LogicUsuarios().InsertarUsuario(model);
        //    }
        //    catch (Exception e)
        //    {
        //        Task.Factory.StartNew(
        //           () =>
        //               _logLogger.EscribeLog(Logger.TipoLog.Error,
        //                   Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
        //                   MethodBase.GetCurrentMethod().Name, "Error al actualizar usuario", e.Message, e, "Error al insertar: " + model.ToString()));
        //        throw;
        //    }
        //}

        ///// <summary>
        ///// Actualiza en la tabla de usuarios
        ///// </summary>
        ///// <param name="model">Campos que se van a modificar</param>
        ///// <returns>retorna un booleano si se completo correctamente ela operacion</returns>
        //public bool ActualizarUsuario(UsUsuarios model)
        //{
        //    try
        //    {
        //        return new LogicUsuarios().ActualizarUsuario(model);
        //    }
        //    catch (Exception e)
        //    {
        //        Task.Factory.StartNew(
        //           () =>
        //               _logLogger.EscribeLog(Logger.TipoLog.Error,
        //                   Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
        //                   MethodBase.GetCurrentMethod().Name, "Error al actualizar usuario", e.Message, e, "Modelo al actualizar: " + model.ToString()));
        //        throw;
        //    }
        //}

        ///// <summary>
        ///// Elimina en la tabla de usuarios
        ///// </summary>
        ///// <param name="model">Campos que se van a modificar</param>
        ///// <returns>retorna un booleano si se completo correctamente ela operacion</returns>
        //public bool EliminarUsuario(UsUsuarios model)
        //{
        //    try
        //    {
        //        return new LogicUsuarios().EliminarUsuario(model);
        //    }
        //    catch (Exception e)
        //    {
        //        Task.Factory.StartNew(
        //           () =>
        //               _logLogger.EscribeLog(Logger.TipoLog.Error,
        //                   Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
        //                   MethodBase.GetCurrentMethod().Name, "Error al actualizar usuario", e.Message, e, "Modelo al actualizar: " + model.ToString()));
        //        throw;
        //    }
        //}

        #endregion

        #region Operaciones

        ///// <summary>
        ///// Metodos wcf para obtener un usuario por contraseña
        ///// </summary>
        ///// <param name="usuario">usuaio que decea realizar un log in</param>
        ///// <param name="contrasena">Contraseña que corresponde al usuario</param>
        ///// <returns>Retorna un modelo con la el registro del usuario</returns>
        //public UsUsuarios ObtenerUsuarioLogin(string usuario, string contrasena)
        //{
        //    try
        //    {
        //        return new LogicUsuarios().ObtenerUsuarioLogin(usuario, contrasena);
        //    }
        //    catch (UserNotFindException e)
        //    {
        //        Task.Factory.StartNew(
        //           () =>
        //               _logLogger.EscribeLog(Logger.TipoLog.Preventivo,
        //                   Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
        //                   MethodBase.GetCurrentMethod().Name, "Login error", e.Message, e, "Usuario: " + e.Usuario + " Contrasena: " + e.Contrasena));
        //        throw;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        #endregion
    }
}
