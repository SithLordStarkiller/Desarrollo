<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.Master" AutoEventWireup="true" CodeBehind="OrdenError.aspx.cs" Inherits="PubliPayments.OrdenError" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.10.4/themes/smoothness/jquery-ui.css">
    
    <script src="Scripts/jquery-1.11.0.min.js"></script>
    <script src="//code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
    <style type="text/css">
        .ui-state-default .ui-tabs-anchor {font: 14px 'Open Sans', sans-serif !important;color: #ffffff !important; background: #4A3A41 !important;}
        .ui-state-default .ui-tabs-anchor:hover {color: gray !important;}
        .ui-state-default {border: none !important;}
        .ui-state-active .ui-tabs-anchor {color: #000000 !important;background: #EDEDED !important;}
        .ui-state-active .ui-tabs-anchor:hover {color: #000000 !important;background: #EDEDED !important;}
        .ui-tabs-active {background: #EDEDED !important;}
        .ui-tabs-nav {background: #4A3A41 !important;}
        .ui-tabs {background: #EDEDED !important;border: none;}
        .ui-widget-content {border-top-style: none !important;border-bottom-style: none !important;border-left-style: none !important;border-right-style: none !important;}
        .tabContent td {vertical-align: top;padding: 8px;}
        .tabTemplate,.tabTemplate .label {border-collapse: collapse;font: 12px Tahoma;color: #333333;}.tabTemplate img {margin: 0;display: block;}
        .tabTemplate td {padding: 0;}
        .pcTemplates .dxtc-leftIndent,.pcTemplates .dxtc-tab,.pcTemplates .dxtc-activeTab,.pcTemplates .dxtc-rightIndent {border-width: 0 !important;background-color: white !important;}
        .pcTemplates .dxtc-leftIndent,.pcTemplates .dxtc-tab,.pcTemplates .dxtc-rightIndent {border-bottom-width: 1px !important;}
    </style>

    <script>

        function OnOrdenErrorClick(s, e) {
            var rowIndex = e.visibleIndex;
            OrdenesError.GetRowValues(rowIndex, 'idError', OnGetId);
        }

        function OnGetId(value) {
            window.open("/Ordenes/DescargarError?idError=" + value, "_self");
        }

    </script>

    <div class="TituloSeleccion" id="Principal" style="margin-left: 30px; position: relative; top: 0; left: 0;" runat="server">
            Reporte de errores de órdenes
    </div>
 

    <div style="margin-left: 10px; margin-top: 30px;">
             <dx:ASPxGridView ID="OrdenesError" runat="server" ClientInstanceName="OrdenesError" AutoGenerateColumns="False" Width="1100px"
                        OnDataBinding="OrdenesError_DataBinding" OnCustomButtonInitialize="OrdenesError_CustomButtonInitialize" OnCustomCallback="OrdenesError_CustomCallback">
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="idError" Caption="Identificador de error" ShowInCustomizationForm="True" VisibleIndex="0" Width="100px">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Origen" Caption="Página de origen" ShowInCustomizationForm="True" VisibleIndex="1">
                                <PropertiesTextEdit>
                                    <FocusedStyle HorizontalAlign="Left">
                                    </FocusedStyle>
                                    <Style HorizontalAlign="Left">
                                    </Style>
                                </PropertiesTextEdit>
                                <CellStyle HorizontalAlign="Left">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Accion" Caption="Acción realizada" ShowInCustomizationForm="True" VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Fecha" Caption="Fecha" ShowInCustomizationForm="True" VisibleIndex="3">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewCommandColumn VisibleIndex="4" ButtonType="Button" Caption="Error">
                                <CustomButtons>
                                    <dx:GridViewCommandColumnCustomButton ID="OrdenesError_descargarError" Text="Descargar">
                                    </dx:GridViewCommandColumnCustomButton>
                                </CustomButtons>
                            </dx:GridViewCommandColumn>
                        </Columns>
                        <ClientSideEvents CustomButtonClick="OnOrdenErrorClick" />
                        <SettingsPager PageSize="50" Position="Bottom">
                            <PageSizeItemSettings Items="10, 20, 50, 100" Visible="true" />
                        </SettingsPager>

                        <Styles>
                            <EditFormTable Font-Size="12px">
                            </EditFormTable>
                            <EditFormCell Font-Size="12px">
                            </EditFormCell>
                            <Cell Font-Size="12px">
                            </Cell>
                            <AlternatingRow Enabled="True" />
                        </Styles>
                    </dx:ASPxGridView>
    </div>
</asp:Content>

