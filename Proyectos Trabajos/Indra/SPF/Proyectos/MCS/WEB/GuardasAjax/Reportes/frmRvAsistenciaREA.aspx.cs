using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SICOGUA.Entidades;
using SICOGUA.Negocio;
using SICOGUA.Seguridad;
using System.Configuration;
using Microsoft.Reporting.WebForms;

public partial class Reportes_frmRvAsistenciaREA : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["objSession" + Session.SessionID] == null)
        {
            Response.Redirect("~/Default.aspx?strError=" + Server.UrlEncode("X500"));
        }
        if (!IsPostBack)
        {
            /*Para el reporte*/
            clsEntReporteListado objReporte = (clsEntReporteListado)Session["objReporteListado" + Session.SessionID];
            string[] strCredenciales = clsNegCredencial.consultarCredenciales((clsEntSesion)Session["objSession" + Session.SessionID]);
            rvReporte.ServerReport.ReportServerCredentials = new clsSegCredencial(strCredenciales[0], clsSegRijndaelSimple.Decrypt(strCredenciales[1]), strCredenciales[2]);
            rvReporte.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["reporte"]);
            rvReporte.ServerReport.ReportPath = "/ReportesMCS/rptAsistenciaPorDiaServInstEst";
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
            string date = objReporte.FechaReporte.ToString("yyyy-MM-dd");

            List<ReportParameter> parametrosReportes = new List<ReportParameter>();
                parametrosReportes.Add(new ReportParameter("idEstatus", objReporte.idEstatus.ToString()));
            parametrosReportes.Add(new ReportParameter("fecha", date));
            parametrosReportes.Add(new ReportParameter("idServicio", objReporte.Servicio.idServicio.ToString()));
            parametrosReportes.Add(new ReportParameter("idInstalacion", objReporte.Instalacion.IdInstalacion.ToString()));
            parametrosReportes.Add(new ReportParameter("idUsuario", objSesion.usuario.IdUsuario.ToString()));


            rvReporte.ServerReport.SetParameters(parametrosReportes);
            rvReporte.ProcessingMode = ProcessingMode.Remote;
            rvReporte.ShowParameterPrompts = false;
            rvReporte.ShowPromptAreaButton = false;
            rvReporte.ServerReport.Refresh();
            /*termina para el reporte*/
            //reporte.Visible = true;
        }
    }
    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        switch (Request.QueryString["hyper"])
        {
            case "Asistencia":
                Response.Redirect("~/PaseLista/frmListaAsistencia.aspx");
                break;
            default:
                Response.Redirect("~/Reportes/frmReporteAsistenciaREA.aspx");
                break;
        }
    }
}