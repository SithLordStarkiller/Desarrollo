using System;
using System.Data;
using System.IO;
using PubliPayments.Negocios;
using PubliPayments.Entidades;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PubliPayments.Utiles;
using  PubliPayments.Models;

namespace PubliPayments.Controllers
{
    public class LlamadasNoExitosasController : Controller
    {
        protected int idUsuario = Int32.Parse(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
        protected string RolActual;
        //
        // GET: /LlamadasNoExitosas/

        public ActionResult Index(string de)
        {
            RolActual = SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol);
            if (RolActual == "0" || RolActual == "1" || RolActual == "2")
            {
                return View(); 
            }
            else
            {
                Response.Redirect("unauthorized.aspx");
                return null;
                
            }
        }

        public ActionResult CargaDocumento()
        {
            return PartialView("CargaDoc");
        }

        [HttpPost]
        public ActionResult Upload()
        {
            var archivo = new LlamadasSinExitos();

            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                var fileName = Path.GetFileName(file.FileName);
                var fileExtencion = Path.GetExtension(file.FileName).ToUpper();

                if (file != null && file.ContentLength > 0)
                {
                    if (fileExtencion == ".TXT")
                    {
                        string mensaje = archivo.LeeArchivoLLamadas(idUsuario, fileName, new StreamReader(file.InputStream));

                        TempData["notice"] = mensaje.Replace("<br>", "\n");
                    }
                    else
                    {
                        TempData["notice"] = "El formato del archivo no es válido";
                    }

                }
                else
                {
                    TempData["notice"] = "No se seleccionó ningún archivo";
                }
            }
            

            return  RedirectToAction("Index","LlamadasNoExitosas");
        }

        public ActionResult NextPage(string tipo)
        {
            var mod = GridBind().Where(f => f.Tipo.Equals(tipo)).ToList();

            RolActual = SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol);
            if (RolActual == "0" || RolActual == "1")
            {
                return PartialView("TablaArchivosLlamadaAdmin", mod);
                
            }
            else
            {
                return PartialView("TablaArchivosLlamada", mod);
            }
        }

        private List<ArchivosModel> GridBind()
        {
            int idUsuario = Int32.Parse(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            var dsArchivos = new DataSet();
            var archivosNegocio = new Negocios.ArchivosN();
            var archivos = new List<ArchivosModel>();

            dsArchivos = archivosNegocio.ObtenerArchivos(idUsuario, 2);

            archivos = dsArchivos.Tables[0].ToList<ArchivosModel>();

            return archivos;
        }
    }
}
