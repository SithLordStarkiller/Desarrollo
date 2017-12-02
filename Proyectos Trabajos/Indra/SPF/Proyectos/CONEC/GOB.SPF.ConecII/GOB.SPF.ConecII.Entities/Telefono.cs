namespace GOB.SPF.ConecII.Entities
{
    public class Telefono : TEntity
    {
        public Telefono()
        {
            TipoTelefono = new TipoTelefono();
        }
        public TipoTelefono TipoTelefono { get; set; }
        public string Numero { get; set; }
        public string Extension { get; set; }
        public bool Activo { get; set; }
        public TEntity UsuarioEntity { get; set; }
    }
}
