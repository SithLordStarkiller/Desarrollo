<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Aplicacion.Sitio.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="align-content: center; background-color: white;">
    <form id="form1" runat="server">
        <div style="align-content: center; width: 50%; margin: auto;">
            <asp:ScriptManager runat="server" ID="UpdatePanel1"/>
            <br/>
            <br/>
            <fieldset style="background-color: royalblue; text-align: center; margin: auto;">
                <table>
                    <tr>
                        <td colspan=2 style="text-align:center;"><h3>Login</h3></td>
                    </tr>
                    <tr>
                        <td>Correo Elecronico</td>
                        <td>
                            <asp:TextBox runat="server" ID="txbCorreoElectronico" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            contraseña
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txbContrasena" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblErrorLogin" Text="Error en Login" Visible="False"></asp:Label>
                        </td>
                        <td>
                            <asp:Button runat="server" ID="btnLogin" OnClick="btnLogin_OnClick" Text="Login" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
    </form>
</body>
</html>
