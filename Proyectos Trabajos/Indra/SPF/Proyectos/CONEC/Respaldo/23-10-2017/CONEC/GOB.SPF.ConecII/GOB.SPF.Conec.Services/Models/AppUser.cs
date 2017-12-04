using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace GOB.SPF.Conec.Services.Models
{
    internal class AppUser:IPrincipal
    {
        public AppUser(IIdentity identity)
        {
            _identity = identity;
        }
        private IIdentity _identity;
        public IIdentity Identity { get { return _identity; } }
        public bool IsInRole(string role) => true;
    }
}