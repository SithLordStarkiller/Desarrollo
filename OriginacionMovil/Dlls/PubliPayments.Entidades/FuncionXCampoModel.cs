using System.Collections.Generic;
using System.Text;

namespace PubliPayments.Entidades
{
   public  class FuncionXCampoModel
    {
        public int IdFormulario { get; set; }
        public int IdSubFormulario { get; set; }
        public int IdFuncionJs { get; set; }
        public bool Mostrar { get; set; }
        public string Campo { get; set; }
        public string Error { get; set; }
        public StringBuilder FuncionFinal = new StringBuilder();
        public List<CondicionalesJsModel> Condicionales { get; set; }
    }
}
