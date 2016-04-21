using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Universidad.Helpers
{
    public class LsApplicationState
    {
        private const string _sqlconnectionstring = "connection_string";
        private const string _errorlogpath = "error_log_path";
        //private const string _barcodepath = "barcode_path";
        private const string _appname = "app_name";
        private const string _appversion = "app_version";
        //private const string _price = "price";
        //private const string _material1Factor = "materialFactor1";
        //private const string _material2Factor = "materialFactor2";
        private const string _mail = "mail";
        private const string _link = "link";
        private const string _mailactive = "notactivemail";
        private const string _mailnotactive = "activemail";
        private const string _mailforgetpwd = "forgetpwdmail";
        private const string _reportserverurl = "reportserverurl";
        private const string _reportfolder = "reportfolder";

        // RSA 2012-12-19 Debug flag to control access to production resources such as web services
        private const string _debugmode = "debugmode";

        private HttpApplicationState _applicationState;

        public HttpApplicationState ApplicationState
        {
            get { return _applicationState; }
        }

        public LsApplicationState(HttpApplicationState application)
        {
            _applicationState = application;
        }

        public string SQLConnString
        {
            get { return ConfigurationManager.ConnectionStrings["UnivesidadDBTSQL"].ConnectionString; }
            // GetValue(_sqlconnectionstring, ""); }
            set { _applicationState[_sqlconnectionstring] = value; }
        }

        public string ErrorLogPath
        {
            get { return GetValue(_errorlogpath, ""); }
            set { _applicationState[_errorlogpath] = value; }
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
            get { return GetValue(_appname, ""); }
            set { _applicationState[_appname] = value; }
        }

        public string AppVersion
        {
            get { return GetValue(_appversion, ""); }
            set { _applicationState[_appversion] = value; }
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
        /*
        public string Price
        {
            get { return GetValue(_price, ""); }
            set { _applicationState[_price] = value; }
        }

        public string Material1Factor
        {
            get { return GetValue(_material1Factor, ""); }
            set { _applicationState[_material1Factor] = value; }
        }

        public string Material2Factor
        {
            get { return GetValue(_material2Factor, ""); }
            set { _applicationState[_material2Factor] = value; }
        }
        */
        public string Link
        {
            get { return GetValue(_link, ""); }
            set { _applicationState[_link] = value; }
        }

        public string MailActive 
        {
            get { return GetValue(_mailactive, ""); }
            set { _applicationState[_mailactive] = value; }
        }
        public string MailNotActive
        {
            get { return GetValue(_mailnotactive, ""); }
            set { _applicationState[_mailnotactive] = value; }
        }
        public string MailForgetPwd
        {
            get { return GetValue(_mailforgetpwd, ""); }
            set { _applicationState[_mailforgetpwd] = value; }
        }
        public string ReportServerURL
        {
            get { return GetValue(_reportserverurl, ""); }
            set { _applicationState[_reportserverurl] = value; }
        }
        public string ReportFolder
        {
            get { return GetValue(_reportfolder, ""); }
            set { _applicationState[_reportfolder] = value; }
        }

        // RSA 2012 12 19 Debug flag to control access to production resources such as web services
        public string DebugMode
        {
            get { return GetValue(_debugmode, "false"); }
            set { _applicationState[_debugmode] = value; }
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