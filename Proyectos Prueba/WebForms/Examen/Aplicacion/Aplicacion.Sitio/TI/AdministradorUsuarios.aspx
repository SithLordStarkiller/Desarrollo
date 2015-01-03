<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio.Master" AutoEventWireup="true" CodeBehind="AdministradorUsuarios.aspx.cs" Inherits="Aplicacion.Sitio.TI.AdministradorUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
        <div>
            <fieldset>
                <table>
                    <tr>
                        <td>Nombre</td>
                        <td>
                            <asp:TextBox runat="server" ID="txbNonbre"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Apellido Paterno</td>
                        <td>
                            <asp:TextBox runat="server" ID="txbAPaterno"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Apellido materno
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txbAMaterno"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>CorreoElectronico</td>
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
                </table>
            </fieldset>
        </div>
</asp:Content>