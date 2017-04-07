namespace PubliPayments.Entidades
{
    public class MensajesServiciosModel
    {
        public string Titulo {get; set; }
        public string Mensaje { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public bool EsHtml { get; set; }
        public int Tipo { get; set; }


        public MensajesServiciosModel(string titulo, string mensaje, string clave, string descripcion, bool esHtml,int tipo)
        {
            Titulo = titulo;
            Mensaje = mensaje;
            Clave = clave;
            Descripcion = descripcion;
            EsHtml = esHtml;
            Tipo = tipo;
        }
    }
}
