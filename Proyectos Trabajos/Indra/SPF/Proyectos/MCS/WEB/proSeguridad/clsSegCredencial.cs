using System;
using Microsoft.Reporting.WebForms;
using System.Security.Principal;
using System.Net;

public class clsSegCredencial : IReportServerCredentials
{
    
    private string _UserName;
    private string _PassWord;
    private string _DomainName;

    public clsSegCredencial(string UserName, string PassWord, string DomainName)
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
    public bool GetFormsCredentials(out Cookie authCookie, out string user, out string password, out string authority)
    {
       
        authCookie = null;
        user = password = authority = null;
        return false;
    }
}