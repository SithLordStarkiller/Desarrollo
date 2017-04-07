using System;

namespace PubliPayments.Entidades.MYO
{
    public class Documento
    {
        public int Id { get; set; }
        public string IdSolicitud { get; set; }
        public string Token { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaRespuesta { get; set; }
        public string UsuarioRespuesta { get; set; }
        public string Respuesta { get; set; }
        public string UrlInterno { get; set; }
        public string UrlExterno { get; set; }
        public int Intentos { get; set; }
        public int Status { get; set; }
    }
}
