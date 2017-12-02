using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proNegocio;
using proEntidad;

namespace mvcSICER.Controllers
{
    public class CatalogosController : Controller
    {
        public JsonResult catalogoEstados()
        {
            return Json(clsNegCatalogos.catalogoEstados(), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }


        public JsonResult catalogoMunicipios()
        {
            return Json(clsNegCatalogos.catalogoMunicipios(Convert.ToInt32(@Request.Params["idEstado"])), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        public JsonResult catalogoEvaluador()
        {
            return Json(clsNegCatalogos.catalogoEvaluador(), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }


        public JsonResult catalogoNivelSeguridad()
        {
            return Json(clsNegCatalogos.catalogoNivelSeguridad(), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }
        public JsonResult catalogoDependenciaExterna()
        {
            return Json(clsNegCatalogos.catalogoDependenciaExterna(Convert.ToInt16(@Request.Params["idNivelSeguridad"])), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        public JsonResult catalogoInstitucionExterna()
        {
            return Json(clsNegCatalogos.catalogoInstitucionExterna(Convert.ToInt32(@Request.Params["idDependenciaExterna"])), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }
        

        public JsonResult catalogoCertificaciones()
        {
            return Json(clsNegCatalogos.catalogoCertificaciones(), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        /*
        public JsonResult catalogoCertificacionesTree()
        {
            return Json(clsNegCatalogos.catalogoCertificacionesTree(), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }
        */

        public JsonResult catalogoInstitucionAplicaExamen()
        {
            return Json(clsNegCatalogos.catalogoInstitucionAplicaExamen(), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult catalogoLugarAplicacion()
        {
            return Json(clsNegCatalogos.catalogoLugarAplicacion(), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        public JsonResult consultarTemas()
        {
            return Json(clsNegCatalogos.consultarTemas(Convert.ToInt32(@Request.Params["idCertificacion"])), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        
        }


        public JsonResult consultarFunciones()
        {
            return Json(clsNegCatalogos.consultarFunciones(Convert.ToInt32(@Request.Params["idCertificacion"])), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);

        }

        public JsonResult consultarPreguntas()
        {
            return Json(clsNegCatalogos.consultarPreguntas(Convert.ToInt32(@Request.Params["idCertificacion"])), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);

        }

        public JsonResult consultarRespuestas()
        {
            return Json(clsNegCatalogos.consultarRespuestas(Convert.ToInt32(@Request.Params["idCertificacion"])), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);

        }

        public JsonResult consultarIntegrantes()
        {
            return Json(clsNegCatalogos.consultarIntegrantes(Convert.ToString(@Request.Params["empPaterno"]), Convert.ToString(@Request.Params["empMaterno"]), Convert.ToString(@Request.Params["empNombre"]), Convert.ToString(@Request.Params["empCURP"]), Convert.ToString(@Request.Params["empRFC"]), Convert.ToString(@Request.Params["empActivo"]), Convert.ToInt32(@Request.Params["empNumero"])), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);

        }

        public JsonResult consultarDatosIntegrantes()
        {
            //Guid idEmpleado = Guid.NewGuid(/*Convert.ToString(@Request.Params["idEmpleado"])*/);

            return Json(clsNegCatalogos.consultarDatosIntegrantes(Convert.ToString(@Request.Params["idEmpleado"])), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        public JsonResult catalogoZona()
        {
            return Json(clsNegCatalogos.consultarZona(), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        public JsonResult catalogoServicio()
        {
            return Json(clsNegCatalogos.catalogoServicio(Convert.ToInt32(@Request.Params["idZona"])), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        public JsonResult consultarDatosCorreo()
        {
            return Json(clsNegCatalogos.consultarDatosCorreo(), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }

        public JsonResult catalogoEntidadEvaluadora()
        {
            return Json(clsNegCatalogos.catalogoEntidadEvaluadora(), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }


        public JsonResult catalogoEntidadCertificadora()
        {
            return Json(clsNegCatalogos.catalogoEntidadCertificadora(), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }


        //Consulta y regresa un bit verdadero cuando la certificación ya fue contestada y por lo tanto no debería modificarse.
        public JsonResult consultarCalificacion(int idCertificacion)
        {
            return Json(clsNegCatalogos.consultarCalificacion(idCertificacion), "text/x-json", System.Text.Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }
    }
}
