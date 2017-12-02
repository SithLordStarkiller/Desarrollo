using System;
using System.Collections.Generic;
using Microsoft.Reporting.WebForms;
using SICOGUA.Entidades;
using SICOGUA.Seguridad;
using System.Configuration;
using SICOGUA.Negocio;

public partial class Reportes_frmRepIncidentes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["objSession" + Session.SessionID] == null)
        {
            Response.Redirect("~/Default.aspx?strError=" + Server.UrlEncode("X500"));
        }
        if (!IsPostBack)
        {
            clsEntReporteIncidente objReporte = (clsEntReporteIncidente)Session["objReporteIncidente" + Session.SessionID];
            string[] strCredenciales = clsNegCredencial.consultarCredenciales((clsEntSesion)Session["objSession" + Session.SessionID]);
            rvReporte.ServerReport.ReportServerCredentials = new clsSegCredencial(strCredenciales[0], clsSegRijndaelSimple.Decrypt(strCredenciales[1]), strCredenciales[2]);
            rvReporte.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["reporte"]);
            rvReporte.ServerReport.ReportPath = "/ReportesMCS/rptReporteIncidentes";
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

            List<ReportParameter> parametrosReportes = new List<ReportParameter>();
            parametrosReportes.Add(new ReportParameter("servicio", objReporte.Servicio.idServicio.ToString()));
            parametrosReportes.Add(new ReportParameter("idServicio", objReporte.Instalacion.IdInstalacion.ToString()));
            parametrosReportes.Add(new ReportParameter("zona", objReporte.ZonaEmpleadoInvolucrado.IdZona.ToString()));
            parametrosReportes.Add(new ReportParameter("riFechaHora", objReporte.RiFechaHora.ToShortDateString()));
            parametrosReportes.Add(new ReportParameter("tarjeta", objReporte.Tarjeta));
            parametrosReportes.Add(new ReportParameter("sede", objReporte.Sede));
            parametrosReportes.Add(new ReportParameter("resumen", objReporte.Resumen));
            rvReporte.ServerReport.SetParameters(parametrosReportes);
            rvReporte.ProcessingMode = ProcessingMode.Remote;
            rvReporte.ShowParameterPrompts = false;
            rvReporte.ShowPromptAreaButton = false;
            rvReporte.ServerReport.Refresh();
        }
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Reportes/frmReporteIncidentes.aspx");
    }
}
