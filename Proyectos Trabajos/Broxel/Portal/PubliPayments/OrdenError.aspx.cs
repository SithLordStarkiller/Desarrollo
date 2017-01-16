using System;
using System.Data;
using System.Web.UI;
using DevExpress.Web;
using PubliPayments.Entidades;

namespace PubliPayments
{
    public partial class OrdenError : Page
    {
        private int _idUsuario = -1;

        ConexionSql conn = ConexionSql.Instance;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionUsuario.ObtenerDato(SessionUsuarioDato.Dominio) == "0" || "4".Contains(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdRol)))
            {
                Response.Redirect("unauthorized.aspx");
            }

            if (!IsPostBack)
            {
                _idUsuario = Convert.ToInt32(SessionUsuario.ObtenerDato(SessionUsuarioDato.IdUsuario));
                OrdenesError.DataBind();
            }
        }
       
        protected void OrdenesError_DataBinding(object sender, EventArgs e)
        {
            OrdenesError.DataSource = GetOrdenErrorTable();
        }

        DataSet GetOrdenErrorTable()
        {
            return conn.ObtenerOrdenesConError(_idUsuario);

        }
        protected void OrdenesError_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            OrdenesError.DataBind();
        }

        protected void OrdenesError_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            if (e.VisibleIndex == -1) return;
            e.Visible = DevExpress.Utils.DefaultBoolean.True;
        }
    }
}