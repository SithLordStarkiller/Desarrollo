<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AltaPersonas.aspx.cs" Inherits="Universidad.WebAdministrativaPrueba.Vistas.Formularios.Personas.AltaPersonas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Content/themes/base/datepicker.css" type="text/css" rel="stylesheet" />
    <script src="/Scripts/jquery-2.1.3.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui-1.11.3.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Content/themes/base/datepicker.css" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <div class="panel panel-default" style="margin: 5px; padding: 10px;">
            <div class="panel-heading" style="margin: 5px; padding: 10px;">
                <div class="panel-title">Orden de compra</div>
            </div>
            <div class="form-group row form-group" style="margin: 5px; padding: 10px;">
                <div class="input-group form-group col-md-5">
                    <asp:Label runat="server" Class="input-group-addon" Text="Orden de compra" />
                    <asp:TextBox runat="server" Class="form-control" ID="txtNoOrdenCompra" placeholder="Orden de compra" />
                </div>
                <div class="input-group form-group col-md-5">
                    <asp:Label runat="server" class="input-group-addon">Fecha de pedido </asp:Label>
                    <asp:TextBox runat="server" class="form-control" ID="txtFechaPedido" placeholder="dd/mm/yyyy" />
                </div>
            </div>
            <div class="form-group row col-md-12" style="margin: 5px; padding: 10px;">
                <div class="col-md-6 input-group">
                    <asp:Label runat="server" Class="input-group-addon">Almacen de entrega </asp:Label>
                    <asp:DropDownList runat="server" class="form-control" ID="ddlCatAlmacenes">
                        <asp:ListItem Value="1" Text="Almacen 1" />
                        <asp:ListItem Value="2" Text="Almacen AC" />
                    </asp:DropDownList>
                </div>
                <div class="col-md-6 input-group">
                    <asp:Label runat="server" Class="input-group-addon">Fecha de Entrega </asp:Label>
                    <asp:TextBox runat="server" class="form-control" ID="txtFehaEntrega" placeholder="dd/mm/yyyy" />
                </div>
            </div>
            <div class="form-group row" style="margin: 5px; padding: 10px;">
                <asp:Label runat="server" Class="control-label col-md-2">Cantidad de piezas </asp:Label>
                <asp:TextBox runat="server" class="form-control col-sm-3 " ID="CantidadPiezas" placeholder="CantidadPiezas" />
                <asp:Label runat="server" Class="control-label col-md-1">Estatus </asp:Label>
                <asp:DropDownList runat="server" class="col-sm-2  form-control" Width="150px" ID="ddlEstatusPedido">
                    <asp:ListItem Value="1" Text="Trancito" />
                    <asp:ListItem Value="2" Text="Cancelado" />
                </asp:DropDownList>
                <asp:CheckBox runat="server" ID="chkEntregaFraccionaria" class=" col-lg-offset-1 col-sm-2" Text="  Entrega Fraccionaria  " />
            </div>
        </div>
    </div>
</asp:Content>
