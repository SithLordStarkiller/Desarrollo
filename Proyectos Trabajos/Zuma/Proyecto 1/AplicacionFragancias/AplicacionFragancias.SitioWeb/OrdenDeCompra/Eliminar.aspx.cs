using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AplicacionFragancias.SitioWeb.OrdenDeCompra
{
    public partial class Eliminar : System.Web.UI.Page
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
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Col1", typeof(string)));
            dt.Columns.Add(new DataColumn("Col2", typeof(string)));
            dt.Columns.Add(new DataColumn("Col3", typeof(string)));
            dt.Columns.Add(new DataColumn("Col4", typeof(string)));
            dt.Columns.Add(new DataColumn("Col5", typeof(string)));
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["Col1"] = string.Empty;
            dr["Col2"] = string.Empty;
            dr["Col3"] = string.Empty;
            dr["Col4"] = string.Empty;
            dr["Col5"] = string.Empty;
            dt.Rows.Add(dr);

            ViewState["CurrentTable"] = dt;


            grvStudentDetails.DataSource = dt;
            grvStudentDetails.DataBind();


            TextBox txn = (TextBox)grvStudentDetails.Rows[0].Cells[1].FindControl("txtName");
            txn.Focus();
            Button btnAdd = (Button)grvStudentDetails.FooterRow.Cells[5].FindControl("ButtonAdd");
            Page.Form.DefaultFocus = btnAdd.ClientID;

        }
        private void AddNewRow()
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
                        TextBox TextBoxName = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("txtName");
                        TextBox TextBoxAge = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[2].FindControl("txtAge");
                        TextBox TextBoxAddress = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[3].FindControl("txtAddress");
                        RadioButtonList RBLGender = (RadioButtonList)grvStudentDetails.Rows[rowIndex].Cells[4].FindControl("RBLGender");
                        DropDownList DrpQualification = (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[5].FindControl("drpQualification");
                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;

                        dtCurrentTable.Rows[i - 1]["Col1"] = TextBoxName.Text;
                        dtCurrentTable.Rows[i - 1]["Col2"] = TextBoxAge.Text;
                        dtCurrentTable.Rows[i - 1]["Col3"] = TextBoxAddress.Text;
                        dtCurrentTable.Rows[i - 1]["Col4"] = RBLGender.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Col5"] = DrpQualification.SelectedValue;
                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;

                    grvStudentDetails.DataSource = dtCurrentTable;
                    grvStudentDetails.DataBind();

                    TextBox txn = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("txtName");
                    txn.Focus();
                    // txn.Focus;
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
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox TextBoxName = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("txtName");
                        TextBox TextBoxAge = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[2].FindControl("txtAge");
                        TextBox TextBoxAddress = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[3].FindControl("txtAddress");
                        RadioButtonList RBLGender = (RadioButtonList)grvStudentDetails.Rows[rowIndex].Cells[4].FindControl("RBLGender");
                        DropDownList DrpQualification = (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[5].FindControl("drpQualification");
                        // drCurrentRow["RowNumber"] = i + 1;

                        grvStudentDetails.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                        TextBoxName.Text = dt.Rows[i]["Col1"].ToString();
                        TextBoxAge.Text = dt.Rows[i]["Col2"].ToString();
                        TextBoxAddress.Text = dt.Rows[i]["Col3"].ToString();
                        RBLGender.SelectedValue = dt.Rows[i]["Col4"].ToString();
                        DrpQualification.SelectedValue = dt.Rows[i]["Col5"].ToString();
                        rowIndex++;
                    }
                }
            }
        }
        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }
        protected void grvStudentDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SetRowData();
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable"] = dt;
                    grvStudentDetails.DataSource = dt;
                    grvStudentDetails.DataBind();

                    for (int i = 0; i < grvStudentDetails.Rows.Count - 1; i++)
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
                        TextBox TextBoxName = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("txtName");
                        TextBox TextBoxAge = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[2].FindControl("txtAge");
                        TextBox TextBoxAddress = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[3].FindControl("txtAddress");
                        RadioButtonList RBLGender = (RadioButtonList)grvStudentDetails.Rows[rowIndex].Cells[4].FindControl("RBLGender");
                        DropDownList DrpQualification = (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[5].FindControl("drpQualification");
                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["Col1"] = TextBoxName.Text;
                        dtCurrentTable.Rows[i - 1]["Col2"] = TextBoxAge.Text;
                        dtCurrentTable.Rows[i - 1]["Col3"] = TextBoxAddress.Text;
                        dtCurrentTable.Rows[i - 1]["Col4"] = RBLGender.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Col5"] = DrpQualification.SelectedValue;
                        rowIndex++;
                    }

                    ViewState["CurrentTable"] = dtCurrentTable;
                    //grvStudentDetails.DataSource = dtCurrentTable;
                    //grvStudentDetails.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            //SetPreviousData();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SetRowData();
                DataTable table = ViewState["CurrentTable"] as DataTable;

                if (table != null)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        string txName = row.ItemArray[1] as string;
                        string txAge = row.ItemArray[2] as string;
                        string txAdd = row.ItemArray[3] as string;
                        string rbGen = row.ItemArray[4] as string;
                        string drpQual = row.ItemArray[5] as string;


                        if (txName != null ||
                            txAge != null ||
                            txAdd != null ||
                            rbGen != null ||
                            drpQual != null)
                        {
                            // Do whatever is needed with this data, 
                            // Possibily push it in database
                            // I am just printing on the page to demonstrate that it is working.
                            Response.Write(string.Format("{0} {1} {2} {3} {4}<br/>", txName, txAge, txAdd, rbGen, drpQual));
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected void ButtonAdd_OnClick(object sender, EventArgs e)
        {
            AddNewRow();
        }
    }
}