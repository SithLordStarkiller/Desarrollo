<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.Master" AutoEventWireup="true" CodeBehind="AdminPerfil.aspx.cs" Inherits="PubliPayments.ActualizarDatosUsuario" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxPopupControl" Assembly="DevExpress.Web.v13.2, Version=13.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="./Scripts/restrictors.js?version=3.0"></script>
    <script>
        /*-Inicio- Validadores y restricciones*/
        function ValidateEmpty(toValidate) {
            var valid = false;
            $.each(toValidate, function (key, value) {
                if ($.trim(value.value) == "")
                    valid = true;
            });
            return valid;
        }
        function validatePass(evt) {
            if (evt.charCode == 34 || evt.charCode == 39 || evt.charCode == 44 || evt.charCode == 47 || evt.charCode == 60 || evt.charCode == 62 || evt.charCode == 92) {
                return false;
            }
            return true;
        }

        function updateTips(t) {
            $("#Mensaje").html(t);
            $("#Mensaje").show();
            setTimeout(function () { $("#Mensaje").hide(); }, 6000);
        }
        /*-Fin- Validadores y restricciones*/
        $(document).ready(function () {
            $(document).keypress(function (e) {
                if (e.keyCode == 13) { return false; }
                return true;
            });
            $("input:text,input:password").bind("contextmenu", function (e) {
                return false;
            });
            var mostrarMensaje = false;
            if ($("#<%=lblInformacion.ClientID%>").html().trim() != "") {
                $("#Mensaje").text($("#<%=lblInformacion.ClientID%>").html());
                $("#Mensaje").show();
            }
            $($("#<%= AUNuevoPassword.ClientID%>,#<%= AUConfirmarPassword.ClientID%>")).blur(function () {
                if ($(this).val() != "") {
                    if (validatePassU($(this))) {
                        updateTips("Nueva contraseña invalida");
                    }
                }
            });

            $("#btActualizar").click(function () {
                mostrarMensaje = false;
                if (ValidateEmpty($("#<%= AUPassword.ClientID%>"))) {
                    updateTips("Ingresar contraseña actual para validar");
                    mostrarMensaje = true;
                } 
                //else {
                //    if (validatePassU($("#<= AUPassword.ClientID%>"))) {   ////al descomentar le falta el %
                //        updateTips("Contraseña actual invalida");
                //        mostrarMensaje = true;
                //    }
                //}
                if ($("#<%= AUNuevoPassword.ClientID%>").val() != $("#<%= AUConfirmarPassword.ClientID%>").val() && !mostrarMensaje) {
                    updateTips("La nueva Contraseña y la confirmación no coincide");
                    mostrarMensaje = true;
                }
                if ($.trim($("#<%= AUNuevoPassword.ClientID%>").val()) != "" && $.trim($("#<%= AUConfirmarPassword.ClientID%>").val()) != "" && !mostrarMensaje) {
                    if (validatePassU($("#<%= AUNuevoPassword.ClientID%>")) && validatePassU($("#<%= AUConfirmarPassword.ClientID%>"))) {
                        updateTips("Nueva contraseña invalida");
                        mostrarMensaje = true;
                    }
                }
                if (!mostrarMensaje) {
                    if (BloquearMultiplesClicks(this)) {
                        $("#<%= btActualizarO.ClientID %>").click();
                        return false;
                    }
                }
                return false;
            });
        });
    </script>
    <style type="text/css">
        .ActualizarUsuario{ Width:440px;margin-top: 30px;margin-left: 35%;}    
        #AUContent{border: 1px #666666 solid}           
        .DivbtActualizar{ margin-top: 20px;}
        .TituloAU{font: 20px 'Open Sans', sans-serif;color:  #666666;text-align: left;height: 36px; display: inline-block; width: 100%; vertical-align: middle;background-color: #FAFAFA;}
        #ContentPlaceHolder1_lblInformacion,#Mensaje{display: none;padding-top: 10px;height: 30px;margin-left: -7px;}
        #Mensaje {display: none;margin: -5px 0 0 0;height: 20px;}
    </style>
    <asp:Panel runat="server" ID="ActualizarUsuario" CssClass="ActualizarUsuario" >
        <div id="AUContent">
            <div class="TituloAU"> &nbsp;Actualizar mi perfil</div>
            <div style="background: #FFFFFF;padding: 30px;height: 394px">
                <table id="Table1" style="padding-right: 0;">
                <tr style="height: 60px">
                    <td style="width: 200px" class="TextoFormulario HelpText" HelpText="Usuario del sistema (no editable)">Usuario</td>
                    <td class="AlinearDerecha">
                        <asp:TextBox runat="server" ID="AUUsuario" CssClass="Combos" Width="200px" MaxLength="50"  Enabled="False" />
                    </td>
                </tr>
                 <tr style="height: 60px">
                    <td style="width: 200px" class="TextoFormulario">Nombre</td>
                    <td class="AlinearDerecha">
                        <asp:TextBox runat="server" ID="AUNombre" CssClass="Combos" Width="200px" onkeypress="return validateString(event)" MaxLength="30"  />
                    </td>
                </tr>
                <tr style="height: 60px">
                    <td style="width: 200px" class="TextoFormulario">Email</td>
                    <td class="AlinearDerecha">
                        <asp:TextBox runat="server" ID="AUEmail" CssClass="Combos HelpText" Width="200px" onkeypress="return validateString(event)" MaxLength="50"  HelpText='ej: soporte@publipayments.com'/>
                    </td>
                </tr>
                 <tr style="height: 60px">
                    <td style="width: 200px" class="TextoFormulario">Nueva Contraseña</td>
                    <td class="AlinearDerecha">
                        <asp:TextBox runat="server" TextMode="Password"  ID="AUNuevoPassword" CssClass="Combos HelpText" onkeypress="return validatePass(event)" Width="200px" MaxLength="20"  HelpText='Al menos una letra minúscula (a-z), una letra mayúscula (A-Z) y un carácter numérico (0 - 9) o un carácter especial (@ # $ & / +). (No repetir contraseñas anteriores, longitud min 8 caracteres)'/>
                    </td>
                </tr>
                 <tr style="height: 60px">
                    <td style="width: 200px" class="TextoFormulario">Confirmar contraseña</td>
                    <td class="AlinearDerecha">
                        <asp:TextBox runat="server" TextMode="Password" ID="AUConfirmarPassword" CssClass="Combos HelpText" onkeypress="return validatePass(event)" Width="200px"   MaxLength="20"  HelpText='Al menos una letra minúscula (a-z), una letra mayúscula (A-Z) y un carácter numérico (0 - 9) o un carácter especial (@ # $ & / +). (No repetir contraseñas anteriores, longitud min 8 caracteres)'/>
                    </td>
                </tr>
                 <tr style="height: 60px">
                    <td style="width: 200px" class="TextoFormulario">Contraseña actual</td>
                    <td class="AlinearDerecha">
                        <asp:TextBox runat="server" TextMode="Password" ID="AUPassword" CssClass="Combos HelpText" onkeypress="return validatePass(event)" Width="200px"  MaxLength="20" HelpText="Contraseña actual del sistema (obligatorio)"/>
                    </td>
                </tr>
            </table>
                <div style="height: 40px"><p id="Mensaje" class="MensajeAlerta"></p></div>
                <asp:Label runat="server" ID="lblInformacion"></asp:Label>
            </div>
        </div>
        <div Class="DivbtActualizar">
            <Button ID="btActualizar" class="Botones" style="Width:200px;Height:30px;">Actualizar</Button>
            <asp:Button runat="server" ID="btActualizarO" CssClass="Botones Oculto"  OnClick="btActualizarO_Click"/>
         </div>
    </asp:Panel>
</asp:Content>
