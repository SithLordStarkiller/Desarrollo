<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WebFormsAjaxControlToolkit.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style>
        .CajaDialogo {
            background-color: #99ffcc;
            border-width: 4px;
            border-style: outset;
            border-color: Yellow;
            padding: 0px;
            width: 200px;
            font-weight: bold;
            font-style: italic;
        }

            .CajaDialogo div {
                margin: 5px;
                text-align: center;
            }

        .FondoAplicacion {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.2;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function mpeSeleccionOnOk() {
            var ddlCiudades = document.getElementById("ddlCiudades");
            var ddlMeses = document.getElementById("ddlMeses");
            var ddlAnualidades = document.getElementById("ddlAnualidades");
            var txtSituacion = document.getElementById("txtSituacion");

            txtSituacion.value = ddlCiudades.options[ddlCiudades.selectedIndex].value + ", " +
                ddlMeses.options[ddlMeses.selectedIndex].value + " de " +
                ddlAnualidades.options[ddlAnualidades.selectedIndex].value;
        }
        function mpeSeleccionOnCancel() {
            var txtSituacion = document.getElementById("txtSituacion");
            txtSituacion.value = "";
            txtSituacion.style.backgroundColor = "#FFFF99";
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>

        <asp:Button ID="Button1" runat="server" Text="Button" />

        <asp:TextBox ID="txtDate" runat="server" ReadOnly="true"></asp:TextBox>
        <asp:ImageButton ID="imgPopup" ImageUrl="imagenes/Calendar (1).png" ImageAlign="Bottom"
            runat="server" />
        <ajaxtoolkit:CalendarExtender ID="Calendar1" PopupButtonID="imgPopup" runat="server" TargetControlID="txtDate"
            Format="dd/MM/yyyy"></ajaxtoolkit:CalendarExtender>

        <div>
            <table border="2">
                <tr>
                    <td colspan="2" align="center">
                        <asp:Label ID="Label1" runat="server" Text="Datos de solicitud" Font-Bold="True"
                            Font-Underline="True" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Nombre:" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtNombre" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="e-mail:" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="Situación:" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtSituacion" runat="server" Width="300" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnAsistente" runat="server" Text="Asistente de selección" Width="150" />
                    </td>
                    <td>
                        <asp:Button ID="btnGrabar" runat="server" Text="Grabar datos" Width="125" />
                    </td>
                </tr>
            </table>
        </div>

        <asp:Panel ID="pnlSeleccionarDatos" runat="server" CssClass="CajaDialogo">
            <div style="padding: 10px; background-color: #0033CC; color: #FFFFFF;">
                <asp:Label ID="Label4" runat="server" Text="Seleccionar datos" />
            </div>
            <div>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Ciudad:" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCiudades" runat="server">
                                <asp:ListItem>Zamora</asp:ListItem>
                                <asp:ListItem>Teruel</asp:ListItem>
                                <asp:ListItem>Salamanca</asp:ListItem>
                                <asp:ListItem>Sevilla</asp:ListItem>
                                <asp:ListItem>Lugo</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="Mes:" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMeses" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label8" runat="server" Text="Año:" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAnualidades" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" />
                &nbsp;&nbsp;
        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" />
            </div>
        </asp:Panel>
        <ajaxtoolkit:ModalPopupExtender ID="mpeSeleccion" runat="server"
            TargetControlID="btnAsistente"
            PopupControlID="pnlSeleccionarDatos"
            OkControlID="btnAceptar"
            CancelControlID="btnCancelar"
            OnOkScript="mpeSeleccionOnOk()"
            OnCancelScript="mpeSeleccionOnCancel()"
            DropShadow="True"
            BackgroundCssClass="FondoAplicacion" />
    </form>
</body>
</html>
