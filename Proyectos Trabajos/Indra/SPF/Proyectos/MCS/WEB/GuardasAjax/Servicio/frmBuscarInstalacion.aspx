<%@ Page Title="" Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmBuscarInstalacion.aspx.cs" Inherits="Servicio_frmBuscarInstalacion" %>
<%@ Import Namespace="SICOGUA.Entidades" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../Generales/JScript.js"></script>
    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
        <ContentTemplate>
            <div id="divMain" class="scrollbar">
                 <asp:Panel ID="panBusIntalacion" runat="server" Visible="true">
                    <table class="tamanio">
                        <tr>
                            <td class="titulo" colspan="4">
                                BUSCAR INSTALACIÓN
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table class="tamanio" border="0">
                                    <tr>
                                        <td colspan="5">
                                        &nbsp;
                                        </td>
                                        <td>
                                        &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="subtitulo" colspan="6">
                                            UBICACIÓN
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
                                        <td class="der" colspan="3">
                                        Servicio:
                                        </td>
                                        <td class="izq" colspan="3">
                                            <asp:DropDownList ID="ddlServicio" runat="server" CssClass="texto" AutoPostBack="True"
                                                onselectedindexchanged="ddlServicio_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="der" colspan="3">
                                        Instalación:
                                        </td>
                                   
                                        <td class="izq" colspan="3">
                                            <asp:DropDownList ID="ddlInstalacion" runat="server" CssClass="texto">
                                            </asp:DropDownList> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="der" colspan="3">
                                            Estatus Instalación:
                                        </td>
                                        <td>
                                              <asp:RadioButton ID="rbtVigente" runat="server" CssClass="text" 
                                                GroupName="Instalacion" Text="Vigentes" Checked="True" />
                                               <asp:RadioButton ID="rbtNoVigente" runat="server" CssClass="text" 
                                                                GroupName="Instalacion" Text="No vigentes" /> 
                                               <asp:RadioButton ID="rbtAmbos" runat="server" CssClass="text" 
                                                                GroupName="Instalacion" Text="Ambos" /> 
                                         </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                        </td>
                                    </tr>
                                    <tr>
                                    <td colspan="4">&nbsp</td>
                                    <td colspan="2" class="izq">
                                        <table class="der">
                                            <tr>
                                            <td> &nbsp;</td>
                                                <td class="der" >
                                                         <asp:Button ID="btnBuscar" runat="server" Text="Buscar Instalación" OnClick="btnBuscar_Click"
                                                        CssClass="boton" onMouseOver="javascript:this.style.cursor='hand';" ToolTip="Buscar "
                                                        ValidationGroup="Datos" />
                                                </td>
                                                <td class="der" colspan="3">
                                                     <asp:Button ID="btnNueva" runat="server" Text="Nueva Búsqueda" 
                                                        CssClass="boton" onMouseOver="javascript:this.style.cursor='hand';" ToolTip="Nueva Búsqueda "
                                                        ValidationGroup="Datos" onclick="btnNueva_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    </tr>
                                    <tr>
                                         <td>&nbsp</td>
                                         <td colspan="5" class="cen">
                                               <div id="Grid">
                                                     <table width="100%">
                                                     <tr>
                                                        <td align="center" colspan="5">
                                                                <strong>Número de registros encontrados:
                                                                <asp:Label ID="lblCount" runat="server" Text="0"></asp:Label>
                                                            </strong>
                                                        </td>
                                                     </tr>
                                                        <tr runat="server" id="trGrid">
                                                         <%-- <td class="der" >
                                                             <asp:ImageButton ID="imgAtras" runat="server" ImageUrl="~/Imagenes/rewind-icon.png"
                                                                    ToolTip="Atrás" onclick="imgAtras_Click" />
                                                            </td>--%>
                                                       
                                                            <td class="cen">
                                                                    <asp:GridView ID="grvBusqueda" runat="server" AutoGenerateColumns="False" 
                                                                    CellPadding="4" ForeColor="#333333"  
                                                                    PageSize="5" Width="637px"  CssClass="texto" EmptyDataText="No se encontraron resultados."
                                                                     onRowUpdating="grvBusqueda_RowUpdating">
                                                                      <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
                                                                    <EmptyDataRowStyle Font-Names="Verdana" Font-Size="Medium" ForeColor="Navy" HorizontalAlign="Center"
                                                                        VerticalAlign="Middle" />
                                                                    <FooterStyle BackColor="#727272" Font-Bold="True" ForeColor="White" />
                                                                    <PagerStyle BackColor="#727272" ForeColor="White" HorizontalAlign="Center" />
                                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                    <HeaderStyle BackColor="#727272" Font-Bold="True" ForeColor="White" />
                                                                    <EditRowStyle BackColor="#999999" />
                                                                    <AlternatingRowStyle BackColor="White" ForeColor="#727272" />
                                                                        <Columns>
                                                                               <asp:TemplateField Visible="False">
                                                                                 <ItemTemplate>
                                                                                 <asp:Label ID="lblServicio" runat="server" Text='<%# ((clsEntServicio)Eval("Servicio")).idServicio %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                             </asp:TemplateField>
                                                                               <asp:TemplateField Visible="False">
                                                                                 <ItemTemplate>
                                                                                 <asp:Label ID="lblInstalacion" runat="server" Text='<%# Eval("idInstalacion") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                             </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Numero">
                                                                                 <ItemTemplate>
                                                                                     <asp:Label ID="lblRaiz" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Zona">
                                                                                 <ItemTemplate>
                                                                                     <asp:Label ID="lblZona" runat="server" Text='<%# ((clsEntZona)Eval("Zona")).ZonDescripcion%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Servicio">
                                                                                 <ItemTemplate>
                                                                                     <asp:Label ID="lblServ" runat="server" Text='<%#((clsEntServicio)Eval("Servicio")).serDescripcion %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Instalacion">
                                                                                 <ItemTemplate>
                                                                                     <asp:Label ID="lblInstal" runat="server" Text='<%# Eval("insNombre") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Vigente">
                                                                                 <ItemTemplate>
                                                                                     <asp:Label ID="lblVigente" runat="server" Text='<%# string.Format("{0}", (bool)Eval("insVigente") ? "SI" : "NO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField HeaderText="Seleccionar">
                                                                              <ItemTemplate>
                                                                                <asp:ImageButton ID="imbSeleccionar" runat="server" CommandName="Update" ImageUrl="~/Imagenes/Download.png"
                                                                                    ToolTip="Consultar" />
                                                                                </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                   <%-- <center>
                                                                    <strong>
                                                                        <asp:Label ID="lblPagina" runat="server" Text="0"></asp:Label>
                                                                        de
                                                                        <asp:Label ID="lblPaginas" runat="server" Text="0"></asp:Label>
                                                                    </strong>
                                                                </center>--%>
                                                            </td>
                                                          <%--  <td class="izq">
                                                                <asp:ImageButton ID="imgAdelante" runat="server" ImageUrl="~/Imagenes/forward-icon.png"
                                                                    ToolTip="Siguiente" onclick="imgAdelante_Click" />
                                                            </td>--%>
                                                        </tr>
                                                     </table>
                                               </div>
                                         </td>
                                    </tr>
                                    <tr>
                                        <td class="der" colspan="5">
                                            <asp:Button ID="btnCerrar" runat="server" CssClass="boton"
                                            Text="Cerrar" ToolTip="Regresar" onclick="btnCerrar_Click"   />
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
</asp:Content>
