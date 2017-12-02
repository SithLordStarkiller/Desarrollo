<%@ Page Title="" Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true" CodeFile="frmPersonalAsignadoHistorico.aspx.cs" Inherits="Reportes_frmPersonalAsignadoHistorico" %>

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
                                        Reporte de Programas Sustantivos
                                    </td>
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
                                                            &nbsp;</td>
                                                        <td class="izq" style="width: 517px">
                                                            <%--<asp:RequiredFieldValidator ID="rfvServicio" runat="server" ErrorMessage="Debe Seleccionar un Servicio."
                                                                ControlToValidate="ddlServicio" Font-Bold="True" SetFocusOnError="True" ValidationGroup="SICOGUA"
                                                                Text="*">
                                                            </asp:RequiredFieldValidator>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der">
                                                            Año:
                                                        </td>
                                                        <td class="izq" style="width: 517px">
                                                            <asp:DropDownList ID="ddlAnio" runat="server" CssClass="texto" 
                                                                AutoPostBack="True" onselectedindexchanged="ddlAnio_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">SELECCIONAR</asp:ListItem>
                                                                <asp:ListItem>2013</asp:ListItem>
                                                                <asp:ListItem>2014</asp:ListItem>
                                                                <asp:ListItem>2015</asp:ListItem>
                                                                <asp:ListItem>2016</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%--<asp:RequiredFieldValidator ID="rfvRecibe" runat="server" ErrorMessage="Debe Seleccionar una Instalación."
                                                                ControlToValidate="ddlInstalacion" Font-Bold="True" SetFocusOnError="True" ValidationGroup="SICOGUA"
                                                                Text="*">
                                                            </asp:RequiredFieldValidator>--%>
                                                            <asp:Label ID="lblAnnio" runat="server" CssClass="divError"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der">
                                                            Mes:</td>
                                                        <td class="izq" style="width: 517px">
                                                            <asp:DropDownList ID="ddlMes" runat="server" AutoPostBack="True" 
                                                                CssClass="texto" onselectedindexchanged="ddlMes_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">SELECCIONAR</asp:ListItem>
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
                                                            <asp:Label ID="lblMes" runat="server" CssClass="divError"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der">
                                                            &nbsp;</td>
                                                        <td class="izq" style="width: 517px">
                                                            <asp:Label ID="lblFechaCorte" runat="server" CssClass="negritas"></asp:Label>
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
                                    <td class="der" style="height: 23px">
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


