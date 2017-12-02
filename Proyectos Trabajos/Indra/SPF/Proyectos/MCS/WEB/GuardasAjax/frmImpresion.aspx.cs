using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Reporting.WebForms;
using System.Reflection;
using Microsoft.SqlServer;
using SICOGUA.Entidades;
using SICOGUA.Seguridad;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SICOGUA.Negocio;
using SICOGUA.Seguridad;

public partial class frmImpresion : System.Web.UI.Page
{
    public string[] arrFormatos = { "", "" };
    protected void Page_Load(object sender, EventArgs e)
    {

        clsEntReporteMasivo objReporte = new clsEntReporteMasivo();

        if (Session["impresion" + Session.SessionID] != null)
        {
            objReporte = (clsEntReporteMasivo)Session["impresion" + Session.SessionID];
        }

        if (Session["objSession" + Session.SessionID] == null) ClientScript.RegisterClientScriptBlock(GetType(), "cerrar", "document.close();", true);
        if (!IsPostBack)
        {
            char[] chrSeparador = { '|' };
            arrFormatos[0] = "pdf";
            arrFormatos[1] = "excel";

            rvReporte.Visible = false;
            rvReporte.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["reporte"]);

            rvReporte.ServerReport.ReportPath = "/ReportesMCS/rptReporteAsignacionMasiva";

            rvReporte.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
            string[] strCredenciales = clsNegCredencial.consultarCredenciales((clsEntSesion)Session["objSession" + Session.SessionID]);
            if (strCredenciales == null) return;
            rvReporte.ServerReport.ReportServerCredentials = new clsSegCredencial(strCredenciales[0], clsSegRijndaelSimple.Decrypt(strCredenciales[1]), strCredenciales[2]);

            this.rvReporte.ShowCredentialPrompts = false;
            int total = rvReporte.ServerReport.GetDataSources().Count;

            DataSourceCredentials[] permisos = new DataSourceCredentials[total];
            string login = "";
            ReportDataSourceInfoCollection dataSource = rvReporte.ServerReport.GetDataSources();

            for (int j = 0; j < total; j++)
            {
                permisos[j] = new DataSourceCredentials();
                permisos[j].Name = dataSource[j].Name;
                permisos[j].UserId = ((clsEntSesion)Session["objSession" + Session.SessionID]).usuario.UsuLogin;
                permisos[j].Password = clsSegRijndaelSimple.Decrypt(((clsEntSesion)Session["objSession" + Session.SessionID]).usuario.UsuContrasenia);
                login = permisos[j].UserId;
            }

            rvReporte.ServerReport.SetDataSourceCredentials(permisos);
            List<ReportParameter> parametrosReportes = new List<ReportParameter>();
            parametrosReportes.Clear();

            string id = ((clsEntSesion)Session["objSession" + Session.SessionID]).usuario.IdUsuario.ToString();
            string FE = objReporte.fechaInicio.ToString("yyyy-MM-dd HH:mm:ss");
            parametrosReportes.Add(new ReportParameter("idUsuarioReporte", id));
            parametrosReportes.Add(new ReportParameter("fechaIni", objReporte.fechaInicio.ToString("yyyy-MM-dd HH:mm:ss")));
            parametrosReportes.Add(new ReportParameter("fechaF", objReporte.fechaFin.ToString("yyyy-MM-dd HH:mm:ss")));

            LocalReport localReport = new LocalReport();
            localReport.ReportPath = new Uri(ConfigurationManager.AppSettings["reporte"]) + rvReporte.ServerReport.ReportPath;
            ReportDataSource reportDataSource = new ReportDataSource("permisos", permisos);
            localReport.DataSources.Add(reportDataSource);

            rvReporte.ServerReport.SetParameters(parametrosReportes);
            string reportType = arrFormatos[1];
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>EXCEL</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.35in</MarginTop>" +
                "  <MarginLeft>0.35in</MarginLeft>" +
                "  <MarginRight>0.35in</MarginRight>" +
                "  <MarginBottom>0.35in</MarginBottom>" +
                "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            Response.Clear();
            renderedBytes = rvReporte.ServerReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            Response.ContentType = "application/excel";
            Response.AddHeader("Content-disposition", "filename=output.xls");
            Response.OutputStream.Write(renderedBytes, 0, renderedBytes.Length);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();
            Response.Flush();
            Response.Close();
        }
    }


    
 



   
    

}