<%@ Page Title="Asigna Ordenes" Language="C#" MasterPageFile="masterpage.Master" AutoEventWireup="true" CodeBehind="AsignaOrdenes.aspx.cs" Inherits="PubliPayments.AsignaOrdenes" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Src="~/UserControls/AsignacionManual.ascx" TagPrefix="uc1" TagName="AsignacionManual" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnNada">
        <style>
            #outerDiv {
                overflow: hidden;
                width: 148px;
                height: 100px;
            }

            #popupDiv {
                overflow: scroll;
                width: 148px;
                height: 117px;
            }

            #AreasTexto div {
                float: left;
            }

            #Resultado {
                width: 100%;
                overflow-y: scroll;
                overflow-x: scroll;
            }

            #TCreditosManual {
                width: 100%;
            }

                #TCreditosManual th, #TCreditosManual td {
                    border: 1px black solid;
                }

            .BtnAsignarManual, #Cargar {
                width: 100px;
                height: 30px;
                margin-top: 10px;
            }

            .BtnAsignarManual {
                margin-top: 32px;
            }

            .FooterCargaM {
                width: 50%;
                text-align: center;
                height: 30px;
                margin-top: 20px;
                float: left;
            }
        </style>
        <script src="./Scripts/restrictors.js?version=3.0"></script>
        <script src="./Scripts/OrdenesController.js?version=1.2"></script>

        <script type="text/javascript">
            function grid_SelectionChanged(s, e) {
                //SelectAllCheckbox.SetChecked(s.GetSelectedRowCount() >= s.cpVisibleRowCount());
                s.GetSelectedFieldValues("num_cred;idOrden", GetSelectedFieldValueCallback);

            }

            function InvisiSort(s, e) {
                ASPxGridView1.SortBy(ASPxGridView1.GetColumnByField('nom_corto'));
            }

            function GetSelectedFieldValueCallback(values) {
                //var node = document.getElementById("popupDiv");
                var ordenes = [];
                var _idRol = "<%=HttpContext.Current.User.IsInRole("3")?"3":(HttpContext.Current.User.IsInRole("2") || HttpContext.Current.User.IsInRole("0"))?"2":"-1"%>";
                for (var i = 0; i < values.length; i++) {
                    if (_idRol == "3") {
                        if (ordenes.length < MaxGestor) {
                            ordenes.push([values[i][1], "1"]);
                        }

                    } else if (_idRol == "2") {
                        if (ordenes.length < MaxSuper) {
                            ordenes.push([values[i][0], "1"]);
                        }
                    }

                }

                ArrIdOrdenes = ordenes;
                var node = document.getElementById("popupDiv");
                while (node.firstChild) {
                    node.removeChild(node.firstChild);
                }

                var limit = (values.length > 100) ? 101 : values.length;

                for (var i = 0; i < limit; i++) {
                    var node = document.createElement("P");
                    var textNode = document.createTextNode(values[i][0]);
                    node.appendChild(textNode);
                    document.getElementById("popupDiv").appendChild(node);
                    if (i >= 100) {
                        var nodeMessage = document.createElement("P");
                        var messageContent = document.createTextNode("...");
                        nodeMessage.appendChild(messageContent);
                        document.getElementById("popupDiv").appendChild(nodeMessage);
                    }

                }

                document.getElementById("selCount").innerHTML = ASPxGridView1.GetSelectedRowCount();
                //ASPxGridView1.PerformCallback();
            }

            function OnAllCheckedChanged(s, e) {
                if (s.GetChecked()) {
                    ASPxGridView1.SelectRows();
                    SelectPageCheckbox.SetChecked(true);
                }
                else {
                    ASPxGridView1.UnselectRows();
                    SelectPageCheckbox.SetChecked(false);
                }
            }

            function seleccionarExpandidas(s, e) {
                if (s.GetChecked())
                    $(".btnHack").click();
                else
                    ASPxGridView1.UnselectRows();
            }

            function CheckAll(Checkbox) {
                ASPxGridView1.SelectAllRowsOnPage();

            }

            function btDescargar_OnClick() {
                window.open('/OrdenesNoAsignadas/Descargar?idUsuarioPadre=' + '<%=Session["idPadre"] %>&&idDominio=' + '<%=Session["idDominio"] %>' + "&&tipoFormulario=" + '<%=FormularioList.SelectedValue%>', "_self");
            }
        </script>

        <script>
            $(document).ready(function () {
                $(".wrapper1").scroll(function () {
                    $(".wrapper2")
                        .scrollLeft($(".wrapper1").scrollLeft());
                });
                $(".wrapper2").scroll(function () {
                    $(".wrapper1")
                        .scrollLeft($(".wrapper2").scrollLeft());
                });
                $(".CerrarPopUp").on("click", function () {
                    $(".PopUpMovimientos .dxWeb_pcCloseButton").click();
                    $(".PopUpMovimientos .dxpc-closeBtn").click();
                    return false;
                });
                UrlAjaxAssignment = '/Ordenes/AsignarGestorCreditos';
                ContainerPage = ["Asignar Órdenes", "1", $(".FormularioList").val()];
                $(".ddlGestores").change(function () {
                    NewManager = $(this).val();
                });
            });
        </script>
        <dx:ASPxPopupControl ID="ASPxPopupControl1" runat="server"
            PopupAction="MouseOver" PopupElementID="countDiv"
            ShowHeader="true" Width="100px" PopupHorizontalAlign="LeftSides"
            PopupVerticalAlign="Above" PopupVerticalOffset="-10"
            PopupHorizontalOffset="-10" CloseAction="MouseOut">
            <HeaderContentTemplate>
                <div>Elementos Seleccionados</div>
            </HeaderContentTemplate>
            <ContentCollection>

                <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                    <div id="outerDiv">
                        <div id="popupDiv">
                        </div>
                    </div>

                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
        <div style="margin-left: 30px; position: relative">



            <div id="cabecero" style="position: relative; text-align: left;">
                <div id="tituloPagina" class="TituloSeleccion" style="display: inline-block; vertical-align: top;">
                    Asignar Órdenes<asp:Image ID="Image2" runat="server" AlternateText="|" ImageUrl="~/imagenes/separador.png" Style="margin-left: 15px; margin-right: 15px; position: relative; top: 10px;" />
                    <br />
                    <asp:DropDownList ID="FormularioList" Style="margin-top: 12px;" runat="server" Height="30px" Width="227px" DataTextField="Descripcion" CssClass="Combos FormularioList" DataValueField="Ruta" AutoPostBack="True" OnSelectedIndexChanged="FormularioList_OnSelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="TituloSeleccion" style="height: 96px; display: inline-block; vertical-align: middle; width: auto;">
                    <div class="Combos" style="height: 19px; width: 140px; display: inline-block; vertical-align: bottom">
                        <div style="text-align: right; display: inline-block; vertical-align: top; margin-top: 2px;">Seleccionar página</div>
                        <dx:ASPxCheckBox ID="SelectPageCheckbox" ClientInstanceName="SelectPageCheckbox" runat="server" ToolTip="Selecciona todas las filas en la página"
                            ClientSideEvents-CheckedChanged="function(s,e) { ASPxGridView1.SelectAllRowsOnPage(s.GetChecked()); }" />
                    </div>
                    <div class="Combos" style="height: 19px; width: 165px; display: inline-block; vertical-align: bottom">
                        <div style="text-align: right; display: inline-block; vertical-align: top; margin-top: 2px;">Seleccionar todos</div>
                        <dx:ASPxCheckBox ID="SelectAllCheckbox" runat="server" ClientInstanceName="SelectAllCheckbox"
                            OnInit="checkAll_init" ToolTip="Selecciona todas las filas">
                            <ClientSideEvents CheckedChanged="OnAllCheckedChanged" />
                        </dx:ASPxCheckBox>
                    </div>
                    <asp:DropDownList ID="ddlUsuarios" runat="server" Height="30px" Width="150px" DataTextField="Usuario" CssClass="Combos ddlGestores" DataValueField="idUsuario"></asp:DropDownList>
                    <div id="countDiv" style="text-align: right; display: inline-block; vertical-align: top; margin-top: 20px; font-size: 12px">Entradas seleccionadas: <span id="selCount" style="font-weight: bold">0</span></div>

                    <% if (AplicacionActual.Contains("SIRA") && HttpContext.Current.User.IsInRole("3")){%>
                               <button id="btAsignarP" style="height: 30px;width: 172px" class="Botones MovType11 ExtraSIRAProg">Asignar programadas</button>
                               <button id="btAsignarNP" style="height: 30px;width: 172px" class="Botones MovType11 ExtraSIRANOProg">Asignar no programadas</button>
                    <%}else{%>
                               <button id="btAsignar" style="height: 30px;width: 152px" class="Botones MovType11">Asignar</button>
                    <%}%>
                    <br/>                
                   <div style="margin-top: 10px; display: inline-block; text-align: left;">
                          <asp:Button ID="btnLimpiar" Style="margin-bottom: 10px" runat="server" Height="30px" Text="Restablecer" Width="153px" OnClick="btLimpiar_Clear" CssClass="Botones btnLimpiar" />
                       <% if (!AplicacionActual.Contains("SIRA")){%>
                                <asp:Button runat="server" CssClass="Botones" Text="Descargar créditos" ID="btDescargar" Width="175px" Height="30px" OnClientClick="btDescargar_OnClick(); return false;" />
                       <%}%>                         
                       <%if (HttpContext.Current.User.IsInRole("3") && !AplicacionActual.Contains("SIRA")){ %>
                                <div style="display: inherit;margin-left: 1px;"><asp:Button runat="server" CssClass="Botones" Text="Carga personalizada" ID="btAsignaManual" Width="150px" Height="30px" OnClientClick="$ManualAssignment.showPopUpManual();  return false;" /></div>  
                       <% } %>
                   </div>
                    <div style="display: inline-block; vertical-align: middle; width: 156px;">
                        <asp:DropDownList ID="ddlDespachos" Style="margin-bottom: -3px" runat="server" Height="30px" Width="155px" DataTextField="nom_corto" CssClass="Combos" DataValueField="idDominio" Visible="False"></asp:DropDownList>
                    </div>
                    <div style="display: inline-block; vertical-align: middle; width: 100px; margin-left: 36px">
                        <asp:Button ID="BtnDespachoVer" runat="server" Height="30px" Text="Ver" Width="90px" OnClick="BtnDespachoVer_Click" OnClientClick="return BloquearMultiplesClicks(this);" CssClass="Botones" Visible="False" />
                    </div>
                </div>
            </div>

            <asp:Panel runat="server" ID="MensajeError" Style="margin: auto; text-align: center; padding: 5px" Height="20px" Visible="false">
                <div>
                    <asp:Label ID="Label1" runat="server" Style="margin-top: 20px" Text="Seleccione un usuario" CssClass="MensajeAlerta"></asp:Label>
                </div>
            </asp:Panel>
            <div style="width: 100%; overflow-x: auto">
                <table>
                    <tr>
                        <td>
                            <div style="width: auto; vertical-align: top;">
                                <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="ASPxGridView1" OnCustomColumnSort="ASPxGridView1_CustomColumnSort" runat="server" AutoGenerateColumns="False" KeyFieldName="num_cred" OnHtmlRowPrepared="ASPxGridView1_RowPrepared" OnCustomCallback="ASPxGridView1_Callback"
                                    SettingsPager-PageSize="50" CssClass="GridCelda" Theme="Default" OnDetailRowExpandedChanged="ASPxGridView1_DetailRowExpandedChanged">
                                    <Templates>
                                        <DetailRow>
                                            <dx:ASPxGridView ID="ASPxGridView2" ClientInstanceName="ASPxGridView2" runat="server" AutoGenerateColumns="False" OnBeforePerformDataSelect="ASPxGridView2_BeforePerformDataSelect" Width="1200" CssClass="GridCelda" Theme="Default">
                                                <Columns>
                                                    <dx:GridViewDataDateColumn FieldName="Fecha" ReadOnly="True" VisibleIndex="1">
                                                    </dx:GridViewDataDateColumn>
                                                    <dx:GridViewDataComboBoxColumn FieldName="Estatus" Caption="Resultado Final" VisibleIndex="2">
                                                        <PropertiesComboBox EnableFocusedStyle="False">
                                                            <Items>
                                                                <dx:ListEditItem Text="Válida aprobada" Value="4" />
                                                                <dx:ListEditItem Text="Válida aprobada" Value="47" />
                                                                <dx:ListEditItem Text="Válida" Value="3" />
                                                                <dx:ListEditItem Text="Válida" Value="37" />
                                                                <dx:ListEditItem Text="Válida no Aprobada" Value="2" />
                                                                <dx:ListEditItem Text="Válida no Aprobada" Value="27" />
                                                            </Items>
                                                        </PropertiesComboBox>
                                                        <Settings AllowAutoFilter="False" HeaderFilterMode="CheckedList" />
                                                    </dx:GridViewDataComboBoxColumn>

                                                    <dx:GridViewDataTextColumn FieldName="Dictamen" Caption="Estatus Final" ReadOnly="True" VisibleIndex="3">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="idDominio" ReadOnly="True" VisibleIndex="4" Visible="False">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="PromesaPago" ReadOnly="True" VisibleIndex="5">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataDateColumn FieldName="FechaPromesa" ReadOnly="True" VisibleIndex="6">
                                                    </dx:GridViewDataDateColumn>
                                                    <dx:GridViewDataTextColumn FieldName="Saldo" ReadOnly="True" VisibleIndex="7">
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                                <SettingsDetail IsDetailGrid="True" />
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
                                        </DetailRow>
                                    </Templates>
                                    <Columns>
                                        <dx:GridViewCommandColumn Caption="Selección" ShowSelectCheckbox="True" VisibleIndex="0" ButtonType="Button">
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn FieldName="num_cred" VisibleIndex="1" Caption="Crédito">
                                            <Settings AllowHeaderFilter="False" AutoFilterCondition="Contains" />
                                            <CellStyle HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="desc_etiq" VisibleIndex="2" Visible="false" Caption="Descripción">
                                            <Settings AllowHeaderFilter="False" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="soluciones" VisibleIndex="3" Caption="Soluciones">
                                            <Settings AllowAutoFilter="False" HeaderFilterMode="CheckedList" AutoFilterCondition="Contains" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="nombre" VisibleIndex="4" Caption="Nombre">
                                            <Settings AllowHeaderFilter="False" AutoFilterCondition="Contains" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="calle" VisibleIndex="5" Caption="Calle">
                                            <Settings AllowHeaderFilter="False" AutoFilterCondition="Contains" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="colonia" VisibleIndex="6" Caption="Colonia">
                                            <Settings AllowHeaderFilter="False" AutoFilterCondition="Contains" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="municipio" VisibleIndex="7" Caption="Municipio">
                                            <Settings HeaderFilterMode="CheckedList" SortMode="DisplayText" AllowAutoFilter="False" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="cp" VisibleIndex="8" Caption="CP">
                                            <Settings AllowAutoFilter="False" HeaderFilterMode="CheckedList" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="estado" VisibleIndex="9" Caption="Delegación">
                                            <Settings AllowAutoFilter="False" HeaderFilterMode="CheckedList" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="idOrden" ReadOnly="True" VisibleIndex="11" Visible="false">
                                            <Settings AllowHeaderFilter="False" AutoFilterCondition="Contains" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="CV_CANAL" VisibleIndex="12" Caption="Canal">
                                            <Settings AllowAutoFilter="False" HeaderFilterMode="CheckedList" AutoFilterCondition="Contains" />
                                            <CellStyle HorizontalAlign="Center"></CellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="auxiliar" VisibleIndex="13" Caption="Auxiliar">
                                            <Settings AllowHeaderFilter="False" AutoFilterCondition="Contains" />
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <SettingsDetail IsDetailGrid="True" ShowDetailRow="True" />
                                    <Templates>
                                        <GroupRowContent>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxCheckBox ID="checkBox" runat="server" />
                                                    </td>
                                                    <td>
                                                        <dx:ASPxLabel ID="CaptionText" runat="server" Text='<%# GetCaptionText(Container) %>' />
                                                    </td>
                                                </tr>
                                            </table>
                                        </GroupRowContent>
                                    </Templates>
                                    <GroupSummary>
                                        <dx:ASPxSummaryItem FieldName="idOrden" SummaryType="Count" />
                                    </GroupSummary>
                                    <SettingsPager Position="Bottom">
                                        <PageSizeItemSettings Items="10, 20, 50, 100" Visible="true" />
                                    </SettingsPager>
                                    <Settings ShowHeaderFilterButton="true" />


                                    <ClientSideEvents SelectionChanged="grid_SelectionChanged"
                                        CustomButtonClick="InvisiSort" />

                                    <Styles>
                                        <EditFormTable Font-Size="12px">
                                        </EditFormTable>
                                        <EditFormCell Font-Size="12px">
                                        </EditFormCell>
                                        <Cell Font-Size="12px">
                                        </Cell>
                                        <AlternatingRow Enabled="True" />
                                    </Styles>

                                    <SettingsPager PageSize="50"></SettingsPager>

                                    <Settings ShowFilterRow="True" ShowGroupPanel="True" />
                                </dx:ASPxGridView>
                                <br />
                                <br />

                            </div>

                        </td>
                    </tr>
                </table>
            </div>
            <%--    </div>
             </div>--%>
            <dx:ASPxPopupControl ID="PopUpMovimientos" runat="server" CloseAction="CloseButton" Modal="True" PopupVerticalAlign="TopSides" PopupVerticalOffset="100" Width="400px"
                PopupHorizontalAlign="WindowCenter" ClientInstanceName="PopUpMovimientos" PopupAnimationType="Fade" EnableViewState="False" Height="200px" CssClass="PopUpMovimientos">
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server" SupportsDisabledAttribute="True">
                        <div style="width: 100%; text-align: center; top: 30px; position: relative">
                            <asp:Label runat="server" ID="lblMensaje" CssClass="Titulo"></asp:Label>
                            <br />
                            <div style="height: 30px"></div>
                            <asp:Button ID="btVer" runat="server" Text="Ver Ordenes Asignadas" OnClick="btVer_Click" CssClass="Botones" Height="30px" />
                            <asp:Button runat="server" ID="CerrarPopUp" CssClass="Botones CerrarPopUp" Width="150px" Text="Cerrar" Height="30px" Visible="False"></asp:Button>
                            <br />
                        </div>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>

            <uc1:AsignacionManual runat="server" id="AsignacionManual" />
        </div>
        <div style="display: none">
            <asp:Button ID="btnNada" runat="server" Visible="true" Width="0" Height="0" BorderStyle="None" OnClientClick="return false;" />
            <asp:Button ID="btnHack" CssClass="btnHack" ClientIDMode="Static" runat="server" Visible="true" Width="0" Height="0" BorderStyle="None" OnClick="SeleccionarExpandidas" />
        </div>
    </asp:Panel>

</asp:Content>
