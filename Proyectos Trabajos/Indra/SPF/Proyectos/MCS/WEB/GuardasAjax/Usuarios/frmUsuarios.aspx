<%@ Page Language="C#" MasterPageFile="~/Generales/MasterPage.master" AutoEventWireup="true"
    CodeFile="frmUsuarios.aspx.cs" Inherits="Usuarios_frmUsuarios" Title="Módulo de Control de Servicios ::: Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../Generales/JScript.js"></script>
    <div id="Contenido" class="scrollbar">
        <table class="tamanio">
            <tr>
                <td colspan="6" class="titulo">
                    Usuarios
                </td>
            </tr>
            <tr>
                <td colspan="6" class="subtitulo">
                    Crear Usuario
                    <asp:HiddenField ID="hfIdUsuario" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:UpdatePanel ID="updOk" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="divOk" runat="server" id="divOk" visible="false">
                                <p>
                                    &nbsp;<asp:Label ID="lblOk" runat="server" Text=""></asp:Label>
                                </p>
                            </div>
                            <asp:Timer ID="timOk" runat="server" Enabled="false" Interval="3000" OnTick="timOk_Tick">
                            </asp:Timer>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="updError" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="divError" runat="server" id="divError" visible="false">
                                <p>
                                    &nbsp;<asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                                </p>
                            </div>
                            <asp:Timer ID="timError" runat="server" Enabled="false" Interval="3000" OnTick="timError_Tick">
                            </asp:Timer>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td colspan="6" runat="server" id="trDatos" visible="false">
                    <table width="100%">
                        <tr>
                            <td colspan="6">
                                <table style="width: 90%;">
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
                                    <tr>
                                        <td class="der">
                                            Sexo:
                                        </td>
                                        <td class="izq" colspan="5">
                                            <asp:Label ID="lblSexo" runat="server" Text="-"></asp:Label>
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
                        <tr>
                            <td colspan="6">
                                <table style="margin: 0 auto 0 auto; width: 100%;">
                                    <tr>
                                        <td class="der">
                                            Nombre de Usuario:
                                        </td>
                                        <td class="izq">
                                            <asp:TextBox ID="txbUsuario" runat="server" onFocus="javascript:onFocus(this)" onblur="javascript:onLostFocus(this)"
                                                CssClass="textbox" MaxLength="50" onkeypress="return uvalidar(event);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" ErrorMessage="Se Requiere el Nombre de Usuario."
                                                ControlToValidate="txbUsuario" CssClass="Validator" SetFocusOnError="True" ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td class="der" runat="server" id="trdContrasenia">
                                            Contraseña:
                                        </td>
                                        <td class="izq" runat="server" id="triContrasenia">
                                            <asp:TextBox ID="txbContrasenia" runat="server" onFocus="javascript:onFocus(this)"
                                                onblur="javascript:onLostFocus(this)" CssClass="textbox" MaxLength="50" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvContrasenia" runat="server" ErrorMessage="Se Requiere la Contraseña."
                                                ControlToValidate="txbContrasenia" CssClass="Validator" SetFocusOnError="True"
                                                ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td class="der" runat="server" id="trdConfirmacion">
                                            Confirmar Contraseña:
                                        </td>
                                        <td class="izq" runat="server" id="triConfirmacion">
                                            <asp:TextBox ID="txbConfirmacion" runat="server" onFocus="javascript:onFocus(this)"
                                                onblur="javascript:onLostFocus(this)" CssClass="textbox" MaxLength="50" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvConfirmacion" runat="server" ErrorMessage="Se Requiere la Confirmación de Contraseña."
                                                ControlToValidate="txbConfirmacion" CssClass="Validator" SetFocusOnError="True"
                                                ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="covContrasenia" runat="server" ErrorMessage="No coinciden las Contraseñas."
                                                ControlToCompare="txbContrasenia" ControlToValidate="txbConfirmacion" CssClass="Validator"
                                                SetFocusOnError="True" ValidationGroup="SICOGUA">*</asp:CompareValidator>
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
                        <tr>
                            <td colspan="6">
                                <table style="margin: 0 auto 0 auto;">
                                    <tr>
                                        <td class="der">
                                            Perfil:
                                        </td>
                                        <td class="izq" colspan="5">
                                            <asp:DropDownList ID="ddlPerfil" runat="server" CssClass="texto">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvPerfil" runat="server" ErrorMessage="Se Requiere el Perfil."
                                                ControlToValidate="ddlPerfil" CssClass="Validator" SetFocusOnError="True" ValidationGroup="SICOGUA">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="der">
                                            Permisos:
                                        </td>
                                        <td class="izq" colspan="5">
                                            <asp:Button runat="server" ID="btnAgregarServicioInstalacion" Text="Agregar Permisos"
                                                CssClass="boton" OnClick="btnAgregarServicioInstalacion_Click" />
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
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:GridView ID="grvUsuario" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" Width="100%" OnDataBound="grvUsuario_DataBound" OnRowDeleting="grvUsuario_RowDeleting"
                        OnRowUpdating="grvUsuario_RowUpdating">
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
                        <FooterStyle BackColor="#727272" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#727272" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#727272" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#999999" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#727272" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblIndice" runat="server" Text="-"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblIdUsuario" runat="server" Text='<%# Eval("idUsuario") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ap. Paterno">
                                <ItemTemplate>
                                    <asp:Label ID="lblPaterno" runat="server" Text='<%# Eval("empPaterno") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ap. Materno">
                                <ItemTemplate>
                                    <asp:Label ID="lblMaterno" runat="server" Text='<%# Eval("empMaterno") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre">
                                <ItemTemplate>
                                    <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("empNombre") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Usuario">
                                <ItemTemplate>
                                    <asp:Label ID="lblUsuario" runat="server" Text='<%# Eval("usuLogin") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sexo" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSexo" runat="server" Text='<%# Eval("empSexo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Perfil">
                                <ItemTemplate>
                                    <asp:Label ID="lblPerfil" runat="server" Text='<%# Eval("perDescripcion") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="idPerfil" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblIdPerfil" runat="server" Text='<%# Eval("idPerfil") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imbSeleccionar" runat="server" CommandName="Update" ImageUrl="~/Imagenes/Download.png"
                                        ToolTip="Modificar" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imbEliminar" runat="server" ImageUrl="~/Imagenes/Symbol-Delete-Mini.png"
                                        ToolTip="Eliminar" CommandName="Delete" OnClientClick="if(!confirm('El registro será eliminado al guardar los cambios, ¿Desea continuar?')) return false;" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <table style="margin: 0 auto 0 auto;">
                        <tr>
                            <td>
                                <asp:Button ID="btnNuevo" runat="server" Text="Nuevo Usuario" class="boton" onMouseOver="javascript:this.style.cursor='hand';"
                                    ToolTip="Nuevo Usuario" OnClick="btnNuevo_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnGuardar" runat="server" Text="Guardar Usuario" class="boton" ValidationGroup="SICOGUA"
                                    onMouseOver="javascript:this.style.cursor='hand';" ToolTip="Guardar Usuario"
                                    Visible="false" OnClick="btnGuardar_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" class="boton" onMouseOver="javascript:this.style.cursor='hand';"
                                    ToolTip="Cancelar" OnClick="btnCancelar_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
