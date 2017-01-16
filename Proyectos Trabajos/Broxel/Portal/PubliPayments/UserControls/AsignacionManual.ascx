<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AsignacionManual.ascx.cs" Inherits="PubliPayments.UserControls.AsignacionManual" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v15.2, Version=15.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
    <style type="text/css">
        #AreasTexto div{float: left}
        #Resultado{width: 100%;overflow-y: scroll;overflow-x: scroll;}
        #TCreditosManual{width: 100%;font: 14px 'Open Sans', sans-serif;}
        #TCreditosManual th,#TCreditosManual td{border: 1px black solid;}
        #BtAsignarManual,#BtCancelarManual,#BtReasignarManual,#BtAutorizarManual,#Cargar{width: 150px;height: 30px;margin-top: 10px}
        #BtAsignarManual,#BtCancelarManual,#BtReasignarManual,#BtAutorizarManual{ margin-top: 32px;margin-right: 10px}
        .FooterCargaM{width: 50%; text-align: center;height: 30px;margin-top: 20px;float: left;}
        #ContentPlaceHolder1_AsignacionManual_PopUpAsignacionM_PW-1{ width: 850px!important;}
        #headerCargaM{height:46px; width: 100%;text-align: center;font: 13px 'Open Sans', sans-serif;}
        #TextAreaEdicion {height:400px; width: 100%;overflow-y: scroll;overflow-x: scroll;-moz-resize:none;-ms-resize:none;-o-resize:none;resize:none;font: 14px 'Open Sans', sans-serif;}
        .MaskAjax {
                background-color: black;
                display:    none;
                position:   fixed;
                z-index:    12001;
                top:        0;
                left:       0;
                height:     100%;
                width:      100%;
                -ms-opacity: 0.2;
                opacity: 0.2;
        }
        body.MaskAjax {overflow: hidden;}
        body.loading .MaskAjax {display: block;}
        #BtnProcesar.IE8,#BtnLimpiaM.IE8,#BtnProcesar.IE9,#BtnLimpiaM.IE9 { width: 73px;margin-left: 4px;height: 30px;}
        #BtnProcesar,#BtnLimpiaM {width: 80px;height: 30px;}
        #BtnLimpiaM {margin-top: 100px;}
    </style>
    <script src="../Scripts/AsignacionManual.js?version=1.01"></script>        
<!--[if IE 8]> <script type="text/javascript"> EmptyTableRows = 25;Explorer = 9; </script> <![endif]-->
<!--[if IE 9]> <script type="text/javascript"> EmptyTableRows = 24;Explorer = 8; </script> <![endif]-->
    <dx:ASPxPopupControl ID="PopUpAsignacionM" runat="server" CloseAction="CloseButton" Modal="True" PopupVerticalAlign="TopSides" PopupVerticalOffset="50" Width="750px"
                PopupHorizontalAlign="WindowCenter" ClientInstanceName="PopUpAsignacionM" PopupAnimationType="Fade" EnableViewState="False" Height="600px" CssClass="PopUpAsignacionM" ShowHeader="True" AllowDragging="True" >
                <HeaderContentTemplate>
                <div>Carga personalizada de créditos</div>
            </HeaderContentTemplate>
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server" SupportsDisabledAttribute="True">
                        <div id="headerCargaM">
                            Como separadores, los caracteres permitidos son: (,) (|) (;)  y tabulador. ej: crédito|usuario|auxiliar<br/>Se recomienda procesar lotes menores a 5 mil créditos para un mejor rendimiento.
                            <br>Los datos obligatorios para: <label id="ContainerPage1">(Asignar -> Credito|Usuario )</label> <label id="ContainerPage2">(Cambiar asignación -> Crédito|NuevoUsuario ; Cancelar -> Crédito  )</label> <label id="ContainerPage3">(Autorizar,Cancelar,Reasignar -> Crédito ; Cambiar asignación -> Crédito|NuevoUsuario)</label>
                        </div>
                        <div style="height:400px; width: 100%;text-align: center;margin-top: 10px" id="AreasTexto">
                            <div style="height: 100%;width: 365px;" id="Edicion">
                                <textarea id="TextAreaEdicion"></textarea>
                            </div>
                            <div style="width: 85px;margin-top: 120px" >
                                <button class="Botones" id="BtnProcesar">Procesar</button>
                                <button class="Botones" id="BtnLimpiaM" >Reset</button>
                            </div>
                            <div style="height: 400px;width: 365px" id="Resultado">
                                <table id="TCreditosManual">
                                    <thead><tr><td>Crédito</td> <td>Usuario</td> <td>Auxiliar</td></tr></thead>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                        </div>

                        <div class="FooterCargaM" >
                            <input type="file" id="archivo"  accept=".csv,application/vnd.ms-excel,text/plain"/><br/>
                            <button id="Cargar" class="Botones">Cargar</button>
                        </div>
                        <div class="FooterCargaM" >
                            <button class="Botones" id="BtAutorizarManual">Autorizar</button>
                            <button class="Botones" id="BtAsignarManual">Asignar</button>
                            <button class="Botones" id="BtCancelarManual">Cancelar</button>
                            <button class="Botones" id="BtReasignarManual">Reasigar</button>
                        </div>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
<div class="MaskAjax" style="display: none"><img src="../imagenes/mask-loader.gif" style="position: absolute;margin: auto;top: 0;left: 0;right: 0;bottom: 0;" /></div>