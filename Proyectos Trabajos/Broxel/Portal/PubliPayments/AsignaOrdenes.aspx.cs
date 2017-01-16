using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using DevExpress.Data.PLinq.Helpers;
using DevExpress.Utils;
using DevExpress.Web;
using PubliPayments.Entidades;
using PubliPayments.Utiles;

namespace PubliPayments
{
    public partial class AsignaOrdenes : Page
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["SqlDefault"].ConnectionString;

        private SqlConnection _cnn;

        private readonly SistemasCobranzaEntities _ctx = new SistemasCobranzaEntities();
        private int _idRol = -1;
        private int _idDominio = -1;
        private int _idUsuario = -1;
        public string AplicacionActual = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                _idDominio = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdDominio));
                _idRol = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol));
                _idUsuario = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
                AplicacionActual = Config.AplicacionActual().Nombre.ToUpper();
                if (User.IsInRole("0") || User.IsInRole("2") || User.IsInRole("3"))
                //Administrador o Administrador de despacho o supervisor
                {
                    _cnn = new SqlConnection(_connectionString);
                    _cnn.Open();
                    var idUsuario = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
                    var idPadre = _idRol == 2 || _idRol == 0 ? 0 : idUsuario;
                    Session["idPadre"] = idPadre;

                    var recargar = !IsPostBack;

                    if (!Page.IsPostBack)
                    {
                        var idDominio = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdDominio));
                        Session["idDominio"] = idDominio;
                        //BtLimpiarOnClick(sender, null);
                        CargaFormulario();

                        if (_idRol == 0)
                        {
                            Session["idDominio"] = -1;
                            var listaDespacho = from d in _ctx.Dominio
                                                where d.Estatus == 1
                                                && d.idDominio > 2
                                                select d;
                            List<Dominio> lD = listaDespacho.ToList();
                            lD.Insert(0, new Dominio { idDominio = -1, nom_corto = "Seleccionar despacho" });
                            ddlDespachos.DataSource = lD.ToList();
                            ddlDespachos.DataBind();
                            ddlDespachos.Visible = true;
                            BtnDespachoVer.Visible = true;

                           
                            var col = new GridViewDataColumn("nom_corto", "Despacho") { VisibleIndex = 3 };
                            col.Settings.AllowAutoFilter = DefaultBoolean.False;
                            col.Settings.HeaderFilterMode = HeaderFilterMode.CheckedList;
                            ASPxGridView1.Columns.Add(col);
                        }

                        if (_idRol == 3 && !AplicacionActual.Contains("SIRA"))
                        {
                            var col = new GridViewDataColumn("idVisita", "Visita") { VisibleIndex = 4 };
                            col.Settings.AllowAutoFilter = DefaultBoolean.False;
                            col.Settings.HeaderFilterMode = HeaderFilterMode.CheckedList;
                            col.CellStyle.HorizontalAlign=HorizontalAlign.Center;
                            ASPxGridView1.Columns.Add(col);
                        }

                        if (!AplicacionActual.Contains("SIRA"))
                        {
                            var colP1 = new GridViewDataColumn("pago_1mes", "Pago " + DateTime.Now.AddMonths(-1).ToString("MMMM", CultureInfo.CreateSpecificCulture("es-MX"))) { VisibleIndex = 11 };
                            colP1.Settings.AllowAutoFilter = DefaultBoolean.True;
                            colP1.Settings.AllowHeaderFilter = DefaultBoolean.False;
                            colP1.CellStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns.Add(colP1);
                            var colP2 = new GridViewDataColumn("pago_2mes", "Pago " + DateTime.Now.AddMonths(-2).ToString("MMMM", CultureInfo.CreateSpecificCulture("es-MX"))) { VisibleIndex = 11 };
                            colP2.Settings.AllowAutoFilter = DefaultBoolean.True;
                            colP2.Settings.AllowHeaderFilter = DefaultBoolean.False;
                            colP2.CellStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns.Add(colP2);
                            var colP3 = new GridViewDataColumn("pago_3mes", "Pago " + DateTime.Now.AddMonths(-3).ToString("MMMM", CultureInfo.CreateSpecificCulture("es-MX"))) { VisibleIndex = 11 };
                            colP3.Settings.AllowAutoFilter = DefaultBoolean.True;
                            colP3.Settings.AllowHeaderFilter = DefaultBoolean.False;
                            colP3.CellStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns.Add(colP3);
                            var colP4 = new GridViewDataColumn("pago_4mes", "Pago " + DateTime.Now.AddMonths(-4).ToString("MMMM", CultureInfo.CreateSpecificCulture("es-MX"))) { VisibleIndex = 11 };
                            colP4.Settings.AllowAutoFilter = DefaultBoolean.True;
                            colP4.Settings.AllowHeaderFilter = DefaultBoolean.False;
                            colP4.CellStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns.Add(colP4);
                        }
                        else
                        {
                            ASPxGridView1.Columns[1].VisibleIndex = 2;
                            ASPxGridView1.Columns[3].VisibleIndex = 3;
                            ASPxGridView1.Columns[1].CellStyle.HorizontalAlign = HorizontalAlign.Left;
                            ASPxGridView1.Columns[1].Caption = "RFC";
                            ASPxGridView1.Columns[3].Caption = "Razon Social";
                            ASPxGridView1.Columns[9].Caption = "Estado";
                            ASPxGridView1.Columns[4].Caption = "Nombre Comercial";
                            ASPxGridView1.Columns.Remove(ASPxGridView1.Columns[ASPxGridView1.Columns.Count-1]);
                            ASPxGridView1.Columns.Remove(ASPxGridView1.Columns[ASPxGridView1.Columns.Count-1]);
                            ASPxGridView1.Columns[ASPxGridView1.Columns.Count - 1].Visible = true;
                            
                            ASPxGridView1.Columns[ASPxGridView1.Columns.Count - 1].SetColVisibleIndex(1);
                            ASPxGridView1.SettingsDetail.ShowDetailRow = false;

                            var colP1 = new GridViewDataColumn("NumReporte", "No de Reporte") { VisibleIndex = 1 };
                            colP1.Settings.AllowAutoFilter = DefaultBoolean.True;
                            colP1.Settings.AllowHeaderFilter = DefaultBoolean.False;
                            colP1.CellStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns.Add(colP1);

                            var colP2 = new GridViewDataColumn("identificadorsucursal", "Sucursal") { VisibleIndex = 2 };
                            colP2.Settings.AllowAutoFilter = DefaultBoolean.True;
                            colP2.Settings.AllowHeaderFilter = DefaultBoolean.False;
                            colP2.CellStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns.Add(colP2);
                            
                        }
                     

                        ListaXAsignar();
                        if (Session["Respuesta"] != null)
                        {
                            Session["idDominio"] = Session["idSeleccionado"] ?? Session["idDominio"];
                            string[] respuestas = Session["Respuesta"].ToString().Split('|');
                            ListaXAsignar();
                            Session.Remove("Respuesta");
                            lblMensaje.Text = respuestas[0];
                            MostrarPopUpMovimientos();
                            if (Convert.ToInt32(respuestas[1]) < 0)
                            {
                                recargar = false;
                                btVer.Visible = false;
                                CerrarPopUp.Visible = true;
                            }
                        }
                        if (Session["idSeleccionado"] != null)
                        {
                            ddlDespachos.SelectedValue = Session["idSeleccionado"].ToString();
                        }
                    }
                    idPadre = (int) Session["idPadre"];
                    var dominioId = (int) Session["idDominio"];
                    ASPxGridView1.DataSource = Persistencia.ObtenerCreditos("%", idPadre,
                        dominioId, FormularioList.SelectedValue, recargar);
                    ASPxGridView1.DataBind();

                  
                    
                }
                else
                {
                    Response.Redirect("unauthorized.aspx");
                }
            }
            catch (ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }


        }

        private void MostrarPopUpMovimientos()
        {
            PopUpMovimientos.ShowOnPageLoad = true;
            PopUpMovimientos.HeaderText = "Ordenes asignadas";
            btVer.Visible = true;
        }

        protected void gridOrdenes_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover",
                    "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#ceedfc'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle");
            }
        }

        protected void btVer_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("OrdenesAsignadas.aspx");
        }

        protected void checkAll_init(object sender, EventArgs e)
        {
            var cbox = sender as ASPxCheckBox;
            if (cbox != null) cbox.Checked = (false);
        }

        protected void btLimpiar_Clear(object sender, EventArgs e)
        {
            Logger.WriteLine(Logger.TipoTraceLog.Trace, _idUsuario, Path.GetFileName(Request.PhysicalPath),
                "btLimpiar_Clear");
            Response.Redirect(Request.RawUrl);
        }
        protected void btVer_Click(object sender, EventArgs e)
        {
            Response.Redirect("OrdenesAsignadas.aspx");
        }

        protected void ASPxGridView1_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
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

        protected void BtnDespachoVer_Click(object sender, EventArgs e)
        {
            Logger.WriteLine(Logger.TipoTraceLog.Trace, _idUsuario, Path.GetFileName(Request.PhysicalPath),
                "BtnDespachoVer_Click");

            int seleccionado = Convert.ToInt32(ddlDespachos.SelectedValue != "" ? ddlDespachos.SelectedValue : "-1");
            CargarSuperAdminD(seleccionado);
            ddlDespachos.SelectedValue = seleccionado.ToString(CultureInfo.InvariantCulture);
        }

        protected void SeleccionarExpandidas(object sender, EventArgs e)
        {
            for (int i = 0; i < ASPxGridView1.VisibleRowCount; i++)
            {
                if (ASPxGridView1.IsRowExpanded(i))
                {
                    ASPxGridView1.Selection.SelectRow(i);
                }
            }
        }

        protected string GetCaptionText(GridViewGroupRowTemplateContainer container)
        {
            string captionText = !string.IsNullOrEmpty(container.Column.Caption) ? container.Column.Caption : container.Column.FieldName;
            return string.Format("{0} : {1} {2}", captionText, container.GroupText, container.SummaryText);
        }

        protected bool GetChecked(int visibleIndex)
        {
            for (int i = 0; i < ASPxGridView1.GetChildRowCount(visibleIndex); i++)
            {
                bool isRowSelected = ASPxGridView1.Selection.IsRowSelectedByKey(ASPxGridView1.GetChildDataRow(visibleIndex, i)["num_cred"]);
                if (!isRowSelected)
                    return false;
            }
            return true;
        }

        protected void ASPxGridView1_Callback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

            string[] parameters = e.Parameters.Split(';');
            int index = int.Parse(parameters[0]);
            int count = 0;
            bool isGroupRowSelected = bool.Parse(parameters[1]);
            do
            {
                var row = ASPxGridView1.GetChildDataRow(index, count);
                if (row == null) break;
                ASPxGridView1.Selection.SetSelectionByKey(row["num_cred"], isGroupRowSelected);
                count++;
            } while (true);
        }

        protected void ASPxGridView1_RowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {

            if (e.RowType == GridViewRowType.Group)
            {
                var checkBox = ASPxGridView1.FindGroupRowTemplateControl(e.VisibleIndex, "checkBox") as ASPxCheckBox;
                if (checkBox != null)
                {
                    checkBox.ClientSideEvents.CheckedChanged = string.Format("function(s, e){{ ASPxGridView1.PerformCallback('{0};' + s.GetChecked()); }}", e.VisibleIndex);
                    checkBox.Checked = GetChecked(e.VisibleIndex);
                }
            }
        }


        protected void ASPxGridView2_BeforePerformDataSelect(object sender, EventArgs e)
        {
            string[] usuarioLog = { "-1", "AsignaOrdenes.aspx" };
            bool expanded = Session["Expanded"] != null && (Session["Expanded"].ToString() == "True" ? true : false);
            string credito = ((ASPxGridView)sender).GetMasterRowKeyValue().ToString();
            ConexionSql cnnSql = ConexionSql.Instance;
            var valExplained = Session["ValExpanded"] ?? "";
            if (expanded && !valExplained.ToString().Contains(credito))
            {
                usuarioLog[0] = _idUsuario.ToString();
                Session["Expanded"] = false;
                DataTable detailTable = cnnSql.ObtenerHistoricoCredito(credito.ToString(), _idDominio, usuarioLog);
                var dv = new DataView(detailTable);
                var detailGridView = (ASPxGridView) sender;
                dv.RowFilter = "num_cred = " + detailGridView.GetMasterRowKeyValue();
                detailGridView.DataSource = dv;
                if (Session["ValExpanded"] != null)
                {
                    if (!Session["ValExpanded"].ToString().Contains(credito))
                    { Session["ValExpanded"] += "," + credito; }
                }
                else
                {
                    Session["ValExpanded"] = credito;
                }
            }
            else if (expanded)
            {
                usuarioLog[0] = "-1";
                DataTable detailTable = cnnSql.ObtenerHistoricoCredito(credito, _idDominio, usuarioLog);
                var dv = new DataView(detailTable);
                var detailGridView = (ASPxGridView)sender;
                dv.RowFilter = "num_cred = " + detailGridView.GetMasterRowKeyValue();
                detailGridView.DataSource = dv;
            }

        }


        private void CargarSuperAdminD(int seleccionado)
        {
            Session["idDominio"] = seleccionado == 2 ? -1 : seleccionado;
            Session["idSeleccionado"] = seleccionado == 2 ? -1 : seleccionado;
            ASPxGridView1.DataSource = Persistencia.ObtenerCreditos("%", Convert.ToInt32(Session["idPadre"]),
                        Convert.ToInt32(Session["idDominio"]), FormularioList.SelectedValue, true);
            ASPxGridView1.DataBind();
            ListaXAsignar();
        }

        private void ListaXAsignar()
        {
            int seleccionado = Convert.ToInt32(Session["idSeleccionado"] != null ? Session["idSeleccionado"].ToString() : "-1");
            int idRolBuscar = -1;
            if (_idRol == 0 || _idRol == 2)
                idRolBuscar = 3;
            if (_idRol == 3)
                idRolBuscar = 4;

            var listaUsuario = (seleccionado > 0) ?
                                from u in _ctx.VUsuarios
                                where u.idRol == idRolBuscar
                                     && u.idDominio == seleccionado
                                     && (u.Estatus == 1 || u.Estatus == 3)
                                orderby u.Usuario
                                select u
                                :
                                from u in _ctx.VUsuarios
                                where u.idDominio == _idDominio
                                && u.idPadre == _idUsuario
                                && (u.Estatus == 1 || u.Estatus == 3)
                                orderby u.Usuario
                                select u;

            List<VUsuarios> l = listaUsuario.ToList();
            l.Insert(0, new VUsuarios { idUsuario = 0, Usuario = "Seleccionar usuario" });
            ddlUsuarios.DataSource = l;
            ddlUsuarios.DataBind();
        }

        protected void ASPxGridView1_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
        {
            if (e.Expanded)
            {
                Session["Expanded"] = true;
            }
        }
        private void CargaFormulario()
        {
            var formularios = new EntFormulario().ObtenerListaFormularios("").FindAll(x => x.Captura == 1);
            FormularioList.DataSource = formularios.ToList();
            FormularioList.SelectedIndex = 0;
            FormularioList.DataBind();
        }

        protected void FormularioList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ASPxGridView1.DataSource = Persistencia.ObtenerCreditos("%", Convert.ToInt32(Session["idPadre"]),
                       Convert.ToInt32(Session["idDominio"]), FormularioList.SelectedValue, true);
            ASPxGridView1.DataBind();
        }
    }
}