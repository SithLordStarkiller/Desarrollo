using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PubliPayments.Models;
using PubliPayments.Utiles;
using PubliPayments.Entidades;
using PubliPayments.Negocios;
using System.Data;

namespace PubliPayments.Controllers
{
    public class HomeController : Controller
    {
        public string LogosDir;
        static string _returnUrl = Config.AplicacionActual().Nombre.ToUpper().Contains("SIRA")?"AsignaOrdenes.aspx":"/Dashboard";

        [AllowAnonymous]
        public ActionResult Index(Login modelo = null)
        {
            if (modelo != null)
            {
            Logger.WriteLine(Logger.TipoTraceLog.Log, 0, Path.GetFileName(Request.PhysicalPath),
            string.Format("Login - Dominio: {0} - Usuario: {1} - IP: {2}", modelo.tbDominio, modelo.tbUsuario,
            GetIpAddress()));
            }

            ViewBag.LogosDir = Config.AplicacionActual().Nombre;
            ViewBag.Version = "Versión: " + Versionado.VersionActual;

            if (!ModelState.IsValid || modelo == null)
                return View("Index");

            if (modelo.tbDominio == "Dominio" || modelo.tbUsuario == "Usuario" || modelo.tbPassword == "Contraseña")
            {
                Logger.WriteLine(Logger.TipoTraceLog.Log,
                Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario)),
                Path.GetFileName(Request.PhysicalPath),
                string.Format("Login - Dominio: {0} - Usuario: {1} - IP: {2} - Login incorrecto", modelo.tbDominio,
                modelo.tbUsuario, GetIpAddress()));
                ViewBag.DatosIncorrectos = "Por favor llene los campos de dominio, usuario y contraseña ";
                return View("Index");
            }
            try
            {
                Session.Clear();
                string passEnciptado = Security.HashSHA512(modelo.tbPassword);

                var modelUsuario = new NegocioUsuario().ValidaUsuario(modelo.tbDominio, modelo.tbUsuario, passEnciptado, Config.EsCallCenter);

                if (modelUsuario == null)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Log,
                    Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario)),
                    Path.GetFileName(Request.PhysicalPath),
                    string.Format("Login - Dominio: {0} - Usuario: {1} - IP: {2} Login incorrecto",
                     modelo.tbDominio, modelo.tbUsuario, GetIpAddress()));
                    ViewBag.DatosIncorrectos = " Datos Incorrectos ";
                    return View("Index");
                }

                if (modelUsuario.IdUsuario == -1)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Log,
                    Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario)),
                    Path.GetFileName(Request.PhysicalPath),
                    string.Format("Login - Dominio: {0} - Usuario: {1} - IP: {2} Login incorrecto", modelo.tbDominio, modelo.tbUsuario, GetIpAddress()));

                    ViewBag.DatosIncorrectos = " Datos Incorrectos ";

                    modelUsuario = new EntUsuario().BloqueoLogin(modelo.tbDominio, modelo.tbUsuario);

                    if (modelUsuario != null)
                    {
                    if (modelUsuario.Estatus == 2 && modelUsuario.Intentos > 2)
                    {
                            ViewBag.DatosIncorrectos =
                                "Ha sobrepasado el número de intentos permitidos, su cuenta permanecerá bloqueada hasta: " +
                                modelUsuario.Bloqueo;
                        
                        if (modelUsuario.Intentos == 3)
                        {
                            if (Request.Url != null)
                            {
                                string url = Request.Url.GetLeftPart(UriPartial.Authority);
                                string llaveUnica = RandomString(15);
                                var mensajeServ = new EntMensajesServicios().ObtenerMensajesServicios("BloqueoUsr");
                                    mensajeServ.Mensaje = string.Format(mensajeServ.Mensaje, GetIpAddress(),
                                        modelUsuario.Bloqueo, url, llaveUnica);
                                    bool success = Email.EnviarEmail(modelUsuario.Email, mensajeServ.Titulo,
                                        mensajeServ.Mensaje, mensajeServ.EsHtml);

                                if (success)
                                {
                                    Logger.WriteLine(Logger.TipoTraceLog.Log,
                                        Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario)),
                                        Path.GetFileName(Request.PhysicalPath),
                                            string.Format(
                                                "Recuperar - Dominio: {0} - Usuario: {1} - Mail: {2} - IP: {3} - Correo enviado",
                                            modelo.tbDominio, modelo.tbUsuario, modelUsuario.Email, GetIpAddress()));
                                    ConexionSql conn = ConexionSql.Instance;
                                    DateTime now = DateTime.Now;
                                    DateTime expira = now.AddHours(5);
                                    conn.InsertaLlave(llaveUnica, modelUsuario.IdUsuario, expira);
                                }
                            }
                        }
                    }
                    }
                    return View("Index");
                }

                if (modelUsuario.Estatus == 3)
                {
                    var camCont = new CambiarContrasenia
                    {
                        tbAPUsuario = modelo.tbUsuario,
                        tbAPPassword = passEnciptado,
                        tbidUsuario = modelUsuario.IdUsuario.ToString(CultureInfo.InvariantCulture),
                        tbAPDominio = modelo.tbDominio,
                        tbADominio = modelo.tbDominio,
                        tbAPCPassword = "",
                        tbAPNPassword = "",
                        tbReturnUrl = _returnUrl,
                        tipo = (modelUsuario.Alta.Substring(0, 16) == modelUsuario.UltimoLogin.Substring(0, 16))
                    };

                    ViewBag.tbReturnUrl = "/Dashboard";

                    return View("~/Views/Home/ActualizarPassword.cshtml", camCont);
                }

                
                Crearcookie(modelUsuario, modelo.cbRecordarme);
                Logger.WriteLine(Logger.TipoTraceLog.Log,
                    modelUsuario.IdUsuario,
                    Path.GetFileName(Request.PhysicalPath),
                    string.Format("Login - Dominio: {0} - Usuario: {1} - Login OK", modelo.tbDominio, modelo.tbUsuario));
                Response.Redirect(_returnUrl); 
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error,
                    Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario)),
                    Path.GetFileName(Request.PhysicalPath),
                    string.Format("Dominio: {0} - Usuario: {1} - Error: {2} - IP: {3}", modelo.tbDominio,
                        modelo.tbUsuario, ex.Message, GetIpAddress()));

                ViewBag.DatosIncorrectos = " Ocurrio un error ";
            }

            return View("Index");
        }

        [AllowAnonymous]
        public ActionResult OlvidastePassword(OlvidasteContasenia modelo)
        {
            string dominio = modelo.tbDom;
            string usuario = modelo.tbUser;
            string mail = modelo.tbMail;
            string idResult;
            ConexionSql cnnSql = ConexionSql.Instance;

            Logger.WriteLine(Logger.TipoTraceLog.Log,
            Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario)),
            Path.GetFileName(Request.PhysicalPath), string.Format("Recuperar - Dominio: {0} - Usuario: {1} - Mail: {2} - IP: {3}", dominio, usuario, mail, GetIpAddress()));

            ViewBag.LogosDir = Config.AplicacionActual().Nombre;
            ViewBag.Version = "Versión: " + Versionado.VersionActual;
            
            if (!ModelState.IsValid)
                return View("OlvidastePassword");

            if (mail.Length <= 0 || mail.Equals("Correo Electrónico"))
            {
                if ((dominio.Length <= 0 || dominio.Equals("Dominio")) || (usuario.Length <= 0 || usuario.Equals("Usuario")))
                {
                    ViewBag.labelRecuperar = "Por favor llene los campos de dominio y usuario o el campo de email";
                    return View(modelo);
                }

                DataSet ds = cnnSql.ValidaUsuarioParcial(dominio, usuario);
                idResult = ds.Tables[0].Rows[0]["idUsuario"].ToString();

                if (idResult.Equals("-1"))
                {
                    ViewBag.labelRecuperar = "Usuario y/o dominio incorrectos";
                    return View(modelo);
                }
                mail = ds.Tables[0].Rows[0]["Email"].ToString();
            }
            else
            {
                DataSet ds = cnnSql.ValidaUsuarioEmail(mail);
                idResult = ds.Tables[0].Rows[0]["idUsuario"].ToString();

                if (idResult.Equals("-1"))
                {
                    ViewBag.labelRecuperar = "Correo no encontrado";
                    return View(modelo);
                }
            }

            if (Request.Url != null)
            {
                string url = Request.Url.GetLeftPart(UriPartial.Authority);
                string llaveUnica = RandomString(15);
                var mensajeServ = new EntMensajesServicios().ObtenerMensajesServicios("RecUsrContrasenia");
                mensajeServ.Mensaje = string.Format(mensajeServ.Mensaje, url, llaveUnica);

                bool success = Email.EnviarEmail(mail, mensajeServ.Titulo, mensajeServ.Mensaje, mensajeServ.EsHtml);
                if (success)
                {
                    Logger.WriteLine(Logger.TipoTraceLog.Log,
                        Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario)),
                        Path.GetFileName(Request.PhysicalPath),
                        string.Format("Recuperar - Dominio: {0} - Usuario: {1} - Mail: {2} - IP: {3} - Correo enviado",
                            dominio, usuario, mail, GetIpAddress()));
                    ViewBag.labelRecuperar = "Correo enviado con éxito";

                    ConexionSql conn = ConexionSql.Instance;
                    DateTime now = DateTime.Now;
                    DateTime expira = now.AddHours(5);
                    conn.InsertaLlave(llaveUnica, Int32.Parse(idResult), expira);
                }
            }
            return View(modelo);
        }

        [AllowAnonymous]
        public ActionResult ActualizarPassword(CambiarContrasenia modelo)
        {
            ViewBag.LogosDir = Config.AplicacionActual().Nombre;
            ViewBag.Version = "Versión: " + Versionado.VersionActual;            

            Logger.WriteLine(Logger.TipoTraceLog.Log,
            Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario)),
            Path.GetFileName(Request.PhysicalPath), string.Format("Actualizar - Dominio: {0} - Usuario: {1} - IP: {2}", modelo.tbADominio, modelo.tbAPUsuario, GetIpAddress()));

            if (!ModelState.IsValid)
                return View(modelo);

            var passRegexn = new Regex(@"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$");
            if (modelo.tbAPDominio != "" && modelo.tbAPUsuario != "" && modelo.tbidUsuario != "" && modelo.tbAPPassword != "" && modelo.tbReturnUrl != "" && modelo.tbAPNPassword != "" && modelo.tbAPCPassword != "")
            {
                var idUsuario = Int32.Parse(modelo.tbidUsuario);

                var usuarioAnterior = new NegocioUsuario().ValidaUsuario(modelo.tbADominio, modelo.tbAPUsuario, modelo.tbAPPassword, Config.EsCallCenter);

                if (usuarioAnterior.IdUsuario != idUsuario)
                {
                    ViewBag.labelAPError = "Datos incorrectos";
                    return View(modelo);
                }

                if (modelo.tbAPNPassword != modelo.tbAPCPassword || modelo.tbAPNPassword.Equals("Contrasena"))
                {
                    ViewBag.labelAPError = "La nueva contraseña y la confirmación no coinciden";
                    return View(modelo);
                }
                
                var passWordSha = Security.HashSHA512(modelo.tbAPNPassword);
                if (passRegexn.IsMatch(modelo.tbAPNPassword))
                {
                    var passValido = new EntUsuario().ValidarPassBitacora(idUsuario, passWordSha);
                    if (passValido)
                    {
                        var modelUsuario = new EntUsuario().CambiarContraseniaUsuario(idUsuario, passWordSha, "66666666xxxxx");
                        if (modelUsuario.IdUsuario == -1)
                        {
                            ViewBag.labelAPError = "Error al actualizar la nueva contraseña";
                        }
                        else
                        {
                            modelUsuario = new NegocioUsuario().ValidaUsuario(modelo.tbADominio, modelo.tbAPUsuario, passWordSha, Config.EsCallCenter);
                            Crearcookie(modelUsuario, true);
                            var mensajeServ = new EntMensajesServicios().ObtenerMensajesServicios("ActUsrContrasenia");
                            bool success = Email.EnviarEmail(modelUsuario.Email, mensajeServ.Titulo, mensajeServ.Mensaje, mensajeServ.EsHtml);
                            if (!success)
                            {
                                Logger.WriteLine(Logger.TipoTraceLog.Error, Convert.ToInt32(modelo.tbidUsuario), "HomeController", "ActualizarPassword - No se pudo mandar email a : " + modelUsuario.Email);
                            }
                            Response.Redirect(_returnUrl);
                        }
                    }
                    else
                    {
                        ViewBag.labelAPError = "Esta contraseña se ha registrado previamente, por favor intente con otra.";
                    }
                }
                else
                {
                    ViewBag.labelAPError = "Esta contraseña no cumple con el formato.";
                }
            }
            return View(modelo);
        }

        public string RandomString(int length)
        {
            const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            if (length < 0) throw new ArgumentOutOfRangeException("length", @"length cannot be less than zero.");
            if (string.IsNullOrEmpty(allowedChars)) throw new ArgumentException("allowedChars may not be empty.");

            const int byteSize = 0x100;
            var allowedCharSet = new HashSet<char>(allowedChars).ToArray();
            if (byteSize < allowedCharSet.Length) throw new ArgumentException(String.Format("allowedChars may contain no more than {0} characters.", byteSize));
            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                var result = new StringBuilder();
                var buf = new byte[128];
                while (result.Length < length)
                {
                    rng.GetBytes(buf);
                    for (var i = 0; i < buf.Length && result.Length < length; ++i)
                    {
                        var outOfRangeStart = byteSize - (byteSize % allowedCharSet.Length);
                        if (outOfRangeStart <= buf[i]) continue;
                        result.Append(allowedCharSet[buf[i] % allowedCharSet.Length]);
                    }
                }
                return result.ToString();
            }
        }

        private void Crearcookie(UsuarioModel modelUsuario, bool cbRecordarme)
        {
            
            string userData = modelUsuario.IdUsuario + "," +
                                          modelUsuario.IdRol + "," +
                                          modelUsuario.NombreRol + "," +
                                          modelUsuario.IdDominio + "," +
                                          modelUsuario.NomCorto + "," +
                                          modelUsuario.NombreDominio + "," +
                                          modelUsuario.Nombre + "," +
                                          modelUsuario.NomCorto + ","+
                                          modelUsuario.EsCallCenterOut;

            // Create a new ticket used for authentication
            var ticket = new FormsAuthenticationTicket(
                1, // Ticket version
                modelUsuario.IdUsuario.ToString(CultureInfo.InvariantCulture), // Username associated with ticket
                DateTime.Now, // Date/time issued
                cbRecordarme ? DateTime.Now.AddDays(2) : DateTime.Now.AddMinutes(30), // Date/time to expire
                true, // "true" for a persistent user cookie
                userData, // User-data, in this case the roles
                FormsAuthentication.FormsCookiePath); // Path cookie valid for

            // Encrypt the cookie using the machine key for secure transport
            var hash = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(
                FormsAuthentication.FormsCookieName, // Name of auth cookie
                hash) { Expires = ticket.Expiration }; // Hashed ticket

            // Set the cookie's expiration time to the tickets expiration time
            //if (ticket.IsPersistent)

            // Add the cookie to the list for outgoing response
            Response.Cookies.Add(cookie);
            System.Web.HttpContext.Current.Session["SessionUsuario"] = userData.Split(',');
        }

        protected string GetIpAddress()
        {
            HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }
            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        public ActionResult RecuperarContrasena(RecuperarContrasenia modelo = null)
        {
            ViewBag.LogosDir = Config.AplicacionActual().Nombre;
            ViewBag.Version = "Versión: " + Versionado.VersionActual;

            LogosDir = Config.AplicacionActual().Nombre;

            string v = Request.QueryString["key"];
            if (v == null) return View("RecuperarContrasena", modelo);
            ConexionSql conn = ConexionSql.Instance;
            DataSet ds = conn.ValidarLlaveRecuperacion(v);
            string idResult = ds.Tables[0].Rows[0]["idUsuario"].ToString();
            string key = v;
            ViewBag.Key = key;
            if (idResult == "-1") return View("RecuperarContrasena", modelo);
            string idUsuario = ds.Tables[0].Rows[0]["idUsuario"].ToString();
            var expira = (DateTime)ds.Tables[0].Rows[0]["fechaExpiracion"];
            String usada = ds.Tables[0].Rows[0]["usada"].ToString();

            if (DateTime.Compare(expira, DateTime.Now) <= 0 || !usada.Equals("False")) return View();
            ViewBag.ContainerValidKey = " block; ";
            ViewBag.ContainerInvalidKey = " none; ";

            var context = new SistemasCobranzaEntities();
            var usr = Convert.ToInt32(ds.Tables[0].Rows[0]["idUsuario"]);
            var usuario = from u in context.Usuario
                          join d in context.Dominio on u.idDominio equals d.idDominio
                          where u.idUsuario == usr
                          select new
                          {
                              u.Usuario1,
                              d.nom_corto
                          };

            if (!usuario.Any()) return View("RecuperarContrasena", modelo);

            if (modelo != null)
            {
            modelo.lblUsuario = usuario.First().Usuario1;
            modelo.lblDominio = usuario.First().nom_corto;
            }
            
            if (!ModelState.IsValid || modelo == null)
                return View("RecuperarContrasena", modelo);

            string pass = modelo.tbPassword;
            string verif = modelo.tbVerificar;
            var passRegexn = new Regex(@"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$");

            if (pass.Length <= 0 || verif.Length <= 0)
            {
                ViewBag.lblMensaje = "Por favor llene todos los campos";
                return View("RecuperarContrasena", modelo);
            }
            if (!pass.Equals(verif))
            {
                ViewBag.lblMensaje = "Las contraseñas no coinciden";
                return View("RecuperarContrasena", modelo);
            }
            if (passRegexn.IsMatch(pass))
            {
                string passEnciptado = Security.HashSHA512(pass);
                var passValido = new EntUsuario().ValidarPassBitacora(Int32.Parse(idUsuario), passEnciptado);

                if (passValido)
                {
                    var usuariomodel = new EntUsuario().CambiarContraseniaUsuario(Int32.Parse(idUsuario), passEnciptado, key);

                    if (usuariomodel.IdUsuario == -1)
                    {
                        ViewBag.lblMensaje = "Error al actualizar contraseña. Por favor intente de nuevo";
                        return View("RecuperarContrasena", modelo);
                    }
                    var mensajeServ = new EntMensajesServicios().ObtenerMensajesServicios("ActUsrContrasenia");
                    bool success = Email.EnviarEmail(usuariomodel.Email, mensajeServ.Titulo, mensajeServ.Mensaje, mensajeServ.EsHtml);
                    if (!success)
                    {
                        Logger.WriteLine(Logger.TipoTraceLog.Error, Convert.ToInt32(idUsuario), "HomeController", "RecuperarContrasena - No se pudo mandar email a :" + usuariomodel.Email);
                    }

                    ViewBag.lblMensaje = "Contraseña actualizada con éxito";

                    var modeloUsuario = new Login
                    {
                        tbDominio = modelo.lblDominio,
                        tbUsuario = modelo.lblUsuario,
                        tbPassword = modelo.tbPassword
                    };
                    Index(modeloUsuario);
                }
                else
                {
                    ViewBag.lblMensaje = "Esta contraseña se ha registrado previamente, por favor intente con otra.";
                }
            }
            else
            {
                ViewBag.lblMensaje = "Esta contraseña no cumple con el formato.";
            }
            return View("RecuperarContrasena", modelo);
        }
    }
}