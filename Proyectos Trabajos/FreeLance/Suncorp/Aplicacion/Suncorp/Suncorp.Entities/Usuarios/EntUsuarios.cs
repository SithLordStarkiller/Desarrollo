namespace Suncorp.Entities.Usuarios
{
    using Models;
    using DataAccess;
    using Helpers;
    using Helpers.CustomExcepciones;
    using System.Threading.Tasks;
    using System;
    using System.Reflection;

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
                    return await aux.Consulta(r => r.Usuario == usuario && r.Contrasena == contrasena)?? throw new UserNotFindException(usuario,contrasena);
                }
            }
            catch (Exception e)
            {
                await Task.Factory.StartNew(
                   () =>
                       _logLogger.EscribeLog(Logger.TipoLog.Preventivo,
                           Assembly.GetExecutingAssembly().GetName().Name, GetType().Name,
                           MethodBase.GetCurrentMethod().Name, "Login error", "", e, ""));
                throw;
            }
        }

    }
}
