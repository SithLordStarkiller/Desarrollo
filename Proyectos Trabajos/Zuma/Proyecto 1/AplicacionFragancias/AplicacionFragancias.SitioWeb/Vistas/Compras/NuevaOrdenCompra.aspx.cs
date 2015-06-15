using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
            var cveProv = ((TextBox)sender).Text;

            var prov = new OperaionesCompras().ExisteProveedor(cveProv);

            txtCveProveedor.Text = prov == null ? cveProv : string.Empty;
        }

        [WebMethod]
        public static string GuardaProv(string cveProv, string nomProv, string nomCont, string telCont, string emailCont, string rfcProv, string dirProv, string telProv)
        {
            try
            {
                var proveedor = new COM_PROVEEDORES
                {
                    CVEPROVEEDOR = cveProv,
                    RAZONSOCIAL = nomProv,
                    RFC = rfcProv,
                    DIRECCION = dirProv,
                    TELEFONO = telProv
                };
                var contacto = new COM_PROVEEDORES_CONTACTOS
                {
                    NOMBRECOMPLETO = nomCont,
                    TELEFONOMOVIL = telCont,
                    CORREOELECTRONICO = emailCont
                };

                new OperaionesCompras().InsertaProveedor(proveedor, contacto);

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

            ddlCveProveedor.DataValueField = "CVEPROVEEDOR";
            ddlCveProveedor.DataTextField = "CVEPROVEEDOR";
            ddlCveProveedor.DataSource = lista;
            ddlCveProveedor.DataBind();

            ddlProveedor.DataValueField = "CVEPROVEEDOR";
            ddlProveedor.DataTextField = "RAZONSOCIAL";
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

            ddlEstatusPro.DataValueField = "IDESTATUSCOMPRA";
            ddlEstatusPro.DataTextField = "NOMBREESTATUS";
            ddlEstatusPro.DataSource = listaEstatus;
            ddlEstatusPro.DataBind();

            ddlProEditEstatus.DataValueField = "IDESTATUSCOMPRA";
            ddlProEditEstatus.DataTextField = "NOMBREESTATUS";
            ddlProEditEstatus.DataSource = listaEstatus;
            ddlProEditEstatus.DataBind();

            var listaProductos = new OperaionesCompras().ObtenCatProductos();

            ddlClaveProducto.DataValueField = "CVEPRODUCTO";
            ddlClaveProducto.DataTextField = "CVEPRODUCTO";
            ddlClaveProducto.DataSource = listaProductos;
            ddlClaveProducto.DataBind();

            ddlProEditClvProd.DataValueField = "CVEPRODUCTO";
            ddlProEditClvProd.DataTextField = "CVEPRODUCTO";
            ddlProEditClvProd.DataSource = listaProductos;
            ddlProEditClvProd.DataBind();

            ddlNombreProducto.DataValueField = "CVEPRODUCTO";
            ddlNombreProducto.DataTextField = "NOMBREPRODUCTO";
            ddlNombreProducto.DataSource = listaProductos;
            ddlNombreProducto.DataBind();

            ddlProEditNomProd.DataValueField = "CVEPRODUCTO";
            ddlProEditNomProd.DataTextField = "NOMBREPRODUCTO";
            ddlProEditNomProd.DataSource = listaProductos;
            ddlProEditNomProd.DataBind();

            var listaUnidades = new OperaionesCompras().ObteUnidadesMedidas();

            ddlUnidad.DataValueField = "IDUNIDADESMEDIDA";
            ddlUnidad.DataTextField = "TIPOUNIDAD";
            ddlUnidad.DataSource = listaUnidades;
            ddlUnidad.DataBind();

            ddlProEditUnidades.DataValueField = "IDUNIDADESMEDIDA";
            ddlProEditUnidades.DataTextField = "TIPOUNIDAD";
            ddlProEditUnidades.DataSource = listaUnidades;
            ddlProEditUnidades.DataBind();

            var listaPresentacion = new OperaionesCompras().ObtenPresentacion();

            ddlPresentacion.DataValueField = "IDPRESENTACION";
            ddlPresentacion.DataTextField = "PRESENTACION";
            ddlPresentacion.DataSource = listaPresentacion;
            ddlPresentacion.DataBind();

            ddlProEditPresentacion.DataValueField = "IDPRESENTACION";
            ddlProEditPresentacion.DataTextField = "PRESENTACION";
            ddlProEditPresentacion.DataSource = listaPresentacion;
            ddlProEditPresentacion.DataBind();

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

        protected void ddlClaveProducto_OnTextChanged(object sender, EventArgs e)
        {
            var a = ddlClaveProducto.SelectedValue;
            ddlNombreProducto.SelectedValue = a;
            ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(Page),
                            "Mensaje", "$('#modalCreaProducto').modal({show: 'false'});", true);
        }

        protected void ddlNombreProducto_OnTextChanged(object sender, EventArgs e)
        {
            var a = ddlNombreProducto.SelectedValue;
            ddlClaveProducto.SelectedValue = a;
            ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(Page),
                            "Mensaje", "$('#modalCreaProducto').modal({show: 'false'});", true);
        }

        protected void btnAgregarPro_OnClick(object sender, EventArgs e)
        {
            List<COM_PRODUCTOS_PEDIDOS> listaProductos;

            if (Session["Productos"] == null)
            {
                listaProductos = new List<COM_PRODUCTOS_PEDIDOS>();
            }
            else
            {
                listaProductos = ((List<COM_PRODUCTOS_PEDIDOS>)Session["Productos"]);
            }

            var producto = new COM_PRODUCTOS_PEDIDOS
            {
                CVEPRODUCTO = ddlClaveProducto.SelectedValue,
                IDUNIDADESMEDIDA = Convert.ToInt16(ddlUnidad.SelectedValue),
                IDESTAUSPRODUCTO = Convert.ToInt16(ddlEstatusPro.SelectedValue),
                IDPRESENTACION = Convert.ToInt16(ddlPresentacion.SelectedValue),
                CANTIDAD = Convert.ToDecimal(txtCantidad.Text),
                FECHAENTREGA = (DateTime.ParseExact(txtFechaEntrada.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture)),
                PRECIOUNITARIO = Convert.ToDecimal(txtPrecioUnitario.Text)
            };

            listaProductos.Add(producto);

            var cont = 1;

            foreach (var item in listaProductos)
            {
                item.PARTIDA = cont;
                cont++;
            }


            Session["Productos"] = listaProductos;

            LimpiaCamposProducto();

            BindingGrid();

            var contador = 0;

            foreach (var item in listaProductos)
            {
                var clave = grvProductos.Rows[contador].FindControl("ddlClaveProd") as DropDownList;
                if (clave != null) clave.SelectedValue = item.CVEPRODUCTO;

                var nombre = grvProductos.Rows[contador].FindControl("ddlNombrePro") as DropDownList;
                if (nombre != null) nombre.SelectedValue = item.CVEPRODUCTO;

                var unidad = grvProductos.Rows[contador].FindControl("ddlUnidadPro") as DropDownList;
                if (unidad != null) unidad.SelectedValue = item.IDUNIDADESMEDIDA.ToString();

                var presentacion = grvProductos.Rows[contador].FindControl("ddlPresentacionPro") as DropDownList;
                if (presentacion != null) presentacion.SelectedValue = item.IDPRESENTACION.ToString();

                var estatus = grvProductos.Rows[contador].FindControl("ddlEstatusPro") as DropDownList;
                if (estatus != null) estatus.SelectedValue = item.IDESTAUSPRODUCTO.ToString();

                contador++;
            }
        }

        private void LimpiaCamposProducto()
        {
            ddlUnidad.SelectedValue = "1";
            ddlEstatusPro.SelectedValue = "1";
            ddlPresentacion.SelectedValue = "1";
            txtCantidad.Text = string.Empty;
            txtFechaEntrada.Text = string.Empty;
            txtPrecioUnitario.Text = string.Empty;
        }

        public void BindingGrid()
        {
            var listaProductos = ((List<COM_PRODUCTOS_PEDIDOS>)Session["Productos"]);

            grvProductos.DataSource = listaProductos;
            grvProductos.DataBind();
        }

        protected void grvProductos_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            //unidades
            var listaUnidades = new OperaionesCompras().ObteUnidadesMedidas();

            var ddlUnidades = new DropDownList();


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ddlUnidades = ((DropDownList)(e.Row.FindControl("ddlUnidadPro")));
            }

            if (ddlUnidades != null)
            {
                ddlUnidades.DataSource = listaUnidades;
                ddlUnidades.DataValueField = "IDUNIDADESMEDIDA";
                ddlUnidades.DataTextField = "TIPOUNIDAD";
                ddlUnidades.DataBind();
            }

            //nombre y clave producto
            var listaNombrePro = new OperaionesCompras().ObtenCatProductos();

            var ddlNombre = new DropDownList();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ddlNombre = ((DropDownList)(e.Row.FindControl("ddlNombrePro")));
            }

            if (ddlNombre != null)
            {
                ddlNombre.DataSource = listaNombrePro;
                ddlNombre.DataValueField = "CVEPRODUCTO";
                ddlNombre.DataTextField = "NOMBREPRODUCTO";
                ddlNombre.DataBind();
            }

            var ddlClaveProductoGv = new DropDownList();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ddlClaveProductoGv = ((DropDownList)(e.Row.FindControl("ddlClaveProd")));
            }

            if (ddlClaveProducto != null)
            {
                ddlClaveProductoGv.DataSource = listaNombrePro;
                ddlClaveProductoGv.DataValueField = "CVEPRODUCTO";
                ddlClaveProductoGv.DataTextField = "CVEPRODUCTO";
                ddlClaveProductoGv.DataBind();
            }

            // presentacion

            var listaPrecentacion = new OperaionesCompras().ObtenPresentacion();

            var ddlListaPresentacion = new DropDownList();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ddlListaPresentacion = ((DropDownList)(e.Row.FindControl("ddlPresentacionPro")));
            }

            if (ddlNombre != null)
            {
                ddlListaPresentacion.DataSource = listaPrecentacion;
                ddlListaPresentacion.DataValueField = "IDPRESENTACION";
                ddlListaPresentacion.DataTextField = "PRESENTACION";
                ddlListaPresentacion.DataBind();
            }

            //Estatus

            var listaEstatus = new OperaionesCompras().ObtenEstatusCompras();

            var ddlListaEstatus = new DropDownList();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ddlListaEstatus = ((DropDownList)(e.Row.FindControl("ddlEstatusPro")));
            }

            if (ddlNombre != null)
            {
                ddlListaEstatus.DataSource = listaEstatus;
                ddlListaEstatus.DataValueField = "IDESTATUSCOMPRA";
                ddlListaEstatus.DataTextField = "NOMBREESTATUS";
                ddlListaEstatus.DataBind();
            }
        }

        protected void btnGuardar_OnClick(object sender, EventArgs e)
        {
            var listaImpuesto = new OperacionesFacturacion().ObtenCatalogoImpuestos();
            var noOrdenCompra = txtOrdenCompra.Text;
            var cveProveedor = ddlCveProveedor.SelectedValue;
            var fechaOrdenCompra = Convert.ToDateTime(txtFechaOrdenCompra.Text);
            var fechaPedido = Convert.ToDateTime(txtFechaPedido.Text);
            var fechaEntrega = Convert.ToDateTime(txtFechaEntrega.Text);
            var estatus = Convert.ToInt16(ddlEstatus.SelectedValue);
            var condicionesPago = Convert.ToInt16(ddlCondicionesPago.SelectedValue);
            var moneda = Convert.ToInt16(ddlMoneda.SelectedValue);
            var impuestos = Convert.ToInt16(ddlImpuesto.SelectedValue);

            var listaProductos = (List<COM_PRODUCTOS_PEDIDOS>)Session["Productos"];

            var contador = 1;
            var subTotal = 0.00m;
            foreach (var item in listaProductos)
            {
                item.PARTIDA = contador;
                subTotal = item.CANTIDAD * item.PRECIOUNITARIO;
                contador++;
            }

            var facCatImpuesto = listaImpuesto.FirstOrDefault(r => r.IDIMPUESTO == impuestos);

            if (facCatImpuesto != null)
            {
                decimal impuestoCalculado;

                if (facCatImpuesto.PORSENTAGEIMPUESTO > 0)
                {
                    impuestoCalculado = 1 + (facCatImpuesto.PORSENTAGEIMPUESTO / 100);
                }
                else
                {
                    impuestoCalculado = 0;
                }

                var ordenCompra = new COM_ORDENCOMPRA
                {
                    NOORDENCOMPRA = noOrdenCompra,
                    CVEPROVEEDOR = cveProveedor,
                    IDESTATUSCOMPRA = estatus,
                    IDCONDICIONESPAGO = condicionesPago,
                    IDIMPUESTO = impuestos,
                    IDMONEDA = moneda,
                    FECHAENTREGA = fechaEntrega,
                    FECHAORDENCOMPRA = fechaOrdenCompra,
                    FECHAPEDIDO = fechaPedido,
                    SUBTOTAL = subTotal,
                    TOTAL = subTotal * impuestoCalculado
                };

                Session.Remove("Productos");

                new OperaionesCompras().InsertaOrdenCompra(ordenCompra, listaProductos);
                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(Page), "Mensaje", "$('#modalMensaje').modal('show');", true);
            }
        }

        protected void grvProductos_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var partida = Convert.ToInt32(grvProductos.DataKeys[e.RowIndex].Values["PARTIDA"]);

            var lista = ((List<COM_PRODUCTOS_PEDIDOS>)Session["Productos"]);

            lista.RemoveAll(r => r.PARTIDA == partida);

            Session["Productos"] = lista;

            BindingGrid();
        }

        protected void grvProductos_OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {

                var obj = grvProductos.DataKeys[e.NewEditIndex];

                var partida = (int)(obj.Values["PARTIDA"]);

                var lista = (List<COM_PRODUCTOS_PEDIDOS>)(Session["Productos"]);
                var producto = lista.FirstOrDefault(r => r.PARTIDA == partida);

                txtProEditPartida.Text = producto.PARTIDA.ToString(CultureInfo.InvariantCulture);
                ddlProEditClvProd.SelectedValue = producto.CVEPRODUCTO;
                ddlProEditNomProd.SelectedValue = producto.CVEPRODUCTO;
                ddlProEditEstatus.SelectedValue = producto.IDESTAUSPRODUCTO.ToString();
                ddlProEditUnidades.SelectedValue = producto.IDUNIDADESMEDIDA.ToString();
                ddlProEditPresentacion.SelectedValue = producto.IDPRESENTACION.ToString();
                txtProEditCantidad.Text = producto.CANTIDAD.ToString();
                txtProEditFechaEntrega.Text = producto.FECHAENTREGA.ToShortDateString();
                txtProEditPrecioUnitario.Text = producto.PRECIOUNITARIO.ToString();

                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(Page), "Mensaje",
                    "$('#modalEditaProducto').modal('show');", true);

                grvProductos.EditIndex = -1;
                BindingGrid();

            }
            catch (NullReferenceException)
            {
            }
        }

        protected void grvProductos_OnRowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grvProductos.EditIndex = -1;
            BindingGrid();
        }

        protected void btnEditaProd_OnClick(object sender, EventArgs e)
        {
            var lista = ((List<COM_PRODUCTOS_PEDIDOS>)Session["Productos"]);

            var producto = lista.FirstOrDefault(r => r.PARTIDA == Convert.ToInt32(txtProEditPartida.Text));

            producto.PARTIDA = Convert.ToInt32(txtProEditPartida.Text);
            producto.CVEPRODUCTO = ddlProEditClvProd.SelectedValue;
            producto.IDESTAUSPRODUCTO = Convert.ToInt16(ddlProEditEstatus.SelectedValue);
            producto.IDUNIDADESMEDIDA = Convert.ToInt16(ddlProEditUnidades.SelectedValue);
            producto.IDPRESENTACION = Convert.ToInt16(ddlProEditPresentacion.SelectedValue);
            producto.CANTIDAD = Convert.ToDecimal(txtProEditCantidad.Text);
            producto.FECHAENTREGA = Convert.ToDateTime(txtProEditFechaEntrega.Text);
            producto.PRECIOUNITARIO = Convert.ToDecimal(txtProEditPrecioUnitario.Text);

            lista = lista.OrderBy(j => j.PARTIDA).ToList();
            Session["Producto"] = lista;
            grvProductos.EditIndex = -1;
            BindingGrid();
        }
    }
}