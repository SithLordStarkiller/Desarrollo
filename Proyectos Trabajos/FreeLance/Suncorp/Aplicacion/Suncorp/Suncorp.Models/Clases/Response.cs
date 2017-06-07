namespace Suncorp.Models
{
    using System;

    /// <summary>
    /// Clase encargada del manejo de la comunicacion con los servicios
    /// </summary>
    public abstract class Response
    {
        public bool ProcesoCorrecto { get; set; }
        public DateTime FechaEjecucion { get; set; }
        public string Mensage { get; set; }
    }

    public class ServiceResponse : Response
    {
        public object Entidad { get; set; }
        public Exception Excepcions { get; set; }
    }
}
