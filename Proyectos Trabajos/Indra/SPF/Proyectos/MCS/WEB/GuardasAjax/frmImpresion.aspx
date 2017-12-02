<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmImpresion.aspx.cs" Inherits="frmImpresion" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>::Impresión::</title>
    <style type="text/css">
        body, html
        {
            margin: 0;
            font-family: Verdana, Geneva, sans-serif;
        }
        #contenedor
        {
            height: 70px;
            position: absolute;
            top: 28%;
            background: #DCDCDC;
            text-align: center;
            border-top: 1px solid #D0D0D0;
            border-bottom: 1px solid #D0D0D0;
            border-left: 1px #DCDCDC;
            border-right: 1px #DCDCDC;
            margin-top: 0px;
        }
        .boton
        {
            border: 1px solid gray;
            font-size: 10px;
        }
        .fuente
        {
            font-size: 7pt;
            font-family: Verdana;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
    <div id="contenedor">
        <table width="100%">
            <tr>
                <td align="center">
                    <table width="400px" style="padding-left: 1em; padding-right: 1em;">
                        <tr>
                            <td>
                                <img src="imagenes/imgDescargar.png" height="50" width="50" alt=""/>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td class="fuente">
                                            FORMATOS GENERADOS EXITOSAMENTE.
                                        </td>
                                    </tr>
                                    </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div style="position: absolute; top: -80%">
        <rsweb:ReportViewer ID="rvReporte" runat="server" ShowWaitControlCancelLink="False"
            ShowZoomControl="False" ShowPrintButton="False" Height="0px" PromptAreaCollapsed="True"
            ShowBackButton="False" ShowCredentialPrompts="False" ShowDocumentMapButton="False"
            ShowFindControls="False" ShowPageNavigationControls="False" ShowParameterPrompts="False"
            ShowPromptAreaButton="False" ShowRefreshButton="False" ZoomPercent="1" DocumentMapWidth="0%"
            ToolBarItemBorderWidth="0px" ZoomMode="FullPage" Width="267px" BackColor="White"
            InternalBorderColor="White" LinkActiveColor="Black" LinkActiveHoverColor="Black"
            LinkDisabledColor="Black">
        </rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
