using System.Collections.Generic;

namespace PubliPayments.Entidades
{
    public class CampoXSubFormularioModel
    {
        public int IdCampoFormulario { get; set; }
        public int IdSubformulario { get; set; }
        public int IdTipoCampo { get; set; }
        public int Orden { get; set; }
        public string Nombre { get; set; }
        public string Texto { get; set; }
        public string ValorPrecargado { get; set; }
        public string ClasesLinea { get; set; }
        public string ClasesTexto { get; set; }
        public string ClasesValor { get; set; }
        public string Validacion { get; set; }
        public string TipoCampo { get; set; }
        public string Error { get; set; }

        public List<CatalogoXCampoModel> ListaCatalogoXCampo = new List<CatalogoXCampoModel>();

        public CampoXSubFormularioModel()
        {
        }
    }
    
}
