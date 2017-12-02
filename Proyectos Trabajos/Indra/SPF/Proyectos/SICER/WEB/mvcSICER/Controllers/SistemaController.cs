using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proNegocio;
using proEntidad;
using proSeguridad;
using System.Globalization;


namespace mvcSICER.Controllers
{
    public class SistemaController : Controller
    {
        public JsonResult catalogoMenu(string tipoOperacion)
        {
            JsonResult json;
            clsEntSesion objSession = (clsEntSesion)System.Web.HttpContext.Current.Session["objSession" + System.Web.HttpContext.Current.Session.SessionID];

            if (objSession == null)
            {
                json = Json(clsNegMenu.menuContextual(tipoOperacion, (byte)254), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
                return json;
            }
            else
            {
                json = Json(clsNegMenu.menuContextual(tipoOperacion, (byte)objSession.Usuario.Perfil.IdRol), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
                return json;
            }
        }

        public JsonResult menuCertificaciones()
        {
            return Json(clsNegMenu.menuCertificaciones(), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        public JsonResult rolPermiso()
        {
            clsEntSesion objSession = (clsEntSesion)System.Web.HttpContext.Current.Session["objSession" + System.Web.HttpContext.Current.Session.SessionID];

            return Json(clsNegMenu.rolPermiso((byte)objSession.Usuario.Perfil.IdRol), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        public JsonResult consultaTemasdeCertificacion()
        {
            return Json(clsNegExamen.consultaTemasdeCertificacion(Convert.ToInt32(@Request.Params["idCertificacion"])), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        public JsonResult consultaFuncionesTema()
        {
            return Json(clsNegExamen.consultaFuncionesTema(Convert.ToInt32(@Request.Params["idTema"])), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }


        public JsonResult consultaPreguntasTema()
        {
           JsonResult resultado=Json(clsNegExamen.consultaPreguntasTema(Convert.ToInt32(@Request.Params["idTema"])), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
           return resultado;
        }

        public JsonResult consultaRespuestasTema()
        {
            JsonResult resultado = Json(clsNegExamen.consultaRespuestasTema(Convert.ToInt32(@Request.Params["idTema"])), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
            return resultado;
        }

        public JsonResult consultaImagenesRespuestas()
        {
            return Json(clsNegExamen.consultaImagenesRespuestas(Convert.ToInt32(@Request.Params["idFuncion"])), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        public JsonResult consultaImagenesPreguntas()
        {
            return Json(clsNegExamen.consultaImagenesPreguntas(Convert.ToInt32(@Request.Params["idFuncion"])), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }


        public JsonResult consultaCertificacionesRegistro(int idRegistro)
        {
            return Json(clsNegExamen.consultaCertificacionesRegistro(Convert.ToInt32(@Request.Params["idRegistro"])), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        public JsonResult validaIngresoCertificacion()
        {
            return Json(clsNegExamen.validaIngresoCertificacion(Convert.ToInt32(@Request.Params["idRegistro"]), Convert.ToInt32(@Request.Params["idCertificacionRegistro"])), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        public JsonResult consultarCalificacion()
        {
            return Json(clsNegExamen.consultarCalificacion(Convert.ToString(@Request.Params["strEvaluacionRespuesta"]), Convert.ToString(@Request.Params["strIdentificadores"])), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }


        public void reiniciaReloj()
        {
            clsNegExamen.reiniciaReloj();
        }
    
        public JsonResult consultaTiempoExam()
        {
            return Json(clsNegExamen.consultaTiempoExam(Convert.ToInt32(@Request.Params["tiempoExam"])), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue
            };
        }

        

    }
}
