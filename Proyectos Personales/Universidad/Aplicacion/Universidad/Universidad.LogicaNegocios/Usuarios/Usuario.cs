namespace Universidad.LogicaNegocios.Usuarios
{
    using Entidades;

    public class Usuarios
    {
        public US_USUARIOS ObtenUsuario(string usuario)
        {
            return new AccesoDatos.Usuarios.Usuarios().ObtenUsuarioLinq(usuario);
        }
    }
}
