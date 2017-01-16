<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReporteRanking.aspx.cs" Inherits="PubliPayments.Reportes.ReporteRankingIndicador.ReporteRanking" %>
<%@ Register Assembly="DevExpress.XtraReports.v15.2.Web, Version=15.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web" TagPrefix="dxxr" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>PubliPayments</title>
</head>
<body>
    <div id="Mask" style="display: none"><img src="../../imagenes/mask-loader.gif" style="position: absolute;margin: auto;top: 0;left: 0;right: 0;bottom: 0;" /></div>
    <style>
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
    </style>
    <form id="form1" runat="server">
    <div>
        <dxxr:ReportToolbar ID="ReportToolbar1" Width="100%" runat="server" ReportViewer="<%# ReportViewer1 %>"
            ShowDefaultButtons="False">
            <Styles>
                <LabelStyle>
                    <Margins MarginLeft="3px" MarginRight="3px" />
                </LabelStyle>
            </Styles>
            <Items>
                <%--<dxxr:ReportToolbarButton ItemKind="Search" ToolTip="Display the search window" />
                <dxxr:ReportToolbarSeparator />
                <dxxr:ReportToolbarButton ItemKind="PrintReport" ToolTip="Print the report" />
                <dxxr:ReportToolbarButton ItemKind="PrintPage" ToolTip="Print the current page" />
                <dxxr:ReportToolbarSeparator />--%>
                <dxxr:ReportToolbarButton Enabled="False" ItemKind="FirstPage" ToolTip="First Page" />
                <dxxr:ReportToolbarButton Enabled="False" ItemKind="PreviousPage" ToolTip="Previous Page" />
                <dxxr:ReportToolbarLabel Text="Page" />
                <dxxr:ReportToolbarComboBox ItemKind="PageNumber" Width="65px">
                </dxxr:ReportToolbarComboBox>
                <dxxr:ReportToolbarLabel Text="of" />
                <dxxr:ReportToolbarTextBox IsReadOnly="True" ItemKind="PageCount" />
                <dxxr:ReportToolbarButton ItemKind="NextPage" ToolTip="Next Page" />
                <dxxr:ReportToolbarButton ItemKind="LastPage" ToolTip="Last Page" />
                <dxxr:ReportToolbarSeparator />
                <dxxr:ReportToolbarButton ItemKind="SaveToDisk" ToolTip="Exportar" />
                <%--<dxxr:ReportToolbarButton ItemKind="SaveToWindow" ToolTip="Export a report and show it in a new window" />--%>
                <dxxr:ReportToolbarComboBox ItemKind="SaveFormat" Width="70px">
                    <Elements>
                        <dxxr:ListElement Text="Pdf" Value="pdf" />
                        <dxxr:ListElement Text="Xls" Value="xls" />
                        <dxxr:ListElement Text="Xlsx" Value="xlsx" />
                        <dxxr:ListElement Text="Csv" Value="csv" />
                    </Elements>
                </dxxr:ReportToolbarComboBox>
            </Items>
        </dxxr:ReportToolbar>
        <dxxr:ReportViewer ID="ReportViewer1" runat="server" OnUnload="ReportViewer1_Unload">
        </dxxr:ReportViewer>
    
    </div>
    </form>
    <script language="javascript" type="text/javascript">

        var base1 = ASPxClientReportViewer.prototype.SaveToWindow;
        var base2 = ASPxClientReportViewer.prototype.SaveToDisk;

        ASPxClientReportViewer.prototype.SaveToWindow = function (format) {
            alert("hola");
            base1.call(this, format);
        };

        ASPxClientReportViewer.prototype.SaveToDisk = function (format) {
            document.getElementById("Mask").style.display="block";
            try {
                base2.call(this, format);
            } catch (err) {
                alert("Error al exportar el reporte.");
            } finally {
                //document.getElementById("Mask").style.display = "none";    
            }
            
            
        };

</script>
</body>
</html>