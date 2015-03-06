<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Editar.aspx.cs" Inherits="AplicacionFragancias.SitioWeb.OrdenDeCompra.EditarOrdenCompra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="jumbotron">
        <div class="panel panel-default" style="margin: 5px; padding: 10px;">
            <div class="panel-heading" style="margin: 5px; padding: 10px;">
                <div class="panel-title">Orden de compra</div>
            </div>
            <div class="form-group row" style="margin: 5px; padding: 10px;">
                <asp:Label runat="server" Class="control-label col-sm-2">Orden de compra:</asp:Label>
                <asp:TextBox runat="server" Class="form-control TextboxWidth col-sm-4 " type="text" ID="txtNoOrdenCompra" placeholder="Orden de compra" disabled/>
                <asp:Label runat="server" class="control-label col-md-2">Fecha de pedido </asp:Label>
                <asp:TextBox runat="server" class="form-control TextboxWidth col-md-4" ID="txtFechaPedido" placeholder="dd/mm/yyyy"/>
            </div>
            <div class="form-group row" style="margin: 5px; padding: 10px;">
                <asp:Label runat="server" Class="control-label col-md-2">Almacen de entrega </asp:Label>
                <asp:DropDownList runat="server" class="col-sm-3 TextboxWidth form-control" ID="ddlCatAlmacenes" Width="350px" >
                    <asp:ListItem Value="1" Text="Almacen 1"/>
                    <asp:ListItem Value="2" Text="Almacen AC"/>
                </asp:DropDownList>
                <asp:Label runat="server" Class="control-label col-md-2">Fecha de Entrega </asp:Label>
                <asp:TextBox runat="server" class="form-control col-sm-3 TextboxWidth" ID="txtFehaEntrega" placeholder="dd/mm/yyyy"/>
            </div>
            <div class="form-group row" style="margin: 5px; padding: 10px;">
                <asp:Label runat="server" Class="control-label col-md-2">Cantidad de piezas </asp:Label>
                <asp:TextBox runat="server" class="form-control col-sm-3 TextboxWidth" ID="CantidadPiezas" placeholder="CantidadPiezas" />
                <asp:Label runat="server" Class="control-label col-md-1">Estatus </asp:Label>
                <asp:DropDownList runat="server" class="col-sm-2 TextboxWidth form-control" Width="150px" ID="ddlEstatusPedido">
                    <asp:ListItem Value="1" Text="Trancito"/>
                    <asp:ListItem Value="2" Text="Cancelado"/>
                </asp:DropDownList>
                <asp:CheckBox runat="server" ID="chkEsFraccionaria" class=" col-lg-offset-1 col-sm-2"  Text="  Entrega Fraccionaria" />
            </div>
        </div>
        <br />
        <div class="panel panel-default" style="margin: 5px; padding: 10px; width: 100%;">
            <div class="panel-heading" style="margin: 5px; padding: 10px;">
                <div class="panel-title">Editar Orden de compra</div>
            </div>
            <div id="grd" style="padding: 10px; width: 100%;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" />
                <asp:GridView ID="gvProductos" runat="server" AutoGenerateColumns="False" DataKeyNames="IDPRODUCTOS,NOORDENCOMPRA" Width="100%"
                    ShowFooter="true" AllowPaging="true" PageSize="10"
                    AllowSorting="True"
                    OnRowCommand="GridView1_RowCommand"
                    OnPageIndexChanging="GridView1_PageIndexChanging"
                    OnRowEditing="GridView1_RowEditing"
                    OnRowUpdating="GridView1_RowUpdating"
                    OnSorting="GridView1_Sorting"
                    OnRowCancelingEdit="GridView1_RowCancelingEdit">
                    <Columns>
                       <asp:TemplateField HeaderText="Nombre del Producto" InsertVisible="False" SortExpression="Id">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtNombreProducto" runat="server"
                                    Text='<%# Eval("NOMBREPRODUCTO") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblNombreProducto" runat="server"
                                    Text='<%# Bind("NOMBREPRODUCTO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Lote" InsertVisible="False" SortExpression="Id">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtLote" runat="server"
                                    Text='<%# Eval("LOTE") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblLote" runat="server"
                                    Text='<%# Bind("LOTE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cantidad" SortExpression="Name">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtCantidadProducto" runat="server"
                                    Text='<%# Bind("CANTIDADPRODUCTO") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCantidadProducto" runat="server"
                                    Text='<%# Bind("CANTIDADPRODUCTO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha de Entrega" SortExpression="Description">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtFechaEntrega" runat="server"
                                    Text='<%# String.Format("{0:d}", Eval("FECHAENTREGA")) %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblFechaEntrega" runat="server"
                                    Text='<%# String.Format("{0:d}", Eval("FECHAENTREGA")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Precio Unitario" SortExpression="Description">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPrecioUnitario" runat="server"
                                    Text='<%# String.Format("{0:C}", Eval("PRECIOUNITARIO")) %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPrecioUnitario" runat="server"
                                    Text='<%# String.Format("{0:C}", Eval("PRECIOUNITARIO")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Subtotal" SortExpression="Description">
                            <ItemTemplate>
                                <asp:Label ID="lblSubtotal" runat="server"
                                    Text='<%# String.Format("{0:C}",((Convert.ToDecimal(Eval("PRECIOUNITARIO"))) * (Convert.ToDecimal(Eval("CANTIDADPRODUCTO"))))) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Producto Entregado" SortExpression="Description">
                            <EditItemTemplate>
                                <asp:CheckBox ID="chbEntregadoE" runat="server" Checked="<%# Eval("ENTREGADO") %>"></asp:CheckBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chbEntregadoB" runat="server" Checked="<%# Eval("ENTREGADO") %>"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" EditImageUrl="/Imagenes/ico_editar.gif" DeleteImageUrl="/Imagenes/eliminar.gif"
                             ButtonType="Image" UpdateImageUrl="/Imagenes/yes.gif" CancelImageUrl="/Imagenes/delete.png" />
                    </Columns>
                    <FooterStyle BackColor="#ffffff" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#FFFFFF" />
                    <EditRowStyle BackColor="#CCCCCC" />
                    <SelectedRowStyle BackColor="#ffffff" Font-Bold="True" ForeColor="#888888" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
