using System.IO;
using System.Text;
using PubliPayments.Entidades.Originacion;
using SelectPdf;

namespace PubliPayments.Negocios.Originacion
{
    /// <summary>
    /// Clase abstracta que representa un generador de un documento PDF a partir de un modelo y un template HTML
    /// Esta clase de debe heredar por cada modelo que se desee renderear en PDF
    /// </summary>
    /// <typeparam name="TM"></typeparam>
    public abstract class GeneradorPdfdeHtml<TM> where TM : class
    {
        public string HtmlTemplateFullPath { get; set; }
        public string FullPdfPath { get; set; }

        protected GeneradorPdfdeHtml(string htmlTemplateFullPath, string fullPdfPath)
        {
            HtmlTemplateFullPath= htmlTemplateFullPath;
            FullPdfPath = fullPdfPath;
        }

        public string ObtenerTemplateEnHtml()
        {
            return File.ReadAllText(HtmlTemplateFullPath);
        }

        public void GenerarPdfxModelo(TM modelo)
        {

            // instantiate the html to pdf converter 
            HtmlToPdf converter = new HtmlToPdf();
            converter.Options.PdfPageSize = PdfPageSize.Letter;
            converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.NoAdjustment;
            converter.Options.AutoFitHeight = HtmlToPdfPageFitMode.NoAdjustment;
            //converter.Options.

            var html = GenerarHtmlPorModelo(modelo);

            // convert the url to pdf 
            var doc = converter.ConvertHtmlString(html);

            // save pdf document             
            doc.Save(FullPdfPath);

            // close pdf document 
            doc.Close();
        }

        public abstract string GenerarHtmlPorModelo(TM modelo);
    }

    /// <summary>
    /// Clase para generar un documento ConsultaBuro en PDF a partir de HTML
    /// Se debe implementar el metodo GenerarHTMLPorModelo para especificar que campos se van a reemplazar con el modelo,
    /// el metodo GenerarHTMLPorModelo es llamado por el metodo GenerarPdfxModelo y este ultimo es el que debe ser llamado en la generación del PDF
    /// </summary>
    public class GeneradorPdfConsultaBuro : GeneradorPdfdeHtml<ConsultaBuroModel>
    {
        public GeneradorPdfConsultaBuro(string htmlTemplateFullPath, string fullPdfPath) 
            : base(htmlTemplateFullPath, fullPdfPath)
        {}

        public override string GenerarHtmlPorModelo(ConsultaBuroModel modelo)
        {
            var htmlTemplate = new StringBuilder(ObtenerTemplateEnHtml());
            htmlTemplate.Replace("%NSS%", modelo.NSS??string.Empty);
            htmlTemplate.Replace("%DIA%", modelo.FechaDia??string.Empty);
            htmlTemplate.Replace("%MES%", modelo.FechaMes??string.Empty);
            htmlTemplate.Replace("%ANIO%", modelo.FechaAnio);
            htmlTemplate.Replace("%NOMBRE%", modelo.Nombre??string.Empty);             	
            htmlTemplate.Replace("%ENTIDADFINANCIERA%", modelo.EntidadFinanciera??string.Empty??string.Empty);

                       
            //if (modelo.PermisoUsoDatosPersonales != null)
            //{
            //    if (modelo.PermisoUsoDatosPersonales == true)
            //    {
            //        //Opcion SI
            //        htmlTemplate.Replace("%DISPLAYVACIO_AUTDATOSCOMERCIALIZARSI%", "none");
            //        htmlTemplate.Replace("%DISPLAYOK_AUTDATOSCOMERCIALIZARSI%", "inline");

            //        //Opcion NO
            //        htmlTemplate.Replace("%DISPLAYVACIO_AUTDATOSCOMERCIALIZARNO%", "inline");
            //        htmlTemplate.Replace("%DISPLAYOK_AUTDATOSCOMERCIALIZARNO%", "none");
            //    }
            //    else
            //    {
            //        //Opcion NO
            //        htmlTemplate.Replace("%DISPLAYVACIO_AUTDATOSCOMERCIALIZARNO%", "none");
            //        htmlTemplate.Replace("%DISPLAYOK_AUTDATOSCOMERCIALIZARNO%", "inline");

            //        //Opcion SI
            //        htmlTemplate.Replace("%DISPLAYVACIO_AUTDATOSCOMERCIALIZARSI%", "inline");
            //        htmlTemplate.Replace("%DISPLAYOK_AUTDATOSCOMERCIALIZARSI%", "none");
            //    }
            //}
            //else
            //{
            //    //Opcion SI
            //    htmlTemplate.Replace("%DISPLAYVACIO_AUTDATOSCOMERCIALIZARSI%", "inline");
            //    htmlTemplate.Replace("%DISPLAYOK_AUTDATOSCOMERCIALIZARSI%", "none");

            //    //Opcion NO
            //    htmlTemplate.Replace("%DISPLAYVACIO_AUTDATOSCOMERCIALIZARNO%", "inline");
            //    htmlTemplate.Replace("%DISPLAYOK_AUTDATOSCOMERCIALIZARNO%", "none");
            //}
            
                        
            return htmlTemplate.ToString();
        }
    }

    /// <summary>
    /// Clase para generar un documento Carta Sesión irrevocable en PDF a partir de HTML
    /// Se debe implementar el metodo GenerarHTMLPorModelo para especificar que campos se van a reemplazar con el modelo,
    /// el metodo GenerarHTMLPorModelo es llamado por el metodo GenerarPdfxModelo y este ultimo es el que debe ser llamado en la generación del PDF
    /// </summary>
    public class GeneradorPdfCartaSesionIrrevocable : GeneradorPdfdeHtml<CartadeSesionIrrevocableModel>
    {
        /// <summary>
        /// Campo que es usado para referencia de  las imágenes y otros recursos HTML dentro de la pagina HTML de Carta de sesion irrevocable
        /// </summary>
        private readonly string _directorioRes;

        public GeneradorPdfCartaSesionIrrevocable(string htmlTemplateFullPath, string fullPdfPath, string directorioRes)
            : base(htmlTemplateFullPath, fullPdfPath)
        {
            _directorioRes = directorioRes;
        }

        public override string GenerarHtmlPorModelo(CartadeSesionIrrevocableModel modelo)
        {
            var htmlTemplate = new StringBuilder(ObtenerTemplateEnHtml());
            htmlTemplate.Replace("%CIUDAD%",modelo.Ciudad??string.Empty);
            htmlTemplate.Replace("%DIAS%", modelo.FechaDia ?? string.Empty);
            htmlTemplate.Replace("%MES%", modelo.FechaMes ?? string.Empty);
            htmlTemplate.Replace("%ANIO%", modelo.FechaAnio ?? string.Empty);
            htmlTemplate.Replace("%NSS%", modelo.NoSeguridadSocial ?? string.Empty);
            htmlTemplate.Replace("%DIRECTORIORES%", _directorioRes ?? string.Empty);
            htmlTemplate.Replace("%NOMBRE%", modelo.NombreTrabajador ?? string.Empty);
            
            return htmlTemplate.ToString();
        }
    }

    /// <summary>
    /// Clase para generar un documento Preventivo en PDF a partir de HTML
    /// Se debe implementar el metodo GenerarHTMLPorModelo para especificar que campos se van a reemplazar con el modelo,
    /// el metodo GenerarHTMLPorModelo es llamado por el metodo GenerarPdfxModelo y este ultimo es el que debe ser llamado en la generación del PDF
    /// </summary>
    public class GeneradorPdfDocumentoPreventivo : GeneradorPdfdeHtml<DocumentoPreventivoDerhabienteModel>
    {
        /// <summary>
        /// Campo que es usado para referencia de  las imágenes y otros recursos HTML dentro de la pagina HTML de Carta de sesion irrevocable
        /// </summary>
        private readonly string _directorioRes;

        public GeneradorPdfDocumentoPreventivo(string htmlTemplateFullPath, string fullPdfPath, string directorioRes)
            : base(htmlTemplateFullPath, fullPdfPath)
        {
            _directorioRes = directorioRes;
        }

        public override string GenerarHtmlPorModelo(DocumentoPreventivoDerhabienteModel modelo)
        {
            var htmlTemplate = new StringBuilder(ObtenerTemplateEnHtml());
            htmlTemplate.Replace("%USUARIO%",modelo.Nombre??string.Empty);
            htmlTemplate.Replace("%NUMERO-DE-CREDITO%", modelo.NoCredito?? string.Empty);
            htmlTemplate.Replace("%NUMERO-ASOCIADO-TARJETA%", EncriptaTarjeta.DesencriptarTarjeta(modelo.NoUnicoAsocATarjeta ?? string.Empty));
            htmlTemplate.Replace("%FECHA%", modelo.FechaCompleta ?? string.Empty);
            htmlTemplate.Replace("%LUGAR%", modelo.Lugar ?? string.Empty);
            htmlTemplate.Replace("%DIRECTORIORES%", _directorioRes ?? string.Empty);            
                                     
            return htmlTemplate.ToString();
        }
    }

    /// <summary>
    /// Clase para generar un documento Acuse de recibo de en PDF a partir HTML
    /// Se debe implementar el metodo GenerarHTMLPorModelo para especificar que campos se van a reemplazar con el modelo,
    /// el metodo GenerarHTMLPorModelo es llamado por el metodo GenerarPdfxModelo y este ultimo es el que debe ser llamado en la generación del PDF
    /// </summary>
    public class GeneradorPdfReciboTarjeta : GeneradorPdfdeHtml<ReciboTarjetaModel>
    {
        /// <summary>
        /// Campo que es usado para referencia de  las imágenes y otros recursos HTML dentro de la pagina HTML de Carta de sesion irrevocable
        /// </summary>
        private readonly string _directorioRes;
        public GeneradorPdfReciboTarjeta(string htmlTemplateFullPath, string fullPdfPath, string directorioRes)
            : base(htmlTemplateFullPath, fullPdfPath)
        {
            _directorioRes = directorioRes;
        }

        public override string GenerarHtmlPorModelo(ReciboTarjetaModel modelo)
        {
            var htmlTemplate = new StringBuilder(ObtenerTemplateEnHtml());

            htmlTemplate.Replace("%DIA%", modelo.Dia ?? string.Empty);
            htmlTemplate.Replace("%MES%", modelo.Mes?? string.Empty);
            htmlTemplate.Replace("%ANIO%", modelo.Anno?? string.Empty);
            htmlTemplate.Replace("%USUARIO%", modelo.NombreTrabajador ?? string.Empty);
            htmlTemplate.Replace("%TARJETA%", EncriptaTarjeta.DesencriptarTarjeta(modelo.NumeroTarjeta ?? string.Empty));
            htmlTemplate.Replace("%BANCO%", modelo.Banco ?? string.Empty);
            htmlTemplate.Replace("%NUMERO%", modelo.Credito ?? string.Empty);

            htmlTemplate.Replace("%DIRECTORIORES%", _directorioRes ?? string.Empty);            
                                    
            return htmlTemplate.ToString();
        }
    }    
}