using ExamenEdenred.Entities.Entities;

namespace ExamenEdenred.BusinessLogic.Usuarios
{
    public class Usuarios
    {
        public UsUsuarios ObtenUsuario(int usuarioId)
        {
            return new DataAccess.Usuarios.Usuarios().ObtenUsuario(usuarioId);
        }
    }
}
