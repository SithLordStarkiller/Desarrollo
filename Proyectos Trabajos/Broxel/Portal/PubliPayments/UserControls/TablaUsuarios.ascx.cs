using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using PubliPayments.Entidades;
using PubliPayments.Negocios;
using PubliPayments.Utiles;

namespace PubliPayments.UserControls
{
    public partial class TablaUsuarios : UserControl
    {
        private readonly SistemasCobranzaEntities _ctx = new SistemasCobranzaEntities();
        protected int _rol;
        private int _idPadre;
        private int _rolHijo;

        protected void Page_Load(object sender, EventArgs e)
        {
            _rol = int.Parse(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol));
            _rolHijo = int.Parse(Request.QueryString["Rol"] ?? "-1");
            int dominio = ObtenerDomino();
            _idPadre = int.Parse(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
            CargaUsuarios(_rol, dominio, _idPadre, _rolHijo);
            var listDespacho = _ctx.Dominio.FirstOrDefault(x => x.idDominio == dominio);
            NDespacho.Text = listDespacho != null ? listDespacho.nom_corto : "";

            if (!Page.IsPostBack && _rolHijo == 5)
            {
                var col = new GridViewDataColumn("Delegacion", "Delegación") { VisibleIndex = 5 };
                lbUsuariosLondon.Columns.Add(col);
            }
        }

        #region ResetDeContraseña
        protected void btnReset_OnClick(object sender, EventArgs e)
        {
            var btn = sender as ASPxButton;
            var condiciones = new[,] { { "1", "MA" }, { "1", "CE" }, { "1", "NU" }, { "1", "MI" }, { "4", "NA" } };

            var password =Security.GeneratePassWord(condiciones);
            int idUsuario = Int32.Parse(btn.CommandArgument != "" ? btn.CommandArgument : "0");
            if (idUsuario != 0)
            {
                var usuario = new EntUsuario().ObtenerUsuarioPorId(idUsuario);
                if (usuario != null)
                {

                    var modelUsuario = new EntUsuario().CambiarContraseniaUsuario(idUsuario, password[1], "66666666xxxxx");
                    
                    if (modelUsuario.IdUsuario==-1)
                    {
                        lblInformacionTabla.Text = "Error al actualizar la contraseña del usuario.";
                    }
                    else
                    {
                        lblInformacionTabla.Text = "La contraseña ha sido cambiada exitosamente";
                        new EntUsuario().ActualizarUsuario(idUsuario, null, null, null, 3, null);
                        var mensajeServ = new EntMensajesServicios().ObtenerMensajesServicios("ResetUsrContrasenia");
                        mensajeServ.Mensaje = string.Format(mensajeServ.Mensaje, password[0]);
                        Email.EnviarEmail(usuario.Email, mensajeServ.Titulo, mensajeServ.Mensaje, mensajeServ.EsHtml);
                    }
                }
            }
            lblInformacionTabla.Visible = lblInformacionTabla.Text != "";
        }
        #endregion

        #region BajaDeUsuario

        /// <summary>
        /// Se en carga de activar o desactivar algún usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBAJA_OnClick(object sender, EventArgs e)
        {
            var btn = sender as ASPxButton;
            string[] argumentos = (btn.CommandArgument).Split(',');
            int idUsuario = int.Parse(argumentos[0]);
            ConexionSql cnnSql = ConexionSql.Instance;
            var entFormulario= new EntFormulario().ObtenerListaFormularios("");// como es una cancelación no importa el tipo de formulario.

          
            if (idUsuario != 0)
            {
                if (_rol == 3)
                {
                    return;
                }

                var usuarioCtx = (from u in _ctx.Usuario
                                  where u.idUsuario == idUsuario
                                  select u).FirstOrDefault();
                if (usuarioCtx != null)
                {
                    string usuario = usuarioCtx.Usuario1;
                    string idRol = usuarioCtx.idRol.ToString(CultureInfo.InvariantCulture);
                    if (argumentos[1] == "0")
                    {
                        usuarioCtx.Estatus = 1;
                        try
                        {
                            _ctx.SaveChanges();
                            lblInformacionTabla.Text = "El usuario " + usuario + " ha sido reactivado exitosamente.";
                            CargaUsuarios(_rol, ObtenerDomino(), _idPadre, _rolHijo);
                        }
                        catch (Exception ex)
                        {
                            lblInformacionTabla.Text = "Error al reactivar a " + usuario + "..." + ex.Message;
                        }
                    }
                    else
                    {
                        DataSet vBaja = cnnSql.ValidarBaja(idUsuario);
                        if (vBaja.Tables.Count > 0 && vBaja.Tables[0].Rows.Count > 0)
                        {
                            if (Convert.ToInt32(vBaja.Tables[0].Rows[0]["Validar"]) > 0)
                            {
                                try
                                {
                                    usuarioCtx.Estatus = 0;
                                    _ctx.SaveChanges();

                                    int contadorOrdenesCanceladas = 0;
                                    if (idRol == "4" || idRol == "3")
                                    {
                                        if (idRol == "4")
                                        {
                                            var ordenes = (from o in _ctx.Ordenes
                                                           where o.Estatus == 1
                                                           && o.idUsuario == idUsuario
                                                           select o.idOrden).ToList();

                                            var orden = new Orden(3, Config.AplicacionActual().Productivo, Config.AplicacionActual().ClientId, Config.AplicacionActual().ProductId, entFormulario[0].Nombre, entFormulario[0].Version,null);
                                            int totOrdenes = ordenes.Count;

                                            for (int i = 0; i < totOrdenes; i += 40)
                                            {
                                                int restantes = totOrdenes - i;
                                                var ordn = (restantes <= 40) ? ordenes.GetRange(i, restantes) : ordenes.GetRange(i, 40);
                                                contadorOrdenesCanceladas += orden.Cancelar(ordn, Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario)));
                                            }
                                        }
                                        else
                                        {
                                            var ordenes = (from o in _ctx.Ordenes
                                                           where o.Estatus == 1
                                                           && o.idUsuarioPadre == idUsuario
                                                           select o.idOrden).ToList();

                                            var orden = new Orden(0, Config.AplicacionActual().Productivo, Config.AplicacionActual().ClientId, Config.AplicacionActual().ProductId, entFormulario[0].Nombre, entFormulario[0].Version,null);
                                            int totOrdenes = ordenes.Count;

                                            for (int i = 0; i < totOrdenes; i += 40)
                                            {
                                                int restantes = totOrdenes - i;
                                                var ordn = (restantes <= 40) ? ordenes.GetRange(i, restantes) : ordenes.GetRange(i, 40);
                                                contadorOrdenesCanceladas += orden.Cancelar(ordn, Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario)));
                                            }
                                        }
                                        if (contadorOrdenesCanceladas == -1)
                                        {
                                            lblInformacionTabla.Text = "Error al intentar dar de baja las ordenes en el dispositivo. ";
                                            return;
                                        }
                                        if (contadorOrdenesCanceladas == -2 || contadorOrdenesCanceladas == 0)
                                        {
                                            lblInformacionTabla.Text = "Error al intentar dar de baja las ordenes. ";
                                        }
                                    }

                                    if (contadorOrdenesCanceladas == 0)
                                        lblInformacionTabla.Text = "El usuario " + usuario + " se ha dado de baja exitosamente.";
                                    else
                                    {
                                        if (idRol == "4")
                                        {
                                            if (contadorOrdenesCanceladas == 1)
                                                lblInformacionTabla.Text = "El gestor " + usuario + " se ha dado de baja exitosamente, se desasignó 1 orden.";
                                            else
                                                lblInformacionTabla.Text = "El gestor " + usuario + " se ha dado de baja exitosamente, se desasignaron " + contadorOrdenesCanceladas + " ordenes.";
                                        }
                                        else
                                        {
                                            if (contadorOrdenesCanceladas == 1)
                                                lblInformacionTabla.Text = "El supervisor " + usuario + " se ha dado de baja exitosamente, se canceló 1 orden.";
                                            else
                                                lblInformacionTabla.Text = "El supervisor " + usuario + " se ha dado de baja exitosamente, se cancelaron " + contadorOrdenesCanceladas + " ordenes.";
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    lblInformacionTabla.Text = "Error... " + usuario + "..." + ex.Message;
                                }
                                CargaUsuarios(_rol, ObtenerDomino(), _idPadre, _rolHijo);
                            }
                            else
                            {
                                lblInformacionTabla.Text = "El usuario tiene dependencias con otros usuarios, tendra que reasignar antes de realizar la baja.";
                            }
                        }
                    }

                }
                lblInformacionTabla.Visible = lblInformacionTabla.Text != "";
            }
        }
        #endregion

        protected void ddlDespacho_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover",
                    "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#ceedfc'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle");
            }
        }

        public int ObtenerDomino()
        {
            int dominio = 0;
            try
            {
                dominio = int.Parse(Request.QueryString["Despacho"] ?? SessionUsuario.ObtenerDato(SessionUsuarioDato.IdDominio));
            }
            catch (Exception ex)
            { System.Diagnostics.Trace.WriteLine(ex.Message); }
            return dominio;
        
        }

        public int ObtenerRol()
        {
            int rol = -1;
            try
            {
                rol = int.Parse(Request.QueryString["Despacho"] ?? SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol));
            }
            catch (Exception ex)
            { System.Diagnostics.Trace.WriteLine(ex.Message); }
            return rol;
        }

        protected void btLimpiar_OnClick(object sender, EventArgs e)
        {
            Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
        }

        #region LlenadoDeTabla

        public void CargaUsuarios(int rol, int dominio, int idPadre, int rolHijo)
        {
            try
            {

                
                switch (rol)
                {
                    case 0:
                        var listaUAdmin =
                            (rolHijo > 0)
                                ? from u in _ctx.VUsuarios
                                  where u.idRol == rolHijo
                                        && u.idDominio == 2
                                  orderby u.Estatus descending
                                  select u
                                : (rolHijo == 0)
                                    ? from u in _ctx.VUsuarios
                                      where u.idRol == rolHijo
                                            && u.idUsuario != idPadre
                                            && u.idDominio == dominio
                                      orderby u.Estatus descending
                                      select u
                                    : from u in _ctx.VUsuarios
                                      where u.idDominio == dominio
                                      orderby u.Estatus descending
                                      select u;


                        if (rolHijo == 5)
                        {

                            var listaDelegaciones = from l in listaUAdmin
                                                    join rd in _ctx.RelacionDelegaciones on l.idUsuario equals rd.idUsuario
                                                    join d in _ctx.CatDelegaciones on rd.Delegacion equals d.Delegacion
                                                    select new
                                                    {
                                                        l.NombreDominio,
                                                        l.Usuario,
                                                        l.Nombre,
                                                        l.Email,
                                                        l.idUsuario,
                                                        l.idRol,
                                                        l.Estatus,
                                                        l.EstatusTxt,
                                                        l.NombreRol,
                                                        l.Padre,
                                                        Delegacion = d.Descripcion
                                                    };

                            lbUsuariosLondon.DataSource = listaDelegaciones.ToList();
                        }
                        else
                        {
                            lbUsuariosLondon.DataSource = listaUAdmin.ToList();
                        }



                        break;
                    case 1:
                        var listaUAdminL = from u in _ctx.VUsuarios
                                           where u.idDominio == 2
                                                 && u.idRol == rolHijo
                                                 && u.idUsuario != idPadre
                                           orderby u.Estatus descending
                                           select u;

                        if (rolHijo == 5)
                        {

                            var listaDelegaciones = from l in listaUAdminL
                                                    join rd in _ctx.RelacionDelegaciones on l.idUsuario equals rd.idUsuario
                                                    join d in _ctx.CatDelegaciones on rd.Delegacion equals d.Delegacion
                                                    select new
                                                    {
                                                        l.NombreDominio,
                                                        l.Usuario,
                                                        l.Nombre,
                                                        l.Email,
                                                        l.idUsuario,
                                                        l.idRol,
                                                        l.Estatus,
                                                        l.EstatusTxt,
                                                        l.NombreRol,
                                                        l.Padre,
                                                        Delegacion = d.Descripcion
                                                    };

                            lbUsuariosLondon.DataSource = listaDelegaciones.ToList();
                        }
                        else
                        {
                            lbUsuariosLondon.DataSource = listaUAdminL.ToList();
                        }

                        break;
                    default:
                        var listaUOtros = from u in _ctx.VUsuarios
                                          join r in _ctx.RelacionUsuarios
                                              on u.idUsuario equals r.idHijo
                                          where r.idPadre == idPadre
                                          orderby u.Estatus descending
                                          select new
                                          {
                                              u.NombreDominio,
                                              u.Usuario,
                                              u.Nombre,
                                              u.Email,
                                              u.idUsuario,
                                              u.idRol,
                                              u.Estatus,
                                              u.EstatusTxt,
                                              u.NombreRol,
                                              u.Padre
                                          };
                        lbUsuariosLondon.DataSource = listaUOtros.ToList();
                        break;
                }
                lbUsuariosLondon.Columns[0].Visible = false;
                if (_rol != 0 && _rol != 1 && _rol != 2 || (rolHijo >= 0 && rolHijo != 3 && rolHijo != 2))
                {
                    lbUsuariosLondon.Columns[8].Visible = false;
                }
                if (_rol != 0 && _rol != 1 || rolHijo > 0)
                {
                    lbUsuariosLondon.Columns[7].Visible = false;
                }

                if (_rol != 0)
                {
                    lbUsuariosLondon.Columns[11].Visible = false;
                }

                lbUsuariosLondon.DataBind();

            }
            catch (Exception ex)
            { System.Diagnostics.Trace.WriteLine(ex.Message); }

        }

        public void CargaUsuariosReasignar(int dominio, int idPadre)
        {
            var listaReasignar = from u in _ctx.VUsuarios
                                 join r in _ctx.RelacionUsuarios
                                     on u.idUsuario equals r.idHijo
                                 join padre in _ctx.Usuario on r.idPadre equals padre.idUsuario into padreJoin
                                 from padreLeft in padreJoin.DefaultIfEmpty()

                                 where r.idPadre == idPadre
                                 select new
                                 {
                                     u.NombreDominio,
                                     u.Usuario,
                                     u.Nombre,
                                     u.Email,
                                     u.idUsuario,
                                     u.idRol,
                                     u.Estatus,
                                     u.EstatusTxt,
                                     u.NombreRol,
                                     Padre = padreLeft.Usuario1
                                 };
            lbUsuariosLondon.DataSource = listaReasignar.ToList();
            lbUsuariosLondon.Columns[0].Visible = true;
            lbUsuariosLondon.Columns[8].Visible = false;
            lbUsuariosLondon.Columns[9].Visible = false;
            lbUsuariosLondon.Columns[10].Visible = false;
            lbUsuariosLondon.DataBind();
        }
        #endregion

        protected void lbUsuariosLondon_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
        {
            var grid = sender as ASPxGridView;
            bool isRow1Selected = grid != null && grid.Selection.IsRowSelectedByKey(e.GetRow1Value(grid.KeyFieldName));
            bool isRow2Selected = grid != null && grid.Selection.IsRowSelectedByKey(e.GetRow2Value(grid.KeyFieldName));
            e.Handled = isRow1Selected != isRow2Selected;
            if (e.Handled)
            {
                if (e.SortOrder == DevExpress.Data.ColumnSortOrder.Descending)
                    e.Result = isRow1Selected ? 1 : -1;
                else
                    e.Result = isRow1Selected ? -1 : 1;
            }
        }

        protected void btnReasignar_OnClick(object sender, EventArgs e)
        {
            var btn = sender as ASPxButton;
            int idUsuario = Int32.Parse(btn.CommandArgument != "" ? btn.CommandArgument : "0");
            int dominio = ObtenerDomino();

            var usuario = _ctx.Usuario.FirstOrDefault(x => x.idUsuario == idUsuario);

            if (usuario != null)
            {
                if (usuario.idRol != 3 && usuario.idRol != 2)
                {
                    lblInformacionTabla.Visible = true;
                    lblInformacionTabla.Text = "No hay nada que reasignar para este usuario.";
                    return;
                }
            }
            else
            {
                lblInformacionTabla.Visible = true;
                lblInformacionTabla.Text = "Error al intentar obtener el usuario.";
                return;
            }
            Session.Remove("PaginaAnterior");
            Session["PaginaAnterior"] = HttpContext.Current.Request.Url.AbsoluteUri;
            Response.Redirect(String.Format("~/ReasignarUsuarios.aspx?Despacho={0}&Usuario={1}", dominio, idUsuario));
        }

        protected void checkAll_init(object sender, EventArgs e)
        {
            var cbox = sender as ASPxCheckBox;
            //ASPxGridView grid = (cbox.NamingContainer as GridViewHeaderTemplateContainer).Grid;
            if (cbox != null) cbox.Checked = false;
        }

        public void EscondeMensaje()
        {
            lblInformacionTabla.Visible = false;
        }
    }
}