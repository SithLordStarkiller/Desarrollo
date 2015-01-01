<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdministracionUsuarios.aspx.cs" Inherits="Aplicacion.Sitio.TI.AdministracionUsuarios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
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
                    <tr>
                        <asp:ListBox runat="server" ID="lbx"/>
                    </tr>
                </table>
            </fieldset>
        </div>
    </form>
</body>
</html>
