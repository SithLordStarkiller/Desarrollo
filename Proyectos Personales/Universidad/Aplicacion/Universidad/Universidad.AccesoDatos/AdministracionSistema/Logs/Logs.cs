namespace Universidad.AccesoDatos.AdministracionSistema.Logs
{
    using Entidades;

    public class Logs
    {
        public SIS_LOG_DB InsertaModificacion(SIS_LOG_DB registro)
        {
            using (var r = new Repositorio<SIS_LOG_DB>())
            {
                return r.Agregar(registro);
            }
        }
    }
}
