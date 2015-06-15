<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="NuevaOrdenCompra.aspx.cs" Inherits="AplicacionFragancias.SitioWeb.Vistas.Compras.NuevaOrdenCompra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/jquery-2.1.3.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui-1.11.3.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Scripts/bootstrap.js"></script>
    <link rel="stylesheet" href="/Content/bootstrap.css" type="text/css" />
    <link rel="stylesheet" href="/Content/bootstrap.min.css" type="text/css" />
    <script src="/Scripts/bootstrap-multiselect.js" type="text/javascript"></script>
    <link href="/Content/bootstrap-multiselect.css" type="text/css" rel="stylesheet" />
    <script src="/Scripts/bootstrap-datepicker.js" type="text/javascript"></script>
    <link href="/Content/bootstrap-datepicker.css" type="text/css" rel="stylesheet" />
    <script src="/Scripts/bootstrap-select.js" type="text/javascript"></script>
    <link href="/Content/bootstrap-select.css" type="text/css" rel="stylesheet" />

    <script>
        $(document).ready(function () {
            $('.datepicker').datepicker({
                language: "es",
                format: 'dd/mm/yyyy',
                autoclose: true,
                orientation: "buttom"
            });

            $('.selectpicker').selectpicker({
                style: 'btn-default btn-sm'
            });

            <%--$("#<%=ddlCveProveedor.ClientID%>").change(function () {
                var id = ($("#<%=ddlCveProveedor.ClientID%>").val());
                var proveedor = document.getElementById("<%=ddlProveedor.ClientID %>");
                proveedor.value= id;
            });

            $("#<%=ddlProveedor.ClientID%>").change(function () {
                var id = ($("#<%=ddlProveedor.ClientID%>").val());
                var proveedor = document.getElementById("<%=ddlCveProveedor.ClientID %>");
                proveedor.value= id;
            });--%>
        });

        function GuardaProv() {
            $(document).ready(function () {
                var cveProv = $("#<%=txtCveProveedor.ClientID%>")[0].value;
                var nomProv = $("#<%=txtNombreProveedor.ClientID%>")[0].value;
                var rfcProv = $("#<%=txtRfc.ClientID%>")[0].value;
                var dirProv = $("#<%=txtDireccion.ClientID%>")[0].value;
                var telProv = $("#<%=txtTelefonoEmp.ClientID%>")[0].value;
                var nomCont = $("#<%=txtNombreContaco.ClientID%>")[0].value;
                var telCont = $("#<%=txtTelefonoContacto.ClientID%>")[0].value;
                var emailCont = $("#<%=txteMailContacto.ClientID%>")[0].value;

                $.ajax({
                    type: "POST",
                    url: "NuevaOrdenCompra.aspx/GuardaProv",
                    data: '{cveProv: "' + cveProv + '", nomProv : "' + nomProv + '", nomCont: "' + nomCont + '", telCont: "' + telCont + '", emailCont: "' + emailCont + '", rfcProv: "' + rfcProv + '", dirProv: "' + dirProv + '", telProv: "' + telProv + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function () {
                        $('#modalCreaProveedor').modal('hide');
                        window.location.href = "NuevaOrdenCompra.aspx";
                    },
                    failure: function (response) {
                        alert(response.d);
                    }
                });
            });
        }

        function Nuevo() {
            window.location = "NuevaOrdenCompra.aspx";
        }

        function Redirigir() {
            window.location = "OrdenesCompra.aspx";
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" />
    <div class="jumbotron col-md-12">
        <div class="panel panel-default" style="margin: 5px; padding: 10px;">
            <div class="panel-heading" style="margin: 5px; padding: 10px;">
                <div class="panel-title">
                    <h3>Crear orden de compra</h3>
                </div>
            </div>
            <div class="col-lg-offset-4 col-md-4">
                <div class="input-group" style="margin: 10px; padding: 5px;">
                    <asp:Label runat="server" Class="input-group-addon">No Orden de compra</asp:Label>
                    <asp:TextBox runat="server" Class="form-control" type="text" ID="txtOrdenCompra" placeholder="No Orden de compra" AutoPostBack="true" OnTextChanged="txtOrdenCompra_OnTextChanged" />
                </div>
            </div>
            <div class="col-lg-offset-2 col-md-8">
                <div class="input-group input-group-sm" style="margin: 10px; padding: 5px;">
                    <asp:Label runat="server" Class="input-group-addon">Clave del Proveedor</asp:Label>
                    <asp:DropDownList ID="ddlCveProveedor" runat="server" Class="selectpicker" AutoPostBack="true" OnTextChanged="ddlCveProveedor_OnTextChanged" data-live-search="true" title="Selecciona Proveedor"></asp:DropDownList>
                    <asp:Label runat="server" Class="input-group-addon">Nombre del Proveedor</asp:Label>
                    <asp:DropDownList ID="ddlProveedor" runat="server" Class="selectpicker" AutoPostBack="True" OnTextChanged="ddlProveedor_OnTextChanged" data-live-search="true" title="Selecciona Proveedor"></asp:DropDownList>
                    <button type="button" class="col-lg-offset-1 btn btn-default btn-sm" title="Nuevo Proveedor" data-toggle="modal" data-target="#modalCreaProveedor" data-whatever="@mdo">Nuevo Proveedor</button>

                </div>
            </div>
            <div class="row">
                <div class="input-group" style="margin: 10px; padding: 5px;">
                    <asp:Label runat="server" Class="input-group-addon">Fecha de orden de compra</asp:Label>
                    <asp:TextBox runat="server" Class="form-control datepicker" type="text" ID="txtFechaOrdenCompra" placeholder="Fecha de orden de compra" />
                    <asp:Label runat="server" Class="input-group-addon">Fecha de pedido</asp:Label>
                    <asp:TextBox runat="server" Class="form-control datepicker" type="text" ID="txtFechaPedido" placeholder="Fecha de pedido" />
                    <asp:Label runat="server" Class="input-group-addon">Fecha de entrega</asp:Label>
                    <asp:TextBox runat="server" Class="form-control datepicker" type="text" ID="txtFechaEntrega" placeholder="Fecha de entrega" />
                </div>
            </div>
            <div class="row">
                <div class="input-group" style="margin: 10px; padding: 5px;">
                    <asp:Label runat="server" Class="input-group-addon">Estatus inicial</asp:Label>
                    <asp:DropDownList ID="ddlEstatus" runat="server" Class="selectpicker" data-live-search="true" title="Selecciona status"></asp:DropDownList>
                    <asp:Label runat="server" Class="input-group-addon">Condiciones de pago</asp:Label>
                    <asp:DropDownList ID="ddlCondicionesPago" runat="server" Class="selectpicker" data-live-search="true" title="Selecciona pago"></asp:DropDownList>
                    <asp:Label runat="server" Class="input-group-addon">Moneda</asp:Label>
                    <asp:DropDownList ID="ddlMoneda" runat="server" Class="selectpicker" data-live-search="true" title="Selecciona Moneda"></asp:DropDownList>
                    <asp:Label runat="server" Class="input-group-addon">Impuestos</asp:Label>
                    <asp:DropDownList ID="ddlImpuesto" runat="server" Class="selectpicker" data-live-search="true" title="Selecciona Impuesto"></asp:DropDownList>
                </div>
            </div>
            <br />
        </div>
        <div class="panel panel-default" style="margin: 5px; padding: 10px;">
            <div class="panel-heading" style="margin: 5px; padding: 10px;">
                <div class="panel-title">
                    <h4>Productos</h4>
                </div>
                <div class="panel-group">
                    <asp:GridView ID="grvProductos" runat="server"
                        ShowFooter="True" AutoGenerateColumns="False"
                        DataKeyNames="PARTIDA"
                        CellPadding="10000" ForeColor="#333333"
                        GridLines="None" Width="100%" HorizontalAlign="Center"
                        OnRowDataBound="grvProductos_OnRowDataBound"
                        OnRowDeleting="grvProductos_OnRowDeleting"
                        OnRowEditing="grvProductos_OnRowEditing"
                        OnRowCancelingEdit="grvProductos_OnRowCancelingEdit"
                        >
                        <Columns>
                            <asp:TemplateField HeaderText="Partida">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Clave Producto">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlClaveProd" runat="server" Class="selectpicker" data-width="100px" disabled="disabled"></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre Producto">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlNombrePro" runat="server" Class="selectpicker" data-width="170px" disabled="disabled"></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cantidad">
                                <ItemTemplate>
                                    <asp:Label ID="lblCantidad" runat="server" Text='<%# Eval("CANTIDAD") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unidad">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlUnidadPro" runat="server" Class="selectpicker" data-width="80px" disabled="disabled"></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Presentacion">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlPresentacionPro" runat="server" Class="selectpicker" data-width="170px" disabled="disabled"></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Precio Unitario">
                                <ItemTemplate>
                                    <asp:Label ID="lblPrecioUnitario" runat="server" Text='<%# Eval("PRECIOUNITARIO") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Estatus">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlEstatusPro" runat="server" Class="selectpicker" data-width="100px" disabled="disabled"></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha de Entrega">
                                <ItemTemplate>
                                    <asp:Label ID="lblFechaEntrega" runat="server" Text='<%# Eval("FECHAENTREGA", "{0:d}") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SubTotal">
                                <ItemTemplate>
                                    <asp:Label ID="lblSubtotal" runat="server" Text='<%# "$" + Convert.ToInt32(Eval("CANTIDAD")) * Convert.ToDecimal(Eval("PRECIOUNITARIO")) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="True" EditImageUrl="/Imagenes/ico_editar.gif" ButtonType="Image" />
                            <asp:CommandField ShowDeleteButton="True" DeleteImageUrl="/Imagenes/eliminar.gif" ButtonType="Image" />
                        </Columns>
                        <FooterStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="black" />
                        <RowStyle BackColor="#FFFFFF" />
                        <EditRowStyle BackColor="#CCCCCC" />
                        <SelectedRowStyle BackColor="#888888" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#EEEEEE" ForeColor="black" HorizontalAlign="Center" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                    <div class="col-lg-offset-10 col-md-2">
                        <br />
                        <br />
                        <div class="form-group">
                            <label class="control-label">Clave Proveedor:</label>
                            <asp:TextBox type="text" class="form-control form-group-sm" ID="TextBox1" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="panel-footer">
                    <br />
                    <br />
                </div>
                <div style="float: right;">
                    <br />
                    <br/>
                    <button type="button" class="btn btn-primary btn-sm col-lg-offset-1" title="Nuevo Proveedor" data-toggle="modal" data-target="#modalCreaProducto" data-whatever="@mdo">Nuevo Producto</button>
                </div>
                <br />
                <br />
                <br />
            </div>
            <br />
            <br />
        </div>
        <br />
        <div style="float: right;">
            <asp:Button runat="server" ID="btnGuardar" type="button" class="btn btn-success btn-sm" Text="Guardar" OnClick="btnGuardar_OnClick" />
        </div>
        <div class="modal fade" id="modalCreaProducto" tabindex="-1" role="dialog" aria-labelledby="modalLabelProducto" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        <h4 class="modal-title" id="modalLabelProducto">Agregar Producto</h4>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <div class="col-lg-offset-2 col-md-8">
                                <div class="input-group" style="margin: 10px; padding: 5px;">
                                    <label class="input-group-addon">Clave Producto</label>
                                    <asp:DropDownList ID="ddlClaveProducto" runat="server" Class="selectpicker" AutoPostBack="true" data-live-search="true" title="Clave del Producto" OnTextChanged="ddlClaveProducto_OnTextChanged"></asp:DropDownList>
                                    <label class="input-group-addon">Nombre Producto</label>
                                    <asp:DropDownList ID="ddlNombreProducto" runat="server" Class="selectpicker" AutoPostBack="True" data-live-search="true" title="Nombre del Producto" OnTextChanged="ddlNombreProducto_OnTextChanged"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="input-group" style="margin: 10px; padding: 5px;">
                                <label class="input-group-addon">Estatus</label>
                                <asp:DropDownList ID="ddlEstatusPro" runat="server" Class="selectpicker" data-live-search="true" title="Selecciona estatus" />
                                <label class="col-lg-offset-1 input-group-addon ">Unidad</label>
                                <asp:DropDownList ID="ddlUnidad" runat="server" Class="selectpicker" data-live-search="true" title="Selecciona unidad" />
                                <label class="input-group-addon">Presentacion</label>
                                <asp:DropDownList ID="ddlPresentacion" runat="server" Class="selectpicker" data-live-search="true" title="Selecciona Presentacion" />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="input-group" style="margin: 10px; padding: 5px;">
                                <asp:Label runat="server" Class="input-group-addon">Cantidad</asp:Label>
                                <asp:TextBox runat="server" Class="form-control" type="text" ID="txtCantidad" placeholder="No Orden de compra" />
                                <asp:Label runat="server" Class="input-group-addon">Precio Unitario</asp:Label>
                                <asp:TextBox runat="server" Class="form-control" type="text" ID="txtPrecioUnitario" placeholder="No Orden de compra" />
                                <asp:Label runat="server" Class="input-group-addon">Fecha de Entrada</asp:Label>
                                <asp:TextBox runat="server" Class="form-control datepicker" type="text" ID="txtFechaEntrada" placeholder="No Orden de compra" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                            <asp:Button ID="btnAgregarPro" runat="server" class="btn btn-primary" Text="Agregar" OnClick="btnAgregarPro_OnClick" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="modalMensaje" tabindex="-1" role="dialog" aria-labelledby="modalLabelMensaje" aria-hidden="true">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        <h4 class="modal-title" id="modalLabelMensaje">
                            <asp:Label runat="server" ID="lblModalTitulo"></asp:Label></h4>
                    </div>
                    <div class="modal-body">
                        <asp:Label runat="server" ID="lblModalMensaje">Se a creado la orden de compra. ¿deceas ir a la pagina de inicio o deceas crea otra orden de compra?</asp:Label>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal" onclick="Nuevo();">Nueva</button>
                        <button type="button" class="btn btn-default" data-dismiss="modal" onclick="Redirigir();">Redirigir</button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="modalCreaProveedor" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        <h4 class="modal-title" id="exampleModalLabel">Nuevo proveedor</h4>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <label class="control-label">Clave Proveedor:</label>
                            <asp:TextBox type="text" class="form-control" ID="txtCveProveedor" runat="server" />
                            <label class="control-label">Nombre Proveedor:</label>
                            <asp:TextBox runat="server" class="form-control" ID="txtNombreProveedor"></asp:TextBox>
                            <label class="control-label">RFC:</label>
                            <asp:TextBox runat="server" class="form-control" ID="txtRfc"></asp:TextBox>
                            <label class="control-label">Direccion:</label>
                            <asp:TextBox runat="server" class="form-control" ID="txtDireccion"></asp:TextBox>
                            <label class="control-label">Telefono:</label>
                            <asp:TextBox runat="server" class="form-control" ID="txtTelefonoEmp"></asp:TextBox>
                            <label class="control-label">Nombre Contacto:</label>
                            <asp:TextBox runat="server" class="form-control" ID="txtNombreContaco"></asp:TextBox>
                            <label class="control-label">Telefono Contacto:</label>
                            <asp:TextBox runat="server" class="form-control" ID="txtTelefonoContacto"></asp:TextBox>
                            <label class="control-label">Correo Electronico Contacto:</label>
                            <asp:TextBox runat="server" class="form-control" ID="txteMailContacto"></asp:TextBox>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                        <button type="button" class="btn btn-primary" onclick="GuardaProv();">Guardar</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="modalEditaProducto" tabindex="-1" role="dialog" aria-labelledby="modalLabelEditaProducto" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        <h4 class="modal-title" id="modalLabelEditaProducto">Editar Producto</h4>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <div class="col-lg-offset-2 col-md-8">
                                <div class="input-group input-group-sm" style="margin: 10px; padding: 5px;">
                                    <label class="input-group-addon">Partida</label>
                                    <asp:TextBox runat="server" ID="txtProEditPartida" Width="100" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                    <label class="input-group-addon">Clave Producto</label>
                                    <asp:DropDownList ID="ddlProEditClvProd" runat="server" Class="selectpicker" AutoPostBack="true" data-live-search="true" title="Clave del Producto" OnTextChanged="ddlClaveProducto_OnTextChanged"></asp:DropDownList>
                                    <label class="input-group-addon">Nombre Producto</label>
                                    <asp:DropDownList ID="ddlProEditNomProd" runat="server" Class="selectpicker" AutoPostBack="True" data-live-search="true" title="Nombre del Producto" OnTextChanged="ddlNombreProducto_OnTextChanged"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="input-group" style="margin: 10px; padding: 5px;">
                                <label class="input-group-addon">Estatus</label>
                                <asp:DropDownList ID="ddlProEditEstatus" runat="server" Class="selectpicker" data-live-search="true" title="Selecciona estatus" />
                                <label class="col-lg-offset-1 input-group-addon ">Unidad</label>
                                <asp:DropDownList ID="ddlProEditUnidades" runat="server" Class="selectpicker" data-live-search="true" title="Selecciona unidad" />
                                <label class="input-group-addon">Presentacion</label>
                                <asp:DropDownList ID="ddlProEditPresentacion" runat="server" Class="selectpicker" data-live-search="true" title="Selecciona Presentacion" />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="input-group" style="margin: 10px; padding: 5px;">
                                <asp:Label runat="server" Class="input-group-addon">Cantidad</asp:Label>
                                <asp:TextBox runat="server" Class="form-control" type="text" ID="txtProEditCantidad" placeholder="Cantidad" />
                                <asp:Label runat="server" Class="input-group-addon">Precio Unitario</asp:Label>
                                <asp:TextBox runat="server" Class="form-control" type="text" ID="txtProEditPrecioUnitario" placeholder="Precio unitario" />
                                <asp:Label runat="server" Class="input-group-addon">Fecha de Entrada</asp:Label>
                                <asp:TextBox runat="server" Class="form-control datepicker" type="text" ID="txtProEditFechaEntrega" placeholder="Fecha de entrada" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                            <asp:Button ID="btnEditaProd" runat="server" class="btn btn-primary" Text="Agregar" OnClick="btnEditaProd_OnClick" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
