<%@ Page Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmCatalogos.aspx.cs" Inherits="Catalogos_frmCatalogos" Title="Módulo de Control de Servicios ::: Catálogos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../Generales/JScript.js"></script>

    <div id="divMain" class="scrollbar">
        <asp:Panel ID="panEstSoc" runat="server" Visible="true">
            <table class="tamanio">
                <tr>
                    <td class="titulo" colspan="4">
                        Catálogos
                    </td>
                </tr>
                <tr>
                    <td class="subtitulo" colspan="4">
                        <asp:Label ID="lblCatalogo" runat="server"></asp:Label>
                        <asp:HiddenField ID="hfCatalogo" runat="server" />
                        <asp:HiddenField ID="hfIdCatalogo" runat="server" />
                        <asp:HiddenField ID="hfIdServicio" runat="server" />                        
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <div class="Catalogos">
                            <br />
                            <asp:Menu ID="mnuCatalogos" runat="server" Font-Bold="True" Orientation="Horizontal"
                                Width="100%" OnMenuItemClick="mnuCatalogos_MenuItemClick">
                                <Items>
                                    <asp:MenuItem Text="SERVICIO" Value="1" Target="Servicio"></asp:MenuItem>
                                    <asp:MenuItem Text="INSTALACI&#211;N" Value="2" Target="Instalaci&#243;n"></asp:MenuItem>
                                    <asp:MenuItem Text="INCIDENCIA" Value="3" Target="Incidencia"></asp:MenuItem>
                                </Items>
                            </asp:Menu>
                            <br />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <br />
                    </td>
                </tr>
                <tr runat="server" id="trDescripcion" visible="false">
                    <td colspan="4">
                        <table style="margin: 0 auto 0 auto;">
                            <tr runat="server" id="trRaiz" visible="false">
                                <td class="der">
                                    <asp:Label ID="lblRaíz" runat="server" Text="Raíz"></asp:Label>
                                </td>
                                <td class="izq">
                                    <asp:DropDownList ID="ddlRaiz" runat="server" CssClass="texto">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvRaiz" runat="server" ErrorMessage="Debe Seleccionar una Raíz."
                                        ControlToValidate="ddlRaiz" Font-Bold="True" SetFocusOnError="True" ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="der">
                                    <asp:Label ID="lblDescripcion" runat="server" Text=""></asp:Label>
                                </td>
                                <td class="izq">
                                    <asp:TextBox ID="txbDescripcion" runat="server" Width="250px" onFocus="javascript:onFocus(this)"
                                         onchange="this.value=quitaacentos(this.value);"
                                        onblur="javascript:onLosFocus(this)" CssClass="textbox" MaxLength="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ErrorMessage="Debe Escribir una Descripción."
                                        ControlToValidate="txbDescripcion" Font-Bold="True" SetFocusOnError="True" ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class="der">
                                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="boton" ValidationGroup="SICOGUA"
                                        onMouseOver="javascript:this.style.cursor='hand';" ToolTip="Guardar" OnClick="btnGuardar_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <br />
                                </td>
                            </tr>
                            <tr runat="server" id="trResultados">
                                <td colspan="2">
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
                                                    <table width="100%">
                                                        <tr>
                                                            <td class="der">
                                                                <asp:ImageButton ID="imgAtras" runat="server" ImageUrl="~/Imagenes/rewind-icon.png"
                                                                    OnClick="imgAtras_Click" ToolTip="Atrás" />
                                                            </td>
                                                            <td class="cen">
                                                                <asp:GridView ID="grvCatalogo" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                                    ForeColor="#333333" Width="100%" OnDataBound="grvCatalogo_DataBound" OnRowDeleting="grvCatalogo_RowDeleting"
                                                                    OnRowUpdating="grvCatalogo_RowUpdating" PageSize="5">
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
                                                                                <asp:Label ID="lblIndice" runat="server" Text="-"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField Visible="False">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblIdCatalogo" runat="server" Text='<%# Eval("idCatalogo") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Ra&#237;z">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRaiz" runat="server" Text='<%# Eval("catRaiz") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Descripci&#243;n">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("catDescripcion") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Modificar">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="imbSeleccionar" runat="server" CommandName="Update" ImageUrl="~/Imagenes/Download.png"
                                                                                    ToolTip="Modificar" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Eliminar">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="imbEliminar" runat="server" ImageUrl="~/Imagenes/Symbol-Delete-Mini.png"
                                                                                    ToolTip="Eliminar" CommandName="Delete" OnClientClick="if(!confirm('El registro será eliminado al guardar los cambios, ¿Desea continuar?')) return false;" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
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
                                    </table>
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
