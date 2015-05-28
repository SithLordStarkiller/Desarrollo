using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using AplicacionFragancias.Entidades;
using AplicacionFragancias.LogicaNegocios.Compras;

namespace AplicacionFragancias.SitioWeb.Vistas.Compras
{
    public partial class OrdenesCompra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            lbxEstatusPedido.DataValueField = "IdEstatusCompra";
            lbxEstatusPedido.DataTextField = "NombreEstatus";
            var listaOperacionesCompras =  new OperaionesCompras().ObtenEstatusCompras();
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

            grvProductos.DataSource = dt;
            grvProductos.DataBind();
        }

        protected void lbxEstatusPedido_OnTextChanged(object sender, EventArgs e)
        {
            var listaOperaciones = new List<int>();
            List<COM_ORDENCOMPRA> ListaOrdenesCompras;
            if (lbxEstatusPedido.Items.Count >= 1 && !string.IsNullOrEmpty(txtFechaFinal.Text) && !string.IsNullOrEmpty(txtFechaInicio.Text))
            {
                for (var i = 0; i < lbxEstatusPedido.Items.Count; i++)
                {
                    if (lbxEstatusPedido.Items[i].Selected)
                        listaOperaciones.Add(Convert.ToInt32(lbxEstatusPedido.Items[i].Value));
                }
                var inicio = Convert.ToDateTime(txtFechaInicio.Text);
                var final = Convert.ToDateTime((txtFechaFinal.Text));
                ListaOrdenesCompras = new OperaionesCompras().ObtenListasOrdenesDeCompra(inicio, final, listaOperaciones);
            }
            else
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(Page),
                            "Mensaje", string.Format("alert('Verifica los campos');"), true);
                
            }
        }
    }
}