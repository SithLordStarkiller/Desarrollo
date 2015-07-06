<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="OrdenesCompra.aspx.cs" Inherits="AplicacionFragancias.SitioWeb.Vistas.Compras.OrdenesCompra" %>

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

    <script type="text/javascript">


        $(document).ready(function () {
            $('#<%=lbxEstatusPedido.ClientID%>').multiselect({
                includeSelectAllOption: true,
                allSelectedText: 'Todos los estatus',
                enableFiltering: true,
                filterPlaceholder: 'Buscar...',
                nonSelectedText: 'Selecciona estatus',
                nSelectedText: 'Seleccionados',
                selectAllText: 'Todos'
            });

            $('.datepicker').datepicker({
                language: "es",
                format: 'dd/mm/yyyy',
                autoclose: true,
                orientation: "buttom"
            });

            $('.selectpicker').selectpicker({
                style: 'btn-default btn-sm'
            });

            $('#exampleModal').on('show.bs.modal', function (event) {
                var button = $(event.relatedTarget);
                var recipient = button.data('whatever');
                var modal = $(this);
                modal.find('.modal-title').text('New message to ' + recipient);
                modal.find('.modal-body input').val(recipient);
            });
        });


        $(function () {
            var $gv = $("table[id$=grvProductos]");
            var $rows = $("> tbody > tr:not(:has(th, table))", $gv);
            var $inputs = $(".datepickers", $rows);

            $rows.css("background-color", "white");

            $inputs.datepicker();
        });


        $(document).ready(function () {
            $("#<%=grvOrdenesCompra.ClientID%> [id*='txtCantidad']").keyup(function () {
                var cantidad = $(this).val();
                var precio = $(this).parent().parent().find("[id*='txtPrecioUnitario']").val();
                $(this).parent().parent().find("[id*='txtSubTotal']").val(cantidad * precio);
            });

            $("#<%=grvOrdenesCompra.ClientID%> [id*='txtPrecioUnitario']").keyup(function () {
                var precio = $(this).val();
                var cantidad = $(this).parent().parent().find("[id*='txtCantidad']").val();
                $(this).parent().parent().find("[id*='txtSubTotal']").val(cantidad * precio);
            });
        });
    </script>
    <style>
        .selectpicker {
            bottom: 0;
            max-width: 100px;
            margin-left: 3px;
            margin-right: 3px;
            margin-top: 0;
            margin-bottom: 0;
            top: 0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" />
    <div class="jumbotron">
        <h2>Ordenes de compra</h2>
        <br />
        <div class="panel panel-default" style="margin: 5px; padding: 10px;">
            <div class="panel-heading" style="margin: 5px; padding: 10px;">
                <div class="panel-title">Listado de ordenes de compra</div>
            </div>
            <br />
            <div class="row">
                <div class="input-group form-group col-lg-offset-3 col-md-6">
                    <asp:Label runat="server" Class="input-group-addon">Fecha de compra desde:</asp:Label>
                    <asp:TextBox runat="server" Class="form-control datepicker" type="text" ID="txtFechaInicio" placeholder="Desde" AutoPostBack="true" OnTextChanged="txtFechaInicio_OnTextChanged" />
                    <asp:Label runat="server" class="input-group-addon">Hasta: </asp:Label>
                    <asp:TextBox runat="server" class="form-control datepicker" ID="txtFechaFinal" placeholder="Hasta" AutoPostBack="True" OnTextChanged="txtFechaFinal_OnTextChanged" />
                </div>
            </div>
            <div class="row">
                <div class="input-group form-group col-lg-offset-5 col-md-4">
                    <asp:Label runat="server" Class="input-group-addon">Estatus </asp:Label>
                    <asp:ListBox runat="server" ID="lbxEstatusPedido" SelectionMode="Multiple" AutoPostBack="true" OnSelectedIndexChanged="lbxEstatusPedido_OnTextChanged" />
                </div>
            </div>
        </div>
        <br />
        <div class="panel panel-default" style="margin: 5px; padding: 10px;">
            <asp:GridView ID="grvOrdenesCompra" runat="server"
                ShowFooter="True" AutoGenerateColumns="False"
                CellPadding="10000" ForeColor="#333333"
                AllowPaging="false"
                PageSize="2"
                DataKeyNames="NOORDENCOMPRA"
                GridLines="None" Width="100%" HorizontalAlign="Center"
                OnSelectedIndexChanging="grvOrdenesCompra_OnSelectedIndexChanging"
                OnRowDataBound="grvOrdenesCompra_OnRowDataBound"
                OnRowDeleting="grvOrdenesCompra_OnRowDeleting"
                OnRowEditing="grvOrdenesCompra_OnRowEditing">
                <Columns>
                    <asp:TemplateField HeaderText="Orden de Compra">
                        <ItemTemplate>
                            <asp:Label ID="lblOrdenCompra" runat="server" Text='<%# Bind("NOORDENCOMPRA") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Estatus">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlEstatusCom" runat="server" Class="selectpicker" data-width="100px" disabled="disabled"></asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fecha">
                        <ItemTemplate>
                            <asp:TextBox ID="txtFechaOrdenCompra" runat="server" Class="form-control input-sm datepicker disabled" placeholder="Fecha" Text='<%# Bind("FECHAORDENCOMPRA", "{0:d}") %>' disabled="disabled"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="Clave prov.">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlCveProducto" runat="server" Class="selectpicker" data-width="100px" disabled="disabled"></asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nombre prov.">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlNombreProv" runat="server" Class="selectpicker" data-width="100px" disabled="disabled"></asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Moneda">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlMoneda" runat="server" Class="selectpicker" data-width="100px" disabled="disabled"></asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Partidas">
                        <ItemTemplate>
                            <asp:TextBox ID="txtPartidas" runat="server" Class="form-control input-sm" placeholder="Partidas" disabled="disabled"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Entregados">
                        <ItemTemplate>
                            <asp:TextBox ID="txtEntregadas" runat="server" Class="form-control input-sm" placeholder="Entregados" disabled="disabled"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SubTotal">
                        <ItemTemplate>
                            <asp:TextBox ID="txtSubTotal" runat="server" Class="form-control input-sm" placeholder="Subtotal" disabled="disabled" Text='<%# Bind("TOTAL")%>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Impuestos">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlImpuestos" runat="server" Class="selectpicker" data-width="100px" disabled="disabled"></asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total">
                        <ItemTemplate>
                            <asp:TextBox ID="txtTotal" runat="server" Class="form-control input-sm" placeholder="Total" disabled="disabled" Text='<%# Bind("TOTAL") %>'></asp:TextBox>
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
            <div style="float: right;">
                <asp:HyperLink runat="server" Text="Crear Orden de compra" class="btn btn-default btn-sm" NavigateUrl="NuevaOrdenCompra.aspx" />
            </div>
            <br />
            <br />
        </div>
    </div>

    <div class="modal fade" id="modalEditaOrdenCompra" tabindex="-1" role="dialog" aria-labelledby="modalLabelProducto" aria-hidden="true">
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
                                <asp:DropDownList ID="ddlClaveProducto" runat="server" Class="selectpicker" AutoPostBack="true" data-live-search="true" title="Clave del Producto"></asp:DropDownList>
                                <label class="input-group-addon">Nombre Producto</label>
                                <asp:DropDownList ID="ddlNombreProducto" runat="server" Class="selectpicker" AutoPostBack="True" data-live-search="true" title="Nombre del Producto"></asp:DropDownList>
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
                        <asp:Button ID="btnAgregarPro" runat="server" class="btn btn-primary" Text="Agregar" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
