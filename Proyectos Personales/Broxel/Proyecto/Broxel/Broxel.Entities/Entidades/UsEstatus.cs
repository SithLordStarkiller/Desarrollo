namespace Broxel.Entities.Entidades
{
    public class UsEstatus
    {
        public int IdEstatus { get; set; }
        public string Estatus { get; set; }
        public string Descripcion { get; set; }

        public UsEstatus()
        {
        }

        public UsEstatus(int idEstatus, string estatus, string descripcion)
        {
            IdEstatus = idEstatus;
            Estatus = estatus;
            Descripcion = descripcion;
        }
    }
}
