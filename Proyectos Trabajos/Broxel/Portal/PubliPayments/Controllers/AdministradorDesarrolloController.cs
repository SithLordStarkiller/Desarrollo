using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Mvc;
using PubliPayments.Negocios;
using PubliPayments.Utiles;

namespace PubliPayments.Controllers
{
    public class AdministradorDesarrolloController : Controller
    {
        //
        // GET: /AdministradorDesarrollo/
        private static Dictionary<int,DateTime> Solicitudes = new Dictionary<int,DateTime>();
        private static object bloqueo = new object();

        public ActionResult Index()
        {
          var idUsuario = SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario);
            var idRol = SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol);
            var idDominio = SessionUsuario.ObtenerDato(SessionUsuarioDato.IdDominio);

            Logger.WriteLine(Logger.TipoTraceLog.Log, Convert.ToInt32(idUsuario), "AdministradorDesarrollo", "Trace - Index");

            if (idDominio == "1" && idRol == "0")
            {
                return View();
            }
            Logger.WriteLine(Logger.TipoTraceLog.Error, Convert.ToInt32(idUsuario), "AdministradorDesarrollo",
                "Trace - Index - rol:" + idRol + " - Dominio:" + idDominio);
            return Redirect("~/unauthorized.aspx");
        }

        public ActionResult EjecutarQuery(string sql)
        {
            var idUsuario = SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario);
            var idRol = SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol);
            var idDominio = SessionUsuario.ObtenerDato(SessionUsuarioDato.IdDominio);

            if (idDominio == "1" && idRol == "0")
            {
                if (sql == null)
                    return Content("");

                Logger.WriteLine(Logger.TipoTraceLog.Log, Convert.ToInt32(idUsuario), "AdministradorDesarrollo", "EjecutarQuery - sql: " + sql);

                var valida = ValidarQuery(sql);

                //Antes de ejecutar la query valído que hayan pasado 5 segundos desde la última consulta para ese usuario
                lock (bloqueo)
                {
                    if (Solicitudes.ContainsKey(Convert.ToInt32(idUsuario)))
                    {
                        if ((DateTime.Now - Solicitudes[Convert.ToInt32(idUsuario)]).TotalSeconds <= 5)
                        {
                            Logger.WriteLine(Logger.TipoTraceLog.Log, Convert.ToInt32(idUsuario), "AdministradorDesarrollo", "EjecutarQuery - Solo se permite ejecutar una consulta cada 5 segundos");
                            return Content("Solo se permite ejecutar una consulta cada 5 segundos.");
                        }
                        Solicitudes[Convert.ToInt32(idUsuario)] = DateTime.Now;
                    }
                    else
                    {
                        Solicitudes.Add(Convert.ToInt32(idUsuario), DateTime.Now);
                    }
                }

                if (valida == "")
                {
                    var negocio = new AdministradorDesarrollo();
                    DataTable resultado;
                    try
                    {
                        resultado = negocio.EjecutarQuery(sql);

                        Logger.WriteLine(Logger.TipoTraceLog.Log, Convert.ToInt32(idUsuario), "AdministradorDesarrollo",
                            "EjecutarQuery - Resultado count" + resultado.Rows.Count);
                    }
                    catch (Exception ex)
                    {
                        string texto = ex.Message;
                        if (ex.InnerException != null)
                            texto += Environment.NewLine + "InnerException: " + ex.InnerException.InnerException.Message;
                        Logger.WriteLine(Logger.TipoTraceLog.Error, Convert.ToInt32(idUsuario), "AdministradorDesarrollo", "EjecutarQuery - Error: " + texto);
                        return Content(texto);
                    }
                    
                    return PartialView(resultado);
                }
                return Content(valida);
            }
            Logger.WriteLine(Logger.TipoTraceLog.Error, Convert.ToInt32(idUsuario), "AdministradorDesarrollo",
                "EjecutarQuery - unauthorized - rol:" + idRol + " - Dominio:" + idDominio);
            return Redirect("~/unauthorized.aspx");
        }

        private string ValidarQuery(string sql)
        {
            var lista = new List<string> { "sp_executesql", "execute", "into", "update", "delete", "transac", "master..", "xp_", "sp_", ";", "--", "/*", "*/"};
            var mensaje = new StringBuilder();

            foreach (string s in lista)
            {
                if (sql.ToLower().IndexOf(s, StringComparison.Ordinal) >= 0)
                {
                    mensaje.Append("No se pueden ejecutar sentencias que contengan la palabra " + s);
                    mensaje.Append(Environment.NewLine);
                }
            }

            if (sql.ToLower().IndexOf("select ", StringComparison.Ordinal) < 0 ||
                sql.ToLower().IndexOf(" top ", StringComparison.Ordinal) < 0 ||
                sql.ToLower().IndexOf("(nolock)", StringComparison.Ordinal) < 0)
            {
                mensaje.Append("Solo se permiten select limitados con top y with (nolock) ");
                mensaje.Append(Environment.NewLine);
            }

            if (!sql.ToLower().StartsWith("select"))
            {
                mensaje.Append("Solo se permiten select limitados con top y with (nolock) ");
                mensaje.Append(Environment.NewLine);
            }

            return mensaje.ToString();
        }
    
    }
}
