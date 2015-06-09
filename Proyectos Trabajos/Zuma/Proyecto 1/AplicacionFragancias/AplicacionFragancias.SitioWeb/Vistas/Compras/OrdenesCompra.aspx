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

            $('#exampleModal').on('show.bs.modal', function(event) {
                var button = $(event.relatedTarget); 
                var recipient = button.data('whatever');
                var modal = $(this);
                modal.find('.modal-title').text('New message to ' + recipient);
                modal.find('.modal-body input').val(recipient);
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
        <br />
        <div class="panel panel-default" style="margin: 5px; padding: 10px;">
            <div class="panel-heading" style="margin: 5px; padding: 10px;">
                <div class="panel-title">Listado de ordenes de compra</div>
            </div>
            <br />
            <div class="row">
                <div class="input-group form-group col-lg-offset-3 col-md-6">
                    <asp:Label runat="server" Class="input-group-addon">Fecha de compra desde:</asp:Label>
                    <asp:TextBox runat="server" Class="form-control datepicker" type="text" ID="txtFechaInicio" placeholder="Desde" AutoPostBack="true" OnTextChanged="txtFechaInicio_OnTextChanged"/>
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
                GridLines="None" Width="100%" HorizontalAlign="Center"
                OnRowDataBound="grvOrdenesCompra_OnRowDataBound"
                >
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
                    <asp:TemplateField HeaderText="Clave prov.">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlCveProducto" runat="server" Class="selectpicker" data-width="100px" disabled="disabled"></asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nombre prov.">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlNombreProv" runat="server" Class="selectpicker" data-width="100px" disabled="disabled"></asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Moneda">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlMoneda" runat="server" Class="selectpicker" data-width="100px" disabled="disabled"></asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Partidas">
                        <ItemTemplate>
                            <asp:TextBox ID="txtPartidas" runat="server" Class="form-control input-sm" placeholder="Subtotal" disabled="disabled"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Entregados">
                        <ItemTemplate>
                            <asp:TextBox ID="txtEntregadas" runat="server" Class="form-control input-sm" placeholder="Subtotal" disabled="disabled"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SubTotal">
                        <ItemTemplate>
                            <asp:TextBox ID="txtSubTotal" runat="server" Class="form-control input-sm" placeholder="Subtotal" disabled="disabled"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Impuestos">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlImpuestos" runat="server" Class="selectpicker" data-width="100px" disabled="disabled"></asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total">
                        <ItemTemplate>
                            <asp:TextBox ID="txtTotal" runat="server" Class="form-control input-sm" placeholder="Total" disabled="disabled"></asp:TextBox>
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
</asp:Content>
