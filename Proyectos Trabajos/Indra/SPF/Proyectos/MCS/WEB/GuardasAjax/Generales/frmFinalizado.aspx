<%@ Page Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmFinalizado.aspx.cs" Inherits="Generales_frmFinalizado" Title="Finalizado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <script type="text/jscript">
    function ini() {
        var window_width = 400;
        var window_height = 150;
        var newfeatures = 'scrollbars=no,resizable=no';
        var window_top = (screen.height - window_height) / 2;
        var window_left = (screen.width - window_width) / 2;
        newWindow = window.open('../frmImpresion.aspx?rep=6', 'titulo', 'width=' + window_width + ',height=' + window_height + ',top=' + window_top + ',left=' + window_left + ',features=' + newfeatures + '');

    }
    </script>
    <table class="tamanio">
        <tr>
            <td class="titulo" colspan="4">
                Finalizado
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="4" class="cen">
                <asp:Image ID="imgIcon" runat="server" Width="10%" />
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center">
              <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                <asp:Button ID="Button1" runat="server" Text="Generar Reporte" 
                    onclick="Button1_Click" CssClass="boton cen" />
                </ContentTemplate>
                </asp:UpdatePanel>
                <br />
            </td>
        </tr>
        <tr>
            <td class="cen" valign="middle">
                <div style="border-style: outset; border-width: 1px; vertical-align: middle;">
                    <br />
                    <div style="font-variant: small-caps;">
                        <asp:Label ID="lblMensaje" runat="server" Text="" Font-Bold="True" Font-Names="Verdana"
                            Font-Size="10pt"></asp:Label>
                    </div>
                    <br />
                    <asp:LinkButton ID="lnkContinuar" runat="server" Font-Bold="true" 
                        onclick="lnkContinuar_Click">Continuar...</asp:LinkButton>
                    <br />
                    <br />
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <br />
            </td>
        </tr>
    </table>
</asp:Content>
