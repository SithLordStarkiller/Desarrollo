using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SICOGUA.Entidades;
using SICOGUA.Negocio;
using SICOGUA.Seguridad;
using System.Configuration;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;

public partial class Reportes_frmRvTipoAsignacion : System.Web.UI.Page
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
            clsEntEmpleadoTipoAsignacion objEmpleadoTipoAsignacionReporte = (clsEntEmpleadoTipoAsignacion)Session["objEmpleadoTipoAsignacionReporte" + Session.SessionID];

            string[] strCredenciales = clsNegCredencial.consultarCredenciales((clsEntSesion)Session["objSession" + Session.SessionID]);
            rvReporte.ServerReport.ReportServerCredentials = new clsSegCredencial(strCredenciales[0], clsSegRijndaelSimple.Decrypt(strCredenciales[1]), strCredenciales[2]);
            rvReporte.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["reporte"]);
            rvReporte.ServerReport.ReportPath = "/ReportesMCS/rptPersonalTipoAsignacion";
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
            if (objEmpleadoTipoAsignacionReporte.idServicio != 0)
            {
                parametrosReportes.Add(new ReportParameter("idServicio", objEmpleadoTipoAsignacionReporte.idServicio.ToString()));
            }
            if (objEmpleadoTipoAsignacionReporte.idInstalacion != 0)
            {
                parametrosReportes.Add(new ReportParameter("idInstalacion", objEmpleadoTipoAsignacionReporte.idInstalacion.ToString()));
            }
            if (objEmpleadoTipoAsignacionReporte.idTipoAsignacion != 0)
            {
                parametrosReportes.Add(new ReportParameter("idTipoAsignacion", objEmpleadoTipoAsignacionReporte.idTipoAsignacion.ToString()));
            }
            if (objEmpleadoTipoAsignacionReporte.empNumero != 0)
            {
                parametrosReportes.Add(new ReportParameter("empNumero", objEmpleadoTipoAsignacionReporte.empNumero.ToString()));
            }
            if (objEmpleadoTipoAsignacionReporte.empPaterno.Length != 0)
            {
                parametrosReportes.Add(new ReportParameter("empPaterno", objEmpleadoTipoAsignacionReporte.empPaterno));
            }
            if (objEmpleadoTipoAsignacionReporte.empMaterno.Length != 0)
            {
                parametrosReportes.Add(new ReportParameter("empMaterno", objEmpleadoTipoAsignacionReporte.empMaterno));
            }
            if (objEmpleadoTipoAsignacionReporte.empNombre.Length > 0)
            {
                parametrosReportes.Add(new ReportParameter("empNombre", objEmpleadoTipoAsignacionReporte.empNombre));
            }
            if (objEmpleadoTipoAsignacionReporte.empRFC.Length != 0)
            {
                parametrosReportes.Add(new ReportParameter("emprfc", objEmpleadoTipoAsignacionReporte.empRFC));
            }
            parametrosReportes.Add(new ReportParameter("intFiltroAsignacion", objEmpleadoTipoAsignacionReporte.idEmpleadoAsignacion.ToString()));


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

        Response.Redirect("~/tipoAsignacion/frmTipoAsignacion.aspx");

    }
}