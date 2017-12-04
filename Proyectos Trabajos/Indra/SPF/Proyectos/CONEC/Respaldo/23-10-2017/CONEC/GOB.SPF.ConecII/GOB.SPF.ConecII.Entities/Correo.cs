namespace GOB.SPF.ConecII.Entities
{
    public class Correo : TEntity
    {
        public TEntity UsuarioEntity { get; set; }
        public bool Activo { get; set; }
        public string CorreoElectronico { get; set; }
    }
}
