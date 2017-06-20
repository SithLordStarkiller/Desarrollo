namespace Suncorp.Entities.Usuarios
{
    using Models;
    using DataAccess;
    using Helpers;
    using Helpers.CustomExcepciones;
    using System.Threading.Tasks;
    using System;
    using System.Reflection;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Se encargara del manejo de los usuarios en general asi como sus componetes
    /// </summary>
    public class EntUsuarios
    {
        private readonly SuncorpEntities _contexto;
        private readonly Logger _logLogger = new Logger(Constantes.UrlLog, Constantes.ConnectionStringTrabajo);

        public EntUsuarios()
        {

        }

        #region Operaciones CRUD

        /// <summary>
        /// Obtiene una lista de la tabla de Ususuarios
        /// </summary>
        /// <returns>Retorna el modelo con el IdUsuario actualizado</returns>
        public async Task<List<UsUsuarios>> ObtenerTablaUsuario()
        {
            try
            {
                using (var aux = new Repositorio<UsUsuarios>())
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
                           MethodBase.GetCurrentMethod().Name, "Error al obtener registro usuario", e.Message, e, ""));
                throw;
            }
        }

        /// <summary>
        /// Retorna un registro de UsUsuario por un criterio
        /// </summary>
        /// <param name="criterio">Criterio por el cual se va a filtrar el registro</param>
        /// <returns>Retorna un registro de UsUsuario</returns>
        public async Task<UsUsuarios> ObteneRegistroUsuario(Expression<Func<UsUsuarios, bool>> criterio)
        {
            try
            {
                using (var aux = new Repositorio<UsUsuarios>())
                {
                    return await aux.Consulta(criterio);
                }
            }
            catch (Exception e)
            {
                await Task.Factory.StartNew(
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
        public async Task<List<UsUsuarios>> ObteneRegistrosUsuarios(Expression<Func<UsUsuarios, bool>> criterio)
        {
            try
            {
                using (var aux = new Repositorio<UsUsuarios>())
                {
                    return await aux.ConsultaLista(criterio);
                }
            }
            catch (Exception e)
            {
                await Task.Factory.StartNew(
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
        public async Task<UsUsuarios> InsertarUsuario(UsUsuarios model)
        {
            try
            {
                using (var aux = new Repositorio<UsUsuarios>())
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
                           MethodBase.GetCurrentMethod().Name, "Error al actualizar usuario", e.Message, e, "Error al insertar: " + model.ToString()));
                throw;
            }
        }

        /// <summary>
        /// Actualiza en la tabla de usuarios
        /// </summary>
        /// <param name="model">Campos que se van a modificar</param>
        /// <returns>retorna un booleano si se completo correctamente ela operacion</returns>
        public async Task<bool> ActualizarUsuario(UsUsuarios model)
        {
            try
            {
                using (var aux = new Repositorio<UsUsuarios>())
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
                           MethodBase.GetCurrentMethod().Name, "Error al actualizar usuario", e.Message, e, "Modelo al actualizar: " + model.ToString()));
                throw;
            }
        }

        /// <summary>
        /// Elimina en la tabla de usuarios
        /// </summary>
        /// <param name="model">Campos que se van a modificar</param>
        /// <returns>retorna un booleano si se completo correctamente ela operacion</returns>
        public async Task<bool> EliminarUsuario(UsUsuarios model)
        {
            try
            {
                using (var aux = new Repositorio<UsUsuarios>())
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
                           MethodBase.GetCurrentMethod().Name, "Error al actualizar usuario", e.Message, e, "Modelo al actualizar: " + model.ToString()));
                throw;
            }
        }

        #endregion

        #region Operaciones

        /// <summary>
        /// Este metodo se encargara de obtener un usuario atra vez del usuario y contraseña
        /// </summary>
        /// <param name="usuario">Usuario que se guarda en la base de datos</param>
        /// <param name="Contrasena">Contraseña sin encriptar </param>
        /// <returns></returns>
        public async Task<UsUsuarios> ObtenerUsuarioLogin(string usuario, string contrasena)
        {
            try
            {
                using (var aux = new Repositorio<UsUsuarios>())
                {
                    return await aux.Consulta(r => r.Usuario == usuario && r.Contrasena == contrasena) ?? throw new UserNotFindException(usuario, contrasena);
                }
            }
            catch (UserNotFindException e)
            {
                await Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.Preventivo,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Login error", e.Message, e, "Usuario: " + e.Usuario + " Contrasena: " + e.Contrasena));
                throw e;
            }
            catch (Exception e)
            {
                await Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.Error,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Login error", e.Message, e, ""));
                throw e;
            }

        }

        #endregion

    }
}
