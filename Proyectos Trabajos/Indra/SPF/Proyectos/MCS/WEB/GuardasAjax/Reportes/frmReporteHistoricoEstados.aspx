<%@ Page Title="" Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true" CodeFile="frmReporteHistoricoEstados.aspx.cs" Inherits="Reportes_frmReporteHistoricoEstados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="tamanio">
        <tr>
            <td class="titulo">
                Histórico Mensual Por Estado</td>
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
                                    Mes:
                                </td>
                                <td class="izq">
                                    <asp:DropDownList ID="ddlMes" runat="server" AutoPostBack="True" 
                                        CssClass="texto" >
                                        <asp:ListItem Value=""></asp:ListItem>
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
                                    <asp:RequiredFieldValidator ID="rfvServicio" runat="server" ErrorMessage="Debe Seleccionar un Mes."
                                                                ControlToValidate="ddlMes" Font-Bold="True" SetFocusOnError="True" ValidationGroup="SICOGUA"
                                                                Text="*">
                                                            </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="der">
                                    Año:
                                </td>
                                <td class="izq">
                                    <asp:DropDownList ID="ddlAnio" runat="server" CssClass="texto" >
                                        <asp:ListItem Value=""></asp:ListItem>
                                        <asp:ListItem>2012</asp:ListItem>
                                        <asp:ListItem>2013</asp:ListItem>
                                        <asp:ListItem>2014</asp:ListItem>
                                        <asp:ListItem>2015</asp:ListItem>
                                        <asp:ListItem>2016</asp:ListItem>
                                        <asp:ListItem>2017</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvRecibe" runat="server" ErrorMessage="Debe Seleccionar un Año."
                                                                ControlToValidate="ddlAnio" Font-Bold="True" SetFocusOnError="True" ValidationGroup="SICOGUA"
                                                                Text="*">
                                                            </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                *Primer reporte JULIO 2012<br />
            </td>
        </tr>
        <tr>
            <td class="der">
                <asp:Button ID="btnGenerar" runat="server" CssClass="boton" 
                    OnClick="btnGenerar_Click" onMouseOver="javascript:this.style.cursor='hand';" 
                    Text="Generar Reporte" ToolTip="Generar Reporte" ValidationGroup="SICOGUA" />
            </td>
        </tr>
        <tr>
            <td>
                <br />
            </td>
        </tr>
    </table>
</asp:Content>

