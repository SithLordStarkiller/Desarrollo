﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true" CodeFile="frmCumplimiento.aspx.cs" Inherits="Reportes_frmCumnplimiento" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../Generales/JScript.js"></script>
    <asp:Panel ID="panReportePersonalAsignado" runat="server">
        <div id="Contenido" class="scrollbar">
            <div>
                <table class="tamanio">
                    <tr runat="server" id="trParametros">
                        <td>
                            <table class="tamanio">
                                <tr>
                                    <td class="titulo">
                                        Reporte de Cumplimiento del Servicio</td>
                                </tr>
                                <tr>
                                    <td class="subtitulo">
                                        Criterios de Búsqueda
                                    </td>
                                </tr>
                                <tr>
                                    <td class="izq">
                                        <asp:UpdatePanel ID="updFiltros" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table>
                                                    <tr>
                                                        <td colspan="2">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der">
                                                            Fecha del Reporte:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:TextBox ID="txbFecha" runat="server" onFocus="javascript:onFocus(this)" onblur="javascript:onLosFocus(this)"
                                                                CssClass="textbox" Width="80px" onkeypress="return validarNoEscritura(event);">
                                                            </asp:TextBox>
                                                            <asp:ImageButton ID="imbFecha" runat="server" ImageUrl="~/Imagenes/Calendar.png"
                                                                Height="18px" Width="18px" />
                                                            <asp:RequiredFieldValidator ID="rfvFecha" runat="server" ErrorMessage="Se Requiere la Fecha."
                                                                Font-Bold="True" ControlToValidate="txbFecha" SetFocusOnError="True" ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>
                                                            <asp:CompareValidator ID="covFecha" runat="server" ErrorMessage="La Fecha No Puede Ser Mayor al Día de Hoy."
                                                                ControlToValidate="txbFecha" Font-Bold="True" SetFocusOnError="True" Type="Date"
                                                                ValidationGroup="SICOGUA" ValueToCompare="01/01/0001" Operator="LessThanEqual">*</asp:CompareValidator>
                                                            <cc1:CalendarExtender ID="calFecha" runat="server" PopupButtonID="imbFecha" TargetControlID="txbFecha">
                                                            </cc1:CalendarExtender>
                                                            <cc1:TextBoxWatermarkExtender ID="twmFecha" runat="server" WatermarkText="dd/mm/aaaa"
                                                                TargetControlID="txbFecha">
                                                            </cc1:TextBoxWatermarkExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der">
                                                            Servicio:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:DropDownList ID="ddlServicio" runat="server" CssClass="texto" OnSelectedIndexChanged="ddlServicio_SelectedIndexChanged"
                                                                AutoPostBack="True">
                                                            </asp:DropDownList>
                                                            <%--<asp:RequiredFieldValidator ID="rfvServicio" runat="server" ErrorMessage="Debe Seleccionar un Servicio."
                                                                ControlToValidate="ddlServicio" Font-Bold="True" SetFocusOnError="True" ValidationGroup="SICOGUA"
                                                                Text="*">
                                                            </asp:RequiredFieldValidator>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der">
                                                            Instalación:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:DropDownList ID="ddlInstalacion" runat="server" CssClass="texto" Enabled="False">
                                                            </asp:DropDownList>
                                                            <%--<asp:RequiredFieldValidator ID="rfvRecibe" runat="server" ErrorMessage="Debe Seleccionar una Instalación."
                                                                ControlToValidate="ddlInstalacion" Font-Bold="True" SetFocusOnError="True" ValidationGroup="SICOGUA"
                                                                Text="*">
                                                            </asp:RequiredFieldValidator>--%>
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
