using System;

namespace Sincronizar
{
    public class OrdenSincronizada
    {
        public string IdOrden { get; set; }
        public int Estatus { get; set; }
        public string EstatusExterno { get; set; }
        public string Usuario { get; set; }
        public string UsuarioExterno { get; set; }
        public string Accion { get; set; }
        public DateTime FechaExterno { get; set; }
        public DateTime FechaEnvio { get; set; }
        public DateTime? FechaRecepcion { get; set; }
        public string FormularioExterno { get; set; }
    }
}
