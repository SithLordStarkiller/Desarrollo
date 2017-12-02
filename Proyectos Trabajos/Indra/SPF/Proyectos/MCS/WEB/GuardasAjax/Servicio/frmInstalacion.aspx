<%@ Page Title="" Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmInstalacion.aspx.cs" Inherits="Servicio_frmInstalacion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../Generales/JScript.js"></script>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    


    <div id="divMain" class="scrollbar">
        <asp:Panel ID="panEstSoc" runat="server" Visible="true">
            <table class="tamanio">
                <tr>
                    <td class="titulo" colspan="4">
                        Instalación
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="tamanio" border="0">
                            <tr>
                                <td colspan="2">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="subtitulo" colspan="2">
                                    Datos del inmueble
                                </td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 99px">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 99px">
                                    Nombre:
                                </td>
                                <td>
                                    <asp:TextBox ID="txbNombre" runat="server" CssClass="textbox" MaxLength="100" 
                                        onblur="javascript:onLosFocus(this)" onFocus="javascript:onFocus(this)" 
                                        Width="470px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvRaiz5" runat="server" 
                                        ControlToValidate="txbNombre" 
                                        ErrorMessage="Debe agregar el nombre de la instalación" Font-Bold="True" 
                                        SetFocusOnError="True" ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 99px">
                                    Servicio:</td>
                                <td>
                                    <asp:DropDownList ID="ddlServicio" runat="server" CssClass="texto" 
                                        Width="475px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvRaiz4" runat="server" 
                                        ControlToValidate="ddlServicio" 
                                        ErrorMessage="Debe seleccionar el servicio" 
                                        Font-Bold="True" SetFocusOnError="True" ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                        <table class="tamanio" border="0">
                            <tr>
                                <td class="der" style="width: 96px">
                                    Zona:
                                </td>
                                <td colspan="4">
                                    <asp:DropDownList ID="ddlZona" runat="server" CssClass="texto" Width="475px" 
                                        Height="16px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvRaiz3" runat="server" 
                                                                ControlToValidate="ddlZona" 
                                                                
                                        ErrorMessage="Debe seleccionar la zona" Font-Bold="True" 
                                                                SetFocusOnError="True" ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>
                                </td>
                                <td class="der" style="width: 233px">
                                    Convenio:
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txbConvenio" runat="server" CssClass="textbox" Width="197px" MaxLength="100"
                                        onFocus="javascript:onFocus(this)" onblur="javascript:onLosFocus(this)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 96px">
                                    Clasificación Instalacion:</td>
                                <td colspan="4">
                                    <asp:DropDownList ID="ddlClasificacion" runat="server" CssClass="texto" 
                                        Width="475px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvClasificacion" runat="server" 
                                        ControlToValidate="ddlClasificacion" 
                                        ErrorMessage="Debe asignar una clasificación" Font-Bold="True" 
                                        SetFocusOnError="True" ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>
                                </td>
                                <td class="der" style="width: 233px">
                                    &nbsp;</td>
                                <td colspan="2">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 96px">
                                    Tipo Instalación:</td>
                                <td colspan="4">
                                    <asp:DropDownList ID="ddlTipoInstalacion" runat="server" CssClass="texto" 
                                        Width="475px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvTipoInstalacion" runat="server" 
                                        ControlToValidate="ddlTipoInstalacion" 
                                        ErrorMessage="Debe asignar el tipo de instalación" Font-Bold="True" 
                                        SetFocusOnError="True" ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>
                                </td>
                                <td class="der" style="width: 233px">
                                    &nbsp;</td>
                                <td colspan="2">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 96px">
                                    F. Inicial:
                                </td>
                                <td style="width: 212px;" class="izq">
                                    <table style="border-collapse: collapse">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txbFechaIni" runat="server" CssClass="textbox" onFocus="javascript:onFocus(this)"
                                                    onblur="javascript:onLosFocus(this)" onkeypress="return validarNoEscritura(event);"></asp:TextBox>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" WatermarkText="dd/mm/aaaa"
                                                                TargetControlID="txbFechaIni">
                                                               </cc1:TextBoxWatermarkExtender>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imbFechaIni" runat="server" Height="18px" ImageUrl="~/Imagenes/Calendar.png"
                                                    Width="18px" />
                                                <cc1:CalendarExtender ID="calFecha" runat="server" PopupButtonID="imbFechaIni" TargetControlID="txbFechaIni">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td>
                                            <asp:RequiredFieldValidator ID="rfvRaiz" runat="server" 
                                        ControlToValidate="txbFechaIni" 
                                        ErrorMessage="Debe seleccionar la fecha de inicio del servicio" Font-Bold="True" 
                                        SetFocusOnError="True" ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="der" style="width: 68px">
                                    F. Final:
                                </td>
                                <td style="width: 148px">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txbFechFinal" runat="server" CssClass="textbox" onFocus="javascript:onFocus(this)"
                                                    onblur="javascript:onLosFocus(this)" onkeypress="return validarNoEscritura(event);"></asp:TextBox>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" WatermarkText="dd/mm/aaaa"
                                                                TargetControlID="txbFechFinal">
                                                              </cc1:TextBoxWatermarkExtender>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgFechaFin" runat="server" Height="18px" ImageUrl="~/Imagenes/Calendar.png"
                                                    Width="18px" />
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="imgFechaFin"
                                                    TargetControlID="txbFechFinal">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 216px">
                       
                                </td>
                                <td style="width: 233px">
                                </td>
                                <td style="width: 179px">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 96px">
                                    &nbsp;</td>
                                <td class="izq" style="width: 212px;">
                                    &nbsp;</td>
                                <td class="der" style="width: 68px">
                                    &nbsp;</td>
                                <td style="width: 148px">
                                    &nbsp;</td>
                                <td style="width: 216px">
                                    &nbsp;</td>
                                <td style="width: 233px">
                                    &nbsp;</td>
                                <td style="width: 179px">
                                   
                                  <asp:HiddenField ID="hfIdInstalacion" runat="server" Value="0" />
                                  <asp:HiddenField ID="hfIdDomicilio" runat="server" Value="0" />
                                   </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="subtitulo" colspan="8">
                                    Responsables</td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 96px">
                                    &nbsp;</td>
                                <td class="izq" colspan="2">
                                    &nbsp;</td>
                                <td class="der" style="width: 148px">
                                    &nbsp;</td>
                                <td colspan="4">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 96px">
                                    Nombre:</td>
                                <td class="izq" colspan="7">
                                    <asp:DropDownList ID="ddlRaiz" runat="server" CssClass="texto" Height="16px" 
                                        Width="475px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 96px">
                                    Observaciones:</td>
                                <td class="izq" colspan="7">
                                    <asp:TextBox ID="txbObservacion" runat="server" CssClass="textbox" 
                                        Width="470px"   onblur="javascript:onLosFocus(this)" 
                                        onFocus="javascript:onFocus(this)" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 96px">
                                    &nbsp;</td>
                                <td class="izq" colspan="7">
                                    <asp:Button ID="btnAgregar" runat="server" CssClass="boton" Text="Agregar" 
                                        onclick="btnAgregar_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 96px">
                                    &nbsp;</td>
                                <td class="izq" colspan="7">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 96px">
                                    &nbsp;</td>
                                <td class="izq" colspan="7">
                              
                              <table style="width: 84%">
                                        <tr runat="server" id="trGrid">
                                            <td>
                                                <div id="Grid">
                                                    <table width="100%">
                                                        <tr>
                                                    
                                                            <td class="cen">
                                                                <asp:GridView ID="grvCatalogo" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                                    ForeColor="#333333" Width="100%" PageSize="5" OnRowDeleting="grvIncidencias_RowDeleting">
                                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
                                                                    <FooterStyle BackColor="#727272" Font-Bold="True" ForeColor="White" />
                                                                    <PagerStyle BackColor="#727272" ForeColor="White" HorizontalAlign="Center" />
                                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                    <HeaderStyle BackColor="#727272" Font-Bold="True" ForeColor="White" />
                                                                    <EditRowStyle BackColor="#999999" />
                                                                    <AlternatingRowStyle BackColor="White" ForeColor="#727272" />
                                                                    <Columns>
                                                                        <asp:TemplateField Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblIndice" runat="server" Text="-"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField Visible="False">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblIdCatalogo" runat="server" Text=''></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField  HeaderText="Nombre">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRaiz" runat="server" Text='<%# Eval("riNombre") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Observaciones">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("riObservaciones") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                
                                                                        <asp:TemplateField HeaderText="Eliminar">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="imbEliminar" runat="server" ImageUrl="~/Imagenes/Symbol-Delete-Mini.png"
                                                                                    ToolTip="Eliminar" CommandName="Delete" OnClientClick="if(!confirm('El registro será eliminado al guardar los cambios, ¿Desea continuar?')) return false;" />
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
                                    </table>
                              



                               </td>
                            </tr>
                            <tr>
                                <td colspan="8">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="subtitulo" colspan="8">
                                    Domicilio
                                </td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 96px">
                                    &nbsp;</td>
                                <td style="width: 212px">
                                    &nbsp;</td>
                                <td class="der" style="width: 68px">
                                    &nbsp;</td>
                                <td style="width: 148px">
                                    &nbsp;</td>
                                <td class="der" style="width: 216px">
                                    &nbsp;</td>
                                <td style="width: 233px">
                                    &nbsp;</td>
                                <td class="der" style="width: 179px">
                                    &nbsp;</td>
                                <td style="text-align: left">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 96px">
                                    Calle:
                                </td>
                                <td style="width: 212px">
                                    <asp:TextBox ID="txbCalle" runat="server" CssClass="textbox" MaxLength="60" 
                                        onblur="javascript:onLosFocus(this)" onFocus="javascript:onFocus(this)" 
                                        Width="187px"></asp:TextBox>
                                </td>
                                <td class="der" style="width: 68px">
                                    No Ext.:
                                </td>
                                <td style="width: 148px">
                                    <asp:TextBox ID="txbNoExt" runat="server" CssClass="textbox" MaxLength="30" 
                                        onblur="javascript:onLosFocus(this)" onFocus="javascript:onFocus(this)"></asp:TextBox>
                                </td>
                                <td class="der" style="width: 216px">
                                    &nbsp;</td>
                                <td style="width: 233px" class="der">
                                    No Int:
                                </td>
                                <td class="der" style="width: 179px; text-align: left">
                                    <asp:TextBox ID="txbNoInt" runat="server" CssClass="textbox" MaxLength="30" 
                                        onblur="javascript:onLosFocus(this)" onFocus="javascript:onFocus(this)" 
                                        Width="108px"></asp:TextBox>
                                    &nbsp;</td>
                                <td style="text-align: left">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 96px">
                                    Estado:
                                </td>
                                <td style="width: 212px">
                                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="texto" Width="193px" 
                                        AutoPostBack="True" 
                                        onselectedindexchanged="ddlEstado_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvRaiz6" runat="server" 
                                        ControlToValidate="ddlEstado" ErrorMessage="Debe seleccionar el Estado" 
                                        Font-Bold="True" SetFocusOnError="True" ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>
                                </td>
                                <td class="der" style="width: 68px">
                                    Municipio:
                                </td>
                                <td colspan="2">
                                    <asp:DropDownList ID="ddlMunicipio" runat="server" CssClass="texto" 
                                        Width="191px" AutoPostBack="True" 
                                        onselectedindexchanged="ddlMunicipio_SelectedIndexChanged" Enabled="False">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvRaiz7" runat="server" 
                                        ControlToValidate="ddlMunicipio" ErrorMessage="Debe seleccionar el Municipio" 
                                        Font-Bold="True" SetFocusOnError="True" ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 233px" class="der">
                                    Colonia:
                                </td>
                                <td colspan="2">
                                    <asp:DropDownList ID="ddlAsentamiento" runat="server" CssClass="texto" 
                                        Width="191px" AutoPostBack="True" 
                                        onselectedindexchanged="ddlAsentamiento_SelectedIndexChanged" 
                                        Enabled="False">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvRaiz8" runat="server" 
                                        ControlToValidate="ddlAsentamiento" ErrorMessage="Debe seleccionar la Colonia" 
                                        Font-Bold="True" SetFocusOnError="True" ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 96px">
                                    &nbsp;</td>
                                <td style="width: 212px">
                                    &nbsp;</td>
                                <td class="der" style="width: 68px">
                                    &nbsp;</td>
                                <td colspan="2">
                                    &nbsp;</td>
                                <td class="der" style="width: 233px">
                                    C.P.:</td>
                                <td colspan="2">
                                    <asp:TextBox ID="txbCP" runat="server" CssClass="textbox" MaxLength="5" 
                                        onblur="javascript:onLosFocus(this)" onFocus="javascript:onFocus(this)" 
                                        onkeypress="return validarNoEscritura(event);" Width="108px" 
                                        Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="der" colspan="8">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="subtitulo" colspan="8">
                                    Coordenadas</td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 96px">
                                    &nbsp;</td>
                                <td style="width: 212px">
                                    &nbsp;</td>
                                <td class="der" style="width: 68px">
                                    &nbsp;</td>
                                <td style="width: 148px">
                                    &nbsp;</td>
                                <td style="width: 216px">
                                    &nbsp;</td>
                                <td style="width: 233px">
                                    &nbsp;</td>
                                <td style="width: 179px">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 96px">
                                    Longitud:
                                </td>
                                <td style="width: 212px">
                                    <asp:TextBox ID="txbLongitud" runat="server" CssClass="textbox" 
                                        onblur="javascript:onLosFocus(this)" 
                                        onchange="this.value=quitaacentos(this.value)" 
                                        onFocus="javascript:onFocus(this)" onkeypress="return dvalidar(event)" 
                                        MaxLength="40"></asp:TextBox>
                                </td>
                                <td class="der" style="width: 68px">
                                    Latitud:
                                </td>
                                <td style="width: 148px">
                                    <asp:TextBox ID="txbLatitud" runat="server" CssClass="textbox" 
                                        onblur="javascript:onLosFocus(this)" 
                                        onchange="this.value=quitaacentos(this.value)" 
                                        onFocus="javascript:onFocus(this)" onkeypress="return dvalidar(event)" 
                                        MaxLength="40"></asp:TextBox>
                                </td>
                                <td class="der" style="width: 216px">
                                    Zona Horaria:
                                </td>
                                <%--ZONA HORARIA--%>
                                   <td style="width: 233px" colspan="3" >
                                       <center>
                                            <asp:DropDownList ID="ddlZonaHoraria" runat="server" CssClass="texto" Width="265px " 
                                        AutoPostBack="True" >
                                        <%--onselectedindexchanged="ddlZonaHoraria_SelectedIndexChanged">--%>
                                    </asp:DropDownList>
                                       </center>
                                   
                                    <asp:RequiredFieldValidator ID="rfvRaiz9" runat="server" 
                                        ControlToValidate="ddlZonaHoraria" ErrorMessage="Debe seleccionar la Zona Horaria" 
                                        Font-Bold="True" SetFocusOnError="True" ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>
                                </td>
                                <%--FIN ZONA HORARIA--%>
                                <td style="width: 62px">
                                </td>
                                <td style="width: 62px">
                                </td>
                                
                               
                            </tr>
                        </table>
                        <table class="tamanio" border="0">
                            <tr>
                                <td class="der" colspan="2">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="subtitulo" colspan="2">
                                    Referencias de la instalación</td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 96px">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 96px">
                                    Colindancia:
                                </td>
                                <td>
                                    <asp:TextBox ID="txbColindancia" runat="server" CssClass="texto" 
                                        MaxLength="3000" onblur="javascript:onLosFocus(this)" 
                                        onFocus="javascript:onFocus(this)" TextMode="MultiLine" Width="760px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 96px">
                                    Descripción de la Instalación:
                                </td>
                                <td>
                                    <asp:TextBox ID="txbDescripcion" runat="server" CssClass="texto" TextMode="MultiLine"
                                        Width="760px" MaxLength="3000" onFocus="javascript:onFocus(this)" 
                                        onblur="javascript:onLosFocus(this)"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <table class="tamanio" border="0">
                            <tr>
                                <td colspan="4">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="subtitulo" colspan="4">
                                    Número de elementos
                                </td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 94px">
                                    &nbsp;</td>
                                <td style="width: 180px">
                                    &nbsp;</td>
                                <td class="der" style="width: 111px">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 94px">
                                    Por Turno:
                                </td>
                                <td style="width: 180px">
                                    <asp:TextBox ID="txbTurno" runat="server" CssClass="textbox" MaxLength="5"  onkeypress="return nvalidar(event)"
                                        onblur="javascript:onLosFocus(this)" onFocus="javascript:onFocus(this)"></asp:TextBox>
                                </td>
                                <td class="der" style="width: 111px">
                                    Masculinos:
                                </td>
                                <td>
                                    <asp:TextBox ID="txbMasculino" runat="server" CssClass="textbox" MaxLength="5"   onkeypress="return nvalidar(event)"
                                        onblur="javascript:onLosFocus(this)" onFocus="javascript:onFocus(this)"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 94px" class="der">
                                    Armados:
                                </td>
                                <td style="width: 180px">
                                    <asp:TextBox ID="txbArmados" runat="server" CssClass="textbox" MaxLength="5" onFocus="javascript:onFocus(this)"  onkeypress="return nvalidar(event)"
                                        onblur="javascript:onLosFocus(this)"></asp:TextBox>
                                </td>
                                <td style="width: 111px" class="der">
                                    femeninos:
                                </td>
                                <td>
                                    <asp:TextBox ID="txbFemenino" runat="server" CssClass="textbox" MaxLength="5" onFocus="javascript:onFocus(this)" onblur="javascript:onLosFocus(this)"   onkeypress="return nvalidar(event)"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <table class="tamanio" border="0">
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="subtitulo" colspan="2">
                                    Funciones
                                </td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 96px">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 96px">
                                    Funciones:
                                </td>
                                <td>
                                    <asp:TextBox ID="txbFunciones" runat="server" CssClass="textbox" 
                                        MaxLength="3000" onblur="javascript:onLosFocus(this)" 
                                        onchange="this.value=quitaacentos(this.value)" 
                                        onFocus="javascript:onFocus(this)" TextMode="MultiLine" Width="760px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 96px">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr >
                                <td class="der" style="width: 96px">
                                </td>
                                <td >
                                    <table>
                                        <tr>
                                            <td style="width: 200px">
                                                &nbsp;</td>
                                            <td>
                                                <asp:Button ID="btnCancelar" runat="server" CssClass="boton" 
                                                    onclick="btnCancelar_Click" Text="Cancelar" />
                                            </td>
                                            <td>
                                                <asp:Button ID="Button1" runat="server" CssClass="boton" 
                                                    onclick="Button1_Click" Text="Nuevo" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnBuscar" runat="server" CssClass="boton" 
                                                    onclick="btnBuscar_Click" Text="Buscar" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnGuardar" runat="server" CssClass="boton" 
                                                    onclick="btnGuardar_Click" Text="Guardar" ValidationGroup="SICOGUA" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 96px">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="subtitulo" colspan="2">
                                    Resultados de la busqueda</td>
                            </tr>
                            <tr>
                                <td  colspan="2">
                                    <table style="margin: 0 auto 0 auto;">
                                        <tr ID="trResultados" runat="server">
                                            <td>
                                                <table width="100%">
                                                    <tr>
                                                        <td align="center">
                                                            <strong>Número de registros encontrados:
                                                            <asp:Label ID="lblCount" runat="server" Text="0"></asp:Label>
                                                            </strong>
                                                        </td>
                                                    </tr>
                                                    <tr ID="trGrid0" runat="server">
                                                        <td>
                                                            <div ID="Grid0">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td class="der">
                                                                            <asp:ImageButton ID="imgAtras" runat="server" 
                                                                                ImageUrl="~/Imagenes/rewind-icon.png" OnClick="imgAtras_Click" 
                                                                                ToolTip="Atrás" />
                                                                        </td>
                                                                        <td class="cen">
                                                                            <asp:GridView ID="grvCatalogo1" runat="server" AutoGenerateColumns="False" 
                                                                                CellPadding="4" ForeColor="#333333" OnDataBound="grvCatalogo1_DataBound" 
                                                                                OnRowDeleting="grvCatalogo1_RowDeleting" 
                                                                                OnRowUpdating="grvCatalogo1_RowUpdating" PageSize="5" Width="100%" 
                                                                                EnableModelValidation="True">
                                                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
                                                                                <FooterStyle BackColor="#727272" Font-Bold="True" ForeColor="White" />
                                                                                <PagerStyle BackColor="#727272" ForeColor="White" HorizontalAlign="Center" />
                                                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                                <HeaderStyle BackColor="#727272" Font-Bold="True" ForeColor="White" />
                                                                                <EditRowStyle BackColor="#999999" />
                                                                                <AlternatingRowStyle BackColor="White" ForeColor="#727272" />
                                                                                <Columns>
                                                                                    <asp:TemplateField Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblIndice1" runat="server" Text="-"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField Visible="False">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblIdServicio" runat="server" Text='<%# Eval("idServicio") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                     <asp:TemplateField Visible="False">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblIdInstalacion" runat="server" Text='<%# Eval("idInstalacion") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField Visible="False">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblIdZona" runat="server" Text='<%# Eval("idZona") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                   <asp:TemplateField HeaderText="Servicio">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblDescripcion1" runat="server" 
                                                                                                Text='<%# Eval("serDescripcion") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Instalación">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblRaiz1" runat="server" Text='<%# Eval("insNombre") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Modificar">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="imbSeleccionar" runat="server" CommandName="Update" 
                                                                                                ImageUrl="~/Imagenes/Download.png" ToolTip="Modificar" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                            <center>
                                                                                <strong>
                                                                                <asp:Label ID="lblPagina" runat="server" Text="0"></asp:Label>
                                                                                de
                                                                                <asp:Label ID="lblPaginas" runat="server" Text="0"></asp:Label>
                                                                                </strong>
                                                                            </center>
                                                                        </td>
                                                                        <td class="izq">
                                                                            <asp:ImageButton ID="imgAdelante" runat="server" 
                                                                                ImageUrl="~/Imagenes/forward-icon.png" OnClick="imgAdelante_Click" 
                                                                                ToolTip="Siguiente" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
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
