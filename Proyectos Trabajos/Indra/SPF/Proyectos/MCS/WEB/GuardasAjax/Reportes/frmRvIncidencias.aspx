
<%@ Page Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmRvIncidencias.aspx.cs" Inherits="Reportes_frmRvIncidencias" Title="Reporte Incidencias" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="tamanio">
        <tr>
            <td class="titulo">
                Reporte Incidencias
            </td>
        </tr>
    </table>
    <br />
    <asp:Button ID="btnRegresar" runat="server" Text="Regresar" class="boton" onMouseOver="javascript:this.style.cursor='hand';"
        ToolTip="Regresar" OnClick="btnRegresar_Click" />
    <br />
    <br />
    <div style="border-color: Black; border-style: solid; border-width: 2px; background-color: #FFFFFF;">
        <rsweb:ReportViewer ID="rvReporte" runat="server" Height="468px" Width="100%" Font-Names="Verdana"
            Font-Size="8pt" ProcessingMode="Remote">
        </rsweb:ReportViewer>
    </div>
</asp:Content>
