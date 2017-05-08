namespace Suncorp.Services.ServiciosWcf
{
    using BusinessLogic.Usuarios;
    using Helpers;
    using Models;
    using System;

    /// <summary>
    /// Clase con los servicios WCF de los usuarios
    /// </summary>
    public class UsuariosWcf : IUsuariosWcf
    {
        private readonly Logger _logLogger = new Logger(Constantes.UrlLog, Constantes.ConnectionStringTrabajo);

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
            catch (Exception)
            {
                return null;
            }
        }
    }
}
