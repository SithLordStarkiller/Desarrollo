
namespace Broxel.BusinessLogic.Usuarios
{
    using DataAccess.Usuarios;
    using Entities.Entidades;

    #region UsUsuario

    public class BlUsUsuario
    {
        public UsUsuarios ObtenUsUsuarion(string usuario, string contrasena)
        {
            return new DaUsUsuarios().ObtenUsUsuarionPorLogin(usuario, contrasena);
        }
    }

    #endregion
}
