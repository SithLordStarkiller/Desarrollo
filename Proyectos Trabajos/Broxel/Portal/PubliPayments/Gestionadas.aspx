<%@ Page Title="" Language="C#" MasterPageFile="~/masterpage.Master" AutoEventWireup="true" CodeBehind="Gestionadas.aspx.cs" Inherits="PubliPayments.Gestionadas" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>



<%@ Register Src="~/UserControls/AsignacionManual.ascx" TagPrefix="uc1" TagName="AsignacionManual" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnNada">
        <style>
             .lb-outerContainer {height: 85% !important;}
             .lightbox .lb-image {height: 640px !important;width: 480px !important;}
             .dxpcLite.dxpclW {width: 400px !important;}
            .ASPxPopupOrdenInfo {width: 850px !important;}

            #tablaRespuesta {width: 830px;height: 400px;overflow-y: auto;font: 12px 'Open Sans', sans-serif;}
            #tablaRespuestaHeader {background: #DCDCDC;margin: 10px;font: 12px 'Open Sans', sans-serif;}
            #tablaRespuesta table td {padding: 0px 6px 0px 6px;}
            #ContentPlaceHolder1_ASPxGridView1 a:active {position: relative;top: 1px;}
            #Mask {
                background-color: black;
                position: absolute;
                z-index: 3;
                top: 0;
                left: 0;
                right: 0;
                bottom: 0;
                -ms-opacity: 0.2;
                opacity: 0.2;
            }
            .Margin{margin-left: 3px}
            .ddlGestores{ margin-left: 154px;margin-right: 3px;}
        </style>
        <script src="./Scripts/restrictors.js?version=3.0"></script>
        <script src="./Scripts/jquery-ui-1.10.4.min.js"></script>
        <script src="./Scripts/lightbox.min.js"></script>
        <link rel="stylesheet" href="./Styles/lightbox.css">
        <script src="./Scripts/OrdenesController.js?version=1.2"></script>
        <script type="text/javascript">

            var ultimoIdComentario = "0";

            function grid_SelectionChanged(s, e) {
                s.GetSelectedFieldValues("idOrden;Estatus", GetSelectedFieldValueCallback);
            }
            function GetSelectedFieldValueCallback(values) {
                var _idRol = "<%=HttpContext.Current.User.IsInRole("3")?"3":(HttpContext.Current.User.IsInRole("2") || HttpContext.Current.User.IsInRole("0"))?"2":"-1"%>";
                ArrIdOrdenesA = values;
                OrdAsig = "0";
                
                var ordenes = [];
                for (var i = 0; i < values.length; i++) {
                    if (_idRol == "3") {
                        if (ordenes.length < MaxGestor) {
                            ordenes.push(values[i][0]);
                        }

                    } else if (_idRol == "2") {
                        if (ordenes.length < MaxSuper) {
                            ordenes.push(values[i][0]);
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
                        var messageNode = document.createElement("P");
                        var messageContent = document.createTextNode("...");
                        messageNode.appendChild(messageContent);
                        document.getElementById("popupDiv").appendChild(messageNode);
                    }
                }
                document.getElementById("selCount").innerHTML = ASPxGridView1.GetSelectedRowCount();
            }
            function InvisiSort(s, e) {
                ASPxGridView1.SortBy(ASPxGridView1.GetColumnByField('idOrden'));
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

            function OnPageCheckedChanged(s, e) {
                if (!s.GetChecked()) {
                    SelectAllCheckbox.SetChecked(s.GetChecked());
                }
                ASPxGridView1.SelectAllRowsOnPage(s.GetChecked());


            }

            function btDescargar_OnClick() {
                window.open('/ArchivoRespuestas/Descargar?idUsuarioPadre=' + '<%=Session["idUsuarioPadre"] %>' + "&&tipoFormulario="+'<%=FormularioList.SelectedValue%>', "_self");
            }

            function CheckAll(Checkbox) {
                ASPxGridView1.SelectAllRowsOnPage();

            }

            function LimpiarFiltros() {
                ASPxGridView1.Refresh();
                ASPxGridView1.ClearFilter();
            }
            function ShowResponse(response) {
                $("#tablaRespuesta").children().remove();
                $("#tablaRespuesta").append(response.HtmlResp);
                $("#tablaRespuestaHeader").html(response.usuario);
                $("#Mask").hide();
                ASPxPopupOrdenInfo.Show();
            };

            function showMask() {
                $("#Mask").css('width', $("body").width());
                $("#Mask").css('height', $("body").height() + 30);
                $("#Mask").show();
            }
            $(document).ready(function () {
                UrlAjaxAuthorize = "/Ordenes/AutorizarCreditos";
                UrlAjaxCancel = "/Ordenes/CancelarCreditos";
                UrlAjaxReallocation = "/Ordenes/ReasignarCreditos";
                UrlAjaxAssignment = "/Ordenes/AsignarDifGestorCreditos";
                ContainerPage = ["Gestionadas", "3", $(".FormularioList").val()];
                $("#ContentPlaceHolder1_ASPxGridView1").on("click", ".idOrden a", function (e) {
                    e.preventDefault();
                    showMask();
                    var orden = ($(this).html());
                    $.ajax({
                        url: '/Respuesta/Ver',
                        type: 'GET',
                        data: {
                            idOrden: orden
                            , proceso: 1
                        },
                        contentType: 'application/json; charset=utf-8',
                        success: ShowResponse,
                        error: function (msg, text, thrown) {
                            alert("error " + thrown);
                            $("#Mask").hide();
                        }
                    });
                });

                $("#ContentPlaceHolder1_ASPxGridView1").on("mouseenter", ".UsuarioComent", function (e) {
                    e.preventDefault();
                    var posicion = $(this).offset();
                    var orden = $(this).siblings(".idOrden")[0].textContent;
                    ultimoIdComentario = orden;
                    var $elemento = $(this);
                    $.ajax({
                        beforeSend: function () {
                            ComentarioFinal.ShowAtPos(posicion.left + $elemento.width() + 12, posicion.top);
                            ComentarioFinal.Show();
                            $('#ComentDiv').html("<img src='imagenes/ajax-loader.gif' alg='Loading...'>");
                        },
                        url: '/Respuesta/VerComentario',
                        type: 'GET',
                        data: {
                            idOrden: orden
                            , proceso: 2
                        },
                        contentType: 'application/json; charset=utf-8',
                        success: function (response) {
                            if (ultimoIdComentario != response.orden) {
                                return false;
                            }
                            $("#ComentDiv").html(response.HtmlResp);
                            ComentarioFinal.SetHeaderText("Comentario final: " + response.usuario + " orden: " + response.orden);
                        },
                        error: function (msg, text, thrown) {
                            return false;
                        }
                    });
                });

                $("#ContentPlaceHolder1_ASPxGridView1").on("mouseout", ".UsuarioComent", function (e) {
                    $("#ComentDiv").html("");
                    ComentarioFinal.Hide();
                });

                $(".ddlGestores").change(function () {
                    NewManager = $(this).val();
                });
            });
        </script>
        <div id="Mask" style="display: none"></div>
         <div style="margin-left: 30px; position: relative">
            <div id="cabecero" style="margin-bottom: 10px; position: relative;text-align: left;">
                 <div id="tituloPagina" class="TituloSeleccion" style="display: inline-block;vertical-align: top;">
                        Autorizar Respuestas<asp:Image ID="Image2" runat="server" AlternateText="|" ImageUrl="~/imagenes/separador.png" Style="margin-left: 15px; margin-right: 15px; position: relative; top: 10px;" />
                     <br/>
                     <asp:DropDownList ID="FormularioList" Style="margin-top: 22px;" runat="server" Height="30px" Width="226px" DataTextField="Descripcion" CssClass="Combos FormularioList" DataValueField="Ruta" AutoPostBack="True" OnSelectedIndexChanged="FormularioList_OnSelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="TituloSeleccion" style="height: 96px; display: inline-block; vertical-align: middle; width: auto;">
                    <div style="margin-top: 10px; display: inline-block; text-align: left;">
                
                            <div class="Combos" style="height: 19px; width: 140px; display: inline-block; vertical-align: bottom">
                                <div style="text-align: right; display: inline-block; vertical-align: top; margin-top: 2px;">Seleccionar página</div>
                                <dx:ASPxCheckBox ID="SelectPageCheckbox" ClientInstanceName="SelectPageCheckbox" runat="server" ToolTip="Selecciona todas las filas en la página" ClientSideEvents-CheckedChanged="OnPageCheckedChanged" />
                            </div>
                            <div class="Combos" style="height: 19px; width: 140px; display: inline-block; vertical-align: bottom">
                                <div style="text-align: right; display: inline-block; vertical-align: top; margin-top: 2px;">Seleccionar todos</div>
                                <dx:ASPxCheckBox ID="SelectAllCheckbox" runat="server" ClientInstanceName="SelectAllCheckbox"   OnInit="checkAll_init" ToolTip="Selecciona todas las filas">    <ClientSideEvents CheckedChanged="OnAllCheckedChanged" /></dx:ASPxCheckBox>
                            </div>
                        <div id="countDiv" style="text-align: right; display: inline-block; vertical-align: top; margin-top: 20px; font-size: 12px; margin-right: 20px;">Entradas seleccionadas: <span id="selCount" style="font-weight: bold">0</span></div>
                
                        <% if (HttpContext.Current.User.IsInRole("3"))
                           { %>
                        <asp:DropDownList ID="ddlGestores" runat="server" Height="30px" Width="150px" DataTextField="Usuario" CssClass="Combos ddlGestores" DataValueField="idUsuario"></asp:DropDownList>
                        <button id="btCAsignacion" style="height: 30px;width: 150px" class="Botones MovType21">Cambiar asignación</button>
                        <% } %>
                
                  </div>
                
                    <br />
                     <div style="margin-top: 10px; display: inline-block; text-align: left;">
                        <asp:Button runat="server" CssClass="Botones btnLimpiar" Text="Restablecer" ID="btLimpiar" Width="150px" Height="30px" OnClick="btLimpiar_OnClick" />
                        <asp:Button runat="server" CssClass="Botones Margin btDescargar" Text="Descargar" ID="btDescargar" Width="150px" Height="30px" OnClientClick="btDescargar_OnClick(); return false;" />
                         <% if (HttpContext.Current.User.IsInRole("3"))
                               { %><div style="display: inherit"><asp:Button runat="server" CssClass="Botones Margin" Text="Carga personalizada" Width="150px" Height="30px" OnClientClick="$ManualAssignment.showPopUpManual();  return false;" /></div>  <% } %>
                        <button id="btAutorizar" style="height: 30px;width: 150px" class="Botones Margin MovType4">Autorizar</button>
                        <button id="btCancelar" style="height: 30px;width: 150px" class="Botones Margin MovType12">Cancelar órdenes</button>

                    <% if (!(HttpContext.Current.User.IsInRole("0") || HttpContext.Current.User.IsInRole("2")))
                       { %>
                        <button id="btReasignar" style="height: 30px;width: 150px" class="Botones Margin MovType15 btReasignar">Reasignar Ordenes</button>
                    <% } %>
                    </div>
                </div>
        
        
            </div>
           
                <div style="width:100%;overflow-x:auto">
                    <table>
                    <tr>
                        <td>
                            <div style="width: auto; vertical-align: top; margin-top: 0px;">
                                <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="ASPxGridView1" runat="server" AutoGenerateColumns="False" CssClass="GridCelda" OnCustomColumnSort="ASPxGridView1_CustomColumnSort" KeyFieldName="idOrden" SettingsPager-PageSize="50"
                                    OnCustomCallback="ASPxGridView1_CustomCallback" OnHtmlRowPrepared="ASPxGridView1_RowPrepared" OnDetailRowExpandedChanged="ASPxGridView1_DetailRowExpandedChanged" >
                                    <Settings ShowFilterRow="True" ShowGroupPanel="True" />
                                    <Settings ShowHeaderFilterButton="true" />
                                    <ClientSideEvents SelectionChanged="grid_SelectionChanged"
                                        CustomButtonClick="InvisiSort" />
                                    <SettingsPager Position="Bottom">
                                        <PageSizeItemSettings Items="10, 20, 50, 100" Visible="true"></PageSizeItemSettings>
                                    </SettingsPager>
                                    <Styles>
                                        <EditFormTable Font-Size="12px"></EditFormTable>
                                        <EditFormCell Font-Size="12px"></EditFormCell>
                                        <Cell Font-Size="12px"></Cell>
                                        <AlternatingRow Enabled="True" />
                                    </Styles>
                                
                                    <Templates>
                                       <DetailRow>
                                            <dx:ASPxGridView ID="ASPxGridView2"  ClientInstanceName="ASPxGridView2" runat="server" AutoGenerateColumns="False" OnBeforePerformDataSelect="ASPxGridView2_BeforePerformDataSelect" Width="1200" CssClass="GridCelda" Theme="Default">
                                        <Columns>
                                            <dx:GridViewDataDateColumn FieldName="Fecha" ReadOnly="True" VisibleIndex="1">
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataComboBoxColumn FieldName="Estatus" Caption="Resultado Final" VisibleIndex="2">
                                            <PropertiesComboBox EnableFocusedStyle="False">
                                                <Items>
                                                    <dx:ListEditItem Text="Válida aprobada" Value="4" />
                                                    <dx:ListEditItem Text="Válida" Value="3" />
                                                    <dx:ListEditItem Text="Válida no Aprobada" Value="2" />
                                                        
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
                                        <dx:GridViewCommandColumn ButtonType="Button" Caption="Selección" ShowSelectCheckbox="True" VisibleIndex="0"></dx:GridViewCommandColumn>
                                        <dx:GridViewDataHyperLinkColumn FieldName="idOrden" VisibleIndex="1" ReadOnly="True" Caption="Orden" Width="5%">
                                            <PropertiesHyperLinkEdit NavigateUrlFormatString=""></PropertiesHyperLinkEdit>
                                            <Settings AllowHeaderFilter="False" AutoFilterCondition="Contains" />
                                            <EditFormSettings Visible="False" />
                                            <CellStyle CssClass="idOrden" HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataHyperLinkColumn>
                                        <dx:GridViewDataTextColumn FieldName="Usuario" VisibleIndex="2" Caption="Usuario">
                                            <Settings AllowAutoFilter="False" HeaderFilterMode="CheckedList" />
                                            <CellStyle CssClass="UsuarioComent"></CellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="nombre" VisibleIndex="3" Caption="Nombre">
                                            <Settings AllowHeaderFilter="False" AutoFilterCondition="Contains" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="num_cred" VisibleIndex="4" Caption="Crédito" Width="5%">
                                            <Settings AllowHeaderFilter="False" AutoFilterCondition="Contains" />
                                            <CellStyle  HorizontalAlign="Right"></CellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="soluciones" VisibleIndex="5" Caption="Solución" Visible="false">
                                            <Settings AllowAutoFilter="False" HeaderFilterMode="CheckedList" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataComboBoxColumn FieldName="EstatusExtra" VisibleIndex="6" Caption="Estatus">
                                            <PropertiesComboBox EnableFocusedStyle="False">
                                                <Items>
                                                    <dx:ListEditItem Text="Respondida" Value="3"  />
                                                    <dx:ListEditItem Text="Respondida SMS" Value="3S" />
                                                    <dx:ListEditItem Text="Respondida CC" Value="3C" />
                                                    <dx:ListEditItem Text="Respondida SMS CC" Value="3CS" />
                                                    <dx:ListEditItem Text="Autorizada" Value="4" />
                                                    <dx:ListEditItem Text="Autorizada SMS" Value="4S" />
                                                    <dx:ListEditItem Text="Autorizada CC" Value="4C" />
                                                    <dx:ListEditItem Text="Autorizada SMS CC" Value="4CS" />
                                                </Items>
                                            </PropertiesComboBox>
                                            <Settings AllowAutoFilter="False" HeaderFilterMode="CheckedList" />
                                        </dx:GridViewDataComboBoxColumn>
                                        <dx:GridViewDataTextColumn FieldName="idVisita" VisibleIndex="7" Caption="Visita">
                                            <Settings AllowAutoFilter="False" HeaderFilterMode="CheckedList" />
                                            <CellStyle HorizontalAlign="center"></CellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Dictamen" VisibleIndex="8" Caption="Dictamen">
                                            <Settings AllowAutoFilter="False" HeaderFilterMode="CheckedList" />
                                        </dx:GridViewDataTextColumn>
                                          <dx:GridViewDataTextColumn FieldName="TX_COLONIA" VisibleIndex="9" Caption="Colonia">
                                            <Settings AllowHeaderFilter="False" AutoFilterCondition="Contains" />
                                        </dx:GridViewDataTextColumn>
                                          <dx:GridViewDataTextColumn FieldName="TX_MUNICIPIO" VisibleIndex="10" Caption="Municipio">
                                            <Settings AllowAutoFilter="False" HeaderFilterMode="CheckedList" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="FechaModificacion" VisibleIndex="11" Caption="Modificación">
                                            <Settings AllowHeaderFilter="False" AutoFilterCondition="Contains" />
                                            <CellStyle  HorizontalAlign="center"></CellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="FechaRecepcion" VisibleIndex="12" Caption="Recepción">
                                            <Settings AllowHeaderFilter="False" AutoFilterCondition="Contains" />
                                            <CellStyle  HorizontalAlign="center"></CellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="FechaEnvio" VisibleIndex="13" Caption="Envío">
                                            <Settings AllowHeaderFilter="False" AutoFilterCondition="Contains" />
                                            <CellStyle  HorizontalAlign="center"></CellStyle>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="CV_CANAL" VisibleIndex="14" Caption="Canal">
                                        <Settings AllowAutoFilter="False" HeaderFilterMode="CheckedList" AutoFilterCondition="Contains" />
                                            <CellStyle  HorizontalAlign="center"></CellStyle>
                                    </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="auxiliar" VisibleIndex="15" Caption="Auxiliar">
                                            <Settings AllowHeaderFilter="False" AutoFilterCondition="Contains" />
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                     <SettingsDetail IsDetailGrid="True" ShowDetailRow="True" />
                                    <Templates>
                                        <GroupRowContent>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxCheckBox ID="checkbox" runat="server" />
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
                                </dx:ASPxGridView>
                            </div>
                        </td>
                    </tr>
                </table>
                </div>
                <dx:ASPxPopupControl ID="ASPxPopupControl1" runat="server" PopupAction="MouseOver" PopupElementID="countDiv" ShowHeader="true" Width="100px" PopupHorizontalAlign="LeftSides"
                    PopupVerticalAlign="Above" PopupVerticalOffset="-10" PopupHorizontalOffset="-10" CloseAction="MouseOut">
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



                <dx:ASPxPopupControl ID="ComentarioFinal" runat="server" CloseAction="CloseButton" Width="400px" PopupVerticalAlign="TopSides"
                    ClientInstanceName="ComentarioFinal" PopupAnimationType="Fade" EnableViewState="False" HeaderText="Comentario Final">
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server" SupportsDisabledAttribute="True">
                            <div id="ComentDiv" style="display: inline-block; font-size: 12px; max-width: 550px; color: black; font-weight: bold;">
                            </div>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>

                <dx:ASPxPopupControl ID="PopUpMovimientos" runat="server" CloseAction="CloseButton" Modal="True" PopupVerticalAlign="TopSides" PopupVerticalOffset="100" CssClass="PopUpMovimientos"
                    PopupHorizontalAlign="WindowCenter" ClientInstanceName="PopUpMovimientos" PopupAnimationType="Fade" EnableViewState="False" Height="160px">
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server" SupportsDisabledAttribute="True" Height="130">
                            <div style="width: 100%; text-align: center; top: 30px; position: relative">
                                <asp:Label runat="server" ID="lblMensaje" CssClass="Titulo"></asp:Label>
                                <br />
                                <div style="height: 30px"></div>
                                <asp:Button ID="btAsignar" runat="server" Text="Asignar mas ordenes" OnClick="btAsignar_Click" CssClass="Botones" Height="30px" />
                                <br />
                            </div>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>

                <dx:ASPxPopupControl ID="ASPxPopupOrdenInfo" runat="server" CloseAction="CloseButton" Modal="True" Width="800px" PopupVerticalAlign="TopSides" PopupVerticalOffset="50"
                    PopupHorizontalAlign="WindowCenter" ClientInstanceName="ASPxPopupOrdenInfo" PopupAnimationType="Fade" EnableViewState="False" HeaderText="Respuestas" AllowDragging="True" CssClass="ASPxPopupOrdenInfo" >
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server" SupportsDisabledAttribute="True">
                            <div id="tablaRespuestaHeader"></div>
                            <div id="tablaRespuesta" style="overflow: auto"></div>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
        </div>
        <div style="display: none">
            <asp:Button ID="btnNada" runat="server" Visible="true" Width="0" Height="0" BorderStyle="None" OnClientClick="return false;" />
        </div>
        <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView1" FileName="Respuestas"></dx:ASPxGridViewExporter>
        <uc1:AsignacionManual runat="server" id="AsignacionManual" />
    </asp:Panel>
</asp:Content>

