namespace GOB.SPF.ConecII.Entities
{
    using System;

    public class ReceptorAlerta
    {
        public int IdNotificacion { get; set; }
        public Guid IdPersona { get; set; }
        public int IdRol { get; set; }
        public int IdUsuario { get; set; }
        public int IdTipoReceptor { get; set; }
        public string Correo { get; set; }
    }
}
