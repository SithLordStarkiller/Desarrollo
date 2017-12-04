namespace GOB.SPF.ConecII.Entities
{
    public class ValidacionActividad
    {
        public int IdValidacionActividad { get; set; }
        public int IdActividad { get; set; }
        public string Actividad { get; set; }
        public string IdCentroCosto { get; set; }
        public string CentroCostos { get; set; }
        public bool Obligatorio { get; set; }
    }
}
