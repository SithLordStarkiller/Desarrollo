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
                return r.Extraer(a => a.USUARIO == usuario);
            }
        }

        public US_USUARIOS ObtenUsuarioLinq(int idUsuario)
        {
            using (var r = new Repositorio<US_USUARIOS>())
            {
                return r.Extraer(a => a.ID_USUARIO == idUsuario);
            }
        }

        public US_USUARIOS InsertaUsuarioLinq(US_USUARIOS usuario)
        {
            using (var r = new Repositorio<US_USUARIOS>())
            {
                return r.Agregar(usuario);
            }
        }

        public bool ActulizaUsuarioLinq(US_USUARIOS usuario)
        {
            using (var r = new Repositorio<US_USUARIOS>())
            {
                return r.Actualizar(usuario);
            }
        }
    }
}
