namespace GOB.SPF.ConecII.Web.Models.Solicitud
{
    public class UiServicioInstalacion : UiInstalacion
    {
        public UiCotizacionDetalle CotizacionDetalle { get; set; }
        public string DireccionCompleta { get; set; }
        public UiFactor FactorActividad { get; set; }
        public UiFactor FactorDistancia { get; set; }
        public UiFactor FactorCriminalidad { get; set; }

    }
}