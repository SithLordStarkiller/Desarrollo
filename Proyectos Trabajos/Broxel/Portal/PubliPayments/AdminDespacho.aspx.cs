using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using PubliPayments.Entidades;
using PubliPayments.Utiles;

namespace PubliPayments
{
    public partial class AdminDespacho : Page
    {
        protected string RolActual;
        private readonly SistemasCobranzaEntities _ctx = new SistemasCobranzaEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            RolActual = SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol);
            if (RolActual != "0" && RolActual != "1")
            {
                Response.Redirect("unauthorized.aspx");
            }
                Despachos("", "", -1);
            
        }
        protected void btCrearDespacho_Click(object sender, EventArgs e)
        {
            string nombreDominio = tbDespacho.Text;
            string nomCorto = tbNombreCorto.Text;
            string usuarioAdmin = tbNombreUsuario.Text;
            string nombre = tbNombre.Text;
            string email = tbEmail.Text;
            var condiciones = new[,] { { "1", "MA" }, { "1", "CE" }, { "1", "NU" }, { "1", "MI" }, { "4", "NA" } };
            var password = Security.GeneratePassWord(condiciones);
            if (nombreDominio != "" && nomCorto != "" && usuarioAdmin != ""  && email != "" && nombre != "")
            {
                ConexionSql cnnSql = ConexionSql.Instance;
                DataSet ds = cnnSql.InsertaDominio(nombreDominio, nomCorto, usuarioAdmin, nombre, password[1], email);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Despachos("", "", -1);
                    lblInformacion.Text = "Despacho Creado Correctamente";
                    var mensajeServ = new EntMensajesServicios().ObtenerMensajesServicios("NuevoUsr");
                    mensajeServ.Mensaje = string.Format(mensajeServ.Mensaje, usuarioAdmin, password[0], Request.Url.GetLeftPart(UriPartial.Authority));
                    bool succes = Email.EnviarEmail(email, mensajeServ.Titulo, mensajeServ.Mensaje, mensajeServ.EsHtml);
                    if (!succes)
                    {
                        Logger.WriteLine(Logger.TipoTraceLog.Error,0, "AdminDespacho", "btCrearDespacho_Click - No se pudo mandar email a :" + email + " usuario:" + nombre);
                    }
                }
                else
                {
                    lblInformacion.Text = "Error al Crear Despacho";
                }
            }
            PopUPDespacho.ShowOnPageLoad = false;
            lblInformacion.Visible = lblInformacion.Text != "";
        }

        protected void btEditDespacho_Click(object sender, EventArgs e)
        {
            int dominio = int.Parse(ADidDominio.Text);
            string nombreDominio = ADNombre.Text;
            string nomCorto = ADNCorto.Text;
            int estatus = ADEstatusH.Text.Trim().ToUpper() == "ACTIVO" ? 1 : 0;

            if (dominio != 0 && nombreDominio != "" && nomCorto != "")
            {
                var dominioCtx = _ctx.Dominio.FirstOrDefault(x => x.idDominio == dominio);

                if (dominioCtx != null)
                {
                    dominioCtx.NombreDominio = nombreDominio;
                    dominioCtx.nom_corto = nomCorto;
                    dominioCtx.Estatus = estatus;
                    try
                    {
                        _ctx.SaveChanges();
                        Despachos("", "", -1);
                        lblInformacion.Text = "Despacho actualizado Correctamente";
                    }
                    catch (Exception ex)
                    {
                        Despachos("", "", -1);
                        lblInformacion.Text = "Error al actualizar Despacho" + ex.Message;
                    }
                }
            }
            PopUPDespachoEdit.ShowOnPageLoad = false;
            lblInformacion.Visible = lblInformacion.Text != "";
        }

        protected void GVDespachos_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover",
                    "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#ceedfc'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle");
            }
        }

        private void Despachos(string nombre, string nCorto, int estatus)
        {
            var lista = (estatus >= 0)
                ? from d in _ctx.Dominio
                  join e in _ctx.CatEstatusUsuario
                      on d.Estatus equals e.Estatus
                  where d.idDominio > 2
                        && e.Estatus == estatus
                        && d.NombreDominio.Contains(nombre)
                        && d.nom_corto.Contains(nCorto)
                  orderby d.Estatus descending
                  select new
                  {
                      d.idDominio,
                      d.NombreDominio,
                      d.nom_corto,
                      d.FechaAlta,
                      d.Estatus,
                      EstatusTxt = e.Descripcion
                  }
                : from d in _ctx.Dominio
                  join e in _ctx.CatEstatusUsuario
                      on d.Estatus equals e.Estatus
                  where d.idDominio > 2
                        && d.NombreDominio.Contains(nombre)
                        && d.nom_corto.Contains(nCorto)
                  orderby d.Estatus descending
                  select new
                  {
                      d.idDominio,
                      d.NombreDominio,
                      d.nom_corto,
                      d.FechaAlta,
                      d.Estatus,
                      EstatusTxt = e.Descripcion
                  };
            try
            {
                lblInformacion.Text = "";
                lblInformacion.Visible = false;
                GVDespachos.DataSource = lista.ToList();
                GVDespachos.DataBind();
            }
            catch (Exception)
            {
                lblInformacion.Text = "Error intentelo nuevamente";
                lblInformacion.Visible = true;

            }
        }

        protected void GVDespachos_CustomColumnSort(object sender, DevExpress.Web.CustomColumnSortEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            bool isRow1Selected = grid.Selection.IsRowSelectedByKey(e.GetRow1Value(grid.KeyFieldName));
            bool isRow2Selected = grid.Selection.IsRowSelectedByKey(e.GetRow2Value(grid.KeyFieldName));
            e.Handled = isRow1Selected != isRow2Selected;
            if (e.Handled)
            {
                if (e.SortOrder == DevExpress.Data.ColumnSortOrder.Descending)
                    e.Result = isRow1Selected ? 1 : -1;
                else
                    e.Result = isRow1Selected ? -1 : 1;
            }
        }

        protected void btLimpiar_OnClick(object sender, EventArgs e)
        {
            Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
        }
    }
 }