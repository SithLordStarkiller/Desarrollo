namespace PubliPayments.Entidades
{
    public class DominioModel
    {
        public string nombreDominio { get; set; }
        public string nom_corto { get; set; }
        public int estatus { get; set; }

        public DominioModel()
        {    
        }

        public DominioModel(string nombreDominio,string nom_corto,int estatus)
        {
            this.nombreDominio = nombreDominio;
            this.nom_corto = nom_corto;
            this.estatus = estatus;
        }
    }
}
