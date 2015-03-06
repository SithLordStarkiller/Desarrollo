using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AplicacionFragancias.Entidades;
using AplicacionFragancias.LogicaNegocios.Compras;

namespace AplicacionFragancias.SitioWeb.OrdenDeCompra
{
    public partial class EditarOrdenCompra : System.Web.UI.Page
    {
        private const string NoOrdenCompra = "CO1902EJ";
        public COM_ORDENCOMPRA OrdenCompra;
        private List<COM_PRODUCTOS> _listaProductos;

        protected void Page_Load(object sender, EventArgs e)
        {
            OrdenCompra = new OperaionesCompras().ObtenOrdencompra(NoOrdenCompra);

            if (IsPostBack) return;

            txtNoOrdenCompra.Text = OrdenCompra.NOORDENCOMPRA;
            txtFehaEntrega.Text = OrdenCompra.FECHAENTREGA.ToShortDateString();
            txtFechaPedido.Text = OrdenCompra.FECHAPEDIDO.ToShortDateString();
            ddlCatAlmacenes.SelectedValue = OrdenCompra.IDALAMACENES.ToString();
            ddlEstatusPedido.SelectedValue = OrdenCompra.IDESTATUSCOMPRA.ToString();
            CantidadPiezas.Text = OrdenCompra.CANTIDADTOTAL.ToString(CultureInfo.InvariantCulture);
            chkEsFraccionaria.Checked = OrdenCompra.ENTREGAFRACCIONARIA;

            BindGrid();
        }

        public void BindGrid()
        {
            _listaProductos = new OperaionesCompras().ObtenListaProductos(OrdenCompra);
            gvProductos.DataSource = _listaProductos;
            gvProductos.DataBind();
        }

        public DataTable CreateDGDataSource()
        {
            // Create sample data for the DataList control.
            DataTable dt = new DataTable();
            DataRow dr;
            int i;
            int y;
            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("ID", typeof(int)));
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Description", typeof(string)));
            //Make some rows and put some sample data in
            for (i = 1; i <= 5; i++)
            {
                dr = dt.NewRow();
                dr[0] = i;
                dr[1] = "Name" + "-" + i;
                dr[2] = "Item " + "_" + i;
                //add the row to the datatable
                dt.Rows.Add(dr);
            }

            Session["y"] = i;
            Session["dt"] = dt;

            return dt;
        }

        public ICollection CreateDGDataSource(int CategoryID)
        {
            DataView dv = new DataView(CreateDGDataSource(), "ID=" + CategoryID, null,
                DataViewRowState.CurrentRows);
            return dv;

        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("New"))
                {
                    LinkButton btnNew = e.CommandSource as LinkButton;
                    GridViewRow row = btnNew.NamingContainer as GridViewRow;
                    if (row == null)
                    {
                        return;
                    }
                    TextBox txtCatName = row.FindControl("QuantityTextBox") as TextBox;
                    TextBox txtDescription = row.FindControl("DescriptionTextBox") as TextBox;
                    DataTable dt = Session["dt"] as DataTable;
                    DataRow dr;
                    int intId = (int)Session["y"];
                    dr = dt.NewRow();
                    dr["Id"] = intId++;
                    Session["y"] = intId;
                    dr["Name"] = txtCatName.Text;
                    dr["Description"] = txtDescription.Text;
                    dt.Rows.Add(dr);
                    dt.AcceptChanges();
                    Session["dt"] = dt;

                    //GridView1.DataSource = Session["dt"] as DataTable;
                    //GridView1.DataBind();

                }

            }
            catch (Exception ex)
            {

            }


        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProductos.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dataTable = Session["dt"] as DataTable;
            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);
                //GridView1.DataSource = dataView;
                //GridView1.DataBind();
            }
        }
        private string ConvertSortDirectionToSql(SortDirection sortDireciton)
        {
            string newSortDirection = String.Empty;
            switch (sortDireciton)
            {
                case SortDirection.Ascending:
                    newSortDirection = "ASC";
                    break;
                case SortDirection.Descending:
                    newSortDirection = "DESC";
                    break;
            }
            return newSortDirection;
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //int ID = (int)GridView1.DataKeys[e.RowIndex].Value;
            // Query the database and get the values based on the ID and delete it.
            //lblMsg.Text = "Deleted Record Id" + ID.ToString();

        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvProductos.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            var idProducto = e.Keys["IDPRODUCTOS"];
            var noOrdenCompra = e.Keys["NOORDENCOMPRA"];

            var index = gvProductos.EditIndex;

            var row = gvProductos.Rows[index];

            var txtNombreProducto = ((TextBox)row.FindControl("txtNombreProducto")).Text;
            var txtLote = ((TextBox) row.FindControl("txtLote")).Text;
            var txtCantidadProducto = Convert.ToDecimal(((TextBox)row.FindControl("txtCantidadProducto")).Text);
            var txtFechaEntrega = Convert.ToDateTime((((TextBox)row.FindControl("txtFechaEntrega")).Text));
            var txtPrecioUnitario = Convert.ToDecimal(((TextBox)row.FindControl("txtPrecioUnitario")).Text);


            var producto = new COM_PRODUCTOS();
            producto.IDPRODUCTOS = (int)idProducto;
            producto.NOORDENCOMPRA = (string) noOrdenCompra;
            producto.NOMBREPRODUCTO = txtNombreProducto;
            producto.LOTE = txtLote;
            producto.FECHAENTREGA = txtFechaEntrega;
            producto.PRECIOUNITARIO = txtPrecioUnitario;
            producto.CANTIDADPRODUCTO = txtCantidadProducto;
            producto.BORRADO = false;

            gvProductos.EditIndex = -1;

            var actualizado = new OperaionesCompras().ActualizaProducto(producto);
            
            if (actualizado)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(Page), "Ingreso exitosos",
                    "alert('Se Actualizo el producto');",
                    true);

                BindGrid();
            }
            else
            {
            }
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvProductos.EditIndex = -1;
            BindGrid();
        }
    }
}