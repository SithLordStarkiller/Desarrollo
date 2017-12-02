<%@ Page Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmIncidentes.aspx.cs" Inherits="Incidentes_frmIncidentes" Title="Módulo de Control de Servicios ::: Incidentes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
                            <asp:HiddenField ID="hfIdIncidente" runat="server" Value="0" />
                        </td>
                    </tr>
                    <tr>
                        <td class="der">
                            Servicio:
                        </td>
                        <td colspan="5" class="izq">
                            <asp:DropDownList ID="ddlServicio" runat="server" CssClass="texto" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlServicio_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvServicio" runat="server" ControlToValidate="ddlServicio"
                                CssClass="Validator" ErrorMessage="Se requiere el Servicio." SetFocusOnError="True"
                                ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="der">
                            Instalación:
                        </td>
                        <td class="izq" colspan="5">
                            <asp:DropDownList ID="ddlInstalacion" runat="server" CssClass="texto" Enabled="False"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlInstalacion_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvInstalacion" runat="server" ControlToValidate="ddlInstalacion"
                                CssClass="Validator" ErrorMessage="Se requiere la Instalación." SetFocusOnError="True"
                                ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="der">
                            Fecha:
                        </td>
                        <td class="izq">
                            <asp:TextBox ID="txbFecha" runat="server" onFocus="javascript:onFocus(this)" onblur="javascript:onLosFocus(this)"
                                CssClass="textbox" Width="81px" onkeypress="return validarNoEscritura(event);"></asp:TextBox>
                            <asp:ImageButton ID="imbFecha" runat="server" ImageUrl="~/Imagenes/Calendar.png"
                                Height="18px" Width="18px" />
                            <asp:RequiredFieldValidator ID="rfvFecha" runat="server" ErrorMessage="Se Requiere la Fecha."
                                Font-Bold="True" ControlToValidate="txbFecha" SetFocusOnError="True" ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="covFecha" runat="server" ErrorMessage="La Fecha No Puede Ser Mayor al Día de Hoy."
                                ControlToValidate="txbFecha" Font-Bold="True" SetFocusOnError="True" Type="Date"
                                ValidationGroup="SICOGUA" ValueToCompare="01/01/0001" Operator="LessThanEqual">*</asp:CompareValidator>
                            <cc1:CalendarExtender ID="calFecha" runat="server" PopupButtonID="imbFecha" TargetControlID="txbFecha">
                            </cc1:CalendarExtender>
                        </td>
                        <td class="der">
                            Hora:
                        </td>
                        <td class="izq">
                            <asp:TextBox ID="txbHora" runat="server" onFocus="javascript:onFocus(this)" onblur="javascript:onLosFocus(this)"
                                CssClass="textbox" Width="92px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvHora" runat="server" ErrorMessage="Se Requiere la Hora."
                                ControlToValidate="txbHora" Font-Bold="True" SetFocusOnError="True" ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revHora" runat="server" ErrorMessage="Hora No Válida."
                                ControlToValidate="txbHora" Font-Bold="True" SetFocusOnError="True" ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d):([0-5]\d)"
                                ValidationGroup="SICOGUA">*</asp:RegularExpressionValidator>
                            <cc1:TextBoxWatermarkExtender ID="twmHora" runat="server" WatermarkText="hh:mm:ss"
                                TargetControlID="txbHora">
                            </cc1:TextBoxWatermarkExtender>
                        </td>
                        <td class="der">
                            Lugar:
                        </td>
                        <td class="izq">
                            <asp:TextBox ID="txbLugar" runat="server" onFocus="javascript:onFocus(this)" onblur="javascript:onLosFocus(this)"
                                CssClass="textbox" Width="238px" onkeypress="return avalidar(event)" MaxLength="130"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txbLugar"
                                CssClass="Validator" ErrorMessage="Se requiere el Lugar." SetFocusOnError="True"
                                ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <br />
                        </td>
                    </tr>
                </table>
                <%--            </ContentTemplate>
        </asp:UpdatePanel>--%>
                <%--<asp:UpdatePanel ID="updPersonalInvolucrado" runat="server" UpdateMode="Conditional">
            <ContentTemplate>--%>
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
                            <asp:DropDownList ID="ddlGrado" runat="server" CssClass="texto" Enabled="False" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlGrado_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="der">
                            <%--Zona:--%>
                        </td>
                        <td class="izq">
                            <asp:DropDownList ID="ddlZona" runat="server" CssClass="texto" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlZona_SelectedIndexChanged" Visible="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="der">
                            Nombre Completo:
                        </td>
                        <td class="izq">
                            <asp:DropDownList ID="ddlNomCompleto" runat="server" CssClass="texto" Enabled="False"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlNomCompleto_SelectedIndexChanged">
                            </asp:DropDownList>
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
            <table class="tamanio">
                <tr>
                    <td class="der">
                        ¿Que actividad realizaba?:
                    </td>
                    <td class="izq">
                        <asp:TextBox ID="txbActividad" runat="server" onFocus="javascript:onFocus(this)"
                            onblur="javascript:onLosFocus(this)" onchange="this.value=quitaacentos(this.value)"
                            CssClass="textbox" Height="30px" Width="550px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="der">
                        ¿Portaba uniforme o vestia de civil?:
                    </td>
                    <td class="izq">
                        <asp:TextBox ID="txbUniformeCivil" runat="server" onFocus="javascript:onFocus(this)"
                            onblur="javascript:onLosFocus(this)" onchange="this.value=quitaacentos(this.value)"
                            CssClass="textbox" Height="30px" Width="550px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="der">
                        Desarrollo de los hechos y consecuencias:
                    </td>
                    <td class="izq">
                        <asp:TextBox ID="txbHecCon" runat="server" onFocus="javascript:onFocus(this)" onblur="javascript:onLosFocus(this)"
                            CssClass="textbox" Height="30px" onchange="this.value=quitaacentos(this.value)"
                            Width="550px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="der">
                        Cuadro general de lesiones:
                    </td>
                    <td class="izq">
                        <asp:TextBox ID="txbCuaLes" runat="server" onFocus="javascript:onFocus(this)" onblur="javascript:onLosFocus(this)"
                            CssClass="textbox" Height="30px" onchange="this.value=quitaacentos(this.value)"
                            Width="550px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="der">
                        ¿Donde se encuentra el cadaver o lesionado? y ¿Cuál fue el medio para su evacuación?:
                    </td>
                    <td class="izq">
                        <asp:TextBox ID="txbCadLes" runat="server" onFocus="javascript:onFocus(this)" onblur="javascript:onLosFocus(this)"
                            CssClass="textbox" Height="30px" onchange="this.value=quitaacentos(this.value)"
                            Width="550px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="der">
                        Acción tomada contra el agresor:
                    </td>
                    <td class="izq">
                        <asp:TextBox ID="txbAccConAgr" runat="server" onFocus="javascript:onFocus(this)"
                            onblur="javascript:onLosFocus(this)" onchange="this.value=quitaacentos(this.value)"
                            CssClass="textbox" Height="30px" Width="550px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <br />
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="updAutoridadHechos" runat="server" UpdateMode="Conditional">
                <contenttemplate>
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
                            <asp:DropDownList ID="ddlAutGra" runat="server" CssClass="texto" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlAutGra_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="der">
                            <%--Zona:--%>
                        </td>
                        <td class="izq">
                            <asp:DropDownList ID="ddlAutZon" runat="server" CssClass="texto" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlAutZon_SelectedIndexChanged" Visible="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="der">
                            Nombre Completo:
                        </td>
                        <td class="izq">
                            <asp:DropDownList ID="ddlAutNom" runat="server" CssClass="texto" Enabled="False"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlAutNom_SelectedIndexChanged">
                            </asp:DropDownList>
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
                            <asp:TextBox ID="txbAccionMando" runat="server" onFocus="javascript:onFocus(this)"
                                onchange="this.value=quitaacentos(this.value)" onblur="javascript:onLosFocus(this)"
                                CssClass="textbox" Height="30px" Width="567px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <br />
                        </td>
                    </tr>
                </table>
            </contenttemplate>
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
                            <asp:DropDownList ID="ddlParIniGra" runat="server" CssClass="texto" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlParIniGra_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="der">
                            <%--Zona:--%>
                        </td>
                        <td class="izq">
                            <asp:DropDownList ID="ddlParIniZon" runat="server" CssClass="texto" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlParIniZon_SelectedIndexChanged" Visible="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="der">
                            Nombre Completo:
                        </td>
                        <td class="izq">
                            <asp:DropDownList ID="ddlParIniNom" runat="server" CssClass="texto" Enabled="False"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlParIniNom_SelectedIndexChanged">
                            </asp:DropDownList>
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
                            <asp:DropDownList ID="ddlSupAutGra" runat="server" CssClass="texto" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlSupAutGra_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="der">
                            <%--Zona:--%>
                        </td>
                        <td class="izq">
                            <asp:DropDownList ID="ddlSupAutZon" runat="server" CssClass="texto" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlSupAutZon_SelectedIndexChanged" Visible="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="der">
                            Nombre Completo:
                        </td>
                        <td class="izq">
                            <asp:DropDownList ID="ddlSupAutNom" runat="server" CssClass="texto" Enabled="False"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlSupAutNom_SelectedIndexChanged">
                            </asp:DropDownList>
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
                        <asp:TextBox ID="txbDanMat" runat="server" onFocus="javascript:onFocus(this)" onblur="javascript:onLosFocus(this)"
                            CssClass="textbox" Width="442px" onchange="this.value=quitaacentos(this.value)"
                            onkeypress="return avalidar(event);"></asp:TextBox>
                    </td>
                    <td class="der">
                        Monto:
                    </td>
                    <td class="izq">
                        <asp:TextBox ID="txbMonto" runat="server" onFocus="javascript:onFocus(this)" onblur="javascript:onLosFocus(this)"
                            CssClass="textbox" Width="150px" onkeypress="return nvalidar(event)" MaxLength="15"></asp:TextBox>
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
                        <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" class="boton" OnClick="btnNuevo_Click"
                            onMouseOver="javascript:this.style.cursor='hand';" ToolTip="Nuevo" />
                    </td>
                    <td>
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" class="boton" ValidationGroup="SICOGUA"
                            onMouseOver="javascript:this.style.cursor='hand';" OnClick="btnGuardar_Click"
                            ToolTip="Guardar" />
                    </td>
                    <td>
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" class="boton" OnClick="btnCancelar_Click"
                            onMouseOver="javascript:this.style.cursor='hand';" ToolTip="Cancelar" />
                    </td>
                    <td>
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" class="boton" ToolTip="Buscar"
                            OnClick="btnBuscar_Click" onMouseOver="javascript:this.style.cursor='hand';" />
                    </td>
                </tr>
            </table>
            <br />
            <br />
        </div>
    </div>
</asp:Content>
