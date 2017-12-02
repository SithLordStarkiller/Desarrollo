<%@ Page Title="" Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmHorario.aspx.cs" Inherits="Catalogos_frmHorario" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../Generales/JScript.js"></script>
    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
        <ContentTemplate>
            <div id="divMain" class="scrollbar">
                <asp:Panel ID="panEstSoc" runat="server" Visible="true">
                    <table class="tamanio">
                        <tr>
                            <td class="titulo" colspan="4">
                                HORARIOS
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table class="tamanio" border="0">
                                    <tr>
                                        <td colspan="5">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="subtitulo" colspan="6">
                                            Datos generales de la instalación
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="der" style="width: 129px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 66px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 59px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 82px" class="der">
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
                                        <td class="der">
                                            Zona:
                                        </td>
                                        <td class="izq" colspan="2">
                                            <asp:Label ID="lblZona" runat="server"></asp:Label>
                                        </td>
                                        <td class="der">
                                            Tipo Instalación:
                                        </td>
                                        <td class="izq" colspan="2">
                                            <asp:Label ID="lbTipo" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="der">
                                            Servicio:
                                        </td>
                                        <td class="izq" colspan="3">
                                            <asp:Label ID="lblServicio" runat="server"></asp:Label>
                                        </td>
                                        <td class="der">
                                            Vigente:
                                        </td>
                                        <td>
                                            <asp:Label ID="lblVigente" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="der">
                                            Instalación:
                                        </td>
                                        <td class="izq" colspan="5">
                                            <asp:Label ID="lbInstalacion" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table class="tamanio">
                                    <tr>
                                        <td colspan="5">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="subtitulo" colspan="6">
                                            HORARIOS DE LA INSTALACIÓN
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="der">
                                            Nombre Horario:
                                        </td>
                                        <td class="izq" colspan="2">
                                            <asp:TextBox runat="server" ID="txbNombre" CssClass="textbox" MaxLength="30" onblur="javascript:onLosFocus(this)"
                                                onFocus="javascript:onFocus(this)" Width="253px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfNombre" runat="server" 
                                        ControlToValidate="txbNombre" 
                                        ErrorMessage="Debe agregar el nombre del Horario" Font-Bold="True" 
                                        SetFocusOnError="True" ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td class="der">
                                            Descripción del Horario
                                        </td>
                                        <td class="izq" colspan="3">
                                            <asp:TextBox ID="txbDescripcion" runat="server" CssClass="textbox" MaxLength="200"
                                                Height="80px" onblur="javascript:onLosFocus(this)" onchange="this.value=quitaacentos(this.value)"
                                                onFocus="javascript:onFocus(this)" TextMode="MultiLine" Width="259px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" 
                                        ControlToValidate="txbDescripcion" 
                                        ErrorMessage="Debe agregar la descripción del Horario." Font-Bold="True" 
                                        SetFocusOnError="True" ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="der">
                                            Clasificación Horario:
                                        </td>
                                        <td class="izq" colspan="2">
                                            <asp:DropDownList ID="ddlTipoHorario" runat="server" CssClass="texto">
                                                <asp:ListItem Value="0">Seleccionar</asp:ListItem>
                                                <asp:ListItem Value="A">ADMINISTRATIVO</asp:ListItem>
                                                <asp:ListItem Value="O">OPERATIVO</asp:ListItem>
                                            </asp:DropDownList>
                                             <asp:RequiredFieldValidator ID="rfvRaiz4" runat="server" 
                                        ControlToValidate="ddlTipoHorario" 
                                        ErrorMessage="Debe seleccionar el tipo de Horario" 
                                        Font-Bold="True" SetFocusOnError="True" ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            &nbsp;
                                        </td>
                                        <td colspan="2">
                                           <table>
                                                <tr>
                                                    <td class="der">
                                                        <asp:CheckBox ID="ckbTolerancia" runat="server" Text="Tiempo de Tolerancia Asistencia"
                                                            CssClass="der" oncheckedchanged="ckbTolerancia_CheckedChanged" AutoPostBack="true"  />
                                                    </td>
                                                    <td class="izq">
                                                        <asp:TextBox ID="txbTolerancia" runat="server" CssClass="textbox" MaxLength="4" onblur="javascript:onLosFocus(this)"
                                                            onFocus="javascript:onFocus(this)" Width="41px" Enabled="false"></asp:TextBox>
                                                             <cc1:FilteredTextBoxExtender ID="ftbTolerancia" runat="server" FilterType="Numbers"
                                                                TargetControlID="txbTolerancia">
                                                            </cc1:FilteredTextBoxExtender>
                                                     </td>
                                                     <td>
                                                         minutos
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="der">
                                            Fecha Inicio Horario:
                                        </td>
                                        <td class="izq">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txbFechaIni" runat="server" CssClass="textbox" onblur="javascript:onLosFocus(this)"
                                                            onFocus="javascript:onFocus(this)" onkeypress="return validarNoEscritura(event);"></asp:TextBox>
                                                        <cc1:TextBoxWatermarkExtender ID="twmFecha" runat="server" WatermarkText="dd/mm/aaaa"
                                                            TargetControlID="txbFechaIni">
                                                        </cc1:TextBoxWatermarkExtender>
                                                         <cc1:MaskedEditExtender ID="meeFecha" runat="server" Mask="99/99/9999" MaskType="Date"
                                                         PromptCharacter="_" TargetControlID="txbFechaIni">
                                                        </cc1:MaskedEditExtender>
                                                      <%--  <cc1:MaskedEditValidator ControlToValidate="txbFechaIni" ControlExtender="meeFecha"
                                                         TooltipMessage="Formato (dd/mm/aaaa)" runat="server" IsValidEmpty="true" InvalidValueMessage="Fecha inválida"
                                                        ID="mevFecha" Font-Size="8" ForeColor="Red" />--%>
                                                           <asp:RequiredFieldValidator ID="rfvRaiz" runat="server" 
                                                            ControlToValidate="txbFechaIni" 
                                                            ErrorMessage="Debe seleccionar la fecha de inicio del Horario" Font-Bold="True" 
                                                            SetFocusOnError="True" ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>

                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="imbFechaIni" runat="server" Height="18px" ImageUrl="~/Imagenes/Calendar.png"
                                                            Width="18px" />
                                                    </td>
                                                    <td>
                                                        <cc1:CalendarExtender ID="calFecha" runat="server" PopupButtonID="imbFechaIni" TargetControlID="txbFechaIni">
                                                        </cc1:CalendarExtender>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="der"> Horas laborables:</td>
                                        <td class="izq">
                                            <table>
                                                <tr>
                                                    <td class="texto">
                                                        Trabajo
                                                    </td>
                                                    <td>&nbsp
                                                    </td>
                                                    <td class="texto">
                                                        Descanso
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txbTrabajo" runat="server" CssClass="textbox" MaxLength="4" onblur="javascript:onLosFocus(this)"
                                                    onFocus="javascript:onFocus(this)" Width="41px"></asp:TextBox>
                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                            ControlToValidate="txbTrabajo" 
                                                            ErrorMessage="Debe ingresar las horas de Trabajo" Font-Bold="True" 
                                                            SetFocusOnError="True" ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>
                                                             <cc1:FilteredTextBoxExtender ID="ftbeTrabajo" runat="server" FilterType="Numbers"
                                                                TargetControlID="txbTrabajo">
                                                            </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td class="negritas">
                                                    X
                                                    </td>
                                                    <td>
                                                         <asp:TextBox ID="txbDescanso" runat="server" CssClass="textbox" MaxLength="4" onblur="javascript:onLosFocus(this)"
                                                            onFocus="javascript:onFocus(this)" Width="41px"></asp:TextBox>
                                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                                        ControlToValidate="txbDescanso" 
                                                                        ErrorMessage="Debe ingresar las horas de descanso" Font-Bold="True" 
                                                                        SetFocusOnError="True" ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                                        <asp:CustomValidator
                                                                            ID="cmvValidador" runat="server" ErrorMessage="La suma de la jornada y el descanso debe ser múltiplo de 24"
                                                                            ControlToValidate="txbDescanso" Font-Bold="true" 
                                                             SetFocusOnError="true" ValidationGroup="SICOGUA" EnableClientScript="false" 
                                                             onservervalidate="cmvValidador_ServerValidate" Display="Dynamic" >*</asp:CustomValidator>
                                                              <cc1:FilteredTextBoxExtender ID="ftbDescanso" runat="server" FilterType="Numbers"
                                                                TargetControlID="txbDescanso">
                                                            </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                    <td class="negritas">
                                                     horas
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td class="der">
                                                        <asp:CheckBox ID="ckbRetardos" runat="server" Text="Tiempo de Tolerancia Retardo" 
                                                            CssClass="der" oncheckedchanged="ckbRetardos_CheckedChanged" autopostback="true"  />
                                                    </td>
                                                    <td class="izq">&nbsp
                                                        <asp:TextBox ID="txbRetardo" runat="server" CssClass="textbox" MaxLength="4" onblur="javascript:onLosFocus(this)"
                                                            onFocus="javascript:onFocus(this)" Width="41px" Enabled="false"></asp:TextBox>
                                                             <cc1:FilteredTextBoxExtender ID="ftbRetardo" runat="server" FilterType="Numbers"
                                                                TargetControlID="txbRetardo">
                                                            </cc1:FilteredTextBoxExtender>
                                                     </td>
                                                     <td>
                                                       minutos
                                                    </td>
                                                </tr>
                                            </table>

                                        </td>
                                 </tr>
                                 <tr>
                                     
                                       <td class="der" colspan="2">
                                            <asp:CheckBox ID="ckbVigente" runat="server" Text="Horario Vigente"
                                                            CssClass="der"   />

                                        </td>
                                        
                                         <td  class="cen" colspan="2">
                                            &nbsp;<asp:CheckBox ID="ckbAbierto" runat="server" Text="Horario Abierto"
                                                            CssClass="der"   />
                                        </td>
                                          
                                      
                                   
                                        <td colspan="2">
                                           <table>
                                                <tr>
                                                    <td class="izq">
                                                        <asp:CheckBox ID="ckbComida" runat="server" Text="Permite Tiempo de Comida"
                                                            CssClass="der" oncheckedchanged="ckbComida_CheckedChanged" autopostback="true"  />
                                                    </td>
                                                    <td class="izq">
                                                        <asp:TextBox ID="txbComidaMin" runat="server" CssClass="textbox" MaxLength="4" onblur="javascript:onLosFocus(this)"
                                                            onFocus="javascript:onFocus(this)" Width="41px" Enabled="false"></asp:TextBox>
                                                          <cc1:FilteredTextBoxExtender ID="ftbComida" runat="server" FilterType="Numbers"
                                                                TargetControlID="txbComidaMin">
                                                            </cc1:FilteredTextBoxExtender>
                                                     </td>
                                                     <td>
                                                         minutos
                                                    </td>
                                                    <td>&nbsp; &nbsp</td>
                                                    <td>
                                                        <asp:TextBox ID="txbHoraIni" runat="server" CssClass="textbox" MaxLength="5" onblur="javascript:onLosFocus(this)"
                                                            onFocus="javascript:onFocus(this)" Width="41px" Enabled="false"></asp:TextBox>
                                                               <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderhora" runat="server" WatermarkText="hh:mm"
                                                            TargetControlID="txbHoraIni">
                                                        </cc1:TextBoxWatermarkExtender>
                                                          <cc1:MaskedEditExtender ID="meeH" runat="server" Mask="99:99" MaskType="Time"
                                                         PromptCharacter="_" TargetControlID="txbHoraIni">
                                                        </cc1:MaskedEditExtender>
                                                        <td>
                                                          <asp:RegularExpressionValidator ID="rvSalidaCom" runat="server"
                                                         ControlToValidate="txbHoraIni" 
                                                            ErrorMessage="La hora de Salida no es correcta" Font-Bold="True" 
                                                            SetFocusOnError="True" ValidationGroup="SICOGUA" ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$">* </asp:RegularExpressionValidator>
                                                            </td>
                                                    </td>
                                                    <td> a</td>
                                                    <td><asp:TextBox ID="txbHoraFin" runat="server" CssClass="textbox" MaxLength="5" onblur="javascript:onLosFocus(this)"
                                                            onFocus="javascript:onFocus(this)" Width="41px" Enabled="false"></asp:TextBox></td>
                                                               <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderFin" runat="server" WatermarkText="hh:mm"
                                                            TargetControlID="txbHoraFin">
                                                        </cc1:TextBoxWatermarkExtender>
                                                         <cc1:MaskedEditExtender ID="meeHf" runat="server" Mask="99:99" MaskType="Time"
                                                         PromptCharacter="_" TargetControlID="txbHoraFin">
                                                        </cc1:MaskedEditExtender>
                                                        </td>
                                                        <td>
                                                         <asp:RegularExpressionValidator ID="rvEntComida" runat="server"
                                                         ControlToValidate="txbHoraFin" 
                                                            ErrorMessage="La hora de Regreso no es correcta" Font-Bold="True" 
                                                            SetFocusOnError="True" ValidationGroup="SICOGUA" ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$">*</asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                 </tr>
                                 <tr>
                                    <td class="der">
                                    Días laborables
                                    </td>
                                    <td colspan="2">
                                        <table>
                                            <tr>
                                            <td>&nbsp</td>
                                            <td>Hora Entrada</td>
                                            <td>Hora Salida</td>
                                           
                                            </tr>
                                            <tr>
                                             <td class="izq">
                                              <asp:CheckBox ID="ckbLunes" runat="server" Text="Lunes"
                                                            CssClass="der" AutoPostBack="true" 
                                                     oncheckedchanged="ckbLunes_CheckedChanged"  />
                                            </td>
                                            <td>
                                            <asp:TextBox ID="txbEntL" runat="server" CssClass="textbox" MaxLength="5" onblur="javascript:onLosFocus(this)"
                                                            onFocus="javascript:onFocus(this)" Width="41px" Enabled="false" ></asp:TextBox>
                                                             <cc1:TextBoxWatermarkExtender ID="tbwEntL" runat="server" WatermarkText="hh:mm"
                                                            TargetControlID="txbEntL">
                                                        </cc1:TextBoxWatermarkExtender>
                                                         <cc1:MaskedEditExtender ID="meeEntL" runat="server" Mask="99:99" MaskType="Time"
                                                         PromptCharacter="_" TargetControlID="txbEntL">
                                                        </cc1:MaskedEditExtender>
                                                <asp:RegularExpressionValidator ID="revHora" runat="server"
                                                 ControlToValidate="txbEntL" 
                                                            ErrorMessage="La hora de Entrada no es correcta" Font-Bold="True" 
                                                            SetFocusOnError="True" ValidationGroup="SICOGUA" ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$">* </asp:RegularExpressionValidator>
                                                      <%--  <cc1:MaskedEditValidator ID="maevEntL" runat="server" ControlExtender="meeEntL" MaximumValue="24:59" 
                                                        MinimumValue="00:00" SetFocusOnError="True" IsValidEmpty="true" InvalidValueBlurredMessage="*" 
                                                        ControlToValidate="txbEntl" Display="Dynamic">                                                    
                                                     </cc1:MaskedEditValidator>--%>
                                            </td>
                                            <td>
                                            <asp:TextBox ID="txbSaL" runat="server" CssClass="textbox" MaxLength="5" onblur="javascript:onLosFocus(this)"
                                                            onFocus="javascript:onFocus(this)" Width="41px" Enabled="false"></asp:TextBox>
                                             <cc1:TextBoxWatermarkExtender ID="txbSL" runat="server" WatermarkText="hh:mm"
                                                            TargetControlID="txbSaL">
                                                        </cc1:TextBoxWatermarkExtender>
                                                         <cc1:MaskedEditExtender ID="meeSl" runat="server" Mask="99:99" MaskType="Time"
                                                         PromptCharacter="_" TargetControlID="txbSaL">
                                                        </cc1:MaskedEditExtender>
                                                     
                                                          <asp:RegularExpressionValidator ID="revHoraL" runat="server"
                                                         ControlToValidate="txbSaL" 
                                                            ErrorMessage="La hora de Salida no es correcta" Font-Bold="True" 
                                                            SetFocusOnError="True" ValidationGroup="SICOGUA" ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$">* </asp:RegularExpressionValidator>

                                            </td>
                                            </tr>
                                              </tr>
                                            <tr>
                                             <td class="izq">
                                              <asp:CheckBox ID="ckbMartes" runat="server" Text="Martes"
                                                            CssClass="der" AutoPostBack="true" 
                                                     oncheckedchanged="ckbMartes_CheckedChanged"  />
                                            </td>
                                            <td>
                                            <asp:TextBox ID="txbEntM" runat="server" CssClass="textbox" MaxLength="5" onblur="javascript:onLosFocus(this)"
                                                            onFocus="javascript:onFocus(this)" Width="41px" Enabled="false"></asp:TextBox>
                                              <cc1:TextBoxWatermarkExtender ID="txmEntM" runat="server" WatermarkText="hh:mm"
                                                            TargetControlID="txbEntM">
                                                        </cc1:TextBoxWatermarkExtender>
                                                         <cc1:MaskedEditExtender ID="meeEntM" runat="server" Mask="99:99" MaskType="Time"
                                                         PromptCharacter="_" TargetControlID="txbEntM">
                                                        </cc1:MaskedEditExtender>
                                                 <asp:RegularExpressionValidator ID="revM" runat="server"
                                                         ControlToValidate="txbEntM" 
                                                            ErrorMessage="La hora de Entrada no es correcta" Font-Bold="True" 
                                                            SetFocusOnError="True" ValidationGroup="SICOGUA" ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$">* </asp:RegularExpressionValidator>
                                            </td>
                                            <td>
                                            <asp:TextBox ID="txbEntSaM" runat="server" CssClass="textbox" MaxLength="5" onblur="javascript:onLosFocus(this)"
                                                            onFocus="javascript:onFocus(this)" Width="41px" Enabled="false"></asp:TextBox>
                                             <cc1:TextBoxWatermarkExtender ID="txbwEntSM" runat="server" WatermarkText="hh:mm"
                                                            TargetControlID="txbEntSaM">
                                                        </cc1:TextBoxWatermarkExtender>
                                                         <cc1:MaskedEditExtender ID="meeEntSM" runat="server" Mask="99:99" MaskType="Time"
                                                         PromptCharacter="_" TargetControlID="txbEntSaM">
                                                        </cc1:MaskedEditExtender>
                                                         <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                         ControlToValidate="txbEntSaM" 
                                                            ErrorMessage="La hora de Salida no es correcta" Font-Bold="True" 
                                                            SetFocusOnError="True" ValidationGroup="SICOGUA" ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$">* </asp:RegularExpressionValidator>
                                            </td>
                                            </tr>
                                              </tr>
                                            <tr>
                                             <td class="izq">
                                              <asp:CheckBox ID="ckbMiercoles" runat="server" Text="Miércoles"
                                                            CssClass="der" AutoPostBack="true" 
                                                     oncheckedchanged="ckbMiercoles_CheckedChanged" />
                                            </td>
                                            <td>
                                            <asp:TextBox ID="txbEntMi" runat="server" CssClass="textbox" MaxLength="5" onblur="javascript:onLosFocus(this)"
                                                            onFocus="javascript:onFocus(this)" Width="41px" Enabled="false"></asp:TextBox>
                                             <cc1:TextBoxWatermarkExtender ID="txbmEntMi" runat="server" WatermarkText="hh:mm"
                                                            TargetControlID="txbEntMi">
                                                        </cc1:TextBoxWatermarkExtender>
                                                         <cc1:MaskedEditExtender ID="meeEntMi" runat="server" Mask="99:99" MaskType="Time"
                                                         PromptCharacter="_" TargetControlID="txbEntMi">
                                                        </cc1:MaskedEditExtender>
                                                 <asp:RegularExpressionValidator ID="rvMi" runat="server"
                                                         ControlToValidate="txbEntMi" 
                                                            ErrorMessage="La hora de Entrada no es correcta" Font-Bold="True" 
                                                            SetFocusOnError="True" ValidationGroup="SICOGUA" ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$">* </asp:RegularExpressionValidator>
                                            </td>
                                            <td>
                                            <asp:TextBox ID="txbSaMi" runat="server" CssClass="textbox" MaxLength="5" onblur="javascript:onLosFocus(this)"
                                                            onFocus="javascript:onFocus(this)" Width="41px" Enabled="false"></asp:TextBox>
                                             <cc1:TextBoxWatermarkExtender ID="txbwSaMi" runat="server" WatermarkText="hh:mm"
                                                            TargetControlID="txbSaMi">
                                                        </cc1:TextBoxWatermarkExtender>
                                                         <cc1:MaskedEditExtender ID="meeSaMi" runat="server" Mask="99:99" MaskType="Time"
                                                         PromptCharacter="_" TargetControlID="txbSaMi">
                                                        </cc1:MaskedEditExtender>
                                                 <asp:RegularExpressionValidator ID="rvMiS" runat="server"
                                                         ControlToValidate="txbSaMi" 
                                                            ErrorMessage="La hora de Salida no es correcta" Font-Bold="True" 
                                                            SetFocusOnError="True" ValidationGroup="SICOGUA" ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$">* </asp:RegularExpressionValidator>
                                            </td>
                                            </tr>
                                              </tr>
                                            <tr>
                                             <td class="izq">
                                              <asp:CheckBox ID="ckbJue" runat="server" Text="Jueves"
                                                            CssClass="der" AutoPostBack="true" 
                                                     oncheckedchanged="ckbJue_CheckedChanged"  />
                                            </td>
                                            <td>
                                            <asp:TextBox ID="txbEntJu" runat="server" CssClass="textbox" MaxLength="5" onblur="javascript:onLosFocus(this)"
                                                            onFocus="javascript:onFocus(this)" Width="41px" Enabled="false"></asp:TextBox>
                                             <cc1:TextBoxWatermarkExtender ID="txwEntJ" runat="server" WatermarkText="hh:mm"
                                                            TargetControlID="txbEntJu">
                                                        </cc1:TextBoxWatermarkExtender>
                                                         <cc1:MaskedEditExtender ID="meeJu" runat="server" Mask="99:99" MaskType="Time"
                                                         PromptCharacter="_" TargetControlID="txbEntJu">
                                                        </cc1:MaskedEditExtender>
                                             <asp:RegularExpressionValidator ID="rvJu" runat="server"
                                                         ControlToValidate="txbEntJu" 
                                                            ErrorMessage="La hora de Entrada no es correcta" Font-Bold="True" 
                                                            SetFocusOnError="True" ValidationGroup="SICOGUA" ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$">* </asp:RegularExpressionValidator>
                                            </td>
                                            <td>
                                            <asp:TextBox ID="txbSaJu" runat="server" CssClass="textbox" MaxLength="5" onblur="javascript:onLosFocus(this)"
                                                            onFocus="javascript:onFocus(this)" Width="41px" Enabled="false"></asp:TextBox>
                                             <cc1:TextBoxWatermarkExtender ID="txwSaJu" runat="server" WatermarkText="hh:mm"
                                                            TargetControlID="txbSaJu">
                                                        </cc1:TextBoxWatermarkExtender>
                                                         <cc1:MaskedEditExtender ID="meeSaJu" runat="server" Mask="99:99" MaskType="Time"
                                                         PromptCharacter="_" TargetControlID="txbSaJu">
                                                        </cc1:MaskedEditExtender>
                                                         <asp:RegularExpressionValidator ID="rvJuS" runat="server"
                                                         ControlToValidate="txbSaJu" 
                                                            ErrorMessage="La hora de Salida no es correcta" Font-Bold="True" 
                                                            SetFocusOnError="True" ValidationGroup="SICOGUA" ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$">* </asp:RegularExpressionValidator>
                                            </td>
                                            </tr>
                                              </tr>
                                            <tr>
                                             <td class="izq">
                                              <asp:CheckBox ID="ckbViernes" runat="server" Text="Viernes"
                                                            CssClass="der" AutoPostBack="true" 
                                                     oncheckedchanged="ckbViernes_CheckedChanged"  />
                                            </td>
                                            <td>
                                            <asp:TextBox ID="txtbEntVi" runat="server" CssClass="textbox" MaxLength="5" onblur="javascript:onLosFocus(this)"
                                                            onFocus="javascript:onFocus(this)" Width="41px" Enabled="false"></asp:TextBox>
                                             <cc1:TextBoxWatermarkExtender ID="txbwEntVi" runat="server" WatermarkText="hh:mm"
                                                            TargetControlID="txtbEntVi">
                                                        </cc1:TextBoxWatermarkExtender>
                                                         <cc1:MaskedEditExtender ID="meeEntVi" runat="server" Mask="99:99" MaskType="Time"
                                                         PromptCharacter="_" TargetControlID="txtbEntVi">
                                                        </cc1:MaskedEditExtender>
                                                <asp:RegularExpressionValidator ID="rvViernes" runat="server"
                                                         ControlToValidate="txtbEntVi" 
                                                            ErrorMessage="La hora de Entrada no es correcta" Font-Bold="True" 
                                                            SetFocusOnError="True" ValidationGroup="SICOGUA" ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$">* </asp:RegularExpressionValidator>
                                            </td>
                                            <td>
                                            <asp:TextBox ID="txtbSalVi" runat="server" CssClass="textbox" MaxLength="5" onblur="javascript:onLosFocus(this)"
                                                            onFocus="javascript:onFocus(this)" Width="41px" Enabled="false"></asp:TextBox>
                                             <cc1:TextBoxWatermarkExtender ID="txbeSalVi" runat="server" WatermarkText="hh:mm"
                                                            TargetControlID="txtbSalVi">
                                                        </cc1:TextBoxWatermarkExtender>
                                                         <cc1:MaskedEditExtender ID="meeVi" runat="server" Mask="99:99" MaskType="Time"
                                                         PromptCharacter="_" TargetControlID="txtbSalVi">
                                                        </cc1:MaskedEditExtender>
                                                         <asp:RegularExpressionValidator ID="rvSalVi" runat="server"
                                                         ControlToValidate="txtbSalVi" 
                                                            ErrorMessage="La hora de Salida no es correcta" Font-Bold="True" 
                                                            SetFocusOnError="True" ValidationGroup="SICOGUA" ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$">* </asp:RegularExpressionValidator>

                                            </td>
                                            </tr>
                                              </tr>
                                            <tr>
                                             <td class="izq">
                                              <asp:CheckBox ID="ckbSabado" runat="server" Text="Sábado"
                                                            CssClass="der" AutoPostBack="true" 
                                                     oncheckedchanged="ckbSabado_CheckedChanged"  />
                                            </td>
                                            <td>
                                            <asp:TextBox ID="txbEntSa" runat="server" CssClass="textbox" MaxLength="5" onblur="javascript:onLosFocus(this)"
                                                            onFocus="javascript:onFocus(this)" Width="41px" Enabled="false"></asp:TextBox>
                                             <cc1:TextBoxWatermarkExtender ID="txbwEntSa" runat="server" WatermarkText="hh:mm"
                                                            TargetControlID="txbEntSa">
                                                        </cc1:TextBoxWatermarkExtender>
                                                         <cc1:MaskedEditExtender ID="meeSa" runat="server" Mask="99:99" MaskType="Time"
                                                         PromptCharacter="_" TargetControlID="txbEntSa">
                                                        </cc1:MaskedEditExtender>
                                                 <asp:RegularExpressionValidator ID="revSab" runat="server"
                                                         ControlToValidate="txbEntSa" 
                                                            ErrorMessage="La hora de Entrada no es correcta" Font-Bold="True" 
                                                            SetFocusOnError="True" ValidationGroup="SICOGUA" ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$">* </asp:RegularExpressionValidator>
                                            </td>
                                            <td>
                                            <asp:TextBox ID="txbSalSa" runat="server" CssClass="textbox" MaxLength="5" onblur="javascript:onLosFocus(this)"
                                                            onFocus="javascript:onFocus(this)" Width="41px" Enabled="false"></asp:TextBox>
                                             <cc1:TextBoxWatermarkExtender ID="txbwSalSa" runat="server" WatermarkText="hh:mm"
                                                            TargetControlID="txbSalSa">
                                                        </cc1:TextBoxWatermarkExtender>
                                                         <cc1:MaskedEditExtender ID="meeSalSa" runat="server" Mask="99:99" MaskType="Time"
                                                         PromptCharacter="_" TargetControlID="txbSalSa">
                                                        </cc1:MaskedEditExtender>
                                                          <asp:RegularExpressionValidator ID="rvSalSa" runat="server"
                                                         ControlToValidate="txbSalSa" 
                                                            ErrorMessage="La hora de Salida no es correcta" Font-Bold="True" 
                                                            SetFocusOnError="True" ValidationGroup="SICOGUA" ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$">* </asp:RegularExpressionValidator>
                                            </td>
                                            </tr>
                                              </tr>
                                            <tr>
                                             <td class="izq">
                                              <asp:CheckBox ID="ckbDomingo" runat="server" Text="Domingo"
                                                            CssClass="der" AutoPostBack="true" 
                                                     oncheckedchanged="ckbDomingo_CheckedChanged" />
                                            </td>
                                            <td>
                                            <asp:TextBox ID="txbEntDo" runat="server" CssClass="textbox" MaxLength="5" onblur="javascript:onLosFocus(this)"
                                                            onFocus="javascript:onFocus(this)" Width="41px" Enabled="false"></asp:TextBox>
                                             <cc1:TextBoxWatermarkExtender ID="txbwEntDo" runat="server" WatermarkText="hh:mm"
                                                            TargetControlID="txbEntDo">
                                                        </cc1:TextBoxWatermarkExtender>
                                                         <cc1:MaskedEditExtender ID="meeEntDo" runat="server" Mask="99:99" MaskType="Time"
                                                         PromptCharacter="_" TargetControlID="txbEntDo">
                                                        </cc1:MaskedEditExtender>
                                                  <asp:RegularExpressionValidator ID="rvEnDom" runat="server"
                                                         ControlToValidate="txbEntDo" 
                                                            ErrorMessage="La hora de Entrada no es correcta" Font-Bold="True" 
                                                            SetFocusOnError="True" ValidationGroup="SICOGUA" ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$">* </asp:RegularExpressionValidator>
                                            </td>
                                            <td>
                                            <asp:TextBox ID="txbSalDo" runat="server" CssClass="textbox" MaxLength="5" onblur="javascript:onLosFocus(this)"
                                                            onFocus="javascript:onFocus(this)" Width="41px" Enabled="false"></asp:TextBox>
                                             <cc1:TextBoxWatermarkExtender ID="txbwSalDo" runat="server" WatermarkText="hh:mm"
                                                            TargetControlID="txbSalDo">
                                                        </cc1:TextBoxWatermarkExtender>
                                                         <cc1:MaskedEditExtender ID="meeSalDo" runat="server" Mask="99:99" MaskType="Time"
                                                         PromptCharacter="_" TargetControlID="txbSalDo">
                                                        </cc1:MaskedEditExtender>
                                                <asp:RegularExpressionValidator ID="rvDom" runat="server"
                                                         ControlToValidate="txbSalDo" 
                                                            ErrorMessage="La hora de Salida no es correcta" Font-Bold="True" 
                                                            SetFocusOnError="True" ValidationGroup="SICOGUA" ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$">* </asp:RegularExpressionValidator>
                                            </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>&nbsp
                                     </td>
                                     <td>
                                        <table>
                                        <tr>
                                            <td class="izq" colspan="2">
                                                        <asp:CheckBox ID="ckbES" runat="server" Text="Permite Salidas/Entradas durante la jornada laboral" 
                                                            CssClass="der" />
                                             </td>
                                        
                                        </tr>
                                        <tr>
                                             <td class="izq" colspan="2">
                                                        <asp:CheckBox ID="ckbDiasf" runat="server" Text="Descanso en días Feriados" 
                                                            CssClass="der" />
                                                    </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" class="izq">
                                                    <asp:CheckBox ID="ckbFinesSemana" runat="server" Text="Labora Fines de Semana" 
                                                            CssClass="der" />  
                                            </td>
                                        </tr>
                                        <tr>
                                        <td colspan="2">&nbsp</td>
                                        </tr>
                                        <tr>
                                        <td class="der">Asistencia:
                                        </td>
                                        <td >
                                             <asp:RadioButton ID="rbtBiometrico" runat="server" CssClass="text" 
                                                                GroupName="Asistencia" Text="Biométrico" Checked="True" />
                                                            <asp:RadioButton ID="rbtMCS" runat="server" CssClass="text" 
                                                                GroupName="Asistencia" Text="MCS" 
                                                 oncheckedchanged="rbtMCS_CheckedChanged" />
                                                           
                                        </td>
                                        </tr>
                                        <tr>
                                        <td class="der">
                                            Tiempo máximo para asistencia en MCS
                                        </td>
                                        
                                        <td>
                                            <table>
                                            <tr>
                                                <td>
                                                  <asp:TextBox ID="txbHora" runat="server" CssClass="textbox" MaxLength="5" onblur="javascript:onLosFocus(this)"
                                                                onFocus="javascript:onFocus(this)" Width="41px" Enabled="false"></asp:TextBox>
                                                 <cc1:FilteredTextBoxExtender ID="ftbHora" runat="server" FilterType="Numbers"
                                                                TargetControlID="txbHora">
                                                            </cc1:FilteredTextBoxExtender>
                                                </td>
                                                <td class="izq">horas</td>
                                            </tr>
                                            </table>
                                         </td>
                                        </tr>
                                        <tr><td colspan="2">&nbsp</td></tr>
                                        <tr><td  class="der" >Tipo Horario:</td>
                                        <td>
                                         <table>
                                            <tr>
                                                <td>
                                                 
                                                     <asp:DropDownList ID="ddlTipoHorarioR" runat="server" CssClass="texto">
                                                        </asp:DropDownList>
                                                      <asp:RequiredFieldValidator ID="rfvTipoHorarioR" runat="server" 
                                                        ControlToValidate="ddlTipoHorarioR" 
                                                         ErrorMessage="Debe seleccionar el tipo de Horario" 
                                                         Font-Bold="True" SetFocusOnError="True" ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>
                                                </td>
                                               
                                            </tr>
                                            </table> </td>
                                        </tr>
                                        <tr><td colspan="2">&nbsp</td></tr>
                                        <tr><td colspan="2">&nbsp</td></tr>
                                        </table>
                                     </td>

                                 </tr>
                                <tr>
                                    <td colspan="3">
                                    </td>
                                    <td colspan="2" class="cen">
                                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="boton" 
                                            onclick="btnAgregar_Click" />
                                    </td>
                                </tr> 
                                <tr>
                                    <td colspan="6"></td>
                                </tr>
                                <tr>
                                <td>&nbsp</td>
                                    <td colspan="5" class="cen">
                                    
                                    <div id="Grid">
                                        <table width="100%">
                                            <tr>
                                                <td class="cen">
                                                    <asp:GridView ID="grvHorario" runat="server" AutoGenerateColumns="False" 
                                                        CellPadding="4" ForeColor="#333333"  
                                                        PageSize="5" Width="637px" 
                                                        OnRowUpdating="grvHorario_RowUpdating" >
                                                       
                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
                                                        <FooterStyle BackColor="#727272" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="#727272" ForeColor="White" HorizontalAlign="Center" />
                                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                        <HeaderStyle BackColor="#727272" Font-Bold="True" ForeColor="White" />
                                                        <EditRowStyle BackColor="#999999" />
                                                        <AlternatingRowStyle BackColor="White" ForeColor="#727272" />
                                                        <Columns>
                                                           <asp:TemplateField Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIdHorario" runat="server" Text='<%# Eval("idHorario") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Numero">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRaiz" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Horario">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDescripcion" runat="server" 
                                                                        Text='<%# Eval("horNombre") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                               <asp:TemplateField HeaderText="Inicio Horario">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFechaInicio" runat="server" 
                                                                        Text='<%# Eval("horFechaInicio","{0:d}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                               <asp:TemplateField HeaderText="Vigente">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVigenteh" runat="server" 
                                                                    Text='<%# string.Format("{0}", (bool)Eval("horVigente") ? "SI" : "NO") %>'>
                                                                     </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Consulta">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imbMostrarDatos" runat="server" CommandName="Update" 
                                                                        ImageUrl="~/Imagenes/search-icon.png" 
                                                                        ToolTip="Consultar / Modificar" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                              
                                    </td>
                                </tr>
                                <tr>
                                <td>
                                </td>
                                <td colspan="5" class="cen">
                                    <table class="cen">
                                    <tr>
                                    <td>
                                     <asp:Button ID="btnGuardar" runat="server" CssClass="boton"
                                      Text="Guardar" onclick="btnGuardar_Click" ValidationGroup="SICOGUA"/>
                                    </td>
                                    <td>
                                      <asp:Button ID="btnCancelar" runat="server" CssClass="boton"
                                      Text="Cancelar" onclick="btnCancelar_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnBuscar" runat="server" CssClass="boton"
                                        text="Buscar" onclick="btnBuscar_Click" />
                                    </td>
                                    </tr>
                                    </table>
                                </td>
                                </tr>

                              </table>
                            </td> 
                        </tr>
                      
                    </table>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
