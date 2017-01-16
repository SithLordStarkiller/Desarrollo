

<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.Master" AutoEventWireup="true" CodeBehind="Archivos.aspx.cs" Inherits="PubliPayments.Archivos" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.10.4/themes/smoothness/jquery-ui.css">
    
    <script src="Scripts/jquery-1.11.0.min.js"></script>
    <script src="//code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
    <style type="text/css">
        .ui-state-default .ui-tabs-anchor {
            font: 14px 'Open Sans', sans-serif !important;
            color: #ffffff !important; 
            background: #4A3A41 !important;
        }

            .ui-state-default .ui-tabs-anchor:hover {
                color: gray !important;
            }

        .ui-state-default {
            border: none !important;
        }

        .ui-state-active .ui-tabs-anchor {
            color: #000000 !important;
            background: #EDEDED !important;
        }

            .ui-state-active .ui-tabs-anchor:hover {
                color: #000000 !important;
                background: #EDEDED !important;
            }

        .ui-tabs-active {
            background: #EDEDED !important;
        }

        .ui-tabs-nav {
            background: #4A3A41 !important;
        }

        .ui-tabs {
            background: #EDEDED !important;
            border: none;
        }

       
        .ui-widget-content {
            border-top-style: none !important;
            border-bottom-style: none !important;
            border-left-style: none !important;
            border-right-style: none !important;
        }




            .tabContent td {
                vertical-align: top;
                padding: 8px;
            }

        .tabTemplate,
        .tabTemplate .label {
            border-collapse: collapse;
            font: 12px Tahoma;
            color: #333333;
        }

            .tabTemplate img {
                margin: 0;
                display: block;
            }

            .tabTemplate td {
                padding: 0;
            }

        .pcTemplates .dxtc-leftIndent,
        .pcTemplates .dxtc-tab,
        .pcTemplates .dxtc-activeTab,
        .pcTemplates .dxtc-rightIndent {
            border-width: 0 !important;
            background-color: white !important;
        }

        .pcTemplates .dxtc-leftIndent,
        .pcTemplates .dxtc-tab,
        .pcTemplates .dxtc-rightIndent {
            border-bottom-width: 1px !important;
        }
    </style>

    <script>
        $(function () {
            $("#tabs").tabs();
        });

        function OnRarErrorClick(s, e) {
            var rowIndex = e.visibleIndex;
            gridRar.GetRowValues(rowIndex, 'id', OnGetId);
            console.log(rowIndex);
        }

        function OnTxtErrorClick(s, e) {
            var rowIndex = e.visibleIndex;
            gridTxt.GetRowValues(rowIndex, 'id', OnGetId);
        }

        function OnGetId(value) {

            window.open("/ArchivoError/Descargar?arg=" + value, "_self");
        }

        function DownloadError(data, status) {
            alert(data);
        }

        function Actualizar() {
            gridTxt.PerformCallback();
            gridRar.PerformCallback();
        }
    </script>

    <div class="TituloSeleccion" id="Principal" style="margin-left: 30px; position: relative; top: 0; left: 0;" runat="server">
            Cargar Archivo<asp:Image ID="Image1" runat="server" AlternateText="|" ImageUrl="~/imagenes/separador.png" Style="margin-left: 30px; margin-right: 30px; position: relative; top: 10px;" />
        <input id="btActualiza" type="button" value="Actualizar"  class="Botones" style="width: 150px;height: 30px" onclick="return Actualizar()" />
    </div>
 

    <div style="margin-left: 10px; margin-top: 30px;">
        <div id="tabs">
            <ul>
                <li><a href="#tabsTxt">TXT</a></li>
                <li><a href="#tabsRar">ZIP</a></li>

            </ul>

            <div id="tabsTxt">
                <div id="txtContainer" style="padding-left: 15px">
                    <dx:ASPxGridView ID="gridTxt" runat="server" ClientInstanceName="gridTxt" AutoGenerateColumns="False"
                        OnDataBinding="gridTxt_DataBinding" OnCustomButtonInitialize="gridTxt_CustomButtonInitialize" OnCustomCallback="gridTxt_CustomCallback">
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="id" ShowInCustomizationForm="True" VisibleIndex="0">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Archivo" ShowInCustomizationForm="True" VisibleIndex="1">
                                <PropertiesTextEdit>
                                    <FocusedStyle HorizontalAlign="Left">
                                    </FocusedStyle>
                                    <Style HorizontalAlign="Left">
                                    </Style>
                                </PropertiesTextEdit>
                                <CellStyle HorizontalAlign="Left">
                                </CellStyle>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Tipo" ShowInCustomizationForm="True" VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Registros" ShowInCustomizationForm="True" VisibleIndex="3">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Fecha" ShowInCustomizationForm="True" VisibleIndex="4">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Tiempo" ShowInCustomizationForm="true" VisibleIndex="5">

                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Estatus" ShowInCustomizationForm="True" VisibleIndex="6">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Error" ShowInCustomizationForm="True" VisibleIndex="7" Visible="false">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewCommandColumn VisibleIndex="8" ButtonType="Button" Caption="Error">
                                <CustomButtons>
                                    <dx:GridViewCommandColumnCustomButton ID="gridTxt_descargarError" Text="Descargar">
                                    </dx:GridViewCommandColumnCustomButton>
                                </CustomButtons>
                            </dx:GridViewCommandColumn>
                        </Columns>
                        <ClientSideEvents CustomButtonClick="OnTxtErrorClick" />
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
                    <asp:SqlDataSource ID="SqlTXTSource" runat="server"></asp:SqlDataSource>

                </div>
            </div>
            <div id="tabsRar">
                <div id="containerRar" style="padding-left: 15px">
                    <dx:ASPxGridView ID="gridRar" runat="server" CssClass="GridCelda" AutoGenerateColumns="False" ClientInstanceName="gridRar"
                        OnDataBinding="gridRar_DataBinding" OnCustomButtonInitialize="gridRar_CustomButtonInitialize">
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="id" ShowInCustomizationForm="True" VisibleIndex="0">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Archivo" ShowInCustomizationForm="True" VisibleIndex="1">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Tipo" ShowInCustomizationForm="True" VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Registros" ShowInCustomizationForm="True" VisibleIndex="3" Visible="false">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Tiempo" ShowInCustomizationForm="true" VisibleIndex="4">

                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Fecha" ShowInCustomizationForm="True" VisibleIndex="5">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Estatus" ShowInCustomizationForm="True" VisibleIndex="6">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Error" ShowInCustomizationForm="True" VisibleIndex="7" Visible="false">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewCommandColumn VisibleIndex="7" ButtonType="Button" Caption="Error">
                                <CustomButtons>
                                    <dx:GridViewCommandColumnCustomButton ID="btnDescargarError" Text="Descargar">
                                    </dx:GridViewCommandColumnCustomButton>
                                </CustomButtons>
                            </dx:GridViewCommandColumn>
                        </Columns>
                        <ClientSideEvents CustomButtonClick="OnRarErrorClick" />
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
            </div>
        </div>  
    </div>
</asp:Content>
