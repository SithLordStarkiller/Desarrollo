<%@ Page Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmParteNovedad.aspx.cs" Inherits="Reportes_frmParteNovedad" Title="Módulo de Control de Servicios ::: Parte de Novedades" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../Generales/JScript.js"></script>

    <asp:Panel ID="panParteNovedad" runat="server">
        <div id="Contenido" class="scrollbar">
            <div>
                <table class="tamanio">
                    <tr runat="server" id="trParametros">
                        <td>
                            <table class="tamanio">
                                <tr>
                                    <td class="titulo">
                                        Parte de Novedades
                                    </td>
                                </tr>
                                <tr>
                                    <td class="subtitulo">
                                        Criterios de Búsqueda
                                    </td>
                                </tr>
                                <tr>
                                    <td class="izq">
                                        <asp:UpdatePanel ID="updFiltros" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table>
                                                    <tr>
                                                        <td colspan="4">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der">
                                                            Fecha del Parte de Novedades:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:TextBox ID="txbFecha" runat="server" onFocus="javascript:onFocus(this)" onblur="javascript:onLosFocus(this)"
                                                                CssClass="textbox" Width="80px" onkeypress="return validarNoEscritura(event);">
                                                            </asp:TextBox>
                                                            <asp:ImageButton ID="imbFecha" runat="server" ImageUrl="~/Imagenes/Calendar.png"
                                                                Height="18px" Width="18px" />
                                                            <asp:RequiredFieldValidator ID="rfvFecha" runat="server" ErrorMessage="Se Requiere la Fecha."
                                                                Font-Bold="True" ControlToValidate="txbFecha" SetFocusOnError="True" ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>
                                                            <asp:CompareValidator ID="covFecha" runat="server" ErrorMessage="La Fecha No Puede Ser Mayor al Día de Hoy."
                                                                ControlToValidate="txbFecha" Font-Bold="True" SetFocusOnError="True" Type="Date"
                                                                ValidationGroup="SICOGUA" ValueToCompare="01/01/0001" Operator="LessThanEqual">*</asp:CompareValidator>
                                                            <cc1:CalendarExtender ID="calFecha" runat="server" PopupButtonID="imbFecha" TargetControlID="txbFecha">
                                                            </cc1:CalendarExtender>
                                                            <cc1:TextBoxWatermarkExtender ID="twmFecha" runat="server" WatermarkText="dd/mm/aaaa"
                                                                TargetControlID="txbFecha">
                                                            </cc1:TextBoxWatermarkExtender>
                                                        </td>
                                                        <td class="der">
                                                            Hora del Parte de Novedades:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:TextBox ID="txbHora" runat="server" onFocus="javascript:onFocus(this)" onblur="javascript:onLosFocus(this)"
                                                                onKeyDown="if(event.keyCode==13){event.keyCode=9;}" CssClass="textbox" Width="92px">
                                                            </asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="revHora" runat="server" ErrorMessage="Hora No Válida."
                                                                ControlToValidate="txbHora" Font-Bold="True" SetFocusOnError="True" ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d):([0-5]\d)"
                                                                ValidationGroup="SICOGUA">*
                                                            </asp:RegularExpressionValidator>
                                                            <cc1:TextBoxWatermarkExtender ID="twmHora" runat="server" WatermarkText="hh:mm:ss"
                                                                TargetControlID="txbHora">
                                                            </cc1:TextBoxWatermarkExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der">
                                                            Responsable de enviar Parte de Novedades:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:DropDownList ID="ddlEnvia" runat="server" CssClass="texto">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvEnvia" runat="server" ErrorMessage="Debe Seleccionar el Responsable que Envía."
                                                                ControlToValidate="ddlEnvia" Font-Bold="True" SetFocusOnError="True" ValidationGroup="SICOGUA"
                                                                Text="*">
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td class="der">
                                                            Responsable de recibir Parte de Novedades:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:DropDownList ID="ddlRecibe" runat="server" CssClass="texto">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvRecibe" runat="server" ErrorMessage="Debe Seleccionar el Responsable que Recibe."
                                                                ControlToValidate="ddlRecibe" Font-Bold="True" SetFocusOnError="True" ValidationGroup="SICOGUA"
                                                                Text="*">
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der">
                                                            Entrada de Fuerza:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:TextBox ID="txbEntrada" onFocus="javascript:onFocus(this)" onblur="javascript:onLosFocus(this)"
                                                                CssClass="textbox" Width="250px" TextMode="MultiLine" Height="50px" runat="server">
                                                            </asp:TextBox>
                                                        </td>
                                                        <td class="der">
                                                            Salida de Fuerza:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:TextBox ID="txbSalida" onFocus="javascript:onFocus(this)" onblur="javascript:onLosFocus(this)"
                                                                CssClass="textbox" Width="250px" TextMode="MultiLine" Height="50px" runat="server">
                                                            </asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der">
                                                            Altas:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:TextBox ID="txbAltas" onFocus="javascript:onFocus(this)" onblur="javascript:onLosFocus(this)"
                                                                CssClass="textbox" Width="250px" TextMode="MultiLine" Height="50px" runat="server">
                                                            </asp:TextBox>
                                                        </td>
                                                        <td class="der">
                                                            Bajas:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:TextBox ID="txbBajas" onFocus="javascript:onFocus(this)" onblur="javascript:onLosFocus(this)"
                                                                CssClass="textbox" Width="250px" TextMode="MultiLine" Height="50px" runat="server">
                                                            </asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der">
                                                            Nota Faltistas de primer día:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:TextBox ID="txbNotaFaltistasPrimerDia" onFocus="javascript:onFocus(this)" onblur="javascript:onLosFocus(this)"
                                                                CssClass="textbox" Width="250px" TextMode="MultiLine" Height="25px" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td class="der">
                                                            Nota Faltistas de segundo día:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:TextBox ID="txbNotaFaltistasSegundoDia" onFocus="javascript:onFocus(this)" onblur="javascript:onLosFocus(this)"
                                                                CssClass="textbox" Width="250px" TextMode="MultiLine" Height="25px" runat="server"
                                                                OnTextChanged="TextBox2_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der">
                                                            Nota Faltistas de tercer día:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:TextBox ID="txbNotaFaltistasTercerDia" onFocus="javascript:onFocus(this)" onblur="javascript:onLosFocus(this)"
                                                                CssClass="textbox" Width="250px" TextMode="MultiLine" Height="25px" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td class="der">
                                                            Nota Faltistas de cuarto día:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:TextBox ID="txbNotaFaltistasCuartoDia" onFocus="javascript:onFocus(this)" onblur="javascript:onLosFocus(this)"
                                                                CssClass="textbox" Width="250px" TextMode="MultiLine" Height="25px" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der">
                                                            Nota Retardos:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:TextBox ID="txbNotaRetardos" onFocus="javascript:onFocus(this)" onblur="javascript:onLosFocus(this)"
                                                                CssClass="textbox" Width="250px" TextMode="MultiLine" Height="25px" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td class="der">
                                                            Nota Exceptuados:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:TextBox ID="txbNotaExceptuados" onFocus="javascript:onFocus(this)" onblur="javascript:onLosFocus(this)"
                                                                CssClass="textbox" Width="250px" TextMode="MultiLine" Height="25px" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der">
                                                            Nota Presentes Primer día:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:TextBox ID="txbNotaPresentesPrimerDia" onFocus="javascript:onFocus(this)" onblur="javascript:onLosFocus(this)"
                                                                CssClass="textbox" Width="250px" TextMode="MultiLine" Height="25px" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td class="der">
                                                            Nota Presentes Segundo día:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:TextBox ID="txbNotaPresentesSegundoDia" onFocus="javascript:onFocus(this)" onblur="javascript:onLosFocus(this)"
                                                                CssClass="textbox" Width="250px" TextMode="MultiLine" Height="25px" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der">
                                                            Nota Presentes Tercer día:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:TextBox ID="txbNotaPresentesTercerDia" onFocus="javascript:onFocus(this)" onblur="javascript:onLosFocus(this)"
                                                                CssClass="textbox" Width="250px" TextMode="MultiLine" Height="25px" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td class="der">
                                                            Nota Presentes Licencia Médica:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:TextBox ID="txbNotaPresentesLicenciaMedica" onFocus="javascript:onFocus(this)"
                                                                onblur="javascript:onLosFocus(this)" CssClass="textbox" Width="250px" TextMode="MultiLine"
                                                                Height="25px" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der">
                                                            Nota Licencias Médicas:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:TextBox ID="txbNotaLicenciasMedicas" onFocus="javascript:onFocus(this)" onblur="javascript:onLosFocus(this)"
                                                                CssClass="textbox" Width="250px" TextMode="MultiLine" Height="25px" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td class="der">
                                                            Nota Presentes Vacaciones:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:TextBox ID="txbNotaPresentesVacaciones" onFocus="javascript:onFocus(this)" onblur="javascript:onLosFocus(this)"
                                                                CssClass="textbox" Width="250px" TextMode="MultiLine" Height="25px" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der">
                                                            Nota Vacaciones:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:TextBox ID="txbNotaVacaciones" onFocus="javascript:onFocus(this)" onblur="javascript:onLosFocus(this)"
                                                                CssClass="textbox" Width="250px" TextMode="MultiLine" Height="25px" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td class="der">
                                                            Con Copia Para:
                                                        </td>
                                                        <td class="izq" colspan="3">
                                                            <asp:TextBox ID="txbCopia" onFocus="javascript:onFocus(this)" onblur="javascript:onLosFocus(this)"
                                                                onKeyDown="if(event.keyCode==13){event.keyCode=9;}" CssClass="textbox" Width="250px"
                                                                runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="der">
                                        <asp:Button ID="btnGenerar" runat="server" Text="Generar Reporte" CssClass="boton"
                                            onMouseOver="javascript:this.style.cursor='hand';" ToolTip="Generar Reporte"
                                            ValidationGroup="SICOGUA" OnClick="btnGenerar_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
