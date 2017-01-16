namespace PubliPayments.Entidades
{
    public  class CatalogoXCampoModel
    {

        public int IdCatalogoCampo { get; set; }
        public int IdCampoFormulario { get; set; }
        public string Texto { get; set; }
        public string Valor { get; set; }
        public string Auxiliar { get; set; }
        public string Ayuda { get; set; }
        public string Error { get; set; }

    }
}
