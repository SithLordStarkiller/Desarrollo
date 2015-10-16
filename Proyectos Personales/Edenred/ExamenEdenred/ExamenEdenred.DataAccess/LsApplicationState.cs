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
        private const string mail = "mail";
        private const string link = "link";
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
            string result;
            if (_applicationState[name] != null)
            {
                result = _applicationState[name].ToString();
            }
            else
            {
                result = defaultvalue;
            }

            return result;
        }

        public string Link
        {
            get { return GetValue(link, ""); }
            set { _applicationState[link] = value; }
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

    public class MailHostModle
    {
        private const string _mail = "mail";
        private const string _host = "host";
        private const string _usermail = "usermail";
        private const string _pwd = "pwd";
        private const string _port = "port";


        private HttpApplicationState _applicationState;

        public HttpApplicationState ApplicationState
        {
            get { return _applicationState; }
        }

        public MailHostModle(HttpApplicationState application)
        {
            _applicationState = application;
        }

        private string GetValue(string name, string defaultvalue)
        {
            string result;
            if (_applicationState[name] != null)
            {
                result = _applicationState[name].ToString();
            }
            else
            {
                result = defaultvalue;
            }

            return result;
        }


        public string Mail
        {
            get { return GetValue(_mail, ""); }
            set { _applicationState[_mail] = value; }
        }


        public string Host
        {
            get { return GetValue(_mail, ""); }
            set { _applicationState[_mail] = value; }
        }
        public string Port
        {
            get { return GetValue(_port, ""); }
            set { _applicationState[_port] = value; }
        }
        public string UserMail
        {
            get { return GetValue(_usermail, ""); }
            set { _applicationState[_usermail] = value; }
        }

        public string Pwd
        {
            get { return GetValue(_pwd, ""); }
            set { _applicationState[_pwd] = value; }
        }
    }
}
