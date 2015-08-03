namespace Universidad.LogicaNegocios.Usuarios
{
    using Entidades;

    public class Usuarios
    {
        public US_USUARIOS ObtenUsuario(string usuario)
        {
            return new AccesoDatos.Usuarios.Usuarios().ObtenUsuarioLinq(usuario);
        }

        public US_USUARIOS ObtenUsuario(int idUsuario)
        {
            return new AccesoDatos.Usuarios.Usuarios().ObtenUsuarioLinq(idUsuario);
        }

        public US_USUARIOS CreaCuentaUsuario(US_USUARIOS usuario, string personaId)
        {
            var nuevoUsuario = new AccesoDatos.Usuarios.Usuarios().InsertaUsuarioLinq(usuario);
            var persona = new AccesoDatos.Personas.Personas().BuscarPersonaLinq(personaId);

            persona.ID_USUARIO = nuevoUsuario.ID_USUARIO;

            var actualizado = new AccesoDatos.Personas.Personas().ActualizaPersonaLinq(persona);

            return actualizado ? nuevoUsuario : null;
        }

        public US_USUARIOS ActualizaCuentaUsuarios(US_USUARIOS usuario)
        {
            var actualizado = new AccesoDatos.Usuarios.Usuarios().ActulizaUsuarioLinq(usuario);

            return actualizado ? usuario : null;
        }
    }
}
