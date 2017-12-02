using GOB.SPF.ConecII.Business;
using GOB.SPF.ConecII.Entities;
using GOB.SPF.ConecII.Entities.Amatzin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace GOB.SPF.Conec.Services.Controllers
{
    [RoutePrefix("api/Firma")]
    public class RequesFirmaController : ApiController
    {
        //private static readonly string ServerUploadFolder = "C:\\Temp"; //Path.GetTempPath();

        //[HttpPost]
        //[Route("RequesFirmaControl/FirmaDocumento")]
        //public IHttpActionResult ObtenerCadenaFirmada(FirmaDocumento firmaDoc, string strCadenaOriginal, string strRazon)
        //{
        //    string cadenaFirmada = string.Empty;
        //    Firma firma = new Firma(firmaDoc);
        //    try
        //    {
        //        if (!string.IsNullOrWhiteSpace(firma.ValidarArchivos()))
        //        {
        //            cadenaFirmada = firma.FirmarCadena(ref strCadenaOriginal, strRazon);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
        //    }

        //    return Ok(cadenaFirmada);
        //}

        [HttpPost]
        [Route("RequesFirmaControl/FirmaDocumento")]
        public IHttpActionResult ObtenerCadenaFirmada(string strCadenaOriginal, string strRazon)
        {
            var documentos = UploadFiles();
            FirmaDocumento firmaDoc = new FirmaDocumento();

            if (documentos.Count == 2)
            {
                 firmaDoc.DocumentoKey = documentos.Find(s => s.Extension == "key");
                 firmaDoc.DocumentoCer = documentos.Find(s => s.Extension == "cer");
            }else
            {
                EventLog.WriteEntry("ConecService", "No son el número de archivos requeridos.", EventLogEntryType.Error);
            }

            string cadenaFirmada = string.Empty;

            Firma firma = new Firma(firmaDoc);
            try
            {
                if (!string.IsNullOrWhiteSpace(firma.ValidarArchivos()))
                {
                    cadenaFirmada = firma.FirmarCadena(ref strCadenaOriginal, strRazon);
                }
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("ConecService", $"{e.Message}\n{e.StackTrace}", EventLogEntryType.Error);
            }

            return Ok(cadenaFirmada);
        }

        List<Documento> UploadFiles()
        {       
            var httpRequest = HttpContext.Current.Request;

            List<Documento> documentos = new List<Documento>();

            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();

                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];

                    string extension = postedFile.FileName.Substring(postedFile.FileName.IndexOf('.') + 1);
                    extension = extension.ToLower();
                   
                    string fileName = postedFile.FileName.Remove(postedFile.FileName.IndexOf('.'));

                    if (extension.Equals("key"))
                    {
                        documentos.Add(new Documento
                        {
                            Directorio = HttpContext.Current.Server.MapPath("~/"),
                            Extension = "key",
                            Nombre = fileName
                        });
                    } 
                    else if(extension.Equals("cer"))
                    {
                        documentos.Add(new Documento
                        {
                            Directorio = HttpContext.Current.Server.MapPath("~/"),
                            Extension = "cer",
                            Nombre = fileName
                        });
                    }

                    var filePath = HttpContext.Current.Server.MapPath("~/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);

                    docfiles.Add(filePath);

                }      

            }
            return documentos;

        }

    }
}
