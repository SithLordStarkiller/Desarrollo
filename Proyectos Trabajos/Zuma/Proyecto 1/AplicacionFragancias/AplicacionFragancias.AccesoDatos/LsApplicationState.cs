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
        private const string Sqlconnectionstring = "connection_string";


        private HttpApplicationState _applicationState;

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
            get
            {
                return ConfigurationManager.ConnectionStrings["UnivesidadDBTSQL"].ConnectionString;
            }
            set
            {
                _applicationState[Sqlconnectionstring] = value;
            }
        }
    }
}