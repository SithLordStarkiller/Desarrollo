namespace PubliPayments.Entidades
{
    public class ResultadosUsuariosRankingModel
    {
        public string Identificador { get; set; }
        public int Posicion { get; set; }
        public string Usuario { get; set; }
        public string Nombre { get; set; }
        public int Valor { get; set; }
        public int Porcentaje { get; set; }
        

        public ResultadosUsuariosRankingModel(string identificador,string nombre,int valor,string usuario,int posicion,int porcentaje)
        {
            Identificador = identificador;
            Posicion = posicion;
            Usuario = usuario;
            Nombre = nombre;
            Valor = valor;
            Porcentaje = porcentaje;
        }

        public ResultadosUsuariosRankingModel()
        {
          
        }
    }

}
