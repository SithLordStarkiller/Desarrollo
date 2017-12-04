namespace GOB.SPF.ConecII.Entities
{
    public class DomicilioFiscal
    {
        public DomicilioFiscal()
        {
            Asentamiento = new Asentamiento();
            Cliente = new Cliente();
        }

        public Cliente Cliente { get; set; }
        public int Identificador { get; set; }
        public Asentamiento Asentamiento { get; set; }
        public int IdPais { get; set; }
        public string Calle { get; set; }
        public string NoInterior { get; set; }
        public string NoExterior { get; set; }
    }
}
