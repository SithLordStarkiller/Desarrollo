<%@ Page Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmReporteIncidentes.aspx.cs" Inherits="Reportes_frmReporteIncidentes"
    Title="Módulo de Control de Servicios ::: Reporte de Hechos Ocurridos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../Generales/JScript.js"></script>

    <asp:Panel ID="panIncidentes" runat="server">
        <div id="Contenido" class="scrollbar">
            <div>
                <table class="tamanio">
                    <tr runat="server" id="trParametros">
                        <td>
                            <table class="tamanio">
                                <tr>
                                    <td class="titulo">
                                        Reporte de Hechos Ocurridos
                                    </td>
                                </tr>
                                <tr>
                                    <td class="subtitulo">
                                        Criterios de Búsqueda
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="updFiltros" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table style="margin: 0 auto 0 auto;">
                                                    <tr>
                                                        <td colspan="4">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der">
                                                            Servicio:
                                                        </td>
                                                        <td class="izq" colspan="3">
                                                            <asp:DropDownList ID="ddlServicio" runat="server" CssClass="texto" OnSelectedIndexChanged="ddlServicio_SelectedIndexChanged"
                                                                AutoPostBack="True" TabIndex="10">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvServicio" runat="server" ErrorMessage="Debe Seleccionar un Servicio."
                                                                ControlToValidate="ddlServicio" Font-Bold="True" SetFocusOnError="True" ValidationGroup="SICOGUA"
                                                                Text="*">
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der">
                                                            Instalación:
                                                        </td>
                                                        <td class="izq" colspan="3">
                                                            <asp:DropDownList ID="ddlInstalacion" runat="server" CssClass="texto" Enabled="False"
                                                                TabIndex="11">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvInstalacion" runat="server" ErrorMessage="Debe Seleccionar una Instalación"
                                                                ControlToValidate="ddlInstalacion" Font-Bold="True" SetFocusOnError="True" ValidationGroup="SICOGUA"
                                                                Text="*">
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der">
                                                            Zona:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:DropDownList ID="ddlZona" runat="server" CssClass="texto" TabIndex="4">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvZona" runat="server" ErrorMessage="Debe Seleccionar una Zona"
                                                                ControlToValidate="ddlZona" Font-Bold="True" SetFocusOnError="True" ValidationGroup="SICOGUA"
                                                                Text="*">
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td class="der">
                                                            Fecha del Incidente:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:TextBox ID="txbFecha" runat="server" onFocus="javascript:onFocus(this)" onblur="javascript:onLosFocus(this)"
                                                                CssClass="textbox" Width="80px" onkeypress="return validarNoEscritura(event);">
                                                                </asp:TextBox>
                                                            <asp:ImageButton ID="imbFecha" runat="server" ImageUrl="~/Imagenes/Calendar.png"
                                                                Height="18px" Width="18px" />
                                                            <asp:RequiredFieldValidator ID="rfvFecha" runat="server" ErrorMessage="Se Requiere la Fecha."
                                                                Font-Bold="True" ControlToValidate="txbFecha" SetFocusOnError="True" ValidationGroup="SICOGUA">*
                                                            </asp:RequiredFieldValidator>
                                                            <cc1:CalendarExtender ID="calFecha" runat="server" PopupButtonID="imbFecha" TargetControlID="txbFecha">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der">
                                                            Tarjeta Informativa Número:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:TextBox ID="txbNumero" onFocus="javascript:onFocus(this)" onblur="javascript:onLosFocus(this)"
                                                                onKeyDown="if(event.keyCode==13){event.keyCode=9;}" CssClass="textbox" onkeypress="return nvalidar(event);"
                                                                MaxLength="5" Width="100px" runat="server">
                                                            </asp:TextBox>
                                                        </td>
                                                        <td class="der">
                                                            Sede:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:TextBox ID="txbSede" onFocus="javascript:onFocus(this)" onblur="javascript:onLosFocus(this)"
                                                                onKeyDown="if(event.keyCode==13){event.keyCode=9;}" CssClass="textbox" Width="200px"
                                                                runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der">
                                                            Resumen:
                                                        </td>
                                                        <td class="izq" colspan="3">
                                                            <asp:TextBox ID="txbResumen" onFocus="javascript:onFocus(this)" onblur="javascript:onLosFocus(this)"
                                                                onchange="this.value=quitaacentos(this.value)" CssClass="textbox" Width="100%"
                                                                Height="50px" TextMode="MultiLine" runat="server">
                                                            </asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="der">
                                        <asp:Button ID="btnGenerar" runat="server" Text="Generar Reporte" CssClass="boton"
                                            onMouseOver="javascript:this.style.cursor='hand';" ToolTip="Generar Reporte"
                                            ValidationGroup="SICOGUA" OnClick="btnGenerar_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
