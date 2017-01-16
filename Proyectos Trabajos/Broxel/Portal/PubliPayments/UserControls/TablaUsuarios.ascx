<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TablaUsuarios.ascx.cs" Inherits="PubliPayments.UserControls.TablaUsuarios" %>
<%@ Import Namespace="System.ComponentModel" %>
<%@ Import Namespace="System.Globalization" %>
<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>



<style type="text/css">
    .TbidUsuarioHidden, .TbEstatusHidden, .TbRolHidden {
        display: none;
    }

    #ContentPlaceHolder1_TablaUsuarios_lbUsuariosLondon input[type=checkbox] {
        position: relative;
        left: 10px;
    }
    .btnBAJA.r3{display: none}
</style>
<script src="../Scripts/restrictors.js?version=3.0"></script>
<asp:TextBox runat="server" ID="NDespacho" CssClass="NDespacho Oculto"></asp:TextBox>
<script type="text/javascript" language="javascript">
    function BotonesAltaBaja() {
        var $tabla = $("#<%=lbUsuariosLondon.ClientID%> tr [id*='DXDataRow']");
        if ($tabla.length > 0) {
            $.each($tabla, function (key, value) {
                if (value.getElementsByClassName("idRolHidden")[0] != undefined) {
                    if (value.getElementsByClassName("idRolHidden")[0].innerText != "3" && value.getElementsByClassName("idRolHidden")[0].innerText != "2" || value.getElementsByClassName("estatusHidden")[0].innerText == "0") {
                        if (value.getElementsByClassName("Reasignar")[0] != undefined)
                            value.getElementsByClassName("Reasignar")[0].innerHTML = "";
                    }
                }
            });
        }
        var _rol = <%=_rol%>;
        $(".btnBAJA").addClass("r" + _rol);
    }

    $(document).ready(function () {
        $("#cbSeleccionarTodo").click(function () {
            $.each($("#ContentPlaceHolder1_TablaUsuarios_lbUsuariosLondon :checkbox"), function (key, value) {
                if ($("#cbSeleccionarTodo").prop('checked')) {
                    value.checked = true;
                }
                else {
                    value.checked = false;
                }
            });
        });

    });

</script>
<div id="SeleccionarTodosTU" style="display: none">
    <div class="Combos" style="height: 19px; width: 140px; display: inline-block; vertical-align: bottom">
        <div style="text-align: right; display: inline-block; vertical-align: top; margin-top: 2px;">Seleccionar página</div>
        <input type="checkbox" id="cbSeleccionarTodo" name="SeleccionarTodo" style="position: relative; top: 4px; margin-left: 5px;" />
    </div>
</div>
<%--<div id="Restablecer" style="width: 200px;">
    <asp:Button runat="server" CssClass="Botones" Text="Restablecer" ID="btLimpiar" Width="100px" Height="30px" OnClick="btLimpiar_OnClick" />
</div>--%>
<div style="text-align: left; width: 100%; margin-top: 10px">
    <asp:Label runat="server" ID="lblInformacionTabla" CssClass="MensajeAlerta ArribaMenos5" Visible="False"></asp:Label>
</div>

<dx:ASPxGridView ID="lbUsuariosLondon" ClientInstanceName="lbUsuariosLondon" runat="server" AutoGenerateColumns="False" CssClass="GridCelda" OnCustomColumnSort="lbUsuariosLondon_CustomColumnSort" KeyFieldName="idUsuario" Width="98%" SettingsPager-PageSize="50">
    <Settings ShowFilterRow="True" ShowGroupPanel="True" />
    <ClientSideEvents EndCallback="function(s,e){BotonesAltaBaja();}" Init="function(s,e){BotonesAltaBaja();}" />
    <SettingsPager Position="Bottom">
        <PageSizeItemSettings Items="10, 20, 50, 100" Visible="true"></PageSizeItemSettings>
    </SettingsPager>
    <Styles>
        <EditFormTable Font-Size="12px"></EditFormTable>
        <EditFormCell Font-Size="12px"></EditFormCell>
        <Cell Font-Size="12px"></Cell>
        <AlternatingRow Enabled="True" />
    </Styles>
    <Columns>
        <dx:GridViewDataTextColumn VisibleIndex="0"  Caption="Selección" Width="50px">
            <DataItemTemplate>
                <div style="margin-left: 15px" ><asp:CheckBox ID="chkSel" runat="server" /></div>
                <div style="display: none">
                    <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"idUsuario") %>' CssClass="idUsuarioHidden" ClientIDMode="AutoID" />
                </div>
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="NombreDominio" Caption="Despacho" Visible="False" VisibleIndex="2">
            <Settings AllowHeaderFilter="False" />
            <Settings AutoFilterCondition="Contains" />
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="Usuario"  Caption="Usuario" VisibleIndex="2">
            <Settings AllowHeaderFilter="False" />
            <Settings AutoFilterCondition="Contains" />
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="Nombre"  Caption="Nombre" VisibleIndex="3">
            <Settings AllowHeaderFilter="False" />
            <Settings AutoFilterCondition="Contains" />
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="Email"  Caption="Email" VisibleIndex="4">
            <Settings AllowHeaderFilter="False" />
            <Settings AutoFilterCondition="Contains" />
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="Padre"  Caption="Asignado" VisibleIndex="5">
            <Settings AutoFilterCondition="Contains" />
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="EstatusTxt"  Caption="Estatus" VisibleIndex="6">
            <Settings AllowHeaderFilter="False" />
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="NombreRol" Caption="Perfil" VisibleIndex="7">
            <Settings AllowHeaderFilter="False" />
            <Settings AutoFilterCondition="Contains" />
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn VisibleIndex="8">
            <DataItemTemplate>
                <div style="padding: 5px; text-align: center;">
                    <dx:ASPxButton ID="btnReasignar" runat="server" Width="130px" Height="30px" Text="Reasignar usuarios" OnClick="btnReasignar_OnClick" CssClass="Botones Reasignar"
                        CommandArgument='<%# DataBinder.Eval(Container.DataItem,"idUsuario") %>' EnableViewState="False" />
                </div>
            </DataItemTemplate>
            <HeaderStyle CssClass="Reasignar"></HeaderStyle>
            <CellStyle CssClass="Reasignar"></CellStyle>
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn VisibleIndex="9" >
            <DataItemTemplate>
                <div style="padding: 5px; text-align: center;">
                    <dx:ASPxButton ID="ASPxButton1" runat="server" ClientIDMode="AutoID" Width="120px" Height="30" Text="Reset Password" OnClick="btnReset_OnClick" CssClass="Botones BotonGris" 
                                   CommandArgument='<%# DataBinder.Eval(Container.DataItem,"idUsuario") %>' EnableViewState="False"><ClientSideEvents Click="function(s,e){e.processOnServer =  BloquearMultiplesClicks(this)}"/></dx:ASPxButton>
                </div>
                <div style="display: none">
                    <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"estatus") %>' CssClass="estatusHidden" ClientIDMode="AutoID" />
                    <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"idRol") %>' CssClass="idRolHidden" ClientIDMode="AutoID" />
                </div>
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn VisibleIndex="10" >
            <DataItemTemplate>
                <div style="padding: 5px; text-align: center;">
                    <dx:ASPxButton ID="ASPxButton2" runat="server" ClientIDMode="AutoID" Width="70px" Height="30px" Text='<%#(DataBinder.Eval(Container.DataItem,"Estatus")).ToString()=="0"?"Alta":"Baja" %>'  OnClick="btnBAJA_OnClick" CssClass="Botones btnBAJA"   
                                   CommandArgument='<%# DataBinder.Eval(Container.DataItem,"idUsuario")+","+DataBinder.Eval(Container.DataItem,"Estatus") %>' EnableViewState="False"><ClientSideEvents Click="function(s,e){e.processOnServer = BloquearMultiplesClicks(this)}"/></dx:ASPxButton>
                    </div>
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>
       
        <dx:GridViewDataTextColumn VisibleIndex='11' >
            <DataItemTemplate>
                <div style="padding: 5px; text-align: center;">
                     <%# Eval("idRol").ToString()!=ObtenerRol().ToString()?
                    "<button style='width: 70px; height: 30px;' class='Botones btEditarUsuario' onclick='editarUsuario(&#39;" + Eval("idUsuario") + "&#39;);'>Editar</button>"
                    :
                    "" %>
                    
                    
                    <%--<button style="width: 70px; height: 30px;" class="Botones btEditarUsuario" onclick="editarUsuario('<%# Eval("Usuario")%>');">Editar</button>--%>
                </div>
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>
    </Columns>
</dx:ASPxGridView>
