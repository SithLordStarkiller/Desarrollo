namespace GOB.SPF.ConecII.Entities.Juridico
{
    public class Articulos : TEntity
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool? Activo { get; set; }
    }
}
