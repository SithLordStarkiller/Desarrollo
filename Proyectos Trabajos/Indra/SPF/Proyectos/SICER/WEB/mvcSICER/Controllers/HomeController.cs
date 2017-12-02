using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proNegocio;
using proEntidad;
using mvcSICER.proNegocio;
using mvcSICER.proEntidad;

namespace mvcSICER.Controllers
{
    public class HomeController : Controller
    {
 
        public ActionResult Index()
        {
            if (@Request.Params["mensaje"] != null)
            {
                ViewBag.Message = clsNegUsuario.consultarPermisoCripter(@Request.Params["mensaje"]);
            }
            else
            {
                ViewBag.Message = false;
            }
            return View();
        }
        /*
        public JsonResult acceso()
        {
            string arr = "";
            if (Session["men"] != null)
            {

                arr = clsNegUsuario.consultarPermisoCripter(Session["men"].ToString());
                Session["men"] = null;
            }
            else
            {
                arr = false.ToString();
            }


            return Json(arr, "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }
        */
        public JsonResult insertarRegistroPersona()
        {
            return Json(clsNegInsertar.insertarRegistroPersona(@Request.Params["strRegistroPersonas"], @Request.Params["participante"], @Request.Params["strCerRegistro"], @Request.Params["strEliminar"]), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);//, @Request.Params["strContrasena"]
        }

        public JsonResult insertarContrasena()
        {
            return Json(clsNegInsertar.insertPass(@Request.Params["strContrasena"]), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);//, @Request.Params["strContrasena"]
        }

        #region Busqueda
        public JsonResult Busqueda()
        {
            //try
            //{

            return Json(clsNegBusqueda.Busqueda(

                    @Request.Params["empPaterno"],
                    @Request.Params["empMaterno"],
                    @Request.Params["empNombre"],
                    @Request.Params["empCURP"],
                    @Request.Params["empRFC"],
                    @Request.Params["empNumero"],
                    @Request.Params["participante"],
                    Convert.ToInt32(@Request.Params["start"]),
                    Convert.ToInt32(@Request.Params["limit"]),
                    @Request.Params["buscar"]), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
            //}
            //catch (Exception e)
            //{
            //    return null;
            //}
        }


        public JsonResult validaCURP()
        {
            return Json(clsNegBusqueda.validaCURP(@Request.Params["CURP"]), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Consulta

        public JsonResult Consulta()
        {

            return Json(clsNegBusqueda.Consulta(Convert.ToInt32(@Request.Params["idRegistro"]), @Request.Params["idEmpleado"]==""? Guid.Empty:new Guid(@Request.Params["idEmpleado"])), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ConsultaRegistrosCertificacion

        public JsonResult ConsultaRegistrosCertificacion()
        {
            return Json(clsNegBusqueda.ConsultaRegistrosCertificacion(

                Convert.ToInt32(@Request.Params["idRegistro"])
                ),
                "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        #endregion

        public JsonResult ConsultaUbicacionInterno()
        {
            Guid idEmpleado = new Guid(@Request.Params["idEmpleado"]);

            return Json(clsNegBusqueda.ConsultaUbicacionInterno(idEmpleado), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        public JsonResult obtieneImagenPersona(string imagen)
        {
            return Json(clsNegImagen.obtieneImagenPersona(imagen), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        public JsonResult obtieneImagen()
        {
            return Json(clsNegImagen.obtieneImagen(@Request), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        public JsonResult cargaImagenPDFExamen()
        {
            return Json(clsNegImagen.cargaImagenPDF(@Request), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }


        public JsonResult cargaImagen()
        {
            return Json(clsNegImagen.obtieneImagenCertificacion(@Request.Params["nombreArchivo"], @Request.Params["identificador"]), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet); ;
        }

        public JsonResult almacenaImagenPersona(string imagen, string nombre)
        {

            return Json(clsNegImagen.almacenaImagenPersona(imagen, nombre), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        public JsonResult insertarCertificacion()
        {
            return Json(clsNegInsertar.insertarCertificacion(@Request.Params["strCertificacion"], @Request.Params["strTemas"], @Request.Params["strFunciones"], @Request.Params["strPreguntas"], @Request.Params["strRespuestas"]), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        public JsonResult buscarCertificacion()
        {
            return Json(clsNegExamen.consultarCertificacion(Convert.ToInt32( @Request.Params["idCertificacion"])), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        public JsonResult enviaCorreo(string pdfName, string nameAttach, string receptor, string sender, string subject, string text)
        {
            return Json(clsNegCertificacionPDF.enviaCorreo(pdfName, nameAttach, receptor, sender, subject, text), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }
    }
}
