using System.Collections.Generic;

namespace PubliPayments.Entidades
{
   public class SubFormularioModel
    {
       public int IdFormulario { get; set; }
        public int IdSubFormulario { get; set; }
        public string SubFormulario { get; set; }
        public string Clase { get; set; }
        public string Error { get; set; }

        public List<CampoXSubFormularioModel> ListaCampoXSubFormularios = new List<CampoXSubFormularioModel>();
        public List<FuncionXCampoModel> ListaFuncionesJs = new List<FuncionXCampoModel>();
        public List<FuncionXCampoModel> ListaFuncionPrecargada = new List<FuncionXCampoModel>();

    }
}
