<%@ Page Title="" Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmReporteAntiguedad.aspx.cs" Inherits="Reportes_frmReporteAntiguedad" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../Generales/JScript.js"></script>
    <asp:Panel ID="panReporteListado" runat="server">
        <div id="Contenido" class="scrollbar">
            <div>
                <table class="tamanio">
                    <tr runat="server" id="trParametros">
                        <td>
                            <table class="tamanio">
                                <tr>
                                    <td>
                                        <div id="divError" runat="server" class="divError" visible="false">
                                            <table>
                                                <tr>
                                                    <td width="835">
                                                        <asp:Label ID="lblerror" runat="server" Width="845px"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="titulo">
                                        Reporte Días Asignación
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
                                                            Zona:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:DropDownList ID="ddlZona" runat="server" CssClass="texto" 
                                                                AutoPostBack="True" onselectedindexchanged="ddlZona_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der">
                                                            Servicio:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:DropDownList ID="ddlServicio" runat="server" AutoPostBack="True" CssClass="texto"
                                                                OnSelectedIndexChanged="ddlServicio_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der">
                                                            Instalación:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:DropDownList ID="ddlInstalacion" runat="server" CssClass="texto" Enabled="False">
                                                            </asp:DropDownList>
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
