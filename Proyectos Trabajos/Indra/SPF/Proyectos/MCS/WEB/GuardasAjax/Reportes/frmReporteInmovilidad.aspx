<%@ Page Title="" Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmReporteInmovilidad.aspx.cs" Inherits="Reportes_frmReporteInmovilidad" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../Generales/JScript.js"></script>
    <table class="tamanio">
        <tr>
            <td colspan="7" class="titulo">
                Reporte Inmovilidad
            </td>
        </tr>
        <tr>
            <td colspan="7"  class="subtitulo">
                Criterios de Búsqueda
            </td>
        </tr>
        <tr>
            <td class="der" style="width: 90px">
                &nbsp;
            </td>
            <td class="der" style="width: 90px">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:RadioButtonList ID="rblTipo" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1" Selected="True">Activos</asp:ListItem>
                    <asp:ListItem Value="0">Inactivos</asp:ListItem>
                    <asp:ListItem Value="3">Todos</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="der" colspan="2">
                &nbsp; Apellido Paterno:
            </td>
            <td>
                <asp:TextBox ID="txbPaterno" runat="server" MaxLength="30" onblur="javascript:onLosFocus(this)"
                    onfocus="javascript:onFocus(this)" onkeypress="return validar(event)" onchange="this.value=quitaacentos(this.value)"
                    onKeyDown="if(event.keyCode==13){event.keyCode=9;}" CssClass="textbox" Width="150px"></asp:TextBox>
            </td>
            <td class="der">
                Apellido Materno:
            </td>
            <td>
                <asp:TextBox ID="txbMaterno" runat="server" MaxLength="30" onblur="javascript:onLosFocus(this)"
                    onfocus="javascript:onFocus(this)" onkeypress="return validar(event)" onchange="this.value=quitaacentos(this.value)"
                    onKeyDown="if(event.keyCode==13){event.keyCode=9;}" CssClass="textbox" Width="150px"></asp:TextBox>
            </td>
            <td class="der">
                Nombre:
            </td>
            <td>
                <asp:TextBox ID="txbNombre" runat="server" MaxLength="30" onblur="javascript:onLosFocus(this)"
                    onfocus="javascript:onFocus(this)" onkeypress="return validar(event)" onchange="this.value=quitaacentos(this.value)"
                    onKeyDown="if(event.keyCode==13){event.keyCode=9;}" CssClass="textbox" Width="150px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="der" style="width: 90px">
                &nbsp;
            </td>
            <td class="der" style="width: 90px">
                No. Empleado:
            </td>
            <td>
                <asp:TextBox ID="txbNumero" runat="server" onkeypress="return nvalidar(event)" MaxLength="8"
                    CssClass="textbox" onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)"
                    onKeyDown="if(event.keyCode==13){event.keyCode=9;}" Width="150px">
                </asp:TextBox>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="der" style="width: 90px">
                &nbsp;
            </td>
            <td class="der" style="width: 90px">
                Zona:
            </td>
            <td colspan="5">
                <asp:DropDownList ID="ddlZona" runat="server" CssClass="texto">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="der" style="width: 90px">
                &nbsp;
            </td>
            <td class="der" style="width: 90px">
                Servicio:
            </td>
            <td colspan="5">
                <asp:DropDownList ID="ddlServicio" runat="server" CssClass="texto" OnSelectedIndexChanged="ddlServicio_SelectedIndexChanged"
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="der" style="width: 90px">
                &nbsp;
            </td>
            <td class="der" style="width: 90px">
                Instalacion:
            </td>
            <td colspan="5">
                <asp:DropDownList ID="ddlInstalacion" runat="server" CssClass="texto">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="der" style="width: 90px">
                &nbsp;
            </td>
            <td class="der" style="width: 90px">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Button ID="btnGenerar" runat="server" Text="Generar Reporte" CssClass="boton"
                    onMouseOver="javascript:this.style.cursor='hand';" ToolTip="Generar Reporte"
                    ValidationGroup="SICOGUA" OnClick="btnGenerar_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
