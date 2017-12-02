<%@ Page Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmBusquedaIncidentes.aspx.cs" Inherits="Incidentes_frmBusquedaIncidentes"
    Title="Módulo de Control de Servicios ::: Incidentes - Búsqueda" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../Generales/JScript.js"></script>

    <asp:Panel ID="panBusqueda" runat="server" >
        <div id="Contenido" class="scrollbar">
            <div>
                <table class="tamanio">
                    <tr runat="server" id="trParametros">
                        <td>
                            <table class="tamanio">
                                <tr>
                                    <td class="titulo">
                                        Búsqueda de Hechos Ocurridos
                                    </td>
                                </tr>
                                <tr>
                                    <td class="subtitulo">
                                        Criterios de Búsqueda
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="updCriterios" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table class="tamanio">
                                                    <tr>
                                                        <td colspan="6">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der">
                                                            Servicio:
                                                        </td>
                                                        <td colspan="5">
                                                            <asp:DropDownList ID="ddlServicio" runat="server" CssClass="texto" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlServicio_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvServicio" runat="server" ErrorMessage="Debe Seleccionar un Servicio."
                                                                ControlToValidate="ddlServicio" Font-Bold="True" SetFocusOnError="True" ValidationGroup="SICOGUA"
                                                                Text="*">
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der">
                                                            Instalación:
                                                        </td>
                                                        <td class="izq" colspan="5">
                                                            <asp:DropDownList ID="ddlInstalacion" runat="server" CssClass="texto" Enabled="False">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der">
                                                            Apellido Paterno:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txbPaterno" runat="server" MaxLength="30" onblur="javascript:onLosFocus(this)"
                                                                onfocus="javascript:onFocus(this)" onkeypress="return validar(event)" onchange="this.value=quitaacentos(this.value)"
                                                                CssClass="textbox" Width="150px">
                                                            </asp:TextBox>
                                                        </td>
                                                        <td class="der">
                                                            Apellido Materno:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:TextBox ID="txbMaterno" runat="server" MaxLength="30" CssClass="textbox" onblur="javascript:onLosFocus(this)"
                                                                onfocus="javascript:onFocus(this)" onkeypress="return validar(event)" onchange="this.value=quitaacentos(this.value)"
                                                                Width="150px">
                                                            </asp:TextBox>
                                                        </td>
                                                        <td class="der">
                                                            Nombre:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txbNombre" runat="server" MaxLength="30" CssClass="textbox" onblur="javascript:onLosFocus(this)"
                                                                onfocus="javascript:onFocus(this)" onkeypress="return validar(event)" onchange="this.value=quitaacentos(this.value)"
                                                                Width="150px">
                                                            </asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der">
                                                            Fecha:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txbFecha" runat="server" onFocus="javascript:onFocus(this)" onblur="javascript:onLosFocus(this)"
                                                                CssClass="textbox" Width="81px" onkeypress="return validarNoEscritura(event);"></asp:TextBox>
                                                            <asp:ImageButton ID="imbFecha" runat="server" ImageUrl="~/Imagenes/Calendar.png"
                                                                Height="18px" Width="18px" />
                                                            <%--<asp:RequiredFieldValidator ID="rfvFecha" runat="server" ErrorMessage="Se Requiere la Fecha."
                                                                Font-Bold="True" ControlToValidate="txbFecha" SetFocusOnError="True" ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>--%>
                                                            <cc1:CalendarExtender ID="calFecha" runat="server" PopupButtonID="imbFecha" TargetControlID="txbFecha">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                        <td class="der">
                                                            N° Empleado:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txbNumero" runat="server" onkeypress="return nvalidar(event)" MaxLength="10"
                                                                CssClass="textbox" onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)"
                                                                Width="150px">
                                                            </asp:TextBox>
                                                        </td>
                                                        <td class="der">
                                                            <%--Zona:--%>
                                                        </td>
                                                        <td class="izq">
                                                            <asp:DropDownList ID="ddlZona" runat="server" CssClass="texto" AutoPostBack="True" Visible="false">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="der">
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="der">
                                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar Hechos" OnClick="btnBuscar_Click"
                                            CssClass="boton" onMouseOver="javascript:this.style.cursor='hand';" ToolTip="Buscar Incidentes" ValidationGroup="SICOGUA"/>
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
                        <td>
                            <table width="100%">
                                <tr>
                                    <td align="center">
                                        <strong>Número de registros encontrados:
                                            <asp:Label ID="lblCount" runat="server" Text="0"></asp:Label>
                                        </strong>
                                    </td>
                                </tr>
                                <tr runat="server" id="trGrid">
                                    <td>
                                        <div id="Grid">
                                            <table class="centro">
                                                <tr>
                                                    <td class="der">
                                                        <asp:ImageButton ID="imgAtras" runat="server" ImageUrl="~/Imagenes/rewind-icon.png"
                                                            OnClick="imgAtras_Click" ToolTip="Atrás" />
                                                    </td>
                                                    <td class="centro">
                                                        <asp:GridView ID="grvBusqueda" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                            CssClass="texto" EmptyDataText="No se encontraron resultados." ForeColor="#333333"
                                                            Width="100%" OnRowUpdating="grvBusqueda_RowUpdating" AllowSorting="True">
                                                            <Columns>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblIdIncidencia" runat="server" Text='<%# Eval("idIncidente") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Grado">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblGrado" runat="server" Text='<%# Eval("grado") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Nombre">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("nombre") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Servicio">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblServicio" runat="server" Text='<%# Eval("servicio") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Instalación">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblInstalacion" runat="server" Text='<%# Eval("instalacion") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Fecha">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFecha" runat="server" Text='<%# Eval("fecha") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Hora">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblHora" runat="server" Text='<%# Eval("hora") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imbSeleccionar" runat="server" CommandName="Update" ImageUrl="~/Imagenes/Download.png"
                                                                            ToolTip="Modificar" />
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
                                    </td>
                                </tr>
                                <tr>
                                    <td class="der">
                                        <asp:HiddenField ID="hfRedirect" runat="server" />
                                        <asp:HiddenField ID="hfCancel" runat="server" />
                                        <asp:Button ID="btnRegresar" runat="server" CssClass="boton" Text="Regresar" ToolTip="Regresar"
                                            Visible="False" />
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
