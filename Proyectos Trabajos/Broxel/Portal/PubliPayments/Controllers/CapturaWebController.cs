using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using PubliPayments.Entidades;
using PubliPayments.Models;
using PubliPayments.Negocios;
using PubliPayments.Utiles;

namespace PubliPayments.Controllers
{
    public class CapturaWebController : Controller
    {
        //
        // GET: /CapturaWeb/

        static readonly string UrlWS01800Pagos = ConfigurationManager.AppSettings["UrlWS01800pagos"];
        public ActionResult Index()
        {
            var idUsuarioAlta = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            var usuario = PermiteCapturaWebUsuario(idUsuarioAlta);
            if (usuario == null) return Redirect("/unauthorized.aspx");
            return View();
        }

        [ValidateInput(false)]
        public ActionResult CapturaWebGridViewPartial()
        {
            var idUsuarioAlta = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            var usuario = PermiteCapturaWebUsuario(idUsuarioAlta);
            if (usuario == null) return Redirect("/unauthorized.aspx");
            var ent = Config.EsCallCenter
                ? new Formularios().ObtenerListaFormularios("")
                    .FirstOrDefault(x => x.Captura == 2 && x.Nombre.Contains("CallCenter"))
                : new FormularioModel(null, null, null, null, null, null, null, null, CtrlComboFormulariosController.ObtenerModeloFormularioActivo(Session).Ruta);
            
            if (ent != null)
            {
                var model = Persistencia.ObtenerOrdenes(1, usuario.idPadre, "", idUsuarioAlta, ent.Ruta, true);
                return PartialView("_CapturaWebGridViewPartial", model);
            }
          
            return null;
        }
        /// <summary>
        /// Metodo encargado de consultar los servicios utilizados en originacion 
        /// </summary>
        /// <param name="idWorkOrderFormType"></param>
        /// <param name="idWorkOrder"></param>
        /// <param name="externalId">credito seleccionado a procesar</param>
        /// <param name="action">Nombre del servicio a consultar Precalificacion - BuroCredito</param>
        /// <param name="nss">Numero de Seguro Social</param>
        /// <param name="username">Nombre de usuario del movil</param>
        /// <param name="workOrderType">Nombre del proceso al que pertenece</param>
        /// <param name="inputFields">Datos extra relacionados al usuario para la consulta</param>
        /// <returns>Resultado de la consulta en un string</returns>
        [ValidateInput(false)]
        public ActionResult InvokeWS01800Pagos(string idWorkOrderFormType, String idWorkOrder, String externalId,
            String action, String nss, String username, String workOrderType, String inputFields)
        {
            switch (action)
            {
                case "Precalificacion":
                    return Json(InvokeWS01800PagosPrecalif(idWorkOrderFormType, idWorkOrder, externalId, action, nss, username,workOrderType), JsonRequestBehavior.AllowGet);
                case "BuroCredito":
                    return Json(InvokeWS01800PagosBuro(idWorkOrderFormType, idWorkOrder, externalId, action, username, workOrderType, inputFields), JsonRequestBehavior.AllowGet);
            }

            return Json("{\"UpdateFieldsValues\":{\"Mensaje\":\"Esta acción no esta permitida\"}}", JsonRequestBehavior.AllowGet);                          
        }

        /// <summary>
        /// Se encarga de consultar las opciones de prestamo que se puede ofrecer, en caso de que no cumpla tambien se informa
        /// </summary>
        /// <param name="idWorkOrderFormType"></param>
        /// <param name="idWorkOrder"></param>
        /// <param name="externalId">credito seleccionado a procesar</param>
        /// <param name="action">Nombre del servicio a consultar - Precalificacion</param>
        /// <param name="nss">Numero de Seguro Social</param>
        /// <param name="username">Nombre de usuario del movil</param>
        /// <param name="workOrderType">Nombre del proceso al que pertenece - Consultas</param>
        /// <returns></returns>
        private string InvokeWS01800PagosPrecalif(string idWorkOrderFormType, String idWorkOrder, String externalId, String action, String nss, String username, String workOrderType)
        {
            var msg = "{\"IdWorkOrderFormType\":\"" + Guid.NewGuid() + "\", \"IdWorkOrder\":\"" + Guid.NewGuid() +
                      "\",\"ExternalId\":\"" + externalId + "\",\"Action\":\"" + action +
                      "\",\"InputFields\": {\"Nss\":\"" + nss + "\" }, \"Username\":\"" + username +
                      "\",\"WorkOrderType\": \"" + workOrderType + "\"}";
            string responseString= InvokeWS(msg);
            responseString = responseString.Replace(Environment.NewLine, " ").Replace("\u000a","");
            return responseString;
        }
        /// <summary>
        /// Se encarga de consultar al servicio de Buro de Credito donde se indica si el trabajador tiene opcion a credito
        /// </summary>
        /// <param name="idWorkOrderFormType"></param>
        /// <param name="idWorkOrder"></param>
        /// <param name="externalId">credito seleccionado a procesar</param>
        /// <param name="action">Nombre del servicio a consultar - BuroCredito</param>
        /// <param name="username">Nombre de usuario del movil</param>
        /// <param name="workOrderType">>Nombre del proceso al que pertenece - BuroCredito</param>
        /// <param name="inputFields">Datos demograficos necesarios para consultar en buro</param>
        /// <returns></returns>
        private string InvokeWS01800PagosBuro(string idWorkOrderFormType, String idWorkOrder, String externalId, String action, String username, String workOrderType, String inputFields)
        {
            var inputFieldsArr = inputFields.Split(new[] { "||" }, StringSplitOptions.None);
            var msg = "{\"Action\":\"" + action + "\",\"ExternalId\":\"" + externalId +
                        "\",\"IdWorkOrder\": \"" + Guid.NewGuid() + "\", \"IdWorkOrderFormType\":\"" + Guid.NewGuid() +
                      "\",\"InputFields\": {\"APaterno\":\"" + inputFieldsArr[0] + "\",\"AMaterno\":\"" + inputFieldsArr[1] + "\",\"Nombres\":\"" + inputFieldsArr[2] + "\",\"Calle\":\"" + inputFieldsArr[3] + "\",\"NumeroExt\":\"" + inputFieldsArr[4] + "\",\"NumeroInt\":\"" + inputFieldsArr[5] + "\",\"Colonia\":\"" + inputFieldsArr[6] + "\",\"Delegacion\":\"" + inputFieldsArr[7] + "\",\"Estado\":\"" + inputFieldsArr[8] + "\",\"Cp\":\"" + inputFieldsArr[9] + "\",\"RfcPrecalificacion\":\"" + inputFieldsArr[10] + "\",\"ExternalType\":\"" + inputFieldsArr[11] + "\"}, \"UrlServiceAction\":null,\"Username\": \"" + username +
                      "\",\"WorkOrderType\": \"" + workOrderType + "\"}";
            string responseString =InvokeWS(msg);
            responseString = responseString.Replace(Environment.NewLine, " ");
            return responseString;
        }
        /// <summary>
        /// Metodo para invocar Ws
        /// </summary>
        /// <param name="msg">String que se envia a servicio tiene formato Json</param>
        /// <returns></returns>
        private string InvokeWS(string msg)
        {
            string responseString = "";
            try
            {
                var httpWReq =
                (HttpWebRequest)WebRequest.Create(UrlWS01800Pagos);

                var encoding = new ASCIIEncoding();
                byte[] data = encoding.GetBytes(msg);

                httpWReq.Method = "POST";
                httpWReq.ContentType = "application/x-www-form-urlencoded";
                httpWReq.ContentLength = data.Length;

                using (Stream stream = httpWReq.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)httpWReq.GetResponse();

                responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            }
            catch (Exception e)
            {
                responseString = "";

                Logger.WriteLine(Logger.TipoTraceLog.Error, 1, "CapturaWeb", "InvokeWS - "+e.Message);

            }
            return responseString;
        }

        public ActionResult Gestionar(string credito)
        {
            //Valida Orden y Asignación
            
            
            //var callBack = DevExpressHelper.IsCallback;
            var idUsuarioAlta = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            var EsCallCenterOut = SessionUsuario.ObtenerDato(SessionUsuarioDato.EsCallCenterOut).ToLower() == "true";

            var entformulario = new EntFormulario();
            var orden = new Orden();
            Logger.WriteLine(Logger.TipoTraceLog.Trace, idUsuarioAlta, "CapturaWeb", "Gestionar - idOrden: " + credito);
            
            var modelOrden = orden.ObtenerOrdenxCredito(credito) ?? new OrdenModel { IdOrden = -1, NumCred = credito };

            var formularioModel = Config.EsCallCenter ? entformulario.ObtenerListaFormularios("").FirstOrDefault(x => x.Captura == 2 && x.Nombre.Contains("CallCenter")) : EsCallCenterOut ? entformulario.ObtenerListaFormularios("").FirstOrDefault(x => x.Captura == 2 && x.Nombre.Contains("RDST")) : (new Formularios().ObtenerFormulariosXOrden(modelOrden.IdOrden, idUsuarioAlta, 2))[0];
            var subformularios = entformulario.ObtenerSubFormularios(formularioModel.IdFormulario);
            
            DataSet dsOrden = orden.ObtieneOrdenXml(1, credito, modelOrden.IdOrden);
            var datosCapturaWeb = new Dictionary<string, string>();
            var usuario = PermiteCapturaWebUsuario(idUsuarioAlta);
            var listaFormularios = new List<FormularioModelCw>();
           
            if (usuario == null)
            {
                listaFormularios.Add(new FormularioModelCw
                {
                    Formulario = "El usuario no está autorizado para acceder a la Captura Web",
                    IdFormulario = 0,
                    ListaCamposXFormularios = new List<CamposXSubFormulario>()
                });

                Logger.WriteLine(Logger.TipoTraceLog.Trace, idUsuarioAlta, "CapturaWeb", "Gestionar - El usuario no está autorizado para acceder a la Captura Web");


                return Redirect("/unauthorized.aspx");
            }
            ViewBag.Ruta = formularioModel.Ruta;
            ViewBag.AssignedTo = usuario.Usuario;
            ViewBag.ImagenGuid = Guid.NewGuid();
            ViewBag.ExternalType = formularioModel.Nombre;

            foreach (DataRow row in dsOrden.Tables[1].Rows)
            {
                var valor = row["Valor"].ToString();
                    valor = Orden.ProcesarValorXml(valor, dsOrden, idUsuarioAlta);

                datosCapturaWeb.Add(row["Nombre"].ToString(), valor);
            }

            if (formularioModel.Ruta.ToUpper().Contains("CSD") || formularioModel.Ruta.ToUpper().Contains("RDST"))
            {
                datosCapturaWeb = datosCapturaWeb.Concat(Orden.AgregarControlesXml(dsOrden))
                      .ToDictionary(x => x.Key, x => x.Value);
            
            }

            if (datosCapturaWeb.Count > 0)
            {
                foreach (var f in subformularios)
                {
                    var form = new FormularioModelCw
                    {
                        Formulario = f.SubFormulario1,
                        IdFormulario = f.idFormulario,
                        CatalogosListas = new Dictionary<int, List<SelectListItem>>(),
                        Clase = f.Clase
                    };

                    var campos = entformulario.ObtenerCampos(f.idSubFormulario);
                    foreach (var campo in campos)
                    {
                        try
                        {
                            if (campo.Valor != null && campo.Valor.StartsWith("[Tabla]"))
                            {
                                campo.Valor = datosCapturaWeb[campo.Valor.Replace("[Tabla]", "")];
                            }

                            if (campo.idTipoCampo == 9) //Lista
                            {
                                //obtiene los catalogos para las listas
                                var catalogos = entformulario.ObtenerCatalogosPorCampo(campo.idCampoFormulario);
                                var listaSelect = catalogos.Select(catalogo => new SelectListItem
                                {
                                    Selected = false,
                                    Text = catalogo.Texto,
                                    Value = catalogo.Valor
                                }).ToList();

                                form.CatalogosListas.Add(campo.idCampoFormulario, listaSelect);
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuarioAlta, "CapturaWeb", "Gestionar - Valor: " + campo.Valor + " - Error: " + ex.Message);
                        }
                       
                    }

                    form.ListaCamposXFormularios = campos;

                    listaFormularios.Add(form);
                }
            }
            else
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, idUsuarioAlta, "CapturaWeb", "Gestionar - No se encontró el crédito");

                listaFormularios.Add(new FormularioModelCw
                {
                    Formulario = "No se encontró el crédito",
                    IdFormulario = 0,
                    ListaCamposXFormularios = new List<CamposXSubFormulario>()
                });
            }

            ViewBag.Funciones = new Formularios().ObtenerFuncionesCampo(idUsuarioAlta, "", formularioModel.IdFormulario);
            ViewBag.PorcentajePermitido = new EntCapturaWeb().ObtenerPorcentajeCapturaWeb(usuario.idPadre, idUsuarioAlta);
            ViewBag.EstatusOrden = modelOrden.Estatus.ToString();
            ViewBag.EsCallCenter = Config.EsCallCenter;
            return PartialView(listaFormularios);
        }

        public ActionResult Upload(string credito, string campo, string contenido, string guid)
        {
            var idUsuarioAlta = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            var usuario = PermiteCapturaWebUsuario(idUsuarioAlta);

            if (usuario == null)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, idUsuarioAlta, "CapturaWeb", "Upload - El Usuario no tiene permisos para guardar archivos");
                return Redirect("unauthorized.aspx");
            }
            if (string.IsNullOrWhiteSpace(guid))
            {
                 return Content("Ocurrió un error al intentar subir el archivo");
            }

            var ordenN = new Orden();
            var ordenModel= ordenN.ObtenerOrdenxCredito(credito) ?? new OrdenModel { IdOrden = -1 ,NumCred = credito};
            Logger.WriteLine(Logger.TipoTraceLog.Trace, idUsuarioAlta, "CapturaWeb",
                string.Format("Trace - orden={0}, credito{1},campo={2},Len(contenido)={3},guid={4}", ordenModel.IdOrden,ordenModel.NumCred, campo, contenido.Length,
                    guid));

            string directorioImagenes = ConfigurationManager.AppSettings["CWDirectorioImagenes"] ?? @"C:\Publipayments\ImagenesCW\";

            var directorioImagenesList = directorioImagenes.Substring(0,(directorioImagenes.IndexOf(".com", System.StringComparison.Ordinal)+4)).Split('\\');

            if (contenido.StartsWith("data:image/png;base64,") || contenido.StartsWith("data:image/jpeg;base64,"))
            {
                try
                {

                    var ent = Config.EsCallCenter ? new EntFormulario().ObtenerListaFormularios("").FirstOrDefault(x => x.Captura == 2 && x.Nombre.Contains("CallCenter"))
                                                    : new EntFormulario().ObtenerListaFormularios("").FirstOrDefault(x => x.Captura == 2 && x.Nombre.Contains("CobSocial"));
                    if (ent != null)
                    {
                        var ordenes = Config.EsCallCenter ? new BuscarOrdenes().BuscaOrdenes(new Busqueda { Credito = ordenModel.NumCred, IdUsuario = idUsuarioAlta }) : Persistencia.ObtenerOrdenes(1, usuario.idPadre, "", idUsuarioAlta, ent.Ruta, true);
                        if (ordenes!=null)
                        {
                            DataRow ordenEncontrada =
                           ordenes.Rows.Cast<DataRow>()
                               .Where(dr => Convert.ToString(dr["idOrden"]) == ordenModel.IdOrden.ToString(CultureInfo.InvariantCulture))
                               .FirstOrDefault(dr => Convert.ToString(dr["Estatus"]) == "1");
                            if (ordenEncontrada != null || Config.EsCallCenter)
                            {
                                var ext = contenido.StartsWith("data:image/png;base64,") ? "png" : "jpg";
                                var img = contenido.Replace("data:image/png;base64,", "");
                                img = img.Replace("data:image/jpeg;base64,", "");
                                byte[] bytes = Convert.FromBase64String(img);
                                var path = directorioImagenes + ((ordenModel.IdOrden > 0) ? ordenModel.IdOrden.ToString(CultureInfo.InvariantCulture) : "temp");
                                if (!Directory.Exists(path))
                                    Directory.CreateDirectory(path);
                                var fullpath = path + @"\" + guid + "_" + campo + "." + ext;
                                var url = "https://" + directorioImagenesList[directorioImagenesList.Length - 1] + "/" + ((ordenModel.IdOrden > 0) ? ordenModel.IdOrden.ToString(CultureInfo.InvariantCulture) : "temp") + "/" + guid + "_" + campo + "." + ext;
                                var imagenOriginal = Imagenes.ByteArrayToImage(bytes);
                                int ancho;
                                int alto;
                                if (imagenOriginal.Width > imagenOriginal.Height)
                                {
                                    ancho = 640;
                                    alto = 480;
                                }
                                else
                                {
                                    ancho = 480;
                                    alto = 640;
                                }
                                Image imagen = Imagenes.ResizeImage(imagenOriginal, ancho, alto);
                                System.IO.File.WriteAllBytes(fullpath, Imagenes.ImageToByteArray(imagen, ext));

                                Logger.WriteLine(Logger.TipoTraceLog.Trace, idUsuarioAlta, "CapturaWeb",
                                    "Upload - OK: " + fullpath + " - url:" + url);

                                return Content("OK|" + url);
                            }
                        }
                       
                    }
                    Logger.WriteLine(Logger.TipoTraceLog.Trace, idUsuarioAlta, "CapturaWeb",
                        "Upload - Orden no encontrada");
                    return Content("Orden no encontrada");
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuarioAlta, "CapturaWeb", "Upload - Orden "+ordenModel.IdOrden+ " - Error:" + ex.Message);
                    Logger.WriteLine(Logger.TipoTraceLog.Error, idUsuarioAlta, "CapturaWeb", "Upload - Orden " + ordenModel.IdOrden + " - ContenidoImagen :" + contenido);
                    return Content("Ocurrió un error al intentar subir el archivo");
                }
            }
            return Content("Formato de archivo no reconocido");
        }

        public ActionResult GuardarGestion(string [] resp)
        {
            if (resp.Count() <  2) return Content("Error");

            
            var usuario = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            var usuarioModel = new Usuarios().ObtenerUsuarioPorId(usuario.ToString());

            var usr = PermiteCapturaWebUsuario(usuario);
            if (usr == null)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, usuario, "CapturaWeb",
                    "GuardarGestion - El Usuario no tiene permisos para guardar la gestion");
                return Content("Error");
            }

            var porcentajePermitido = new EntCapturaWeb().ObtenerPorcentajeCapturaWeb(usr.idPadre, usuario);
            if ((porcentajePermitido == null || porcentajePermitido.CantidadActual >= porcentajePermitido.PorcentajePermitido) && !Config.EsCallCenter)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, usuario, "CapturaWeb",
                    "GuardarGestion - Se llego al limite de capturas permitidas para el supervisor:" +
                    usr.idPadre);
                return Content("Error");
            }

            try
            {
                int idOrden = 0;
                foreach (string s in resp)
                {
                    if (s.StartsWith("idOrden"))
                        idOrden = Config.EsCallCenter || usuarioModel.EsCallCenterOut ? -1 : Convert.ToInt32(s.Substring(s.IndexOf('|') + 1));
                }
                Logger.WriteLine(Logger.TipoTraceLog.Trace, usuario, "CapturaWeb", "Recibiendo orden: " + idOrden);

                var listaRespuestas = new Dictionary<string, string>();
                if (idOrden > 0 || Config.EsCallCenter || usuarioModel.EsCallCenterOut)
                {
                    foreach (string s in resp)
                    {
                        int pipe = s.IndexOf('|');

                        if (pipe > 0 )
                        {
                            if (s.StartsWith("idOrden")) continue;
                            string llave = s.Substring(0, pipe);
                            string valor = s.Substring(pipe + 1, s.Length - pipe - 1);
                            listaRespuestas.Add(llave, valor);
                        }
                        else
                        {
                            Logger.WriteLine(Logger.TipoTraceLog.Error, usuario, "CapturaWeb",
                                "GuardarGestion - Error valor incorrecto: " + s);
                            return Content("Error"); 
                        }
                    }
                    
                    var respuesta = new Respuesta();
                    var mensaje = respuesta.GuardarRespuesta(idOrden, listaRespuestas, "CapturaWeb", usuarioModel.Usuario, usuario,
                        Config.AplicacionActual().Productivo,Config.AplicacionActual().Nombre);

                    if (mensaje != string.Empty)
                    {
                        Logger.WriteLine(Logger.TipoTraceLog.Error, usuario, "CapturaWeb", mensaje);
                        return Content("Error");
                    }
                    
                    return Content("OK");
                }
                return Content("Error");
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, usuario, "CapturaWeb", "GuardarGestion - " + ex.Message);
                return Content("Error");
            }
        }

        private VUsuarios  PermiteCapturaWebUsuario(int idUsuario )
        {
            var usr = new EntUsuario().ObtenerUsuarioPorId(idUsuario);
            if (Config.AplicacionActual().Nombre.ToUpper().Contains("MYO"))
            {
                return usr;
            }
            if (usr.idRol != 4) return null; //Que sea un gestor
            if (usr.Estatus != 1) return null; //Que este activo
            return usr;
        }

        public ActionResult EncriptaCodigo(string codigo)
        {
            return Content(Security.HashSHA512(codigo));
        }
    }
}
