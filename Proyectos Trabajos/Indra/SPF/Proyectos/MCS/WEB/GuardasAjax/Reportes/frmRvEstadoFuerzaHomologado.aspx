<%@ Page Title="" Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true" CodeFile="frmRvEstadoFuerzaHomologado.aspx.cs" Inherits="Reportes_frmRvEstadoFuerzaHomologado" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updLogon" runat="server" UpdateMode="Always">
                                            <ContentTemplate>
    <table class="tamanio">
        <tr>
            <td class="titulo" style="height: 20px">
            
            </td>
        </tr>
    </table>
    <br />
    <asp:Button ID="btnRegresar" runat="server" Text="Regresar" class="boton" onMouseOver="javascript:this.style.cursor='hand';"
        ToolTip="Regresar" OnClick="btnRegresar_Click" />
    <br />
    <br />
    <div style="background-color: #FFFFFF;">
        <rsweb:ReportViewer ID="rvReporte" runat="server" Height="468px" Width="100%" Font-Names="Verdana"
            Font-Size="8pt" ProcessingMode="Remote">
        </rsweb:ReportViewer>
    </div>
    </ContentTemplate></asp:UpdatePanel>
</asp:Content>