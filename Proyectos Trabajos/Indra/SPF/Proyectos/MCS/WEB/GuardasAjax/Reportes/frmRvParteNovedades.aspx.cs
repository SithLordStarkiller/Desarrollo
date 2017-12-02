using System;
using SICOGUA.Entidades;
using Microsoft.Reporting.WebForms;
using SICOGUA.Seguridad;
using System.Collections.Generic;
using System.Configuration;
using SICOGUA.Negocio;

public partial class Reportes_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["objSession" + Session.SessionID] == null)
        {
            Response.Redirect("~/Default.aspx?strError=" + Server.UrlEncode("X500"));
        }
        if (!IsPostBack)
        {
            clsEntParteNovedad objReporte = (clsEntParteNovedad)Session["objNovedades" + Session.SessionID];
            string[] strCredenciales = clsNegCredencial.consultarCredenciales((clsEntSesion)Session["objSession" + Session.SessionID]);
            rvReporte.ServerReport.ReportServerCredentials = new clsSegCredencial(strCredenciales[0], clsSegRijndaelSimple.Decrypt(strCredenciales[1]), strCredenciales[2]);
            rvReporte.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["reporte"]);
            rvReporte.ServerReport.ReportPath = "/ReportesMCS/rptParteNovedad";
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
            parametrosReportes.Add(new ReportParameter("pnFecha", objReporte.PnFecha.ToShortDateString()));
            parametrosReportes.Add(new ReportParameter("horaPN", objReporte.PnFecha.ToShortTimeString()));
            parametrosReportes.Add(new ReportParameter("empleadoRecibe", objReporte.IdEmpleadoRecibe.ToString()));
            parametrosReportes.Add(new ReportParameter("empleadoReporta", objReporte.IdEmpleadoReporte.ToString()));
            parametrosReportes.Add(new ReportParameter("pnEntradaFuerza", objReporte.PnEntradaFuerza));
            parametrosReportes.Add(new ReportParameter("pnSalidaFuerza", objReporte.PnSalidaFuerza));
            parametrosReportes.Add(new ReportParameter("pnAltas", objReporte.PnAltas));
            parametrosReportes.Add(new ReportParameter("pnBajas", objReporte.PnBajas));
            parametrosReportes.Add(new ReportParameter("pnNotaFaltistasPrimerDia", objReporte.PnNotaFaltistasPrimerDia));
            parametrosReportes.Add(new ReportParameter("pnNotaFaltistasSegundoDia", objReporte.PnNotaFaltistasSegundoDia));
            parametrosReportes.Add(new ReportParameter("pnNotaFaltistasTercerDia", objReporte.PnNotaFaltistasTercerDia));
            parametrosReportes.Add(new ReportParameter("pnNotaFaltistasCuartoDia", objReporte.PnNotaFaltistasCuartoDia));
            parametrosReportes.Add(new ReportParameter("pnNotaRetardos", objReporte.PnNotaRetardos));
            parametrosReportes.Add(new ReportParameter("pnNotaExceptuados", objReporte.PnNotaExceptuados));
            parametrosReportes.Add(new ReportParameter("pnNotaPresentesPrimerDia", objReporte.PnNotaPresentesPrimerDia));
            parametrosReportes.Add(new ReportParameter("pnNotaPresentesSegundoDia", objReporte.PnNotaPresentesSegundoDia));
            parametrosReportes.Add(new ReportParameter("pnNotaPresentesTercerDia", objReporte.PnNotaPresentesTercerDia));
            parametrosReportes.Add(new ReportParameter("pnNotaPresentesLicenciaMedica", objReporte.PnNotaPresentesLicenciaMedica));
            parametrosReportes.Add(new ReportParameter("pnNotaLicenciasMedicas", objReporte.PnNotaLicenciasMedicas));
            parametrosReportes.Add(new ReportParameter("pnNotaPresentesVacaciones", objReporte.PnNotaPresentesVacaciones));
            parametrosReportes.Add(new ReportParameter("pnNotaVacaciones", objReporte.PnNotaVacaciones));
            parametrosReportes.Add(new ReportParameter("pnCopia", objReporte.PnCopia));
            parametrosReportes.Add(new ReportParameter("idUsuario", objSesion.usuario.IdUsuario.ToString()));
            rvReporte.ServerReport.SetParameters(parametrosReportes);
            rvReporte.ProcessingMode = ProcessingMode.Remote;
            rvReporte.ShowParameterPrompts = false;
            rvReporte.ShowPromptAreaButton = false;
            rvReporte.ServerReport.Refresh();
        }
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Reportes/frmParteNovedad.aspx");
    }
}
