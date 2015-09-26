using System;
using System.Net;
using System.Security.Principal;
using Microsoft.Reporting.WebForms;

namespace Universidad.WebAdministrativa
{
    public partial class VisorReportes : System.Web.UI.Page
    {
        private string _urlReportServer;
        private const string CarpetaReporte = "/Reportes/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            _urlReportServer = Properties.Settings.Default.ServidorReportes;

            var nombreReporte = Request.QueryString["nombreReporte"];
            var peticion = Request.QueryString["TipoPeticion"];
            var parametros = Request.QueryString["parametros"];

            switch (peticion)
            {
                case "MuestraReportePorNombre":
                    MuestraReportePorNombre(nombreReporte);
                    break;

                case "ReportePrueba":
                    ReportePrueba();
                    break;

                case "MuestraReportePorNombreConParametros":
                    var param = Newtonsoft.Json.JsonConvert.DeserializeObject<ReportParameterCollection>(parametros);
                    MuestraReportePorNombreConParametros(nombreReporte, param);
                    break;
            }
        }

        public void MuestraReportePorNombre(string nombreReporte)
        {
            ReportViewer1.ProcessingMode = ProcessingMode.Remote;

            ReportViewer1.ServerReport.ReportServerUrl = new Uri(_urlReportServer);

            ReportViewer1.ServerReport.ReportPath = CarpetaReporte + nombreReporte;

            ReportViewer1.ServerReport.Refresh();
        }

        public void MuestraReportePorNombreConParametros(string nombreReporte, ReportParameterCollection parametros )
        {
            ReportViewer1.ProcessingMode = ProcessingMode.Remote;

            ReportViewer1.ServerReport.ReportServerUrl = new Uri(_urlReportServer);

            ReportViewer1.ServerReport.ReportPath = CarpetaReporte + nombreReporte;

            ReportViewer1.ServerReport.SetParameters(parametros);

            ReportViewer1.ServerReport.Refresh();
        }

        public void ReportePrueba()
        {
            //_urlReportServer = "http://localhost/ReportServer_MSSQLSERVER2014";

            // ProcessingMode will be Either Remote or Local  
            ReportViewer1.ProcessingMode = ProcessingMode.Remote;

            //Set the ReportServer Url  
            ReportViewer1.ServerReport.ReportServerUrl = new Uri(_urlReportServer);

            // setting report path  
            //Passing the Report Path with report name no need to add report extension   
            ReportViewer1.ServerReport.ReportPath = "/Reportes/Reporte de Personas";

            //Set report Parameter  
            //Creating an ArrayList for combine the Parameters which will be passed into SSRS Report  
            //ArrayList reportParam = new ArrayList();  
            //reportParam = ReportDefaultPatam();  

            //ReportParameter[] param = new ReportParameter[reportParam.Count];  
            //for (int k = 0; k < reportParam.Count; k++)  
            //{  
            //    param[k] = (ReportParameter)reportParam[k];  
            //}  

            // pass credential as if any... no need to pass anything if we use windows authentication  
            //ReportViewer1.ServerReport.ReportServerCredentials = new ReportViewerCredentials("sa", "A@141516182235", @"DR5Q402");  

            //pass parameters to report  
            //rptViewer.ServerReport.SetParameters(param);   
            ReportViewer1.ServerReport.Refresh();
        }
    }
    public class ReportViewerCredentials : IReportServerCredentials
    {
        private readonly string _userName;
        private readonly string _password;
        private readonly string _domain;

        public ReportViewerCredentials(string userName, string password, string domain)
        {
            _userName = userName;
            _password = password;
            _domain = domain;

        }


        public WindowsIdentity ImpersonationUser
        {
            get
            {
                //return null;
                return WindowsIdentity.GetCurrent();
            }
        }

        public ICredentials NetworkCredentials
        {
            get
            {

                return new NetworkCredential(_userName, _password, _domain);

            }
        }

        public bool GetFormsCredentials(out Cookie authCookie,
                out string userName, out string password,
                out string authority)
        {
            authCookie = null;
            userName = _userName;
            password = _password;
            authority = _domain;

            // Not using form credentials   
            return false;
        }

    }
}