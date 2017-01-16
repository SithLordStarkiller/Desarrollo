using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using PubliPayments.Entidades;
using PubliPayments.Utiles;

namespace PubliPayments
{
    public partial class ActualizarDatosUsuario : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int idUsuario = int.Parse(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            if (!IsPostBack)
            {
                try
                {
                    var usuarios = new EntUsuario().ObtenerUsuarioPorId(idUsuario);
                    if (usuarios != null)
                    {
                        AUUsuario.Text = usuarios.Usuario;
                        AUEmail.Text = usuarios.Email;
                        AUNombre.Text = usuarios.Nombre;
                    }
                    else
                    {
                        lblInformacion.Text = "Error intentelo nuevamente";
                        lblInformacion.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    lblInformacion.Text = "Error intentelo nuevamente";
                    lblInformacion.Visible = true;
                    Logger.WriteLine(Logger.TipoTraceLog.Error,
                                 idUsuario,
                                 "AdminPerfil",
                                 "Page_Load: " + ex.Message + (ex.InnerException != null ? " - Inner: " + ex.InnerException.Message : ""));
                }
            }
        }

        protected void btActualizarO_Click(object sender, EventArgs e)
        {
            string nombre = AUNombre.Text;
            string password = AUPassword.Text;
            string email = AUEmail.Text;
            string nPassword = AUNuevoPassword.Text;
            string confirmar = AUConfirmarPassword.Text;
            string passEnciptado = "", npassEnciptado="";
            int idUsuario = int.Parse(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            lblInformacion.Text = "";
            passEnciptado = Security.HashSHA512(password);
            npassEnciptado = Security.HashSHA512(nPassword);
            if (password != "" && nombre != "" && email != "")
            {
                try
                {
                    var usuarioEmail = new EntUsuario().ObtenerUsuarioPorEmail(email);
                    if (usuarioEmail != null)
                    {
                        if (usuarioEmail.idUsuario != idUsuario)
                        {
                            lblInformacion.Text = "El email ya se encuentra regisrado en el sistema";
                            return;
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
                                    var modelUsuario = new EntUsuario().CambiarContraseniaUsuario(idUsuario, npassEnciptado, "66666666xxxxx");
                                    if (modelUsuario.IdUsuario==-1)
                                    {
                                        lblInformacion.Text = "Error al actualizar los datos";
                                    }
                                    else
                                    {
                                        ActualizarDatos(idUsuario, nombre, email);
                                    }
                                }
                                else
                                {
                                    lblInformacion.Text = "Esta contraseña se ha registrado previemente.";
                                }
                            }
                            else
                            {
                                ActualizarDatos(idUsuario, nombre, email);
                            }
                        }
                        else
                        {
                            lblInformacion.Text = "La nueva contraseña y la confirmacion no coinciden.";
                        }
                    }
                    else
                    {
                        lblInformacion.Text = "Contraseña actual incorrecta.";
                    }
                }
                catch (Exception)
                {
                    lblInformacion.Text = "Error intentelo nuevamente";
                    lblInformacion.Visible = true;
                }
                lblInformacion.Visible = lblInformacion.Text != "";
            }
        }

        private void ActualizarDatos(int idUsuario, string nombre, string email)
        {
            var actUsuario = new EntUsuario().ActualizarUsuario(idUsuario, nombre, email, null, null, null);
            if (actUsuario != null)
            {
                lblInformacion.Text = "Se han actualizado los datos correctamente.";
                var mensajeServ = new EntMensajesServicios().ObtenerMensajesServicios("ActUsrDatos");
                mensajeServ.Mensaje = string.Format(mensajeServ.Mensaje, actUsuario.Usuario1, actUsuario.Email);

                Email.EnviarEmail(actUsuario.Email, mensajeServ.Titulo, mensajeServ.Mensaje,mensajeServ.EsHtml);
            }
            else
            {
                lblInformacion.Text = "Error al actualizar datos de usuario.";
            }
        }
    }
}