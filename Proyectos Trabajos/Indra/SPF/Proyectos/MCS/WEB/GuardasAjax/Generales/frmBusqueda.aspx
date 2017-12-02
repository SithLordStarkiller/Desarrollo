<%@ Page Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmBusqueda.aspx.cs" Inherits="SICOGUA.Generales.Escoltas_frmPopUps_frmBusqueda"
    Title="Módulo de Control de Servicios - Búsqueda" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="JScript.js"></script>
    <asp:Panel ID="panBusqueda" runat="server">
        <div id="Contenido" class="scrollbar">
            <div>
                <table class="tamanio">
                    <tr runat="server" id="trParametros">
                        <td>
                            <table class="tamanio">
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
                                                <td colspan="6">
                                                    <asp:RadioButton ID="rbtActivos" runat="server" Text="Activos" CssClass="text" GroupName="Empleados"
                                                        Checked="True"  />
                                                    <asp:RadioButton ID="rbtInactivos" runat="server" Text="Inactivos" CssClass="text"
                                                        GroupName="Empleados" />
                                                    <asp:RadioButton ID="rbtTodos" runat="server" Text="Todos" CssClass="text" GroupName="Empleados" />
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="der">
                                                    Apellido Paterno:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txbPaterno" runat="server" MaxLength="30" onblur="javascript:onLosFocus(this)"
                                                        onfocus="javascript:onFocus(this)" onkeypress="return validar(event)" onchange="this.value=quitaacentos(this.value)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" CssClass="texto" Width="150px"></asp:TextBox>
                                                </td>
                                                <td class="der">
                                                    Apellido Materno:
                                                </td>
                                                <td class="izq">
                                                    <asp:TextBox ID="txbMaterno" runat="server" MaxLength="30"  onblur="javascript:onLosFocus(this)"
                                                        onfocus="javascript:onFocus(this)" onkeypress="return validar(event)" onchange="this.value=quitaacentos(this.value)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" CssClass="texto" Width="150px">
                                                    </asp:TextBox>
                                                </td>
                                                <td class="der">
                                                    Nombre:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txbNombre" runat="server" MaxLength="30" CssClass="textbox" onblur="javascript:onLosFocus(this)"
                                                        onfocus="javascript:onFocus(this)" onkeypress="return validar(event)" onchange="this.value=quitaacentos(this.value)"
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
                                                        onkeypress="return validarNoEscritura(event);" onfocus="javascript:onFocus(this)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" CssClass="textbox" Width="125px">
                                                    </asp:TextBox>
                                                    <asp:ImageButton ID="imbNacimiento" runat="server" Height="16px" ImageUrl="~/Imagenes/Calendar.png"
                                                        Width="16px" />
                                                    <cc1:CalendarExtender ID="calNacimiento" runat="server" PopupButtonID="imbNacimiento"
                                                        TargetControlID="txbNacimiento">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td class="der">
                                                    Fecha Alta:
                                                </td>
                                                <td class="izq">
                                                    <asp:TextBox ID="txbIngreso" runat="server" MaxLength="10" onblur="javascript:onLosFocus(this)"
                                                        onkeypress="return validarNoEscritura(event);" onfocus="javascript:onFocus(this)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" CssClass="textbox" Width="125px">
                                                    </asp:TextBox>
                                                    <asp:ImageButton ID="imbIngreso" runat="server" Height="16px" ImageUrl="~/Imagenes/Calendar.png"
                                                        Width="16px" />
                                                    <cc1:CalendarExtender ID="calIngreso" runat="server" PopupButtonID="imbIngreso" TargetControlID="txbIngreso">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td class="der">
                                                    Fecha Baja:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txbBaja" onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)"
                                                        onkeypress="return validarNoEscritura(event);" runat="server" MaxLength="10"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" CssClass="textbox" Width="125px">
                                                    </asp:TextBox>
                                                    <asp:ImageButton ID="imbCaptura" runat="server" Height="16px" ImageUrl="~/Imagenes/Calendar.png"
                                                        Width="16px" />
                                                    <cc1:CalendarExtender ID="calCaptura" runat="server" PopupButtonID="imbCaptura" TargetControlID="txbBaja">
                                                    </cc1:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="der">
                                                    No Cartilla:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txbCartilla" runat="server" MaxLength="20" CssClass="textbox" onblur="javascript:onLosFocus(this)"
                                                        onfocus="javascript:onFocus(this)" onkeypress="return avalidar(event)" onchange="this.value=quitaacentos(this.value)"
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
                                                    <asp:TextBox ID="txbNumero" runat="server" onkeypress="return nvalidar(event)" MaxLength="10"
                                                        CssClass="textbox" onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" Width="150px">
                                                    </asp:TextBox>
                                                </td>
                                                <td class="der">
                                                    CUIP:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txbCuip" runat="server" MaxLength="22" CssClass="textbox" onblur="javascript:onLosFocus(this)"
                                                        onfocus="javascript:onFocus(this)" onkeypress="return avalidar(event)" onchange="this.value=quitaacentos(this.value)"
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
                                                        onfocus="javascript:onFocus(this)" onkeypress="return avalidar(event)" onKeyUp="this.value=quitaacentos(this.value)"
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
                                                        onfocus="javascript:onFocus(this)" onkeypress="return avalidar(event)" onKeyUp="this.value=quitaacentos(this.value)"
                                                        onKeyDown="if(event.keyCode==13){event.keyCode=9;}" Width="150px">
                                                    </asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="revCURP" runat="server" ControlToValidate="txbCurp"
                                                        CssClass="Validator" ErrorMessage="CURP Inválida." SetFocusOnError="True" ValidationExpression="^[a-zA-Z]{4}((\d{2}((0[13578]|1[02])(0[1-9]|[12]\d|3[01])|(0[13456789]|1[012])(0[1-9]|[12]\d|30)|02(0[1-9]|1\d|2[0-8])))|([02468][048]|[13579][26])0229)(H|M)(AS|BC|BS|CC|CL|CM|CS|CH|DF|DG|GT|GR|HG|JC|MC|MN|MS|NT|NL|OC|PL|QT|QR|SP|SL|SR|TC|TS|TL|VZ|YN|ZS|SM|NE)([a-zA-Z]{3})([a-zA-Z0-9\s]{1})\d{1}$"
                                                        ValidationGroup="Datos">*
                                                    </asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                           <%-- <tr>
                                                <td class="der">
                                                    Tipo de Servicio:
                                                </td>
                                                <td colspan="5">
                                                    <asp:DropDownList ID="ddlTipoServicio" runat="server" CssClass="texto" OnSelectedIndexChanged="ddlTipoServicio_SelectedIndexChanged"
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td class="der">
                                                    Servicio:
                                                </td>
                                                <td colspan="5">
                                                    <asp:DropDownList ID="ddlServicio" runat="server" CssClass="texto" OnSelectedIndexChanged="ddlServicio_SelectedIndexChanged"
                                                        AutoPostBack="True">
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
                                        <asp:UpdatePanel ID="updGrid" runat="server">
                                            <ContentTemplate>
                                                <div id="Grid" class="centro">
                                                    <table class="centro">
                                                        <tr>
                                                            <td class="der">
                                                                <asp:ImageButton ID="imgAtras" runat="server" ImageUrl="~/Imagenes/rewind-icon.png"
                                                                    OnClick="imgAtras_Click" ToolTip="Atrás" />
                                                            </td>
                                                            <td class="cen">
                                                                <asp:GridView ID="grvBusqueda" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                                    CssClass="texto" EmptyDataText="No se encontraron resultados." ForeColor="#333333"
                                                                    Width="100%" OnRowUpdating="grvBusqueda_RowUpdating" AllowSorting="True" 
                                                                    onselectedindexchanged="grvBusqueda_SelectedIndexChanged">
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
            </div>
        </div>
    </asp:Panel>
</asp:Content>
