using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using AplicacionFragancias.LogicaNegocios.Compras;
using AplicacionFragancias.LogicaNegocios.Facturacion;

namespace AplicacionFragancias.SitioWeb.Vistas.Compras
{
    public partial class OrdenesCompra : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            lbxEstatusPedido.DataValueField = "IdEstatusCompra";
            lbxEstatusPedido.DataTextField = "NombreEstatus";
            var listaOperacionesCompras = new OperaionesCompras().ObtenEstatusCompras();
            lbxEstatusPedido.DataSource = listaOperacionesCompras;
            lbxEstatusPedido.DataBind();
        }

        public void PrimeraFila()
        {
            var dt = new DataTable();
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Col1", typeof(string)));
            dt.Columns.Add(new DataColumn("Col2", typeof(string)));
            dt.Columns.Add(new DataColumn("Col3", typeof(string)));
            dt.Columns.Add(new DataColumn("Col4", typeof(string)));
            dt.Columns.Add(new DataColumn("Col5", typeof(string)));
            dt.Columns.Add(new DataColumn("Col6", typeof(string)));
            var dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["Col1"] = string.Empty;
            dr["Col2"] = string.Empty;
            dr["Col3"] = string.Empty;
            dr["Col4"] = string.Empty;
            dr["Col5"] = string.Empty;
            dr["Col6"] = string.Empty;
            dt.Rows.Add(dr);

            ViewState["CurrentTable"] = dt;

            grvOrdenesCompra.DataSource = dt;
            grvOrdenesCompra.DataBind();
        }

        private void CargarGrid()
        {

            var listaOperaciones = new List<int>();
            if (lbxEstatusPedido.Items.Count >= 1 && !string.IsNullOrEmpty(txtFechaFinal.Text) && !string.IsNullOrEmpty(txtFechaInicio.Text))
            {
                for (var i = 0; i < lbxEstatusPedido.Items.Count; i++)
                {
                    if (lbxEstatusPedido.Items[i].Selected)
                        listaOperaciones.Add(Convert.ToInt32(lbxEstatusPedido.Items[i].Value));
                }

                var inicio = Convert.ToDateTime(txtFechaInicio.Text);
                var final = Convert.ToDateTime((txtFechaFinal.Text));
                
                var listaOrdenesCompras = new OperaionesCompras().ObtenListasOrdenesDeCompra(inicio, final, listaOperaciones);
                
                grvOrdenesCompra.DataSource = listaOrdenesCompras;
                grvOrdenesCompra.DataBind();

                var contador = 0;

                foreach (var item in listaOrdenesCompras)
                {
                    var listaproductos = new OperaionesCompras().ObtenListaProductos(item.NOORDENCOMPRA);

                    var partidas = listaproductos.Count;

                    var entregadas = listaproductos.Where(r => r.ENTREGADO).ToList().Count;
                    
                    var moneda = grvOrdenesCompra.Rows[contador].FindControl("ddlMoneda") as DropDownList;
                    if (moneda != null) moneda.SelectedValue = item.IDMONEDA.ToString();

                    var estatus = grvOrdenesCompra.Rows[contador].FindControl("ddlEstatusCom") as DropDownList;
                    if (estatus != null) estatus.SelectedValue = item.IDESTATUSCOMPRA.ToString();

                    var impuestos = grvOrdenesCompra.Rows[contador].FindControl("ddlImpuestos") as DropDownList;
                    if (impuestos != null) impuestos.SelectedValue = item.IDIMPUESTO.ToString();

                    var tPartidas = grvOrdenesCompra.Rows[contador].FindControl("txtPartidas") as TextBox;
                    if (tPartidas != null) tPartidas.Text = partidas.ToString();

                    var tPartidasEntregadas = grvOrdenesCompra.Rows[contador].FindControl("txtEntregadas") as TextBox;
                    if (tPartidasEntregadas != null) tPartidasEntregadas.Text = entregadas.ToString();

                    contador++;
                }
            }


            //else
            //{
            //    //ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(Page),
            //    //            "Mensaje", string.Format("alert('Verifica los campos');"), true);

            //}
        }

        protected void lbxEstatusPedido_OnTextChanged(object sender, EventArgs e)
        {
            CargarGrid();
        }

        protected void txtFechaInicio_OnTextChanged(object sender, EventArgs e)
        {
            CargarGrid();
        }

        protected void txtFechaFinal_OnTextChanged(object sender, EventArgs e)
        {
            CargarGrid();
        }

        protected void grvOrdenesCompra_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            var ddlEstatusGv = new DropDownList();
            var ddlMonedaGv = new DropDownList();
            var ddlImpuestosGv = new DropDownList();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ddlEstatusGv = ((DropDownList)(e.Row.FindControl("ddlEstatusCom")));
                ddlMonedaGv = ((DropDownList)(e.Row.FindControl("ddlMoneda")));
                ddlImpuestosGv = ((DropDownList)(e.Row.FindControl("ddlImpuestos")));
            }

            if (ddlEstatusGv != null)
            {
                var listaEstatus = new OperaionesCompras().ObtenEstatusCompras();

                ddlEstatusGv.DataSource = listaEstatus;
                ddlEstatusGv.DataValueField = "IDESTATUSCOMPRA";
                ddlEstatusGv.DataTextField = "NOMBREESTATUS";
                ddlEstatusGv.DataBind();
            }

            if (ddlMonedaGv != null)
            {
                var listaMoneda = new OperacionesFacturacion().ObtenCatalogosMonedas();

                ddlMonedaGv.DataSource = listaMoneda;
                ddlMonedaGv.DataValueField = "IDMONEDA";
                ddlMonedaGv.DataTextField = "NOMBRECORTO";
                ddlMonedaGv.DataBind();
            }

            if (ddlImpuestosGv != null)
            {
                var listaInpuestos = new OperacionesFacturacion().ObtenCatalogoImpuestos();

                ddlImpuestosGv.DataValueField = "IDIMPUESTO";
                ddlImpuestosGv.DataTextField = "NOMBRECORTO";
                ddlImpuestosGv.DataSource = listaInpuestos;
                ddlImpuestosGv.DataBind();
            }
        }

        protected void grvOrdenesCompra_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var ordenCompra = ((Label)(grvOrdenesCompra.Rows[e.RowIndex].FindControl("lblOrdenCompra"))).Text;

            new OperaionesCompras().EliminarOrdenCompra(ordenCompra);

            CargarGrid();
        }

        protected void grvOrdenesCompra_OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            var ordenCompra = ((Label)(grvOrdenesCompra.Rows[e.NewEditIndex].FindControl("lblOrdenCompra"))).Text;
            ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(Page), "Mensaje", "$('#modalEditaOrdenCompra').modal('show');", true);
            grvOrdenesCompra.EditIndex = -1;
            CargarGrid();
        }

        protected void grvOrdenesCompra_OnSelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            grvOrdenesCompra.PageIndex = e.NewSelectedIndex;
            CargarGrid();
        }
    }
}