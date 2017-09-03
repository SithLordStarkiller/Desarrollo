namespace GOB.SPF.ConecII.Web.Models
{
    public class UiNotificacion
    {
        public int IdNotificacion { get; set; }
        public int IdTipoServicio { get; set; }
        public string TipoServicio { get; set; }
        public int IdActividad { get; set; }
        public string Actividad { get; set; }
        public int IdFase { get; set; }
        public string Fase { get; set; }
        public string CuerpoCorreo { get; set; }
        public bool EsCorreo { get; set; }
        public bool EsSistema { get; set; }
        public bool EmitirAlerta { get; set; }
        public int TiempoAlerta { get; set; }
        public int Frecuencia { get; set; }
        public bool AlertaEsCorreo { get; set; }
        public bool AlertaEsSistema { get; set; }
        public string CuerpoAlerta { get; set; }
        public bool Activo { get; set; }
    }
}