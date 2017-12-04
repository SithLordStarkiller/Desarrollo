using GOB.SPF.ConecII.Web.Resources;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;

namespace GOB.SPF.ConecII.Web.ReportViewer
{
    public class ReportConfig
    {

        private string url;
        private string carpeta;
        private string usuario;
        private string clave;
        private string domain;

        public ReportConfig()
        {
            var reportConfiguration = (NameValueCollection)ConfigurationManager.GetSection(ResourceAppSettings.SECTIONREPORT);
            url = reportConfiguration[ResourceAppSettings.REPORTSERVER];
            carpeta = reportConfiguration[ResourceAppSettings.REPORTFOLDER];
            usuario = reportConfiguration[ResourceAppSettings.REPORTUSUARIO];
            clave = reportConfiguration[ResourceAppSettings.REPORTCLAVE];
            domain = reportConfiguration[ResourceAppSettings.REPORTDOMAIN];

        }

        public string URL { get { return url; } }

        public string Carpeta { get { return carpeta; } }

        public string Reporte { get; set; }

        public string Usuario { get { return usuario; }  }

        public string Clave { get { return clave; } }

        public string Domain { get { return domain; } }
    }
}