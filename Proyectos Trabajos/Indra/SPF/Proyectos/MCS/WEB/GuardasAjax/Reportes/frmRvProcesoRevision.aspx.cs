using System;
using SICOGUA.Entidades;
using Microsoft.Reporting.WebForms;
using SICOGUA.Seguridad;
using System.Collections.Generic;
using System.Configuration;
using SICOGUA.Negocio;

public partial class Reportes_frmRvProcesoRevision : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["objSession" + Session.SessionID] == null)
        {
            Response.Redirect("~/Default.aspx?strError=" + Server.UrlEncode("X500"));
        }

        if (!IsPostBack)
        {
            clsEntActaAdministrativa objProceso =  (clsEntActaAdministrativa) Session["objReporteRevision" + Session.SessionID];
            string[] strCredenciales = clsNegCredencial.consultarCredenciales((clsEntSesion)Session["objSession" + Session.SessionID]);
            rvReporte.ServerReport.ReportServerCredentials = new clsSegCredencial(strCredenciales[0], clsSegRijndaelSimple.Decrypt(strCredenciales[1]), strCredenciales[2]);
            rvReporte.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["reporte"]);

        

            switch (objProceso.proceso)
            {
                case 1:
                    rvReporte.ServerReport.ReportPath = "/ReportesMCS/rptRevision";
                    break;
                case 2:
                    rvReporte.ServerReport.ReportPath = "/ReportesMCS/rptRevisionRenuncia";
                    break;
            }

   
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
            string id = ((clsEntSesion)Session["objSession" + Session.SessionID]).usuario.IdUsuario.ToString();
            parametrosReportes.Add(new ReportParameter("idUsuario", id));
            parametrosReportes.Add(new ReportParameter("idServicio", objProceso.idServicio == 0 ? null : objProceso.idServicio.ToString()));
            parametrosReportes.Add(new ReportParameter("idInstalacion", objProceso.idInstalacion == 0 ? null : objProceso.idInstalacion.ToString()));
            parametrosReportes.Add(new ReportParameter("fechaPrimerActa",objProceso.fechaActaPr=="" ? null:objProceso.fechaActaPr));
            parametrosReportes.Add(new ReportParameter("fechaPrimerActaOficio",objProceso.fechaOficioPr=="" ? null:objProceso.fechaOficioPr));
            parametrosReportes.Add(new ReportParameter("primerActaOficio",objProceso.NoOficioPr=="" ? null:objProceso.NoOficioPr));
            parametrosReportes.Add(new ReportParameter("fechaRenuncia",(objProceso.objRenuncia.fechaRenuncia).ToShortDateString()=="01/01/1900" ? null: (objProceso.objRenuncia.fechaRenuncia).ToShortDateString()));
            parametrosReportes.Add(new ReportParameter("fechaRenunciaOficio",(objProceso.objRenuncia.fechaOficio).ToShortDateString()=="01/01/1900" ? null: (objProceso.objRenuncia.fechaOficio).ToShortDateString()));
            parametrosReportes.Add(new ReportParameter("renunciaOficio",objProceso.objRenuncia.noOficio=="" ? null:objProceso.objRenuncia.noOficio));
            parametrosReportes.Add(new ReportParameter("acta",objProceso.acta==0 ? null:(objProceso.acta).ToString()));
            parametrosReportes.Add(new ReportParameter("proceso",objProceso.proceso==0 ? null:(objProceso.proceso.ToString())));
            parametrosReportes.Add(new ReportParameter("movimiento",objProceso.movimiento==0 ? null:(objProceso.movimiento.ToString())));
            parametrosReportes.Add(new ReportParameter("fechaCancelacion", objProceso.fechaCancelacion == "" ? null : objProceso.fechaCancelacion));
        
	
            rvReporte.ServerReport.SetParameters(parametrosReportes);
            rvReporte.ProcessingMode = ProcessingMode.Remote;
            rvReporte.ShowParameterPrompts = false;
            rvReporte.ShowPromptAreaButton = false;
            rvReporte.ServerReport.Refresh();
        }
    }
    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Reportes/frmProcesoRevision.aspx");
    }
}