using System;
using System.Web;
using System.Web.Security;
using PubliPayments.Utiles;

namespace PubliPayments
{
    public class SessionUsuario
    {
        public static string ObtenerDato(SessionUsuarioDato dato)
        {
            try
            {
                if (HttpContext.Current.User.Identity.GetType().FullName == "System.Web.Security.FormsIdentity")
                {
                    FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                    FormsAuthenticationTicket ticket = id.Ticket;
                    string userData = ticket.UserData;
                    string[] datos = userData.Split(',');
                    return datos[(int)dato];
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "ObtenerDato",
                    "SessionUsuarioDato - " + dato + " - Error: " + ex.Message);
            }

            return "0";
        }
    }

    public enum SessionUsuarioDato
    {
        IdUsuario = 0,
        IdRol = 1,
        NombreRol = 2,
        IdDominio = 3,
        Dominio = 4,
        NombreDominio = 5,
        NombreUsuario = 6,
        NomCorto= 7,
        EsCallCenterOut = 8
    }
}