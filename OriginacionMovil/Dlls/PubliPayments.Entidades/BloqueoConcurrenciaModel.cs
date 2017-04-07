namespace PubliPayments.Entidades
{
    public class BloqueoConcurrenciaModel
    {
        public int Id { get; set; }
        public string Llave { get; set; }
        public string Aplicacion { get; set; }
        public string Origen { get; set; }
        public int Estatus { get; set; }
        public int Error { get; set; }
        public string ErrorMensaje { get; set; }
        public string Resultado { get; set; }
    }
}
