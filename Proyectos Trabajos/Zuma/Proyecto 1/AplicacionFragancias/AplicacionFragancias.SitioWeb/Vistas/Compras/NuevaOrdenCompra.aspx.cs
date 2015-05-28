using System;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using AplicacionFragancias.Entidades;
using AplicacionFragancias.LogicaNegocios.Compras;
using AplicacionFragancias.LogicaNegocios.Facturacion;

namespace AplicacionFragancias.SitioWeb.Vistas.Compras
{
    public partial class NuevaOrdenCompra : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
            }
            else
            {
                LlenarListas();
            }
        }

        protected void txtOrdenCompra_OnTextChanged(object sender, EventArgs e)
        {
            var cveProv = ((TextBox) sender).Text;

            var prov = new OperaionesCompras().ExisteProveedor(cveProv);

            txtCveProveedor.Text = prov == null ? cveProv : string.Empty;
        }

        [WebMethod]
        public static string GuardaProv(string cveProv, string nomProv, string nomCont, string telCont, string emailCont)
        {
            try
            {
                var proveedor = new COM_PROVEEDORES
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

        public void LlenarListas()
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

            var listaMondas = new OperacionesFacturacion().ObtenCatalogosMonedas();

            ddlMoneda.DataValueField = "IDMONEDA";
            ddlMoneda.DataTextField = "NOMBRECORTO";
            ddlMoneda.DataSource = listaMondas;
            ddlMoneda.DataBind();

            var listaImupestos = new OperacionesFacturacion().ObtenCatalogoImpuestos();

            ddlImpuesto.DataValueField = "IDIMPUESTO";
            ddlImpuesto.DataTextField = "NOMBRECORTO";
            ddlImpuesto.DataSource = listaImupestos;
            ddlImpuesto.DataBind();

            var listaTipoPago = new OperacionesFacturacion().ObtenCatalogoTipoPago();

            ddlCondicionesPago.DataValueField = "IDCONDICIONESPAGO";
            ddlCondicionesPago.DataTextField = "CONDICIONPAGO";
            ddlCondicionesPago.DataSource = listaTipoPago;
            ddlCondicionesPago.DataBind();

            var listaEstatus = new OperaionesCompras().ObtenEstatusCompras();
            
            ddlEstatus.DataValueField = "IDESTATUSCOMPRA";
            ddlEstatus.DataTextField = "NOMBREESTATUS";
            ddlEstatus.DataSource = listaEstatus;
            ddlEstatus.DataBind();
        }

        protected void ddlCveProveedor_OnTextChanged(object sender, EventArgs e)
        {
            var a = ddlCveProveedor.SelectedValue;
            ddlProveedor.SelectedValue = a;
        }

        protected void ddlProveedor_OnTextChanged(object sender, EventArgs e)
        {
            var a = ddlProveedor.SelectedValue;
            ddlCveProveedor.SelectedValue = a;
        }
    }
}