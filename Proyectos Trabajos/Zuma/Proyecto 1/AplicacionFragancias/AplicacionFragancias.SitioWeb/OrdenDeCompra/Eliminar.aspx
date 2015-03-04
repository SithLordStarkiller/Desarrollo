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
                <asp:GridView ID="grvStudentDetails" runat="server"
                    ShowFooter="True" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333"
                    GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="RowNumber" HeaderText="SNo" />
                        <asp:TemplateField HeaderText="Student Name">
                            <ItemTemplate>
                                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Student Age">
                            <ItemTemplate>
                                <asp:TextBox ID="txtAge" runat="server"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Student Address">
                            <ItemTemplate>
                                <asp:TextBox ID="txtAddress" runat="server"
                                    Height="55px" TextMode="MultiLine"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Gender">
                            <ItemTemplate>
                                <asp:RadioButtonList ID="RBLGender"
                                    runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="M">Male</asp:ListItem>
                                    <asp:ListItem Value="F">Female</asp:ListItem>
                                </asp:RadioButtonList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qualification">
                            <ItemTemplate>
                                <asp:DropDownList ID="drpQualification" runat="server">
                                    <asp:ListItem Value="G">Graduate</asp:ListItem>
                                    <asp:ListItem Value="P">Post Graduate</asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                            <FooterStyle HorizontalAlign="Right" Width="90px"/>
                            <FooterTemplate>
                                <asp:Button ID="ButtonAdd" runat="server"
                                    Text="Add New Row" OnClick="ButtonAdd_OnClick" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#EFF3FB" />
                    <EditRowStyle BackColor="#2461BF" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
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
