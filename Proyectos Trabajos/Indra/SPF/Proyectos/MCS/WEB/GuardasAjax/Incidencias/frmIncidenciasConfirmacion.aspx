<%@ Page Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmIncidenciasConfirmacion.aspx.cs" Inherits="Incidencias_frmIncidenciasConfirmacion"
    Title="Módulo de Control de Servicios ::: Incidencias - Confirmación" %>

<%@ Import Namespace="SICOGUA.Entidades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divMain" class="scrollbar">

        <script language="javascript" type="text/javascript" src="../Generales/JScript.js"></script>

        <asp:Panel ID="panIncidencias" runat="server" Visible="true">
            <table class="tamanio">
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
                        <table width="75%">
                            <tr>
                                <td class="der">
                                    No. de Empleado:
                                </td>
                                <td class="izq">
                                    <asp:Label ID="lblNumero" runat="server" Text="910524"></asp:Label>
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
                        <asp:GridView ID="grvIncidencias" runat="server" AutoGenerateColumns="False" CellPadding="4"
                            ForeColor="#333333" Width="100%">
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
                                        <asp:Label ID="lblIdIncidencia" runat="server" Text='<%# Eval("idIncidencia") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tipo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTipo" runat="server" Text='<%# ((clsEntTipoIncidencia)Eval("tipoIncidencia")).TiDescripcion %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Descripción">
                                    <ItemTemplate>
                                    <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("IncDescripcion") %>'>
                                                </asp:Label>
                                      <%--  <asp:TextBox ID="txbDescripcion" runat="server" Text='<%# Eval("IncDescripcion") %>'
                                            ReadOnly="true" Width="200px" CssClass="texto"></asp:TextBox>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inicio">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInicio" runat="server" Text='<%# Eval("sfechaInicial") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fin">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFin" runat="server" Text='<%# Eval("sfechaFinal") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Persona Que Autoriza">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAutoriza" runat="server" Text='<%# Eval("sEmpleadoAutoriza") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <table align="center">
                            <tr>
                                <td>
                                    <asp:Button ID="btnConfirmar" runat="server" CssClass="boton" Text="Confirmar" OnClick="btnConfirmar_Click"
                                        onMouseOver="javascript:this.style.cursor='hand';" />
                                </td>
                                <td>
                                    <asp:Button ID="btnRegresar" runat="server" CssClass="boton" Text="Regresar" onMouseOver="javascript:this.style.cursor='hand';"
                                        PostBackUrl="~/Incidencias/frmIncidencias.aspx" />
                                </td>
                                <td>
                                    <asp:Button ID="btnCancelar" runat="server" CssClass="boton" Text="Cancelar" OnClick="btnCancelar_Click"
                                        onMouseOver="javascript:this.style.cursor='hand';" />
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
            </table>
            <br />
        </asp:Panel>
    </div>
</asp:Content>
