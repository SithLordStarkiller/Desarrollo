namespace PubliPayments.Entidades
{
    public class IndicadoresRankingModel
    {
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        

        public IndicadoresRankingModel(string clave,string descripcion)
        {
            Clave = clave;
            Descripcion = descripcion;
        }

        public IndicadoresRankingModel()
        {
          
        }
    }
}
