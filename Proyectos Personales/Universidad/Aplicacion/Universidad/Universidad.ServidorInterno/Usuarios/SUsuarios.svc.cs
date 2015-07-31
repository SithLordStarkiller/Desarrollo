namespace Universidad.ServidorInterno.Usuarios
{
    using Entidades;
    using LogicaNegocios.Usuarios;

    public class SUsuarios : ISUsuarios
    {
        public US_USUARIOS ObtenUsuario(string usuario)
        {
            return new Usuarios().ObtenUsuario(usuario);
        }

        public US_USUARIOS ObtenUsuarioPorId(int idUsuario)
        {
            return new Usuarios().ObtenUsuario(idUsuario);
        }

        public US_USUARIOS CrearCuantaUsuario(US_USUARIOS usuario, string personaId)
        {
            return new Usuarios().CreaCuentaUsuario(usuario, personaId);
        }
    }
}
