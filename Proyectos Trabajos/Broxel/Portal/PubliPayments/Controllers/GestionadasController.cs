using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Windows.Documents;
using Ionic.Zip;
using Newtonsoft.Json;
using PubliPayments.Entidades;
using PubliPayments.Entidades.MYO;
using Publipayments.Negocios.MYO;
using PubliPayments.Utiles;

namespace PubliPayments.Controllers
{
    public class GestionadasController : Controller
    {
        
        public ActionResult Index(TipoGestionada tipo = TipoGestionada.AutorizarRechazar)
        {
            if (User.IsInRole("0") || User.IsInRole("3") || User.IsInRole("4"))
            {
                ViewBag.idAplicacion = Config.AplicacionActual().idAplicacion;

                ViewBag.isOriginacion = Config.AplicacionActual().Nombre.ToUpper().Contains("ORIGINACIONMOVIL") || 
                                        Config.AplicacionActual().Nombre.ToUpper().Contains("SIRA") || 
                                        Config.AplicacionActual().Nombre.ToUpper().Contains("MYO");

                string nombreAplicacion= Config.AplicacionActual().Nombre.ToUpper();
                ViewBag.nombreAplicacion = nombreAplicacion;
                
                if (!ViewBag.isOriginacion) //Todo:Por el momento se bloquea para las demas aplicaciones
                    return Redirect("/unauthorized.aspx");

                if (Session["Respuesta"] != null)
                {
                    string[] respuestas = Session["Respuesta"].ToString().Split('|');
                    Session.Remove("Respuesta");
                    ViewBag.MensajeResultado = respuestas[0];
                }
                Session["Refresh"] =  !nombreAplicacion.Contains("Sira");
                Session["TipoGestionada"] = Request.QueryString["tipo"] ?? "Default";
                ViewBag.idUsuarioPadre = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
                ViewBag.Tipo = tipo.ToString();
                var firstOrDefault = Config.AplicacionActual().Formulario.FirstOrDefault();
                if (firstOrDefault != null)
                {
                    var ruta = firstOrDefault.Ruta;
                    if (ruta != null)
                        ViewBag.Ruta = ruta;
                }
            
                return View();
            }
            return Redirect("/unauthorized.aspx");
        }


        [ValidateInput(false)]
        public ActionResult GridGestionadasPartial()
        {
            //Para manejar los postbacks
            if (Session["Refresh"] != null)
            {
                Session.Remove("Refresh");
            }
            
            string estatus;

            TipoGestionada tipo;
            if (Session["TipoGestionada"] != null)
                Enum.TryParse(Session["TipoGestionada"] == null ? "Default" : Session["TipoGestionada"].ToString(), true,
                    out tipo);
            else
                Enum.TryParse(Request.QueryString["tipo"] ?? "Default", true, out tipo);
            switch (tipo)
            {
                case TipoGestionada.Autorizadas:
                    estatus = "4";
                    break;
                case TipoGestionada.AutorizarRechazar:
                    estatus = "3";
                    break;
                case TipoGestionada.Rechazadas:
                    estatus = "2";
                    break;
                default:
                    estatus = "3,4";
                   break;
            }


           
            if (Config.AplicacionActual().Nombre.ToUpper().Contains("SIRA"))
                ViewData["Sira"] = 1;
            string ruta = "";
            ViewData["VerArchivos"] = false;
            var firstOrDefault = Config.AplicacionActual().Formulario.FirstOrDefault();
            if (firstOrDefault != null)
            {
                 ruta = firstOrDefault.Ruta;
            }
            if (Config.AplicacionActual().Nombre.ToUpper().Contains("ORIGINACIONMOVIL"))
            {
                ViewData["VerArchivos"] = true;
                ruta = "Formaliza";
            }
            if (Config.AplicacionActual().Nombre.ToUpper().Contains("MYO"))
            {
                ViewData["VerArchivos"] = true;
                ruta = "MYO";
            }
            var model = Config.AplicacionActual().Nombre.ToUpper().Contains("SIRA") ? new EntGestionadas().ObtenerRespuestasSira(0, estatus, ruta) : Config.AplicacionActual().Nombre.ToUpper().Contains("ORIGINACIONMOVIL") ? new EntGestionadas().ObtenerRespuestasOriginacion(0, estatus, ruta) : new EntGestionadas().ObtenerRespuestasMYO(Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario)), estatus, ruta);
            ViewData["datosColumnas"]=(from string dato in model.Tables[0].Rows[0].ItemArray select dato.Split(',')).ToArray();
            return PartialView("_GridGestionadasPartial", model.Tables[1]);
        }



        public ActionResult SetRefresh(string valor)
        {
            if (User.IsInRole("0") || User.IsInRole("3"))
            {
                ViewBag.idAplicacion = Config.AplicacionActual().idAplicacion;
                ViewBag.isOriginacion = Config.AplicacionActual().Nombre.Contains("OriginacionMovil");

                if (!ViewBag.isOriginacion) //Todo:Por el momento se bloquea para las demas aplicaciones
                    return Redirect("/unauthorized.aspx");

                Session["Refresh"] = Config.AplicacionActual().Nombre.Contains("OriginacionMovil");
                Session["TipoGestionada"] = valor;
                return Content("OK");
            }
            return Content("Error");
        }

        /// <summary>
        /// Obtiene archivos de originacion, los monta al layout y regresa json para popUp
        /// </summary>
        /// <param name="idOrden"></param>
        /// <returns>Json con la pantalla que se mostrara en el popUp</returns>
        public ActionResult VerArchivos(String idOrden)
        {
            string ordenHtml;
            var credito = "";
            var etiqueta = "";
            var idVisita = 99;
            var estatus = 1;
            try
            {
                var ds = new EntGestionadas().ObtenerDocumentosOrden(idOrden,Config.AplicacionActual().Nombre);
                //var archivosADescargar = from myRow in ds.Tables[1].AsEnumerable()
                //                         where myRow.Field<string>("Valor") != null && myRow.Field<string>("Valor").StartsWith("https:")
                //                         select myRow;

                //foreach (var row in archivosADescargar)
                //{
                //    DescargaImagenOrden(row["idOrden"].ToString(), row["Valor"].ToString(), row["Titulo"].ToString(),
                //        row["visitaCorresp"].ToString(), row["idCampo"].ToString());
                //}

                //ds = new EntGestionadas().ObtenerDocumentosOrden(idOrden);
                credito = ds.Tables[0].Rows[0][0].ToString();
                
                idVisita = Convert.ToInt32(ds.Tables[0].Rows[0][1].ToString());
                estatus = Convert.ToInt32(ds.Tables[0].Rows[0][2].ToString());
                etiqueta = ds.Tables[0].Rows[0][3].ToString();
                ViewData["idVisita"] = idVisita;
                var html = new StringBuilder();
                html.Append("<div id='archivosFotosOrden' style='width:100%;'>");
                html.Append(RenderActionResultToString(TabsArchivos(idOrden,idVisita, ds.Tables[1])));
                html.Append("</div>");
                html.Append("<div id='archivosFotosOrdenBotones' style='width:100%;margin-top:15px;margin-bottom:15px;text-align:right;'>");
                html.Append("<input type='button' value='Descargar' class='Botones' style='float:left;height:45px;width:130px;' onclick='descargaDocumentos("+idOrden+");' />");
                if (estatus==3)
                {
                   
                    if (!Config.AplicacionActual().Nombre.ToUpper().Contains("MYO"))
                    {
                        html.Append(
                       "<input type='button' value='Rechazar' class='Botones' style='position:relative;margin-right:10px;height:45px;width:130px;' onclick='rechazarOrig(" +
                       idOrden + "," + idVisita + ");' />");
                        html.Append(
                            "<input type='button' value='Reenviar Incompletos' class='Botones' style='position:relative;margin-right:10px;height:45px;width:150px;' onclick='reenviaIncompletos(" +
                            idOrden + "," + idVisita + ");' />");
                        html.Append(
                        "<input type='button' value='Autorizar' class='Botones' style='position:relative;margin-right:10px;height:45px;width:130px;' onclick='autorizaOrig(" +
                        idOrden + "," + idVisita + ");' />");
                    }
                    if (Config.AplicacionActual().Nombre.ToUpper().Contains("MYO"))
                    {
                        if (etiqueta.ToUpper()=="ACREDITADO")
                        {
                            if (idVisita == 1 || idVisita == 3)
                            {
                                html.Append(
                                    "<input type='button' value='Continuar' class='Botones' style='position:relative;margin-right:10px;height:45px;width:130px;' onclick='guardaMC1(" +
                                    idOrden + "," + idVisita + ");' />");
                            }

                            if (idVisita == 2)
                            {
                                //html.Append(
                                //    "<input type='button' value='Continuar' class='Botones' style='position:relative;margin-right:10px;height:45px;width:130px;' onclick='guardaReferencias(" +
                                //    idOrden + "," + idVisita + ");' />");
                                return RedirectToAction("Gestionar", "CapturaWeb", new {credito });
                            }
                        }
                        else
                        {
                            if (idVisita == 1 )
                            {
                                html.Append(
                                    "<input type='button' value='Continuar' class='Botones' style='position:relative;margin-right:10px;height:45px;width:130px;' onclick='guardaMC1(" +
                                    idOrden + "," + idVisita + ");' />");
                            }

                            if (idVisita == 4 || idVisita == 5)
                            {
                                html.Append(
                                    "<input type='button' value='Continuar' class='Botones' style='position:relative;margin-right:10px;height:45px;width:130px;' onclick='guardaRevision(" +
                                    idOrden + "," + idVisita + ");' />");
                            }
                        }

                    }
                }
                html.Append("</div>");
                ordenHtml = html.ToString();

                credito = ds.Tables[0].Rows[0][0].ToString();
                idVisita = Convert.ToInt32(ds.Tables[0].Rows[0][1].ToString());
                
            }
            catch (Exception ex)
            {
                int idUsuario = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
                Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuario, "Gestionadas/VerArchivos", "Error: " + ex.Message);
                ordenHtml="<div>No se encuentra  el registro</div>";
            }


            return Json(new { usuario = credito, HtmlResp = ordenHtml, orden = idOrden, visita = idVisita }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Borra imagen de la tabla Respuesta y envia el archivo a carpeta bitacora
        /// </summary>
        /// <param name="idOrden"></param>
        /// <param name="fotosABorrar">nombre de la foto a borrar</param>
        /// <param name="rutasABorrar">ruta de foto que se borrara</param>
        /// <param name="visita">fase a la cual pertenece la imagen</param>
        /// <returns></returns>
        public ActionResult BorrarImagen(int idOrden, string fotosABorrar, string rutasABorrar,string visita)
        {
            var idUsuarioAlta = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            new EntGestionadas().BorrarImagen(idOrden,fotosABorrar);
            var fotos = rutasABorrar.Replace("'", "").Split(',');

            foreach (var foto in fotos)
            {
                try
                {
                    string directorioBitacoraImagenes = ConfigurationManager.AppSettings["CWDirectorioBitacoraImagenesOriginacion"];
                    string directorioImagenes = ConfigurationManager.AppSettings["CWDirectorioImagenesOriginacion"];
                    
                    var imagen = foto.Split('/');
                    var imagenFinal = imagen[imagen.Length - 1];
                    var fecha = DateTime.Now.Year + DateTime.Now.Month.ToString(CultureInfo.InvariantCulture) + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute;

                    var fase = visita == "1" ? "Originacion" : (visita == "2" ? "Formalizacion" : "Preautorizacion");
                    var pathBitacora = directorioBitacoraImagenes + idOrden + @"\" + fase;
                    var path = directorioImagenes + idOrden + @"\" + fase;
                    var fullpathBitacora = pathBitacora + @"\" +fecha+ imagenFinal;
                    var fullpath = path + @"\" + imagenFinal;


                    if (!Directory.Exists(pathBitacora))
                        Directory.CreateDirectory(pathBitacora);
                    System.IO.File.Move(fullpath, fullpathBitacora);

                    Logger.WriteLine(Logger.TipoTraceLog.Trace, idUsuarioAlta, "Gestionadas",
                        "BorrarImagen - OK: " + foto);
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuarioAlta, "Gestionadas", "BorrarImagen - Error:" + ex.Message);
                    return Content("Ocurrió un error al intentar borrar el achivo. Intente nuevamente mas tarde. Si el problema continua intente recargar la página, disculpe las molestias que esto le pueda ocasionar.");
                }
            }

            return Json(new {orden = idOrden }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Borra documento de la tabla Respuesta y envia el archivo a carpeta bitacora
        /// </summary>
        /// <param name="idOrden"></param>
        /// <param name="campo">campo que se eliminara de la tabla</param>
        /// <param name="visita">fase a la que pertenece</param>
        /// <returns></returns>
        public ActionResult BorrarArchivo(int idOrden, string campo,string visita)
        {
            var idUsuarioAlta = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            try
            {
                const string ext = "pdf";
                string directorioImagenes = ConfigurationManager.AppSettings["CWDirectorioImagenesOriginacion"];
                string directorioBitacoraImagenes = ConfigurationManager.AppSettings["CWDirectorioBitacoraImagenesOriginacion"];

                var fecha = DateTime.Now.Year + DateTime.Now.Month.ToString(CultureInfo.InvariantCulture) + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute;
                var fase = visita == "1" ? "Originacion" : (visita == "2" ? "Formalizacion" : "Preautorizacion");
                var path = directorioImagenes + idOrden + @"\" + fase;
                var pathBitacora = directorioBitacoraImagenes + idOrden + @"\" + fase;
                var fullpath = path + @"\" + campo + "." + ext;
                var fullpathBitacora = pathBitacora + @"\" +fecha+ campo + "." + ext;
                var url = "https://imagenes.01800pagos.com/" + idOrden + "/" + campo + "." + ext;


                if (!Directory.Exists(pathBitacora))
                    Directory.CreateDirectory(pathBitacora);
                new EntGestionadas().BorrarDocumento(idOrden, campo);
                System.IO.File.Move(fullpath,fullpathBitacora);
                

                Logger.WriteLine(Logger.TipoTraceLog.Trace, idUsuarioAlta, "Gestionadas","BorrarArchivo - OK: " + fullpath + " - url:" + url);

                return Content("OK|" + url);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuarioAlta, "Gestionadas", "BorrarArchivo - Error:" + ex.Message);
                return Content("Ocurrió un error al intentar borrar el achivo. Intente nuevamente mas tarde. Si el problema continua intente recargar la página, disculpe las molestias que esto le pueda ocasionar.");
            }
        }

        public ActionResult TabsArchivos(string idOrden,int idVisita,DataTable documentos)
        {
            ViewBag.idVisita = idVisita;
            ViewBag.idOrden = idOrden;
            ViewBag.Aplicacion = Config.AplicacionActual().Nombre.ToUpper();
            if (Config.AplicacionActual().Nombre.ToUpper().Contains("MYO"))
            {
                return PartialView("tabsArchivosMyo", documentos);   
            }
            else
            {
                return PartialView("tabsArchivos", documentos);
            }
        }

        public ActionResult Upload(int orden, string campo, string contenido, string visita)
        {
            var idUsuarioAlta = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
        
            Logger.WriteLine(Logger.TipoTraceLog.Trace, idUsuarioAlta, "Gestionadas",
                string.Format("Upload - orden={0},campo={1},Len(contenido)={2}", orden, campo, contenido.Length));

            string directorioImagenes = ConfigurationManager.AppSettings["CWDirectorioImagenesOriginacion"];
            string urlmagenes = ConfigurationManager.AppSettings["CWDirectorioDocumentosOriginacionDescarga"];

            if (contenido.StartsWith("data:image/jpeg;base64,"))
            {
                try
                {
                    const string ext = "jpg";
                    var archivo = contenido.Replace("data:image/jpeg;base64,", "");
                    byte[] bytes = Convert.FromBase64String(archivo);
                    var fase = visita == "1" ? "Originacion" : (visita == "2" ? "Formalizacion" : "Preautorizacion");
                    var path = directorioImagenes + orden + @"\" + fase;
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    var fullpath = path + @"\" + campo + "." + ext;
                    //var url = "https://imagenes.01800pagos.com/" + orden + "/" + fase + "/" + campo + "." + ext;
                    var url = urlmagenes + orden + "/" + fase + "/" + campo + "." + ext;

                    System.IO.File.WriteAllBytes(fullpath, bytes);

                    new EntGestionadas().AgregarDocumentoOrden(orden, campo, url);

                    Logger.WriteLine(Logger.TipoTraceLog.Trace, idUsuarioAlta, "Gestionadas",
                        "Upload - OK: " + fullpath + " - url:" + url);

                    return Content("OK|" + url);
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuarioAlta, "Gestionadas",
                        "Upload - Error:" + ex.Message);
                    return
                        Content(
                            "Ocurrió un error al intentar subir el achivo. Intente nuevamente mas tarde. Si el problema continua intente recargar la página, disculpe las molestias que esto le pueda ocasionar.");
                }
            }
            
            return Content("Formato de archivo no reconocido");
        }

        public ActionResult UploadMYO(int orden, string campo, string contenido, string visita,string ext)
        {
            var idUsuarioAlta = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));

            Logger.WriteLine(Logger.TipoTraceLog.Trace, idUsuarioAlta, "Gestionadas",
                string.Format("UploadMYO - orden={0},campo={1},Len(contenido)={2}", orden, campo, contenido.Length));

            string directorioImagenes = ConfigurationManager.AppSettings["CWDirectorioImagenesMYO"];
            string urlmagenes = ConfigurationManager.AppSettings["CWDirectorioDocumentosMYODescarga"];


            if (contenido.StartsWith("data:image/jpeg;base64,") || contenido.StartsWith("data:application/pdf;base64,") || contenido.StartsWith("data:image/jpg;base64,") || contenido.StartsWith("data:image/gif;base64,") || contenido.StartsWith("data:image/png;base64,"))
            {
                try
                {
                    var archivo = contenido.Replace("data:application/pdf;base64,", "").Replace("data:image/jpeg;base64,", "").Replace("data:image/jpg;base64,", "").Replace("data:image/gif;base64,", "").Replace("data:image/png;base64,", "");
                    byte[] bytes = Convert.FromBase64String(archivo);
                    var path = directorioImagenes + orden;
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    var fullpath = path + @"\" + campo + ext;
                    var url = urlmagenes + orden + "/" + campo  + ext;

                    System.IO.File.WriteAllBytes(fullpath, bytes);

                    new EntGestionadas().AgregarDocumentoOrden(orden, campo, url);

                    Logger.WriteLine(Logger.TipoTraceLog.Trace, idUsuarioAlta, "Gestionadas",
                        "UploadMYO - OK: " + fullpath + " - url:" + url);

                    return Content("OK|" + url);
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuarioAlta, "Gestionadas",
                        "UploadMYO - Error:" + ex.Message);
                    return
                        Content(
                            "Ocurrió un error al intentar subir el achivo. Intente nuevamente mas tarde. Si el problema continua intente recargar la página, disculpe las molestias que esto le pueda ocasionar.");
                }
            }

            return Content("Formato de archivo no reconocido");
        }

        public ActionResult ArchivosPartial()
        {
            return PartialView();
        }

        public ActionResult ArchivosPartialComentarios()
        {
            return PartialView();
        }

        public ActionResult Asignacion(int idOrden=0)
        {
            if (idOrden == 0)
            {
                return PartialView();
            }
            else
            {
                var sql = "exec BorradoSMS " +
                          "@idOrden="+idOrden;
                var conexion = ConexionSql.Instance;
                var cnn = conexion.IniciaConexion();
                var sc = new SqlCommand(sql, cnn);
                var sda = new SqlDataAdapter(sc);
                var ds = new DataSet();
                sda.Fill(ds);
                conexion.CierraConexion(cnn);
                
                return Content("Exito");
            }
        }

        public ActionResult GridAsignacion()
        {
            var sql = "select idOrden,Estatus,idVisita from Ordenes where Estatus=3";
            var conexion = ConexionSql.Instance;
            var cnn = conexion.IniciaConexion();
            var sc = new SqlCommand(sql, cnn);
            var sda = new SqlDataAdapter(sc);
            var ds = new DataSet();
            sda.Fill(ds);
            conexion.CierraConexion(cnn);

            return PartialView(ds.Tables[0]);
        }

        private string RenderActionResultToString(ActionResult result)
        {
            // Create memory writer.
            var sb = new StringBuilder();
            var memWriter = new StringWriter(sb);

            // Create fake http context to render the view.
            var fakeResponse = new HttpResponse(memWriter);
            var fakeContext = new HttpContext(System.Web.HttpContext.Current.Request,
                fakeResponse);
            var fakeControllerContext = new ControllerContext(
                new HttpContextWrapper(fakeContext),
                ControllerContext.RouteData,
                ControllerContext.Controller);
            var oldContext = System.Web.HttpContext.Current;
            System.Web.HttpContext.Current = fakeContext;

            // Render the view.
            result.ExecuteResult(fakeControllerContext);

            // Restore old context.
            System.Web.HttpContext.Current = oldContext;

            // Flush memory and return output.
            memWriter.Flush();
            return sb.ToString();
        }

        private static void DescargaImagenOrden(string orden, string url,string nombre,string visita,string campo)
        {
            var idUsuarioAlta = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            string directorioImagenes = ConfigurationManager.AppSettings["CWDirectorioImagenesOriginacion"];
            try
            {
                var uri = new Uri(url);

                var filename = Path.GetFileName(uri.LocalPath);
                var fase = visita == "1" ? "Originacion" : (visita == "2" ? "Formalizacion" : "Preautorizacion");
                var path = directorioImagenes + orden + @"\" + fase;
                var url1 = "https://imagenes.01800pagos.com/" + orden + "/" + fase ;

                using (var client = new WebClient())
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    if (!System.IO.File.Exists(path+ @"\" + filename))
                    {
                        client.DownloadFile(url, path + @"\" + filename);
                        Logger.WriteLine(Logger.TipoTraceLog.Log, idUsuarioAlta, "Gestionadas","DescargaImagenOrden - Imagen Descargada - "+orden+" - " + url);
                        System.IO.File.Move(path + @"\" + filename, path + @"\" + nombre + Path.GetExtension(path + @"\" + filename));
                        new EntGestionadas().ActualizaRutaImagenes(orden, campo,(path + @"\" + nombre + Path.GetExtension(path + @"\" + filename)));
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuarioAlta, "Gestionadas", "DescargaImagenOrden - Error:" + ex.Message);
            }

        }

        public String DescargarDocumentos(string idOrden)
        {
            var directorioImagenes = ConfigurationManager.AppSettings["CWDirectorioImagenesOriginacion"] ?? "";
            var directorioComp = ConfigurationManager.AppSettings["CWDirectorioComprimidosImagenesOriginacion"] ?? "";
            var directorioCompFinal = ConfigurationManager.AppSettings["CWDirectorioComprimidosOriginacionDescarga"] ?? "";

            var ruta = idOrden + "_" + DateTime.Now.Year +
                DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + ".zip";  
            var pathComp = directorioComp + ruta;
            var pathCompFinal = directorioCompFinal + ruta;  
            var path = directorioImagenes + idOrden ;
            using (ZipFile zip = new ZipFile())
            {
                zip.AddDirectory(path);
                zip.Save(pathComp);
            }
            return pathCompFinal;
        }

        public String DescargarDocumentosMyo(string idOrden)
        {
            var directorioImagenes = ConfigurationManager.AppSettings["CWDirectorioImagenesMYO"] ?? "";
            var directorioComp = ConfigurationManager.AppSettings["CWDirectorioComprimidosImagenesMYO"] ?? "";
            var directorioCompFinal = ConfigurationManager.AppSettings["CWDirectorioComprimidosMYODescarga"] ?? "";

            var ruta = idOrden + "_" + DateTime.Now.Year +
                DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + ".zip";
            var pathComp = directorioComp + ruta;
            var pathCompFinal = directorioCompFinal + ruta;
            var path = directorioImagenes + idOrden;
            using (ZipFile zip = new ZipFile())
            {
                zip.AddDirectory(path);
                zip.Save(pathComp);
            }
            return pathCompFinal;
        }

        public string EncriptaTc(string idOrden)
        {
            var cifrada = new EntGestionadas().ObtenerCifradoTc(idOrden);

            var cadena=Encoding.UTF8.GetString(FromBase32String(cifrada));

            return cadena.Substring(6);
        }

        public string ValidaMyo(string tipo,List<string> valores,int idOrden)
        {
            var nombreCorto=SessionUsuario.ObtenerDato(SessionUsuarioDato.NomCorto);
            var result="Ocurrio un error al intentar guardar. Por favor intente de nuevo.";

            switch (tipo)
            {
                case "Documentos":
                    {
                        var documentos = valores.Select(JsonConvert.DeserializeObject<DocumentosModel>).ToList();
                        var resultDoc = ValidaDocumentos.DocumentosValidas(documentos, ConfigurationManager.AppSettings["documentosRechazo"] ?? "", ConfigurationManager.AppSettings["documentosNoObligatorios"] ?? "");

                        if (resultDoc.Valido)
                        {
                            if (ValidaDocumentos.GuardaDocumentos(idOrden, documentos))
                            {
                                result = MovimientosOrdenesMyo.AutorizaMyo(idOrden, SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
                            }
                        }
                        else
                        {
                            if (resultDoc.Dictamen == "Debe llenar todos los campos obligatorios.")
                            {
                                result = resultDoc.Dictamen;
                            }
                            else
                            {
                                if (ValidaDocumentos.GuardaDocumentos(idOrden, documentos))
                                {

                                    if (resultDoc.Dictamen == "Rechazado Completo por Documentacion")
                                    {
                                        ValidaDocumentos.BorrarDocumentos(idOrden, documentos);
                                        result = MovimientosOrdenesMyo.RechazaMyo(idOrden, SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
                                    }
                                    else
                                    {
                                        ValidaDocumentos.BorrarDocumentos(idOrden, documentos);
                                        result = MovimientosOrdenesMyo.ReasignaMyo(idOrden, SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
                                    }

                                }
                            }
                        }
                    }
                    break;
                case "Referencias":
                {
//                    //var referencias = valores.Select(JsonConvert.DeserializeObject<ReferenciasModel>).ToList();
//                    //var resultRef = ValidaReferencias.ReferenciasValidas(referencias);
//                    //if (resultRef.Valido)
//                    //{
//                    //    if (ValidaReferencias.GuardaReferencias(idOrden, referencias))
//                    //    {
////result = MovimientosOrdenesMyo.AutorizaMyo(idOrden);
//                    //    }
//                    //}
//                    //else
//                    //{
//                    //    if (ValidaReferencias.GuardaReferencias(idOrden, referencias))
//                    //    {
//       if(ValidaReferencias.BorrarReferencias(idOrden, referencias))
//                    //        {
//result = MovimientosOrdenesMyo.ReasignaMyo(idOrden);
//                    //        }
                            
                    //    }
                    //}
                }
                    break;
                case "Revision":
                {
                    var documentos = valores.Select(JsonConvert.DeserializeObject<DocumentosModel>).ToList();

                    if (ValidaDocumentos.GuardaDocumentos(idOrden, documentos))
                    {
                        if (documentos[0].Valor=="No")
                        {
                            result = MovimientosOrdenesMyo.RechazaMyo(idOrden, SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
                            CorreosMyo.LegalRechazo(idOrden);
                        }
                        else
                        {
                            result = MovimientosOrdenesMyo.AutorizaMyo(idOrden, SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
                        }
                    }   
                }
                    break;
            }

            return result;
        }


        /// <summary>
        /// Convert base32 string to array of bytes
        /// </summary>
        /// <param name="base32String">Base32 string to convert</param>
        /// <returns>Returns a byte array converted from the string</returns>
        internal static byte[] FromBase32String(string base32String)
        {
            const int InByteSize = 8;
            const int OutByteSize = 5;
            const string Base32Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";

            // Check if string is null
            if (base32String == null)
            {
                return null;
            }
            // Check if empty
            else if (base32String == string.Empty)
            {
                return new byte[0];
            }

            // Convert to upper-case
            string base32StringUpperCase = base32String.ToUpperInvariant();

            // Prepare output byte array
            byte[] outputBytes = new byte[base32StringUpperCase.Length * OutByteSize / InByteSize];

            // Check the size
            if (outputBytes.Length == 0)
            {
                throw new ArgumentException("Specified string is not valid Base32 format because it doesn't have enough data to construct a complete byte array");
            }

            // Position in the string
            int base32Position = 0;

            // Offset inside the character in the string
            int base32SubPosition = 0;

            // Position within outputBytes array
            int outputBytePosition = 0;

            // The number of bits filled in the current output byte
            int outputByteSubPosition = 0;

            // Normally we would iterate on the input array but in this case we actually iterate on the output array
            // We do it because output array doesn't have overflow bits, while input does and it will cause output array overflow if we don't stop in time
            while (outputBytePosition < outputBytes.Length)
            {
                // Look up current character in the dictionary to convert it to byte
                int currentBase32Byte = Base32Alphabet.IndexOf(base32StringUpperCase[base32Position]);

                // Check if found
                if (currentBase32Byte < 0)
                {
                    throw new ArgumentException(string.Format("Specified string is not valid Base32 format because character \"{0}\" does not exist in Base32 alphabet", base32String[base32Position]));
                }

                // Calculate the number of bits we can extract out of current input character to fill missing bits in the output byte
                int bitsAvailableInByte = Math.Min(OutByteSize - base32SubPosition, InByteSize - outputByteSubPosition);

                // Make space in the output byte
                outputBytes[outputBytePosition] <<= bitsAvailableInByte;

                // Extract the part of the input character and move it to the output byte
                outputBytes[outputBytePosition] |= (byte)(currentBase32Byte >> (OutByteSize - (base32SubPosition + bitsAvailableInByte)));

                // Update current sub-byte position
                outputByteSubPosition += bitsAvailableInByte;

                // Check overflow
                if (outputByteSubPosition >= InByteSize)
                {
                    // Move to the next byte
                    outputBytePosition++;
                    outputByteSubPosition = 0;
                }

                // Update current base32 byte completion
                base32SubPosition += bitsAvailableInByte;

                // Check overflow or end of input array
                if (base32SubPosition >= OutByteSize)
                {
                    // Move to the next character
                    base32Position++;
                    base32SubPosition = 0;
                }
            }

            return outputBytes;
        }

        public ActionResult AutorizarAnalisis(int idUsuario, string[] textoOrdenes)
        {
            var entidad = new EntGestionadas();
            Logger.WriteLine(Logger.TipoTraceLog.Trace, idUsuario, "GestionadasController", "AutorizarAnalisis- Ordenes: " + textoOrdenes);
            var resultado = entidad.AutorizarAnalisis(string.Join(",", textoOrdenes));
            return Json(new { Respuesta = resultado }, JsonRequestBehavior.AllowGet);
        }



        public ActionResult ProcesarRespuestas(int idorden,int idusuario)
        {
            return Json(new { resultado = MovimientosOrdenesMyo.ProcesarRespuestas(idorden, idusuario) }, JsonRequestBehavior.AllowGet);
        }
    }

    public enum TipoGestionada
    {
        Autorizadas,
        AutorizarRechazar,
        Rechazadas
    }
}
