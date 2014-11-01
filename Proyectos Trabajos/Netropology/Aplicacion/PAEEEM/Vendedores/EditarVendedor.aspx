﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditarVendedor.aspx.cs" Inherits="PAEEEM.Vendedores.EditarVendedor" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <script type="text/javascript">
            var pre = "ctl00_MainContent_";

            function postackControl() {
                var oButton = document.getElementById(pre + "btnRefres");
                oButton.click();
            }
            function OnClientValidationFailedUploadArchivo(sender, args) {
                alert("Ocurrió un problema al cargar el archivo");
            }

            function confirmCallBackFn(arg) {
                if (arg == true) {
                    var oButton = document.getElementById("ctl00_MainContent_" + "HiddenButton");
                    oButton.click();
                }
            }
            </script>
    </telerik:RadScriptBlock>
    <script type="text/javascript">
        function poponload() {
            var testwindow = window.open('VisorImagenes.aspx?tipo=1&Id=0', '', 'top=0,left=0,toolbar=no,menubar=no,scrollbars=yes, resizable=yes,location=no, status=no');
            testwindow.moveTo(0, 0);
        }


        function OnClientClicking(sender, args) {
            var event = args.get_domEvent();
            if (event.keyCode == 13) {
                args.set_cancel(true);
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="LabelEncabezado" runat="server" Text="Editar vendedor"
        Font-Size="medium" ForeColor="#00A3D9" Font-Bold="True"></asp:Label>
    <hr class="ruleNet" />
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Office2010Blue"></telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxPanel ID="RadAjaxPanel2" LoadingPanelID="RadAjaxLoadingPanel1" runat="server" Width="100%">
        <fieldset class="fieldset_Netro">
            <table width="100%">
                <tr>
                    <td style="width: 12%">
                        <br />
                        <asp:Label ID="Label2" runat="server" Text="NOMBRE(S):" Font-Size="X-SMALL"></asp:Label>
                    </td>
                    <td style="width: 12%">
                        <br />
                        <telerik:RadTextBox ID="txtNombre" runat="server" Font-Size="SMALL" MaxLength="60" Enabled="False">
                        </telerik:RadTextBox>
                    </td>
                    <td style="width: 12%">
                        <asp:Label ID="lblAP" runat="server" Text="APELLIDO PATERNO:" Font-Size="X-Small"></asp:Label>

                    </td>
                    <td style="width: 12%">
                        <telerik:RadTextBox ID="txtAP" runat="server" Font-Size="SMALL" MaxLength="60" Enabled="False">
                        </telerik:RadTextBox>
                    </td>
                    <td style="width: 12%">
                        <asp:Label ID="lblAM" runat="server" Text="APELLIDO MATERNO:" Font-Size="X-Small"></asp:Label>

                    </td>
                    <td style="width: 12%">
                        <telerik:RadTextBox ID="txtAM" runat="server" Font-Size="SMALL" MaxLength="60" Enabled="False">
                        </telerik:RadTextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblFecha" runat="server" Text="FECHA DE NACIMIENTO:" Font-Size="X-Small"></asp:Label>
                    </td>
                    <td >
                        <telerik:RadDatePicker ID="rdpFecha" runat="server" Font-Size="SMALL" Enabled="False"></telerik:RadDatePicker>
                    </td>
                </tr>
                <tr>
                    <td style="width: 12%"><br />
                        <asp:Label ID="lblCurp" runat="server" Text="CURP:" Font-Size="X-Small"></asp:Label>
                    </td>
                    <td style="width: 12%"><br />
                        <telerik:RadTextBox ID="txtCurp" runat="server" Font-Size="SMALL" MaxLength="18"  Enabled="False">
                        </telerik:RadTextBox>
                    </td>
                    <td>
                        <br />
                        <asp:Label ID="lblIdentificacion" runat="server" Text="IDENTIFICACIÓN OFICIAL:" Font-Size="X-Small"></asp:Label>
                    </td>
                    <td>
                        <br />
                        <telerik:RadComboBox ID="cmbIdentificacion" runat="server" Font-Size="SMALL">
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <br />
                        <asp:Label ID="lblNumIden" runat="server" Text="NÚMERO IDENTIFICACIÓN:" Font-Size="X-Small"></asp:Label>

                    </td>
                    <td>
                        <br />
                        <telerik:RadTextBox ID="txtNumIdenti" runat="server" Font-Size="SMALL" MaxLength="18">
                        </telerik:RadTextBox>
                    </td>
                    <td>
                        <br />
                        <asp:Label ID="lblAcceso" runat="server" Text="ACCESO AL SISTEMAL:" Font-Size="X-Small"></asp:Label>

                    </td>
                    <td>
                        <br />
                        <telerik:RadComboBox ID="cmbAcceso" runat="server" Font-Size="SMALL">
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Text="Seleccione" />
                                <telerik:RadComboBoxItem runat="server" Text="NO" Value="0" />
                                <telerik:RadComboBoxItem runat="server" Text="SI" Value="1" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" aling="left" >
                        <br />
                        <asp:Label ID="Label1" runat="server" Text="ARCHIVO IDENTIFICACIÓN OFICIAL" Font-Size="X-Small"> </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="1">
                        <telerik:RadAsyncUpload runat="server" ID="UploadedImagen"
                            MaxFileInputsCount="1"
                            Style="margin-left: auto"
                            Localization-Select="Examinar" Width="80px"
                            OnClientFilesUploaded="postackControl" 
                            OnFileUploaded="UploadedImagen_OnFileUploaded"
                            onclientfileuploadfailed="OnClientValidationFailedUploadArchivo" 
                                onclientvalidationfailed="OnClientValidationFailedUploadArchivo">
                            
                            <FileFilters>
                                <telerik:FileFilter Description="Images(emf;wmf;jpg;jpeg;jpe;png;bmp;tif)" Extensions="emf,wmf,jpg,jpeg,jpe,png,bmp,tif" />
                            </FileFilters>
                            <Localization Select="Examinar" />
                        </telerik:RadAsyncUpload>
                        
                        </td>
                  </tr>
                <tr>
                    <td colspan="2">
                        <asp:Image ID="ImgCorrecto" runat="server" ImageUrl="~/CentralModule/images/icono_correcto.png" ToolTip="Correto" Height="25px" Width="25px" Visible="False" />&nbsp;
                        <asp:ImageButton runat="server" ID="verImagen"  ToolTip="Ver" Height="20px" Width="20px" OnClientClick="OnClientClicking;poponload()"
                            ImageUrl="~/CirculoCredito/imagenes/Buscar.png" Visible="false" />&nbsp;
                        <asp:ImageButton runat="server" ID="EliminarImagen"  ToolTip="Eliminar" Height="20px" Width="20px"
                            ImageUrl="~/CirculoCredito/imagenes/delete.gif" Visible="false" OnClick="EliminarImagen_OnClick"/>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="8" align="center">
                        <br/>
                        <telerik:RadButton runat="server" Text="Guardar" ID="btnGuardar" OnClick="btnGuardar_OnClick"></telerik:RadButton>&nbsp;&nbsp;
                        <telerik:RadButton runat="server" Text="Salir" ID="btnSalir" OnClick="btnSalir_OnClick"></telerik:RadButton>
                    </td>
                </tr>
            </table>
        </fieldset>
    </telerik:RadAjaxPanel>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true"></telerik:RadWindowManager>
    <div style="display: none">
        <asp:Button ID="btnRefres" runat="server" Text="Button"
             />
        <asp:Button ID="HiddenButton" BackColor="#FFFFFF"  runat="server" Width="0px" />
    </div>
</asp:Content>
