using System;
using System.Data;
using AplicacionFragancias.Entidades;
using AplicacionFragancias.LogicaNegocios;
using AplicacionFragancias.LogicaNegocios.Compras;

namespace AplicacionFragancias.SitioWeb.Vistas.Compras
{
    public partial class OrdenesCompra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lbxEstatusPedido.DataValueField = "IdEstatusCompra";
            lbxEstatusPedido.DataTextField = "NombreEstatus";
            var lista =  new OperaionesCompras().ObtenEstatusCompras();
            lbxEstatusPedido.DataSource = lista;
            lbxEstatusPedido.DataBind();
            PrimeraFila();
        }

        private void PrimeraFila()
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

            grvProductos.DataSource = dt;
            grvProductos.DataBind();
        }
    }
}