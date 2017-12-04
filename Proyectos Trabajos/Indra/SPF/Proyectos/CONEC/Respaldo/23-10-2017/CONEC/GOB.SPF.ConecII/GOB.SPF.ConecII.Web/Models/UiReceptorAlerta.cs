namespace GOB.SPF.ConecII.Web.Models
{
    using System;

    public class UiReceptorAlerta
    {
        public int IdNotificacion { get; set; }
        public Guid IdPersona { get; set; }
        public int? IdRol { get; set; }
        public int? IdTipoContacto { get; set; }
        public int? IdTipoReceptor { get; set; }
        public string TipoReceptor { get; set; }
        public string Correo { get; set; }
        public string Descripcion { get; set; }
        public bool EsCopia { get; set; }
    }
}