<%@ Page Title="" Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true" CodeFile="frmReemplazo.aspx.cs" Inherits="Reemplazos_frmReemplazo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script language="javascript" type="text/javascript" src="../Generales/JScript.js"></script>
        <table>
        <tr>
        <td>
                                        <table class="tamanio" __designer:mapid="6fb">
                                            <tr __designer:mapid="6fc">
                                                <td colspan="6" __designer:mapid="6fd" class="titulo" style="height: 17px">
                                                    Reemplazos</td>
                                            </tr>
                                            <tr __designer:mapid="6fc">
                                                <td colspan="6" __designer:mapid="6fd" class="subtitulo" style="height: 17px">
                                                    Búsqueda de integrantes</td>
                                            </tr>
                                            <tr __designer:mapid="6ff">
                                                <td class="der" __designer:mapid="700">
                                                    Fecha:</td>
                                                <td __designer:mapid="701">
                                                    <asp:Label ID="lblFecha" runat="server" CssClass="texto"></asp:Label>
                                                </td>
                                                <td class="der" __designer:mapid="703">
                                                    &nbsp;</td>
                                                <td class="izq" __designer:mapid="704">
                                                    &nbsp;</td>
                                                <td class="der" __designer:mapid="706">
                                                    &nbsp;</td>
                                                <td __designer:mapid="707">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr __designer:mapid="6ff">
                                                <td class="der" __designer:mapid="700">
                                                    Apellido Paterno:
                                                </td>
                                                <td __designer:mapid="701">
                                                    <asp:TextBox ID="txbPaternoReemplazar" runat="server" MaxLength="30" onblur="javascript:onLosFocus(this)"
                                                        onfocus="javascript:onFocus(this)" onkeypress="return validar(event)" onchange="this.value=quitaacentos(this.value)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" CssClass="texto" 
                                                        Width="150px"></asp:TextBox>
                                                </td>
                                                <td class="der" __designer:mapid="703">
                                                    Apellido Materno:
                                                </td>
                                                <td class="izq" __designer:mapid="704">
                                                    <asp:TextBox ID="txbMaternoReemplazar" runat="server" MaxLength="30"  onblur="javascript:onLosFocus(this)"
                                                        onfocus="javascript:onFocus(this)" onkeypress="return validar(event)" onchange="this.value=quitaacentos(this.value)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" CssClass="texto" 
                                                        Width="150px"></asp:TextBox>
                                                </td>
                                                <td class="der" __designer:mapid="706">
                                                    Nombre:
                                                </td>
                                                <td __designer:mapid="707">
                                                    <asp:TextBox ID="txbNombreReemplazar" runat="server" MaxLength="30" 
                                                        CssClass="textbox" onblur="javascript:onLosFocus(this)"
                                                        onfocus="javascript:onFocus(this)" onkeypress="return validar(event)" onchange="this.value=quitaacentos(this.value)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" Width="150px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr __designer:mapid="729">
                                                <td class="der" __designer:mapid="72a">
                                                    N° Empleado:
                                                </td>
                                                <td __designer:mapid="72b">
                                                    <asp:TextBox ID="txbNumeroReemplazar" runat="server" 
                                                        onkeypress="return nvalidar(event)" MaxLength="10"
                                                        CssClass="textbox" onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" Width="150px"></asp:TextBox>
                                                </td>
                                                <td class="der" __designer:mapid="72d">
                                                    &nbsp;</td>
                                                <td __designer:mapid="72e">
                                                    &nbsp;</td>
                                                <td class="der" __designer:mapid="730">
                                                </td>
                                                <td __designer:mapid="731">
                                                </td>
                                            </tr>
                                        
                                            <tr __designer:mapid="73b">
                                                <td class="der" __designer:mapid="73c">
                                                    Servicio:
                                                </td>
                                                <td colspan="5" __designer:mapid="73d">
                                                    <asp:DropDownList ID="ddlServicioReemplazar" runat="server" CssClass="texto" OnSelectedIndexChanged="ddlServicioReemplazar_SelectedIndexChanged"
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr __designer:mapid="73f">
                                                <td class="der" __designer:mapid="740">
                                                    Instalación:
                                                </td>
                                                <td colspan="5" __designer:mapid="741">
                                                    <asp:DropDownList ID="ddlInstalacionReemplazar" runat="server" CssClass="texto" 
                                                        Enabled="False">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr __designer:mapid="73f">
                                                <td class="der" __designer:mapid="740">
                                                    &nbsp;</td>
                                                <td colspan="5" __designer:mapid="741" class="der">
                                        <asp:Button ID="btnNuevaBusquedaReemplazar" runat="server" Text="Nueva Búsqueda" CssClass="boton"
                                            onMouseOver="javascript:this.style.cursor='hand';" 
                                            ToolTip="Nueva Búsqueda" OnClick="btnNuevaBusquedaReemplazar_Click" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnBuscarReemplazar" runat="server" Text="Buscar" CssClass="boton" ToolTip="Buscar"
                                            onMouseOver="javascript:this.style.cursor='hand';" 
                                            OnClick="btnBuscarReemplazar_Click" />
                                                </td>
                                            </tr>
                                        </table>
        </td>
        </tr>
            <tr>
                <td>
              
                        <asp:UpdatePanel ID="updGrid" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hfRenglon" runat="server" EnableViewState="False" />
                                <table style="width:100%;">
                                    <tr>
                                        <td class="subtitulo" colspan="2">
                                            Integrantes que se Reemplazarán</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" CssClass="negritas" 
                                                Text="Integrantes a Reemplazar:"></asp:Label>
                                            &nbsp;&nbsp;
                                            <asp:Label ID="lblAReemplazar" runat="server" Text="Label"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" CssClass="negritas" Text="Reemplazos:"></asp:Label>
                                            &nbsp;&nbsp;
                                            <asp:Label ID="lblNoReemplazos" runat="server" Text="Label"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table style="width:100%;">
                                    <tr>
                                        <td class="divError" colspan="2">
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
                                        <td colspan="2">
                                            <asp:Table ID="Table1" runat="server" CssClass="titulo" Width="873px">
                                                <asp:TableHeaderRow CssClass="texto">
                                                    <asp:TableHeaderCell>
                                INTEGRANTE A REEMPLAZAR
                                </asp:TableHeaderCell>
                                                    <asp:TableHeaderCell>
                                REEMPLAZO
                                </asp:TableHeaderCell>
                                                </asp:TableHeaderRow>
                                            </asp:Table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="cen">
                                            <table style="width:100%;">
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="imgAtrasReemplazar" runat="server" 
                                                            ImageUrl="~/Imagenes/rewind-icon.png" OnClick="imgAtrasReemplazar_Click" 
                                                            ToolTip="Atrás" />
                                                    </td>
                                                    <td>
                                                        <asp:GridView ID="gvReemplazos" runat="server" AllowSorting="True" 
                                                            AutoGenerateColumns="False" CellPadding="4" CssClass="texto" 
                                                            ForeColor="#333333" onrowcommand="gvReemplazos_RowCommand" 
                                                            OnRowUpdating="gvReemplazos_RowUpdating" Width="100%" 
                                                            onpageindexchanging="gvReemplazos_PageIndexChanging" AllowPaging="True" 
                                                            PageSize="20" >
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNumero" runat="server" Text='<%# Eval("intContador") %>'>
                                                </asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="SERVICIO">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblServicio" runat="server" Text='<%# Eval("serDescripcion") %>'>
                                                </asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="INSTALACIÓN">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblInstalacion" runat="server" Text='<%# Eval("insNombre") %>'>
                                                </asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PATERNO">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPaterno" runat="server" Text='<%# Eval("empPaterno") %>'>
                                                </asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="MATERNO">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMaterno" runat="server" Text='<%# Eval("empMaterno") %>'>
                                                </asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="NOMBRE">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("empNombre") %>'>
                                                </asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="D">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imbDetalle" runat="server" CommandArgument="1" 
                                                                            CommandName="Update" Height="20px" ImageUrl="~/Imagenes/informacion.png" 
                                                                            ToolTip="Detalles Asignación" Width="20px" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PATERNO">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPaternoReemplaza" runat="server" 
                                                                            Text='<%# Eval("empPaternoReemplazo") %>'>
                                                </asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="MATERNO">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMaternoReemplaza" runat="server" 
                                                                            Text='<%# Eval("empMaternoReemplazo") %>'>
                                                </asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="NOMBRE">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNombreReemplaza" runat="server" 
                                                                            Text='<%# Eval("empNombreReemplazo") %>'>
                                                </asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="BR">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imbBuscar" runat="server" CommandArgument="1" 
                                                                            CommandName="cmdBuscar" ImageUrl="~/Imagenes/Download.png" 
                                                                            ToolTip="Buscar Reemplazo" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PA">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imbPeriodo" runat="server" CommandName="cmdPeriodo" 
                                                                            ImageUrl="~/Imagenes/cal01d.ico" ToolTip="Definir Periodo" />
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
                                                        <asp:Label ID="lblPaginaReemplazos" runat="server" Text="0"></asp:Label>
                                                         &nbsp;de
                                                        <asp:Label ID="lblPaginasReemplazos" runat="server" Text="0"></asp:Label>
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
                                        <td class="der">
                                            <asp:Button ID="btnGuardar" runat="server" CssClass="boton" 
                                                onclick="btnGuardar_Click" Text="Guardar" ValidationGroup="agregarAsignacion" 
                                                Width="100px" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnCancelarReemplazo" runat="server" CssClass="boton" 
                                                onclick="btnCancelarReemplazo_Click" Text="Cancelar" 
                                                ValidationGroup="agregarAsignacion" Width="100px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
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
                     <td style="width: 234px" class="der">Nombre:</td><td class="izq" colspan="3">
                      <asp:Label ID="lblNombre" runat="server" CssClass="textbox" Text="Label"></asp:Label>
                     </td>
                   </tr>
                   <tr>
                            <td style="width: 234px" class="der">
                                No. Empleado:</td><td class="izq" colspan="3">
                                    <asp:Label ID="lblNumeroEmpleado" runat="server" CssClass="textbox" 
                                        Text="Label"></asp:Label>
                                </td></tr><tr><td class="der" style="width: 234px">Cargo:</td>
                                <td style="width: 361px" class="izq">
                                    <asp:Label ID="lblCargo" runat="server" CssClass="textbox" Text="Label"></asp:Label>
                                </td><td class="der" style="width: 148px">LOC:</td>
                                <td style="width: 155px" class="izq">
                                    <asp:Label ID="lblLoc" runat="server" CssClass="textbox" Text="Label"></asp:Label>
                                </td></tr><tr><td style="width: 234px; height: 17px;" class="der">Jerarquía:</td>
                                <td class="izq" colspan="3" style="height: 17px">
                                    <asp:Label ID="lblJerarquia" runat="server" CssClass="textbox" Text="Label"></asp:Label>
                                </td>
                                </tr><tr><td style="width: 234px" class="der">&nbsp;</td>
                                <td style="width: 361px" class="izq">&nbsp;</td>
                                <td style="width: 148px">&nbsp;</td><td style="width: 155px">&nbsp;</td></tr><tr>
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
                                style="width: 234px">Función:</td><td class="izq" colspan="3">
                                <asp:Label ID="lblFuncion" runat="server" CssClass="textbox" Text="Label"></asp:Label>
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
                            </tr><tr><td style="width: 234px; height: 17px;" class="der">Horario:</td>
                                <td class="izq" style="height: 17px; width: 361px;">
                                    <asp:Label ID="lblHorario" runat="server" CssClass="textbox" Text="Label"></asp:Label>
                                </td>
                                <td class="izq" style="height: 17px; width: 148px;">
                                    </td>
                                <td class="izq" style="height: 17px">
                                    </td>
                            </tr><tr>
                            <td style="width: 234px" class="der">Fecha de Inicio Horario:</td>
                                <td class="izq" style="width: 361px">
                                    <asp:Label ID="lblInicioHorario" runat="server" CssClass="textbox" Text="Label"></asp:Label>
                                </td>
                                <td class="der" style="width: 148px">
                                    al</td>
                                <td class="izq">
                                    <asp:Label ID="lblFinHorario" runat="server" CssClass="textbox" Text="Label"></asp:Label>
                                </td>
                            </tr><tr>
                            <td style="width: 234px" class="der">
                                &nbsp;</td><td style="width: 361px" class="izq">&nbsp;</td>
                            <td style="width: 148px">&nbsp;</td><td style="width: 155px">&nbsp;</td></tr>
                        
                            <tr>
                                <td class="der" style="width: 234px">
                                    Tipo de Incidencia:</td>
                                <td class="izq" colspan="3">
                                    <asp:Label ID="lblIncidencia" runat="server" CssClass="textbox" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="der" style="width: 234px">
                                    Período:</td>
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

          <asp:ModalPopupExtender ID="popBusqueda" runat="server" 
                  BackgroundCssClass="modalBackground" PopupControlID="panBusqueda" 
                  TargetControlID="btnBusqueda" >
              </asp:ModalPopupExtender>


<asp:Panel ID="panBusqueda" runat="server" DefaultButton="btnBusqueda">
<div style="background-repeat: repeat; background-image: url('../Imagenes/line.png'); margin: 30px auto 30px auto; border: outset 2px Black;width: 900px;"  >
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
 <table class="tamanio">
                    <tr runat="server" id="trParametros">
                        <td>
                            <table class="tamanio" width="900px">
                                <tr>
                                    <td class="titulo">
                                        Búsqueda
                                    </td>
                                </tr>
                                <tr>
                                    <td class="subtitulo">
                                        Criterios de Búsqueda
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table class="tamanio">
                                            <tr>
                                                <td colspan="6">
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="der">
                                                    Apellido Paterno:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txbPaterno" runat="server" MaxLength="30" onblur="javascript:onLosFocus(this)"
                                                        onfocus="javascript:onFocus(this)" onkeypress="event.returnValue=validar(event)" onchange="this.value=quitaacentos(this.value)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" CssClass="texto" Width="150px"></asp:TextBox>
                                                </td>
                                                <td class="der">
                                                    Apellido Materno:
                                                </td>
                                                <td class="izq">
                                                    <asp:TextBox ID="txbMaterno" runat="server" MaxLength="30"  onblur="javascript:onLosFocus(this)"
                                                        onfocus="javascript:onFocus(this)" onkeypress="event.returnValue=validar(event)" onchange="this.value=quitaacentos(this.value)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" CssClass="texto" Width="150px">
                                                    </asp:TextBox>
                                                </td>
                                                <td class="der">
                                                    Nombre:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txbNombre" runat="server" MaxLength="30" CssClass="textbox" onblur="javascript:onLosFocus(this)"
                                                        onfocus="javascript:onFocus(this)" onkeypress="event.returnValue=validar(event)" onchange="this.value=quitaacentos(this.value)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" Width="150px">
                                                    </asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="der">
                                                    Fecha Nacimiento:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txbNacimiento" runat="server" MaxLength="10" onblur="javascript:onLosFocus(this)"
                                                        onkeypress="event.returnValue= validarNoEscritura(event);" onfocus="javascript:onFocus(this)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" CssClass="textbox" Width="125px">
                                                    </asp:TextBox>
                                                    <asp:ImageButton ID="imbNacimiento" runat="server" Height="16px" ImageUrl="~/Imagenes/Calendar.png"
                                                        Width="16px" />
                                                    <asp:CalendarExtender ID="calNacimiento" runat="server" PopupButtonID="imbNacimiento"
                                                        TargetControlID="txbNacimiento">
                                                    </asp:CalendarExtender>
                                                </td>
                                                <td class="der">
                                                    Fecha Alta:
                                                </td>
                                                <td class="izq">
                                                    <asp:TextBox ID="txbIngreso" runat="server" MaxLength="10" onblur="javascript:onLosFocus(this)"
                                                        onkeypress="event.returnValue= validarNoEscritura(event);" onfocus="javascript:onFocus(this)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" CssClass="textbox" Width="125px">
                                                    </asp:TextBox>
                                                    <asp:ImageButton ID="imbIngreso" runat="server" Height="16px" ImageUrl="~/Imagenes/Calendar.png"
                                                        Width="16px" />
                                                    <asp:CalendarExtender ID="calIngreso" runat="server" PopupButtonID="imbIngreso" TargetControlID="txbIngreso">
                                                    </asp:CalendarExtender>
                                                </td>
                                                <td class="der">
                                                    Fecha Baja:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txbBaja" onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)"
                                                        onkeypress="event.returnValue= validarNoEscritura(event);" runat="server" MaxLength="10"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" CssClass="textbox" Width="125px">
                                                    </asp:TextBox>
                                                    <asp:ImageButton ID="imbCaptura" runat="server" Height="16px" ImageUrl="~/Imagenes/Calendar.png"
                                                        Width="16px" />
                                                    <asp:CalendarExtender ID="calCaptura" runat="server" PopupButtonID="imbCaptura" TargetControlID="txbBaja">
                                                    </asp:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="der">
                                                    No Cartilla:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txbCartilla" runat="server" MaxLength="20" CssClass="textbox" onblur="javascript:onLosFocus(this)"
                                                        onfocus="javascript:onFocus(this)" onkeypress="event.returnValue= avalidar(event)" onchange="this.value=quitaacentos(this.value)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" Width="150px">
                                                    </asp:TextBox>
                                                </td>
                                                <td class="der">
                                                    LOC:
                                                </td>
                                                <td class="izq">
                                                    <asp:RadioButtonList ID="rblLOC" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                        <asp:ListItem Selected="True" Value="2">Todos</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td class="der">
                                                    Curso Básico:
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList ID="rblCurso" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">Si</asp:ListItem>
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                        <asp:ListItem Selected="True" Value="2">Todos</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="der">
                                                    N° Empleado:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txbNumero" runat="server" onkeypress="event.returnValue= nvalidar(event)" MaxLength="10"
                                                        CssClass="textbox" onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" Width="150px">
                                                    </asp:TextBox>
                                                </td>
                                                <td class="der">
                                                    CUIP:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txbCuip" runat="server" MaxLength="22" CssClass="textbox" onblur="javascript:onLosFocus(this)"
                                                        onfocus="javascript:onFocus(this)" onkeypress="event.returnValue= avalidar(event)" onchange="this.value=quitaacentos(this.value)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" Width="150px">
                                                    </asp:TextBox>
                                                </td>
                                                <td class="der">
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="der">
                                                    RFC:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txbRfc" runat="server" MaxLength="13" CssClass="textbox" onblur="javascript:onLosFocus(this)"
                                                        onfocus="javascript:onFocus(this)" onkeypress="event.returnValue= avalidar(event)" onKeyUp="this.value=quitaacentos(this.value)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" Width="150px">
                                                    </asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="revRFC" runat="server" ControlToValidate="txbRfc"
                                                        CssClass="Validator" ErrorMessage="RFC Inválido." SetFocusOnError="True" ValidationExpression="^([A-Z|a-z|&]{3}\d{2}((0[1-9]|1[012])(0[1-9]|1\d|2[0-8])|(0[13456789]|1[012])(29|30)|(0[13578]|1[02])31)|([02468][048]|[13579][26])0229)(\w{2})([A|a|0-9]{1})$|^([A-Z|a-z]{4}\d{2}((0[1-9]|1[012])(0[1-9]|1\d|2[0-8])|(0[13456789]|1[012])(29|30)|(0[13578]|1[02])31)|([02468][048]|[13579][26])0229)((\w{2})([A|a|0-9]{1})){0,3}$"
                                                        ValidationGroup="Datos">*
                                                    </asp:RegularExpressionValidator>
                                                </td>
                                                <td class="der">
                                                    CURP:
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txbCurp" runat="server" MaxLength="18" CssClass="textbox" onblur="javascript:onLosFocus(this)"
                                                        onfocus="javascript:onFocus(this)" onkeypress="event.returnValue= avalidar(event)" onKeyUp="this.value=quitaacentos(this.value)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" Width="150px">
                                                    </asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="revCURP" runat="server" ControlToValidate="txbCurp"
                                                        CssClass="Validator" ErrorMessage="CURP Inválida." SetFocusOnError="True" ValidationExpression="^[a-zA-Z]{4}((\d{2}((0[13578]|1[02])(0[1-9]|[12]\d|3[01])|(0[13456789]|1[012])(0[1-9]|[12]\d|30)|02(0[1-9]|1\d|2[0-8])))|([02468][048]|[13579][26])0229)(H|M)(AS|BC|BS|CC|CL|CM|CS|CH|DF|DG|GT|GR|HG|JC|MC|MN|MS|NT|NL|OC|PL|QT|QR|SP|SL|SR|TC|TS|TL|VZ|YN|ZS|SM|NE)([a-zA-Z]{3})([a-zA-Z0-9\s]{1})\d{1}$"
                                                        ValidationGroup="Datos">*
                                                    </asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                        
                                            <tr>
                                                <td class="der">
                                                    Servicio:
                                                </td>
                                                <td colspan="5">
                                                    <asp:DropDownList ID="ddlServicio" runat="server" CssClass="texto" 
                                                        AutoPostBack="True" 
                                                        onselectedindexchanged="ddlServicio_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="der">
                                                    Instalación:
                                                </td>
                                                <td colspan="5">
                                                    <asp:DropDownList ID="ddlInstalacion" runat="server" CssClass="texto" Enabled="False">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="der">
                                        <asp:HiddenField ID="hfBanderaNuevo" runat="server" />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="der">
                                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar Empleados" OnClick="btnBuscar_Click"
                                            CssClass="boton" onMouseOver="javascript:this.style.cursor='hand';" ToolTip="Buscar Empleados"
                                            ValidationGroup="Datos" />
                                        <asp:Button ID="btnNuevaBusqueda" runat="server" Text="Nueva Búsqueda" CssClass="boton"
                                            onMouseOver="javascript:this.style.cursor='hand';" ToolTip="Nueva Búsqueda" OnClick="btnNuevaBusqueda_Click" />
                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="boton" ToolTip="Cancelar"
                                            onMouseOver="javascript:this.style.cursor='hand';" OnClick="btnCancelar_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>
                    <tr runat="server" id="trResultados">
                        <td class="cen">
                            <table class="centro">
                                <tr>
                                    <td align="center">
                                        <strong>Número de registros encontrados:
                                            <asp:Label ID="lblCount" runat="server" Text="0"></asp:Label>
                                        </strong>
                                    </td>
                                </tr>
                                <tr runat="server" id="trGrid">
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <div id="Div1" class="centro">
                                                    <table class="centro">
                                                        <tr>
                                                            <td class="der">
                                                                <asp:ImageButton ID="imgAtras" runat="server" ImageUrl="~/Imagenes/rewind-icon.png"
                                                                    OnClick="imgAtras_Click" ToolTip="Atrás" />
                                                            </td>
                                                            <td class="cen">
                                                                <asp:GridView ID="grvBusqueda" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                                    CssClass="texto" EmptyDataText="No se encontraron resultados." ForeColor="#333333"
                                                                    Width="100%" OnRowUpdating="grvBusqueda_RowUpdating" AllowSorting="True">
                                                                    <Columns>
                                                                        <asp:TemplateField Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblIdEmpleado" runat="server" Text='<%# Eval("idEmpleado") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="No. Empleado">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblNumero" runat="server" Text='<%# Eval("empNumero") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Apellido Paterno">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPaterno" runat="server" Text='<%# Eval("empPaterno") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Apellido Materno">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblMaterno" runat="server" Text='<%# Eval("empMaterno") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Nombre">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("empNombre") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Servicio">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblServicio" runat="server" Text='<%# Eval("serDescripcion") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Instalación">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblInstalacion" runat="server" Text='<%# Eval("insNombre") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="imbSeleccionar" runat="server" CommandName="Update" ImageUrl="~/Imagenes/Download.png"
                                                                                    ToolTip="Consultar" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
                                                                    <EmptyDataRowStyle Font-Names="Verdana" Font-Size="Medium" ForeColor="Navy" HorizontalAlign="Center"
                                                                        VerticalAlign="Middle" />
                                                                    <FooterStyle BackColor="#727272" Font-Bold="True" ForeColor="White" />
                                                                    <PagerStyle BackColor="#727272" ForeColor="White" HorizontalAlign="Center" />
                                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                    <HeaderStyle BackColor="#727272" Font-Bold="True" ForeColor="White" />
                                                                    <EditRowStyle BackColor="#999999" />
                                                                    <AlternatingRowStyle BackColor="White" ForeColor="#727272" />
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
                                                                <asp:ImageButton ID="imgAdelante" runat="server" ImageUrl="~/Imagenes/forward-icon.png"
                                                                    OnClick="imgAdelante_Click" ToolTip="Siguiente" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="der">
                                        <asp:HiddenField ID="hfRedirect" runat="server" />
                                        <asp:HiddenField ID="hfCancel" runat="server" />
                                        <asp:Button ID="btnRegresar" runat="server" CssClass="boton" OnClick="btnRegresar_Click"
                                            Text="Regresar" ToolTip="Regresar" Visible="False" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </td>
                    </tr>
                </table>
                 
</ContentTemplate>
</asp:UpdatePanel>
                  
                  
            </div>

        </asp:Panel>
         <asp:Button ID="btnBusqueda" runat="server" Text="" style="visibility:hidden;" />


          <asp:ModalPopupExtender ID="popPeriodo" runat="server" 
                  BackgroundCssClass="modalBackground" PopupControlID="pnlPeriodo" 
                  TargetControlID="btnPeriodo" >
              </asp:ModalPopupExtender>


<asp:Panel ID="pnlPeriodo" runat="server" DefaultButton="btnPeriodo">
<div style="background-repeat: repeat; background-image: url(./../Imagenes/line.png);margin: 30px auto 30px auto; border: outset 2px Black;">
<asp:UpdatePanel ID="UpdatePanel3" runat="server">
<ContentTemplate>

                  <table width="750px" style="width: 680px; padding-left:1em; padding-right:1em; padding-top:iem; padding-bottom:1em;">
                  <tr>
                  <td colspan="4">&nbsp;
                  </td>
                  </tr>
                  <tr>
                     <td colspan="4" class="subtitulo">
                          PERÍODO DE ASIGNACIÓN DEL REEMPLAZO</td>
                  </tr>
                  <tr>
                     <td class="der" colspan="4">
                                            <div ID="divErrorPeriodo"  style="width: 100%" runat="server" class="divError" visible="false">
                                    <table style="width: 100%">
                                        <tr>
                                            <td width="100%" align="left">
                                                <asp:Label ID="lblErrorPeriodo" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                                               </td>
                   </tr>
                      <tr>
                          <td class="der" style="width: 365px">
                              Nombre:</td>
                          <td class="izq" colspan="3">
                              <asp:Label ID="lblNombrePeriodo" runat="server" CssClass="textbox" Text="Label"></asp:Label>
                          </td>
                      </tr>
                   <tr>
                            <td style="width: 365px" class="der">
                                No. Empleado:</td><td class="izq" colspan="3">
                                    <asp:Label ID="lblNoEmpleadoPeriodo" runat="server" CssClass="textbox" 
                                        Text="Label"></asp:Label>
                                </td></tr><tr><td class="der" style="width: 365px">Cargo:</td>
                                <td style="width: 361px" class="izq">
                                    <asp:Label ID="lblCargoPeriodo" runat="server" CssClass="textbox" Text="Label"></asp:Label>
                                </td><td class="der" style="width: 148px">LOC:</td>
                                <td style="width: 155px" class="izq">
                                    <asp:Label ID="lblLocPeriodo" runat="server" CssClass="textbox" Text="Label"></asp:Label>
                                </td></tr><tr><td style="width: 365px; height: 17px;" class="der">Jerarquía:</td>
                                <td class="izq" colspan="3" style="height: 17px">
                                    <asp:Label ID="lblJerarquiaPeriodo" runat="server" CssClass="textbox" Text="Label"></asp:Label>
                                </td>
                                </tr><tr><td style="width: 365px" class="der">&nbsp;</td>
                                <td style="width: 361px" class="izq">&nbsp;</td>
                                <td style="width: 148px">&nbsp;</td><td style="width: 155px">&nbsp;</td></tr><tr>
                            <td class="der" style="width: 365px; height: 17px;">Fecha de Inicio del Reemplazo </td>
                            <td class="izq" style="height: 17px; width: 361px;">
                                  <asp:TextBox ID="txbInicioPeriodo" onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)"
                                                        onkeypress="event.returnValue= validarNoEscritura(event);" runat="server" MaxLength="10"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" CssClass="textbox" Width="125px">
                                                    </asp:TextBox>
                                                    <asp:ImageButton ID="imbInicioPeriodo" runat="server" Height="16px" ImageUrl="~/Imagenes/Calendar.png"
                                                        Width="16px" />
                                                    <asp:CalendarExtender ID="calInicioPeriodo" runat="server" PopupButtonID="imbInicioPeriodo" TargetControlID="txbInicioPeriodo">
                                                    </asp:CalendarExtender></td> </td>
                            <td class="der" style="height: 17px; width: 148px;">
                                &nbsp;</td>
                            <td class="izq" style="height: 17px">
                                &nbsp;</td>
                            </tr><tr><td style="width: 365px; height: 17px;" class="der">Fecha de Fin del 
                          Reemplazo:</td>
                                <td class="izq" style="height: 17px; width: 361px;">
                                  <asp:TextBox ID="txbFinPeriodo" onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)"
                                                        onkeypress="event.returnValue= validarNoEscritura(event);" runat="server" MaxLength="10"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" CssClass="textbox" Width="125px">
                                                    </asp:TextBox>
                                                    <asp:ImageButton ID="imgFinPeriodo" runat="server" Height="16px" ImageUrl="~/Imagenes/Calendar.png"
                                                        Width="16px" />
                                                    <asp:CalendarExtender ID="calFinPeriodo" runat="server" PopupButtonID="imgFinPeriodo" TargetControlID="txbFinPeriodo">
                                                    </asp:CalendarExtender>  </td>
                                <td class="izq" style="height: 17px; width: 148px;">
                                    </td>
                                <td class="izq" style="height: 17px">
                                    </td>
                            </tr><tr>
                            <td style="width: 365px" class="der">&nbsp;</td>
                                <td class="izq" style="width: 361px">
                                    &nbsp;</td>
                                <td class="der" style="width: 148px">
                                    &nbsp;</td>
                                <td class="izq" style="width: 155px">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 365px">
                                    &nbsp;</td>
                                <td style="width: 361px">
                                    &nbsp;</td>
                                <td style="width: 148px">
                                    <asp:Button ID="btnAgregarPeriodo" runat="server" CssClass="boton" 
                                       Text="Agregar" ValidationGroup="agregarAsignacion" 
                                        Width="100px" onclick="btnAgregarPeriodo_Click" />
                                </td>
                                <td style="width: 155px">
                                 <asp:Button ID="btnCerrarPeriodo" runat="server" CssClass="boton" Text="Cerrar" 
                        Width="100px" ValidationGroup="agregarAsignacion" onclick="btnCerrarPeriodo_Click" />   &nbsp;</td>
                            </tr>
                        
                            </table>
                              </ContentTemplate>
                            </asp:UpdatePanel>
                  
                  
            </div>

        </asp:Panel>
         <asp:Button ID="btnPeriodo" runat="server" Text="" style="visibility:hidden;" />
</asp:Content>

