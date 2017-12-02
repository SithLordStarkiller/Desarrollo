<%@ Page Title="" Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmReporteRotacion.aspx.cs" Inherits="Reportes_frmReporteRotacion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../Generales/JScript.js"></script>
    <table class="tamanio">
        <tr>
            <td class="titulo" colspan="7">
                Reporte Rotación</td>
        </tr>
         <tr>
            <td colspan="7"  class="subtitulo">
                Criterios de Búsqueda
            </td>
        </tr>
        <tr>
            <td class="der" style="width: 90px">
                &nbsp;
            </td>
            <td class="der" style="width: 90px">
                Zona:
            </td>
            <td colspan="5">
                <asp:DropDownList ID="ddlZona" runat="server" CssClass="texto">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="der" style="width: 90px">
                &nbsp;
            </td>
            <td class="der" style="width: 90px">
                Servicio:
            </td>
            <td colspan="5">
                <asp:DropDownList ID="ddlServicio" runat="server" CssClass="texto" OnSelectedIndexChanged="ddlServicio_SelectedIndexChanged"
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="der" style="width: 90px">
                &nbsp;
            </td>
            <td class="der" style="width: 90px">
                Instalacion:
            </td>
            <td colspan="5">
                <asp:DropDownList ID="ddlInstalacion" runat="server" CssClass="texto">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="der" style="width: 90px">
                &nbsp;
            </td>
            <td class="der" style="width: 90px">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td align="right">
                <asp:Button ID="btnGenerar" runat="server" Text="Generar Reporte" CssClass="boton"
                    onMouseOver="javascript:this.style.cursor='hand';" ToolTip="Generar Reporte"
                    ValidationGroup="SICOGUA" OnClick="btnGenerar_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
