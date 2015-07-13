namespace Universidad.ServidorInterno.Usuarios
{
    using Entidades;
    public class SUsuarios : ISUsuarios
    {
        public US_USUARIOS ObtenUsuario(string usuario)
        {
            return new LogicaNegocios.Usuarios.Usuarios().ObtenUsuario(usuario);
        }
    }
}
