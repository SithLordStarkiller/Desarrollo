<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="OrdenesCompra.aspx.cs" Inherits="AplicacionFragancias.SitioWeb.Vistas.Compras.OrdenesCompra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/jquery-2.1.3.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui-1.11.3.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Scripts/bootstrap.js"></script>
    <script type="text/javascript" src="/Scripts/bootstrap.min.js"></script>
    <link rel="stylesheet" href="/Content/bootstrap.min.css" type="text/css" />
    <script type="text/javascript" src="/Scripts/bootstrap.min.js"></script>
    <script src="/Scripts/bootstrap-multiselect.js" type="text/javascript"></script>
    <link href="/Content/bootstrap-multiselect.css" type="text/css" rel="stylesheet" />
    <script src="/Scripts/bootstrap-datepicker.js" type="text/javascript"></script>
    <link href="/Content/bootstrap-datepicker.css" type="text/css" rel="stylesheet" />

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
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" />
    <br />
    <br />
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
                    <asp:TextBox runat="server" Class="form-control datepicker" type="text" ID="txtFechaInicio" placeholder="Desde" />
                    <asp:Label runat="server" class="input-group-addon">Hasta: </asp:Label>
                    <asp:TextBox runat="server" class="form-control datepicker" ID="txtFechaFinal" placeholder="Hasta" />
                </div>
            </div>
            <div class="row">
                <div class="input-group form-group col-lg-offset-5 col-md-4">
                    <asp:Label runat="server" Class="input-group-addon">Estatus </asp:Label>
                    <asp:ListBox runat="server" ID="lbxEstatusPedido" SelectionMode="Multiple" />
                </div>
            </div>
        </div>
        <br/>
        <div class="panel panel-default" style="margin: 5px; padding: 10px;">
            <asp:GridView ID="grvProductos" runat="server"
                    ShowFooter="True" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333"
                    GridLines="None" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="RowNumber" HeaderText="No" />
                        <asp:TemplateField HeaderText="Nombre del Producto">
                            <ItemTemplate>
                                <asp:TextBox ID="txtNombreProducto" runat="server" Class="form-control TextboxWidth" placeholder="Nombre del Producto" Width="95%"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Lote">
                            <ItemTemplate>
                                <asp:TextBox ID="txtLote" runat="server" Class="form-control TextboxWidth" placeholder="Lote" Width="95%" Style="position: relative; align-items: center;"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cantidad">
                            <ItemTemplate>
                                <asp:TextBox ID="txtCantidad" runat="server" Class="form-control TextboxWidth" placeholder="Cantidad" Width="95%"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Precio Unitario">
                            <ItemTemplate>
                                <asp:TextBox ID="txtPrecioUnitario" runat="server" Class="form-control TextboxWidth" placeholder="Precio unitario" Width="95%"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha de entrega">
                            <ItemTemplate>
                                <asp:TextBox ID="txtFechaEntrega" runat="server" CssClass="form-control TextboxWidth datepickers" placeholder="dd/mm/aaaa" Width="95%"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sub Total">
                            <ItemTemplate>
                                <asp:TextBox ID="txtSubTotal" runat="server" Class="form-control TextboxWidth" placeholder="Subtotal" Width="95%"></asp:TextBox>
                            </ItemTemplate>
                            <FooterStyle HorizontalAlign="Right" Height="50px" />
                            <FooterTemplate>
                                <asp:Button ID="ButtonAdd" runat="server"
                                    Text="Nuevo Producto" class="btn btn-default btn-xs" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True" DeleteImageUrl="/Imagenes/eliminar.gif" ButtonType="Image" />
                    </Columns>
                    <FooterStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="black" />
                    <RowStyle BackColor="#FFFFFF" />
                    <EditRowStyle BackColor="#CCCCCC" />
                    <SelectedRowStyle BackColor="#888888" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#EEEEEE" ForeColor="black" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
        </div>
    </div>
</asp:Content>
