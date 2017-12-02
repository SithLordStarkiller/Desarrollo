<%@ Page Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmBusquedaListaAsistencia.aspx.cs" Inherits="PaseLista_frmBusquedaListaAsistencia"
    Title="Módulo de Control de Servicios ::: Lista de Asistencia - Búsqueda" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../Generales/JScript.js"></script>
    <asp:Panel ID="panBusqueda" runat="server">
        <div id="Contenido" class="scrollbar">
            <div>
                <table class="tamanio">
                    <tr runat="server" id="trParametros">
                        <td>
                            <table class="tamanio">
                                <tr>
                                    <td class="titulo">
                                        Búsqueda de Lista de Asistencia
                                    </td>
                                </tr>
                                <tr>
                                    <td class="subtitulo">
                                        Criterios de Búsqueda
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="updFiltros" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table class="tamanio">
                                                    <tr>
                                                        <td class="der">
                                                            Fecha de Pase de Lista:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:TextBox ID="txbFechaPaseLista" runat="server" onFocus="javascript:onFocus(this)"
                                                                onblur="javascript:onLosFocus(this)" onkeypress="return fvalidar(event);" CssClass="textbox"
                                                                Width="120px" MaxLength="10"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="calIngreso" runat="server" TargetControlID="txbFechaPaseLista"
                                                                PopupButtonID="imbFechaPaseLista">
                                                            </cc1:CalendarExtender>
                                                            <asp:ImageButton ID="imbFechaPaseLista" runat="server" Height="18px" ImageUrl="~/Imagenes/Calendar.png"
                                                                Width="18px" />
                                                            <asp:CompareValidator ID="covFechaPaseLista" runat="server" ErrorMessage="Fecha No Válida."
                                                                ControlToValidate="txbFechaPaseLista" Font-Bold="True" SetFocusOnError="True"
                                                                Type="Date" ValidationGroup="SICOGUA" ValueToCompare="01/01/0001" Operator="LessThanEqual">*</asp:CompareValidator>
                                                            <asp:RegularExpressionValidator ID="revFecha" runat="server" ErrorMessage="Formato de Fecha Inválido."
                                                                ControlToValidate="txbFechaPaseLista" Font-Bold="True" ValidationExpression="^([0][1-9]|[12][0-9]|3[01])(/|-)(0[1-9]|1[012])\2(\d{4})$"
                                                                SetFocusOnError="True" ValidationGroup="SICOGUA">*</asp:RegularExpressionValidator>
                                                            <cc1:TextBoxWatermarkExtender ID="twaFechaPaseLista" runat="server" TargetControlID="txbFechaPaseLista"
                                                                WatermarkText="dd/mm/aaaa">
                                                            </cc1:TextBoxWatermarkExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der">
                                                            Hora del Pase de Lista:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:TextBox ID="txbHoraPaseLista" runat="server" onFocus="javascript:onFocus(this)"
                                                                onblur="javascript:onLosFocus(this)" onkeypress="return hvalidar(event);" CssClass="textbox"
                                                                Width="120px" MaxLength="5"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="revHoraPaseLista" runat="server" ErrorMessage="Hora No Válida."
                                                                ControlToValidate="txbHoraPaseLista" Font-Bold="True" SetFocusOnError="True"
                                                                ValidationExpression="([0-1]\d|2[0-3]):([0-5]\d)" ValidationGroup="SICOGUA">*</asp:RegularExpressionValidator>
                                                            <cc1:TextBoxWatermarkExtender ID="twaHoraPaseLista" runat="server" TargetControlID="txbHoraPaseLista"
                                                                WatermarkText="hh:mm">
                                                            </cc1:TextBoxWatermarkExtender>
                                                        </td>
                                                    </tr>
                                                    <%--<tr>
                                                        <td class="der">
                                                            Zona:</td>
                                                        <td class="izq" colspan="3">
                                                            <asp:DropDownList ID="ddlZona" runat="server" CssClass="textbox" AutoPostBack="false"
                                                                >
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>--%>
                                                    <%--  <tr>
                                                        <td class="der">
                                                        </td>
                                                        <td class="izq" colspan="3">
                                                            <asp:DropDownList ID="ddlAgrupamiento" runat="server" CssClass="textbox" AutoPostBack="true"
                                                                Enabled="False" OnSelectedIndexChanged="ddlAgrupamiento_SelectedIndexChanged"
                                                                Visible="false">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der">
                                                        </td>
                                                        <td class="izq" colspan="3">
                                                            <asp:DropDownList ID="ddlCompania" runat="server" CssClass="textbox" AutoPostBack="true"
                                                                Enabled="False" OnSelectedIndexChanged="ddlCompania_SelectedIndexChanged" Visible="false">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der">
                                                        </td>
                                                        <td class="izq" colspan="3">
                                                            <asp:DropDownList ID="ddlSeccion" runat="server" CssClass="textbox" AutoPostBack="true"
                                                                Enabled="False" OnSelectedIndexChanged="ddlSeccion_SelectedIndexChanged" Visible="false">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der">
                                                        </td>
                                                        <td class="izq" colspan="3">
                                                            <asp:DropDownList ID="ddlPeloton" runat="server" CssClass="textbox" AutoPostBack="false"
                                                                Visible="false" Enabled="False">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>--%>
                                                    <tr>
                                                        <td class="der">
                                                            Servicio:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:DropDownList ID="ddlServicio" runat="server" CssClass="textbox" AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlServicio_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="der">
                                                            Instalación:
                                                        </td>
                                                        <td class="izq">
                                                            <asp:DropDownList ID="ddlInstalacion" runat="server" CssClass="textbox" AutoPostBack="true"
                                                                Enabled="false">
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
                                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar Lista de Asistencia" OnClick="btnBuscar_Click"
                                            CssClass="boton" onMouseOver="javascript:this.style.cursor='hand';" ToolTip="Buscar Lista de Asistencia"
                                            ValidationGroup="SICOGUA" />
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
                    <tr runat="server" id="trResultados" visible="false">
                        <td>
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
                                        <div id="Grid">
                                            <table class="centro">
                                                <tr>
                                                    <td class="der">
                                                        <asp:ImageButton ID="imgAtras" runat="server" ImageUrl="~/Imagenes/rewind-icon.png"
                                                            ToolTip="Atrás" OnClick="imgAtras_Click" />
                                                    </td>
                                                    <td class="centro">
                                                        <asp:GridView ID="grvAsistencia" runat="server" CssClass="texto" AutoGenerateColumns="False"
                                                            CellPadding="4" ForeColor="#333333" Font-Bold="False" 
                                                            EmptyDataText="No se ha encontrado ningún registro." Width="100%">
                                                            <Columns>
                                            <asp:TemplateField HeaderText="No" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNo" runat="server" Text='<%# Eval("conteo") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                           
                                                                <asp:TemplateField HeaderText="idPaseLista" Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblIdPaseLista" runat="server" Text='<%# Eval("idPaseLista") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Nombre">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("empNombre") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                       
                                                                <%--<asp:TemplateField HeaderText="Agrupamiento">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAgrupamiento" runat="server" Text='<%# Eval("agrupamiento") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Compa&#241;ia">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCompania" runat="server" Text='<%# Eval("compania") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Secci&#243;n">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSeccion" runat="server" Text='<%# Eval("seccion") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Pelot&#243;n">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPeloton" runat="server" Text='<%# Eval("peloton") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                                <asp:TemplateField HeaderText="Servicio">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblServicio" runat="server" Text='<%# Eval("servicio") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Instalaci&#243;n">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblInstalacion" runat="server" Text='<%# Eval("instalacion") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Estatus">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEstatus" runat="server" Text='<%# Eval("asistencia") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ControlStyle />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Fecha">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFecha" runat="server" Text='<%# Eval("plFecha") %>'></asp:Label>
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
                                                            ToolTip="Siguiente" OnClick="imgAdelante_Click" />
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
