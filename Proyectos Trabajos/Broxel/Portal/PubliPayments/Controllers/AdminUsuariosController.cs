using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DevExpress.Data.PLinq.Helpers;
using DevExpress.Office.Utils;
using PubliPayments.Entidades;
using PubliPayments.Models;
using PubliPayments.Utiles;

namespace PubliPayments.Controllers
{
    public class AdminUsuariosController : Controller
    {
        //private readonly SistemasCobranzaEntities _ctx = new SistemasCobranzaEntities();
        public int Aplicacion = Config.AplicacionActual().idAplicacion;
        /// <summary>
        /// Index del administrador de usuario
        /// </summary>
        /// <returns>Vista Index</returns>
        public ActionResult Index()
        {
            var rolHijo = int.Parse(Request.QueryString["Rol"] ?? "-1");
            var rolActual = int.Parse(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol));
            var dominio = Int32.Parse(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdDominio));
            var despachoActual = ObtenerDomino(rolHijo);

            var nombreDespacho = new EntAdminDespacho().ObtenerDatosDominio(despachoActual).nom_corto;

            ViewBag.TipoAdmin = rolActual == 2
                ? "Supervisores"
                : rolActual == 3
                    ? "Gestores"
                    : rolHijo == 0
                        ? "Super Administradores"
                        : rolHijo == 1
                            ? (Aplicacion == 1 ? "Admin Infonavit" : "Admin General")
                            : rolHijo == 5 ? "Delegaciones" : rolHijo == 6 ? "Directores" : "usuarios";
            ViewBag.RolActual = rolActual;
            ViewBag.RolHijo = int.Parse(Request.QueryString["Rol"] ?? "-1");

            if (rolActual == 4 || rolActual == 5)
            {
                Response.Redirect("unauthorized.aspx");
            }
            else if (rolActual != 0 && rolActual != 1)
            {
                if (despachoActual != dominio)
                    Response.Redirect("/AdminUsuarios.aspx?Despacho=" + dominio);
            }
            if (rolActual == 1 && rolHijo != 1 && rolHijo != 5)
            {
                Response.Redirect("/AdminUsuarios.aspx?Rol=1");
            }

            try
            {

            }
            catch (Exception ex)
            {

            }


            return View();
        }

        /// <summary>
        /// Obtiene el dominio del rol que se elije en el menu
        /// </summary>
        /// <returns>id de dominio</returns>
        protected int ObtenerDomino(int rolHijo)
        {
            var dominio = 0;
            //var rolHijo = int.Parse(Request.QueryString["Rol"] ?? "-1");
            try
            {
                dominio = int.Parse(Request.QueryString["Despacho"] ?? SessionUsuario.ObtenerDato(SessionUsuarioDato.IdDominio));
                if (rolHijo != 0)
                    dominio = dominio == 1 ? 2 : dominio;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "AdminUsuarios", "ObtenerDomino - Error: " + ex.Message);
            }
            return dominio;
        }

        /// <summary>
        /// Popup para editar datos del usuario 
        /// </summary>
        /// <param name="modelo">recibe clase AdminUsuariosModel</param>
        /// <returns>Vista</returns>
        public ActionResult PopupEditar(AdminUsuariosModel modelo)
        {
            var idPadre = int.Parse(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            try
            {
               
                if (modelo != null)
                {
                    var idUsuario = modelo.idUsuario;
                    var usuario = modelo.Usuario;
                    var nombre = modelo.Nombre;
                    var correo = modelo.Correo;
                    var esCallCenter = modelo.EsCallCenter;
                    var datosUsuario = new EntUsuario().ObtenerUsuarioPorId(modelo.idUsuario.ToString());

                    if (!ModelState.IsValid)
                    {
                        modelo.Nombre = datosUsuario.Nombre;
                        modelo.Correo = datosUsuario.Email;
                        modelo.Usuario = datosUsuario.Usuario;
                        modelo.EsCallCenter = Convert.ToInt32(datosUsuario.EsCallCenterOut);
                        modelo.Accion = "editar";
                        return View(modelo);
                    }

                    if (modelo.Accion == "editar")
                    {
                        Logger.WriteLine(Logger.TipoTraceLog.Trace, idPadre, "AdminUsuarios", "PopupEditar -Actulizando idusuario: " + modelo.idUsuario + " nombre: " + nombre + " correo: " + correo);

                        var actualizo = new EntUsuario().CambiarDatosUsuarioPoridUsuario(idUsuario, modelo.Nombre, modelo.Correo, datosUsuario.Password, datosUsuario.Estatus, esCallCenter);
                        if (actualizo == false)
                        {
                            Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "AdminUsuarios", "PopupEditar - Error al actualizar idUsuario " + idPadre + " el de " + modelo.idUsuario + ", Actualizado");
                            return View();
                        }

                            var mensajeServ = new EntMensajesServicios().ObtenerMensajesServicios("ActUsrDatos");
                            mensajeServ.Mensaje = string.Format(mensajeServ.Mensaje, nombre, correo);

                            Email.EnviarEmail(correo, mensajeServ.Titulo, mensajeServ.Mensaje, mensajeServ.EsHtml);
                            Logger.WriteLine(Logger.TipoTraceLog.Trace, 0, "AdminUsuarios", "PopupEditar - Actulizo idUsuario " + idPadre + " el de " + modelo.idUsuario + ", Actualizado");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, idPadre, "AdminUsuarios", "PopupEditar: " + ex.Message);
            }
            return View();
        }

        /// <summary>
        /// Valida que el correo no este asignado a otro usuario en el sistema excluyendo el de si mismo
        /// </summary>
        /// <param name="correo">correo que se validara</param>
        /// <param name="usuario">usuario que se excluira</param>
        /// <returns>Ok o error en caso de que el correo exista en otro usuario</returns>
        public ActionResult validarCorreo(string correo,string usuario)
        {
            try
            {
                var usuarioEmail = new EntUsuario().ObtenerUsuarioPorEmail(correo);
                
                if (usuarioEmail != null)
                {
                    if (usuarioEmail.Usuario != usuario)
                    {
                        return Content("El email ya se encuentra registrado en el sistema");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine(Logger.TipoTraceLog.Error, 0, "AdminUsuarios", "validarCorreo: " + ex.Message);
            }
            return Content("OK");
        }

        /// <summary>
        /// Llamado para llenar la grid de los usuarios
        /// </summary>
        /// <returns>Vista de la grid</returns>
        public ActionResult LbUsuariosLondon()
        {



            return PartialView("lbUsuariosLondon");
        }
    }
}
