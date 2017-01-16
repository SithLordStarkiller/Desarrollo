using System;
using System.Diagnostics;
using System.Web.Mvc;
using PubliPayments.Utiles;

namespace PubliPayments
{
    public class ExceptionPublisherExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext exceptionContext)
        {
            var objErr = exceptionContext.Exception;
            var request = exceptionContext.HttpContext.Request;

            try
            {
                string err = "Error en " + Config.AplicacionActual().Nombre +
                             "\nError en: " + request.Url +
                             "\nUsuario: " + SessionUsuario.ObtenerDato(SessionUsuarioDato.Dominio) + "\\" +
                             SessionUsuario.ObtenerDato(SessionUsuarioDato.NombreUsuario) + " - id:" +
                             SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario) +
                             "\nMessage: " + objErr.Message +
                             "\nStack Trace: " + objErr.StackTrace;
                if (objErr.InnerException != null)
                    err += "\nInnerExeption: " + objErr.InnerException.Message;
                Logger.WriteLine(Logger.TipoTraceLog.Error, Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario)), "OnException", err);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(DateTime.Now + " - " + ex.Message);
            }
        }
    }
}