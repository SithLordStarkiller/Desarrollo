namespace ExamenEdenred.DataAccess
{
    using System.Configuration;
    using System.Web;

    public class LsApplicationState
    {

        private const string Sqlconnectionstring = "connection_string";
        private const string Errorlogpath = "error_log_path";
        private const string Appname = "app_name";
        private const string Appversion = "app_version";
        private const string Mailactive = "notactivemail";
        private const string Mailnotactive = "activemail";
        private const string Mailforgetpwd = "forgetpwdmail";
        private const string Reportserverurl = "reportserverurl";
        private const string Reportfolder = "reportfolder";
        private const string Debugmode = "debugmode";


        private readonly HttpApplicationState _applicationState;

        public HttpApplicationState ApplicationState
        {
            get { return _applicationState; }
        }

        public LsApplicationState(HttpApplicationState application)
        {
            _applicationState = application;
        }

        public string SqlConnString
        {
            get { return ConfigurationManager.ConnectionStrings["UnivesidadDBTSQL"].ConnectionString; }
            // GetValue(_sqlconnectionstring, ""); }
            set { _applicationState[Sqlconnectionstring] = value; }
        }

        public string ErrorLogPath
        {
            get { return GetValue(Errorlogpath, ""); }
            set { _applicationState[Errorlogpath] = value; }
        }
        /*
        public string BarCodePath
        {
            get { return GetValue(_barcodepath, ""); }
            set { _applicationState[_barcodepath] = value; }
        }
        */
        public string AppName
        {
            get { return GetValue(Appname, ""); }
            set { _applicationState[Appname] = value; }
        }

        public string AppVersion
        {
            get { return GetValue(Appversion, ""); }
            set { _applicationState[Appversion] = value; }
        }

        private string GetValue(string name, string defaultvalue)
        {
            string result = _applicationState[name] != null ? _applicationState[name].ToString() : defaultvalue;

            return result;
        }

        public string MailActive
        {
            get { return GetValue(Mailactive, ""); }
            set { _applicationState[Mailactive] = value; }
        }
        public string MailNotActive
        {
            get { return GetValue(Mailnotactive, ""); }
            set { _applicationState[Mailnotactive] = value; }
        }
        public string MailForgetPwd
        {
            get { return GetValue(Mailforgetpwd, ""); }
            set { _applicationState[Mailforgetpwd] = value; }
        }
        public string ReportServerUrl
        {
            get { return GetValue(Reportserverurl, ""); }
            set { _applicationState[Reportserverurl] = value; }
        }
        public string ReportFolder
        {
            get { return GetValue(Reportfolder, ""); }
            set { _applicationState[Reportfolder] = value; }
        }

        // RSA 2012 12 19 Debug flag to control access to production resources such as web services
        public string DebugMode
        {
            get { return GetValue(Debugmode, "false"); }
            set { _applicationState[Debugmode] = value; }
        }
    }
}
