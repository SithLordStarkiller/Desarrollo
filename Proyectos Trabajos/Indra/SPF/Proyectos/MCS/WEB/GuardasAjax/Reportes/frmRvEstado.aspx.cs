using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using SICOGUA.Entidades;
using SICOGUA.Negocio;
using System.Configuration;
using SICOGUA.Seguridad;

public partial class Reportes_frmRvEstado : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
                if (Session["objSession" + Session.SessionID] == null)
        {
            Response.Redirect("~/Default.aspx?strError=" + Server.UrlEncode("X500"));
        }
        if (!IsPostBack)
        {
            clsEntEstadoFuerza objReporte = (clsEntEstadoFuerza)Session["objEstadoFuerza" + Session.SessionID];


            string[] strCredenciales = clsNegCredencial.consultarCredenciales((clsEntSesion)Session["objSession" + Session.SessionID]);
            rvReporte.ServerReport.ReportServerCredentials = new clsSegCredencial(strCredenciales[0], clsSegRijndaelSimple.Decrypt(strCredenciales[1]), strCredenciales[2]);
            rvReporte.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["reporte"]);
            rvReporte.ServerReport.ReportPath = "/ReportesMCS/rptXEstado";
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
            parametrosReportes.Add(new ReportParameter("idUsuario", ((clsEntSesion)Session["objSession" + Session.SessionID]).usuario.IdUsuario.ToString()));

            rvReporte.ServerReport.SetParameters(parametrosReportes);
            rvReporte.ProcessingMode = ProcessingMode.Remote;
            rvReporte.ShowParameterPrompts = false;
            rvReporte.ShowPromptAreaButton = false;
            rvReporte.ServerReport.Refresh();
        }
    }
    }
