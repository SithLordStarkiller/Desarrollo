<%@ Page Title="" Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true" CodeFile="frmInventario.aspx.cs" Inherits="Servicio_frmInventario" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script language="javascript" type="text/javascript" src="../Generales/JScript.js"></script>

   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                        <ContentTemplate>
    <table class="tamanio">
    <tr><td class="titulo">Inventario de armamento y equipo</td></tr>
    <tr><td>
    
         <div id="divErrorFrm" runat="server" class="divError" visible="false">
                                                                <table>
                                                                    <tr>
                                                                        <td width="835">
                                                                            <asp:Label ID="lblerrorFrm" runat="server" Width="845px"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
    
    </td></tr>
        <tr>
            <td class="der">
                <table style="width:100%;">

                    <tr>
                        <td style="height: 20px; width: 112px">
                Servicio:</td>
                        <td class="izq" style="height: 20px">
                                                            <asp:DropDownList ID="ddlServicio" runat="server" 
                                                                onselectedindexchanged="ddlServicio_SelectedIndexChanged" 
                                                                AutoPostBack="True" CssClass="texto">
                                                            </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvRaiz4" runat="server" 
                                        ControlToValidate="ddlServicio" 
                                        ErrorMessage="Debe seleccionar el servicio" 
                                        Font-Bold="True" SetFocusOnError="True" ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>
                                                        </td>
                    </tr>
                    <tr>
                        <td style="width: 112px">
                Instalación:</td>
                        <td class="izq">
                                                            <asp:DropDownList ID="ddlInstalacion" runat="server" CssClass="texto" 
                                                                AutoPostBack="True" 
                                                                onselectedindexchanged="ddlInstalacion_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                    </tr>
                    <tr>
                        <td style="width: 112px">
                            &nbsp;</td>
                        <td class="izq">
                                                            <table width="240">
                                                                <tr>
                                                                    <td style="width: 122px">
                                                                        <asp:Button ID="btnAgregar" runat="server" CssClass="boton" Enabled="False" 
                                                                            onclick="btnAgregar_Click" Text="Agregar Inventario" Width="120px" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="margin: 0 auto 0 auto;" width="400px" class="texto">
                                                                    <tr>
                                                                        <td>
                                                    <asp:ImageButton ID="imgAtras" runat="server" 
                                                        ImageUrl="~/Imagenes/retroceder.png" OnClick="imgAtras_Click" ToolTip="Atrás" 
                                                        Visible="False" Width="50px" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:GridView ID="grvInventario" runat="server" AutoGenerateColumns="False"  OnPageIndexChanging="grvInventario_PageIndexChanging"  AllowPaging="True" 
                                                                                OnRowUpdating="grvInventario_RowUpdating"  OnRowDeleting="grvInventario_RowDeleting" 
                                                                                CellPadding="4" ForeColor="#333333" PageSize="5" Width="100%">
                                                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
                                                                                <FooterStyle BackColor="#727272" Font-Bold="True" ForeColor="White" />
                                                                                <PagerStyle BackColor="#727272" ForeColor="White" HorizontalAlign="Center" />
                                                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                                <HeaderStyle BackColor="#727272" Font-Bold="True" ForeColor="White" />
                                                                                <EditRowStyle BackColor="#999999" />
                                                                                <AlternatingRowStyle BackColor="White" ForeColor="#727272" />
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="No" Visible="True">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="idInventario" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                                                                            </Label>
                                                                                        </ItemTemplate>
                                                                                        <ControlStyle Width="25px" />
                                                                                        <ItemStyle />
                                                                                        <HeaderStyle Width="50px" />
                                                                                    </asp:TemplateField>


                                                                                    

                                                                                    <asp:TemplateField HeaderText="Fecha Inicio">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblFechaInicio" runat="server" 
                                                                                                Text='<%# Eval("ieFechaInicio") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                                    </asp:TemplateField>

                                                                                         <asp:TemplateField HeaderText="Fecha Fin">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblFechaFin" runat="server" 
                                                                                                Text='<%# Eval("ieFechaFin") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                                    </asp:TemplateField>


                                                                                           <asp:TemplateField HeaderText="Vigente">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="ieVigente" runat="server" 
                                                                                                Text='<%# Eval("ieVigente") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                                    </asp:TemplateField>


                                                                                               <asp:TemplateField HeaderText="Detalle">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="imbSeleccionar" runat="server" CommandName="Update" 
                                                                                                ImageUrl="~/Imagenes/informacion.png" ToolTip="Modificar" Width="25px" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                          
                                                                             <asp:TemplateField HeaderText="Eliminar">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imbEliminar" runat="server" CommandName="Delete" visible="false"
                                                                        ImageUrl="~/Imagenes/Symbol-Delete-Mini.png" 
                                                                        OnClientClick="if(!confirm('El registro será eliminado al guardar los cambios, ¿Desea continuar?')) return false;" 
                                                                        ToolTip="Eliminar" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </td>
                                                                        <td>
                                                    <asp:ImageButton ID="imgAdelante" runat="server" 
                                                        ImageUrl="~/Imagenes/avanzar.png" OnClick="imgAdelante_Click" 
                                                        ToolTip="Siguiente" Visible="False" Width="50px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            &nbsp;</td>
                                                                        <td>
                                                                            &nbsp;</td>
                                                                        <td>
                                                                            &nbsp;</td>
                                                                    </tr>
                                                                </table>
               </td>
        </tr>




                 <tr>
                                <td colspan="2">
                                 
                                 

                                 <table style="margin: 0 auto 0 auto; width: 600px;">
                                                <tr>
                                                    <td>
                                                        <cc1:ModalPopupExtender ID="popDetalle" runat="server" PopupControlID="pnlArmamento"
                                                            TargetControlID="btnArmamento" BackgroundCssClass="modalBackground">
                                                        </cc1:ModalPopupExtender>
                                                        <asp:Panel ID="pnlArmamento" runat="server" >
                                                            <div style="background-color: White; margin: 0 auto 0 auto;">
                                                                <div style="background-repeat: repeat; background-image: url('../Imagenes/line.png');
                                                                    border: outset 1px Black; margin-right: auto; margin-left: auto; text-align: left;">
                                                                    <asp:UpdatePanel ID="UpdatePanelDetalle" runat="server">
                                                                        <ContentTemplate>
                                                                            <table style="width: 600px">
                                                                            
                                                                                <tr>
                                                                                    <td class="titulo">
                                                                                        &nbsp;<asp:Label ID="lblPop" runat="server"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                             <div id="divError" runat="server" class="divError" visible="false">
                                                                <table>
                                                                    <tr>
                                                                        <td width="835">
                                                                            <asp:Label ID="lblerror" runat="server" Width="100%"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        &nbsp;
                                                                                        <table style="width: 100%;">
                                                                                            <tr>
                                                                                                <td class="der">
                                                                                                    Fecha Inicio:</td>
                                                                                                <td>
                                                                                                      <asp:TextBox ID="txbFechaInicio" runat="server" CssClass="textbox" MaxLength="10"
                                                                onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)" onKeyDown="if(event.keyCode==13){event.keyCode=9;}"
                                                                onkeypress="return validarNoEscritura(event);" Width="125px"></asp:TextBox><asp:ImageButton ID="imbFechaInicio" 
                                                                                                          runat="server" Height="16px" ImageUrl="~/Imagenes/Calendar.png"
                                                                Width="16px" />
                                                            <cc1:CalendarExtender ID="calNacimiento" runat="server" PopupButtonID="imbFechaInicio"
                                                                TargetControlID="txbFechaInicio">
                                                            </cc1:CalendarExtender></td>
                                                                                                <td class="der">
                                                                                                    Fecha Fin:</td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txbFechaFin" runat="server" CssClass="textbox" MaxLength="10" 
                                                                                                        onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)" 
                                                                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" 
                                                                                                        onkeypress="return validarNoEscritura(event);" Width="125px" 
                                                                                                        Enabled="False"></asp:TextBox>
                                                                                                    <asp:ImageButton ID="imbFechaFin" runat="server" Height="16px" 
                                                                                                        ImageUrl="~/Imagenes/Calendar.png" Width="16px" Enabled="False" />
                                                                                                    <cc1:CalendarExtender ID="calIngreso" runat="server" PopupButtonID="imbFechaFin" 
                                                                                                        TargetControlID="txbFechaFin">
                                                                                                    </cc1:CalendarExtender>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        &nbsp;</td>
                                                                                </tr>
                                                                                <tr >
                                                                                    <td >
                                                                                    
                                 <table style="margin: 0 auto 0 auto;" width="300px" class="texto"><tr><td>
                                                                                        <asp:GridView ID="grvArmamento" runat="server" AutoGenerateColumns="False" CellPadding="4" 
                                                                                            ForeColor="#333333" PageSize="6" Width="100%">
                                                                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
                                                                                            <FooterStyle BackColor="#727272" Font-Bold="True" ForeColor="White" />
                                                                                            <PagerStyle BackColor="#727272" ForeColor="White" HorizontalAlign="Center" />
                                                                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                                            <HeaderStyle BackColor="#727272" Font-Bold="True" ForeColor="White" />
                                                                                            <EditRowStyle BackColor="#999999" />
                                                                                            <AlternatingRowStyle BackColor="White" ForeColor="#727272" />
                                                                                            <Columns>
                                                                                                <asp:TemplateField HeaderText="No" Visible="True">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="id" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                                                                                        </Label>
                                                                                                    </ItemTemplate>
                                                                                                    <ControlStyle Width="25px" />
                                                                                                    <ItemStyle />
                                                                                                    <HeaderStyle Width="50px" />
                                                                                                </asp:TemplateField>
                                                                                                   
                                                                                                <asp:TemplateField HeaderText="Descripción">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("equDescripcion") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                                </asp:TemplateField>

                                                                                                <asp:TemplateField HeaderText="Cantidad">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:TextBox ID="txbNumero" runat="server"  Width="100px"  Text='<%# Eval("ieCantidad") %>' onkeypress="return nvalidar(event,this)" onchange="this.value=quitaacentos(this.value)" MaxLength="6"  onpaste="return false" />
                                                                                                    </ItemTemplate>
                                                                                                    <ControlStyle Width="100px" />
                                                                                                    <HeaderStyle Width="50px" />
                                                                                                </asp:TemplateField>

                                                                                                 


                                                                                                       <asp:TemplateField HeaderText="idEquipo" Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="idEquipo" runat="server"  Width="100px"  Text='<%# Eval("idEquipo") %>' />
                                                                                                    </ItemTemplate>
                                                                                                    <ControlStyle Width="100px" />
                                                                                                    <HeaderStyle Width="50px" />
                                                                                                </asp:TemplateField>


                                                                                            </Columns>
                                                                                        </asp:GridView>


                                                                                        </td></tr></table>
                                                                                        <center>
                                                                                            <strong>
                                                                                                <asp:Label ID="Label1" runat="server" Text="0"></asp:Label>
                                                                                                &nbsp;de
                                                                                                <asp:Label ID="Label2" runat="server" Text="0"></asp:Label>
                                                                                            </strong>
                                                                                        </center>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <table style="margin: 0 auto 0 auto;" width="240px">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Button ID="btnAgregarCancelar" runat="server" CssClass="boton" 
                                                                                                        onclick="btnAgregarCancelar_Click" Text="Cancelar" Width="120px" />
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:Button ID="btnAgregarPop" runat="server" CssClass="boton" Text="Agregar" 
                                                                                                        Width="120px" onclick="btnAgregarPop_Click" />
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
                                                    </td>
                                                </tr>
                                            </table>






                                 
                                 </td>
                           
                            <tr><td>
                                <table style="margin: 0 auto 0 auto;" width="240px">
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnCancelarFrm" runat="server" CssClass="boton" 
                                                onclick="btnCancelarFrm_Click" Text="Cancelar" Width="120px" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnGuardarFrm" runat="server" CssClass="boton" Enabled="False" 
                                                onclick="btnGuardarFrm_Click" Text="Guardar" Width="120px" />
                                        </td>
                                    </tr>
                                </table>
                                </td></tr>
                            </tr>
        </table>
       <asp:Button ID="btnArmamento" runat="server" Text="Button" style="visibility:hidden" />
       </ContentTemplate>
       </asp:UpdatePanel>
</asp:Content>

