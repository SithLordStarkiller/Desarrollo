<%@ WebHandler Language="C#" Class="SSYCSE.verFoto" %>

using System.IO;
using System.Web;
using System.Web.SessionState;

namespace SSYCSE
{
    public class verFoto : IHttpHandler, IReadOnlySessionState 
    {

        public void ProcessRequest(HttpContext context)
        {
            string strSesion = "";
            context.Response.ContentType = "image/x-png";
            if (context.Request.QueryString["var"] != null)
            {
                strSesion = context.Request.QueryString["var"];
            }

            byte[] objByte = new byte[0];

            string fileName = System.AppDomain.CurrentDomain.BaseDirectory + @"Imagenes\User.png";

            if (context.Session != null)
            {
                if (!Equals(context.Session[strSesion + context.Session.SessionID], null))
                {
                    objByte = (byte[])context.Session[strSesion + context.Session.SessionID];
                }
                else
                {
                    objByte = File.ReadAllBytes(fileName);
                }
            }
            else
            {
                objByte = File.ReadAllBytes(fileName);
            }
            if (objByte.Length > 0)
            {
                context.Response.BinaryWrite(objByte);
            }
            else
            {
                objByte = File.ReadAllBytes(fileName);
                context.Response.BinaryWrite(objByte);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}