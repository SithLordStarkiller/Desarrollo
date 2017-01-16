using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Utils;
using PubliPayments.Entidades;
using System.Diagnostics;
using System.Data;
using DevExpress.Web;

namespace PubliPayments

{
    public partial class OrdenesAsignadas : Page
    {
        private readonly SistemasCobranzaEntities _ctx = new SistemasCobranzaEntities();

        private int _idRol = -1;
        private int _idDominio = -1;
        private int _idUsuario = -1;
        private int _dSeleccionado;
        public string AplicacionActual = "";
        int tipo = -1;
         int usuarioPadre = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsCallback && !IsPostBack)
                {
                    ASPxGridView1.FilterExpression = "Estatus != 2";
                }
                AplicacionActual = Config.AplicacionActual().Nombre.ToUpper();
                if (User.IsInRole("0") || User.IsInRole("2") || User.IsInRole("3"))
                //Administrador o Administrador de despacho o supervisor
                {
                    _idDominio = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdDominio));
                    _idRol = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol));
                    _idUsuario = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));

                    
                    if (!Page.IsPostBack)
                    {
                        usuarioPadre = _idRol == 0 ? 0 : _idUsuario;
                        Session["idUsuario"] = _idRol == 0 ? 0 : _idUsuario;
                        tipo = _idRol == 2 || _idRol == 0 ? 2 : 1;
                        Session["idRol"] = _idRol == 2 || _idRol == 0 ? 2 : 1;
                        ddlAdminDespachos.Visible = false;
                        ddlSupDespachos.Visible = false;
                        BtnDespachoSup.Visible = false;
                        CargaFormulario();

                        CargaGestor();

                        if (_idRol == 0)
                        {
                            var listaDespacho = from d in _ctx.Dominio
                                                where d.Estatus == 1
                                                && d.idDominio > 2
                                                select d;
                            List<Dominio> lD = listaDespacho.ToList();
                            lD.Insert(0, new Dominio { idDominio = -1, nom_corto = "Seleccionar despacho" });
                            ddlDespachos.DataSource = lD.ToList();
                            ddlDespachos.DataBind();
                            ddlDespachos.Visible = true;
                        }

                        if (Session["Respuesta"] != null)
                        {
                            string[] respuestas = Session["Respuesta"].ToString().Split('|');
                            PopUpMovimientos.ShowOnPageLoad = true;
                            PopUpMovimientos.HeaderText = "Resultado";
                            lblMensaje.Text = respuestas[0];
                            Session.Remove("Respuesta");
                            if (_idRol == 0)
                            {
                                Session["idUsuario"] = respuestas[2];
                                ddlAdminDespachos.Visible = true;
                                ddlSupDespachos.Visible = true;
                                CargaAdmin(Convert.ToInt32(respuestas[1]));
                                CargaSupervisor(Convert.ToInt32(respuestas[1]), Convert.ToInt32(respuestas[3]));
                                ddlDespachos.SelectedValue = respuestas[1];
                                ddlAdminDespachos.SelectedValue = respuestas[3];
                                ddlSupDespachos.SelectedValue = respuestas[4];
                                if (respuestas[4] != "")
                                {
                                    BtnDespachoSup.Visible = false;
                                    BtnDespachoGest.Visible = true;
                                    btnReasignar.Visible = true;
                                    Session["idRol"] = 1;
                                    OrdAsig.Value = "1";
                                }
                                else
                                {
                                    BtnDespachoSup.Visible = true;
                                    BtnDespachoGest.Visible = false;
                                    btnReasignar.Visible = false;
                                    Session["idRol"] = 2;
                                    OrdAsig.Value = "0";
                                }

                            }
                            else
                            {
                                ddlAdminDespachos.Visible = false;
                                ddlDespachos.Visible = false;
                                ddlSupDespachos.Visible = false;
                            }
                        }

                        if (AplicacionActual.Contains("SIRA"))
                        {
                            ASPxGridView1.Columns[1].Caption = "id Orden";
                            ASPxGridView1.Columns[6].Caption = "RFC";
                            ASPxGridView1.Columns[8].Caption = "Razon Social";
                            ASPxGridView1.Columns[10].Caption = "Nombre comercial";
                            ASPxGridView1.Columns[15].Caption = "Estado";
                            ASPxGridView1.Columns[8].VisibleIndex = 11;

                            var colP1 = new GridViewDataColumn("FVisita","Programacion Visita") { VisibleIndex = 10 };
                            colP1.CellStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns.Add(colP1);
                            var colP2 = new GridViewDataColumn("NumReporte", "No de Reporte") { VisibleIndex = 2 };
                            colP2.CellStyle.HorizontalAlign = HorizontalAlign.Left;
                            colP2.Settings.AllowAutoFilter = DefaultBoolean.True;
                            colP2.Settings.AllowHeaderFilter = DefaultBoolean.False;
                            ASPxGridView1.Columns.Add(colP2);
                            var colP3 = new GridViewDataColumn("identificadorsucursal", "Sucursal") { VisibleIndex = 7 };
                            colP3.Settings.AllowAutoFilter = DefaultBoolean.True;
                            colP3.Settings.AllowHeaderFilter = DefaultBoolean.False;
                            colP3.CellStyle.HorizontalAlign = HorizontalAlign.Right;
                            ASPxGridView1.Columns.Add(colP3);
                            
                            ASPxGridView1.Columns.Remove(ASPxGridView1.Columns[9]);
                            ASPxGridView1.Columns.Remove(ASPxGridView1.Columns[15]);
                            ASPxGridView1.Columns.Remove(ASPxGridView1.Columns[15]);
                            ASPxGridView1.SettingsDetail.ShowDetailRow = false;
                        }
                    }
                    tipo = Convert.ToInt32(Session["idRol"]);
                    usuarioPadre = Convert.ToInt32(Session["idUsuario"]);
                    var c = FormularioList.SelectedValue;

                    ASPxGridView1.DataSource = Persistencia.ObtenerOrdenes(Convert.ToInt32(Session["idRol"]),
                        Convert.ToInt32(Session["idUsuario"]), "%", 0, FormularioList.SelectedValue, !IsPostBack);
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

        protected void btnReasignarOrden_OnClick(object sender, EventArgs e)
        {
            var listOrdenes = (ASPxGridView1.GetSelectedFieldValues(new[] { "idOrden" })).Select(Convert.ToInt32).ToList();

            int contadorOrdenesReasignadas = 0;
            int totOrdenes = listOrdenes.Count;
            string respuesta = "";

            for (int i = 0; i < totOrdenes; i += 40)
            {
                int restantes = totOrdenes - i;
                var ordn = (restantes <= 40) ? listOrdenes.GetRange(i, restantes) : listOrdenes.GetRange(i, 40);

                string ordenesString = string.Join(",", ordn);
                contadorOrdenesReasignadas += new EntOrdenes().ActualizarEstatusUsuarioOrdenes(ordenesString, 15, -1,
                    false, true, _idUsuario);
            }

            respuesta = respuesta != "" ? respuesta : "Se reasignó " + contadorOrdenesReasignadas + " órden.";

            int seleccionado = Session["Despacho"] != null ? Convert.ToInt32(Session["Despacho"].ToString()) : 0;
            if (respuesta != "")
                Session["Respuesta"] = (respuesta + "|" + ((_idRol == 0 && seleccionado > 0) ? seleccionado : _idDominio) + "" + "|" + Session["idUsuario"] + "|" + Session["idUsuarioAlta"] + "|" + Session["idUsuarioPadre"]);
            Response.Redirect(Request.Path);
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

        protected void checkAll_init(object sender, EventArgs e)
        {
            var cbox = sender as ASPxCheckBox;
            //ASPxGridView grid = (cbox.NamingContainer as GridViewHeaderTemplateContainer).Grid;
            if (cbox != null) cbox.Checked = false;
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

        protected void btLimpiar_Clear(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);

        }

        protected void ASPxGridView1_LaunchSort(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            var gridView = (ASPxGridView)sender;
            ((GridViewDataColumn)gridView.Columns["nom_corto"]).SortAscending();
        }

        protected void ASPxGridView1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

            string[] parameters = e.Parameters.Split(';');
            if (parameters.Length < 2)
            {
                ASPxGridView1.Selection.UnselectAll();
                bool check = Convert.ToBoolean(e.Parameters);

                ASPxGridView1.FilterExpression = !check ? "Estatus != 2" : "Estatus > 0";

            }
            else
            {
                if (parameters[0].Equals("") || parameters[1].Equals("")) return;

                int index = int.Parse(parameters[0]);

                bool isGroupRowSelected = bool.Parse(parameters[1]);
                for (int i = 0; i < ASPxGridView1.GetChildRowCount(index); i++)
                {
                    DataRow row = ASPxGridView1.GetChildDataRow(index, i);
                    ASPxGridView1.Selection.SetSelectionByKey(row["idOrden"], isGroupRowSelected);
                }
            }
        }

        /*protected void ASPxGridView1_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e) 
        {
            
        }*/

        protected string GetCaptionText(GridViewGroupRowTemplateContainer container)
        {
            string captionText = !string.IsNullOrEmpty(container.Column.Caption) ? container.Column.Caption : container.Column.FieldName;
            return string.Format("{0} : {1} {2}", captionText, container.GroupText, container.SummaryText);
        }

        protected bool GetChecked(int visibleIndex)
        {
            for (int i = 0; i < ASPxGridView1.GetChildRowCount(visibleIndex); i++)
            {
                bool isRowSelected = ASPxGridView1.Selection.IsRowSelectedByKey(ASPxGridView1.GetChildDataRow(visibleIndex, i)["idOrden"]);
                if (!isRowSelected)
                    return false;
            }
            return true;
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

        protected void btAsignarMas_Click(object sender, EventArgs e)
        {
            Response.Redirect("AsignaOrdenes.aspx");
        }
        
        protected void DespachoSeleccionado(object sender, EventArgs e)
        {
            var list = sender as DropDownList;
            _dSeleccionado = Convert.ToInt32(list != null && list.SelectedValue != "" ? list.SelectedValue : "-1");
            Session["Despacho"] = _dSeleccionado;
            CargaAdmin(_dSeleccionado);
            CargaSupervisor(-1, -1);
        }

        protected void AdministradorSeleccionado(object sender, EventArgs e)
        {
            var list = sender as DropDownList;
            _dSeleccionado = Convert.ToInt32(list != null && list.SelectedValue != "" ? list.SelectedValue : "-1");
            CargaSupervisor(Convert.ToInt32(Session["Despacho"]), _dSeleccionado);
            BtnDespachoSup.Visible = true;
            BtnDespachoGest.Visible = false;
        }
        protected void SupervisorSeleccionado(object sender, EventArgs e)
        {
            var list = sender as DropDownList;
            _dSeleccionado = Convert.ToInt32(list != null && list.SelectedValue != "" ? list.SelectedValue : "-1");

            if (_dSeleccionado > 0)
            {
                BtnDespachoSup.Visible = false;
                BtnDespachoGest.Visible = true;
                OrdAsig.Value = "1";
            }
            else
            {
                BtnDespachoSup.Visible = true;
                BtnDespachoGest.Visible = false;
                btnReasignar.Visible = false;
                OrdAsig.Value = "0";
            }

        }

        private void CargaAdmin(int despacho)
        {
            var listaUsuario = from u in _ctx.Usuario
                               where u.idRol == 2
                               && u.idDominio == despacho
                               && (u.Estatus != 0 )
                               select u;

            List<Usuario> l = listaUsuario.ToList();
            l.Insert(0, new Usuario { idUsuario = 0, Usuario1 = "Seleccionar Admin" });
            ddlAdminDespachos.DataSource = l;
            ddlAdminDespachos.DataBind();
            ddlAdminDespachos.SelectedIndex = 0;
            ddlAdminDespachos.Visible = true;
        }

        private void CargaSupervisor(int despacho, int idAdmin)
        {
            var listaUsuario = from u in _ctx.Usuario
                               join r in _ctx.RelacionUsuarios on u.idUsuario equals r.idHijo
                               where u.idRol == 3
                                     && u.idDominio == despacho
                                     && (u.Estatus != 0 )
                                     && r.idPadre == idAdmin
                               select u;

            List<Usuario> l = listaUsuario.ToList();
            l.Insert(0, new Usuario { idUsuario = 0, Usuario1 = "Seleccionar Supervisor" });
            ddlSupDespachos.DataSource = l;
            ddlSupDespachos.DataBind();
            ddlSupDespachos.SelectedIndex = 0;
            ddlSupDespachos.Visible = true;
        }


        private void CargaGestor()
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
                                     && u.Estatus != 0 
                                orderby u.Usuario
                                select u
                                :
                                from u in _ctx.VUsuarios
                                where u.idDominio == _idDominio
                                && u.idPadre == _idUsuario
                                && u.Estatus != 0 
                                orderby u.Usuario
                                select u;

            List<VUsuarios> l = listaUsuario.ToList();
            l.Insert(0, new VUsuarios { idUsuario = 0, Usuario = "Seleccionar usuario" });
            ddlGestores.DataSource = l;
            ddlGestores.DataBind();
        }

        private void CargaFormulario()
        {
            var formularios = new EntFormulario().ObtenerListaFormularios("").FindAll(x => x.Captura == 1);
            FormularioList.DataSource = formularios.ToList();
            FormularioList.SelectedIndex = 0;
            FormularioList.DataBind();
        }

        protected void BtnDespachoSupr_Click(object sender, EventArgs e)
        {
            int supSeleccionado = Convert.ToInt32(ddlSupDespachos.SelectedValue != "" ? ddlSupDespachos.SelectedValue : "-1");
            int adminSeleccionado = Convert.ToInt32(ddlAdminDespachos.SelectedValue != "" ? ddlAdminDespachos.SelectedValue : "-1");
            if (adminSeleccionado > 0)
            {
                Session["idUsuario"] = adminSeleccionado;
                Session["idUsuarioAlta"] = adminSeleccionado;
                Session["idUsuarioPadre"] = supSeleccionado;
                Session["idRol"] = 2;
                ASPxGridView1.DataSource = Persistencia.ObtenerOrdenes(2, Convert.ToInt32(Session["idUsuario"]), "%", 0, FormularioList.SelectedValue, true);
                ASPxGridView1.DataBind();
            }

        }
        
        protected void BtnDespachoGest_Click(object sender, EventArgs e)
        {
            int supSeleccionado = Convert.ToInt32(ddlSupDespachos.SelectedValue != "" ? ddlSupDespachos.SelectedValue : "-1");
            int adminSeleccionado = Convert.ToInt32(ddlAdminDespachos.SelectedValue != "" ? ddlAdminDespachos.SelectedValue : "-1");
            if (supSeleccionado > 0)
            {
                Session["idUsuario"] = supSeleccionado;
                Session["idUsuarioAlta"] = adminSeleccionado;
                Session["idUsuarioPadre"] = supSeleccionado;
                Session["idRol"] = 1;
                btnReasignar.Visible = true;
                ASPxGridView1.DataSource = Persistencia.ObtenerOrdenes(1, Convert.ToInt32(Session["idUsuario"]), "%", 0, FormularioList.SelectedValue, true);
                ASPxGridView1.DataBind();

            }

        }


        protected void ASPxGridView2_BeforePerformDataSelect(object sender, EventArgs e)
        {
            string[] usuarioLog = { "-1", "OrdenesAsignadas.aspx" };
            bool expanded = Session["Expanded"] != null && (Session["Expanded"].ToString() == "True");
            string idOrden = (((ASPxGridView)sender).GetMasterRowKeyValue() ).ToString().Split('|')[0];
            ConexionSql cnnSql = ConexionSql.Instance;
            var valExplained = Session["ValExpanded"] ?? "";
            if (expanded && !valExplained.ToString().Contains(idOrden))
            {
                usuarioLog[0] = _idUsuario.ToString(CultureInfo.InvariantCulture);
                Session["Expanded"]= false;
                DataTable detailTable = cnnSql.ObtenerHistoricoOrden(idOrden, _idDominio, usuarioLog);
                var dv = new DataView(detailTable);
                var detailGridView = (ASPxGridView)sender;
                dv.RowFilter = "num_cred = " + (detailTable.Rows.Count > 0 ? detailTable.Rows[0].Field<string>(0) : "0000000000");
                detailGridView.DataSource = dv;
                if (Session["ValExpanded"] != null)
                {    if (!Session["ValExpanded"].ToString().Contains(idOrden))
                    { Session["ValExpanded"] += "," + idOrden; }
                }
                else
                {
                    Session["ValExpanded"] =  idOrden;
                }
            }
            else if (expanded)
            {
                usuarioLog[0] ="-1"; 
                DataTable detailTable = cnnSql.ObtenerHistoricoOrden(idOrden, _idDominio, usuarioLog);
                var dv = new DataView(detailTable);
                var detailGridView = (ASPxGridView)sender;
                dv.RowFilter = "num_cred = " + (detailTable.Rows.Count > 0 ? detailTable.Rows[0].Field<string>(0) : "0000000000");
                detailGridView.DataSource = dv;
            }
        }
        protected void ASPxGridView1_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
        {
            if (e.Expanded)
            {
                Session["Expanded"] = true;

            }
        }

        protected void FormularioList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ASPxGridView1.DataSource = Persistencia.ObtenerOrdenes(Convert.ToInt32(Session["idRol"]),
                       Convert.ToInt32(Session["idUsuario"]), "%", 0, FormularioList.SelectedValue, true);
            ASPxGridView1.DataBind();
        }
    }
}