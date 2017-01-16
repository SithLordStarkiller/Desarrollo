<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.Master" AutoEventWireup="true" CodeBehind="AdminUsuarios.aspx.cs" EnableEventValidation="false" Inherits="PubliPayments.AdminUsuarios" %>
<%@ Register Assembly="DevExpress.Web.v15.2" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/UserControls/TablaUsuarios.ascx" TagPrefix="uc1" TagName="TablaUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="./Styles/jquery-ui.css">
    <script src="./Scripts/jquery-ui-1.10.4.min.js"></script>
    <script src="./Scripts/restrictors.js?version=3.0"></script>
    <script type="text/javascript">
        var RolActual = "<%= RolActual %>";
        var rolHijo = "<%= RolHijo %>";
        var nuevoRol = ["Super Admin", "Administrador <%= (Aplicacion==1)?"Infonavit":"General" %>", "Administrador de despacho", "Supervisor", "Gestor", "Delegación", "Dirección"];
        var PostBack = "<%= IsPostBack %>";
        var asignar = RolActual;
        ++asignar;

        /*-Inicio- Validadores y restricciones*/
        function ValidateEmpty(toValidate) {
            var valid = true;
            $.each(toValidate, function (key, value) {
                if ($.trim(value.val()) == "") {
                    valid = false;
                    updateTips("Se tienen que llenar todos los campos.");
                }
            });
            return valid;
        }

        function ValidateEmail(val) {
            var filter = /[\w-\.]{3,}@([\w-]{2,}\.)*([\w-]{2,}\.)[\w-]{2,4}/;
            if (filter.test(val.val()))
                return true;

            else {
                updateTips("El email no tiene el formato correcto.");
                return false;
            }

        }

        function ValidateUserDomVal(val, nameField) {
            var filter = /[^abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789?_]/;
            if (!filter.test(val.val()))
                return true;

            else {
                updateTips("Formato incorrecto en el campo " + nameField + " ");
                return false;
            }
        }

        function checkLength(o, n, min, max) {
            if (o.val().length > max || o.val().length < min) {
                updateTips("La longitud de " + n + " debe estar entre " + min + " y " + max + ".");
                return false;
            } else {
                return true;
            }
        }

        function validarCorreoExiste(email, Usuario) {

            $("#MensajeUsuario").text("");
            var modelo =
            {
                correo: email.val(),
                Usuario: Usuario.val()
            }
            $.ajax({
                url: 'AdminUsuarios/validarCorreo',
                type: 'POST',
                async: false,
                cache: false,
                data: JSON.stringify(modelo),
                contentType: 'application/json; charset=utf-8',
                success: function (response, result) {
                    result = true;
                    if (response != "OK") {
                        updateTips(response);
                    }
                },
                error: function (msg, text, thrown) {
                    alert("error: " + thrown + msg + text);
                }
            });
            if ($("#MensajeUsuario").text() != "") {
                return false;
            } else {
                return true;
            }

        }
        /*-Fin- Validadores y restricciones*/

        function updateTips(t) {
            $("#MensajeUsuario").html(t);
            $("#MensajeUsuario").show();
            setTimeout(function () { $("#MensajeUsuario").hide(); }, 6000);

            $("#MensajeUsuario1").html(t);
            $("#MensajeUsuario1").show();
            setTimeout(function () { $("#MensajeUsuario1").hide(); }, 6000);
        }

        function limpiarText() {
            $("#<%= NUUsuario.ClientID %>").val("");
            $("#<%= NUNombre.ClientID %>").val("");
            $("#<%= NUEmail.ClientID %>").val("");
        }

        function cargaPopUp() {
            limpiarText();
            if (RolActual != 0) {
                $("#DivRoles select").remove();
                if (rolHijo != -1) {
                    $("#DivRoles").html(nuevoRol[rolHijo]);
                } else {
                    $("#DivRoles").html(nuevoRol[asignar]);
                }
            } else {
                if (rolHijo != -1) {
                    $("#DivRoles select").remove();
                    $("#DivRoles").html(nuevoRol[rolHijo]);
                }
            }
            PopUPUsuario.Show();
        }

        function ValidarPopUP(boton) {
            var bValid;
            bValid = checkLength($("#<%= NUUsuario.ClientID %>"), "Usuario", 3, 30);
            bValid = bValid && checkLength($("#<%= NUNombre.ClientID %>"), "Nombre", 1, 30);
            bValid = bValid && ValidateEmpty([$("#<%= NUUsuario.ClientID %>"), $("#<%= NUNombre.ClientID %>"), $("#<%= NUEmail.ClientID %>")]);
            bValid = bValid && ValidateEmail($("#<%= NUEmail.ClientID %>"));
            bValid = bValid && ValidateUserDomVal($("#<%= NUUsuario.ClientID %>"), "Usuario");

            if (bValid) {
                $("#<%= NURolesH.ClientID %>").val($("#<%= NURolHijo.ClientID %>").val());
                $("#<%= NUPadreAdmin.ClientID %>").val($("#<%= ListAdminU.ClientID %>").val());
                $("#<%= NUPadreSuper.ClientID %>").val($("#<%= ListSupervisorU.ClientID %>").val());
                $("#<%= DelegacionH.ClientID %>").val($("#<%= ListCatDelegacion.ClientID %>").val());
                if (BloquearMultiplesClicks(boton)) {
                    NUAltaUsuario.DoClick();

                    return true;
                }

            } else {
                return false;
            }
        }

        function ValidarPopupEditar(boton) {
            var bValid = true;

            var nombre = document.getElementById("Nombre").id;
            var usuario = document.getElementById("Usuario").id;
            var correo = document.getElementById("Correo").id;

            bValid = ValidateEmpty([$("#" + usuario), $("#" + nombre), $("#" + correo)]);
            bValid = bValid && checkLength($("#" + nombre), "Nombre", 1, 30);
            bValid = bValid && checkLength($("#" + usuario), "Usuario", 1, 30);
            bValid = bValid && checkLength($("#" + correo), "Email", 1, 50);
            bValid = bValid && ValidateEmail($("#" + correo));
            bValid = bValid && ValidateUserDomVal($("#" + usuario), "Usuario");
            bValid = bValid && validarCorreoExiste($("#" + correo), $("#" + usuario));

            if (bValid) {
                if (BloquearMultiplesClicks(boton)) {
                    var modelo = {
                        Usuario: document.getElementById("Usuario").value,
                        Nombre: document.getElementById("Nombre").value,
                        Correo: document.getElementById("Correo").value,
                        Accion: document.getElementById("Accion").value,
                        idUsuario: document.getElementById("idUsuario").value,
                        EsCallCenter: $("input:radio[name='EsCallCenter']:checked").val()
                    };
                    $.ajax({
                        url: 'AdminUsuarios/PopupEditar',
                        type: 'POST',
                        data: JSON.stringify(modelo),
                        contentType: 'application/json; charset=utf-8',
                        success: function (response) {
                            $("#MensajeUsuario1").html("Usuario guardado correctamente");
                            $("#MensajeUsuario1").show();
                            setTimeout(function () {
                                window.location = window.location.href;
                            }, 3000);
                        },
                        error: function (msg, text, thrown) {
                            alert("error: " + thrown + msg + text);
                        }
                    });
                    return true;
                }

            } else {
                return false;
            }
        }

        $(document).ready(function () {
            limpiarText();
            $("#NombreDespacho").html("<%=NombreDespacho%>");
            $(document).keypress(function (e) {
                if (e.keyCode == 13) { return false; }
                return true;
            });
            $("input:text,input:password").bind("contextmenu", function (e) {
                return false;
            });

            $("#<%= NURolHijo.ClientID %>").on("change", function () {
                if ($("#<%= NURolHijo.ClientID %>").val() == 3) {
                    $(".AsignarA").show();
                    $("#UAdminDespachoDiv").show();
                    $("#USupervisorDiv").hide();
                } else if ($("#<%= NURolHijo.ClientID %>").val() == 4) {
                    $(".AsignarA").show();
                    $("#USupervisorDiv").show();
                    $("#UAdminDespachoDiv").hide();
                } else if ($("#<%= NURolHijo.ClientID %>").val() == 2) {
                    $(".AsignarA").hide();
                    $("#USupervisorDiv").hide();
                    $("#UAdminDespachoDiv").hide();
                }
            });
        });

    function editarUsuario(idUsuario) {

        var rol;
        var loc = document.location.href;
        if (loc.indexOf('?') > 0) {
            var getString = loc.split('?')[1];
            var GET = getString.split('&');
            for (var i = 0, l = GET.length; i < l; i++) {
                var tmp = GET[i].split('=');
                rol = unescape(decodeURI(tmp[1]));
            }
        }

        var modelo = {
            idUsuario: idUsuario,
            Rol: rol
        };

        $.ajax({
            url: '/AdminUsuarios/popupEditar',
            type: 'POST',
            data: JSON.stringify(modelo),
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                window.PopupEditar.Show();
                $("#PopUpContainerEditar").html(response);
            },
            error: function (msg, text, thrown) {
                alert("Error - " + msg + text + thrown);
            }
        });
    }

    </script>
    <style type="text/css">
        .BotonGris {
            background: #666666;
            border: 1px #666666;
        }

        #MensajeUsuario {
            display: none;
            margin: -5px 0 0 0;
            height: 20px;
        }

        #MensajeUsuario1 {
            display: none;
            margin: -5px 0 0 0;
            height: 20px;
        }

        #ContentPlaceHolder1_TablaUsuarios_lbUsuariosLondon tr .chkSel {
            display: none;
        }

        .AsignarA {
            display: none;
        }

        .buttonFooter {
            padding-top: 10px;
            padding-bottom: 10px;
        }

        .dxpc-headerContent {
            font: 18px 'Open Sans', sans-serif;
            text-align: left;
        }
    </style>
    <div style="margin-left: 30px; position: relative">
        <!-- inicia Subtitulo y busquedas de Nuevo  Usuario-->
        <div class="TituloSeleccion" style="min-height: 42px; display: inline-block; width: 100%; vertical-align: middle; margin-top: 15px;">
            <div style="max-width: 600px; float: left; width: 340px">
                Administrador de <%= RolActual == 2 ? "Supervisores" : RolActual == 3 ? "Gestores" :RolHijo == 0 ? "Super Administradores":  RolHijo == 1  ? (Aplicacion==1 ?"Admin Infonavit":"Admin General") : RolHijo == 5 ? "Delegaciones":RolHijo == 6  ? "Directores"  : "usuarios" %>
                <%= (RolActual == 0 && RolHijo == -1) ? "<br/><div style='float:left'> Despacho:&nbsp;</div> <div id='NombreDespacho' style='max-width:485px; float:left'></div>" : "" %>
            </div>
            <asp:Image ID="Image1" runat="server" AlternateText="|" ImageUrl="~/imagenes/separador.png" Style="margin-left: 30px; margin-right: 30px; float: left;" />
            <div style="width: 20px; display: inline-block; margin-left: 35px">
                <% if (RolActual == 0)
                   { %>
                <dx:ASPxButton ID="btNuevoUsuario" runat="server" Text="Crear Usuario" AutoPostBack="False" UseSubmitBehavior="false" Width="200px" CssClass="Botones">
                    <ClientSideEvents Click="cargaPopUp" />
                </dx:ASPxButton>
                <% } %>
                <div <%=((RolActual == 0) ? "style='margin-top: 20px;'" : "")%> id="PanelBusqueda">
                    <input type="button" name="ctl00$ContentPlaceHolder1$TablaUsuarios$btLimpiar" value="Restablecer" id="ContentPlaceHolder1_TablaUsuarios_btLimpiar" class="Botones" style="height: 30px; width: 100px;" onclick="window.location = window.location.href;">
                </div>
            </div>
        </div>

        <!-- inicia popup de Nuevo  Usuario-->
        <dx:ASPxPopupControl ID="PopUPUsuario" runat="server" CloseAction="CloseButton" Modal="True" AllowDragging="True" PopupVerticalAlign="TopSides" PopupVerticalOffset="110"
            PopupHorizontalAlign="WindowCenter" ClientInstanceName="PopUPUsuario"
            HeaderText="Nuevo Usuario" PopupAnimationType="Fade" EnableViewState="False" ShowFooter="True">
            <FooterTemplate>
                <dx:ASPxButton runat="server" ID="NuevoUButton" ClientInstanceName="NuevoUButton" Text="Crear Usuario" Width="200px" CssClass="Botones" AutoPostBack="False">
                    <ClientSideEvents Click="function(s, e) { ValidarPopUP(this) }" />
                </dx:ASPxButton>
            </FooterTemplate>
            <FooterStyle CssClass="buttonFooter" />
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                    <div id="PopUpContainer" style="margin-left: 30px">
                        <div style="height: 330px">
                            <table id="Table1" style="padding-right: 0; width: 430px">
                                <tr style="height: 60px">
                                    <td style="width: 200px" class="TextoFormulario">Usuario</td>
                                    <td>
                                        <asp:TextBox runat="server" ID="NUUsuario" CssClass="Combos AlinearIzquierda" Width="200px" onkeypress="return validateStringU(event)" onkeydown="return noCopyKey(event)" MaxLength="30" />
                                    </td>
                                </tr>
                                <tr style="height: 60px">
                                    <td style="width: 200px" class="TextoFormulario">Nombre</td>
                                    <td>
                                        <asp:TextBox runat="server" ID="NUNombre" CssClass="Combos AlinearIzquierda" Width="200px" onkeypress="return validateString(event)" onkeydown="return noCopyKey(event)" MaxLength="30" />
                                    </td>
                                </tr>
                                <tr style="height: 60px">
                                    <td style="width: 200px" class="TextoFormulario">Email</td>
                                    <td>
                                        <asp:TextBox runat="server" ID="NUEmail" CssClass="Combos AlinearIzquierda" Width="200px" onkeypress="return validateString(event)" onkeydown="return noCopyKey(event)" MaxLength="49" />
                                    </td>
                                </tr>
                                <tr style="height: 60px">
                                    <td style="width: 200px" class="TextoFormulario">Rol</td>
                                    <td>
                                        <div id="DivRoles">
                                            <asp:DropDownList runat="server" ID="NURolHijo" Width="200px" CssClass="Combos AlinearIzquierda" DataTextField="NombreRol" DataValueField="idRol" />
                                        </div>
                                    </td>
                                </tr>
                                <tr style="height: 60px">
                                    <td style="width: 200px" class="TextoFormulario">CallCenter</td>
                                    <td>
                                        <div id="DivEsCallCenter">
                                            <asp:RadioButtonList ID="NUEsCallCenter" runat="server" RepeatDirection="Horizontal" Width="200px" Font="10px" >
                                                <asp:ListItem Value="1">Si</asp:ListItem>
                                                <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </td>
                                </tr>
                                <tr class="AsignarA" style="height: 60px">
                                    <td style="width: 200px" class="TextoFormulario">Asignar a</td>
                                    <td>
                                        <div id="UAdminDespachoDiv" style="color: #666666;">
                                            <% 
                                                if (ListAdminU.Items.Count > 0)
                                                { 
                                            %>
                                            <asp:DropDownList runat="server" ID="ListAdminU" Width="200px" CssClass="Combos AlinearIzquierda" DataTextField="Usuario1" DataValueField="idUsuario" />
                                            <% }
                                                else
                                                { %>
                                             No hay Administradores activos    
                                    <% } %>
                                        </div>
                                        <div id="USupervisorDiv" style="color: #666666;">

                                            <% if (ListSupervisorU.Items.Count > 0)
                                               { %>
                                            <asp:DropDownList runat="server" ID="ListSupervisorU" Width="200px" CssClass="Combos AlinearIzquierda" DataTextField="Usuario1" DataValueField="idUsuario" />
                                            <% }
                                               else
                                               { %>
                                                    No hay Supervisores activos    
                                            <% } %>
                                        </div>
                                    </td>
                                </tr>
                                <tr <%=(RolHijo != 5)?"hidden='True'":"" %> style="height: 60px">
                                    <td style="width: 200px" class="TextoFormulario">Delegación</td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="ListCatDelegacion" Width="200px" CssClass="Combos AlinearIzquierda" DataTextField="Descripcion" DataValueField="Delegacion" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="height: 25px">
                            <p id="MensajeUsuario" class="MensajeAlerta"></p>
                        </div>
                    </div>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
        <dx:ASPxButton ID="NUAltaUsuario" runat="server" ClientInstanceName="NUAltaUsuario" ClientVisible="false" OnClick="NUAltaUsuario_Click"></dx:ASPxButton>
        <asp:TextBox runat="server" ID="NURolesH" CssClass="Oculto" />
        <asp:TextBox runat="server" ID="NUPadreAdmin" CssClass="Oculto" />
        <asp:TextBox runat="server" ID="NUPadreSuper" CssClass="Oculto" />
        <asp:TextBox runat="server" ID="DelegacionH" CssClass="Oculto" />
        <!-- termina popup de Nuevo  Usuario-->

        <!-- Inica popup de editar Usuario-->
        <dx:ASPxPopupControl ID="PopupEditar" runat="server" CloseAction="CloseButton" Modal="True" AllowDragging="True" PopupVerticalAlign="TopSides"
            PopupVerticalOffset="110" PopupHorizontalAlign="WindowCenter" ClientInstanceName="PopupEditar" HeaderText="Editar Usuario"
            PopupAnimationType="Fade" EnableViewState="False" ShowFooter="True">
            <FooterTemplate>
                <dx:ASPxButton runat="server" ID="EditarButton" Text="Guardar" Width="200px" CssClass="Botones" AutoPostBack="False">
                    <ClientSideEvents Click="function(s, e) { ValidarPopupEditar(this) }" />
                </dx:ASPxButton>
            </FooterTemplate>
            <FooterStyle CssClass="buttonFooter" />
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server" SupportsDisabledAttribute="True">
                    <div id="PopUpContainer" style="margin-left: 30px">
                        <div style="height: 200px" id="PopUpContainerEditar">
                        </div>
                        <br />
                        <br />
                        <br />
                        <div style="height: 25px; margin-bottom: 10px;">
                            <p id="MensajeUsuario1" class="MensajeAlerta"></p>
                        </div>
                    </div>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
        <dx:ASPxButton ID="btnEditaUsuario" runat="server" ClientInstanceName="btnEditaUsuario" AutoPostBack="False" ClientVisible="false" Click="GuardarUsuario();"></dx:ASPxButton>
        <!-- Finaliza popup de editar Usuario-->

        <!-- inicia tabla de usuarios-->
        <div style="text-align: left; width: 100%; margin-top: 10px">
            <asp:Label runat="server" ID="lblInformacion" CssClass="MensajeAlerta ArribaMenos5" Visible="False"></asp:Label>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <uc1:TablaUsuarios runat="server" ID="TablaUsuarios" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <!-- fin tabla de usuarios-->
    </div>
</asp:Content>


