<%@ Page Title="OrdenesAsignadas" Language="C#" MasterPageFile="masterpage.Master" AutoEventWireup="true" CodeBehind="OrdenesAsignadas.aspx.cs" Inherits="PubliPayments.OrdenesAsignadas" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>



<%@ Register Src="~/UserControls/AsignacionManual.ascx" TagPrefix="uc1" TagName="AsignacionManual" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    3,k<asp:Panel runat="server" DefaultButton="btnNada">
        <style>
            #outerDiv {
                overflow: hidden;
                width: 148px;
                height: 100px;
                border: 1px solid #fff;
            }

            #popupDiv {
                overflow: scroll;
                width: 148px;
                height: 117px;
            }

            .dxpcLite.dxpclW {
                top: 20px !important;
            }
        </style>
        <script src="./Scripts/restrictors.js?version=3.0"></script>
        <script src="./Scripts/OrdenesController.js?version=1.2"></script>
        <script type="text/javascript">

            function CheckAll(checkbox) {
                ASPxGridView1.SelectAllRowsOnPage();
            }

            function grid_SelectionChanged(s, e) {
                s.GetSelectedFieldValues("idOrden;Estatus", GetSelectedFieldValueCallback);

            }

            function InvisiSort(s, e) {
                ASPxGridView1.SortBy(ASPxGridView1.GetColumnByField('nom_corto'));
            }

            function ClearAll() {
                ASPxGridView1.ClearSort();
            }

            function GetSelectedFieldValueCallback(values) {
                var _idRol = "<%=HttpContext.Current.User.IsInRole("3")?"3":(HttpContext.Current.User.IsInRole("2") || HttpContext.Current.User.IsInRole("0"))?"2":"-1"%>";
                if (_idRol == "3") {
                        ArrIdOrdenes = values.slice(0,MaxGestor);
                } else if (_idRol == "2") {
                        ArrIdOrdenes = values.slice(0,MaxSuper);
                }
                
                OrdAsig = <%=OrdAsig.Value%>;
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

            function filterCanceladas(s, e) {
                SelectPageCheckbox.SetChecked(false);
                SelectAllCheckbox.SetChecked(false);

                ASPxGridView1.PerformCallback(s.GetChecked());
            }

            function CheckAll(checkbox) {
                ASPxGridView1.SelectAllRowsOnPage();

            }
            function btTotalOnClick() {
                window.open('ReporteDescarga/DescargaReporteInfoComercio?idUsuarioPadre=' + '<%=Session["idPadre"] %>' + "&&tipoFormulario=<%=FormularioList.SelectedValue%>'", "_self");
                 }

            $(document).ready(function () {
                UrlAjaxAssignment = "/Ordenes/AsignarDifGestorCreditos";
                UrlAjaxCancel = "/Ordenes/CancelarCreditos";
                ContainerPage = ["Órdenes Asignadas","2" ,$(".FormularioList").val()];
                $(".ddlGestores").change(function () {
                    NewManager = $(this).val();
                });
            });
        </script>


        <dx:ASPxPopupControl ID="ASPxPopupControl1" runat="server"
            PopupAction="MouseOver" PopupElementID="countDiv"
            ShowHeader="true" Width="217px" PopupHorizontalAlign="LeftSides"
            PopupVerticalAlign="Above" PopupVerticalOffset="-10"
            PopupHorizontalOffset="-10" CloseAction="MouseOut">
            <HeaderContentTemplate>
                <div>Elementos Seleccionados</div>
            </HeaderContentTemplate>
            <ContentCollection>

                <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server" SupportsDisabledAttribute="True">
                    <div id="outerDiv">
                        <div id="popupDiv">
                            &nbsp;
                        </div>
                    </div>

                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>

        <div style="margin-left: 30px; position: relative">
            
            <div id="cabecero" style="margin-bottom: 10px; position: relative;text-align: left;">
                <div id="tituloPagina" class="TituloSeleccion" style="display: inline-block;vertical-align: top;">
                    Órdenes Asignadas<asp:Image ID="Image1" runat="server" AlternateText="|" ImageUrl="~/imagenes/separador.png" Style="margin-left: 8px; margin-right:8px; position: relative; top: 10px;" />
                 <br/>
                    <asp:DropDownList ID="FormularioList" Style="margin-top: 22px;" runat="server" Height="30px" Width="207px" DataTextField="Descripcion" CssClass="Combos FormularioList" DataValueField="Ruta" AutoPostBack="True" OnSelectedIndexChanged="FormularioList_OnSelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="TituloSeleccion" style="height: <%=(HttpContext.Current.User.IsInRole("0"))?"142px":"96px"%> ;  display: inline-block; vertical-align: middle; width: 75%;">
                        <div class="Combos" style="height: 19px; width: 130px; display: inline-block; vertical-align: bottom">
                        <div style="text-align: right; display: inline-block; vertical-align: top; margin-top: 2px;">Seleccionar página</div>
                        <dx:ASPxCheckBox ID="SelectPageCheckbox" ClientInstanceName="SelectPageCheckbox" runat="server" ToolTip="Selecciona todas las filas en la página"
                            ClientSideEvents-CheckedChanged="OnPageCheckedChanged" />
                        </div>
                <div class="Combos" style="height: 19px; width: 130px; display: inline-block; vertical-align: bottom">
                    <div style="text-align: right; display: inline-block; vertical-align: top; margin-top: 2px;">Seleccionar todos</div>
                    <dx:ASPxCheckBox ID="SelectAllCheckbox" runat="server" ClientInstanceName="SelectAllCheckbox"
                        OnInit="checkAll_init" ToolTip="Selecciona todas las filas">
                        <ClientSideEvents CheckedChanged="OnAllCheckedChanged" />
                    </dx:ASPxCheckBox>
                </div>
                <div id="countDiv" style="text-align: center; display: inline-block; vertical-align: top; margin-top: 20px; font-size: 12px; width: 140px;">
                    Entradas seleccionadas: <span id="selCount" style="font-weight: bold">0</span>
                </div>
                <button id="btnCancelar" style="height: 30px;width: 140px" class="Botones MovType12">Cancelar</button>
                <% if (HttpContext.Current.User.IsInRole("3") && !AplicacionActual.Contains("SIRA"))
                   { %>
                    <asp:Image ID="Image2" runat="server" AlternateText="|" ImageUrl="~/imagenes/separador.png" Style="margin-left: 15px; margin-right: 15px; position: relative; top: 10px;" />
                    <asp:DropDownList ID="ddlGestores" runat="server" Height="30px" Width="140px" DataTextField="Usuario" CssClass="Combos ddlGestores"
                        DataValueField="idUsuario">
                    </asp:DropDownList>
                    <div style="width: 5px; display: inline-block"></div>
                     <button id="btAsignar" style="height: 30px;width: 140px" class="Botones MovType21">Cambiar asignación</button>
               <% } %>
                    <br />
                <div style="display: inline-block; vertical-align: middle; width: 100%; height: 50px; margin-top: 10px; margin-bottom: 100px">
                    <asp:Button ID="btnLimpiar" Style="margin-bottom: 10px" runat="server" Height="30px" Text="Restablecer" Width="150px" OnClick="btLimpiar_Clear" OnClientClick="return BloquearMultiplesClicks(this);" CssClass="Botones btnLimpiar" TabIndex="1" />
                   
                      <% if ( HttpContext.Current.User.IsInRole("3")  && AplicacionActual.Contains("SIRA")){%>
                       <button class="Botones Margin" id="btDescargar" style="width: 150px;height: 30px" onclick="btTotalOnClick(); return false;">Rep Total</button>
                       <%}%> 
                    
                     <% if (HttpContext.Current.User.IsInRole("3") && !AplicacionActual.Contains("SIRA"))
                           { %>
                             <%--<div class="Combos" style="height: 19px; width: 140px; display: none;">
                                <div style="text-align: right; display: inline-block;">Mostrar Canceladas</div>
                                <dx:ASPxCheckBox ID="ASPxCheckBox1" runat="server" ClientInstanceName="CanceladasCheckbox"
                                    OnInit="checkAll_init" ToolTip="Selecciona todas las filas">
                                    <ClientSideEvents CheckedChanged="filterCanceladas" />
                                </dx:ASPxCheckBox>
                            </div>
                                <div class="Combos" style="height: 19px; width: 140px; display: inline-block">
                                    <div style="text-align: right; display: inline-block; vertical-align: top">Mostrar canceladas</div>
                                    <dx:ASPxCheckBox ID="FilterCb" runat="server" ClientInstanceName="FilterCb"
                                        OnInit="checkAll_init" ToolTip="Mostrar canceladas">
                                        <ClientSideEvents CheckedChanged="filterCanceladas" />
                                    </dx:ASPxCheckBox>
                                </div>--%>
                                <div style="display: inherit"><asp:Button ID="Button1" runat="server" CssClass="Botones" Text="Carga personalizada" Width="150px" Height="30px" OnClientClick="$ManualAssignment.showPopUpManual();  return false;" /></div> 
                            <% } %>
                    
                    <% if (HttpContext.Current.User.IsInRole("0"))
                       { %>
                            <div style="display: inline-block; vertical-align: middle; width: 100%; height: 50px; ">
                                   <div style="display: inline-block; height: 19px; width: 120px;">
                                        <asp:DropDownList ID="ddlDespachos" runat="server" Height="30px" Width="150px" DataTextField="nom_corto" CssClass="Combos" DataValueField="idDominio" Visible="False" OnSelectedIndexChanged="DespachoSeleccionado" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                    <div style="display: inline-block; height: 19px; max-width: 120px; margin-left: 30px">
                                        <asp:DropDownList ID="ddlAdminDespachos" runat="server" Height="30px" Width="150px" DataTextField="Usuario1" DataValueField="idUsuario" CssClass="Combos" OnSelectedIndexChanged="AdministradorSeleccionado" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                    <div style="display: inline-block; height: 19px; max-width: 120px;margin-left: 30px">
                                        <asp:DropDownList ID="ddlSupDespachos" runat="server" Height="30px" Width="150px" DataTextField="Usuario1" DataValueField="idUsuario" CssClass="Combos" OnSelectedIndexChanged="SupervisorSeleccionado" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                    <div style="display: inline-block; height: 19px; width: 95px;margin-left: 30px">
                                        <asp:Button ID="BtnDespachoSup" runat="server" Height="30px" Text="Ver órdenes" Width="90px" CssClass="Botones" Visible="False" OnClick="BtnDespachoSupr_Click" />
                                        <asp:Button ID="BtnDespachoGest" runat="server" Height="30px" Text="Ver órdenes" Width="90px" CssClass="Botones" Visible="False" OnClick="BtnDespachoGest_Click" />
                                    </div>
                                    <div style="display:inline-block; height: 19px; width: 95px;margin-left: 20px">
                                        <asp:Button ID="btnReasignar" runat="server" Height="30px" Text="Reasignar" Width="90px" CssClass="Botones" Visible="False" OnClick="btnReasignarOrden_OnClick" OnClientClick="return confirm('Las ordenes seleccionadas seran canceladas y reasignadas al mismo gestor, deseas continuar?')"  />
                                    </div>
                            </div>
                       <% } %>
                </div>
                    </div>
        
            </div>
            
            <div style="width:100%;overflow-x:auto">
                <table>
                <tr>
                    <td>
                        <div style="width: auto; vertical-align: top;">
                            <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="ASPxGridView1" runat="server" AutoGenerateColumns="False" CssClass="GridCelda" OnCustomColumnSort="ASPxGridView1_CustomColumnSort" OnCustomCallback="ASPxGridView1_CustomCallback" KeyFieldName="idOrden"
                                OnHtmlRowPrepared ="ASPxGridView1_RowPrepared" OnDetailRowExpandedChanged="ASPxGridView1_DetailRowExpandedChanged" >

                                <SettingsPager PageSize="50">
                                    <PageSizeItemSettings Items="10, 20, 50, 100" Visible="True"></PageSizeItemSettings>
                                </SettingsPager>

                                <Settings ShowFilterRow="True" ShowGroupPanel="True" />


                                <ClientSideEvents SelectionChanged="grid_SelectionChanged"
                                    CustomButtonClick="InvisiSort" />
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
                                    <dx:GridViewCommandColumn ButtonType="Button" Caption="Selección" ShowSelectCheckbox="True" VisibleIndex="0">
                                        
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn FieldName="idOrden" VisibleIndex="1" ReadOnly="True" Caption="Orden" Width="5%" >
                                        <CellStyle HorizontalAlign="Right"></CellStyle>
                                        <Settings AllowHeaderFilter="False" AutoFilterCondition="Contains" />
                                        
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="idUsuario" VisibleIndex="2" Visible="false">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="NombreUsuario" VisibleIndex="3" Caption="Usuario">
                                        <Settings AllowAutoFilter="False" HeaderFilterMode="CheckedList" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="idUsuarioPadre" VisibleIndex="4" Visible="false">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="NombreUsuarioPadre" VisibleIndex="5" Visible="false">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="num_cred" VisibleIndex="6" Caption="Crédito" Width="5%">
                                        <Settings AllowHeaderFilter="False" AutoFilterCondition="Contains" />
                                        <CellStyle HorizontalAlign="Right"></CellStyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="desc_etiq" VisibleIndex="10" Visible="false" Caption="Descripción">
                                        <Settings AllowAutoFilter="False" HeaderFilterMode="CheckedList" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="soluciones" VisibleIndex="11" Caption="Solución">
                                        <Settings AllowAutoFilter="False" HeaderFilterMode="CheckedList" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="idVisita" VisibleIndex="11" Caption="Visita">
                                        <Settings AllowAutoFilter="False" HeaderFilterMode="CheckedList" />
                                        <CellStyle HorizontalAlign="Center"></CellStyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="nombre" VisibleIndex="12" Caption="Nombre">
                                        <Settings AllowHeaderFilter="False" AutoFilterCondition="Contains" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="calle" VisibleIndex="13" Caption="Calle">
                                        <Settings AllowHeaderFilter="False" AutoFilterCondition="Contains" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Colonia" FieldName="colonia" VisibleIndex="14">
                                        <Settings AllowHeaderFilter="False" AutoFilterCondition="Contains" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="municipio" VisibleIndex="15" Caption="Municipio">
                                        <Settings AllowAutoFilter="False" HeaderFilterMode="CheckedList"></Settings>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="CP" FieldName="cp" VisibleIndex="16">
                                        <Settings AllowAutoFilter="False" HeaderFilterMode="CheckedList" />
                                        <CellStyle HorizontalAlign="Center"></CellStyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Delegación" FieldName="estado" VisibleIndex="17">
                                        <Settings SortMode="Custom" AllowAutoFilter="False" HeaderFilterMode="CheckedList" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="CV_CANAL" VisibleIndex="18" Caption="Canal">
                                        <Settings AllowAutoFilter="False" HeaderFilterMode="CheckedList" AutoFilterCondition="Contains"/>
                                        <CellStyle HorizontalAlign="Center"></CellStyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="auxiliar" VisibleIndex="19" Caption="Auxiliar">
                                        <Settings AllowHeaderFilter="False" AutoFilterCondition="Contains" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="nom_corto" VisibleIndex="20" Visible="False">
                                        <Settings AllowAutoFilter="False" HeaderFilterMode="CheckedList" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataComboBoxColumn FieldName="EstatusExtra" VisibleIndex="9" Caption="Estatus">
                                        <PropertiesComboBox EnableFocusedStyle="False">
                                            <Items>
                                                <dx:ListEditItem Text="Asignada" Value="1" />
                                                <dx:ListEditItem Text="Asignada" Value="11" />
                                                <dx:ListEditItem Text="Asignada" Value="12" />
                                                <dx:ListEditItem Text="Asignada" Value="15" />
                                                <dx:ListEditItem Text="Cancelada" Value="2" />
                                                <dx:ListEditItem Text="Reasignada" Value="5" />
                                                <dx:ListEditItem Text="Sincronizando" Value="6" />

                                                <dx:ListEditItem Text="Asignada SMS" Value="1S" />
                                                <dx:ListEditItem Text="Asignada SMS" Value="15S" />
                                                <dx:ListEditItem Text="Cancelada SMS" Value="2S" />
                                                <dx:ListEditItem Text="Sincronizando" Value="6S" />
                                                
                                                <dx:ListEditItem Text="Asignada CC" Value="1C" />
                                                <dx:ListEditItem Text="Asignada CC" Value="11C" />
                                                <dx:ListEditItem Text="Asignada CC" Value="12C" />
                                                <dx:ListEditItem Text="Asignada CC" Value="15C" />
                                                <dx:ListEditItem Text="Cancelada CC" Value="2C" />
                                                <dx:ListEditItem Text="Reasignada CC" Value="5C" />
                                                <dx:ListEditItem Text="Sincronizando CC" Value="6C" />
                                                <dx:ListEditItem Text="Asignada SMS CC" Value="1CS" />
                                                <dx:ListEditItem Text="Respondida CC" Value="3C" />
                                                <dx:ListEditItem Text="Respondida SMS CC" Value="3CS" />
                                                <dx:ListEditItem Text="Autorizada CC" Value="4C" />
                                                <dx:ListEditItem Text="Autorizada SMS CC" Value="4CS" />
                                                <dx:ListEditItem Text="Asignada SMS CC" Value="15CS" />
                                                <dx:ListEditItem Text="Cancelada SMS CC" Value="2CS" />
                                                <dx:ListEditItem Text="Visita No Programada" Value="1NP" />
                                                <dx:ListEditItem Text="Visita No Programada" Value="11NP" />
                                                <dx:ListEditItem Text="Visita No Programada" Value="12NP" />
                                                <dx:ListEditItem Text="Visita No Programada" Value="15NP" />
                                                <dx:ListEditItem Text="Visita Programada" Value="1P" />
                                                <dx:ListEditItem Text="Visita Programada" Value="11P" />
                                                <dx:ListEditItem Text="Visita Programada" Value="12P" />
                                                <dx:ListEditItem Text="Visita Programada" Value="15P" />
                                                
                                            </Items>
                                        </PropertiesComboBox>
                                        <Settings AllowAutoFilter="False" HeaderFilterMode="CheckedList" />
                                    </dx:GridViewDataComboBoxColumn>
                                </Columns>

                                <SettingsDetail IsDetailGrid="True" ShowDetailRow="True" />                              
                                <Templates>
                                    <GroupRowContent>
                                        <table>
                                            <tr>
                                                <td>
                                                    <dx:ASPxCheckBox ID="checkbox" runat="server"/>
                                                </td>
                                                <td>
                                                    <dx:ASPxLabel ID="CaptionText" runat="server" Text='<%# GetCaptionText(Container) %>' />
                                                </td>
                                            </tr>
                                        </table>
                                    </GroupRowContent>
                                </Templates>
                                <GroupSummary>
                                    <dx:ASPxSummaryItem FieldName="num_cred;idOrden" SummaryType="Count" />
                                </GroupSummary>
                                <SettingsPager Position="Bottom">
                                    <PageSizeItemSettings Items="10, 20, 50, 100" Visible="true" />
                                </SettingsPager>
                                <Settings ShowHeaderFilterButton="true" />


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
                            <br />
                        </div>
                    </td>
                </tr>
            </table>
            </div>
            <dx:ASPxPopupControl ID="PopUpMovimientos" runat="server" CloseAction="CloseButton" Modal="True" CssClass="PopUpMovimientos"
                PopupHorizontalAlign="WindowCenter" ClientInstanceName="PopUpMovimientos" PopupAnimationType="Fade" EnableViewState="False" Height="180px" Width="300px">
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server" SupportsDisabledAttribute="True">
                        <div style="width: 100%; text-align: center; top: 30px; position: relative">
                            <asp:Label runat="server" ID="lblMensaje" CssClass="Titulo"></asp:Label>
                            <br />
                            <div style="height: 30px"></div>
                            <asp:Button ID="btAsignarMas" runat="server" Text="Asignar mas ordenes" OnClick="btAsignarMas_Click" CssClass="Botones" Height="30px" />
                            <br />
                        </div>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
        </div>
        <uc1:AsignacionManual runat="server" id="AsignacionManual" />
        <div style="display:none">
             <asp:Button ID="btnNada" runat="server" Visible="true" Width="0" Height="0" BorderStyle="None" OnClientClick="return false;" />
        </div>
        <asp:HiddenField runat="server" ID="OrdAsig" Value="0"></asp:HiddenField>
    </asp:Panel>
</asp:Content>
