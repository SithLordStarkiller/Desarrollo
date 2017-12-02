<%@ Page Title="" Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true" CodeFile="frmTurnos.aspx.cs" Inherits="Reportes_frmTurnos" %>

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
                                        Reporte de Turnos Cumplidos</td>
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
                                                            Año:</td>
                                                        <td class="izq">
                                                            <asp:DropDownList ID="ddlAnio" runat="server" CssClass="texto">
                                                                <asp:ListItem Value="">SELECCIONAR</asp:ListItem>
                                                                <asp:ListItem Value="13">2013</asp:ListItem>
                                                                <asp:ListItem Value="14">2014</asp:ListItem>
                                                                <asp:ListItem Value="15">2015</asp:ListItem>
                                                                <asp:ListItem Value="16">2016</asp:ListItem>
                                                                <asp:ListItem Value="17">2017</asp:ListItem>
                                                            </asp:DropDownList>
                                                             <asp:RequiredFieldValidator ID="rfvAnio" runat="server" ErrorMessage="Se requiere el año del reporte."
                                                                Font-Bold="True" ControlToValidate="ddlAnio" SetFocusOnError="True" ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>
                                                           
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der">
                                                            Mes:</td>
                                                        <td class="izq">
                                                            <asp:DropDownList ID="ddlMes" runat="server" CssClass="texto">
                                                                <asp:ListItem Value="">SELECCIONAR</asp:ListItem>
                                                                <asp:ListItem Value="1">ENERO</asp:ListItem>
                                                                <asp:ListItem Value="2">FEBRERO</asp:ListItem>
                                                                <asp:ListItem Value="3">MARZO</asp:ListItem>
                                                                <asp:ListItem Value="4">ABRIL</asp:ListItem>
                                                                <asp:ListItem Value="5">MAYO</asp:ListItem>
                                                                <asp:ListItem Value="6">JUNIO</asp:ListItem>
                                                                <asp:ListItem Value="7">JULIO</asp:ListItem>
                                                                <asp:ListItem Value="8">AGOSTO</asp:ListItem>
                                                                <asp:ListItem Value="9">SEPTIEMBRE</asp:ListItem>
                                                                <asp:ListItem Value="10">OCTUBRE</asp:ListItem>
                                                                <asp:ListItem Value="11">NOVIEMBRE</asp:ListItem>
                                                                <asp:ListItem Value="12">DICIEMBRE</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="ddlMesValida" runat="server" 
                                                                ControlToValidate="ddlMes" ErrorMessage="Se requiere el mes del reporte." 
                                                                Font-Bold="True" SetFocusOnError="True" ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>
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
                                                        <td class="izq">
                                                            <asp:DropDownList ID="ddlInstalacion" runat="server" CssClass="texto" Enabled="False">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvRecibe" runat="server" ErrorMessage="Debe Seleccionar una Instalación."
                                                                ControlToValidate="ddlInstalacion" Font-Bold="True" SetFocusOnError="True" ValidationGroup="SICOGUA"
                                                                Text="*">
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der">
                                                            Por Parte del SPF:</td>
                                                        <td class="izq">
                                                            <asp:TextBox ID="txbSPF" runat="server" CssClass="texto" Height="69px" 
                                                                TextMode="MultiLine" Width="476px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der">
                                                            Por PArte del Contratante:</td>
                                                        <td class="izq">
                                                            <asp:TextBox ID="txbContratante" runat="server" CssClass="texto" Height="65px" 
                                                                TextMode="MultiLine" Width="473px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der">
                                                            Observaciones:</td>
                                                        <td class="izq">
                                                            <asp:TextBox ID="txbObservaciones" runat="server" CssClass="texto" 
                                                                Height="176px" TextMode="MultiLine" Width="714px"></asp:TextBox>
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



