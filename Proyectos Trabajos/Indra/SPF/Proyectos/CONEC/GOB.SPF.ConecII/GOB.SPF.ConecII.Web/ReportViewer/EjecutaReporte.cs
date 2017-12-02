using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Reporting.WebForms;
using System.Security.Principal;
using System.Net;
using System.Configuration;

namespace GOB.SPF.ConecII.Web.ReportViewer
{
    public static class EjecutaReporte
    {
        public static byte[] BufferRemoteReport(string NombreReporte, List<ReportParameter> listParametros)
        {
            try
            {
                Reporte reporte = new Reporte();
                reporte.CREDENCIAL_USUARIO = ConfigurationManager.AppSettings["CredencialUsr"].ToString();
                reporte.CREDENCIAL_PASSWORD = ConfigurationManager.AppSettings["CredencialPwd"].ToString();
                reporte.CREDENCIAL_DOMINIO = ConfigurationManager.AppSettings["CredencialDom"].ToString();
                reporte.CONFIGURACION_URL_SERVER = ConfigurationManager.AppSettings["RSServidorURL"].ToString();
                reporte.CONFIGURACION_PATH_PARTIAL_REPORTS = ConfigurationManager.AppSettings["RSServidorPath"].ToString();
                reporte.ARCHIVO_EXTENSION = ConfigurationManager.AppSettings["ArchivoExtension"].ToString();
                reporte.ARCHIVO_NOMBRE = NombreReporte;

                Microsoft.Reporting.WebForms.ReportViewer rv = new Microsoft.Reporting.WebForms.ReportViewer();
                rv.ServerReport.ReportServerCredentials = new ReportingCredentials(reporte.CREDENCIAL_USUARIO, reporte.CREDENCIAL_PASSWORD, reporte.CREDENCIAL_DOMINIO);
                rv.ServerReport.ReportServerUrl = new Uri(reporte.CONFIGURACION_URL_SERVER);
                rv.ServerReport.ReportPath = "/" + reporte.CONFIGURACION_PATH_PARTIAL_REPORTS + "/" + reporte.ARCHIVO_NOMBRE;
                rv.ProcessingMode = ProcessingMode.Remote;

                listParametros.ForEach(param => rv.ServerReport.SetParameters(param));

                rv.ServerReport.Refresh();
                string MimeType;
                string EncodingString;
                string FileNameExts;
                Warning[] Warnings;
                string[] StreamIds;
                byte[] bytes = rv.ServerReport.Render(reporte.ARCHIVO_EXTENSION, null, out MimeType, out EncodingString, out FileNameExts, out StreamIds, out Warnings);

                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = MimeType;
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=OutputReporte." + FileNameExts);
                HttpContext.Current.Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush();
                //HttpContext.Current.Response.End();

                return bytes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class ReportingCredentials : Microsoft.Reporting.WebForms.IReportServerCredentials
    {
        private string _UserName;
        private string _PassWord;
        private string _DomainName;

        public ReportingCredentials(string userName, string passWord, string domainName)
        {
            _UserName = userName;
            _PassWord = passWord;
            _DomainName = domainName;
        }

        public WindowsIdentity ImpersonationUser
        {
            get
            {
                return null;
            }
        }

        public ICredentials NetworkCredentials
        {
            get
            {
                return new NetworkCredential(_UserName, _PassWord, _DomainName);
            }
        }

        public bool GetFormsCredentials(out Cookie authCookie, out string user, out string password, out string authority)
        {
            authCookie = null;
            user = password = authority = null;
            return false;
        }
    }

    public class Reporte
    {
        public string CREDENCIAL_USUARIO { get; set; }
        public string CREDENCIAL_PASSWORD { get; set; }
        public string CREDENCIAL_DOMINIO { get; set; }
        public string CONFIGURACION_URL_SERVER { get; set; }
        public string CONFIGURACION_PATH_PARTIAL_REPORTS { get; set; }
        public string ARCHIVO_EXTENSION { get; set; }
        public string ARCHIVO_NOMBRE { get; set; }
    }
}