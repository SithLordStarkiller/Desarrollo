<%@ WebHandler Language="C#" Class="verArchivo" %>

using System.Web;
using System.Web.SessionState;

public class verArchivo : IHttpHandler, IReadOnlySessionState
{
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.QueryString["idSession"] != null)
        {
            byte[] objByte = new byte[0];
            objByte = (byte[])context.Session[context.Request.QueryString["idSession"] + context.Session.SessionID];            
            context.Response.ContentType = "application/pdf";
            context.Response.BinaryWrite(objByte);
        }
        else
            return;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}