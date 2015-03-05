using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AplicacionFragancias.LogicaNegocios;

namespace AplicacionFragancias.SitioWeb.OrdenDeCompra
{
    public partial class EditarOrdenCompra : System.Web.UI.Page
    {
        private const int _ordenCompra = 1;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        public void BindGrid()
        {

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

                    GridView1.DataSource = Session["dt"] as DataTable;
                    GridView1.DataBind();

                }

            }
            catch (Exception ex)
            {

            }


        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dataTable = Session["dt"] as DataTable;
            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);
                GridView1.DataSource = dataView;
                GridView1.DataBind();
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
            int ID = (int)GridView1.DataKeys[e.RowIndex].Value;
            // Query the database and get the values based on the ID and delete it.
            //lblMsg.Text = "Deleted Record Id" + ID.ToString();

        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindGrid();
        }
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            // Retrieve the row being edited.
            int index = GridView1.EditIndex;
            GridViewRow row = GridView1.Rows[index];
            TextBox t1 = row.FindControl("TextBox1") as TextBox;
            TextBox t2 = row.FindControl("TextBox2") as TextBox;
            string t3 = GridView1.DataKeys[e.RowIndex].Value.ToString();

            //lblMsg.Text = "Updated record " + t1.Text + "," + t2.Text + "Value From Bound Field" + t3;
        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BindGrid();
        }
    }
}