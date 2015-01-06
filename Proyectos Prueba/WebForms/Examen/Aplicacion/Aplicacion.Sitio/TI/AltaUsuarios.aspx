<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="AltaUsuarios.aspx.cs" Inherits="Aplicacion.Sitio.TI.AltaUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <div id="Contenido">
        <br />
        <h2 style="color: silver; text-align: center;">Alta de usuarios
        </h2>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            </asp:UpdatePanel>
            <table>
                <tr>
                    <td>Nombre</td>
                    <td>
                        <asp:TextBox runat="server" ID="txbNombre"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Apellido Paterno</td>
                    <td>
                        <asp:TextBox runat="server" ID="txbAPaterno"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Apellido Materno
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txbAMaterno"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Correo Electronico</td>
                    <td>
                        <asp:TextBox runat="server" ID="txbCorreoElectronico"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Contraseña
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txbContrasena"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Confirma Contraseña
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txbContrasenaConfimada"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Area de Negocio
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAreaNegocio" runat="server" Width="100%"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td colspan="2">
                        <asp:Button runat="server" ID="btnRegistrar" Text="Registrar" OnClick="btnRegistrar_OnClick" />
                    </td>
                </tr>
            </table>
<%--        </asp:UpdatePanel>--%>
    </div>
</asp:Content>
