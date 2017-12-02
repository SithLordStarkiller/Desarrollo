namespace GOB.SPF.ConecII.Entities.Juridico
{
    public class TiposOficioDistribucion : TEntity
    {
        public TipoServicio TipoServicio { get; set; }
        public Area Area { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }
    }
}
