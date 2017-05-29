<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="wsBroxel.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="Label">Inicio</asp:Label>
        <asp:TextBox ID="tbxInicio" runat="server"></asp:TextBox>
        <br/>
        <br/>
        <asp:Label ID="Label2" runat="server" Text="Label">Fin</asp:Label>
        <asp:TextBox ID="tbxFin" runat="server"></asp:TextBox>
        <br/>
        <br/>
        <asp:Button ID="Button1" runat="server" Text="Button" />
    </div>
    </form>
</body>
</html>
