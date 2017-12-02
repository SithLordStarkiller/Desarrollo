<%@ Page Title="" Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmAnexoTecnico.aspx.cs" Inherits="Catalogos_frmAnexoTecnico" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Generales/wucMensaje.ascx" TagName="wucMensaje" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../Generales/JScript.js"></script>
    <asp:UpdatePanel ID="upPrincipal" runat="server">
        <ContentTemplate>
            <div id="divMain" class="scrollbar">
                <asp:Panel ID="panEstSoc" runat="server" Visible="true">
                    <table class="tamanio">
                        <tr>
                            <td class="titulo" colspan="4">
                                ANEXO TÉCNICO
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table class="tamanio" border="0">
                                    <tr>
                                        <td colspan="6">
                                            <uc1:wucMensaje ID="wucMensaje" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="subtitulo" colspan="6">
                                            Datos generales de la instalación
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="der" style="width: 129px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 66px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 59px">
                                            &nbsp;
                                        </td>
                                        <td style="width: 82px" class="der">
                                            &nbsp;
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
                                            Zona:
                                        </td>
                                        <td class="izq" colspan="2">
                                            <asp:Label ID="lblZona" runat="server"></asp:Label>
                                        </td>
                                        <td class="der">
                                            &nbsp;
                                        </td>
                                        <td class="der">
                                            Fecha de Inicio de Instalación
                                        </td>
                                        <td class="izq">
                                            <asp:Label ID="lblFechaInicioInstalacion" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="der">
                                            Servicio:
                                        </td>
                                        <td class="izq" colspan="3">
                                            <asp:Label ID="lblServicio" runat="server"></asp:Label>
                                        </td>
                                        <td class="der">
                                            Fecha de Baja de Instalación:
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFechaBajaInstalacion" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="der">
                                            Instalación:
                                        </td>
                                        <td class="izq" colspan="5">
                                            <asp:Label ID="lbInstalacion" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table class="tamanio">
                                    <tr>
                                        <td colspan="2">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="subtitulo" colspan="3">
                                            anexos técnicos</td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Button ID="btnBusqueda" runat="server" Style="visibility: hidden;" Text="" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td class="cen" colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td class="derecha" colspan="2">
                                            <div id="Grid">
                                                <table width="100%">
                                                    <tr>
                                                        <td class="cen">
                                                            <asp:GridView ID="grvAnexo" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                                ForeColor="#333333" PageSize="5" Width="637px" OnRowUpdating="grvAnexo_RowUpdating"
                                                                CssClass="texto" OnRowDataBound="grvAnexo_RowDataBound">
                                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
                                                                <FooterStyle BackColor="#727272" Font-Bold="True" ForeColor="White" />
                                                                <PagerStyle BackColor="#727272" ForeColor="White" HorizontalAlign="Center" />
                                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                <HeaderStyle BackColor="#727272" Font-Bold="True" ForeColor="White" />
                                                                <EditRowStyle BackColor="#999999" />
                                                                <AlternatingRowStyle BackColor="White" ForeColor="#727272" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Numero">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRaiz" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Fecha de Inicio">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblFechaInicio" runat="server" Text='<%# ((DateTime)Eval("fechaInicio")).ToShortDateString() == "01/01/1900"? "" : ((DateTime)Eval("fechaInicio")).ToShortDateString() %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Fecha Fin">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblFechaFin" runat="server" Text='<%# ((DateTime)Eval("fechaFin")).ToShortDateString() == "01/01/1900"? "" : ((DateTime)Eval("fechaFin")).ToShortDateString() %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="No. Convenio">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblVigenteh" runat="server" Text='<%# Eval("strConvenio") %>'>
                                                                            </asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Consulta">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="imbMostrarDatos" runat="server" CommandName="Update" ImageUrl="~/Imagenes/search-icon.png"
                                                                                ToolTip="Consultar / Modificar" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td class="der" colspan="2">
                                            <asp:Button ID="btnNuevoRegistro" runat="server" CssClass="boton" OnClick="btnNuevoRegistro_Click"
                                                onMouseOver="javascript:this.style.cursor='hand';" Text="Nuevo Registro" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnGuardarAnexos" runat="server" CssClass="boton" Text="Guardar"
                                                ValidationGroup="SICOGUA" OnClick="btnGuardarAnexos_Click" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnCancelarAnexos" runat="server" CssClass="boton" Text="Cancelar"
                                                OnClick="btnCancelarAnexos_Click" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnBuscar" runat="server" CssClass="boton" Text="Buscar Instalación"
                                                OnClick="btnBuscar_Click" />
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
    <asp:ModalPopupExtender ID="popDetalle" runat="server" BackgroundCssClass="modalBackground"
        PopupControlID="pnlDetalle" TargetControlID="btnDetalle">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlDetalle" runat="server" DefaultButton="btnDetalle">
        <div style="background-repeat: repeat; background-image: url('../Imagenes/line.png');
            margin: 30px auto 30px auto; border: outset 2px Black; width: 900px;">
            <asp:UpdatePanel ID="upAnexo" runat="server" Visible="True">
                <ContentTemplate>
                    <table class="tamanio">
                        <tr id="trParametros" runat="server">
                            <td>
                                <table class="tamanio" width="900px">
                                    <tr>
                                        <td class="titulo">
                                            Anexo Técnico
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
                                                    <td colspan="4">
                                                        <uc1:wucMensaje ID="wucMensajeAnexo" runat="server" />
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="der">
                                                        Fecha de Inicio:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txbFechaInicioConvenio" runat="server" CssClass="textbox" MaxLength="10"
                                                            onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)" onKeyDown="if(event.keyCode==13){event.keyCode=9;}"
                                                            onkeypress="event.returnValue= validarNoEscritura(event);" Width="125px"></asp:TextBox>
                                                        <asp:ImageButton ID="imbNacimiento" runat="server" Height="16px" ImageUrl="~/Imagenes/Calendar.png"
                                                            Width="16px" />
                                                        <asp:CalendarExtender ID="calNacimiento" runat="server" PopupButtonID="imbNacimiento"
                                                            TargetControlID="txbFechaInicioConvenio">
                                                        </asp:CalendarExtender>
                                                    </td>
                                                    <td class="der">
                                                        &nbsp;</td>
                                                    <td class="izq">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="der">
                                                        No. de Convenio:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txbNoConvenio" runat="server" CssClass="textbox" 
                                                            MaxLength="100" onblur="javascript:onLosFocus(this)" 
                                                            onchange="this.value=quitaacentos(this.value)" 
                                                            onfocus="javascript:onFocus(this)" 
                                                            onKeyDown="if(event.keyCode==13){event.keyCode=9;}" 
                                                            onkeypress="event.returnValue= oficioN(event)" Width="700px"></asp:TextBox>
                                                    </td>
                                                    <td class="der">
                                                        &nbsp;</td>
                                                    <td class="izq">
                                                        &nbsp;</td>
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
                                </table>
                            </td>
                        </tr>
                        <tr id="trResultados" runat="server">
                            <td class="cen">
                                <table class="centro">
                                    <tr id="trGrid" runat="server">
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <div id="Div1" class="centro">
                                                        <table class="centro">
                                                            <tr>
                                                                <td class="der">
                                                                </td>
                                                                <td class="cen">
                                                                    <div id="Layer1" style="width: 800px; height: 300px; overflow: scroll;">
                                                                        <asp:GridView ID="grvJerarquias" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                            CellPadding="4" CssClass="texto" EmptyDataText="No se encontraron resultados."
                                                                            ForeColor="#333333" Width="770px" 
                                                                            OnRowUpdating="grvJerarquias_RowUpdating" OnRowDataBound="grvJerarquias_RowDataBound"
                                                                            OnSelectedIndexChanged="grvJerarquias_SelectedIndexChanged">
                                                                            <%-- OnRowUpdating="grvBusqueda_RowUpdating"--%>
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="No.">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblNumero" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Tipo Horario">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblPaterno" runat="server" Text='<%# Eval("thHorario") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Turno">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblMaterno" runat="server" Text='<%# Eval("thTurno") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Jerarquía">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("jerDescripcion") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Lu">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblLunes" runat="server" Text='<%# Eval("lunes") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Ma">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblMartes" runat="server" Text='<%# Eval("martes") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Mi">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblMiercoles" runat="server" Text='<%# Eval("miercoles") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Ju">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblJueves" runat="server" Text='<%# Eval("jueves") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Vi">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblViernes" runat="server" Text='<%# Eval("viernes") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Sa">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSabado" runat="server" Text='<%# Eval("sabado") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Do">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblDomingo" runat="server" Text='<%# Eval("domingo") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="imbSeleccionar" runat="server" CommandName="Update" ImageUrl="~/Imagenes/Download.png"
                                                                                            ToolTip="Consultar" Visible="false" />
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
                                                                    </div>
                                                                </td>
                                                                <td class="izq">
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
                                            <asp:HiddenField ID="hfNuevoAnexo" runat="server" />
                                            <asp:Button ID="btnNuevo" runat="server" CssClass="boton" onMouseOver="javascript:this.style.cursor='hand';"
                                                Text="Agregar horario" ToolTip="Agregar" OnClick="btnNuevo_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="der">
                                            <table class="cen">
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnGuardar" runat="server" CssClass="boton" Text="Aceptar" ValidationGroup="SICOGUA"
                                                            OnClick="btnGuardar_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnCancelar" runat="server" CssClass="boton" Text="Cancelar" OnClick="btnCancelar_Click" />
                                                    </td>
                                                </tr>
                                            </table>
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
    <asp:Button ID="btnDetalle" runat="server" Text="" Style="visibility: hidden;" class="der" />
    <asp:ModalPopupExtender ID="mpeDetalle" runat="server" BackgroundCssClass="modalBackground"
        PopupControlID="pnlCaptura" TargetControlID="btnCaptura">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlCaptura" runat="server" DefaultButton="btnCaptura">
        <div style="background-repeat: repeat; background-image: url('../Imagenes/line.png');
            margin: 30px auto 30px auto; border: outset 2px Black; width: 900px;">
            <asp:UpdatePanel ID="upHorario" runat="server" Visible="True">
                <ContentTemplate>
                    <table class="tamanio">
                        <tr id="trParametros0" runat="server">
                            <td>
                                <table class="tamanio" width="900px">
                                    <tr>
                                        <td class="titulo">
                                            Datos del Anexo
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="subtitulo">
                                            Criterios de Búsqueda
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <uc1:wucMensaje ID="wucMensajeHorario" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table class="tamanio">
                                                <tr>
                                                    <td class="der" style="width: 310px">
                                                        &nbsp;Tipo Horario:
                                                    </td>
                                                    <td style="width: 180px">
                                                        <asp:DropDownList ID="ddlTipoHorario" runat="server" AutoPostBack="True" CssClass="textbox"
                                                            OnSelectedIndexChanged="ddlTipoHorario_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="der">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="der" style="width: 310px">
                                                        Turno Horario:
                                                    </td>
                                                    <td style="width: 180px">
                                                        <asp:DropDownList ID="ddlTurno" runat="server" CssClass="textbox">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="der">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="der" style="width: 310px">
                                                        Jerarquía:
                                                    </td>
                                                    <td style="width: 180px">
                                                        <asp:DropDownList ID="ddlJerarquia" runat="server" CssClass="textbox">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="der">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="der" colspan="2">
                                                        &nbsp;<asp:CheckBox ID="chbSexo" runat="server" Text="¿No. de Integrantes por Sexo?"
                                                            AutoPostBack="True" OnCheckedChanged="chbSexo_CheckedChanged" />
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="der" style="width: 310px">
                                                        &nbsp;
                                                    </td>
                                                    <td style="width: 180px; margin-left: 440px;">
                                                        &nbsp;
                                                    </td>
                                                    <td class="izq">
                                                        &nbsp;
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
                                            <table>
                                                <tr>
                                                    <td class="der" style="width: 310px">
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblEtiquetaH" runat="server" Text="H"></asp:Label>
                                                    </td>
                                                    <td class="izq">
                                                        <asp:Label ID="lblEtiquetaM" runat="server" Text="M"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center; width: 150px">
                                                        &nbsp;Indistinto
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="der" style="width: 310px">
                                                        Lunes:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txbHombresLunes" runat="server" CssClass="textbox" Enabled="False"
                                                            MaxLength="9" onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)"
                                                            onKeyDown="if(event.keyCode==13){event.keyCode=9;}" onkeypress="event.returnValue= avalidar(event)"
                                                            onKeyUp="this.value=quitaacentos(this.value)" Width="65px"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="Filteredtextboxextender1" runat="server" TargetControlID="txbHombresLunes"
                                                            FilterType="Custom, Numbers" ValidChars="0123456789" />
                                                    </td>
                                                    <td class="izq">
                                                        <asp:TextBox ID="txbMujeresLunes" runat="server" CssClass="textbox" Enabled="False"
                                                            MaxLength="9" onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)"
                                                            onKeyDown="if(event.keyCode==13){event.keyCode=9;}" onkeypress="event.returnValue= avalidar(event)"
                                                            onKeyUp="this.value=quitaacentos(this.value)" Width="65px"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="Filteredtextboxextender2" runat="server" TargetControlID="txbMujeresLunes"
                                                            FilterType="Custom, Numbers" ValidChars="0123456789" />
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txbIndistintoLunes" runat="server" CssClass="textbox" MaxLength="9"
                                                            onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)" onKeyDown="if(event.keyCode==13){event.keyCode=9;}"
                                                            onkeypress="event.returnValue= avalidar(event)" onKeyUp="this.value=quitaacentos(this.value)"
                                                            Width="65px"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="Filteredtextboxextender3" runat="server" TargetControlID="txbIndistintoLunes"
                                                            FilterType="Custom, Numbers" ValidChars="0123456789" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="der" style="width: 310px">
                                                        Martes:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txbHombresMartes" runat="server" CssClass="textbox" MaxLength="9"
                                                            onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)" onKeyDown="if(event.keyCode==13){event.keyCode=9;}"
                                                            onkeypress="event.returnValue= avalidar(event)" onKeyUp="this.value=quitaacentos(this.value)"
                                                            Width="65px" Enabled="False"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="Filteredtextboxextender4" runat="server" TargetControlID="txbHombresMartes"
                                                            FilterType="Custom, Numbers" ValidChars="0123456789" />
                                                    </td>
                                                    <td class="izq">
                                                        <asp:TextBox ID="txbMujeresMartes" runat="server" CssClass="textbox" MaxLength="9"
                                                            onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)" onKeyDown="if(event.keyCode==13){event.keyCode=9;}"
                                                            onkeypress="event.returnValue= avalidar(event)" onKeyUp="this.value=quitaacentos(this.value)"
                                                            Width="65px" Enabled="False"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="Filteredtextboxextender5" runat="server" TargetControlID="txbMujeresMartes"
                                                            FilterType="Custom, Numbers" ValidChars="0123456789" />
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txbIndistintoMartes" runat="server" CssClass="textbox" MaxLength="9"
                                                            onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)" onKeyDown="if(event.keyCode==13){event.keyCode=9;}"
                                                            onkeypress="event.returnValue= avalidar(event)" onKeyUp="this.value=quitaacentos(this.value)"
                                                            Width="65px"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="Filteredtextboxextender6" runat="server" TargetControlID="txbIndistintoMartes"
                                                            FilterType="Custom, Numbers" ValidChars="0123456789" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="der" style="width: 310px; height: 22px;">
                                                        Miércoles:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txbHombresMiercoles" runat="server" CssClass="textbox" MaxLength="9"
                                                            onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)" onKeyDown="if(event.keyCode==13){event.keyCode=9;}"
                                                            onkeypress="event.returnValue= avalidar(event)" onKeyUp="this.value=quitaacentos(this.value)"
                                                            Width="65px" Enabled="False"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="Filteredtextboxextender7" runat="server" TargetControlID="txbHombresMiercoles"
                                                            FilterType="Custom, Numbers" ValidChars="0123456789" />
                                                    </td>
                                                    <td class="izq" style="height: 22px">
                                                        <asp:TextBox ID="txbMujeresMiercoles" runat="server" CssClass="textbox" MaxLength="9"
                                                            onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)" onKeyDown="if(event.keyCode==13){event.keyCode=9;}"
                                                            onkeypress="event.returnValue= avalidar(event)" onKeyUp="this.value=quitaacentos(this.value)"
                                                            Width="65px" Enabled="False"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="Filteredtextboxextender8" runat="server" TargetControlID="txbMujeresMiercoles"
                                                            FilterType="Custom, Numbers" ValidChars="0123456789" />
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txbIndistintoMiercoles" runat="server" CssClass="textbox" MaxLength="9"
                                                            onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)" onKeyDown="if(event.keyCode==13){event.keyCode=9;}"
                                                            onkeypress="event.returnValue= avalidar(event)" onKeyUp="this.value=quitaacentos(this.value)"
                                                            Width="65px"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="Filteredtextboxextender9" runat="server" TargetControlID="txbIndistintoMiercoles"
                                                            FilterType="Custom, Numbers" ValidChars="0123456789" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="der" style="width: 310px">
                                                        Jueves:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txbHombresJueves" runat="server" CssClass="textbox" MaxLength="9"
                                                            onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)" onKeyDown="if(event.keyCode==13){event.keyCode=9;}"
                                                            onkeypress="event.returnValue= avalidar(event)" onKeyUp="this.value=quitaacentos(this.value)"
                                                            Width="65px" Enabled="False"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="Filteredtextboxextender10" runat="server" TargetControlID="txbHombresJueves"
                                                            FilterType="Custom, Numbers" ValidChars="0123456789" />
                                                    </td>
                                                    <td class="izq">
                                                        <asp:TextBox ID="txbMujeresJueves" runat="server" CssClass="textbox" MaxLength="9"
                                                            onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)" onKeyDown="if(event.keyCode==13){event.keyCode=9;}"
                                                            onkeypress="event.returnValue= avalidar(event)" onKeyUp="this.value=quitaacentos(this.value)"
                                                            Width="65px" Enabled="False"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="Filteredtextboxextender11" runat="server" TargetControlID="txbMujeresJueves"
                                                            FilterType="Custom, Numbers" ValidChars="0123456789" />
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txbIndistintoJueves" runat="server" CssClass="textbox" MaxLength="9"
                                                            onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)" onKeyDown="if(event.keyCode==13){event.keyCode=9;}"
                                                            onkeypress="event.returnValue= avalidar(event)" onKeyUp="this.value=quitaacentos(this.value)"
                                                            Width="65px"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="Filteredtextboxextender12" runat="server" TargetControlID="txbIndistintoJueves"
                                                            FilterType="Custom, Numbers" ValidChars="0123456789" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="der" style="width: 310px">
                                                        Viernes:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txbHombresViernes" runat="server" CssClass="textbox" MaxLength="9"
                                                            onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)" onKeyDown="if(event.keyCode==13){event.keyCode=9;}"
                                                            onkeypress="event.returnValue= avalidar(event)" onKeyUp="this.value=quitaacentos(this.value)"
                                                            Width="65px" Enabled="False"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="Filteredtextboxextender13" runat="server" TargetControlID="txbHombresViernes"
                                                            FilterType="Custom, Numbers" ValidChars="0123456789" />
                                                    </td>
                                                    <td class="izq">
                                                        <asp:TextBox ID="txbMujeresViernes" runat="server" CssClass="textbox" MaxLength="9"
                                                            onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)" onKeyDown="if(event.keyCode==13){event.keyCode=9;}"
                                                            onkeypress="event.returnValue= avalidar(event)" onKeyUp="this.value=quitaacentos(this.value)"
                                                            Width="65px" Enabled="False"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="Filteredtextboxextender14" runat="server" TargetControlID="txbMujeresViernes"
                                                            FilterType="Custom, Numbers" ValidChars="0123456789" />
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txbIndistintoViernes" runat="server" CssClass="textbox" MaxLength="9"
                                                            onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)" onKeyDown="if(event.keyCode==13){event.keyCode=9;}"
                                                            onkeypress="event.returnValue= avalidar(event)" onKeyUp="this.value=quitaacentos(this.value)"
                                                            Width="65px"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="Filteredtextboxextender15" runat="server" TargetControlID="txbIndistintoViernes"
                                                            FilterType="Custom, Numbers" ValidChars="0123456789" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="der" style="width: 310px">
                                                        Sábado:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txbHombresSabado" runat="server" CssClass="textbox" MaxLength="9"
                                                            onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)" onKeyDown="if(event.keyCode==13){event.keyCode=9;}"
                                                            onkeypress="event.returnValue= avalidar(event)" onKeyUp="this.value=quitaacentos(this.value)"
                                                            Width="65px" Enabled="False"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="Filteredtextboxextender16" runat="server" TargetControlID="txbHombresSabado"
                                                            FilterType="Custom, Numbers" ValidChars="0123456789" />
                                                    </td>
                                                    <td class="izq">
                                                        <asp:TextBox ID="txbMujeresSabado" runat="server" CssClass="textbox" MaxLength="9"
                                                            onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)" onKeyDown="if(event.keyCode==13){event.keyCode=9;}"
                                                            onkeypress="event.returnValue= avalidar(event)" onKeyUp="this.value=quitaacentos(this.value)"
                                                            Width="65px" Enabled="False"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="Filteredtextboxextender17" runat="server" TargetControlID="txbMujeresSabado"
                                                            FilterType="Custom, Numbers" ValidChars="0123456789" />
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txbIndistintoSabado" runat="server" CssClass="textbox" MaxLength="9"
                                                            onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)" onKeyDown="if(event.keyCode==13){event.keyCode=9;}"
                                                            onkeypress="event.returnValue= avalidar(event)" onKeyUp="this.value=quitaacentos(this.value)"
                                                            Width="65px"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="Filteredtextboxextender18" runat="server" TargetControlID="txbIndistintoSabado"
                                                            FilterType="Custom, Numbers" ValidChars="0123456789" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="der" style="width: 310px">
                                                        Domingo:
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txbHombresDomingo" runat="server" CssClass="textbox" MaxLength="9"
                                                            onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)" onKeyDown="if(event.keyCode==13){event.keyCode=9;}"
                                                            onkeypress="event.returnValue= avalidar(event)" onKeyUp="this.value=quitaacentos(this.value)"
                                                            Width="65px" Enabled="False"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="Filteredtextboxextender19" runat="server" TargetControlID="txbHombresDomingo"
                                                            FilterType="Custom, Numbers" ValidChars="0123456789" />
                                                    </td>
                                                    <td class="izq">
                                                        <asp:TextBox ID="txbMujeresDomingo" runat="server" CssClass="textbox" MaxLength="9"
                                                            onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)" onKeyDown="if(event.keyCode==13){event.keyCode=9;}"
                                                            onkeypress="event.returnValue= avalidar(event)" onKeyUp="this.value=quitaacentos(this.value)"
                                                            Width="65px" Enabled="False"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="Filteredtextboxextender20" runat="server" TargetControlID="txbMujeresDomingo"
                                                            FilterType="Custom, Numbers" ValidChars="0123456789" />
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txbIndistintoDomingo" runat="server" CssClass="textbox" MaxLength="9"
                                                            onblur="javascript:onLosFocus(this)" onfocus="javascript:onFocus(this)" onKeyDown="if(event.keyCode==13){event.keyCode=9;}"
                                                            onkeypress="event.returnValue= avalidar(event)" onKeyUp="this.value=quitaacentos(this.value)"
                                                            Width="65px"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="Filteredtextboxextender21" runat="server" TargetControlID="txbIndistintoDomingo"
                                                            FilterType="Custom, Numbers" ValidChars="0123456789" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="der">
                                            <asp:HiddenField ID="hNuevoDetalle" runat="server" />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="der">
                                            <asp:Button ID="btnAceptarCaptura" runat="server" CssClass="boton" onMouseOver="javascript:this.style.cursor='hand';"
                                                Text="Aceptar" ToolTip="Aceptar" OnClick="btnAceptarCaptura_Click" />
                                            <asp:Button ID="btnCancelar1" runat="server" CssClass="boton" onMouseOver="javascript:this.style.cursor='hand';"
                                                Text="Cancelar" ToolTip="Cancelar" OnClick="btnCancelar1_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="trResultados0" runat="server">
                            <td class="cen">
                                <table class="centro">
                                    <tr>
                                        <td class="der">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="chbSexo" EventName="CheckedChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </asp:Panel>
    <asp:Button ID="btnCaptura" runat="server" Style="visibility: hidden;" Text="" />
</asp:Content>
