using GOB.SPF.ConecII.Web.Models;
using GOB.SPF.ConecII.Web.Resources;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GOB.SPF.ConecII.Web.ReportViewer
{
    public partial class Reportviewer : System.Web.UI.Page
    {

        private string reportPath;

        protected void Page_Load(object sender, EventArgs e)
        {
            try 
            {
                string nombreReporte = Request["nombre"];
                if (!IsPostBack)
                {
                    ReportConfig reportConfig = new ReportConfig();                    
                    reportViewer.ServerReport.ReportServerUrl = new Uri(reportConfig.URL);
                    reportViewer.ServerReport.ReportPath = $"{reportConfig.Carpeta}/{nombreReporte}";
                    reportViewer.ServerReport.ReportServerCredentials = new ReportCredentials(reportConfig.Usuario, reportConfig.Clave, reportConfig.Domain);
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("ConecWeb", $"{ex.Message}\n{ex.StackTrace}", EventLogEntryType.Error);
            }
            
        }



        private void GetParameters()
        {


        }
    }
}