namespace Broxel.Services.Usuarios
{
    using Entities.Entidades;
    using BusinessLogic.Usuarios;
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "UsuariosServer" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione UsuariosServer.svc o UsuariosServer.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class UsuariosServer : IUsuariosServer
    {
        public UsUsuarios ObtenUsUsuarionPorLogin(string usuario, string contrasena)
        {
            return new BlUsUsuario().ObtenUsUsuarion(usuario,contrasena);
        }
    }
}
