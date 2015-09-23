using Universidad.Helpers;

namespace Universidad.AccesoDatos.Usuarios
{
    using Entidades;

    public class Usuarios
    {
        //private readonly UniversidadBDEntities _contexto = new UniversidadBDEntities();

        public US_USUARIOS ObtenUsuarioLinq(string usuario)
        {
            using (var r = new Repositorio<US_USUARIOS>())
            {
                var registro = r.Extraer(a => a.USUARIO == usuario);

                var textoEncriptado = new Encriptacion().DesencriptarTexto(registro.CONTRASENA);

                registro.CONTRASENA = textoEncriptado;

                return registro;
            }
        }

        public US_USUARIOS ObtenUsuarioLinq(int idUsuario)
        {
            using (var r = new Repositorio<US_USUARIOS>())
            {
                var registro = r.Extraer(a => a.ID_USUARIO == idUsuario);

                var textoEncriptado = new Encriptacion().DesencriptarTexto(registro.CONTRASENA);

                registro.CONTRASENA = textoEncriptado;

                return registro;
            }
        }

        public US_USUARIOS InsertaUsuarioLinq(US_USUARIOS usuario)
        {
            var textoEncriptado = new Encriptacion().EncriptarTexto(usuario.CONTRASENA);

            usuario.CONTRASENA = textoEncriptado;
            
            using (var r = new Repositorio<US_USUARIOS>())
            {
                return r.Agregar(usuario);
            }
        }

        public bool ActulizaUsuarioLinq(US_USUARIOS usuario)
        {
            var textoEncriptado = new Encriptacion().EncriptarTexto(usuario.CONTRASENA);

            usuario.CONTRASENA = textoEncriptado;

            using (var r = new Repositorio<US_USUARIOS>())
            {
                return r.Actualizar(usuario);
            }
        }
    }
}
