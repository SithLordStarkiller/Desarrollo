using System;
using System.Diagnostics;
using System.Web.Mvc;
using PubliPayments.Utiles;

namespace PubliPayments
{
    public class LoggerTraceFilterAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Application["Aplicacion"] == null)
                try
                {
                    filterContext.RequestContext.HttpContext.Response.Redirect("/Errores.html");
                }
                catch (Exception ex)
                {
                    Trace.Write(ex.Message);
                }


            base.OnResultExecuting(filterContext);

             var req = filterContext.RequestContext.HttpContext.Request;

            if (req.QueryString != null && req.QueryString.ToString() != "")
                Logger.WriteLine(Logger.TipoTraceLog.Trace,
                    Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario)),
                    req.FilePath, "Trace - QueryString: " + req.QueryString);
            else
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace,
                    Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario)),
                    req.FilePath, "Trace");
            }
        }
    }
}