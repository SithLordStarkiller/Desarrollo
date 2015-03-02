<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Crear.aspx.cs" Inherits="AplicacionFragancias.SitioWeb.OrdenDeCompra.CrearOrdenCompra" %>

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
                <asp:GridView ID="grdCredit" runat="server" AutoGenerateColumns="false" CssClass="GridViewStyle"
                    AllowPaging="True" PageSize="20" DataKeyNames="No_Credito, Cve_Estatus_Credito">
                    <%--OnRowCommand="OnRowCommand" OnRowCreated="OnRowCreated" OnDataBound="OnDataBound"--%>
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <Columns>
                        <asp:HyperLinkField DataTextField="Nombre de producto" DataNavigateUrlFields="No_Credito, Cve_Estatus_Credito"
                            DataNavigateUrlFormatString="../RegionalModule/CreditReview.aspx?creditno={0}&statusid={1}&Flag=M" HeaderText="Nombre de producto" HeaderStyle-Font-Underline="true">
                            <HeaderStyle Font-Underline="True" />
                        </asp:HyperLinkField>
                        <asp:BoundField DataField="Dt_Fecha_Pendiente" HeaderText="Cantidad" ItemStyle-Width="80px">
                            <ItemStyle Width="80px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Dx_Razon_Social" HeaderText="No Lote" />
                        <asp:BoundField DataField="Dx_Nombre_Repre_Legal" HeaderText="Precio Unitario" />
                        <asp:BoundField DataField="Dx_Tel_Fisc" HeaderText="Subtotal por Producto" />
                        <asp:BoundField DataField="Mt_Monto_Solicitado" HeaderText="Fecha de entrega" DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                    </Columns>
                    <PagerTemplate>
                    </PagerTemplate>
                </asp:GridView>
            </div>
        </div>
        <div style="float: right; margin: 0px 80px;">
            <asp:Button runat="server" class=" btn btn-danger " Text="Cancelar Orden de compra" style="margin: 10px;"/>
            <asp:Button runat="server" class=" btn btn-warning " Text="Entrega Fraccionaria"  style="margin: 10px;"/>
            <asp:Button runat="server" class=" btn btn-success disabled" Text="Entrega"  style="margin: 10px;"/>
        </div>
    </div>
</asp:Content>
