<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExportData.ascx.cs" Inherits="Engage.Dnn.Booking.ExportData" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<div>
    <div>Start Date: <telerik:RadDatePicker ID="StartDatePicker" runat="server" /></div>
    <div>End Date: <telerik:RadDatePicker ID="EndDatePicker" runat="server" /></div>
</div>
<div>
    <asp:Button ID="ExportButton" Text="Export Data" OnClick="ExportButton_Click" runat="server" />
</div>