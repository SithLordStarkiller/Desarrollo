<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmVisorReporte.aspx.cs" Inherits="mvcSICER.Generales.frmVisorReporte" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
   <form id="form1" runat="server">
    <div class="reporte">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        
          <rsweb:ReportViewer ID="rvReporte" runat="server" Width="100%" ShowPromptAreaButton="False"
            SizeToReportContent="True"
            ShowFindControls="False" ShowPrintButton="False" ShowRefreshButton="False">
        </rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>