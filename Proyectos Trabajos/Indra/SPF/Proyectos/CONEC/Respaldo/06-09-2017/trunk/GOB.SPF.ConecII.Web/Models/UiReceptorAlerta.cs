namespace GOB.SPF.ConecII.Web.Models
{
    using System;
    using Entities;

    public class UiReceptorAlerta
    {
        public int IdNotificacion { get; set; }
        public Guid IdPersona { get; set; }
        public int IdRol { get; set; }
        public int IdUsuario { get; set; }
        public int IdTipoReceptor { get; set; }
        public string Correo { get; set; }
    }
}