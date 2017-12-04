namespace GOB.SPF.ConecII.Entities
{
    public class Asentamiento : TEntity
    {
        public Asentamiento()
        {
            Municipio = new Municipio();
            Estado = new Estado();
        }
        public string Nombre { get; set; }
        public string CodigoPostal { get; set; }
        public Municipio Municipio { get; set; }
        public Estado Estado { get; set; }
    }
}