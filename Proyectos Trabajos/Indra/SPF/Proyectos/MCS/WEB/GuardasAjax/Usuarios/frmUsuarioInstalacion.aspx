<%@ Page Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmUsuarioInstalacion.aspx.cs" Inherits="Usuarios_frmUsuarioInstalacion"
    Title="Módulo de Control de Servicios ::: Usuarios/Servicio/Instalacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../Generales/JScript.js"></script>
    <div id="Contenido" class="scrollbar">
        <table class="tamanio">
            <tr>
                <td colspan="6" class="titulo">
                    Servicio/Instalación
                </td>
            </tr>
            <tr>
                <td colspan="6" class="subtitulo">
                    Asignación de Servicio Instalación
                    <asp:HiddenField ID="hfIdUsuario" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>            
            <tr>
                <td colspan="6" runat="server" id="trDatos" visible="true">
                    <table width="100%">
                        <tr>
                            <td colspan="6">
                                <table style="width: 90%;">
                                    <tr>
                                        <td class="der">
                                            Apellido Paterno:
                                        </td>
                                        <td class="izq">
                                            <asp:Label ID="lblPaterno" runat="server" Text="-"></asp:Label>
                                        </td>
                                        <td class="der">
                                            Apellido Materno:
                                        </td>
                                        <td class="izq">
                                            <asp:Label ID="lblMaterno" runat="server" Text="-"></asp:Label>
                                        </td>
                                        <td class="der">
                                            Nombre:
                                        </td>
                                        <td class="izq">
                                            <asp:Label ID="lblNombre" runat="server" Text="-"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="der">
                                        </td>
                                        <td class="izq" colspan="5">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <table style="margin: 0 auto 0 auto; width: 100%;">
                                    <tr>
                                        <td class="der">
                                            Zona:
                                        </td>
                                        <td class="izq">
                                            <asp:DropDownList runat="server" ID="lstZona" CssClass="texto" AutoPostBack="True"
                                                OnSelectedIndexChanged="lstZona_SelectedIndexChanged" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="der" runat="server">
                                            Servicio:
                                        </td>
                                        <td class="izq" runat="server">
                                            <asp:DropDownList runat="server" ID="lstServicio" CssClass="texto" AutoPostBack="True"
                                                OnSelectedIndexChanged="lstServicio_SelectedIndexChanged" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="Td1" class="der" runat="server">
                                            Instalación:
                                        </td>
                                        <td id="Td2" class="izq" runat="server">
                                            <asp:DropDownList runat="server" ID="lstInstalacion" CssClass="texto" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <table style="margin: 0 auto 0 auto;">
                                    <tr>
                                        <td class="izq">
                                            Permisos:
                                        </td>
                                        <td class="izq" colspan="10">
                                            <asp:CheckBoxList runat="server" ID="chklstPermisos" CssClass="centro" RepeatDirection="Vertical">
                                                <asp:ListItem Text="¿Puede Consultar?" />
                                                <asp:ListItem Text="¿Puede Asignar?" />
                                                <asp:ListItem Text="¿Es Vigente?" />
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <br />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <table style="margin: 0 auto 0 auto;">
                        <tr>
                            <td>
                                <asp:Button ID="btnNuevo" runat="server" Text="Cerrar" CssClass="boton" onMouseOver="javascript:this.style.cursor='hand';"
                                    ToolTip="Cerrar ventana" OnClick="btnNuevo_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnGuardar" runat="server" Text="Guardar Servicio/Instalación" CssClass="boton"
                                    ValidationGroup="SICOGUA" onMouseOver="javascript:this.style.cursor='hand';"
                                    ToolTip="Guardar Servicio Instalación" OnClick="btnGuardar_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
