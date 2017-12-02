<%@ Page Title="" Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true" CodeFile="frmServicio.aspx.cs" Inherits="Servicio_frmServicio" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script language="javascript" type="text/javascript" src="../Generales/JScript.js"></script>
    
    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
    <ContentTemplate>


    
    <div id="divMain" class="scrollbar">
        <asp:Panel ID="panEstSoc" runat="server" Visible="true">
            <table class="tamanio">
                <tr>
                    <td class="titulo" colspan="4">
                        SERVICIO</td>
                </tr>
                <tr>
                    <td>
                        <table class="tamanio" border="0">
                            <tr>
                                <td colspan="5">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="subtitulo" colspan="6">
                                    Datos del servicio</td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 129px">
                                    &nbsp;</td>
                                <td style="width: 66px">
                                    &nbsp;</td>
                                <td style="width: 59px">
                                    &nbsp;</td>
                                <td style="width: 82px" class="der">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 129px">
                                    Razón Social:
                                </td>
                                <td style="width: 66px">
                                    <asp:TextBox ID="txbRazonSocial" runat="server" CssClass="textbox" MaxLength="100" 
                                        onblur="javascript:onLosFocus(this)" onFocus="javascript:onFocus(this)" 
                                        Width="509px"></asp:TextBox>
                                </td>
                                <td style="width: 59px">
                                    &nbsp;</td>
                                <td class="der" style="width: 82px">
                                    Tipo de Servicio:</td>
                                <td>
                                    <asp:DropDownList ID="ddlTipoServicio" runat="server" CssClass="texto" 
                                        Width="150px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvRaiz9" runat="server" 
                                        ControlToValidate="ddlTipoServicio" 
                                        ErrorMessage="Debe seleccionar el tipo de servicio" Font-Bold="True" 
                                        SetFocusOnError="True" ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 129px">
                                    Nombre abreviado:</td>
                                <td style="width: 66px">
                                    <asp:TextBox ID="txbAbreviado" runat="server" CssClass="textbox" 
                                        Width="509px"         onFocus="javascript:onFocus(this)" 
                                        onblur="javascript:onLosFocus(this)"></asp:TextBox>
                                    
                                </td>
                                <td style="width: 59px">
                                  <asp:RequiredFieldValidator ID="rfvRaiz" runat="server" 
                                        ControlToValidate="txbAbreviado" 
                                        ErrorMessage="Debe asignar un nombre abreviado" Font-Bold="True" 
                                        SetFocusOnError="True" ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator></td>
                                <td class="der" style="width: 82px">
                                    R.F.C.:</td>
                                <td>
                                    <asp:TextBox ID="txbRfc" runat="server" CssClass="textbox" 
                                        Width="150px"         onFocus="javascript:onFocus(this)" 
                                        onblur="javascript:onLosFocus(this)"></asp:TextBox>
                                </td>

                                 <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txbRfc"
                                MaskType="None" Mask="$$$-999999-$$9" PromptCharacter="_">
                            </cc1:MaskedEditExtender>

                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 129px">
                                    Pagina Web:</td>
                                <td style="width: 66px">
                                    <asp:TextBox ID="txbPaginaWeb" runat="server" CssClass="textbox" 
                                        onblur="javascript:onLosFocus(this)" onFocus="javascript:onFocus(this)" 
                                        Width="509px"></asp:TextBox>
                                </td>
                                <td style="width: 59px">
                                    &nbsp;</td>
                                <td class="der" style="width: 82px">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 129px">
                                    Tipo de Servicio:</td>
                                <td style="width: 66px">
                                    <asp:DropDownList ID="ddlClasificacionServ" runat="server" CssClass="texto">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 59px">
                                    &nbsp;</td>
                                <td class="der" style="width: 82px">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 129px">
                                    &nbsp;</td>
                                <td style="width: 66px">
                                    &nbsp;</td>
                                <td style="width: 59px">
                                    &nbsp;</td>
                                <td class="der" style="width: 82px">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 129px">
                                    Fecha Inicial:</td>
                                <td style="width: 66px">
                                    <table style="width: 420px">
                                        <tr>
                                            <td style="width: 140px">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txbFechaIni" runat="server" CssClass="textbox" 
                                                                onblur="javascript:onLosFocus(this)" onFocus="javascript:onFocus(this)"  onkeypress="return validarNoEscritura(event);"></asp:TextBox>
                                                                <cc1:TextBoxWatermarkExtender ID="twmFecha" runat="server" WatermarkText="dd/mm/aaaa"
                                                                TargetControlID="txbFechaIni">
                                                               </cc1:TextBoxWatermarkExtender>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="imbFechaIni" runat="server" Height="18px" 
                                                                ImageUrl="~/Imagenes/Calendar.png" Width="18px" />
                                                            
                                                            <cc1:CalendarExtender ID="calFecha" runat="server" PopupButtonID="imbFechaIni" 
                                                                TargetControlID="txbFechaIni">
                                                            </cc1:CalendarExtender>

                                                        </td>
                                                        <td>
                                                        <asp:RequiredFieldValidator ID="rfvRaiz3" runat="server" 
                                                                ControlToValidate="txbFechaIni" 
                                                                ErrorMessage="Debe seleccionar la fecha de inicio del servicio" Font-Bold="True" 
                                                                SetFocusOnError="True" ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td class="der" style="width: 315px">
                                                Fecha Final:</td>
                                            <td style="width: 170px">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txbFechFinal" runat="server" CssClass="textbox" 
                                                                onblur="javascript:onLosFocus(this)" onFocus="javascript:onFocus(this)"  onkeypress="return validarNoEscritura(event);"></asp:TextBox>
                                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" WatermarkText="dd/mm/aaaa"
                                                                TargetControlID="txbFechFinal">
                                                               </cc1:TextBoxWatermarkExtender>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="imgFechaFin" runat="server" Height="18px" 
                                                                ImageUrl="~/Imagenes/Calendar.png" Width="18px" />
                                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" 
                                                                PopupButtonID="imgFechaFin" TargetControlID="txbFechFinal">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 59px">
                                    &nbsp;</td>
                                <td class="der" style="width: 82px">
                                    Logotipo:</td>
                                <td>
                                    <asp:Button ID="btnLogotipo" runat="server" Text="Agregar" CssClass="boton" />
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 129px">
                                    Observaciones:</td>
                                <td style="width: 66px">
                                    <asp:TextBox ID="txbObservaciones" runat="server" CssClass="textbox" Height="122px" 
                                        MaxLength="3000" onblur="javascript:onLosFocus(this)" 
                                        onchange="this.value=quitaacentos(this.value)" 
                                        onFocus="javascript:onFocus(this)" TextMode="MultiLine" Width="507px"></asp:TextBox>
                                </td>
                                <td style="width: 59px">
                                    &nbsp;</td>
                                <td class="der" style="width: 82px">
                                    &nbsp;</td>
                                <td>
                                    <asp:Image ID="imgDoc" runat="server" Height="126px" 
                                        ImageUrl="~/Imagenes/nodisponible.jpg" Width="102px" />
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 129px">
                                    &nbsp;</td>
                                <td style="width: 66px; ">
                                    <asp:HiddenField ID="hfIdServicio" runat="server" Value="0" />
                                    <asp:HiddenField ID="hfIdDomicilio" runat="server" Value="0" />
                                </td>
                                <td style="width: 59px; ">
                                    &nbsp;</td>
                                <td style="width: 82px">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="subtitulo" colspan="6">
                                    Responsables</td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 129px">
                                    &nbsp;</td>
                                <td style="width: 66px; ">
                                    &nbsp;</td>
                                <td style="width: 59px; ">
                                    &nbsp;</td>
                                <td style="width: 82px">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 129px">
                                    Nombre:</td>
                                <td style="width: 66px; ">
                                    <asp:DropDownList ID="ddlRaiz" runat="server" CssClass="texto" Height="16px" 
                                        Width="485px">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 59px; ">
                                    &nbsp;</td>
                                <td style="width: 82px">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 129px" class="der">
                                    Observaciones:</td>
                                <td style="width: 66px; ">
                                    <asp:TextBox ID="txbObservacion" runat="server" CssClass="textbox" 
                                        onblur="javascript:onLosFocus(this)" onFocus="javascript:onFocus(this)" 
                                        Width="479px"></asp:TextBox>
                                </td>
                                <td style="width: 59px; ">
                                    &nbsp;</td>
                                <td style="width: 82px">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 129px">
                                    &nbsp;</td>
                                <td style="width: 66px; ">
                                    <asp:Button ID="btnAgregar" runat="server" CssClass="boton" 
                                        onclick="btnAgregar_Click" Text="Agregar" />
                                </td>
                                <td style="width: 59px; ">
                                    &nbsp;</td>
                                <td style="width: 82px">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 129px">
                                </td>
                                <td style="width: 66px; ">
                                    <cc1:ModalPopupExtender ID="mpeLogotipo" runat="server" 
                                        BackgroundCssClass="modalBackground" PopupControlID="panFoto" 
                                        TargetControlID="btnLogotipo">
                                    </cc1:ModalPopupExtender>
                                </td>
                                <td style="width: 59px; ">
                                    &nbsp;</td>
                                <td style="width: 82px">
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 129px">
                                    &nbsp;</td>
                                <td colspan="4">
                                    <div ID="Grid">
                                        <table width="100%">
                                            <tr>
                                                <td class="cen">
                                                    <asp:GridView ID="grvCatalogo" runat="server" AutoGenerateColumns="False" 
                                                        CellPadding="4" ForeColor="#333333" OnRowDeleting="grvIncidencias_RowDeleting" 
                                                        PageSize="5" Width="637px">
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
                                                                    <asp:Label ID="lblIdCatalogo" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Nombre">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRaiz" runat="server" Text='<%# Eval("riNombre") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Observaciones">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDescripcion" runat="server" 
                                                                        Text='<%# Eval("riObservaciones") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Eliminar">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imbEliminar" runat="server" CommandName="Delete" 
                                                                        ImageUrl="~/Imagenes/Symbol-Delete-Mini.png" 
                                                                        OnClientClick="if(!confirm('El registro será eliminado al guardar los cambios, ¿Desea continuar?')) return false;" 
                                                                        ToolTip="Eliminar" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 129px">
                                    &nbsp;</td>
                                <td colspan="4">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            <table class="tamanio">
            
              <tr>
                                <td class="subtitulo" colspan="8">
                                    Domicilio
                                </td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 104px">
                                    &nbsp;</td>
                                <td style="width: 205px">
                                    &nbsp;</td>
                                <td class="der" style="width: 70px">
                                    &nbsp;</td>
                                <td style="width: 148px">
                                    &nbsp;</td>
                                <td class="der" style="width: 44px">
                                    &nbsp;</td>
                                <td style="width: 52px">
                                    &nbsp;</td>
                                <td class="der">
                                    &nbsp;</td>
                                <td style="text-align: left">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 104px">
                                    Calle:
                                </td>
                                <td style="width: 205px">
                                    <asp:TextBox ID="txbCalle" runat="server" CssClass="textbox" MaxLength="60" 
                                        onblur="javascript:onLosFocus(this)" onFocus="javascript:onFocus(this)" 
                                        Width="187px"></asp:TextBox>
                                </td>
                                <td class="der" style="width: 70px">
                                    No Ext.:
                                </td>
                                <td style="width: 148px">
                                    <asp:TextBox ID="txbNoExt" runat="server" CssClass="textbox" MaxLength="30" 
                                        onblur="javascript:onLosFocus(this)" onFocus="javascript:onFocus(this)"></asp:TextBox>
                                </td>
                                <td class="der" style="width: 44px">
                                    &nbsp;</td>
                                <td style="width: 52px" class="der">
                                    No Int:</td>
                                <td class="izq">
                                    <asp:TextBox ID="txbNoInt" runat="server" CssClass="textbox" MaxLength="30" 
                                        onblur="javascript:onLosFocus(this)" onFocus="javascript:onFocus(this)" 
                                        Width="108px"></asp:TextBox>
                                </td>
                                <td style="text-align: left">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 104px">
                                    Estado:
                                </td>
                                <td style="width: 205px">
                                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="texto" Width="193px" 
                                        AutoPostBack="True" 
                                        onselectedindexchanged="ddlEstado_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvRaiz6" runat="server" 
                                        ControlToValidate="ddlEstado" ErrorMessage="Debe seleccionar el Estado" 
                                        Font-Bold="True" SetFocusOnError="True" ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>
                                </td>
                                <td class="der" style="width: 70px">
                                    Municipio:
                                </td>
                                <td colspan="2">
                                    <asp:DropDownList ID="ddlMunicipio" runat="server" CssClass="texto" 
                                        Width="193px" AutoPostBack="True" 
                                        onselectedindexchanged="ddlMunicipio_SelectedIndexChanged" Enabled="False">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvRaiz7" runat="server" 
                                        ControlToValidate="ddlMunicipio" ErrorMessage="Debe seleccionar el Municipio" 
                                        Font-Bold="True" SetFocusOnError="True" ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 52px" class="der">
                                    Colonia:
                                </td>
                                <td colspan="2">
                                    <asp:DropDownList ID="ddlAsentamiento" runat="server" CssClass="texto" 
                                        Width="193px" AutoPostBack="True" 
                                        onselectedindexchanged="ddlAsentamiento_SelectedIndexChanged" 
                                        Enabled="False">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvRaiz8" runat="server" 
                                        ControlToValidate="ddlAsentamiento" ErrorMessage="Debe seleccionar la Colonia" 
                                        Font-Bold="True" SetFocusOnError="True" ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 104px">
                                    &nbsp;</td>
                                <td style="width: 205px">
                                    &nbsp;</td>
                                <td class="der" style="width: 70px">
                                    &nbsp;</td>
                                <td colspan="2">
                                    &nbsp;</td>
                                <td class="der" style="width: 52px">
                                    C.P.: </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txbCP" runat="server" CssClass="textbox" MaxLength="5" 
                                        onblur="javascript:onLosFocus(this)" onFocus="javascript:onFocus(this)" 
                                        onkeypress="return validarNoEscritura(event);" Width="108px" 
                                        Enabled="False"></asp:TextBox>
                                </td>
                </tr>
                <tr>
                    <td class="der" style="width: 104px">
                        &nbsp;</td>
                    <td colspan="7">
                        <table style="350px: ;">
                            <tr>
                                <td style="width: 200px">
                                </td>
                                <td>
                                    <asp:Button ID="btnCancelar" runat="server" CssClass="boton" 
                                        onclick="btnCancelar_Click" Text="Cancelar" />
                                </td>
                                <td>
                                    <asp:Button ID="btnNuevo" runat="server" CssClass="boton" 
                                        onclick="btnNuevo_Click" Text="Nuevo" />
                                </td>
                                <td>
                                    <asp:Button ID="btnBuscar" runat="server" CssClass="boton" Text="Buscar" 
                                        onclick="btnBuscar_Click" />
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
                                <td class="der" colspan="8">
                                    &nbsp;</td>
                            </tr>
                <tr>
                    <td class="subtitulo" colspan="8">
                        Resultados de la busqueda</td>
                </tr>
                <tr>
                    <td colspan="8">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="8">
                        <table style="margin: 0 auto 0 auto;">
                            <tr ID="trResultados" runat="server">
                                <td>
                                    <table width="100%">
                                           <tr runat="server" id="trGrid">
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
                                        
                                          
                                                                        <asp:TemplateField HeaderText="Servicio">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDescripcion1" runat="server" 
                                                                                    Text='<%# Eval("serDescripcion") %>'></asp:Label>
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


        </asp:Panel>


        





              





    </div>






    
            </ContentTemplate>
    </asp:UpdatePanel>

    



          <asp:Panel ID="panFoto" runat="server">
            <div style="background-color: White; margin: 0 auto 0 auto;" class="nder">
                <div style="background-repeat: repeat; background-image: url(./../Imagenes/line.png);
                    margin: 30px auto 30px auto; border: outset 1px Black;">
                    <asp:UpdatePanel ID="upnFotoRd" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td class="subtitulo">
                                        Logotipo</td>
                                </tr>
                                <tr>
                                    <td class="divError">
                                        <asp:UpdatePanel ID="updMensaje1" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div style="border-color: Red; border-style: solid; border-width: 1px; background-color: #FFFF80;
                                                    font-size: 7pt; font-weight: bold; text-align: justify; color: Red" runat="server"
                                                    id="divMensajes" visible="false">
                                                    <asp:BulletedList ID="bulMensajes" runat="server" Width="100%" BulletStyle="Disc"
                                                        ValidationGroup="AgregarFoto">
                                                    </asp:BulletedList>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="izq">
                                        <asp:ValidationSummary ID="valFoto" runat="server" ValidationGroup="AgregarFoto"
                                            CssClass="divError" HeaderText="Mensaje(s):" Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td align="center">
                                                    <asp:FileUpload ID="fiuImagen" runat="server" Width="500px" ToolTip="Seleccionar"
                                                        EnableViewState="true" />
                                                    <asp:RequiredFieldValidator ID="rfvFoto" runat="server" ErrorMessage="Debes Seleccionar Un Archivo."
                                                        ControlToValidate="fiuImagen" Font-Bold="True" SetFocusOnError="True" ValidationGroup="AgregarFoto">*</asp:RequiredFieldValidator>

                                                                    
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                
      
                        </ContentTemplate>
                    </asp:UpdatePanel>

                      <br />
                              <table>
                        <tr>
                            <td style="width: 65px">
                                <asp:Button ID="btnAgregarFoto" runat="server" Text="Agregar" CssClass="boton" onMouseOver="javascript:this.style.cursor='hand';"
                                    ToolTip="Agregar Fotografía" OnClick="btnAgregarFoto_Click" ValidationGroup="AgregarFoto" />
                            </td>
                            <td>
                                <asp:Button ID="btnCancelarFoto" runat="server" Text="Cancelar" CssClass="boton"
                                    onMouseOver="javascript:this.style.cursor='hand';" ToolTip="Cancelar" />
                            </td>
                        </tr>
                    </table>
                  
                </div>
            </div>
        </asp:Panel>


</asp:Content>

