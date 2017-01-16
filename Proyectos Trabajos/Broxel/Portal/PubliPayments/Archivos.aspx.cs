using System;
using System.Data;
using DevExpress.Web.ASPxGridView;
using PubliPayments.Entidades;

namespace PubliPayments
{
    public partial class Archivos : System.Web.UI.Page
    {
        ConexionSql conn = ConexionSql.Instance;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (SessionUsuario.ObtenerDato(SessionUsuarioDato.Dominio) == "0" || "234".Contains(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol)))
            {
                if (!Config.AplicacionActual().Nombre.ToUpper().Contains("SIRA"))    
                    Response.Redirect("unauthorized.aspx");
            }

            if (!IsPostBack)
            {
                gridRar.DataBind();
                gridTxt.DataBind();
            }
        }

        private bool CustomButtonVisibleCriteria(ASPxGridView grid, int visibleIndex)
        {
            object row = grid.GetRow(visibleIndex);
            return ((DataRowView)row)["Error"].ToString().Length > 1;
        }

        protected void gridRar_DataBinding(object sender, EventArgs e)
        {
            gridRar.DataSource = GetRarTable();
        }

        DataTable GetRarTable()
        {
            return conn.ObtenerArchivosConError("zip", false).Tables[0];
        }

        protected void gridTxt_DataBinding(object sender, EventArgs e)
        {
            gridTxt.DataSource = GetTxtTable();
        }

        DataTable GetTxtTable()
        {
            return conn.ObtenerArchivosConError("txt", false).Tables[0];

        }

        protected void gridTxt_DisplayText(object sender, ASPxGridViewColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "Error")
            {
                if (e.Value != null)
                {
                    string value = e.Value.ToString();
                    if (value.Length > 10)
                    {
                        e.DisplayText = value.Substring(0, 10) + "...";
                    }
                }
            }
        }

        protected void gridTxt_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {

        }

        protected void gridTxt_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            //string[] parameter = e.Parameters.Split('|');
            //int index = int.Parse(parameter[0]);


            //DataTable dt = conn.ObtenerArchivosConError("txt", false).Tables[0];
            //String error = dt.Rows[index]["Error"].ToString();

            //try
            //{
            //    Response.Clear();
            //    Response.Buffer = true;
            //    Response.AddHeader("content-disposition", "attachment;filename=reporte.txt");
            //    Response.ContentType = "text/plain";
            //    Response.Output.Write(error);
            //    Response.End();
            //}
            //catch (System.Threading.ThreadAbortException)
            //{
            //}
            gridRar.DataBind();
            gridTxt.DataBind();
            
        }

        protected void btDescargarError_Click(object sender, EventArgs e)
        {
            int index = 30;

            DataTable dt = conn.ObtenerArchivosConError("txt", true).Tables[0];
            String error = dt.Rows[index]["Error"].ToString();

            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=reporte.txt");
                Response.ContentType = "text/plain";
                Response.Output.Write(error);
                Response.End();
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
        }

        protected void gridRar_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            if (e.VisibleIndex == -1) return;
            e.Visible = CustomButtonVisibleCriteria((ASPxGridView)sender, e.VisibleIndex) ?
                DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
        }

        protected void gridTxt_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            if (e.VisibleIndex == -1) return;
            e.Visible = CustomButtonVisibleCriteria((ASPxGridView)sender, e.VisibleIndex) ?
                DevExpress.Utils.DefaultBoolean.True : DevExpress.Utils.DefaultBoolean.False;
        }

        //protected void btActualizar_OnClick(object sender, EventArgs e)
        //{
        //    gridRar.DataBind();
        //    gridTxt.DataBind();
        //}
    }
}