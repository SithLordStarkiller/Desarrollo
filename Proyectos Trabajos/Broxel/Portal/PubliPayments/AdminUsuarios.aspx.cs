using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Web.UI;
using PubliPayments.Entidades;
using PubliPayments.Negocios;
using PubliPayments.Utiles;

namespace PubliPayments
{
    public partial class AdminUsuarios : Page
    {
        protected int RolActual, RolHijo;
        private readonly SistemasCobranzaEntities _ctx = new SistemasCobranzaEntities();
        public string NombreDespacho = "";
        public int Aplicacion = Config.AplicacionActual().idAplicacion;
        protected void Page_Load(object sender, EventArgs e)
        {
            RolActual = int.Parse(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol));
            int dominio = Int32.Parse(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdDominio));
            lblInformacion.Text = "";
            lblInformacion.Visible = lblInformacion.Text != "";
            int despachoActual = ObtenerDomino();
            NombreDespacho = (_ctx.Dominio.FirstOrDefault(x => x.idDominio == despachoActual)) != null ? _ctx.Dominio.FirstOrDefault(x => x.idDominio == despachoActual).nom_corto : "";
            RolHijo = int.Parse(Request.QueryString["Rol"] ?? "-1");
            if (RolActual == 4 || RolActual == 5)
            {
                Response.Redirect("unauthorized.aspx");
            }
            else if (RolActual != 0 && RolActual != 1)
            {
                if (despachoActual != dominio)
                    Response.Redirect("~/AdminUsuarios.aspx?Despacho=" + dominio);
            }
            if (RolActual == 1 && RolHijo != 1 && RolHijo != 5)
            {
                Response.Redirect("~/AdminUsuarios.aspx?Rol=1");
            }
            try
            {
                var listaRoles = from r in _ctx.Roles
                                 where r.idRol == 2 || r.idRol == 3 || r.idRol == 4
                                 select r;
                NURolHijo.DataSource = listaRoles.ToList();
                NURolHijo.DataBind();

                CargarListas();
            }
            catch (Exception)
            {
                lblInformacion.Text = "Error intentelo nuevamente.";
                lblInformacion.Visible = true;
            }

            //txtIdUsuario.Attributes.Add("readonly", "true");

        }

        protected void CargarListas()
        {
            int despachoActual = ObtenerDomino();
            var listAdminDisponibles = from u in _ctx.Usuario
                                       where u.idRol == 2
                                             && u.idDominio == despachoActual
                                             && u.Estatus != 0 
                                       select u;
            ListAdminU.DataSource = listAdminDisponibles.ToList();
            ListAdminU.DataBind();

            var listSupervisorDisponibles = from u in _ctx.Usuario
                                            where u.idRol == 3
                                                  && u.idDominio == despachoActual
                                                  && u.Estatus != 0 
                                            select u;
            ListSupervisorU.DataSource = listSupervisorDisponibles.ToList();
            ListSupervisorU.DataBind();

            var listDelegacion = from cD in _ctx.CatDelegaciones
                                 select cD;
            ListCatDelegacion.DataSource = listDelegacion.ToList();
            ListCatDelegacion.DataBind();
        }

        protected void NUAltaUsuario_Click(object sender, EventArgs e)
        {
            string rolesH = NURolesH.Text != ""
                ? NURolesH.Text
                : RolHijo != -1
                    ? RolHijo.ToString(CultureInfo.InvariantCulture)
                    : RolActual == 2 ? "3" : RolActual == 3 ? "4" : "5";
            string usuarioH = NUUsuario.Text;
            string nombreH = NUNombre.Text;
            string emailH = NUEmail.Text.Trim();
            string idPadre = (rolesH == "3" && RolActual == 0)
                ? NUPadreAdmin.Text
                : rolesH == "4" && RolActual == 0 ? NUPadreSuper.Text : (SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            string delegacion = DelegacionH.Text;
            int esCallCenter = int.Parse(NUEsCallCenter.SelectedValue);
            Usuarios user = new Usuarios();

            if (RolActual != 0)
            {
                lblInformacion.Text = "No se tienen los privilegios para realizar esta actividad.";
                TablaUsuarios.EscondeMensaje();
                PopUPUsuario.ShowOnPageLoad = false;
                lblInformacion.Visible = true;
                return;
            }

            if (rolesH != "" && usuarioH != "" && nombreH != "" && emailH != "")
            {
                int dominio = ObtenerDomino();
                var bUsuario = (from u in _ctx.VUsuarios
                                where u.idDominio == dominio
                                      && u.Usuario.ToUpper() == usuarioH.ToUpper()
                                      || u.Email.ToUpper() == emailH.ToUpper()
                                select u).FirstOrDefault();

                if (bUsuario == null)
                {
                    var condiciones = new[,] { { "1", "MA" }, { "1", "CE" }, { "1", "NU" }, { "1", "MI" }, { "4", "NA" } };
                    var passRdm = Security.GeneratePassWord(condiciones);

                    DataSet dsUsuario = user.InsertaUSuario(idPadre, ObtenerDomino().ToString(CultureInfo.InvariantCulture), rolesH, usuarioH, nombreH, passRdm[1], emailH, esCallCenter);
                    if (dsUsuario.Tables.Count > 0 && dsUsuario.Tables[0].Rows.Count > 0)
                    {
                        if (rolesH == "5")
                        {
                            var relacionDelegacion = new RelacionDelegaciones
                            {
                                idUsuario = Int32.Parse(dsUsuario.Tables[0].Rows[0]["idUsuario"].ToString()),
                                Delegacion = delegacion
                            };
                            _ctx.RelacionDelegaciones.Add(relacionDelegacion);
                            _ctx.SaveChanges();
                        }
                        var mensajeServ = new EntMensajesServicios().ObtenerMensajesServicios("NuevoUsr");
                        mensajeServ.Mensaje = string.Format(mensajeServ.Mensaje, NUUsuario.Text, passRdm[0], Request.Url.GetLeftPart(UriPartial.Authority));

                        Email.EnviarEmail(NUEmail.Text, mensajeServ.Titulo, mensajeServ.Mensaje, mensajeServ.EsHtml);
                        lblInformacion.Text = "Usuario creado correctamente." ;
                        TablaUsuarios.CargaUsuarios(RolActual, ObtenerDomino(), int.Parse(idPadre), RolHijo);
                        TablaUsuarios.EscondeMensaje();
                    }
                }
                else
                {
                    if (bUsuario.Usuario.ToUpper() == usuarioH.ToUpper())
                    {

                        lblInformacion.Text = "El nombre de usuario " + usuarioH + " ya se encuentra registrado para este despacho.";
                    }
                    else if (bUsuario.Email.ToUpper() == emailH.ToUpper())
                    {
                        lblInformacion.Text = "El email " + emailH + " ya se encuentra registrado en el sistema.";
                    }
                    TablaUsuarios.EscondeMensaje();
                }
            }
            PopUPUsuario.ShowOnPageLoad = false;
            lblInformacion.Visible = lblInformacion.Text != "";
            CargarListas();
        }

        protected int ObtenerDomino()
        {
            int dominio = 0;
            try
            {
                dominio = int.Parse(Request.QueryString["Despacho"] ?? SessionUsuario.ObtenerDato(SessionUsuarioDato.IdDominio));
                if (RolHijo != 0)
                    dominio = dominio == 1 ? 2 : dominio;
            }
            catch (Exception)
            {
            }
            return dominio;
        }

        protected void btnEditaUsuario_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    var usuario = txtIdUsuario.Text;
            //    var nombre = txtNombre.Text;
            //    var correo = txtEmail.Text;
            //    string rolesH = NURolesH.Text != ""
            //        ? NURolesH.Text
            //        : RolHijo != -1
            //            ? RolHijo.ToString(CultureInfo.InvariantCulture)
            //            : RolActual == 2 ? "3" : RolActual == 3 ? "4" : "5";
            //    string idPadre = (rolesH == "3" && RolActual == 0) ? NUPadreAdmin.Text : rolesH == "4" && RolActual == 0
            //            ? NUPadreSuper.Text : (SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));

            //    lblInformacion.Visible = false;
            //    if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(correo))
            //        return;

            //    if (RolActual != 0)
            //    {
            //        lblInformacion.Text = @"No se tienen los privilegios para realizar esta actividad.";
            //        TablaUsuarios.EscondeMensaje();
            //        PopupEditar.ShowOnPageLoad = false;
            //        lblInformacion.Visible = true;
            //        return;
            //    }

            //    int dominio = ObtenerDomino();
            //    var usuarioEditar = (from u in _ctx.Usuario
            //                         where u.idDominio == dominio
            //                               && u.Usuario1.ToUpper() == usuario.ToUpper()
            //                         select u).FirstOrDefault();

            //    if (usuarioEditar != null)
            //    {
            //        usuarioEditar.Email = correo;
            //        usuarioEditar.Nombre = nombre;
            //        _ctx.SaveChanges();
            //    }

            //    lblInformacion.Text = @"Usuario guardado correctamente.";
            //    PopupEditar.ShowOnPageLoad = false;
            //    lblInformacion.Visible = true;
            //    TablaUsuarios.EscondeMensaje();
            //    TablaUsuarios.CargaUsuarios(RolActual, ObtenerDomino(), int.Parse(idPadre), RolHijo);
            //}
            //catch (Exception ex)
            //{
            //    lblInformacion.Text = @"Error intentelo nuevamente.";
            //    lblInformacion.Visible = true;
            //}
        }
    }
}