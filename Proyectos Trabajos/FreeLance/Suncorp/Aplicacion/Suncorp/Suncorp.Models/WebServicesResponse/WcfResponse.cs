namespace Suncorp.Models
{
    public class WcfResponse
    {
        public EstatusProceso EstatusProceso { get; set; }
        public object ObjetoRespuesta { get; set; }
        public string CodigoError { get; set; }
        public string Mensaje { get; set; }
    }

    public enum EstatusProceso
    {
        Exitoso = 1,
        CompletadoConAdvetencias = 2,
        Error = 3,
        ErrorCritico = 4
    }
}
