namespace ExamenEdenred.BusinessLogic.Usuarios
{
    public class Usuarios
    {
        public bool ObtenUsuario(int usuarioId)
        {
            return new DataAccess.Usuarios.Usuarios().ObtenUsuario(usuarioId);
        }
    }
}
