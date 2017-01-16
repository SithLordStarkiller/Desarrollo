<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="AdminDespacho.aspx.cs" Inherits="PubliPayments.AdminDespacho" %>
<%@ Register Assembly="DevExpress.Web.v15.2" Namespace="DevExpress.Web" TagPrefix="dx" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="./Styles/jquery-ui.css">
    <script src="./Scripts/jquery-ui-1.10.4.min.js"></script>
    <script src="./Scripts/restrictors.js?version=3.0"></script>
    <script>
        var rolActual = "<%=RolActual%>";
        /*-Inicio- Validadores y restricciones*/
        function checkLength(o, n, min, max, container) {
            if (o.val().length > max || o.val().length < min) {
                updateTips(container, "La longitud de " + n + " debe estar entre " + min + " y " + max + ".");
                return false;
            } else {
                return true;
            }
        }

        function ValidateEmpty(toValidate, container) {
            var valid = true;
            $.each(toValidate, function (key, value) {
                if ($.trim(value.val()) == "") {
                    valid = false;
                    updateTips(container, "Se tienen que llenar todos los campos.");
                } else {
                    for (var i = 0; i < Watermarks.length; i++) {
                        if ($.trim(value.val()) == Watermarks[i]) {
                            valid = false;
                            updateTips(container, "Se tienen que llenar todos los campos.");
                            if (!valid)
                                return valid;
                        }
                    }

                }
            });
            return valid;
        }

        function ValidateEmail(val, container) {
            var filter = /[\w-\.]{3,}@([\w-]{2,}\.)*([\w-]{2,}\.)[\w-]{2,4}/;
            if (filter.test(val.val()))
                return true;

            else {
                updateTips(container, "El email no tiene el formato correcto.");
                return false;
            }

        }
        function ValidateUserDomVal(val, nameField, container) {
            var filter = /[^abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_?]/;
            if (!filter.test(val.val()))
                return true;

            else {
                updateTips(container, "Formato incorrecto en el campo " + nameField + " ");
                return false;
            }
        }

        function validatePass(evt) {
            if (evt.charCode == 34 || evt.charCode == 39 || evt.charCode == 44 || evt.charCode == 47 || evt.charCode == 60 || evt.charCode == 62 || evt.charCode == 92) {
                return false;
            }
            return true;
        }

        function validatePassU(campo, container) {
            var regExPattern = /^(?=(.*\d){2})(?=.*[A-Z]{2})(?=.*[!!#$%&()*+-.¡@[\]_?]{2})[0-9a-zA-Z!!#$%&()*+-.¡@[\]_?]{10,}/;
            if (regExPattern.test(campo.val())) {
                return true;
            } else {
                updateTips(container, "Contraseña invalida");
                return false;
            }
        }

        function updateTips(container, t) {
            container.html(t);
            container.show();
            setTimeout(function () { container.hide(); }, 6000);
        }

        function ValidarPopUpCrear(boton) {
            var bValid;
            bValid = ValidateEmpty([$("#<%= tbDespacho.ClientID %>"), $("#<%= tbNombreCorto.ClientID%>"), $("#<%= tbNombreUsuario.ClientID%>"), $("#<%= tbNombre.ClientID%>"), $("#<%= tbEmail.ClientID%>")], $("#MensajeUsuario"));
            bValid = bValid && checkLength($("#<%= tbNombreUsuario.ClientID %>"), "Usuario administrador", 3, 30, $("#MensajeUsuario"));
            bValid = bValid && checkLength($("#<%= tbNombre.ClientID %>"), "Nombre", 1, 30, $("#MensajeUsuario"));
            bValid = bValid && ValidateEmail($("#<%= tbEmail.ClientID %>"), $("#MensajeUsuario"));
            bValid = bValid && ValidateUserDomVal($("#<%= tbNombreUsuario.ClientID %>"), "Usuario Administrador", $("#MensajeUsuario"));
            bValid = bValid && ValidateUserDomVal($("#<%= tbNombreCorto.ClientID %>"), "Nombre Corto", $("#MensajeUsuario"));
            if (bValid) {
                if (BloquearMultiplesClicks(boton)) {
                    btAltaDespacho.DoClick();
                    return true;
                }

            } else {
                return false;
            }
        }

        function cargaPopUpCrear() {
            limpiarText();
            PopUPDespacho.Show();
        }

        function ValidarPopUpEdit(boton) {
            var bValid;
            bValid = ValidateEmpty([$("#<%= ADNombre.ClientID %>"), $("#<%= ADNCorto.ClientID%>")], $("#MensajeUsuarioAc"));
        if (bValid) {
            $("#<%= ADEstatusH.ClientID %>").val($("#<%= ADEstatus.ClientID%> input:checked").val());
            if (BloquearMultiplesClicks(boton)) {
                btEditDespacho.DoClick();
                return true;
            } else {
                return false;
            }
        }
    }

    function cargaPopUpEdit() {
        PopUPDespachoEdit.Show();
    }
    function limpiarText() {

        $('#<%= tbDespacho.ClientID %>').val("");
        $('#<%= tbNombreCorto.ClientID %>').val("");
        $('#<%= tbNombreUsuario.ClientID %>').val("");
        $('#<%= tbNombre.ClientID %>').val("");
        $('#<%= tbEmail.ClientID %>').val("");

        $('#<%= tbDespacho.ClientID %>').Watermark(Watermarks[0]);
        $('#<%= tbNombreCorto.ClientID %>').Watermark(Watermarks[1]);
        $('#<%= tbNombreUsuario.ClientID %>').Watermark(Watermarks[2]);
        $('#<%= tbNombre.ClientID %>').Watermark(Watermarks[3]);
        $('#<%= tbEmail.ClientID %>').Watermark(Watermarks[4]);
    }

    function NoLinks() {
        if (rolActual != "0") {
            $("table a").css("color", "black").removeAttr("href").removeClass("dxeHyperlink");
        }
    }

    var Watermarks = ["razón social", "nombre corto ", "usuario", "nombre", "email"];
    $(document).ready(function () {
        limpiarText();
        $(document).keypress(function (e) {
            if (e.keyCode == 13) { return false; }
            return true;
        });
        $("input:text,input:password").bind("contextmenu", function () {
            return false;
        });

        $("#ContentPlaceHolder1_GVDespachos").on("click", ".btEditarDominio", function () {
            $("#<%= ADNombre.ClientID %>").val($(this).closest("tr")[0].cells[0].textContent);
                $("#<%= ADNCorto.ClientID %>").val($(this).closest("tr")[0].cells[1].textContent);
                $("#<%= ADidDominio.ClientID %>").val($(this).closest("tr")[0].getElementsByClassName("TbidDominioHidden")[0].textContent);

                if ($(this).closest("tr")[0].getElementsByClassName("TbEstatusHidden")[0].textContent == 1) {
                    $('#<%= ADEstatus.ClientID %>_1').prop('checked', true);
            } else {
                $('#<%= ADEstatus.ClientID %>_0').prop('checked', true);
            }
                $(".MensajeAlerta").hide();
                cargaPopUpEdit();
                return false;
            });
    });
</script>
<style type="text/css">
    .BotonGris{background: #666666;border: 1px #666666;}
    #Mensaje{display: none;margin: 0 0 0 30px;padding-top: 10px;height: 30px;width: 400px;}
    .TbidDominioHidden,.TbEstatusHidden{display: none}
    #MensajeUsuario,#MensajeUsuarioAc {display: none;height: 20px;}
    .buttonFooter{ padding-top: 10px;padding-bottom: 10px}
    .dxpc-headerContent{font: 18px 'Open Sans', sans-serif; text-align: left}
    .PopUpContainer{margin-left: 30px; margin-right: 10px;}
    table a{float: left}
</style>
<div style="margin-left: 30px; position: relative">
    <div class="TituloSeleccion" style="min-height: 42px; display: inline-block; width: 100%; vertical-align: middle; margin-top: 15px;">
        <div style="max-width: 600px;float:left;width: 340px">Administrador de Despachos</div>
        <asp:Image ID="Image1" runat="server" AlternateText="|" ImageUrl="~/imagenes/separador.png" Style="margin-left: 30px; margin-right: 30px;float: left;" />
        <div style="width: 20px; display: inline-block; margin-left: 35px">
                <dx:ASPxButton ID="btNuevoDespacho" runat="server" Text="Crear Despacho" AutoPostBack="False" UseSubmitBehavior="false" Width="200px" CssClass="Botones">
                        <ClientSideEvents Click="cargaPopUpCrear"  />
                </dx:ASPxButton>
                <div  id="Restablecer" style="width: 200px; margin-top: 20px;">
                    <asp:Button runat="server" CssClass="Botones" Text="Restablecer" ID="btLimpiar" Width="100px" Height="30px" OnClick="btLimpiar_OnClick" /> 
                </div>
            </div>
    </div>
     <div style="text-align: left;width: 100%;">
         <asp:Label runat="server" ID="lblInformacion" CssClass="MensajeAlerta ArribaMenos5"></asp:Label>
     </div>
    <!-- tabla de despachos--->
    <div id="DivTabla" style="margin-top: 15px">
        <dx:ASPxGridView ID="GVDespachos" ClientInstanceName="GVDespachos" runat="server" AutoGenerateColumns="False" CssClass="GridCelda" OnCustomColumnSort="GVDespachos_CustomColumnSort"  KeyFieldName="idDominio" Width="1200px" SettingsPager-PageSize="50">
            <Settings ShowFilterRow="True" ShowGroupPanel="True" />
            <ClientSideEvents EndCallback="function(s,e){NoLinks()}" Init="function(s,e){NoLinks()}"/>
            <SettingsPager Position="Bottom">
                <PageSizeItemSettings Items="10, 20, 50, 100" Visible="true"></PageSizeItemSettings>
            </SettingsPager>
             <Styles>
                <EditFormTable Font-Size="12px"></EditFormTable>
                <EditFormCell Font-Size="12px"></EditFormCell>
                <Cell Font-Size="12px"></Cell>
                <AlternatingRow Enabled="True" />
            </Styles>
             <Columns>
                  <dx:GridViewDataHyperLinkColumn FieldName="idDominio" ShowInCustomizationForm="True"  VisibleIndex="0" Caption="Despachos">
                    <PropertiesHyperLinkEdit TextField="NombreDominio" NavigateUrlFormatString='~/AdminUsuarios.aspx?Despacho={0}' />
                  </dx:GridViewDataHyperLinkColumn>
                  <dx:GridViewDataTextColumn FieldName="nom_corto" VisibleIndex="1" Caption="Nombre corto">
                     <Settings AllowHeaderFilter="False" />
                     <Settings AutoFilterCondition="Contains" />
                  </dx:GridViewDataTextColumn> 
                  <dx:GridViewDataTextColumn FieldName="FechaAlta" VisibleIndex="2" Caption="Fecha de Alta">
                        <Settings AllowHeaderFilter="False" />
                        <Settings AutoFilterCondition="Contains" />
                  </dx:GridViewDataTextColumn>
                 <dx:GridViewDataTextColumn FieldName="EstatusTxt" VisibleIndex="3" Caption="Estatus">
                    <Settings AllowHeaderFilter="False" />
                    <Settings AutoFilterCondition="Contains" />
                 </dx:GridViewDataTextColumn>
                 <dx:GridViewDataTextColumn VisibleIndex="10">
                    <DataItemTemplate>
                      <div style="padding: 5px;text-align: center;">
                            <asp:Button ID="Button1"  runat="server" Width="100px" Height="25px" Text="Modificar" CssClass="Botones btEditarDominio" />
                       </div>
                        <div style="display:none">
                          <asp:Label ID="Label1"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"idDominio") %>' CssClass="TbidDominioHidden" ClientIDMode="AutoID" />
                          <asp:Label ID="Label2"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Estatus") %>' CssClass="TbEstatusHidden" ClientIDMode="AutoID" />
                        </div>
                    </DataItemTemplate>
                 </dx:GridViewDataTextColumn>
             </Columns>
        </dx:ASPxGridView>
    </div>
    <!-- fin tabla de despachos--->
    
    <!-- PopUp de nuevo despacho-->
    <dx:ASPxPopupControl ID="PopUPDespacho" runat="server" CloseAction="CloseButton" Modal="True"  AllowDragging="True" PopupVerticalAlign="TopSides" PopupVerticalOffset="100"
            PopupHorizontalAlign="WindowCenter" ClientInstanceName="PopUPDespacho" 
            HeaderText="Nuevo Despacho"  PopupAnimationType="Fade" EnableViewState="False" ShowFooter="True"  >
            <FooterTemplate>
                <dx:ASPxButton runat="server" ID="NuevoDButton" Text="Crear Despacho" Width="200px" CssClass="Botones" AutoPostBack="False">
                    <ClientSideEvents Click="function(s, e) { ValidarPopUpCrear(this) }" />
                </dx:ASPxButton>
            </FooterTemplate>
            <FooterStyle CssClass="buttonFooter"/> 
            <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
            <div class="PopUpContainer">
                <div style="height: 335px" >
                     <table id="TNuevoDespacho" style="padding-right: 0;width:430px">
                        <tr style="height: 60px">
                            <td style="width: 200px" class="TextoFormulario">Nombre </td>
                            <td>
                                <asp:TextBox runat="server" ID="tbDespacho" CssClass="Combos AlinearIzquierda" Width="200px" onkeypress="return validateString(event)" onkeydown="return noCopyKey(event)" MaxLength="30"/>
                            </td>
                        </tr>
                        <tr style="height: 60px">
                            <td style="width: 200px" class="TextoFormulario">Nombre Corto  </td>
                            <td>
                                <asp:TextBox runat="server" ID="tbNombreCorto" CssClass="Combos AlinearIzquierda" Width="200px" MaxLength="20" ToolTip="Sólo se permiten nombres con letras MAYUSCULAS"  onkeypress="return validateStringD(event)" onkeydown="return noCopyKey(event)"/>
                            </td>
                        </tr>
                        <tr style="height: 60px">
                            <td style="width: 200px" class="TextoFormulario">Usuario Administrador</td>
                            <td>
                                <asp:TextBox runat="server" ID="tbNombreUsuario" CssClass="Combos AlinearIzquierda" Width="200px" onkeypress="return validateStringU(event)" onkeydown="return noCopyKey(event)" MaxLength="30" />
                            </td>
                        </tr>
                          <tr style="height: 60px">
                            <td style="width: 200px" class="TextoFormulario">Nombre Administrador</td>
                            <td>
                                <asp:TextBox runat="server" ID="tbNombre" CssClass="Combos AlinearIzquierda" Width="200px" onkeypress="return validateString(event)" onkeydown="return noCopyKey(event)" MaxLength="30" />
                            </td>
                        </tr>
                        <tr style="height: 60px">
                            <td style="width: 200px" class="TextoFormulario">Email  </td>
                            <td>
                                <asp:TextBox runat="server" ID="tbEmail" CssClass="Combos AlinearIzquierda" AutoCompleteType="Disabled" Width="200px" onkeypress="return validateString(event)" onkeydown="return noCopyKey(event)" MaxLength="49" />
                            </td>
                        </tr>
                    </table>        
                </div>
                <div style="height: 30px">
                     <p id="MensajeUsuario" class="MensajeAlerta"></p>
                </div>
            </div>
            </dx:PopupControlContentControl>
            </ContentCollection>
            </dx:ASPxPopupControl>
        <dx:ASPxButton ID="btAltaDespacho" runat="server" ClientInstanceName="btAltaDespacho" ClientVisible="false" OnClick="btCrearDespacho_Click"></dx:ASPxButton>
   
    <!--Fin PopUp de nuevo despacho-->
    
    <!-- PopUp de editar despacho-->
    <dx:ASPxPopupControl ID="PopUPDespachoEdit" runat="server" CloseAction="CloseButton" Modal="True"  AllowDragging="True" PopupVerticalAlign="TopSides" PopupVerticalOffset="110"
            PopupHorizontalAlign="WindowCenter" ClientInstanceName="PopUPDespachoEdit" 
            HeaderText="Actualizar Despacho"  PopupAnimationType="Fade" EnableViewState="False" ShowFooter="True"  >
            <FooterTemplate>
                <dx:ASPxButton runat="server" ID="EditarDButton" Text="Actualizar Despacho" Width="200px" CssClass="Botones" AutoPostBack="False">
                    <ClientSideEvents Click="function(s, e) { ValidarPopUpEdit(this) }" />
                </dx:ASPxButton>
            </FooterTemplate>
            <FooterStyle CssClass="buttonFooter"/> 
            <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server" SupportsDisabledAttribute="True">
                <div class="PopUpContainer">
                <div style="height: 210px" >
                    <table id="Table1" style="padding-right: 0;width:430px">
                         <tr style="height: 60px">
                <td style="width: 200px" class="TextoFormulario">Nombre </td>
                <td>
                    <asp:TextBox runat="server" ID="ADNombre" CssClass="Combos AlinearIzquierda" Width="200px" MaxLength="50"/>
                </td>
            </tr>
                         <tr style="height: 60px">
                <td style="width: 200px" class="TextoFormulario">Nombre Corto  </td>
                <td>
                    <asp:TextBox runat="server" ID="ADNCorto" CssClass="Combos AlinearIzquierda" Width="200px" MaxLength="20" onkeypress="return validateNC(event)" />
                </td>
            </tr>
                         <tr style="height: 60px">
                <td style="width: 200px" class="TextoFormulario">Estatus</td>
                <td>
                    <asp:RadioButtonList ID="ADEstatus" runat="server" CssClass="AlinearIzquierda">
                       <asp:ListItem> Inactivo</asp:ListItem>
                        <asp:ListItem> Activo</asp:ListItem>
                   </asp:RadioButtonList>
                </td>
            </tr>
                  </table> 
                </div>
                <div style="height: 30px">
                    <p id="MensajeUsuarioAc" class="MensajeAlerta" ></p>   
                </div>
        </div>

            </dx:PopupControlContentControl>
            </ContentCollection>
            </dx:ASPxPopupControl>
        <dx:ASPxButton ID="btEditDespacho" ClientInstanceName="btEditDespacho"  runat="server"  ClientVisible="false" OnClick="btEditDespacho_Click"></dx:ASPxButton>
        <asp:TextBox runat="server" ID="ADEstatusH" CssClass="Oculto" />
        <asp:TextBox runat="server" ID="ADidDominio" CssClass="Oculto" />
    <!--Fin PopUp de editar despacho-->
</div>
</asp:Content>
