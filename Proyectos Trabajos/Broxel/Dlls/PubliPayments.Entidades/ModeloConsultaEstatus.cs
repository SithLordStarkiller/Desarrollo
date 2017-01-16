namespace PubliPayments.Entidades
{
    public sealed class ModeloOrdesConsultaEstatus
    {
        public int IdOrden { get; set; }
        public string Nss { get; set; }

        public string Tipo { get; set; }

        public string Correo { get; set; }
        
        public string Nombre { get; set; }

        public string NumeroCredito { get; set; }

    }
}
