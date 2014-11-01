﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="UnabledOldEquipmentUploadImage.aspx.cs"
    Inherits="PAEEEM.DisposalModule.UnabledOldEquipmentUploadImage" Title="Carga de Fotografía Equipo Inhabilitado" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<link href="../Resources/Css/Pager.css" rel="stylesheet" type="text/css" />
    <link href="../Resources/Css/GridView.css" rel="stylesheet" type="text/css" />

    <script src="../Resources/Script/Calendar/WdatePicker.js" type="text/javascript"></script>

    <link href="../Resources/Script/Calendar/skin/default/datepicker.css" type="text/css" />
    <style type="text/css">
        .Label
        {
            width: 100px;
            color: #333333;
            font-size: 16px;
        }
        .DropDownList
        {
            width: 250px;
        }
        .Button
        {
            width: 120px;
        }
        .style1
        {
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<asp:UpdatePanel ID="panel" runat="server">
        <ContentTemplate>
            <div>
         
         			<br>
            		<asp:Image runat="server" ImageUrl="../images/busqueda_equipos_inhabilitados1.png" />
                          
                </div>
            <br />
            <table width="100%">
                <tr>
                    <td>
                        <asp:Label ID="lblProgram" runat="server" CssClass="Label" Text="Programa" />
                    </td>
                    <td>
                        <asp:DropDownList ID="drpProgram" runat="server" AutoPostBack="true" CssClass="DropDownList"
                            OnSelectedIndexChanged="drpProgram_SelectedIndexChanged" />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="drpProgram" ErrorMessage="(*) Campo Vacío o con Formato Inválido"></asp:RequiredFieldValidator>
                    </td>
                    <td colspan="3" style="text-align: center">
                        <asp:Literal ID="literal1" runat='server' Text="Fecha Recepción" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <asp:Label ID="lblCredit" runat="server" CssClass="Label" Text="No. Crédito" />
                        </div>
                    </td>
                    <td>
                        <div>
                            <asp:DropDownList ID="drpCredit" runat="server" AutoPostBack="true" CssClass="DropDownList"
                                OnSelectedIndexChanged="drpCredit_SelectedIndexChanged" />
                        </div>
                    </td>
                    <td style="text-align: right">
                        <div>
                            <asp:Label ID="lblFromDate" Text="Desde" runat="server" CssClass="Label" /></div>
                    </td>
                    <td>
                        <div>
                            <asp:TextBox ID="txtFromDate" runat="server"  Width="200px" onFocus="WdatePicker({maxDate:'#F{$dp.$D(\'ctl00_MainContent_txtToDate\')}'})" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',lang:'en'})"></asp:TextBox>
                        </div>
                    </td>
                    <td style="text-align: center">
                        <div>
                            <asp:Label ID="lblToDate" Text="Hasta" runat="server" CssClass="Label" /></div>
                    </td>
                    <td>
                        <div>
                            <asp:TextBox ID="txtToDate" runat="server"  Width="200px" onFocus="WdatePicker({minDate:'#F{$dp.$D(\'ctl00_MainContent_txtFromDate\')}'})" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',lang:'en'})"></asp:TextBox>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <asp:Label ID="lblInteralCode" runat="server" CssClass="Label" Text="Folio Ingreso" />
                        </div>
                    </td>
                    <td>
                        <div>
                            <asp:DropDownList ID="drpInteralCode" runat="server" CssClass="DropDownList" />
                        </div>
                    </td>
                    <td>
                    </td>
                    <td colspan="3" style="text-align: center">
                        <asp:Label ID="lblInhabilitacion" runat="server" Text="Fecha Inhabilitación"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <asp:Label ID="lblComfirm" runat="server" CssClass="Label" Text="Con/Sin Foto Asociada" />
                        </div>
                    </td>
                    <td>
                        <div>
                            <asp:DropDownList ID="drpType" runat="server" CssClass="DropDownList">
                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                <asp:ListItem Text="Sin" Value="0" />
                                <asp:ListItem Text="Con" Value="1" />
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td style="text-align: right">
                        <div>
                            <asp:Label ID="lblInFromDate" Text="Desde" runat="server" CssClass="Label" /></div>
                    </td>
                    <td>
                        <div>
                            <asp:TextBox ID="txtInFromDate" runat="server" Width="200px" onFocus="WdatePicker({maxDate:'#F{$dp.$D(\'ctl00_MainContent_txtInToDate\')}'})"  onclick="WdatePicker({dateFmt:'yyyy-MM-dd',lang:'en'})"></asp:TextBox>
                        </div>
                    </td>
                    <td style="text-align: center">
                        <div>
                            <asp:Label ID="lblInToDate" Text="Hasta" runat="server" CssClass="Label" /></div>
                    </td>
                    <td>
                        <div>
                            <asp:TextBox ID="txtInToDate" runat="server" Width="200px" onFocus="WdatePicker({minDate:'#F{$dp.$D(\'ctl00_MainContent_txtInFromDate\')}'})" onclick="WdatePicker({dateFmt:'yyyy-MM-dd',lang:'en'})"></asp:TextBox>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <asp:Label ID="lblDistributor" runat="server" CssClass="Label" Text="Proveedor" />
                        </div>
                    </td>
                    <td>
                        <div>
                            <asp:DropDownList ID="drpDistributor" runat="server" CssClass="DropDownList" 
                                onselectedindexchanged="drpDistributor_SelectedIndexChanged" AutoPostBack="true" />
                        </div>
                    </td>
                    <td>
                        <asp:Label ID="lblTechnology" runat="server" CssClass="Label" Text="Tecnología" />
                    </td>
                    <td>
                        <asp:DropDownList ID="drpTechnology" runat="server" CssClass="DropDownList" 
                            onselectedindexchanged="drpTechnology_SelectedIndexChanged" AutoPostBack="true" />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="drpTechnology" ErrorMessage="(*) Campo Vacío o con Formato Inválido"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td style="text-align: right">
                        <%--<asp:Button ID="btnSearch" runat="server" Text="Buscar" CssClass="Button" OnClick="btnSearch_Click" />--%></td>
                    <td>
                    </td>
                    <td style="text-align: left">
                        <%--<asp:Button ID="btnCancel" runat="server" Text="Salir" CssClass="Button" OnClientClick="return confirm('¿ Desea Salir de ésta Pantalla ?')"
                            OnClick="btnCancel_Click" />--%></td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <div align="center">
        <asp:Button ID="btnFindAll" runat="server" Text="Buscar Todos" 
            CssClass="Button" OnClick="btnFindAll_Click" />&nbsp;&nbsp;&nbsp;&nbsp
        <asp:Button ID="btnSearch" runat="server" Text="Buscar" CssClass="Button" OnClick="btnSearch_Click" />&nbsp;&nbsp;&nbsp;&nbsp
        <asp:Button ID="btnCancel" runat="server" Text="Salir" CssClass="Button" OnClientClick="return confirm('¿ Desea Salir de ésta Pantalla ?')"
                            OnClick="btnCancel_Click" />
    </div>
    <div>
        <asp:GridView ID="grdReceivedOldEquipment" runat="server" AutoGenerateColumns="False"
            CssClass="GridViewStyle" AllowPaging="True" PageSize="20" DataKeyNames="Id_Credito_Sustitucion,IsUpload"
            OnDataBound="grdReceivedOldEquipment_DataBound">
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField DataField="No_Credito" HeaderText="No. Crédito"></asp:BoundField>
                <asp:BoundField DataField="Id_Folio" HeaderText="Folio Ingreso" />
                <asp:BoundField DataField="ProviderComercialName" HeaderText="Proveedor" />
                <asp:BoundField DataField="Dx_Nombre_Programa" HeaderText="Programa" />
                <asp:BoundField DataField="Dx_Nombre_General" HeaderText="Tecnología" />
                <asp:BoundField DataField="No_Capacidad" HeaderText="Capacidad"></asp:BoundField>
                <asp:BoundField DataField="Dx_Marca" HeaderText="Marca"></asp:BoundField>
                <asp:BoundField DataField="Dt_Fecha_Recepcion" HeaderText="Fecha Recepción" DataFormatString="{0:dd-MM-yyyy}" />
                <asp:BoundField DataField="Dt_Fecha_Inhabilitacion" HeaderText="Fecha Inhabilitación"
                    DataFormatString="{0:dd-MM-yyyy}" />
                <asp:TemplateField HeaderText="Con Foto">
                    <ItemTemplate>
                        <asp:Image ID="imgFoto" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ver Foto">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkShowImage" runat="server" OnClick="linkShowImage_Click">Ver</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Seleccionar Archivo">
                    <ItemTemplate>
                        <asp:FileUpload ID="fldSelect" runat="server" Width="8px" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cargar">
                    <ItemTemplate>
                        <asp:Button ID="btnUpLaod" runat="server" Text="Cargar" OnClientClick="return confirm('Confirmar Cargar Foto y Asociar al Equipo Seleccionado')"
                            OnClick="btnUpLaod_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerTemplate>
            </PagerTemplate>
        </asp:GridView>
        <webdiyer:AspNetPager ID="AspNetPager" CssClass="pagerDRUPAL" runat="server" PageSize="20"
            AlwaysShow="True" ShowCustomInfoSection="Right" ShowDisabledButtons="true" ShowPageIndexBox="always"
            PageIndexBoxType="DropDownList" CustomInfoHTML="Page:<font color='red'><b>%currentPageIndex%</b></font>/%PageCount%&nbsp;&nbsp;PageSize:%PageSize%&nbsp;&nbsp;Record:%StartRecordIndex%-%EndRecordIndex% of %RecordCount%"
            UrlPaging="false" OnPageChanged="AspNetPager_PageChanged" OnPageChanging="AspNetPager_PageChanging"
            FirstPageText="Primero" LastPageText="Último" NextPageText="Siguiente" PrevPageText="Anterior"
            CurrentPageButtonClass="cpb">
        </webdiyer:AspNetPager>
    </div>
</asp:Content>