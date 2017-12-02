<%@ Page Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmDatosLaborales.aspx.cs" Inherits="Personal_frmDatosLaborales" Title="Módulo de Control de Servicios ::: Datos Laborales" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Generales/wucMensaje.ascx" TagName="wucMensaje" TagPrefix="uc1" %>
<%@ Import Namespace="SICOGUA.Entidades" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../Generales/JScript.js"></script>


    <div id="Contenido">
        <asp:HiddenField ID="hfIdEmpleado" runat="server" />
        <table class="tamanio">
            <tr>
             
                <td  colspan="2">
                               <div ID="divErrorAsi"  style="width: 100%" runat="server" class="divError" visible="false">
                                    <table style="width: 100%">
                                        <tr>
                                            <td width="100%" align="left">
                                                <asp:Label ID="lblEmpleadoBaja" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                </td>
            </tr>
            <tr>
                <td class="titulo" colspan="2">
                    Asignaciones
                </td>
            </tr>
            <tr>
                <td class="subtitulo" colspan="2">
                    Datos Generales
                </td>
            </tr>
            <tr>
                <td style="width: 105px;" rowspan="3">
                   
                    <img id="imgFoto" alt="" src="ghFotografia.ashx" 
                        style="height: 112px; width: 98px" /></td>
                <td style="height: 28px">
                   
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td class="der">
                                Empleado:
                            </td>
                            <td class="izq">
                                <asp:Label ID="lblNombreEmpleado" runat="server" Text="-"> </asp:Label>
                            </td>
                            <td class="der">
                                N° Empleado:
                            </td>
                            <td class="izq">
                                <asp:Label ID="lblNumeroEmpleado" runat="server" Text="-"> </asp:Label>
                            </td>
                            <td class="der">
                                CUIP:
                            </td>
                            <td class="izq">
                                <asp:Label ID="lblCUIP" runat="server" Text="-"> </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="der">
                                Fecha de Alta:
                            </td>
                            <td class="izq" colspan="5">
                                <asp:Label ID="lblFechaAlta" runat="server" Text="-"> </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="der">
                                Fecha de Baja:</td>
                            <td class="izq" colspan="5">
                                <asp:Label ID="lblFechaBajas" runat="server" Text="-"></asp:Label>
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

           

        </table>
        <table class="tamanio">
            <tr>
                <td class="subtitulo">
                    Datos de la Asignación
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td class="cen">
                    <asp:UpdatePanel ID="updNivelPuesto" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table width="100%">
                                <tr>
                                    <td class="der">
                                        Jerarquía:
                                    </td>
                                    <td class="izq">
                                        <asp:DropDownList ID="ddlJerarquia" runat="server" CssClass="texto" AutoPostBack="True"
                                            Enabled="False" OnSelectedIndexChanged="ddlJerarquia_SelectedIndexChanged" TabIndex="1">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvJerarquia" runat="server" ErrorMessage="Debe Seleccionar una Jerarquía."
                                            ControlToValidate="ddlJerarquia" Font-Bold="True" SetFocusOnError="True" ValidationGroup="SICOGUA"
                                            Text="*">                                            
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td class="der">
                                        Cargo:
                                    </td>
                                    <td class="izq">
                                        <asp:DropDownList ID="ddlPuesto" runat="server" CssClass="texto" Enabled="False"
                                            TabIndex="2">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvPuesto" runat="server" ErrorMessage="Debe Seleccionar un Cargo."
                                            ControlToValidate="ddlPuesto" Font-Bold="True" SetFocusOnError="True" ValidationGroup="SICOGUA"
                                            Text="*">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td class="der">
                                        &nbsp;</td>
                                    <td class="izq">
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:HiddenField ID="hfIdEmpleadoPuesto" runat="server" Value="0" />
                         <asp:HiddenField ID="hfEliminar" runat="server" Value="0" />
                         <asp:HiddenField ID="hfIdRevision" runat="server" Value="-1" />
                         <asp:HiddenField ID="hfIdRenuncia" runat="server" Value="-1" />
                </td>
            </tr>
             <tr>
             <td style="visibility: hidden">
                         <asp:Button ID="btnAbrirOficioAsig" runat="server" CssClass="boton" Text="" />
                </td>
            <td style="visibility: hidden">
                         <asp:Button ID="btnCrearAsignacion" runat="server" CssClass="boton" Text="" />
                </td>
            </tr>
            </table>
  
        <table class="tamanio">
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td class="subtitulo" colspan="6">
                    Asignación
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <div id="grid">
                        <asp:UpdatePanel ID="updGrid" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grvAsignacion" runat="server" AutoGenerateColumns="False" 
                                    CellPadding="4" OnRowCommand="grvCartilla_RowCommand"
                                    CssClass="texto" ForeColor="#333333" Width="100%" AllowSorting="True" 
                                    OnRowUpdating="grvAsignacion_RowUpdating" 
                                    OnRowDeleting="grvAsignacion_RowDeleting" 
                                    onselectedindexchanged="grvAsignacion_SelectedIndexChanged">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Servicio">
                                            <ItemTemplate>
                                                <asp:Label ID="lblServicio" runat="server" Text='<%# ((clsEntServicio)Eval("Servicio")).serDescripcion %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Instalación">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInstalación" runat="server" Text='<%# ((clsEntInstalacion)Eval("Instalacion")).InsNombre %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Función">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFuncion" runat="server" Text='<%# Eval("funcionAsignacion") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fecha Inicio Comisión">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFechaAlta" runat="server" Text='<%# Eval("FechaIngreso") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fecha Término Comisión">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFechaBaja" runat="server" Text='<%# Eval("FechaBaja") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                                      <asp:TemplateField HeaderText="Detalles">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imbDetalle" runat="server" CommandName="Update" ImageUrl="~/Imagenes/informacion.png" CommandArgument="1" Width="20px" Height="20px"
                                                    ToolTip="Detalles" />
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField >
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imbSeleccionar" runat="server" CommandName="Update" ImageUrl="~/Imagenes/Download.png" CommandArgument="-1" 
                                                    ToolTip="Consultar / Modificar" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField >
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imbConsultar" runat="server" CommandName="Consultar" ImageUrl="~/Imagenes/search-icon.png" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                    ToolTip="Consultar " />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                                 <asp:TemplateField >
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imbEliminar" runat="server"  ImageUrl="~/Imagenes/Symbol-Delete-Mini.png"  
                                                    ToolTip="Eliminar" CommandName="Delete" OnClientClick="if(!confirm('El registro será eliminado al guardar los cambios, ¿Desea continuar?')) return false;" /> 
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
                                            <asp:HiddenField ID="hfColumna" runat="server" Value="-1" />

                                  <table class="tamanio"><tr>
                                      <td style="text-align: right">
                                          &nbsp;</td>
                                      </tr>
                                      <tr>
                                          <td style="text-align: right">
                                              <asp:Button ID="btnAgregarAsignacion" runat="server" CssClass="boton" 
                                                  onMouseOver="javascript:this.style.cursor='hand';" Text="Agregar Asignación" 
                                                  ToolTip="Agregar Asignación" onclick="btnAgregarAsignacion_Click1" />
                                          </td>
                                      </tr>
                                   </table>
                       

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
        </table>
              <table style="margin: 0 auto 0 auto;">
            <tr>
                <td>
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="boton" onMouseOver="javascript:this.style.cursor='hand';"
                        TabIndex="10" OnClick="btnGuardar_Click" ValidationGroup="SICOGUA" ToolTip="Guardar" />
                </td>
                <td>
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="boton" onMouseOver="javascript:this.style.cursor='hand';"
                        TabIndex="11" ToolTip="Cancelar" OnClick="btnCancelar_Click" />
                </td>
                <td>
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="boton" OnClick="btnBuscar_Click"
                        onMouseOver="javascript:this.style.cursor='hand';" TabIndex="12" ToolTip="Buscar" />
                </td>
            </tr>
        </table>
    </div>

   
            <asp:ModalPopupExtender ID="popAsignacion" runat="server" 
                  BackgroundCssClass="modalBackground" PopupControlID="pnlAsignacion" 
                  TargetControlID="btnCrearAsignacion">
              </asp:ModalPopupExtender>

        <asp:Panel ID="pnlAsignacion" runat="server" DefaultButton="btnAgregar">
            <div style="background-color: White; margin: 0 auto 0 auto;" class="nder">
                <div style="background-repeat: repeat; background-image: url(./../Imagenes/line.png);
                    margin: 30px auto 30px auto; border: outset 2px Black;">
                    <asp:UpdatePanel ID="upnAsignacion" runat="server">
                        <ContentTemplate>
                            <table>
                                <tr id="trResultados">
                                    <td colspan="4" class="subtitulo">
                                        Asignación
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <br />
                                        <asp:HiddenField ID="hfRowIndex" runat="server" Value="-1" />
                                        <asp:HiddenField ID="hfOficioFlag" runat="server" Value="-1" />
                                        <asp:HiddenField ID="hfIdEmpleadoAsignacion" runat="server" Value="0" />
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="izq">
                                              <div ID="divErrorAsignacion"  style="width: 100%" runat="server" class="divError" visible="false">
                                    <table style="width: 100%">
                                        <tr>
                                            <td width="100%" align="left">
                                                <asp:Label ID="lblErrorAsignacion" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                    </td>
                                   
                                </tr>
                                <tr>
                                    <td class="der">
                                        Servicio:
                                    </td>
                                    <td class="izq">
                                        <asp:DropDownList ID="ddlServicio" runat="server" AutoPostBack="True" 
                                            CssClass="texto" OnSelectedIndexChanged="ddlServicio_SelectedIndexChanged" 
                                            TabIndex="10">
                                        </asp:DropDownList>
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
                                        Instalación:
                                    </td>
                                    <td class="izq"colspan="3">
                                        <asp:DropDownList ID="ddlInstalacion" runat="server" CssClass="texto" 
                                            Enabled="False" onselectedindexchanged="ddlInstalacion_SelectedIndexChanged" 
                                            TabIndex="11" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="der">
                                        Función</td>
                                    <td class="izq" colspan="3">
                                        <asp:DropDownList ID="ddlFuncion" runat="server" CssClass="texto">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="der" colspan="4">
                                        <cc1:CalendarExtender ID="cleFechaBaja" runat="server" 
                                            PopupButtonID="imbFechaBaja" TargetControlID="txbFechaBaja">
                                        </cc1:CalendarExtender>
                                        <cc1:CalendarExtender ID="cleFechaAlta" runat="server" 
                                            PopupButtonID="imbFechaAlta" TargetControlID="txbFechaAlta">
                                        </cc1:CalendarExtender>
                                        <table style="width:100%; height: 78px;">
                                            <tr>
                                                <td colspan='2'>
                                                    Fecha Inicio de Comisión:
                                                </td>
                                                <td class='izq' colspan='1'>
                                                    <asp:TextBox ID="txbFechaAlta" runat="server" CssClass="textbox" 
                                                        Enabled="False" MaxLength="10" onblur="javascript:onLosFocus(this)" 
                                                        onfocus="javascript:onFocus(this)" 
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" 
                                                        onkeypress="return validarNoEscritura(event);" AutoPostBack="True" 
                                                        ontextchanged="txbFechaAlta_TextChanged"></asp:TextBox>
                                                    <asp:ImageButton ID="imbFechaAlta" runat="server" Enabled="False" Height="18px" 
                                                        ImageUrl="~/Imagenes/Calendar.png" TabIndex="12" Width="18px" 
                                                        onclick="imbFechaAlta_Click" />
                                                 </td>
                                                 <td rowspan='3' colspan='2' align='center'>
                                                           <table>
                                                            <tr class='center'>
                                                            <td>
                                                                <asp:Button ID="btnCargarOficio" runat="server" BackColor="Transparent" Style='cursor: pointer'
                                                                    BorderColor="Transparent" BorderStyle="None" Font-Bold="True" Text="Cargar Oficio"
                                                                    Width="120px" OnClick="btnCargarOficio_Click" />
                                                            </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td colspan="2">
                                                                    <asp:ImageButton ID="imbOficio" runat="server" Height="80px" ImageUrl="~/Imagenes/lm.png"
                                                                        OnClientClick="alert('No hay archivo adjunto.');" OnClick="imbOficio_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                 </td>
                                            </tr>
                                            <tr>
                                             <td colspan='2'>
                                                    Fecha Término de Comisión:
                                             </td>
                                             <td class='izq' colspan='1'>
                                                <asp:TextBox ID="txbFechaBaja" runat="server" 
                                                        CssClass="textbox" MaxLength="10" 
                                                        onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)" 
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" 
                                                        onkeypress="return validarNoEscritura(event);" AutoPostBack="True" 
                                                        ontextchanged="txbFechaBaja_TextChanged"></asp:TextBox>
                                                    <asp:ImageButton ID="imbFechaBaja" runat="server" Height="18px" 
                                                        ImageUrl="~/Imagenes/Calendar.png" Width="18px" 
                                                        onclick="imbFechaBaja_Click" />
                                                <asp:CompareValidator 
                                                    id="CompareValidator1" runat="server"  
                                                    Type="Date" 
                                                    Operator="DataTypeCheck" 
                                                    ControlToValidate="txbFechaBaja"  
                                                    ErrorMessage="Fecha inválida"
                                                    SetFocusOnError = "true" > 
                                                </asp:CompareValidator> 
                                                </td>
                                            </tr>
                                                    <tr>
                                                        <td style="width: 163px">
                                                            Oficio:</td>
                                                        <td class="izq" colspan='2'>
                                                            <asp:TextBox ID="txbOficio" runat="server" CssClass="textbox" MaxLength="60" 
                                                                onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)" 
                                                                onKeyDown="if(event.keyCode==13){event.keyCode=9;}" 
                                                                onkeypress="return validarNoEscritura(event);" Width="422px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                           
                                            <tr>
                                                <td class="subtitulo" colspan = 4>
                                                    Horario</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Horario:</td>
                                                <td align='left'>
                                                    <asp:DropDownList ID="ddlHorario" runat="server" CssClass="texto">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    Fecha Inicio Horario:</td>
                                                <td>
                                                    <asp:TextBox ID="txbFechaHorario" runat="server" CssClass="textbox" 
                                                        MaxLength="10" onblur="javascript:onLosFocus(this)" 
                                                        onfocus="javascript:onFocus(this)" 
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" 
                                                        onkeypress="javascript:validarNoEscritura(event);" ></asp:TextBox>
                                                        
                                                   <cc1:CalendarExtender ID="txbFechaHorario_CalendarExtender" runat="server" 
                                                        PopupButtonID="imbFechaHorario" TargetControlID="txbFechaHorario">
                                                    </cc1:CalendarExtender>
                                                    <asp:ImageButton ID="imbFechaHorario" runat="server" Height="18px" 
                                                        ImageUrl="~/Imagenes/Calendar.png" Width="18px" />

                                                   <asp:CompareValidator 
                                                            id="dateValidator" runat="server"  
                                                            Type="Date" 
                                                            Operator="DataTypeCheck" 
                                                            ControlToValidate="txbFechaHorario"  
                                                            ErrorMessage="Fecha inválida"
                                                            SetFocusOnError = "true"> 
                                                        </asp:CompareValidator> 


                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 30px">
                                                    </td>
                                                <td style="height: 30px">
                                                    </td>
                                                <td style="height: 30px">
                                                    </td>
                                                <td style="height: 30px">
                                                    <asp:Button ID="btnAgregarHorario" runat="server" CssClass="boton" 
                                                        Height="16px" 
                                                        onMouseOver="javascript:this.style.cursor='hand';" Text="+ Agregar" 
                                                        ToolTip="Aceptar" ValidationGroup="agregarAsignacion" 
                                                        onclick="btnAgregarHorario_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class=cen colspan="4">
                                        <asp:Panel ID="panelHorario" runat="Server" Height="130px"  ScrollBars="Auto" 
                                            Width="875px">
                                        <asp:GridView ID="grVHorario" runat="server" AllowSorting="True" 
                                            AutoGenerateColumns="False" CellPadding="4" CssClass="texto" 
                                            ForeColor="#333333" OnRowCommand="grvCartilla_RowCommand" 
                                            OnRowDeleting="grvAsignacion_RowDeleting" 
                                            OnRowUpdating="grvAsignacion_RowUpdating" 
                                            onselectedindexchanged="grvAsignacion_SelectedIndexChanged" Width="95%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblServicio0" runat="server" 
                                                            Text='<%# Eval("idAsignacionHorario") %>'>
                                                </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Horario">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInstalación0" runat="server" 
                                                            Text='<%#Eval("horNombre") %>'>
                                                </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Inicio Horario">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFuncion0" runat="server" 
                                                            Text='<%# Eval("strFechaInicio") %>'>
                                                </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fin Horario">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFechaAlta0" runat="server" Text='<%# Eval("strFechaFin") %>'>
                                                </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" 
                                                Wrap="False" />
                                            <EmptyDataRowStyle Font-Names="Verdana" Font-Size="Medium" ForeColor="Navy" 
                                                HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <FooterStyle BackColor="#727272" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#727272" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#727272" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#999999" />
                                            <AlternatingRowStyle BackColor="White" ForeColor="#727272" />
                                        </asp:GridView>
                                        </asp:Panel>    
                                    </td>
                                </tr>
                            </table>
                   
                            <table align="center">
                                <tr>
                                    <td class="der">
                                        <br />
                                        <asp:Button ID="btnAgregar" runat="server" Text="Aceptar" CssClass="boton" onMouseOver="javascript:this.style.cursor='hand';"
                                            OnClick="btnAgregar_Click" ValidationGroup="agregarAsignacion" ToolTip="Aceptar" />
                                             <asp:Button ID="btnGenerarOficio" runat="server" Text="Generar oficio" CssClass="boton"
                                                onMouseOver="javascript:this.style.cursor='hand';"
                                                ToolTip="Generar oficio" onclick="btnGenerarOficio_Click" />
                                        <asp:Button ID="btnCancelarAsignacion" runat="server" Text="Cancelar" CssClass="boton"
                                            onMouseOver="javascript:this.style.cursor='hand';" OnClick="btnCancelarAsignacion_Click"
                                            ToolTip="Cancelar" />
                                    </td>
                                </tr>
                            </table>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

        </asp:Panel>


             <cc1:ModalPopupExtender ID="popDetalle" runat="server" 
                  BackgroundCssClass="modalBackground" PopupControlID="pnlDetalle" 
                  TargetControlID="btnDetalle">
              </cc1:ModalPopupExtender>


    <asp:Panel ID="pnlDetalle" runat="server" DefaultButton="btnDetalle">
            <div style="background-color: White; margin: 0 auto 0 auto;" class="nder">
                <div style="background-repeat: repeat; background-image: url(./../Imagenes/line.png);
                    margin: 30px auto 30px auto; border: outset 2px Black;">
                    <asp:UpdatePanel ID="upnDetalle" runat="server">
                        <ContentTemplate>





                            <table style="width: 53%;">
                                <tr>
                                    <td class="subtitulo" colspan="2">
                                        Detalles de la asignación</td>
                                </tr>
                                <tr>
                                    <td class="der" style="width: 141px">
                                        &nbsp;</td>
                                    <td class="izq">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="der" style="width: 141px">
                                        Nombre del usuario:</td>
                                    <td class="izq">
                                        <asp:Label ID="lblNombreUsuario" runat="server" Text="Label"></asp:Label>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="der" style="width: 141px">
                                        Fecha de creación:&nbsp;
                                    </td>
                                    <td class="izq">
                                        <asp:Label ID="lblCreacion" runat="server" Text="Label"></asp:Label>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="der" style="width: 141px">
                                        &nbsp;</td>
                                    <td class="izq">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="cen" colspan="2">
                                        &nbsp;
                                        <asp:Button ID="btnCerrarDetalle" runat="server" CssClass="boton" 
                                            OnClick="btnCerrarDetalle_Click" onMouseOver="javascript:this.style.cursor='hand';" 
                                            Text="Cerrar" ToolTip="Cerrar" ValidationGroup="agregarAsignacion" />
                                    </td>
                                </tr>
                            </table>




        
     </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

        </asp:Panel>





    <asp:Button ID="btnDetalle" runat="server" Text="" style="visibility:hidden;" />





          <cc1:ModalPopupExtender ID="popConsulta" runat="server"  BackgroundCssClass="modalBackground" PopupControlID="pnlConsulta" 
                  TargetControlID="btnConsulta">
              </cc1:ModalPopupExtender>


<asp:Panel ID="pnlConsulta" runat="server" DefaultButton="btnConsulta">
            <div style="background-color: White; margin: 0 auto 0 auto;" class="nder">
                <div style="background-repeat: repeat; background-image: url(./../Imagenes/line.png);
                    margin: 30px auto 30px auto; border: outset 2px Black;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>





                            <table style="width: 53%;">
                                <tr>
                                    <td class="subtitulo" colspan="4">
                                        consulta de la asignación&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="der" style="width: 141px">
                                        &nbsp;</td>
                                    <td class="izq" colspan="3">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="der" style="width: 141px">
                                        Servicio:</td>
                                    <td class="izq" colspan="3">
                                        <asp:Label ID="lblServicio" runat="server"></asp:Label>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="der" style="width: 141px">
                                        Instalación:</td>
                                    <td class="izq" colspan="3">
                                        <asp:Label ID="lblInstalacion" runat="server"></asp:Label>
                                        &nbsp;
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td class="der" style="width: 141px">
                                        Función:</td>
                                    <td class="izq" colspan="3">
                                        <asp:Label ID="lblFuncion" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="der" style="width: 141px">
                                        Fecha Inicio:</td>
                                    <td class="izq">
                                        <asp:Label ID="lblFechaInicio" runat="server"></asp:Label>
                                    </td>
                                    <td rowspan='3' align='center'>
                                            <table>
                                               
                                                <tr align="center">
                                                    <td colspan="2">
                                                     
                                                            Ver Oficio
                                                            <br />
                                                            <asp:ImageButton ID="imgOficioVista" runat="server" Height="80px" ImageUrl="~/Imagenes/lm.png"
                                                            OnClientClick="alert('No hay archivo adjunto.');" OnClick="imbOficioVista_Click"/>
                                                           </td>
                                                </tr>
                                                
                                            </table>
                                    </td>
                               </tr>
                               <tr>

                                    <td class="der" style="width: 141px">
                                        Fecha Fin:
                                    </td>
                                    <td class="izq">
                                        <asp:Label ID="lblFechaFin" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="der" style="width: 141px">
                                        Oficio:</td>
                                    <td class="izq">
                                        <asp:Label ID="lblOficio" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="subtitulo" colspan="4">
                                        Horario</td>
                                </tr>
                                <tr>
                                    <td class="cen" colspan="4">
                                    <asp:Panel ID="panelHorarioConsulta" runat="Server" Height="130px"  ScrollBars="Auto" 
                                            Width="600px">
                                        <asp:GridView ID="grvHorarioConsulta" runat="server" AllowSorting="True" 
                                            AutoGenerateColumns="False" CellPadding="4" CssClass="texto" 
                                            ForeColor="#333333" OnRowCommand="grvCartilla_RowCommand" 
                                            OnRowDeleting="grvAsignacion_RowDeleting" 
                                            OnRowUpdating="grvAsignacion_RowUpdating" 
                                            onselectedindexchanged="grvAsignacion_SelectedIndexChanged" Width="95%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblServicio1" runat="server" 
                                                            Text='<%# Eval("idAsignacionHorario") %>'>
                                                </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Horario">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInstalación1" runat="server" Text='<%#Eval("horNombre") %>'>
                                                </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Inicio Horario">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFuncion1" runat="server" Text='<%# Eval("strFechaInicio") %>'>
                                                </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fin Horario">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFechaAlta1" runat="server" Text='<%# Eval("strFechaFin") %>'>
                                                </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" 
                                                Wrap="False" />
                                            <EmptyDataRowStyle Font-Names="Verdana" Font-Size="Medium" ForeColor="Navy" 
                                                HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <FooterStyle BackColor="#727272" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#727272" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#727272" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#999999" />
                                            <AlternatingRowStyle BackColor="White" ForeColor="#727272" />
                                        </asp:GridView>
                                    </asp:Panel>

                                    </td>
                                </tr>
                                <tr>
                                    <td class="cen" colspan="4">
                                        &nbsp;
                                        <asp:Button ID="btnCerrar" runat="server" CssClass="boton" 
                                            OnClick="btnCerrar_Click" onMouseOver="javascript:this.style.cursor='hand';" 
                                            Text="Cerrar" ToolTip="Cerrar" ValidationGroup="agregarAsignacion" />
                                    </td>
                                </tr>
                            </table>




        
     </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

        </asp:Panel>

        <asp:ModalPopupExtender ID="mpeCargarOficio" runat="server" PopupControlID="pnlCargarOficio"
            TargetControlID="btnAbrirOficioAsig" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
        
<asp:Panel ID="pnlCargarOficio" runat="server" Width="40%">
    <div>
        <div class="nder" style="margin: 0px auto; background-color: white">
            <table style="width:100%">
                <tr>
                    <td class="subtitulo" colspan="2" >
                        Carga de Oficio de Asignación
                    </td>
                </tr>
                <tr>
                   
                    <td colspan="2" align="center">
                        <asp:Label ID="lblErrorOficioAsig" runat="server" ForeColor="#CC0000"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblRuta" runat="server" CssClass="etiquetaVisita" Text="Ruta del documento:"
                            Width="150px" />
                    </td>
                    <td align="left">
                        <asp:FileUpload ID="fuOficioAsig"  runat="server"  CssClass="boton"  />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                                    
                        <asp:Button ID="btnCargarOficioAsig" runat="server" CssClass="boton"  Text="Agregar" OnClick="btnCargarOficioAsig_Click" />
                    </td>
                    <td align="left">
                        <asp:Button ID="btnCancelarCargarOficio" runat="server" CssClass="boton"  Text="Cancelar"
                            OnClick="btnCancelarCargarOficioAsig_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Panel>




    <asp:Button ID="btnConsulta" runat="server" Text="" style="visibility:hidden;" />
    <asp:Button ID="btnOficio" runat="server" Text="" style="visibility:hidden;" />


     <cc1:ModalPopupExtender ID="mpuGenOficio" runat="server" PopupControlID="pnlGenOficio"
        TargetControlID="btnOficio" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>


    
       <asp:Panel ID="pnlGenOficio" runat="server" DefaultButton="btnOficio" 
        Width="500px">
        <div style="background-color: White; margin: 0 auto 0 auto;" class="nder">
            <div style="background-repeat: repeat; background-image: url(../Imagenes/line.png);
                margin: 30px auto 30px auto; border: outset 1px Black;">



                 <asp:UpdatePanel ID="UpdatepnlGenOficio" runat="server">
                    <ContentTemplate>


                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 10px">
                                                    &nbsp;</td>
                                               
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <table  style="width: 100%">
                                                        <tr>
                                                            <th class="titulo" colspan="5" class="cen">
                                                               <strong> Generando Oficios de Asignación</strong>
                                                            </th>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="5" class="izq">
                                                               <div ID="divErrorOficio"  style="width: 100%" runat="server" class="divError" visible="false">
                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td width="100%" align="left">
                                                                            <asp:Label ID="lblErrorFirmante" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                                </td>
                                   
                                                        </tr>
                                                        <tr>
                                                            <td colspan="1" class="cen">
                                                        </td>
                                                            <td colspan="4" class="izq">
                                                                Seleccionar persona que firmará el Oficio de Asignación:
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                        <td colspan="1" class="izq">
                                                        </td>
                                                            <td colspan="3" class="izq">
                                                                <asp:DropDownList ID="ddlPersonaFirma" runat="server" Font-Size="Small">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td colspan="1" class="izq">
                                                        </td>
                                                        </tr>
                                                       
                                                    </table>
                                                </td>
                                            </tr>
                                        <tr>
                                            <td class="style3" colspan="2">
                                             </td>
                                            <td class="style3" colspan="1">
                                                <asp:Button ID="btnAceptarGO" runat="server" CssClass="boton" 
                                                    onclick="btnGenOficio_Click" Text="Aceptar" Width="80px" />
                                            
                                                <asp:Button ID="btnCancelarGO" runat="server" CssClass="boton" 
                                                    onclick="btnCancelarGO_Click" Text="Cancelar" Width="80px" />
                                            </td>
                                             <td class="style3" colspan="2">
                                             </td>
                                           
                                        </tr>
                                         <tr>
                                                <td colspan="5" style="width: 10px">
                                                    <br>
                                                </br>
                                                   </td>
                                               
                                            </tr>
                                       
                                        </table>
                                        </td>
                                        </tr>
                                        
                                        </table>
                            </ContentTemplate>
                    </asp:UpdatePanel>
            </div>
        </div>
    </asp:Panel>




</asp:Content>
