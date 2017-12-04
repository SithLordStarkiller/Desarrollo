namespace GOB.SPF.ConecII.Web.Models
{
    public class UiAsentamiento : UiEntity
    {
        public int Identificador { get;  set; }
        public string Nombre { get; set; }
        public string CodigoPostal { get; set; }
        public UiMunicipio Municipio { get; set; }
        public UiEstado Estado { get; set; }
    }
}