using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Security.Principal;

namespace GOB.SPF.ConecII.Web.Models
{
    public class ReportCredentials : IReportServerCredentials
    {
        private string _UserName;
        private string _PassWord;
        private string _DomainName;

        public ReportCredentials(string UserName, string PassWord, string DomainName)
        {
            _UserName = UserName;
            _PassWord = PassWord;
            _DomainName = DomainName;
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

        public bool GetFormsCredentials(out Cookie authCookie, out string userName, out string password, out string authority)
        {
            authCookie = null;
            userName = password = authority = null;
            return false;
        }
    }
}