<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.Master" AutoEventWireup="true" CodeBehind="FormularioXml.aspx.cs" Inherits="PubliPayments.FormularioXml" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">

        function getCheckedRadio(theRadio) {
            var valor = theRadio.value;
            if (valor == 3) {
                campoDesc.hidden = true;
            } else {
                campoDesc.hidden = false;
            }
        }
    </script>
    <div style="margin-left: 30px; width: 80%; position: relative" align="center">
        <div class="TituloSeleccion" style="min-height: 42px; display: inline-block; width: 100%; vertical-align: middle; margin-top: 15px;">
            <div style="max-width: 600px; float: left; width: 340px">Carga Archivo Formulario XML</div>
            <asp:Image ID="Image1" runat="server" AlternateText="|" ImageUrl="~/imagenes/separador.png" Style="margin-left: 30px; margin-right: 30px; float: left;" />
        </div>
        <div id="ElementosCargaXml" style="margin-left: 30px">
            <div style="height: 330px">
                <table id="tbCargaXml" style="padding-right: 0; width: 50%">
                    <tr style="height: 60px">
                        <td style="width: 200px" class="TextoFormulario">Captura</td>
                        <td>
                            <div>
                                <asp:RadioButtonList ID="TipoCaptura" runat="server" RepeatDirection="Horizontal" Width="250px">
                                    <asp:ListItem Value="1" Selected="True" onclick="getCheckedRadio(this)">Móvil</asp:ListItem>
                                    <asp:ListItem Value="2" onclick="getCheckedRadio(this)">Web</asp:ListItem>
                                    <asp:ListItem Value="3" onclick="getCheckedRadio(this)">Carga Xml</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </td>
                    </tr>
                    <tr style="height: 60px" id="campoDesc">
                        <td style="width: 200px" class="TextoFormulario">Descripción</td>
                        <td>
                            <asp:TextBox runat="server" ID="Descripcion" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="height: 60px">
                        <td style="width: 200px" class="TextoFormulario">
                            <div>Ruta</div>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="Ruta" Width="250px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="height: 60px">
                        <td style="width: 200px" class="TextoFormulario">Carga Archivo</td>
                        <td>
                            <asp:FileUpload ID="FileUploadControl" runat="server" />
                            <asp:Button runat="server" ID="Button1" OnClick="BtnUploadClick" Text="Upload" />
                        </td>
                    </tr>
                    <tr class="AsignarA" style="height: 60px">
                        <td style="width: 200px" class="TextoFormulario">Estatus carga</td>
                        <td>
                            <asp:Label runat="server" ID="StatusLabel" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>

