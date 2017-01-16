using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web.UI.WebControls;
using DevExpress.Web;
using System.Data;
using PubliPayments.Entidades;

namespace PubliPayments
{
    public partial class Gestionadas : System.Web.UI.Page
    {
        private readonly SistemasCobranzaEntities _ctx = new SistemasCobranzaEntities();
        private int _idUsuario = -1;
        private int _idDominio = -1;

        public StringBuilder OrdenHtml = new StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (User.IsInRole("0") || User.IsInRole("2") || User.IsInRole("3"))
                //Administrador o Administrador de despacho o supervisor
                {
                    _idUsuario = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
                    _idDominio = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdDominio));
                    Session["idUsuarioPadre"] = User.IsInRole("0") ? 0 : _idUsuario;
                    if (!Page.IsPostBack)
                    {
                        if (Session["Respuesta"] != null)
                        {
                            string[] respuestas = Session["Respuesta"].ToString().Split('|');
                            Session.Remove("Respuesta");
                            lblMensaje.Text = respuestas[0];
                            MostrarPopUpMovimientos();
                            if (Convert.ToInt32(respuestas[1]) > 0)
                            {
                                btAsignar.Visible = true;
                            }
                        }
                        CargaGestor();
                        CargaFormulario();
                    }
                }
                else
                {
                    Response.Redirect("unauthorized.aspx");
                }
            }
            catch (ThreadAbortException)
            {
            }

            ASPxGridView1.DataSource = Persistencia.ObtenerRespuestasUsuario(1, "0", 0, Convert.ToInt32(Session["idUsuarioPadre"]), "3,4", FormularioList.SelectedValue, !IsPostBack);
            ASPxGridView1.DataBind();
            //if (IsPostBack)

        }

        protected void btLimpiar_OnClick(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void gridRespuestas_OnRowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void ASPxGridView1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

            string[] parameters = e.Parameters.Split(';');
            if (parameters[0].Equals("") || parameters[1].Equals("")) return;

            int index = int.Parse(parameters[0]);

            bool isGroupRowSelected = bool.Parse(parameters[1]);
            for (int i = 0; i < ASPxGridView1.GetChildRowCount(index); i++)
            {
                DataRow row = ASPxGridView1.GetChildDataRow(index, i);
                ASPxGridView1.Selection.SetSelectionByKey(row["idOrden"], isGroupRowSelected);
            }
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

        protected void btDescargar_OnClick(object sender, EventArgs e)
        {

            var columnaCalle = new GridViewDataColumn {FieldName = "calle", Caption = "Calle", VisibleIndex = 8};
            ASPxGridView1.Columns.Add(columnaCalle);

            /*for (int i = 8; i < 33; i++)
            {
                ASPxGridView1.Columns[i].Visible = true;
            }*/
            ASPxGridView1.DataBind();
            gridExport.WriteCsvToResponse();
            ASPxGridView1.Columns.Remove(columnaCalle);


            /*for (int i = 8; i < 33; i++)
            {
                ASPxGridView1.Columns[i].Visible = false;
            }*/
        }

        
        private void MostrarPopUpMovimientos()
        {
            PopUpMovimientos.ShowOnPageLoad = true;
            PopUpMovimientos.HeaderText = "Ordenes asignadas";
            btAsignar.Visible = false;
        }
      
        protected void btAsignar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AsignaOrdenes.aspx");
        }

        protected void ASPxGridView2_BeforePerformDataSelect(object sender, EventArgs e)
        {
            string[] usuarioLog = { "-1", "Gestionadas.aspx" };
            bool expanded = Session["Expanded"] != null && (Session["Expanded"].ToString() == "True");
            string idOrden = (((ASPxGridView)sender).GetMasterRowKeyValue()).ToString().Split('|')[0];
            ConexionSql cnnSql = ConexionSql.Instance;
            var valExplained = Session["ValExpanded"] ?? "";
            if (expanded && !valExplained.ToString().Contains(idOrden))
            {
                usuarioLog[0] = _idUsuario.ToString(CultureInfo.InvariantCulture);
                Session["Expanded"] = false;
                DataTable detailTable = cnnSql.ObtenerHistoricoOrden(idOrden, _idDominio, usuarioLog);
                var dv = new DataView(detailTable);
                var detailGridView = (ASPxGridView)sender;
                dv.RowFilter = "num_cred = " + (detailTable.Rows.Count > 0 ? detailTable.Rows[0].Field<string>(0) : "0000000000");
                detailGridView.DataSource = dv;
                if (Session["ValExpanded"] != null)
                {
                    if (!Session["ValExpanded"].ToString().Contains(idOrden))
                    { Session["ValExpanded"] += "," + idOrden; }
                }
                else
                {
                    Session["ValExpanded"] = idOrden;
                }
            }
            else if (expanded)
            {
                usuarioLog[0] = "-1";
                DataTable detailTable = cnnSql.ObtenerHistoricoOrden(idOrden, _idDominio, usuarioLog);
                var dv = new DataView(detailTable);
                var detailGridView = (ASPxGridView)sender;
                dv.RowFilter = "num_cred = " + (detailTable.Rows.Count > 0 ? detailTable.Rows[0].Field<string>(0) : "0000000000");
                detailGridView.DataSource = dv;
            }
        }
       
        private void CargaGestor()
        {
            var listaUsuario =  from u in _ctx.VUsuarios
                                where u.idDominio == _idDominio
                                && u.idPadre == _idUsuario
                                && (u.Estatus == 1 || u.Estatus == 3)
                                orderby u.Usuario
                                select u;

            List<VUsuarios> l = listaUsuario.ToList();
            l.Insert(0, new VUsuarios { idUsuario = 0, Usuario = "Seleccionar usuario" });
            ddlGestores.DataSource = l;
            ddlGestores.DataBind();
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
            ASPxGridView1.DataSource = Persistencia.ObtenerRespuestasUsuario(1, "0", 0, Convert.ToInt32(Session["idUsuarioPadre"]), "3,4", FormularioList.SelectedValue, true);
            ASPxGridView1.DataBind();
        }
    }
}