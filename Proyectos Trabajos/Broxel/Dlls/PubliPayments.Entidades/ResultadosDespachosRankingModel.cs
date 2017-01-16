namespace PubliPayments.Entidades
{
    public class ResultadosDespachosRankingModel
    {
        public int Posicion { get; set; }
        public string Identificador { get; set; }
        public string Nombre { get; set; }
        public int Valor { get; set; }
        

        public ResultadosDespachosRankingModel(string identificador,string nombre,int valor,int posicion)
        {
            Identificador = identificador;
            Nombre = nombre;
            Valor = valor;
            Posicion = posicion;
        }

        public ResultadosDespachosRankingModel()
        {
          
        }
    }

}
