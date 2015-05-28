using System;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using AplicacionFragancias.Entidades;
using AplicacionFragancias.LogicaNegocios.Compras;
using Microsoft.Ajax.Utilities;

namespace AplicacionFragancias.SitioWeb.Vistas.Compras
{
    public partial class NuevaOrdenCompra : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                LlenaListaProveedores();
            }
            else
            {
                LlenaListaProveedores();
            }
        }

        protected void txtOrdenCompra_OnTextChanged(object sender, EventArgs e)
        {
            var a = txtOrdenCompra.Text;
            ddlCveProveedor.SelectedValue = a;
            ddlProveedor.SelectedValue = a;
        }



        [WebMethod]
        public static string  GuardaProv(string cveProv, string nomProv, string nomCont, string telCont, string emailCont )
        {
            try
            {

                var proveedor = new COM_PROVEEDORES()
                {
                    CVEPROVEEDOR = cveProv,
                    NOMBREPROVEEDOR = nomProv,
                    NOMBRECONTACTO = nomCont,
                    TELEFONOCONTACTO = telCont,
                    CORREOELECTRONICOCONTACTO = emailCont
                };

                new OperaionesCompras().InsertaProveedor(proveedor);

                return "Proveedor se almaceno correctamente";
            }
            catch (Exception)
            {
                return "Ocurrio un arror al amacenas proveedor";
            }
        }

        public void LlenaListaProveedores()
        {
            var lista = new OperaionesCompras().ObtenListaProveedores();

            ddlCveProveedor.DataValueField = "IDPROVEEDOR";
            ddlCveProveedor.DataTextField = "CVEPROVEEDOR";
            ddlCveProveedor.DataSource = lista;
            ddlCveProveedor.DataBind();

            ddlProveedor.DataValueField = "IDPROVEEDOR";
            ddlProveedor.DataTextField = "NOMBREPROVEEDOR";
            ddlProveedor.DataSource = lista;
            ddlProveedor.DataBind();
        }
    }
}