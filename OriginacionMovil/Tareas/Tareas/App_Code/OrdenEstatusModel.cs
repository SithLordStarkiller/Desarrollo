namespace PubliPayments
{
    class OrdenEstatusModel
    {
        public int Id { get; set; }
        public int Orden { get; set; }
        public int EstatusActual { get; set; }
        public int EstatusAnterior { get; set; }
    }
}
