<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Editar.aspx.cs" Inherits="AplicacionFragancias.SitioWeb.OrdenDeCompra.EditarOrdenCompra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="jumbotron">
        <div class="panel panel-default" style="margin: 5px; padding: 10px; width: 100%;">
            <div class="panel-heading" style="margin: 5px; padding: 10px;">
                <div class="panel-title">Editar Orden de compra</div>
            </div>
            <div id="grd" style="padding: 10px; width: 100%;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" />
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="IDPRODUCTO" Width="100%"
                    ShowFooter="true" AllowPaging="true" PageSize="4"
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
                        <asp:TemplateField HeaderText="Lote" SortExpression="Name">
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
                                    Text='<%# Bind("FECHAENTREGA") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblFechaEntrega" runat="server"
                                    Text='<%# Bind("FECHAENTREGA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Precio Unitario" SortExpression="Description">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPrecioUnitario" runat="server"
                                    Text='<%# Bind("PRECIOUNITARIO") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPrecioUnitario" runat="server"
                                    Text='<%# Bind("PRECIOUNITARIO") %>'></asp:Label>
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
