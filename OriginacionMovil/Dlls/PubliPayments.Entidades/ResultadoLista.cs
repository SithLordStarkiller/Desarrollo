using System.Collections.Generic;

namespace PubliPayments.Entidades
{
    public class ResultadoLista
    {
        public string Resultado { get; set; }
        public List<int> Lista { get; set; }

        public ResultadoLista()
        {
            Lista = new List<int>();
        }
    }
}
