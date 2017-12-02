<%@ Page Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmIncidentesConfirmacion.aspx.cs" Inherits="Incidentes_frmIncidentesConfirmacion"
    Title="Módulo de Control de Servicios ::: Incidentes - Confirmación" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../Generales/JScript.js"></script>

    <div id="Contenido" class="scrollbar">
        <asp:UpdatePanel ID="updIncidentes" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table class="tamanio">
                    <tr>
                        <td colspan="6" class="titulo">
                            Hechos Ocurridos
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="der">
                            Servicio:
                        </td>
                        <td class="izq">
                            <asp:Label ID="lblServicio" runat="server" Text="-"></asp:Label>
                        </td>
                        <%--<td class="der">
                            Instalación:
                        </td>
                        <td class="izq" colspan="3">
                            <asp:Label ID="lblInstalacion" runat="server" Text="-"></asp:Label>
                        </td>--%>
                    </tr>
                    <tr>
                    <td class="der">
                            Instalación:
                        </td>
                        <td class="izq" colspan="3">
                            <asp:Label ID="lblInstalacion" runat="server" Text="-"></asp:Label>
                        </td></tr>
                    <tr>
                        <td class="der">
                            Fecha:
                        </td>
                        <td class="izq">
                            <asp:Label ID="lblFecha" runat="server" Text="-"></asp:Label>
                        </td>
                        <td class="der">
                            Hora:
                        </td>
                        <td class="izq">
                            <asp:Label ID="lblHora" runat="server" Text="-"></asp:Label>
                        </td>
                        <td class="der">
                            Lugar:
                        </td>
                        <td class="izq">
                            <asp:Label ID="lblLugar" runat="server" Text="-"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <br />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="updPersonalInvolucrado" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table class="tamanio">
                    <tr>
                        <td colspan="4" class="subtitulo">
                            Personal Involucrado en los Hechos
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="der">
                            Grado:
                        </td>
                        <td class="izq">
                            <asp:Label ID="lblGrado" runat="server" Text="-"></asp:Label>
                        </td>
                        <td class="der">
                           <%-- Zona:--%>
                        </td>
                        <td class="izq">
                            <asp:Label ID="lblZona" runat="server" Text="-"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="der">
                            Nombre Completo:
                        </td>
                        <td class="izq">
                            <asp:Label ID="lblNomCompleto" runat="server" Text="-"></asp:Label>
                        </td>
                        <td class="der">
                            No. Empleado:
                        </td>
                        <td class="izq">
                            <asp:Label ID="lblNoEmpleado" runat="server" Text="-"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table>
            <tr>
                <td class="der">
                    ¿Que actividad realizaba?:
                </td>
                <td class="izq">
                    <asp:Label ID="lblActividad" runat="server" Text="-"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="der">
                    ¿Portaba uniforme o vestia de civil?:
                </td>
                <td class="izq">
                    <asp:Label ID="lblUniformeCivil" runat="server" Text="-"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="der">
                    Desarrollo de los hechos y consecuencias:
                </td>
                <td class="izq">
                    <asp:Label ID="lblHecCon" runat="server" Text="-"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="der">
                    Cuadro general de lesiones:
                </td>
                <td class="izq">
                    <asp:Label ID="lblCuaLes" runat="server" Text="-"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="der">
                    ¿Donde se encuentra el cadaver o lesionado? y ¿Cuál fue el medio para su evacuación?:
                </td>
                <td class="izq">
                    <asp:Label ID="lblCadLes" runat="server" Text="-"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="der">
                    Acción tomada contra el agresor:
                </td>
                <td class="izq">
                    <asp:Label ID="lblAccConAgr" runat="server" Text="-"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <br />
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="updAutoridadHechos" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table class="tamanio">
                    <tr>
                        <td colspan="4" class="subtitulo">
                            Autoridad que Tomo Nota de los Hechos
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="der">
                            Grado:
                        </td>
                        <td class="izq">
                            <asp:Label ID="lblAutGra" runat="server" Text="-"></asp:Label>
                        </td>
                        <td class="der">
                            <%--Zona:--%>
                        </td>
                        <td class="izq">
                            <asp:Label ID="lblAutZon" runat="server" Text="-"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="der">
                            Nombre Completo:
                        </td>
                        <td class="izq">
                            <asp:Label ID="lblAutNom" runat="server" Text="-"></asp:Label>
                        </td>
                        <td class="der">
                            No. Empleado:
                        </td>
                        <td class="izq">
                            <asp:Label ID="lblAutNum" runat="server" Text="-"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="der">
                            Acción del mando:
                        </td>
                        <td class="izq" colspan="3">
                            <asp:Label ID="lblAccionMando" runat="server" Text="-"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <br />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="updAutorParteInicial" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table class="tamanio">
                    <tr>
                        <td colspan="4" class="subtitulo">
                            Autor del Parte Inicial
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="der">
                            Grado:
                        </td>
                        <td class="izq">
                            <asp:Label ID="lblParIniGra" runat="server" Text="-"></asp:Label>
                        </td>
                        <td class="der">
                            <%--Zona:--%>
                        </td>
                        <td class="izq">
                            <asp:Label ID="lblParIniZon" runat="server" Text="-"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="der">
                            Nombre Completo:
                        </td>
                        <td class="izq">
                            <asp:Label ID="lblParIniNom" runat="server" Text="-"></asp:Label>
                        </td>
                        <td class="der">
                            No. Empleado:
                        </td>
                        <td class="izq">
                            <asp:Label ID="lblParIniNum" runat="server" Text="-"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <br />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="updSuperiorParteInicial" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table class="tamanio">
                    <tr>
                        <td colspan="4" class="subtitulo">
                            Superior del Autor del Parte Inicial
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="der">
                            Grado:
                        </td>
                        <td class="izq">
                            <asp:Label ID="lblSupAutGra" runat="server" Text="-"></asp:Label>
                        </td>
                        <td class="der">
                            <%--Zona:--%>
                        </td>
                        <td class="izq">
                            <asp:Label ID="lblSupAutZon" runat="server" Text="-"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="der">
                            Nombre Completo:
                        </td>
                        <td class="izq">
                            <asp:Label ID="lblSupAutNom" runat="server" Text="-"></asp:Label>
                        </td>
                        <td class="der">
                            No. Empleado:
                        </td>
                        <td class="izq">
                            <asp:Label ID="lblSupAutNum" runat="server" Text="-"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <br />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="cen" style="text-align: center">
            <table class="tamanio">
                <tr>
                    <td colspan="4" class="subtitulo">
                        En caso de accidente Aéreo o Terrestre
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td class="der">
                        Daños materiales:
                    </td>
                    <td class="izq">
                        <asp:Label ID="lblDanMat" runat="server" Text="-"></asp:Label>
                    </td>
                    <td class="der">
                        Monto:
                    </td>
                    <td class="izq">
                        <asp:Label ID="lblMonto" runat="server" Text="-"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <br />
                    </td>
                </tr>
            </table>
            <table style="margin: 0 auto 0 auto;">
                <tr>
                    <td>
                        <asp:Button ID="btnConfirmar" runat="server" CssClass="boton" Text="Confirmar" OnClick="btnConfirmar_Click"
                            onMouseOver="javascript:this.style.cursor='hand';" />
                    </td>
                    <td>
                        <asp:Button ID="btnRegresar" runat="server" CssClass="boton" Text="Regresar" onMouseOver="javascript:this.style.cursor='hand';"
                            PostBackUrl="~/Incidentes/frmIncidentes.aspx" />
                    </td>
                    <td>
                        <asp:Button ID="btnCancelar" runat="server" CssClass="boton" Text="Cancelar" OnClick="btnCancelar_Click"
                            onMouseOver="javascript:this.style.cursor='hand';" />
                    </td>
                </tr>
            </table>
            <br />
            <br />
        </div>
    </div>
</asp:Content>
