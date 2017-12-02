<%@ WebHandler Language="C#" Class="ghFotografia" %>

using System;
using System.Web;

public class ghFotografia :IHttpHandler, System.Web.SessionState.IReadOnlySessionState
{


    public void ProcessRequest(HttpContext context)
    {
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        byte[] arrFoto = (byte[]) context.Session["empleadoFoto" + context.Session.SessionID];

        if (arrFoto == null)
        {
            context.Response.ContentType = "image/png";
            context.Response.WriteFile("../Imagenes/UserFoto.png");
        }
        else
        {
            context.Response.ContentType = "image/jpeg";
            context.Response.BinaryWrite(arrFoto);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}