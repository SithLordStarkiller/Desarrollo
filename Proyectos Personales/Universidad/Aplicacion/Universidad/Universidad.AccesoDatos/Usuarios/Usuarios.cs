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
    }
}
