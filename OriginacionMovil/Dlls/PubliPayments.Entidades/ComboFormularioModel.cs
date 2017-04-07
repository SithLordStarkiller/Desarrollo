namespace PubliPayments.Entidades
{
    public class ComboFormularioModel
    {
        public int IdFormulario { get; set; }
        public int IdAplicacion { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Version { get; set; }
        public string Estatus { get; set; }
        public string FechaAlta { get; set; }
        public int Captura { get; set; }
        public string Ruta { get; set; }
    }
}
