<%@ Page Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmDatosPersonales.aspx.cs" Inherits="Personal_frmDatosGenerales" Title="Módulo de Control de Servicios ::: Datos Personales" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../Generales/JScript.js"></script>

    <div id="Contenido" class="scrollbar">
        <table class="tamanio">
            <tr>
                <td class="titulo" colspan="5">
                    Datos Personales
                </td>
            </tr>
            <tr>
                <td rowspan="12" style="width: 166px">
                    <img id="imgFoto" alt="" src="ghFotografia.ashx" style="height: 191px; width: 161px" /></td>
                <td class="subtitulo" colspan="4">
                    Personales
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <table style="margin: 0 auto 0 auto; width: 100%;">
                        <tr>
                            <td class="der">
                                Apellido Paterno:
                            </td>
                            <td class="izq">
                                <asp:Label ID="lblApellidoPaterno" runat="server" Text="-">
                                </asp:Label>
                            </td>
                            <td class="der">
                                Apellido Materno:
                            </td>
                            <td class="izq">
                                <asp:Label ID="lblApellidoMaterno" runat="server" Text="-">
                                </asp:Label>
                            </td>
                            <td class="der">
                                Nombre:
                            </td>
                            <td class="izq">
                                <asp:Label ID="lblNombre" runat="server" Text="-">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="der">
                                Tipo de Sangre:
                            </td>
                            <td class="izq">
                                <asp:Label ID="lblTipoSangre" runat="server" Text="-">
                                </asp:Label>
                            </td>
                            <td class="der">
                                CURP:
                            </td>
                            <td class="izq">
                                <asp:Label ID="lblCURP" runat="server" Text="-">
                                </asp:Label>
                            </td>
                            <td class="der">
                                RFC:
                            </td>
                            <td class="izq">
                                <asp:Label ID="lblRFC" runat="server" Text="-">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="der">
                                Sexo:
                            </td>
                            <td>
                                <asp:Label ID="lblSexo" runat="server" Text="-">
                                </asp:Label>
                            </td>
                            <td class="der">
                                N° de Empleado:
                            </td>
                            <td>
                                <asp:Label ID="lblNumero" runat="server" Text="-">
                                </asp:Label>
                            </td>
                            <td class="der">
                                CUIP:
                            </td>
                            <td>
                                <asp:Label ID="lblCUIP" runat="server" Text="-">
                                </asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td class="der">
                                No Cartilla:</td>
                            <td>
                                <asp:Label ID="lblNoCartilla" runat="server" Text="-"></asp:Label>
                            </td>
                            <td class="der">
                                LOC:</td>
                            <td>
                                <asp:Label ID="lblLOC" runat="server" Text="-"></asp:Label>
                            </td>
                            <td class="der">
                                Curso Básico:</td>
                            <td>
                                <asp:Label ID="lblCursoBasico" runat="server" Text="-"></asp:Label>
                            </td>
                        </tr>

                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                </td>
            </tr>
            <tr>
                <td class="subtitulo" colspan="4">
                    Datos de Nacimiento
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                </td>
            </tr>
            <tr>
                <%--<td class="der">
                    Pais:
                </td>
                <td>
                    <asp:Label ID="lblPais" runat="server" Text="-"></asp:Label>
                </td>
                <td class="der">
                    Estado:
                </td>
                <td>
                    <asp:Label ID="lblEstado" runat="server" Text="-"></asp:Label>
                </td>
                <td class="der">
                    Municipio:
                </td>
                <td>
                    <asp:Label ID="lblMunicipio" runat="server" Text="-"></asp:Label>
                </td>--%>
            </tr>
            <tr>
                <td class="der">
                    Fecha de Nacimiento:
                </td>
                <td colspan="3">
                    <asp:Label ID="lblFechaNacimiento" runat="server" Text="-"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                </td>
            </tr>
            <tr>
                <td class="subtitulo" colspan="4">
                    Ingreso a SPF
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                </td>
            </tr>
            <tr>
                <td class="der">
                    Fecha de Alta:
                </td>
                <td class="izq">
                    <asp:Label ID="lblFechaAlta" runat="server" Text="-"></asp:Label>
                </td>
                <td class="der">
                    Fecha de Baja
                </td>
                <td class="izq">
                    <asp:Label ID="lblFechaBaja" runat="server" Text="-"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                </td>
            </tr>
        </table>
        <table class="tamanio">
            <tr>
                <td colspan="6" class="cen">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar Datos Personales" CssClass="boton"
                        TabIndex="16" OnClick="btnBuscar_Click" onMouseOver="javascript:this.style.cursor='hand';"
                        ToolTip="Buscar Datos Personales" />
                </td>
            </tr>
        </table>
        <br />
    </div>
</asp:Content>
