namespace Suncorp.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Clase encargada del manejo de la comunicacion con los servicios
    /// </summary>
    public abstract class WebServiceResponse
    {
        public bool ProcesoCorrecto { get; set; }
        public DateTime FechaEjecucion { get; set; }
        public string Mensage { get; set; }
    }
    
    [Serializable]
    public class UsUsuarioResponse : WebServiceResponse
    {
        public UsUsuarios Entidad { get; set; }
    }
    
    [Serializable]
    public class ListUsUsuarioResponse : WebServiceResponse
    {
        public List<UsUsuarios> ListaEntidades { get; set; }
    }
}
