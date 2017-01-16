using System;
using System.Globalization;
using System.Web.Mvc;
using PubliPayments.Entidades;
using PubliPayments.Models;
using PubliPayments.Utiles;

namespace PubliPayments.Controllers
{
    public class AdminDespachoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GvDespachos()
        {
            var listaDespachos = new Negocios.AdminDespacho().ObtenerDespachos();
            return PartialView(listaDespachos);
        }

        public ActionResult PopupNuevo(DespachoModel modelo)
        {
            return PartialView(modelo);
        }

        public ActionResult PopupEditar(DespachoEditModel modelo)
        {
            Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "AdminDespacho",
                                "popupEditar");
            try
            {
                if (!ModelState.IsValid)
                {
                    var datosDominio = new Negocios.AdminDespacho().ObtenerDatosDominio(modelo.idDominio);

                    modelo.ADNombre = datosDominio.nombreDominio;
                    modelo.ADNCorto = datosDominio.nom_corto;
                    modelo.ADEstatus = datosDominio.estatus == 1;

                    return PartialView(modelo);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "AdminDespacho",
                                "popupEditar error: " + ex.Message);   
            }

            return PartialView(modelo);
        }

        public string ValidarNomCorto(string nomCorto,int idDominio)
        {
            try
            {
                Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "ValidarNomCorto",
                                "Valida nombre corto: " + nomCorto);

                var usuarioEmail = new Negocios.AdminDespacho().ValidaNomCorto(nomCorto, idDominio);

                if (usuarioEmail)
                {
                    return "El nombre corto ya se encuentra registrado en el sistema";
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "ValidarNomCorto", "ValidarNomCorto: " + ex.Message);
            }
            return "OK";
        }

        public string BtEditDespacho(DespachoEditModel modelo)
        {
            if (!ModelState.IsValid)
                return "-1";
            
            var idUsuario = SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario);
            int dominio = modelo.idDominio;
            string nombreDominio = modelo.ADNombre;
            string nomCorto = modelo.ADNCorto;

            Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "btEditDespacho",
                                "btEditDespacho - Editando por el usuario " + idUsuario + " el dominio " + dominio.ToString(CultureInfo.InvariantCulture));

            int estatus = modelo.ADEstatusH.Trim().ToUpper() == "TRUE" ? 1 : 0;
            try
            {
                if (dominio != 0 && nombreDominio != "" && nomCorto != "")
                {
                    var resAct = new Negocios.AdminDespacho().ActualizarDominio(dominio, nombreDominio, nomCorto, estatus);
                    if (resAct)
                    {
                        Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "btEditDespacho",
                                "btEditDespacho - Editado por el usuario " + idUsuario + " el dominio " + dominio.ToString(CultureInfo.InvariantCulture));
                        return "1";
                    }
                }
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "btEditDespacho",
                    "btEditDespacho - Error al editar el dominio " + dominio.ToString(CultureInfo.InvariantCulture) + " por el idUsuario " + idUsuario);
                return "-1";
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "btEditDespacho",
                                "btEditDespacho - Error: " + ex.Message);
                return "-1";
            }
        }

        public string ValidaCorreo(string correo, string usuario)
        {
            try
            {
                var usuarioEmail = new Negocios.AdminDespacho().ObtenerUsuarioPorEmail(correo);

                if (usuarioEmail != null)
                {
                    if (usuarioEmail.Usuario != usuario)
                    {
                        return "El email ya se encuentra registrado en el sistema";
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "AdminUsuarios", "validarCorreo: " + ex.Message);
            }
            return "OK";
        }

        public string ValidaUsuario(string usuario)
        {
            try
            {
                var validacion = new Negocios.AdminDespacho().ValidaUsuario(usuario);

                if (validacion == false)
                {
                    return "El usuario ya se encuentra registrado en el sistema";
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "AdminUsuarios", "validarCorreo: " + ex.Message);
            }
            return "OK";
        }

        public string BtCrearDespacho(DespachoModel modelo)
        {
            if (!ModelState.IsValid)
                return "-1";

            string nombreDominio = modelo.tbDespacho;
            string nomCorto = modelo.tbNombreCorto;
            string usuarioAdmin = modelo.tbNombreUsuario;
            string nombre = modelo.tbNombre;
            string email = modelo.tbEmail;

            Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "btCrearDespacho",
                                "btCrearDespacho - Creando ");
            try
            {
                var condiciones = new[,] { { "1", "MA" }, { "1", "CE" }, { "1", "NU" }, { "1", "MI" }, { "4", "NA" } };
                var password = Security.GeneratePassWord(condiciones);
                if (nombreDominio != "" && nomCorto != "" && usuarioAdmin != "" && email != "" && nombre != "")
                {
 
                    var exito = new Negocios.AdminDespacho().InsertaDominio(nombreDominio, nomCorto, usuarioAdmin,
                        nombre, password[1], email);

                    Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "btCrearDespacho",
                                "btCrearDespacho - Creado ");

                    if (exito)
                    {
                        var mensajeServ = new EntMensajesServicios().ObtenerMensajesServicios("NuevoUsr");
                        if (Request.Url != null)
                            mensajeServ.Mensaje = string.Format(mensajeServ.Mensaje, usuarioAdmin, password[0],
                                Request.Url.GetLeftPart(UriPartial.Authority));
                        bool succes = Email.EnviarEmail(email, mensajeServ.Titulo, mensajeServ.Mensaje,
                            mensajeServ.EsHtml);
                        if (!succes)
                        {
                            Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "AdminDespacho",
                                "btCrearDespacho_Click - No se pudo mandar email a :" + email + " usuario:" + nombre);
                        }
                        return "1";
                    }
                }
                return "-1";
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "btCrearDespacho",
                                "btCrearDespacho - Error: " + ex.Message);
                return "-1";
            }
        }
    }
}
