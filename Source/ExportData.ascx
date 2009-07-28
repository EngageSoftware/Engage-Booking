<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExportData.ascx.cs" Inherits="Engage.Dnn.Booking.ExportData" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<div>
    <div><asp:Label ResourceKey="StartDatePickerLabel" runat="server" /><telerik:RadDatePicker ID="StartDatePicker" ResourceKey="StartDatePicker" runat="server" /></div>
    <div><asp:Label ResourceKey="EndDatePickerLabel" runat="server" /> <telerik:RadDatePicker ID="EndDatePicker" ResourceKey="EndDatePicker" runat="server" /></div>
</div>
<div>
    <asp:Button ID="ExportButton" ResourceKey="ExportDataButton" OnClick="ExportButton_Click" runat="server" />
    <asp:CheckBox ID="HeaderRowCheckBox" ResourceKey="HeaderRowCheckBox" runat="server" />
</div>