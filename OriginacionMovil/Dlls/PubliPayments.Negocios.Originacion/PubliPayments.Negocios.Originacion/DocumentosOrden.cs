using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Org.BouncyCastle.Asn1;
using PubliPayments.Entidades;
using PubliPayments.Entidades.Originacion;
using PubliPayments.Entidades.Originacion.Modelos;
using PubliPayments.Utiles;

namespace PubliPayments.Negocios.Originacion
{
    public class DocumentosOrden
    {
        public readonly List<Documento> _lista= new List<Documento>();
        private readonly string _idOrden = "";
        List<Documento> _listaIncompletos = new List<Documento>();

        public DocumentosOrden()
        {
            
        }
        public DocumentosOrden(string idOrden)
        {
            _idOrden = idOrden;
            _lista = ObtenerDocumentosOrden();
        }

        private List<Documento> ObtenerDocumentosOrden()
        {
            var ds = EntDatosProspecto.ObtenerDocumentos(_idOrden);

            return (from DataRow row in ds.Tables[1].Rows
                    where row["TipoCampos"].ToString()=="2"
                    select new Documento
                    {
                        NombreDocumento = row["Titulo"].ToString(),
                        Fase = Convert.ToInt32(row["visitaCorresp"].ToString()), 
                        Cargado = (row["Valor"] != null && row["Valor"].ToString() != ""),
                        Ruta = (row["Valor"] != null && row["Valor"].ToString() != "" ? row["Valor"] : "").ToString()
                    }
            ).ToList();
        }

        /// <summary>
        /// Documentos o Fotos al archivo unificado
        /// </summary>
        /// <returns>Lista de documentos adicionales</returns>
        private List<Documento> ObtenerDocumentosOrdenDocUnificado()
        {
            var ds = EntDatosProspecto.ObtenerDocumentos(_idOrden);

            var anexoFotos = ConfigurationManager.AppSettings["AnexoFotos"];

            if(anexoFotos == null)
                return new List<Documento>();

            //var listAdd = new List<string> { "FotoDH_IDEOficial" };
            var listAdd = anexoFotos.Split('|');

            return (from DataRow row in ds.Tables[1].Rows
                    where row["TipoCampos"].ToString() == "1" && listAdd.Contains(row["Titulo"])
                    select new Documento
                    {
                        NombreDocumento = row["Titulo"].ToString(),
                        Fase = Convert.ToInt32(row["visitaCorresp"].ToString()),
                        Cargado = (row["Valor"] != null && row["Valor"].ToString() != ""),
                        Ruta = (row["Valor"] != null && row["Valor"].ToString() != "" ? row["Valor"] : "").ToString()
                    }
            ).ToList();
        }

        public Boolean DocumentosCompletos(int fase)
        {
            _listaIncompletos = (from Documento d in _lista
                where d.Cargado == false
                && d.Fase <= fase
                select d).ToList();

            return _listaIncompletos.Count == 0;
        }

        public string GenerarDocumentos(string NombreDocumento)
        {           
            string fullpath = "";
            string url = "";
            
            switch (NombreDocumento)
            {
                case "DocSolCredito":
                    Rutas(ref fullpath, ref url, "2", _idOrden.ToString(CultureInfo.InvariantCulture), "DocSolCredito");
                    var TaskGenerarDocSolCredFin = new Task(GenerarDocSolCredFin);
                    TaskGenerarDocSolCredFin.Start();
                    break;
                case "DocCarContrato":
                    Rutas(ref fullpath, ref url, "2", _idOrden.ToString(CultureInfo.InvariantCulture), "DocCarContrato");
                    var taskGenerarDocCarContrato = new Task(GenerarDocCarContrato);
                    taskGenerarDocCarContrato.Start();
                    break;
                case "DocBuroCredito":
                    Rutas(ref fullpath, ref url, "2", _idOrden.ToString(CultureInfo.InvariantCulture), "DocBuroCredito");
                    var taskGenerarDocBuroCredito = new Task(GenerarDocBuroCredito);
                    taskGenerarDocBuroCredito.Start();
                    break;
                case "DocAcuRecTarjeta":
                    Rutas(ref fullpath, ref url, "2", _idOrden.ToString(CultureInfo.InvariantCulture), "DocAcuRecTarjeta");
                    var taskGenerarDocAcuRecTarjeta = new Task(GenerarDocAcuRecTarjeta);
                    taskGenerarDocAcuRecTarjeta.Start();
                    break;

                case "DocPreventivo":
                    Rutas(ref fullpath, ref url, "2", _idOrden.ToString(CultureInfo.InvariantCulture), "DocPreventivo");
                    var taskGenerarDocPreventivo = new Task(GenerarDocPreventivo);
                    taskGenerarDocPreventivo.Start();
                    break;
                case "DocCartaSessionIrr":
                    Rutas(ref fullpath, ref url, "2", _idOrden.ToString(CultureInfo.InvariantCulture), "DocCartaSessionIrr");
                    var taskGenerarDocCartaSessionIrr = new Task(GenerarDocCartaSesionIrre);
                    taskGenerarDocCartaSessionIrr.Start();
                    break;
                case "DocUnificado":
                    Rutas(ref fullpath, ref url, "2", _idOrden.ToString(CultureInfo.InvariantCulture), "DocUnificado");
                    var taskGenerarDocUnificado = new Task(GenerarDocUnificado);
                    taskGenerarDocUnificado.Start();
                    break;
                
                default:
                    break;
            }
  //DocSolCredito DocCarContrato DocPreventivo DocCartaSessionIrr DocContrato DocAcuRecTarjeta DocBuroCredito
            return url;
        }

        public void Rutas(ref string fullpath, ref string url, string visita, string orden, string campo)
        {
            if (fullpath == null) throw new ArgumentNullException("fullpath");
            string directorioImagenes = ConfigurationManager.AppSettings["CWDirectorioImagenesOriginacion"];
            string urlmagenes = ConfigurationManager.AppSettings["CWDirectorioDocumentosOriginacionDescarga"];

            const string ext = "pdf";
            var fase = visita == "1" ? "Originacion" : (visita == "2" ? "Formalizacion" : "Preautorizacion");
            var path = directorioImagenes + orden + @"\" + fase;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            fullpath = path + @"\" + campo + "." + ext;
            url = urlmagenes + orden + "/" + fase + "/" + campo + "." + ext;

        }        
        
        private void GenerarDocPreventivo()
        {
            try
            {
                var url = "";
                var fullpath = "";
                Rutas(ref fullpath, ref url, "2", _idOrden.ToString(CultureInfo.InvariantCulture), "DocPreventivo");
                var model = DocumentoPreventivoDerhabienteModel.ObtenerDocPreventivoModel(int.Parse(_idOrden));
                var pdf = new GeneraPdf();
                var res = pdf.Upload(model, "2", Convert.ToInt32(_idOrden), "DocAcuRecTarjeta", url, fullpath);
                if (res == "-1")
                {
                    throw new Exception("Error no se puedo generar el archivo");
                }
            }
            catch (Exception ex)
            {
                var mensaje = "GenerarDocumentos - GenerarDocAcuRecTarjeta - " + ex.Message;
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "DocumentosOrden", mensaje);
            }
        }

        private void GenerarDocUnificado()
        {
            try
            {
                var todoDocs = true;
                var vuelta = 0;
                var rutaArcs = ConfigurationManager.AppSettings["CWDirectorioImagenesOriginacion"];
                var listDocs = new List<byte[]>();
                
                var url = "";
                var fullpath = "";

                Rutas(ref fullpath, ref url, "2", _idOrden.ToString(CultureInfo.InvariantCulture), "DocUnificado");

                //Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "UnificarDocs", "Orden : " + _idOrden + " DocsUnificar : " + documentosOrden._lista.Count);
                //Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "UnificarDocs", "Orden : " + _idOrden + " DocsUnificar : " + docsUnificados.Count);

                do
                {

                    var docsUnificados = ObtenerDocumentosLista();

                    if (docsUnificados.Count == 0)
                        throw new Exception("No hay documentos a unificar");
                    todoDocs = true;
                    vuelta++;
                    listDocs.Clear();
                    Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "UnificarDocs", "Orden : " + _idOrden + " Vuelta : " + vuelta);
                    foreach (var documento in docsUnificados)
                    {

                        Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "UnificarDocs", "Orden : " + _idOrden + " Documento : " + Newtonsoft.Json.JsonConvert.SerializeObject(documento));
                        
                        if(string.IsNullOrEmpty(documento.Ruta))
                        {
                            todoDocs = false;
                            Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "UnificarDocs", "Orden : " + _idOrden + " Ruta no encontrada : " + documento.NombreDocumento + " sin ruta");
                            break;
                        }

                        var rutaCorrecta = CorrecionUrl(documento.Ruta);

                        Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "UnificarDocs", "Orden : " + _idOrden + " Ruta Url " + rutaCorrecta);

                        var myUri = new Uri(rutaCorrecta, UriKind.Absolute);

                        var rutaOri = rutaArcs + @"\" + myUri.AbsolutePath;

                        Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "UnificarDocs",
                            "Orden : " + _idOrden + " RutaUni " + rutaOri);

                        if (!File.Exists(rutaOri))
                        {
                            todoDocs = false;
                            Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "UnificarDocs", "Orden : " + _idOrden + " Ruta no encontrada : " + rutaOri);
                            break;
                        }

                        Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "UnificarDocs", "Orden : " + _idOrden + " Ruta " + rutaOri + " Existe!!!");
                        byte[] bytes = myUri.AbsolutePath.Contains("Foto") ? CrearPdfPorFoto(rutaOri) : ReadBytes(rutaOri);
                        listDocs.Add(bytes);
                    }
                    Thread.Sleep(2000);
                } while (vuelta <= 5 && todoDocs == false);

                if (todoDocs)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "UnificarDocs", "Orden : " + _idOrden + " Unificando Archivo Ruta : " + fullpath);
                    File.WriteAllBytes(fullpath, MergeFiles(listDocs));
                    Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "UnificarDocs", "Orden : " + _idOrden + " Unificado Archivo Ruta : " + fullpath);
                }
                else
                    throw new Exception("Error no se encontraron todos los documentos y/o fotos");
            }
            catch (Exception ex)
            {
                var mensaje = "GenerarDocumentos - Error : " + ex.Message;
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "UnificarDocs", mensaje);   
                Email.EnviarEmail(new List<string> { "pablo.rendon@broxel.com" }, "Error documento unificado",
                        "Error al crear documento unificado orden : " + _idOrden, true);
            }
        }

        public byte[] ReadBytes(string myFile)
        {
            byte[] oFileBytes;
            using (var fs = File.Open(myFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                int numBytesToRead = Convert.ToInt32(fs.Length);
                oFileBytes = new byte[(numBytesToRead)];
                fs.Read(oFileBytes, 0, numBytesToRead);
            }
            return oFileBytes;
        }

        public string CorrecionUrl(string ruta)
        {
            string cadena;

            try
                {
                    var myUri = new Uri(ruta, UriKind.Absolute);
                    cadena = myUri.AbsoluteUri;
                }
                catch (Exception)
                {
                    var url = ruta.Replace(@"\", "/").Replace("//", "/");
                    var myUri = new Uri(url, UriKind.Absolute);
                    cadena = myUri.AbsoluteUri;
                }

            return cadena;
        }

        public List<Documento> ObtenerDocumentosLista()
        {
            var documentosOrden = new DocumentosOrden(_idOrden.ToString(CultureInfo.InvariantCulture));
            var docsAdd = documentosOrden.ObtenerDocumentosOrdenDocUnificado();

            var docsUnificados = documentosOrden._lista.Concat(docsAdd).ToList();
            return docsUnificados;
        }

        public static byte[] CrearPdfPorFoto(string ruta)
        {
            var streamMemory = new MemoryStream();
            var document = new Document(PageSize.A4, 3, 3, 15, 3);
            var pdfNuevo = PdfWriter.GetInstance(document, streamMemory);
            document.Open();
            
            var imagen = Image.GetInstance(ruta);
            imagen.Alignment = Element.ALIGN_MIDDLE;
            imagen.ScaleAbsoluteWidth(550);
            imagen.ScaleAbsoluteHeight(800);
            document.Add(imagen);
            
            document.Close();
            pdfNuevo.Close();
            return streamMemory.ToArray();
        }

        public static byte[] MergeFiles(List<byte[]> sourceFiles)
        {
            var document = new Document();
            using (var ms = new MemoryStream())
            {
                var copy = new PdfCopy(document, ms);
                document.Open();
                var documentPageCounter = 0;

                for (var fileCounter = 0; fileCounter < sourceFiles.Count; fileCounter++)
                {
                    var reader = new PdfReader(sourceFiles[fileCounter]);
                    var numberOfPages = reader.NumberOfPages;

                    for (var currentPageIndex = 1; currentPageIndex <= numberOfPages; currentPageIndex++)
                    {
                        documentPageCounter++;
                        var importedPage = copy.GetImportedPage(reader, currentPageIndex);
                        var pageStamp = copy.CreatePageStamp(importedPage);

                        //ColumnText.ShowTextAligned(pageStamp.GetOverContent(), Element.ALIGN_CENTER,
                        //    new Phrase("PDF Merger by Helvetic Solutions"), importedPage.Width / 2, importedPage.Height - 30,
                        //    importedPage.Width < importedPage.Height ? 0 : 1);

                        //ColumnText.ShowTextAligned(pageStamp.GetOverContent(), Element.ALIGN_CENTER,
                        //    new Phrase(String.Format("Page {0}", documentPageCounter)), importedPage.Width / 2, 30,
                        //    importedPage.Width < importedPage.Height ? 0 : 1);

                        pageStamp.AlterContents();

                        copy.AddPage(importedPage);
                    }

                    copy.FreeReader(reader);
                    reader.Close();
                }

                document.Close();
                return ms.GetBuffer();
            }
        }

        private void GenerarDocCartaSesionIrre()
        {
            try
            {
                var url = "";
                var fullpath = "";
                Rutas(ref fullpath, ref url, "2", _idOrden.ToString(CultureInfo.InvariantCulture), "DocCartaSessionIrr");
                var model = CartadeSesionIrrevocableModel.ObtenerCartadeSesionIrrevocable(int.Parse(_idOrden));
                var pdf = new GeneraPdf();
                var res = pdf.Upload(model, "2", Convert.ToInt32(_idOrden), "DocAcuRecTarjeta", url, fullpath);
                if (res == "-1")
                {
                    throw new Exception("Error no se puedo generar el archivo");
                }
            }
            catch (Exception ex)
            {
                var mensaje = "GenerarDocumentos - GenerarDocCartaSesionIrre - " + ex.Message;
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "DocumentosOrden", mensaje);
            }
        }

        private void GenerarDocAcuRecTarjeta()
        {
            try
            {
                var url = "";
                var fullpath = "";
                Rutas(ref fullpath, ref url, "2", _idOrden.ToString(CultureInfo.InvariantCulture), "DocAcuRecTarjeta");
                var model = ReciboTarjetaModel.ObtenerReciboTarjetaModel(int.Parse(_idOrden));
                var pdf = new GeneraPdf();
                var res = pdf.Upload(model, "2", Convert.ToInt32(_idOrden), "DocAcuRecTarjeta",url,fullpath);
                if (res == "-1")
                {
                    throw new Exception("Error no se puedo generar el archivo");
                }
            }
            catch (Exception ex)
            {
                var mensaje = "GenerarDocumentos - GenerarDocAcuRecTarjeta - " + ex.Message;
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "DocumentosOrden", mensaje);
            }
        }

        private void GenerarDocBuroCredito()
        {
            try
            {
                var url = "";
                var fullpath = "";
                Rutas(ref fullpath, ref url, "2", _idOrden.ToString(CultureInfo.InvariantCulture), "DocBuroCredito");

                var model = ConsultaBuroModel.ObtenerConsultaBuroModel(int.Parse(_idOrden));
                var pdf = new GeneraPdf();
                var res = pdf.Upload(model, "2", Convert.ToInt32(_idOrden), "DocBuroCredito",url,fullpath);
                if (res == "-1")
                {
                    throw new Exception("Error no se puedo generar el archivo");
                }
            }
            catch (Exception ex)
            {
                var mensaje = "GenerarDocumentos - GenerarDocBuroCredito - " + ex.Message;
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "DocumentosOrden", mensaje);
            }
        }

        private void GenerarDocCarContrato()
        {
            try
            {
                var url = "";
                var fullpath = "";
                Rutas(ref fullpath, ref url, "2", _idOrden.ToString(CultureInfo.InvariantCulture), "DocCarContrato");

                var model = CaratulaContratoModel.ObtenerCaratulaContratoModel(int.Parse(_idOrden));
                var pdf = new GeneraPdf();
                var res = pdf.Upload(model, "2", Convert.ToInt32(_idOrden), "DocCarContrato",url,fullpath);
                if (res == "-1")
                {
                    throw new Exception("Error no se puedo generar el archivo");
                }
            }
            catch (Exception ex)
            {
                var mensaje = "GenerarDocumentos - GenerarDocCarContrato - " + ex.Message;
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "DocumentosOrden", mensaje);
            }
        }

        private void GenerarDocSolCredFin()
        {
            try
            {
                var fullpath = "";
                var url = "";
                Rutas(ref fullpath, ref url, "2", _idOrden.ToString(CultureInfo.InvariantCulture), "DocSolCredito");

                var model = SolicitudInscripcionCreditoModel.ObtenerSolicitudInscripcionCredito(int.Parse(_idOrden));
                var pdf = new GeneraPdf();
                var res = pdf.Upload(model, "2", Convert.ToInt32(_idOrden), "DocSolCredito", url, fullpath);
                if (res == "-1")
                {
                    throw new Exception(" Error no se puedo generar el archivo");
                }
            }
            catch (Exception ex)
            {
                var mensaje = "GenerarDocumentos - GenerarDocSolCredFin - " + ex.Message;
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "DocumentosOrden", mensaje);
            }
            
        }

        public string GenerarDocSolicitudCreditoSimple(string json)
        {
            //var r = new StreamReader(streamuser).ReadToEnd();
            var result = Task.Run(() => GenerarDocSolCredSimple(json));
            return result.Result;            
        }

        public string GenerarDocSolicitudCreditoSimple(SolicitudInscripcionCreditoSimpleModel model)
        {
            var result = Task.Run(() => GenerarDocSolCredSimple(model));
            return result.Result;
        }

        private string GenerarDocSolCredSimple(string json)
        {            
            var model = SolicitudInscripcionCreditoSimpleModel.LlenarModelo(json);
            return GenerarDocSolCredSimple(model);
        }

        private string GenerarDocSolCredSimple(SolicitudInscripcionCreditoSimpleModel model)
        {
            var id = Guid.NewGuid().ToString().Split('-').FirstOrDefault();
            string nombrePdf = string.Format("SolInscripcionCredito-{0}-{1:yyyy-MM-dd_hh-mm-ss-tt}", id, DateTime.UtcNow);
            nombrePdf = nombrePdf + ".pdf";
            try
            {
                //string directorioImagenes = @"C:\\Users\\giovanni.crescencio\\Documents\\tmp\\docs01800\\"; 
                //string directorioImagenes = ConfigurationManager.AppSettings["CWDirectorioImagenesOriginacion"];
                //string urlmagenes = ConfigurationManager.AppSettings["CWDirectorioDocumentosOriginacionDescarga"];


                string fullPathDocsSolCred = ConfigurationManager.AppSettings["DirectorioDocsSolicitudCredito"]; //@"\DocsSolicitudCredito\";
                string urlDocsSolCred = ConfigurationManager.AppSettings["urlDocsSolCred"];

                var fullpath = fullPathDocsSolCred + @"\" + nombrePdf;
                var url = urlDocsSolCred + @"/" + nombrePdf;
                //Rutas(ref fullpath, ref url, "2", _idOrden.ToString(CultureInfo.InvariantCulture), "DocSolCredito");
                //ConfigurationManager.AppSettings["CWDirectorioDocumentosOriginacionDescarga"]

                
                var pdf = new GeneraPdf();
                var res = pdf.Upload(model, url, fullpath);                
                if (res == "-1")                
                    throw new Exception(" Error no se puedo generar el archivo");
                return res;
            }
            catch (Exception ex)
            {
                var mensaje = "GenerarDocumentos - GenerarDocSolCredSimple - " + ex.Message;
                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "DocumentosOrden", mensaje);
                return "-1";
            }           
        }
    }
}
