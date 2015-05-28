<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="NuevaOrdenCompra.aspx.cs" Inherits="AplicacionFragancias.SitioWeb.Vistas.Compras.NuevaOrdenCompra" %>

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

    <script>
        $(document).ready(function () {
            $('.datepicker').datepicker({
                language: "es",
                format: 'dd/mm/yyyy',
                autoclose: true,
                orientation: "buttom"
            });

            $('.selectpicker').selectpicker({
                style: 'btn-default btn-sm'
            });

            <%--$("#<%=ddlCveProveedor.ClientID%>").change(function () {
                var id = ($("#<%=ddlCveProveedor.ClientID%>").val());
                var proveedor = document.getElementById("<%=ddlProveedor.ClientID %>");
                proveedor.value= id;
            });

            $("#<%=ddlProveedor.ClientID%>").change(function () {
                var id = ($("#<%=ddlProveedor.ClientID%>").val());
                var proveedor = document.getElementById("<%=ddlCveProveedor.ClientID %>");
                proveedor.value= id;
            });--%>
        });

        function GuardaProv() {
            $(document).ready(function () {
                var cveProv = $("#<%=txtCveProveedor.ClientID%>")[0].value;
                var nomProv = $("#<%=txtNombreProveedor.ClientID%>")[0].value;
                var nomCont = $("#<%=txtNombreContaco.ClientID%>")[0].value;
                var telCont = $("#<%=txtTelefonoContacto.ClientID%>")[0].value;
                var emailCont = $("#<%=txteMailContacto.ClientID%>")[0].value;
                $.ajax({
                    type: "POST",
                    url: "NuevaOrdenCompra.aspx/GuardaProv",
                    data: '{cveProv: "' + cveProv + '", nomProv : "' + nomProv + '", nomCont: "' + nomCont + '", telCont: "' + telCont + '", emailCont: "' + emailCont + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            alert(msg);
                            $('#modalCreaProveedor').modal('hide');
                        },
                        failure: function (response) {
                            alert(response.d);
                        }
                    });
            });
        }

        function OnChange(dropdown) {

            var myindex = dropdown.selectedIndex;
            var selValue = dropdown.options[myindex].value;
            var cve = document.getElementById("<%=ddlCveProveedor.ClientID %>");
            var nom = document.getElementById("<%=ddlProveedor.ClientID %>");

            cve.options[2].selected = true;
            nom.options[2].selected = true;
        }


    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="jumbotron col-md-12">
        <br />
        <div class="panel panel-default" style="margin: 5px; padding: 10px;">
            <div class="panel-heading" style="margin: 5px; padding: 10px;">
                <div class="panel-title">
                    <h3>Crear orden de compra</h3>
                </div>
            </div>
            <div class="col-lg-offset-4 col-md-4">
                <div class="input-group" style="margin: 10px; padding: 5px;">
                    <asp:Label runat="server" Class="input-group-addon">No Orden de compra</asp:Label>
                    <asp:TextBox runat="server" Class="form-control" type="text" ID="txtOrdenCompra" placeholder="No Orden de compra" AutoPostBack="true" OnTextChanged="txtOrdenCompra_OnTextChanged" />
                </div>
            </div>
            <div class="col-lg-offset-2 col-md-8">
                <div class="input-group" style="margin: 10px; padding: 5px;">
                    <asp:Label runat="server" Class="input-group-addon">Clave del Proveedor</asp:Label>
                    <asp:DropDownList ID="ddlCveProveedor" runat="server" Class="selectpicker" onchange='OnChange(this);' data-live-search="true" title="Selecciona Proveedor"></asp:DropDownList>
                    <asp:Label runat="server" Class="input-group-addon">Nombre del Proveedor</asp:Label>
                    <asp:DropDownList ID="ddlProveedor" runat="server" Class="selectpicker" onchange='OnChange(this);' data-live-search="true" title="Selecciona Proveedor"></asp:DropDownList>
                    <button type="button" class="col-lg-offset-1 btn btn-primary btn-sm" title="Nuevo Proveedor" data-toggle="modal" data-target="#modalCreaProveedor" data-whatever="@mdo">Nuevo Proveedor</button>

                </div>
            </div>
            <div class="row">
                <div class="input-group" style="margin: 10px; padding: 5px;">
                    <asp:Label runat="server" Class="input-group-addon">Fecha de orden de compra</asp:Label>
                    <asp:TextBox runat="server" Class="form-control datepicker" type="text" ID="txtFechaOrdenCompra" placeholder="Fecha de orden de compra" />
                    <asp:Label runat="server" Class="input-group-addon">Fecha de pedido</asp:Label>
                    <asp:TextBox runat="server" Class="form-control datepicker" type="text" ID="txtFechaPedido" placeholder="Fecha de pedido" />
                    <asp:Label runat="server" Class="input-group-addon">Fecha de entrega</asp:Label>
                    <asp:TextBox runat="server" Class="form-control datepicker" type="text" ID="txtFechaEntrega" placeholder="Fecha de entrega" />
                </div>
            </div>
            <div class="row">
                <div class="input-group" style="margin: 10px; padding: 5px;">
                    <asp:Label runat="server" Class="input-group-addon">Estatus inicial</asp:Label>
                    <asp:DropDownList ID="ddlEstatus" runat="server" Class="selectpicker" data-live-search="true" title="Selecciona status"></asp:DropDownList>
                    <asp:Label runat="server" Class="input-group-addon">Condiciones de pago</asp:Label>
                    <asp:DropDownList ID="ddlCondicionesPago" runat="server" Class="selectpicker" data-live-search="true" title="Selecciona pago"></asp:DropDownList>
                    <asp:Label runat="server" Class="input-group-addon">Moneda</asp:Label>
                    <asp:DropDownList ID="ddlMoneda" runat="server" Class="selectpicker" data-live-search="true" title="Selecciona Moneda"></asp:DropDownList>
                    <asp:Label runat="server" Class="input-group-addon">Impuestos</asp:Label>
                    <asp:DropDownList ID="ddlImpuesto" runat="server" Class="selectpicker" data-live-search="true" title="Selecciona Impuesto"></asp:DropDownList>
                </div>
            </div>
            <br />
        </div>
    </div>



    <div class="modal fade" id="modalCreaProveedor" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 class="modal-title" id="exampleModalLabel">Nuevo Proveedor</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label class="control-label">Clave Proveedor:</label>
                        <asp:TextBox type="text" class="form-control" ID="txtCveProveedor" runat="server" />
                        <label class="control-label">Nombre Proveedor:</label>
                        <asp:TextBox runat="server" class="form-control" id="txtNombreProveedor"></asp:TextBox>
                        <label class="control-label">Nombre Contacto:</label>
                        <asp:TextBox runat="server" class="form-control" id="txtNombreContaco"></asp:TextBox>
                        <label class="control-label">Telefono Contacto:</label>
                        <asp:TextBox runat="server" class="form-control" id="txtTelefonoContacto"></asp:TextBox>
                        <label class="control-label">Correo Electronico Contacto:</label>
                        <asp:TextBox runat="server" class="form-control" id="txteMailContacto"></asp:TextBox>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    <button type="button" class="btn btn-primary" onclick="GuardaProv();">Guardar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
