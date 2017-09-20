namespace GOB.SPF.ConecII.Entities
{
    public class Telefono : TEntity
    {
        public int IdTipoTelefono { get; set; }
        public string Numero { get; set; }
        public string Extension { get; set; }
    }
}
