using System;
using System.Web.Mvc;
using PubliPayments.Entidades;
using PubliPayments.Models;
using PubliPayments.Utiles;
using Newtonsoft.Json;

namespace PubliPayments.Controllers
{
    public class AdminPerfilController : Controller
    {
        public ActionResult Index()
        {
            var idUsuario = int.Parse(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            var modelo = new AdminPerfilModel();
            try
            {
                var usuarios = new EntUsuario().ObtenerUsuarioPorId(idUsuario);
                if (usuarios != null)
                {
                    modelo.AuUsuario = usuarios.Usuario;
                    modelo.AuEmail = usuarios.Email;
                    modelo.AuNombre = usuarios.Nombre;
                }
                else
                {
                    ViewBag.Msj = "Error intentelo nuevamente";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msj = "Error intentelo nuevamente";
                Logger.WriteLine(Logger.TipoTraceLog.Error,
                             idUsuario,
                             "AdminPerfil",
                             "Page_Load: " + ex.Message + (ex.InnerException != null ? " - Inner: " + ex.InnerException.Message : ""));
            }

            return View(modelo);
        }

        public ActionResult ActualizarPerfil(AdminPerfilModel modelo)
        {
            var nombre = modelo.AuNombre;
            var password = modelo.AuPassword;
            var email = modelo.AuEmail;
            var nPassword = modelo.AuNuevoPassword;
            var confirmar = modelo.AuConfirmarPassword;
            var idUsuario = int.Parse(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            var passEnciptado = Security.HashSHA512(password);
            var npassEnciptado = Security.HashSHA512(nPassword);
            var msj = "";
            if (password == "" || nombre == "" || email == "") return Content(msj);
            try
            {
                var usuarioEmail = new EntUsuario().ObtenerUsuarioPorEmail(email);
                if (usuarioEmail != null)
                {
                    if (usuarioEmail.idUsuario != idUsuario)
                    {
                        return Content("El email ya se encuentra registrado en el sistema");
                    }
                }
                var usuario = new EntUsuario().ObtenerUsuarioPorId(idUsuario);
                if (usuario != null && usuario.Password == passEnciptado)
                {
                    if (nPassword == "" || (nPassword == confirmar))
                    {
                        if (nPassword != "")
                        {
                            var passValido = new EntUsuario().ValidarPassBitacora(idUsuario, npassEnciptado);
                            if (passValido)
                            {
                                if (string.IsNullOrEmpty(nPassword) && string.IsNullOrEmpty(confirmar))
                                {
                                    msj = ActualizarDatos(idUsuario, nombre, email);
                                }
                                else
                                {
                                    var modelUsuario = new EntUsuario().CambiarContraseniaUsuario(idUsuario, npassEnciptado, "66666666xxxxx");
                                    msj = modelUsuario.IdUsuario == -1 ? "Error al actualizar los datos" : ActualizarDatos(idUsuario, nombre, email);
                                }
                            }
                            else
                            {
                                msj = "Esta contraseña se ha registrado previamente";
                            }
                        }
                        else
                        {
                            msj = ActualizarDatos(idUsuario, nombre, email);
                        }
                    }
                    else
                    {
                        msj = "La nueva contraseña y la confirmación no coinciden";
                    }
                }
                else
                {
                    msj = "Contraseña actual incorrecta";
                }
            }
            catch (Exception)
            {
                msj = "Error intentelo nuevamente";
            }
            return Content(msj);
        }

        public ActionResult ObtieneUsuario()
        {
            var idUsuario = int.Parse(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            var usuario = new EntUsuario().ObtenerUsuarioPorId(idUsuario);
            return Content(JsonConvert.SerializeObject(usuario));
        }

        private string ActualizarDatos(int idUsuario, string nombre, string email)
        {
            string msj;
            var actUsuario = new EntUsuario().ActualizarUsuario(idUsuario, nombre, email, null, null, null);
            if (actUsuario != null)
            {
                msj = "Se han actualizado los datos correctamente.";
                var mensajeServ = new EntMensajesServicios().ObtenerMensajesServicios("ActUsrDatos");
                mensajeServ.Mensaje = string.Format(mensajeServ.Mensaje, actUsuario.Usuario1, actUsuario.Email);

                Email.EnviarEmail(actUsuario.Email, mensajeServ.Titulo, mensajeServ.Mensaje, mensajeServ.EsHtml);
            }
            else
            {
                msj = "Error al actualizar datos de usuario.";
            }

            return msj;
        }
    }
}
