namespace GOB.SPF.ConecII.Entities
{
    public class Correo : TEntity
    {
        public bool Activo { get; internal set; }
        public string CorreoElectronico { get; set; }
    }
}
