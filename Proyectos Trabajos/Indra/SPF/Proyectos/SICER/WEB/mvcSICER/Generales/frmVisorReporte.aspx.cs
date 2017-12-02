using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Reporting.WebForms;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using proNegocio;
using proSeguridad;
using System.Reflection;


namespace mvcSICER.Generales
{
    public partial class frmVisorReporte : System.Web.UI.Page
    {
        #region funciones
        public void DisableUnwantedExportFormats(ref ReportViewer reportViewer)
        {
            FieldInfo info;

            foreach (RenderingExtension extension in reportViewer.ServerReport.ListRenderingExtensions())
            {
                if (extension.Name.ToUpper() != "PDF" && extension.Name.ToUpper() != "EXCEL") // && extension.Name.ToUpper() != "WORD") // only PDF and WORD - remove the other options
                {
                    info = extension.GetType().GetField("m_isVisible", BindingFlags.Instance | BindingFlags.NonPublic);
                    info.SetValue(extension, false);
                }
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                try
                {
                    string reporte = string.Empty;

                    rvReporte.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["reportes"]);
                    List<ReportParameter> parametrosReportes = new List<ReportParameter>();
                    parametrosReportes.Clear();

                    #region Parámetros

                    int rep = Convert.ToInt32(Request.QueryString["reporte"].Trim());

                    
                    switch (rep)
                    {
                        case 1:
                            parametrosReportes.Add(new ReportParameter("fechaInicio", Request.QueryString["fechaInicio"].Trim() == "" ? "01/01/1901" : Request.QueryString["fechaInicio"].Trim()));
                            parametrosReportes.Add(new ReportParameter("fechaFin", Request.QueryString["fechaFin"].Trim() == "" ? "01/01/1901" : Request.QueryString["fechaFin"].Trim()));
                            parametrosReportes.Add(new ReportParameter("cerActivaDesactivada", Request.QueryString["cerActivaDesactivada"].Trim() == "" ? "3" : Request.QueryString["cerActivaDesactivada"].Trim()));
                            parametrosReportes.Add(new ReportParameter("idEntidadCertificadora", Request.QueryString["idEntidadCertificadora"].Trim() == "null" ? "0" : Request.QueryString["idEntidadCertificadora"].Trim()));
                            parametrosReportes.Add(new ReportParameter("idEntidadEvaluadora", Request.QueryString["idEntidadEvaluadora"].Trim() == "null" ? "0" : Request.QueryString["idEntidadEvaluadora"].Trim()));
                             break;

                  
                    }



                    switch (rep)
                    {
                        case 1:
                            rvReporte.ServerReport.ReportPath = "/rptSICER/rptCertificaciones";
                            break;
             
                    }
                    
                    #endregion

                    #region Configuración

                    rvReporte.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    string[] strCredenciales = clsNegCredencial.consultarCredenciales();


                    rvReporte.ServerReport.ReportServerCredentials = new clsSegCredencial(strCredenciales[0], clsSegRijndaelSimple.Decrypt(strCredenciales[1]), strCredenciales[2]);
                    rvReporte.ShowCredentialPrompts = false;
                    int total = rvReporte.ServerReport.GetDataSources().Count;
                    DataSourceCredentials[] permisos = new DataSourceCredentials[total];
                    ReportDataSourceInfoCollection datasources = rvReporte.ServerReport.GetDataSources();
                    for (int j = 0; j < total; j++)
                    {
                        permisos[j] = new DataSourceCredentials
                        {
                            Name = datasources[j].Name,
                            UserId = ((clsEntSesion)Session["objSession" + Session.SessionID]).Usuario.UsuLogin,
                            Password = clsSegRijndaelSimple.Decrypt(((clsEntSesion)Session["objSession" + Session.SessionID]).Usuario.UsuContrasenia)
                        };
                    }

                    rvReporte.ServerReport.SetDataSourceCredentials(permisos);
                    #endregion

                    #region Materialización

                    rvReporte.ServerReport.SetParameters(parametrosReportes);
                    rvReporte.ProcessingMode = ProcessingMode.Remote;
                    rvReporte.ShowParameterPrompts = false;
                    rvReporte.ShowDocumentMapButton = true;
                    DisableUnwantedExportFormats(ref rvReporte);

                    #endregion
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "Salir", "javascript:alert('NO ES POSIBLE GENERAR EL REPORTE '" + ex.Message + ");", true);
                }
            }
        }
    }
    
}