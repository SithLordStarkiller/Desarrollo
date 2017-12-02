using GOB.SPF.ConecII.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GOB.SPF.ConecII.Web.Controllers
{
    [Authorize]
    public class FilesController : Controller
    {
        // GET: Files
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult UploadFile()
        {
            UiDocumento documento = new UiDocumento();
            
            HttpPostedFileBase file = (Request.Files.Count == 1) ? Request.Files[0] : Request.Files["files[]"];

            //if (file == null) { file = Request.Files["files[]"]; }
            //if (file == null) { file = Request.Files["documentoSolicitud"]; }
            //if (file == null) { file = Request.Files["Document"]; }
            string directory = Request.Form["Directory"];

            var result = "";
            bool isUploaded = false;
            string message = "Ningún archivo fue cargado.";

            if (file != null && file.ContentLength != 0)
            {
                try
                {
                    isUploaded = true;
                    message = "Archivo cargado satisfactoriamente!";
                    var path = CreatePath(directory);

                    documento.Nombre = file.FileName;
                    documento.IsActive = true;
                    documento.DocumentId = Guid.NewGuid();

                    file.SaveAs(Path.Combine(path, $"{documento.DocumentId}{Path.GetExtension(file.FileName)}"));
                }
                catch (Exception ex)
                {
                    result = string.Empty;
                    isUploaded = true;
                    message = $"La carga del archivo falló: {ex.Message}";
                }
            }

            return Json(new { isUploaded = isUploaded, message = message, documento = documento });
        }

        public FileStreamResult DownloadFile(UiDocumento documento)
        {
            string path = "";
            //if(documento.Identificador != 0)
            //{

            //    path = Server.MapPath($"~/{directory}/{id}/{Path.GetExtension(fileName)}");
            //}
            //path = Server.MapPath($"~/{directory}/{id}/{Path.GetExtension(fileName)}");
            var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);

            return new FileStreamResult(fileStream, "application/pdf");
        }

        private string CreatePath(string clientDirectory)
        {
            string pathResult = Server.MapPath($"~/TemporaryFiles/{clientDirectory}");

            if (!Directory.Exists(pathResult))
            {
                Directory.CreateDirectory(pathResult);
            }

            return pathResult;
        }
    }
}