<%@ Page Title="" Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true" CodeFile="frmTipoAsignacion.aspx.cs" Inherits="tipoAsignacion_frmTipoAsignacion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script language="javascript" type="text/javascript" src="../Generales/JScript.js"></script>
        <table >
        <tr>
        <td style="width: 987px">
                                        <table __designer:mapid="6fb" style="width: 982px">
                                            <tr __designer:mapid="6fc">
                                                <td colspan="7" __designer:mapid="6fd" class="titulo" style="height: 17px">
                                                    Tipo de Asignación</td>
                                            </tr>
                                            <tr __designer:mapid="6fc">
                                                <td colspan="7" __designer:mapid="6fd" class="subtitulo" style="height: 17px">
                                                    Criterios de Búsqueda</td>
                                            </tr>
                                            <tr __designer:mapid="6ff">
                                                <td class="der" __designer:mapid="700">
                                                    Fecha:</td>
                                                <td __designer:mapid="701" style="width: 30px">
                                                    <asp:Label ID="lblFecha" runat="server" CssClass="texto"></asp:Label>
                                                </td>
                                                <td __designer:mapid="701" colspan="2">
                                                    Tipo de&nbsp; Asignación ¿capturado?</td>
                                                <td class="izq" __designer:mapid="704" colspan="3">
                                                    <asp:RadioButtonList ID="rblTipoAsignacion" runat="server" 
                                                        RepeatDirection="Horizontal" Width="296px">
                                                        <asp:ListItem Selected="True" Value="0">Todos</asp:ListItem>
                                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                                        <asp:ListItem Value="2">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr __designer:mapid="6ff">
                                                <td class="der" __designer:mapid="700">
                                                    Tipo de Asignación:</td>
                                                <td __designer:mapid="701" colspan="6">
                                                    <asp:DropDownList ID="ddlTipoAsignacion" runat="server" CssClass="texto" OnSelectedIndexChanged="ddlServicio_SelectedIndexChanged"
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr __designer:mapid="6ff">
                                                <td class="der" __designer:mapid="700" style="height: 22px">
                                                    Apellido Paterno:
                                                </td>
                                                <td __designer:mapid="701" colspan="2" style="height: 22px">
                                                    <asp:TextBox ID="txbPaterno" runat="server" MaxLength="30" onblur="javascript:onLosFocus(this)"
                                                        onfocus="javascript:onFocus(this)" onkeypress="return validar(event)" onchange="this.value=quitaacentos(this.value)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" CssClass="texto" 
                                                        Width="150px"></asp:TextBox>
                                                </td>
                                                <td class="der" __designer:mapid="703" style="height: 22px">
                                                    Apellido Materno:
                                                </td>
                                                <td class="izq" __designer:mapid="704" style="height: 22px">
                                                    <asp:TextBox ID="txbMaterno" runat="server" MaxLength="30"  onblur="javascript:onLosFocus(this)"
                                                        onfocus="javascript:onFocus(this)" onkeypress="return validar(event)" onchange="this.value=quitaacentos(this.value)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" CssClass="texto" 
                                                        Width="150px"></asp:TextBox>
                                                </td>
                                                <td class="der" __designer:mapid="706" style="height: 22px">
                                                    Nombre:
                                                </td>
                                                <td __designer:mapid="707" style="height: 22px;">
                                                    <asp:TextBox ID="txbNombre" runat="server" MaxLength="30" 
                                                        CssClass="textbox" onblur="javascript:onLosFocus(this)"
                                                        onfocus="javascript:onFocus(this)" onkeypress="return validar(event)" onchange="this.value=quitaacentos(this.value)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" Width="150px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr __designer:mapid="729">
                                                <td class="der" __designer:mapid="72a">
                                                    N° Empleado:
                                                </td>
                                                <td __designer:mapid="72b" colspan="2">
                                                    <asp:TextBox ID="txbNumero" runat="server" 
                                                        onkeypress="return nvalidar(event)" MaxLength="10"
                                                        CssClass="textbox" onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" Width="150px"></asp:TextBox>
                                                </td>
                                                <td class="der" __designer:mapid="72d">
                                                    RFC:</td>
                                                <td __designer:mapid="72e">
                                                    <asp:TextBox ID="txbRFC" runat="server" MaxLength="30"  onblur="javascript:onLosFocus(this)"
                                                        onfocus="javascript:onFocus(this)" 
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" CssClass="texto" 
                                                        Width="150px"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="revRFC" runat="server" ControlToValidate="txbRfc"
                                                        CssClass="Validator" ErrorMessage="RFC Inválido." SetFocusOnError="True" ValidationExpression="^([A-Z|a-z|&]{3}\d{2}((0[1-9]|1[012])(0[1-9]|1\d|2[0-8])|(0[13456789]|1[012])(29|30)|(0[13578]|1[02])31)|([02468][048]|[13579][26])0229)(\w{2})([A|a|0-9]{1})$|^([A-Z|a-z]{4}\d{2}((0[1-9]|1[012])(0[1-9]|1\d|2[0-8])|(0[13456789]|1[012])(29|30)|(0[13578]|1[02])31)|([02468][048]|[13579][26])0229)((\w{2})([A|a|0-9]{1})){0,3}$"
                                                        ValidationGroup="Datos">*
                                                    </asp:RegularExpressionValidator>
                                                </td>
                                                <td class="der" __designer:mapid="730">
                                                </td>
                                                <td __designer:mapid="731">
                                                </td>
                                            </tr>
                                        
                                            <tr __designer:mapid="73b">
                                                <td class="der" __designer:mapid="73c">
                                                    Servicio:
                                                </td>
                                                <td colspan="6" __designer:mapid="73d">
                                                    <asp:DropDownList ID="ddlServicio" runat="server" CssClass="texto" OnSelectedIndexChanged="ddlServicio_SelectedIndexChanged"
                                                        AutoPostBack="True" >
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr __designer:mapid="73f">
                                                <td class="der" __designer:mapid="740">
                                                    Instalación:
                                                </td>
                                                <td colspan="6" __designer:mapid="741">
                                                    <asp:DropDownList ID="ddlInstalacion" runat="server" CssClass="texto" 
                                                        Enabled="False">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr __designer:mapid="73f">
                                                <td class="der" __designer:mapid="740">
                                                    &nbsp;</td>
                                                <td colspan="6" __designer:mapid="741" class="der">
                                        <asp:Button ID="btnBuscarReemplazar" runat="server" Text="Buscar" CssClass="boton" ToolTip="Buscar"
                                            onMouseOver="javascript:this.style.cursor='hand';" 
                                            OnClick="btnBuscarReemplazar_Click" />
                                                &nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnNuevaBusquedaReemplazar" runat="server" Text="Nueva Búsqueda" CssClass="boton"
                                            onMouseOver="javascript:this.style.cursor='hand';" 
                                            ToolTip="Nueva Búsqueda" OnClick="btnNuevaBusquedaReemplazar_Click" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                            </tr>
                                        </table>
        </td>
        </tr>
            <tr>
                <td style="width: 987px">
              
                        <asp:UpdatePanel ID="updGrid" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hfRenglon" runat="server" EnableViewState="False" />
                                <table style="width:100%;">
                                    <tr>
                                        <td class="subtitulo" colspan="2">
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" CssClass="negritas" 
                                                Text="No. Registros:"></asp:Label>
                                            &nbsp;&nbsp;
                                            <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;
                                            </td>
                                    </tr>
                                </table>
                                <table style="width:100%;">
                                    <tr>
                                        <td class="divError">
                                            <div ID="divErrorReemplazo"  style="width: 100%" runat="server" class="divError" visible="false">
                                    <table style="width: 100%">
                                        <tr>
                                            <td width="100%" align="left">
                                                  <asp:Label ID="lblError" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                           
                                           
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="cen">
                                            <table style="width:100%;">
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="imgAtrasReemplazar" runat="server" 
                                                            ImageUrl="~/Imagenes/rewind-icon.png" OnClick="imgAtrasReemplazar_Click" 
                                                            ToolTip="Atrás" />
                                                    </td>
                                                    <td>
                                                        <asp:GridView ID="grvEmpleadoTipoAsignacion" runat="server" AllowSorting="True" 
                                                            AutoGenerateColumns="False"  CssClass="texto" 
                                                            ForeColor="#333333" onrowcommand="gvReemplazos_RowCommand" 
                                                            Width="100%" 
                                                            onpageindexchanging="gvReemplazos_PageIndexChanging" AllowPaging="True" 
                                                            PageSize="20" onrowdatabound="grvEmpleadoTipoAsignacion_RowDataBound" >
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="No.">
                                                                <ItemTemplate>
                                                                       <%# Container.DataItemIndex + 1 %>

                                               
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                               
                                                              <asp:TemplateField HeaderText="INTEGRANTE">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblIntegrante" runat=server Text='<%# Eval("empCompleto") %>'>
                                                </asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                     
                                                                   
                                                                     
                                              
                                                                <asp:TemplateField HeaderText="NO. EMPLEADO">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblInstalacion" runat="server" Text='<%# Eval("empNumero") %>'>
                                                </asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="JERARQUÍA">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPaterno" runat="server" Text='<%# Eval("jerDescripcion") %>'>
                                                </asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="TIPO ASIGNACION" Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMaterno" runat="server" Text='<%# Eval("tiaDescripcion") %>'>
                                                </asp:Label>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                                   <asp:TemplateField HeaderText="TIPO ASIGNACION">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="ddlAsignacionTabla"  runat="server" OnSelectedIndexChanged="seleccionaTipoAsignacion"
                                                                           CssClass=texto AutoPostBack="True" >
                                                </asp:DropDownList>
                                                </ItemTemplate>
                                                                     
                                                </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Detalles">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="cmdDetalle" runat="server" CommandName = "cmdDetalle"
                                                                         ImageUrl="~/Imagenes/Download.png" CommandArgument='<%# Container.DataItemIndex %>' >
                                                </asp:ImageButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                         
                                                            </Columns>
                                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
                                                            <EmptyDataRowStyle Font-Names="Verdana" Font-Size="Medium" ForeColor="Navy" 
                                                                HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <FooterStyle BackColor="#727272" Font-Bold="True" ForeColor="White" />
                                                            <PagerSettings Visible="False" />
                                                            <PagerStyle BackColor="#727272" ForeColor="White" HorizontalAlign="Center" />
                                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                            <HeaderStyle BackColor="#727272" Font-Bold="True" ForeColor="White" />
                                                            <EditRowStyle BackColor="#999999" />
                                                            <AlternatingRowStyle BackColor="White" ForeColor="#727272" />
                                                        </asp:GridView>
                                                        <strong style="text-align: center">
                                                        <asp:Label ID="lblPagina" runat="server" Text="0"></asp:Label>
                                                         &nbsp;de
                                                        <asp:Label ID="lblPaginas" runat="server" Text="0"></asp:Label>
                                                        </strong>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="imgAdelanteReemplazar" runat="server" 
                                                            ImageUrl="~/Imagenes/forward-icon.png" OnClick="imgAdelanteReemplazar_Click" 
                                                            ToolTip="Siguiente" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="cen">
                                            <asp:Button ID="btnGuardar" runat="server" CssClass="boton" 
                                                onclick="btnGuardar_Click" Text="Guardar" ValidationGroup="agregarAsignacion" 
                                                Width="100px" />
                                                <asp:Button ID="btnCancelar" runat="server" CssClass="boton" 
                                            Text="Cancelar" 
                                                ValidationGroup="agregarAsignacion" Width="100px" 
                                                onclick="btnCancelar_Click" />
                                            <asp:Button ID="btnReporte" runat="server" CssClass="boton" 
                                             Text="Generar Reporte" 
                                                ValidationGroup="agregarAsignacion" Width="100px" 
                                                onclick="btnReporte_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                                            <asp:HiddenField ID="hfColumna" runat="server" Value="-1" />



      
  






                       

                            </ContentTemplate>
                        </asp:UpdatePanel>
                   
          
         
        </td>
      
    </tr>
     </table> 

 
  <asp:ModalPopupExtender ID="popDetalle" runat="server" 
                  BackgroundCssClass="modalBackground" PopupControlID="pnlDetalle" 
                  TargetControlID="btnDetalle" >
              </asp:ModalPopupExtender>


<asp:Panel ID="pnlDetalle" runat="server" DefaultButton="btnDetalle">
<div style="background-repeat: repeat; background-image: url(./../Imagenes/line.png);margin: 30px auto 30px auto; border: outset 2px Black;">
<asp:UpdatePanel ID="upnDetalle" runat="server">
<ContentTemplate>

                  <table width="750px" style="width: 680px; padding-left:1em; padding-right:1em; padding-top:iem; padding-bottom:1em;">
                  <tr>
                  <td colspan="4">&nbsp;
                  </td>
                  </tr>
                  <tr>
                     <td colspan="4" class="subtitulo">
                          Detalles del Integrante
                     </td>
                  </tr>
                  <tr>
                     <td style="width: 234px" class="der">Integrante:</td><td class="izq" colspan="3">
                      <asp:Label ID="lblNombre" runat="server" CssClass="textbox" Text="Label"></asp:Label>
                     </td>
                   </tr>
                   <tr>
                            <td style="width: 234px" class="der">
                                No. Empleado:</td><td class="izq" colspan="3">
                                    <asp:Label ID="lblNumeroEmpleado" runat="server" CssClass="textbox" 
                                        Text="Label"></asp:Label>
                                </td></tr><tr><td style="width: 234px; height: 17px;" class="der">Jerarquía:</td>
                                <td class="izq" colspan="3" style="height: 17px">
                                    <asp:Label ID="lblJerarquia" runat="server" CssClass="textbox" Text="Label"></asp:Label>
                                </td>
                                </tr><tr><td style="width: 234px" class="der">LOC:</td>
                                <td style="width: 361px" class="izq">
                                    <asp:Label ID="lblLoc" runat="server" CssClass="textbox" Text="Label"></asp:Label>
                          </td>
                                <td style="width: 148px">&nbsp;</td><td style="width: 155px">&nbsp;</td></tr>
                      <tr>
                          <td class="der" style="width: 234px">
                              Sexo:</td>
                          <td class="izq" style="width: 361px">
                              <asp:Label ID="lblSexo" runat="server" CssClass="textbox" Text="Label"></asp:Label>
                          </td>
                          <td style="width: 148px">
                              &nbsp;</td>
                          <td style="width: 155px">
                              &nbsp;</td>
                      </tr>
                      <tr>
                          <td class="der" style="width: 234px">
                              &nbsp;</td>
                          <td class="izq" style="width: 361px">
                              &nbsp;</td>
                          <td style="width: 148px">
                              &nbsp;</td>
                          <td style="width: 155px">
                              &nbsp;</td>
                      </tr>
                      <tr>
                          <td class="der" style="width: 234px">
                              Zona:</td>
                          <td class="izq" colspan="3">
                              <asp:Label ID="lblZona" runat="server" CssClass="textbox" Text="Label"></asp:Label>
                          </td>
                      </tr>
                      <tr>
                          <td class="der" style="width: 234px">
                              Estación:</td>
                          <td class="izq" colspan="3">
                              <asp:Label ID="lblEstacion" runat="server" CssClass="textbox"></asp:Label>
                          </td>
                      </tr>
                      <tr>
                            <td style="width: 234px" class="der">Servicio:</td>
                            <td 
                                class="izq" colspan="3">
                            <asp:Label ID="lblServicio" runat="server" CssClass="textbox" Text="Label"></asp:Label>
                            </td>
                            </tr><tr>
                            <td style="width: 234px" class="der">
                                Instalación:</td><td class="izq" colspan="3">
                                <asp:Label ID="lblInstalacion" runat="server" CssClass="textbox" Text="Label"></asp:Label>
                            </td></tr><tr>
                            <td class="der" 
                                style="width: 234px">Estado:</td><td class="izq" colspan="3">
                                <asp:Label ID="lblEstado" runat="server" CssClass="textbox" Text="Label"></asp:Label>
                            </td></tr><tr>
                            <td class="der" style="width: 234px; height: 17px;">Período </td>
                            <td class="izq" style="height: 17px; width: 361px;">
                                <asp:Label ID="lblInicioAsignacion" runat="server" CssClass="textbox" 
                                    Text="Label"></asp:Label>
                            </td>
                            <td class="der" style="height: 17px; width: 148px;">
                                al</td>
                            <td class="izq" style="height: 17px">
                                <asp:Label ID="lblFinAsignacion" runat="server" CssClass="textbox" Text="Label"></asp:Label>
                            </td>
                            </tr>
                      <tr>
                          <td class="der" style="width: 234px; height: 17px;">
                              Función:</td>
                          <td class="izq" style="height: 17px; width: 361px;">
                              <asp:Label ID="lblFuncion" runat="server" CssClass="textbox" Text="Label"></asp:Label>
                          </td>
                          <td class="der" style="height: 17px; width: 148px;">
                              &nbsp;</td>
                          <td class="izq" style="height: 17px">
                              &nbsp;</td>
                      </tr>
                      <tr><td style="width: 234px; height: 17px;" class="der">&nbsp;</td>
                                <td class="izq" style="height: 17px; width: 361px;">
                                    &nbsp;</td>
                                <td class="der" style="height: 17px; width: 148px;">
                                    &nbsp;</td>
                                <td class="izq" style="height: 17px">
                                    &nbsp;</td>
                            </tr>
                      <tr>
                          <td class="der" style="width: 234px; height: 17px;">
                              Observaciones:</td>
                          <td class="izq" style="height: 17px; " colspan="3">
                              <asp:Label ID="lblObservaciones" runat="server" CssClass="textbox" Text="Label"></asp:Label>
                          </td>
                      </tr>
                        
                            <tr>
                                <td class="der" style="width: 234px">
                                    Período Incidencia:</td>
                                <td class="izq" style="width: 361px">
                                    <asp:Label ID="lblInicioIncidencia" runat="server" CssClass="textbox" 
                                        Text="Label"></asp:Label>
                                </td>
                                <td class="der" style="width: 148px">
                                    al</td>
                                <td class="izq" style="width: 155px">
                                    <asp:Label ID="lblFinInicidencia" runat="server" CssClass="textbox" 
                                        Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 234px">
                                    Incidencia Revisada:</td>
                                <td class="izq" style="width: 361px">
                                    <asp:Label ID="lblIncidenciaRevisada" runat="server" CssClass="textbox" 
                                        Text="Label"></asp:Label>
                                </td>
                                <td class="der" style="width: 148px">
                                    &nbsp;</td>
                                <td class="izq" style="width: 155px">
                                    &nbsp;</td>
                      </tr>
                      <tr>
                          <td class="der" style="width: 234px; height: 18px;">
                          </td>
                          <td class="izq" style="width: 361px; height: 18px;">
                          </td>
                          <td class="der" style="width: 148px; height: 18px;">
                          </td>
                          <td class="izq" style="width: 155px; height: 18px;">
                          </td>
                      </tr>
                      <tr>
                          <td class="der" style="width: 234px">
                              Tipo de Asignación:</td>
                          <td class="izq" colspan="3">
                              <asp:Label ID="lblTipoAsignacion" runat="server" CssClass="textbox" 
                                  Text="Label"></asp:Label>
                          </td>
                      </tr>
                      <tr>
                          <td class="der" style="width: 234px">
                              Usuario que Registró:</td>
                          <td class="izq" colspan="3">
                              <asp:Label ID="lblUsuario" runat="server" CssClass="textbox" Text="Label"></asp:Label>
                          </td>
                      </tr>
                      <tr>
                          <td class="der" style="width: 234px">
                              Fecha y Hora:</td>
                          <td class="izq" style="width: 361px">
                              <asp:Label ID="lblFechaHora" runat="server" CssClass="textbox" Text="Label"></asp:Label>
                          </td>
                          <td class="der" style="width: 148px">
                              &nbsp;</td>
                          <td class="izq" style="width: 155px">
                              &nbsp;</td>
                      </tr>
                            <tr>
                                <td style="width: 234px">
                                    &nbsp;</td>
                                <td style="width: 361px">
                                    &nbsp;</td>
                                <td style="width: 148px">
                                    &nbsp;</td>
                                <td style="width: 155px">
                                 <asp:Button ID="btnCerrar" runat="server" CssClass="boton" Text="Cerrar" 
                        Width="100px" onclick="btnCerrar_Click" ValidationGroup="agregarAsignacion" />   &nbsp;</td>
                            </tr>
                        
                            </table>
                              </ContentTemplate>
                            </asp:UpdatePanel>
                  
                  
            </div>

        </asp:Panel>
         <asp:Button ID="btnDetalle" runat="server" Text="" style="visibility:hidden;" />

      

   
</asp:Content>




