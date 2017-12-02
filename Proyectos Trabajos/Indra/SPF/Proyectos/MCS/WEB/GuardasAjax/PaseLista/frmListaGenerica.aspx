<%@ Page Title="Módulo de Control de Servicios ::: Lista Genérica" Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmListaGenerica.aspx.cs" Inherits="PaseLista_frmListaGenerica" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <%--<script type="text/javascript">  
    function disabledValidation() {  
        ValidatorEnable(document.getElementById('<%=btnAgregar.ClientID%>'), false);
        ValidationSummaryOnSubmit();
    }  
    function enabledValidation() {  
        ValidatorEnable(document.getElementById('<%=btnModificar.ClientID%>'), true); 
        ValidatorEnable(document.getElementById('<%=btnCerrar.ClientID%>'), true);
        ValidationSummaryOnSubmit();
    }  
</script>--%>

    <cc1:ModalPopupExtender ID="popConsulta" runat="server"  BackgroundCssClass="modalBackground" PopupControlID="pnlConsulta" 
        TargetControlID="btnConsulta" OkControlID="btnOK">
    </cc1:ModalPopupExtender>

    <asp:Panel ID="pnlConsulta" runat="server" HorizontalAlign="Center" DefaultButton="btnConsulta" Style="display:none;">
            <div class="nder" style="background-color: White; margin: 0px auto;">
                <div style="background-repeat: repeat; background-image: url(./../Imagenes/line.png);
                    margin: 30px auto 30px auto; border: outset 2px Black;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>

                            <table>
                                <tr>
                                    <td class="subtitulo" colspan="4">
                                        Inconsistencia&nbsp;</td>
                                </tr>

                                <tr>
                                    <td>
                                        <div id="divErrorWin" runat="server" class="divError" visible="false">
                                            <table>
                                                <tr>
                                                    <td width="100%">
                                                        <asp:Label ID="lblErrorWin" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                        
                                            </table>
                                        </div>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="der">
                                        &nbsp;</td>
                                    <td class="izq" colspan="3">
                                        &nbsp;</td>
                                </tr>

                                <tr>
                                    <td class="izq" colspan="4" style="font-size: 14px; font-weight: bold">
                                        <asp:Label ID="lblMensajeWin" runat="server"></asp:Label>
                                        
                                    
                                    </td>
                                </tr>

                                <tr>
                                    <td class="der">
                                        &nbsp;</td>
                                    <td class="izq" colspan="3">
                                        &nbsp;</td>
                                </tr>

                                <tr>
                                    <td class="der">
                                        <asp:Label ID="lblServicio" runat="server" text="Servicio:"></asp:Label></td>
                                    <td class="izq">
                                        <asp:DropDownList ID="ddlServicioWin" runat="server" AutoPostBack="True" 
                                            CssClass="texto" OnSelectedIndexChanged="ddlServicioWin_SelectedIndexChanged" TabIndex="10">
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
                                        <asp:Label ID="lblInstalacion" runat="server" text="Instalación:"></asp:Label></td>
                                    <td class="izq" colspan="3">
                                        <asp:DropDownList ID="ddlInstalacionWin" runat="server" CssClass="texto" 
                                            onselectedindexchanged="ddlInstalacion_SelectedIndexChanged" 
                                            TabIndex="11" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td class="der">
                                        <asp:Label ID="lblFuncion" runat="server" text="Función:"></asp:Label></td>
                                    <td class="izq" colspan="3">
                                        <asp:DropDownList ID="ddlFuncion" runat="server" CssClass="texto">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td class="der">
                                        &nbsp;</td>
                                    <td class="izq" colspan="3">
                                        &nbsp;</td>
                                </tr>

                                <%--<tr>
                                    <td class="cen" colspan="4">
                                        <asp:Button ID="btnModificar" runat="server" CssClass="boton"  Text="Modificar"
                                            OnClick="btnModificar_Click" />  
                                            
                                        <asp:Button ID="btnCerrar" runat="server" CssClass="boton" Text="Cancelar"
                                            OnClick="btnCerrar_Click" />

                                        <asp:Button ID="btnOK" runat="server" CssClass="boton" Text="" Style="display:none;" />
                                    </td>
                                </tr>--%>
                            </table>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <table>
                        <tr>
                                    <td class="cen" colspan="4">
                                        <asp:Button ID="btnModificar" runat="server" CssClass="boton"  Text="Modificar"
                                            OnClick="btnModificar_Click" CausesValidation="false"/>  
                                            
                                        <asp:Button ID="btnCerrar" runat="server" CssClass="boton" Text="Cancelar"
                                            OnClick="btnCerrar_Click" CausesValidation="false"/>

                                        <asp:Button ID="btnSalir" runat="server" CssClass="boton" Text="Salir"
                                            OnClick="btnSalir_Click" CausesValidation="false"/>
                                        <asp:Button ID="btnOK" runat="server" CssClass="boton" Text="" Style="display:none;" />
                                    </td>
                                </tr>
                    </table>

                </div>
            </div>

        </asp:Panel>
    
    <script language="javascript" type="text/javascript" src="../Generales/JScript.js"></script>
    
    
    <div id="Contenido">

    <table style="width: 100%;">
        <tr>
            <td class="titulo">
                pase de lista
            </td>
        </tr>
        <tr>
            <td class="subtitulo">
                datos del pase de lista
            </td>
        </tr>
        <tr>
            <td>
                <div id="divError" runat="server" class="divError" visible="false">
                    <table>
                        <tr>
                            <td width="100%">
                                <asp:Label ID="lblerror" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <%--ACTUALIZACIÓN MARZO 2017--%>

                        <tr width="100%">
                            <asp:Label ID="lblInconsistencia" runat="server"></asp:Label>
                        </tr>

                        <%--FIN ACTUALIZACIÓN--%>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <table class="tamanio">
                    <tr>
                        <td class="der" width="450px">
                            fecha:
                        </td>
                        <td>
                            <asp:TextBox ID="txbFecha" runat="server" CssClass="textbox" MaxLength="10" onblur="javascript:onLosFocus(this)"
                                onfocus="javascript:onFocus(this)" onKeyDown="if(event.keyCode==13){event.keyCode=9;}"
                                onkeypress="return validarNoEscritura(event);" Width="125px" 
                                AutoPostBack="True" ontextchanged="txbFecha_TextChanged"></asp:TextBox>
                            <asp:ImageButton ID="imbNacimiento" runat="server" Height="16px" ImageUrl="~/Imagenes/Calendar.png"
                                Width="16px"  />
                            <asp:CalendarExtender ID="calNacimiento" runat="server" PopupButtonID="imbNacimiento"
                                TargetControlID="txbFecha">
                            </asp:CalendarExtender>

                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="der" width="450px">
                            responsable de la asistencia:
                        </td>
                        <td>
                            <asp:Label ID="lblResponsable" runat="server" Text="-"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="der">
                            servicio:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlServicio" runat="server" CssClass="textbox" AutoPostBack="True"  Enabled="false"
                                OnSelectedIndexChanged="ddlServicio_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="der">
                            instalacion:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlInstalacion" runat="server" CssClass="textbox" OnSelectedIndexChanged="ddlInstalacion_SelectedIndexChanged"
                                Enabled="false" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="der">
                            horario:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlHorario" runat="server" CssClass="textbox" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="der">
                            tipo de asistencia:
                        </td>
                        <td>
                            <table style="width: 100%; border-collapse: collapse">
                                <tr>
                                    <td>
                                        
                                        <%-- Cambio por Integracion con CONEC Mayo 2016
                                        <asp:DropDownList ID="ddlTipoAsistencia" runat="server" CssClass="textbox">
                                            <asp:ListItem>SELECCIONAR</asp:ListItem>
                                            <asp:ListItem Value="6">DESCANSO</asp:ListItem>
                                            <asp:ListItem Value="3">FALTA</asp:ListItem>
                                            <asp:ListItem Value="1">PRESENTE</asp:ListItem>
                                        </asp:DropDownList>--%>
                                         
                                        <asp:DropDownList ID="ddlTipoAsistencia" runat="server" CssClass="textbox"
                                            Enabled="false" AutoPostBack="True">
                                        </asp:DropDownList>


                                    </td>
                                    <td>
                                        <asp:Button ID="btnAgregar" runat="server" CssClass="boton" Text="Agregar a la lista de empleados"
                                            OnClick="btnAgregar_Click" CausesValidation="false" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="der">
                            no. empleado
                        </td>
                        <td>
                            <asp:TextBox ID="txbNoEmpleado" runat="server" MaxLength="10" CssClass="textbox"
                                onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)" onKeyDown="if(event.keyCode==13){event.keyCode=9;}"
                                Width="150px">
                            </asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="margin: 0 auto 0 auto;">
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="rblMonta" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">MONTA</asp:ListItem>
                                <asp:ListItem Value="2">NO MONTA</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

        <%--Colocar aqui el boton oculto y probar--%>
        <tr>
            <td style="visibility: hidden">
                <asp:Button ID="btnConsulta" runat="server" CssClass="boton" Text="" />

                
            </td>
        </tr>
        <tr>
            <td class="subtitulo">
                lista de empleados
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table class="centro">
                    <tr runat="server" id="trGrid">
                        <td>
                            <div id="Grid">
                                <table class="centro">
                                    <tr>
                                        <td class="centro">
                                            <asp:GridView ID="grvListado" runat="server" CssClass="texto" AutoGenerateColumns="False"
                                                CellPadding="4" ForeColor="#333333" Font-Bold="False" Width="100%" OnRowUpdating="grvListado_RowUpdating">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No." Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="idNumber" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                                            </Label>
                                                        </ItemTemplate>
                                                        <ControlStyle Width="25px" />
                                                        <ItemStyle />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Paterno">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPaterno" runat="server" Text='<%# Eval("empPaterno") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Materno">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMaterno" runat="server" Text='<%# Eval("empMaterno") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nombre">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("empNombre") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No Empleado">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNumero" runat="server" Text='<%# Eval("empNumero") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Horario">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHorario" runat="server" Text='<%# Eval("horDescripcion") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Asistencia">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTipoAsistencia" runat="server" Text='<%# Eval("tipoAsistencia") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Hora">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHora" runat="server" Text='<%# Eval("asiHora") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Eliminar">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imbEliminar" runat="server" CommandName="Update" ImageUrl="~/Imagenes/Symbol-Delete-Mini.png"
                                                                ToolTip="Eliminar" />
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
            <td>
                <table style="margin: 0 auto 0 auto;">
                    <tr>
                        <td>
                            <asp:Button ID="btnGuardar" runat="server" CssClass="boton" Text="Guardar" Width="100px"
                                OnClick="btnGuardar_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnCancelar" runat="server" CssClass="boton" Text="Cancelar" Width="100px"
                                OnClick="btnCancelar_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnNuevo" runat="server" CssClass="boton" Text="Nuevo" Width="100px"
                                OnClick="btnNuevo_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

        <%--ACTUALIZACIÓN MARZO 2017--%>
        <tr>
            <%--<td style="visibility: hidden">
                <asp:Button ID="btnConsulta" runat="server" CssClass="boton" Text="" />
            </td>--%>
            <%--<td>
                <cc1:ModalPopupExtender ID="popConsulta" runat="server"  BackgroundCssClass="modalBackground" PopupControlID="pnlConsulta" 
                   TargetControlID="btnConsulta">
                </cc1:ModalPopupExtender>
            </td>--%>
        </tr>
        <%--FIN ACTUALIZACIÓN--%>

    </table>

    </div>

    <%--ACTUALIZACIÓN MARZO 2017--%>
    
    


    <%--FIN ACTUALIZACIÓN--%>

</asp:Content>
