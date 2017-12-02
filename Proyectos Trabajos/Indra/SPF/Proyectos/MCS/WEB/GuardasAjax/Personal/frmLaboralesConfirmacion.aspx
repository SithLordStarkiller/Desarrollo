<%@ Page Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmLaboralesConfirmacion.aspx.cs" Inherits="Personal_frmLaboralesConfirmacion"
    Title="Módulo de Control de Servicios ::: Datos Laborales - Confirmación" %>

<%@ Import Namespace="SICOGUA.Entidades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../Generales/JScript.js"></script>

    <div id="Contenido">
        <asp:HiddenField ID="hfIdEmpleado" runat="server" />
        <table class="tamanio">
            <tr>
                <td class="titulo" colspan="6">
                    Asignaciones</td>
            </tr>
            <tr>
                <td class="subtitulo" colspan="6">
                    Datos Generales
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <table width="100%">
                        <tr>
                            <td class="der">
                                Empleado:
                            </td>
                            <td class="izq">
                                <asp:Label ID="lblNombreEmpleado" runat="server" Text="-">
                                </asp:Label>
                            </td>
                            <td class="der">
                                N° Empleado:
                            </td>
                            <td class="izq">
                                <asp:Label ID="lblNumeroEmpleado" runat="server" Text="-">
                                </asp:Label>
                            </td>
                            <td class="der">
                                CUIP:
                            </td>
                            <td class="izq">
                                <asp:Label ID="lblCUIP" runat="server" Text="-">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="der">
                                Fecha de Alta:
                            </td>
                            <td class="izq" colspan="5">
                                <asp:Label ID="lblFechaAlta" runat="server" Text="-">
                                </asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <br />
                </td>
            </tr>
        </table>
        <table class="tamanio">
            <tr>
                <td class="subtitulo" colspan="6">
                    Datos de la Asignación
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:UpdatePanel ID="updNivelPuesto" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table width="90%">
                                <tr>
                                    <td class="der">
                                        Jerarquía:
                                    </td>
                                    <td class="izq">
                                        <asp:Label ID="lblJerarquia" runat="server" Text="-"></asp:Label>
                                    </td>
                                    <td class="der">
                                        Cargo:
                                    </td>
                                    <td class="izq">
                                        <asp:Label ID="lblPuesto" runat="server" Text="-"></asp:Label>
                                    </td>
                                    <td class="der">
                                        &nbsp;</td>
                                    <td class="izq">
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="updCatalogos" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table>
                    <tr>
                        <td class="der">
                        </td>
                        <td class="izq">
                            <asp:Label ID="lblAgrupamiento" runat="server" Text="-" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="der">
                        </td>
                        <td class="izq">
                            <asp:Label ID="lblCompania" runat="server" Text="-" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="der">
                        </td>
                        <td class="izq">
                            <asp:Label ID="lblSeccion" runat="server" Text="-" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="der">
                        </td>
                        <td class="izq">
                            <asp:Label ID="lblPeloton" runat="server" Text="-" Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
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
                        <asp:GridView ID="grvAsignacion" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            CssClass="texto" ForeColor="#333333" Width="100%" AllowSorting="True">
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

                                                      <asp:TemplateField HeaderText="función">
                                    <ItemTemplate>
                                        <asp:Label ID="lblfuncion" runat="server" Text='<%# Eval("funcionAsignacion") %>'>
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
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
        </table>
        <center>
            <table>
                <tr>
                    <td>
                        <asp:Button ID="btnGuardar" runat="server" Text="Confirmar" CssClass="boton" onMouseOver="javascript:this.style.cursor='hand';"
                            ToolTip="Confirmar" OnClick="btnGuardar_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="boton" onMouseOver="javascript:this.style.cursor='hand';"
                            ToolTip="Regresar" OnClick="btnRegresar_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="boton" onMouseOver="javascript:this.style.cursor='hand';"
                            ToolTip="Cancelar" OnClick="btnCancelar_Click" />
                    </td>
                </tr>
            </table>
        </center>
    </div>
</asp:Content>
