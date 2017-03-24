﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportForm.aspx.cs" Inherits="ViewsStatic_Report" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="reportForm" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div style="overflow: hidden">
            <rsweb:ReportViewer ID="mainReportViewer" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" ShowPrintButton="true" AsyncRendering="false" SizeToReportContent="True" runat="server">
            </rsweb:ReportViewer>           
        </div>
    </form>
</body>
</html>
