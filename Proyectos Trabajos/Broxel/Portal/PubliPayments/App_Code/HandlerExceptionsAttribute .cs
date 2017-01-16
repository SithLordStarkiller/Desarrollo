using System;
using System.Web.Mvc;
using PubliPayments.Utiles;

namespace PubliPayments
{
    public class HandlerExceptionsAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            var exception = filterContext.Exception;

            var req = filterContext.RequestContext.HttpContext.Request;

            if (req.QueryString != null && req.QueryString.ToString() != "")
                Logger.WriteLine(Logger.TipoTraceLog.Trace,
                    Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario)),
                    req.FilePath, "Error: - QueryString: " + req.QueryString + " - " + exception.Message +
                                  (exception.InnerException != null
                                      ? " - InnerExcception: " + exception.InnerException.Message
                                      : ""));
            else
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace,
                    Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario)),
                    req.FilePath,
                    "Error:" + exception.Message +
                    (exception.InnerException != null ? " - InnerExcception: " + exception.InnerException.Message : ""));
            }

            base.OnException(filterContext);
        }
    }
}