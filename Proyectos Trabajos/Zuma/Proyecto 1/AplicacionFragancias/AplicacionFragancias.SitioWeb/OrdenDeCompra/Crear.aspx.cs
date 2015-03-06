using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AplicacionFragancias.Entidades;
using AplicacionFragancias.LogicaNegocios;

namespace AplicacionFragancias.SitioWeb.OrdenDeCompra
{
    public partial class CrearOrdenCompra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FirstGridViewRow();
            }
        }

        private void FirstGridViewRow()
        {
            var dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Col1", typeof(string)));
            dt.Columns.Add(new DataColumn("Col2", typeof(string)));
            dt.Columns.Add(new DataColumn("Col3", typeof(string)));
            dt.Columns.Add(new DataColumn("Col4", typeof(string)));
            dt.Columns.Add(new DataColumn("Col5", typeof(string)));
            dt.Columns.Add(new DataColumn("Col6", typeof(string)));
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["Col1"] = string.Empty;
            dr["Col2"] = string.Empty;
            dr["Col3"] = string.Empty;
            dr["Col4"] = string.Empty;
            dr["Col5"] = string.Empty;
            dr["Col6"] = string.Empty;
            dt.Rows.Add(dr);

            ViewState["CurrentTable"] = dt;

            grvStudentDetails.DataSource = dt;
            grvStudentDetails.DataBind();
        }

        private void AddNewRow()
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {
                var dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;

                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (var i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        var txtNombreProducto =
                          (TextBox)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("txtNombreProducto");
                        var txtLote =
                          (TextBox)grvStudentDetails.Rows[rowIndex].Cells[2].FindControl("txtLote");
                        var txtCantidad =
                          (TextBox)grvStudentDetails.Rows[rowIndex].Cells[3].FindControl("txtCantidad");
                        var txtPreciounitario =
                          (TextBox)grvStudentDetails.Rows[rowIndex].Cells[4].FindControl("txtPrecioUnitario");
                        var txtFechaEntrega =
                          (TextBox)grvStudentDetails.Rows[rowIndex].Cells[5].FindControl("txtFechaEntrega");
                        var txtSubtotal =
                          (TextBox)grvStudentDetails.Rows[rowIndex].Cells[5].FindControl("txtSubTotal");
                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;
                        var subtotal = (Convert.ToDecimal(string.IsNullOrEmpty(txtCantidad.Text) ? "0" : txtCantidad.Text)) * (Convert.ToDecimal(string.IsNullOrEmpty(txtPreciounitario.Text) ? "0" : txtPreciounitario.Text));
                        dtCurrentTable.Rows[i - 1]["Col1"] = txtNombreProducto.Text;
                        dtCurrentTable.Rows[i - 1]["Col2"] = txtLote.Text;
                        dtCurrentTable.Rows[i - 1]["Col3"] = txtCantidad.Text;
                        dtCurrentTable.Rows[i - 1]["Col4"] = txtPreciounitario.Text;
                        dtCurrentTable.Rows[i - 1]["Col5"] = txtFechaEntrega.Text;
                        dtCurrentTable.Rows[i - 1]["Col6"] = subtotal.ToString();
                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;

                    grvStudentDetails.DataSource = dtCurrentTable;
                    grvStudentDetails.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousData();
        }

        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (var i = 0; i < dt.Rows.Count; i++)
                    {
                        var txtNombreProducto = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("txtNombreProducto");
                        var txtLote = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("txtLote");
                        var txtCantidad = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("txtCantidad");
                        var txtPrecioUnitario = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("txtprecioUnitario");
                        var txtFechaEntrega = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("txtFechaEntrega");
                        var txtSubTotal = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("txtSubTotal");

                        txtNombreProducto.Text = dt.Rows[i]["Col1"].ToString();
                        txtLote.Text = dt.Rows[i]["Col2"].ToString();
                        txtCantidad.Text = dt.Rows[i]["Col3"].ToString();
                        txtPrecioUnitario.Text = dt.Rows[i]["Col4"].ToString();
                        txtFechaEntrega.Text = dt.Rows[i]["Col5"].ToString();
                        txtSubTotal.Text = dt.Rows[i]["Col6"].ToString();
                        rowIndex++;
                    }
                }
            }
        }

        protected void grvStudentDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SetRowData();
            if (ViewState["CurrentTable"] != null)
            {
                var dt = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                var rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable"] = dt;
                    grvStudentDetails.DataSource = dt;
                    grvStudentDetails.DataBind();

                    for (var i = 0; i < grvStudentDetails.Rows.Count - 1; i++)
                    {
                        grvStudentDetails.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                    }
                    SetPreviousData();
                }
            }
        }

        private void SetRowData()
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        var txtNombreProducto = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("txtNombreProducto");
                        var txtLote = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("txtLote");
                        var txtCantidad = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("txtCantidad");
                        var txtPrecioUnitario = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("txtprecioUnitario");
                        var txtFechaEntrega = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("txtFechaEntrega");
                        var txtSubTotal = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("txtSubTotal");

                        drCurrentRow = dtCurrentTable.NewRow();

                        drCurrentRow["RowNumber"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["Col1"] = txtNombreProducto.Text;
                        dtCurrentTable.Rows[i - 1]["Col2"] = txtLote.Text;
                        dtCurrentTable.Rows[i - 1]["Col3"] = txtCantidad.Text;
                        dtCurrentTable.Rows[i - 1]["Col4"] = txtPrecioUnitario.Text;
                        dtCurrentTable.Rows[i - 1]["Col5"] = txtFechaEntrega.Text;
                        dtCurrentTable.Rows[i - 1]["Col6"] = txtSubTotal.Text;
                        rowIndex++;
                    }

                    ViewState["CurrentTable"] = dtCurrentTable;
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            //SetPreviousData();
        }

        protected void ButtonAdd_OnClick(object sender, EventArgs e)
        {
            AddNewRow();
        }

        protected void btnAceptar_OnClick(object sender, EventArgs e)
        {
            var listaProductos = new List<COM_PRODUCTOS>();
            var rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {
                var dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (var i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        var txtNombreProducto =
                            (TextBox)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("txtNombreProducto");
                        var txtLote = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("txtLote");
                        var txtCantidad = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("txtCantidad");
                        var txtPrecioUnitario =
                            (TextBox)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("txtprecioUnitario");
                        var txtFechaEntrega =
                            (TextBox)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("txtFechaEntrega");
                        var txtSubTotal = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("txtSubTotal");

                        var entidad = new COM_PRODUCTOS
                        {
                            NOMBREPRODUCTO = txtNombreProducto.Text,
                            LOTE = txtLote.Text,
                            CANTIDADPRODUCTO = Convert.ToDecimal(txtCantidad.Text),
                            PRECIOUNITARIO = Convert.ToDecimal(txtPrecioUnitario.Text),
                            FECHAENTREGA = Convert.ToDateTime(txtFehaEntrega.Text),
                            ENTREGADO = false
                        };

                        listaProductos.Add(entidad);
                        rowIndex++;
                    }
                    var ordenCompra = new COM_ORDENCOMPRA
                    {
                        NOORDENCOMPRA = txtNoOrdenCompra.Text,
                        FECHAPEDIDO = Convert.ToDateTime(txtFechaPedido.Text),
                        FECHAENTREGA = Convert.ToDateTime(txtFehaEntrega.Text),
                        IDALAMACENES = Convert.ToInt16(ddlCatAlmacenes.SelectedValue),
                        CANTIDADTOTAL = Convert.ToDecimal(CantidadPiezas.Text),
                        IDESTATUSCOMPRA = Convert.ToInt32(ddlEstatusPedido.SelectedValue)
                    };

                    var insertado = new LogicaNegocios.Compras.OperaionesCompras().InsertaOrdenCompra(ordenCompra,
                        listaProductos);

                    if (insertado != null)
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(Page), "Ingreso exitosos",
                            "alert('Se creo la nueva orden de compra " + insertado.NOORDENCOMPRA + "');",
                            true);
                    }
                    else
                    {
                    }

                }
                else
                {
                    Response.Write("ViewState is null");
                }
            }
        }
    }
}