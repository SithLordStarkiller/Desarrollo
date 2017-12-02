using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace GOB.SPF.Conec.Services.Models
{
    internal class AppIdentity:IIdentity
    {
        public AppIdentity(string authType, bool isAuthenticated, string name)
        {
            _authType = authType;
            _isAuthenticated = isAuthenticated;
            _name = name;
        }
        private string _authType;
        private bool _isAuthenticated;
        private string _name;
        public string AuthenticationType { get { return _authType; } }
        public bool IsAuthenticated { get { return _isAuthenticated; } }
        public string Name { get { return _name; } }
    }
}