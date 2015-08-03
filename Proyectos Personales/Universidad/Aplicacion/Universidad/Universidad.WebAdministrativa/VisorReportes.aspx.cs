using System;
using System.Data;
using System.Net;
using System.Security.Principal;
using Microsoft.Reporting.WebForms;



namespace Universidad.WebAdministrativa
{
    public partial class VisorReportes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowReport();

            
        }

        private void ShowCustomReport(string reporte, string dataSet, DataTable source,ReportParameterCollection parametes)
        {
            var datasource = source;
            ReportViewer1.Reset();
            ReportViewer1.LocalReport.ReportPath = Server.MapPath(reporte);

            var rds = new ReportDataSource(dataSet, datasource);

            const string error1Value = "Test Error 1";
            const string error2Value = "Test Error 2";
            const string error3Value = "Test Error 3";
            const string error4Value = "Test Error 4";


            var parametros = new ReportParameterCollection
            {
                new ReportParameter("Error_1", error1Value),
                new ReportParameter("Error_2", error2Value),
                new ReportParameter("Error_3", error3Value),
                new ReportParameter("Error_4", error4Value)
            };

            ReportViewer1.LocalReport.SetParameters(parametros);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.DataBind();
            ReportViewer1.LocalReport.Refresh();
        }

        public void ShowReport()
        {
            const string urlReportServer = "http://localhost/ReportServer_MSSQLSERVER2014";

            // ProcessingMode will be Either Remote or Local  
            ReportViewer1.ProcessingMode = ProcessingMode.Remote;

            //Set the ReportServer Url  
            ReportViewer1.ServerReport.ReportServerUrl = new Uri(urlReportServer);

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