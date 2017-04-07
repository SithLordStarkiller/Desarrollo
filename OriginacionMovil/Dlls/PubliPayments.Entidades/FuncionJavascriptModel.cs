using System;
using System.Text;

namespace PubliPayments.Entidades
{
    public class FuncionJavascriptModel
    {
         public string Nombre { get; set; }
        public string Validacion { get; set; }
        public string FuncionSi { get; set; }
        public string FuncionNo { get; set; }
        public StringBuilder FuncionPrincipal { get; set; }

        public FuncionJavascriptModel()
        {
        }
        public FuncionJavascriptModel(String nombre)
        {
            Nombre = nombre;
        }
    }
}
