<%@ Page Title="" Language="C#" EnableEventValidation="true" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Crear.aspx.cs" Inherits="AplicacionFragancias.SitioWeb.OrdenDeCompra.CrearOrdenCompra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="/Content/themes/base/datepicker.css" type="text/css" rel="stylesheet" />
    <script src="/Scripts/jquery-2.1.3.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui-1.11.3.js" type="text/javascript"></script>
    <script src="/Scripts/datepicker-es.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Content/themes/base/datepicker.css" type="text/css" />
    <link rel="stylesheet" href="/Content/Sitio.css" type="text/css" />

    <script type="text/javascript">
        $(document).ready(function () {

            $("#<%=txtFehaEntrega.ClientID%>").datepicker();
            $("#<%=txtFechaPedido.ClientID%>").datepicker();
        });
    </script>

    <script>
        $(function () {
            var $gv = $("table[id$=grvStudentDetails]");
            var $rows = $("> tbody > tr:not(:has(th, table))", $gv);
            var $inputs = $(".datepickers", $rows);

            $rows.css("background-color", "white");

            $inputs.datepicker();
        });
    </script>
    
    <script type="text/javascript">

        $(document).ready(function () {

            $("#<%=grvStudentDetails.ClientID%> [id*='txtCantidad']").keyup(function() {
                var cantidad = $(this).val();
                var precio = $(this).parent().parent();

                alert('Cantidad= ' + cantidad + ' Precio= ' + precio);

                //$("td:eq(3) span", tr).html($(this).val() * precio);

            });

});

</script>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" />
    <div class="jumbotron">
        <div class="panel panel-default" style="margin: 5px; padding: 10px;">
            <div class="panel-heading" style="margin: 5px; padding: 10px;">
                <div class="panel-title">Orden de compra</div>
            </div>
            <div class="form-group row" style="margin: 5px; padding: 10px;">
                <asp:Label runat="server" Class="control-label col-sm-2">Orden de compra:</asp:Label>
                <asp:TextBox runat="server" Class="form-control TextboxWidth col-sm-4" type="text" ID="txtNoOrdenCompra" placeholder="Orden de compra" />
                <asp:Label runat="server" class="control-label col-md-2">Fecha de pedido </asp:Label>
                <asp:TextBox runat="server" class="form-control TextboxWidth col-md-4" ID="txtFechaPedido" placeholder="dd/mm/yyyy" />
            </div>
            <div class="form-group row" style="margin: 5px; padding: 10px;">
                <asp:Label runat="server" Class="control-label col-md-2">Almacen de entrega </asp:Label>
                <asp:DropDownList runat="server" class="col-sm-3 TextboxWidth form-control" ID="ddlCatAlmacenes" Width="350px">
                    <asp:ListItem Value="1" Text="Almacen 1" />
                    <asp:ListItem Value="2" Text="Almacen AC" />
                </asp:DropDownList>
                <asp:Label runat="server" Class="control-label col-md-2">Fecha de Entrega </asp:Label>
                <asp:TextBox runat="server" class="form-control col-sm-3 TextboxWidth" ID="txtFehaEntrega" placeholder="dd/mm/yyyy" />
            </div>
            <div class="form-group row" style="margin: 5px; padding: 10px;">
                <asp:Label runat="server" Class="control-label col-md-2">Cantidad de piezas </asp:Label>
                <asp:TextBox runat="server" class="form-control col-sm-3 TextboxWidth" ID="CantidadPiezas" placeholder="CantidadPiezas" />
                <asp:Label runat="server" Class="control-label col-md-1">Estatus </asp:Label>
                <asp:DropDownList runat="server" class="col-sm-2 TextboxWidth form-control" Width="150px" ID="ddlEstatusPedido">
                    <asp:ListItem Value="1" Text="Trancito" />
                    <asp:ListItem Value="2" Text="Cancelado" />
                </asp:DropDownList>
                <asp:CheckBox runat="server" class=" col-lg-offset-1 col-sm-2" Text="  Entrega Fraccionaria  " />
            </div>
        </div>
        <br />
        <div class="panel panel-default" style="margin: 5px; padding: 10px;">
            <div class="panel-heading" style="margin: 5px; padding: 10px;">
                <div class="panel-title">Productos</div>
            </div>
            <div>
                <asp:GridView ID="grvStudentDetails" runat="server"
                    ShowFooter="True" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333"
                    GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting" Width="100%">
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
                                    Text="Nuevo Producto" OnClick="ButtonAdd_OnClick" class="btn btn-default" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True" DeleteImageUrl="/Imagenes/eliminar.gif" ButtonType="Image" />
                    </Columns>
                    <FooterStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#FFFFFF" />
                    <EditRowStyle BackColor="#CCCCCC" />
                    <SelectedRowStyle BackColor="#888888" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </div>
        </div>
        <div style="float: right;">
            <asp:Button runat="server" class=" btn btn-danger " Text="Cancelar" Style="margin: 10px 20px 10px 20px;" />
            <asp:Button runat="server" ID="btnAceptar" class=" btn btn-success" Text="Aceptar" Style="margin: 10px 20px 10px 20px;" OnClick="btnAceptar_OnClick" />
        </div>
    </div>
</asp:Content>
