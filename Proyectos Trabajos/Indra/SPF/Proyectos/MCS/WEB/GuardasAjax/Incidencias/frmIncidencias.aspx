<%@ Page Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmIncidencias.aspx.cs" Inherits="Incidencias_frmIncidencias" Title="Módulo de Control de Servicios ::: Incidencias"
    EnableEventValidation="false" %>

<%@ Register Src="../Generales/wucMensaje.ascx" TagName="wucMensaje" TagPrefix="uc1" %>
<%@ Import Namespace="SICOGUA.Entidades" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../Generales/JScript.js"></script>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div id="divMain" class="scrollbar">
                <asp:HiddenField ID="hfidEmpleado" runat="server" />
                <asp:Panel ID="panEstSoc" runat="server" Visible="true">
                    <table class="tamanio">
                        <tr>
                            <td  colspan="4">


                                        <div ID="divErrorInc"  style="width: 100%" runat="server" class="divError" visible="false">
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
                            <td class="titulo" colspan="4">
                                Incidencias
                            </td>
                        </tr>
                        <tr>
                            <td class="subtitulo" colspan="4">
                                Datos Generales
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table width="100%">
                                    <tr>
                                        <td class="der">
                                            No. de Empleado:
                                        </td>
                                        <td class="izq">
                                            <asp:Label ID="lblNumero" runat="server" Text="-"></asp:Label>
                                        </td>
                                        <td class="der">
                                            CUIP:
                                        </td>
                                        <td class="izq" colspan="3">
                                            <asp:Label ID="lblCuip" runat="server" Text="-"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="der">
                                            Apellido Paterno:
                                        </td>
                                        <td class="izq">
                                            <asp:Label ID="lblPaterno" runat="server" Text="-"></asp:Label>
                                        </td>
                                        <td class="der">
                                            Apellido Materno:
                                        </td>
                                        <td class="izq">
                                            <asp:Label ID="lblMaterno" runat="server" Text="-"></asp:Label>
                                        </td>
                                        <td class="der">
                                            Nombre:
                                        </td>
                                        <td class="izq">
                                            <asp:Label ID="lblNombre" runat="server" Text="-"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="subtitulo" colspan="4">
                                Incidencias
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:GridView ID="grvIncidencias" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                            ForeColor="#333333" Width="100%" OnRowDeleting="grvIncidencias_RowDeleting" OnRowUpdating="grvIncidencias_RowUpdating">
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
                                                        <asp:Label ID="lblIdIncidencia" runat="server" Text='<%# Eval("idIncidencia") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tipo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTipo" runat="server" Text='<%# ((clsEntTipoIncidencia)Eval("tipoIncidencia")).TiDescripcion %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Descripción">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("IncDescripcion") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Inicio">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInicio" runat="server" Text='<%# Eval("sfechaInicial") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fin">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFin" runat="server" Text='<%# Eval("sfechaFinal") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Persona Que Autoriza">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAutoriza" runat="server" Text='<%# Eval("sEmpleadoAutoriza") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imbSeleccionar" runat="server" CommandName="Update" ImageUrl="~/Imagenes/Download.png"
                                                            ToolTip="Consultar / Modificar" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imbEliminar" runat="server" ImageUrl="~/Imagenes/Symbol-Delete-Mini.png"
                                                    ToolTip="Eliminar" CommandName="Delete" OnClientClick="if(!confirm('El registro será eliminado al guardar los cambios, ¿Desea continuar?')) return false;" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="der">
                                <asp:Button ID="btnAgregar" runat="server" Text="Agregar Incidencia" CssClass="boton"
                                    ValidationGroup="SICOGUA" onMouseOver="javascript:this.style.cursor='hand';"
                                    ToolTip="Agregar Incidencia" OnClick="btnAgregar_Click1" />
                                <%--<cc1:ModalPopupExtender ID="popIncidencia" runat="server" TargetControlID="btnAgregar"
                            PopupControlID="panIncidencia" BackgroundCssClass="modalBackground">
                        </cc1:ModalPopupExtender>--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <br />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table align="center">
                        <tr>
                            <td colspan="6">
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" CssClass="boton" OnClientClick="if(!confirm('Perdera la información capturada, ¿Desea continuar?')) return false;"
                                    onMouseOver="javascript:this.style.cursor='hand';" ToolTip="Nuevo Registro" OnClick="btnNuevo_Click"
                                    Visible="False" />
                            </td>
                            <td id="tdModificar" runat="server" visible="false">
                                <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="boton" onMouseOver="javascript:this.style.cursor='hand';"
                                    ToolTip="Modificar Registro" />
                            </td>
                            <td id="tdEliminar" runat="server" visible="false">
                                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="boton" onMouseOver="javascript:this.style.cursor='hand';"
                                    ToolTip="Eliminar Registro" OnClientClick="if(!confirm('Esta acción eliminará el registro, ¿Deseas continuar?')) return false;" />
                            </td>
                            <td>
                                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="boton" onMouseOver="javascript:this.style.cursor='hand';"
                                    ToolTip="Guardar" OnClick="btnGuardar_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="boton" onMouseOver="javascript:this.style.cursor='hand';"
                                    ToolTip="Cancelar" OnClick="btnCancelar_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="boton" onMouseOver="javascript:this.style.cursor='hand';"
                                    ToolTip="Buscar" OnClick="btnBuscar_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <br />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <%--        Agregue   --%>
                <cc1:ModalPopupExtender ID="popIncidencia" runat="server" PopupControlID="panIncidencia"
                    TargetControlID="btnAgregar" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
                <asp:Panel ID="panIncidencia" runat="server">
                    <div style="background-color: White; margin: 0 auto 0 auto;" class="nder">
                        <div style="background-repeat: repeat; background-image: url(./../Imagenes/line.png);
                            margin: 30px auto 30px auto; border: outset 2px Black;">
                            <asp:UpdatePanel ID="upnIncidencia" runat="server">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td class="subtitulo" colspan="4">
                                                Datos de la Incidencia
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <br />
                                                <asp:HiddenField ID="hfRowIndex" runat="server" Value="-1" />
                                                <asp:HiddenField ID="hfIdEmpleadoIncidencia" runat="server" Value="0" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" class="izq">
                                                <asp:ValidationSummary ID="valIncidencia" runat="server" ValidationGroup="AgregarIncidencia"
                                                    CssClass="divError" BackColor="#FFFF80" BorderColor="Red" BorderStyle="Outset"
                                                    BorderWidth="1px" Font-Bold="True" Font-Size="7pt" HeaderText="Mensaje(s):" ShowMessageBox="True"
                                                    Width="100%" />
                                                <uc1:wucMensaje ID="wucMensajeIncidencia" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="der">
                                                Tipo:
                                            </td>
                                            <td class="izq" colspan="3">
                                                <asp:DropDownList ID="ddlTipoIncidencia" runat="server" CssClass="texto">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvTipo" runat="server" ErrorMessage="Debe Seleccionar un Tipo de Incidencia."
                                                    ControlToValidate="ddlTipoIncidencia" Font-Bold="True" SetFocusOnError="True"
                                                    ValidationGroup="AgregarIncidencia">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="der">
                                                Fecha de Inicio:
                                            </td>
                                            <td class="izq">
                                                <asp:TextBox ID="txbFechaInicial" runat="server" onblur="javascript:onLosFocus(this)"
                                                    CssClass="textbox" onkeypress="return validarNoEscritura(event);" onfocus="javascript:onFocus(this)"></asp:TextBox>
                                                <cc1:CalendarExtender ID="calInicial" runat="server" PopupButtonID="imbInicial" TargetControlID="txbFechaInicial">
                                                </cc1:CalendarExtender>
                                                <asp:ImageButton ID="imbInicial" runat="server" Height="18px" ImageUrl="~/Imagenes/Calendar.png"
                                                    Width="18px" />
                                                <asp:RequiredFieldValidator ID="rfvFechaInicio" runat="server" ErrorMessage="Debe Seleccionar una Fecha de Inicio."
                                                    ControlToValidate="txbFechaInicial" Font-Bold="True" SetFocusOnError="True" ValidationGroup="AgregarIncidencia">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td class="der">
                                                Fecha de Fin:
                                            </td>
                                            <td class="izq">
                                                <asp:TextBox ID="txbFechaFinal" runat="server" onblur="javascript:onLosFocus(this)"
                                                    CssClass="textbox" onkeypress="return validarNoEscritura(event);" onfocus="javascript:onFocus(this)">
                                                </asp:TextBox>
                                                <cc1:CalendarExtender ID="calFinal" runat="server" PopupButtonID="imbFinal" TargetControlID="txbFechaFinal">
                                                </cc1:CalendarExtender>
                                                <asp:ImageButton ID="imbFinal" runat="server" Height="18px" ImageUrl="~/Imagenes/Calendar.png"
                                                    Width="18px" />
                                                <asp:CompareValidator ID="covPeriodo" runat="server" ErrorMessage="La Fecha Final tiene que ser mayor o igual que la Fecha Inicial"
                                                    CssClass="Validator" Text="*" ControlToCompare="txbFechaFinal" ControlToValidate="txbFechaInicial"
                                                    Operator="LessThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="AgregarIncidencia">
                                                </asp:CompareValidator>
                                                <asp:RequiredFieldValidator ID="rfvFechaFin" runat="server" ErrorMessage="Debe Seleccionar una Fecha de Fin."
                                                    ControlToValidate="txbFechaFinal" Font-Bold="True" SetFocusOnError="True" ValidationGroup="AgregarIncidencia">*
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="der">
                                                Autoriza:
                                            </td>
                                            <td colspan="3" class="izq">
                                                <asp:DropDownList ID="ddlAutoriza" runat="server" CssClass="texto">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvAutoriza" runat="server" ErrorMessage="Debe Seleccionar la persona que autoriza."
                                                    ControlToValidate="ddlAutoriza" Font-Bold="True" SetFocusOnError="True" ValidationGroup="AgregarIncidencia">*
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="der">
                                                Oficio:
                                            </td>
                                            <td colspan="3" class="izq">
                                                <asp:TextBox ID="txbOficioIncidencia" runat="server" onblur="javascript:onLosFocus(this)"
                                                    CssClass="textbox" onfocus="javascript:onFocus(this)">
                                                </asp:TextBox>
                                                <%--                                        <asp:RequiredFieldValidator ID="rfvOficioIncidencia" runat="server" ErrorMessage="Debe Seleccionar el oficio de la Incidencia."
                                            ControlToValidate="txbOficioIncidencia" Font-Bold="True" SetFocusOnError="True" ValidationGroup="AgregarIncidencia">*
                                        </asp:RequiredFieldValidator>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="der">
                                                Descripción:
                                            </td>
                                            <td class="izq" colspan="3">
                                                <asp:TextBox ID="txbDescripcion" TextMode="MultiLine" Width="95%" Height="70px" runat="server"
                                                    onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)" onKeyUp="Count(this,200)"
                                                    onChange="Count(this,200); this.value=quitaacentos(this.value)" CssClass="textbox">
                                                </asp:TextBox>
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
                            <br />
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnGuardarIncidencia" runat="server" CssClass="boton" Text="Agregar"
                                            OnClick="btnAgregar_Click" onMouseOver="javascript:this.style.cursor='hand';"
                                            ValidationGroup="AgregarIncidencia" />
                                        <asp:Button ID="btnCancelarIncidencia" runat="server" CssClass="boton" Text="Cancelar"
                                            onMouseOver="javascript:this.style.cursor='hand';" OnClick="btnCancelarIncidencia_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
