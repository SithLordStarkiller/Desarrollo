<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Eliminar.aspx.cs" Inherits="AplicacionFragancias.SitioWeb.OrdenDeCompra.Eliminar" %>

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
                <asp:TextBox runat="server" Class="form-control TextboxWidth col-sm-4" type="text" ID="email" placeholder="Orden de compra" />
                <asp:Label runat="server" class="control-label col-md-2">Fecha de pedido </asp:Label>
                <asp:TextBox runat="server" class="form-control TextboxWidth col-md-4" ID="pwd" placeholder="dd/mm/yyyy" />
            </div>
            <div class="form-group row" style="margin: 5px; padding: 10px;">
                <asp:Label runat="server" Class="control-label col-md-2">Almacen de entrega </asp:Label>
                <asp:DropDownList runat="server" class="col-sm-3 TextboxWidth" Width="350px" />
                <asp:Label runat="server" Class="control-label col-md-2">Fecha de Entrega </asp:Label>
                <asp:TextBox runat="server" class="form-control col-sm-3 TextboxWidth" placeholder="dd/mm/yyyy" />
            </div>
            <div class="form-group row" style="margin: 5px; padding: 10px;">
                <asp:Label runat="server" Class="control-label col-md-2">Cantidad de piezas </asp:Label>
                <asp:TextBox runat="server" class="form-control col-sm-3 TextboxWidth" />
                <asp:Label runat="server" Class="control-label col-md-1">Estatus </asp:Label>
                <asp:DropDownList runat="server" class="col-sm-2 TextboxWidth" Width="150px" />
                <asp:CheckBox runat="server" class=" col-lg-offset-1 col-sm-2" Text="  Entrega Fraccionaria  " />
            </div>
        </div>
        <br />
        <div class="panel panel-default" style="margin: 5px; padding: 10px;">
            <div class="panel-heading" style="margin: 5px; padding: 10px;">
                <div class="panel-title">Productos</div>
            </div>
            <div>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" Width="100%"
                    ShowFooter="true" AllowPaging="true" PageSize="4"
                    AllowSorting="True"
                    OnRowCommand="GridView1_RowCommand"
                    OnPageIndexChanging="GridView1_PageIndexChanging"
                    OnRowEditing="GridView1_RowEditing"
                    OnRowUpdating="GridView1_RowUpdating"
                    OnSorting="GridView1_Sorting"
                    OnRowCancelingEdit="GridView1_RowCancelingEdit">
                    <Columns>
                       <asp:TemplateField HeaderText="Id" InsertVisible="False" SortExpression="Id">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server"
                                    Text='<%# Eval("Id") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server"
                                    Text='<%# Bind("Id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Id" ShowHeader="True" />
                        <asp:TemplateField HeaderText="Name" SortExpression="Name">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server"
                                    Text='<%# Bind("Name") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server"
                                    Text='<%# Bind("Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description" SortExpression="Description">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server"
                                    Text='<%# Bind("Description") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server"
                                    Text='<%# Bind("Description") %>'></asp:Label>
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
        <div style="float: right; margin: 0px 80px;">
            <asp:Button runat="server" class=" btn btn-danger " Text="Cancelar Orden de compra" Style="margin: 10px;" />
            <asp:Button runat="server" class=" btn btn-warning " Text="Entrega Fraccionaria" Style="margin: 10px;" />
            <asp:Button runat="server" class=" btn btn-success disabled" Text="Entrega" Style="margin: 10px;" />
        </div>
    </div>
</asp:Content>
