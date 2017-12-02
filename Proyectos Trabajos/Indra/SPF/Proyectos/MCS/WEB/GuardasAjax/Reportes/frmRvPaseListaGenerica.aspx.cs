using System;
using System.Configuration;
using SICOGUA.Entidades;
using Microsoft.Reporting.WebForms;
using SICOGUA.Seguridad;
using System.Collections.Generic;
using SICOGUA.Negocio;

public partial class Reportes_frmRvPaseListaGenerica : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
   if (Session["objSession" + Session.SessionID] == null)
        {
            Response.Redirect("~/Default.aspx?strError=" + Server.UrlEncode("X500"));
        }
        if (IsPostBack) return;
        clsEntReporteListado objReporte = (clsEntReporteListado)Session["objListaGenerica" + Session.SessionID];
        string[] strCredenciales = clsNegCredencial.consultarCredenciales((clsEntSesion)Session["objSession" + Session.SessionID]);
        rvReporte.ServerReport.ReportServerCredentials = new clsSegCredencial(strCredenciales[0], clsSegRijndaelSimple.Decrypt(strCredenciales[1]), strCredenciales[2]);
        rvReporte.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["reporte"]);
        rvReporte.ServerReport.ReportPath = "/ReportesMCS/rptPaseListaGenerica";
        rvReporte.ShowCredentialPrompts = false;
        int total = rvReporte.ServerReport.GetDataSources().Count;

        // Creo un array de DataSourceCredentials con el total de DataSources que tiene el informe.
        DataSourceCredentials[] permisos = new DataSourceCredentials[total];

        // Obtengo los datasources del informe.
        ReportDataSourceInfoCollection datasources = rvReporte.ServerReport.GetDataSources();

        // Ahora por dataSource se asigna el usuario y password.
        for (int j = 0; j < total; j++)
        {
            permisos[j] = new DataSourceCredentials();
            permisos[j].Name = datasources[j].Name;
            permisos[j].UserId = ((clsEntSesion)Session["objSession" + Session.SessionID]).usuario.UsuLogin;
            permisos[j].Password = clsSegRijndaelSimple.Decrypt(((clsEntSesion)Session["objSession" + Session.SessionID]).usuario.UsuContrasenia);
        }
        // Asigno los permisos.
        rvReporte.ServerReport.SetDataSourceCredentials(permisos);

        clsEntSesion objSesion = (clsEntSesion)Session["objSession" + Session.SessionID];

        List<ReportParameter> parametrosReportes = new List<ReportParameter>();
        parametrosReportes.Add(new ReportParameter("fechaAsistencia", objReporte.FechaReporte.ToShortDateString()));
        parametrosReportes.Add(new ReportParameter("idServicio", objReporte.Servicio.idServicio == 0 ? null : objReporte.Servicio.idServicio.ToString()));
        parametrosReportes.Add(new ReportParameter("idInstalacion", objReporte.Instalacion.IdInstalacion == 0 ? null : objReporte.Instalacion.IdInstalacion.ToString()));

        rvReporte.ServerReport.SetParameters(parametrosReportes);
        rvReporte.ProcessingMode = ProcessingMode.Remote;
        rvReporte.ShowParameterPrompts = false;
        rvReporte.ShowPromptAreaButton = false;
        rvReporte.ServerReport.Refresh();
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Reportes/frmPaseListaGenerica.aspx");
    }
}