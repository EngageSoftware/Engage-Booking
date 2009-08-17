<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ExportData.ascx.cs" Inherits="Engage.Dnn.Booking.ExportData" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="exportdata">
    <ol class="exportdata-form">
        <li class="startdate">
            <asp:Label ResourceKey="StartDatePickerLabel" CssClass="startdate-label" runat="server" />
            <telerik:RadDatePicker ID="StartDatePicker" ResourceKey="StartDatePicker" CssClass="startdate-picker" runat="server" />
        </li>
        <li class="enddate">
            <asp:Label ResourceKey="EndDatePickerLabel" CssClass="enddate-label" runat="server" />
            <telerik:RadDatePicker ID="EndDatePicker" ResourceKey="EndDatePicker" CssClass="enddate-picker" runat="server" />
        </li>
        <li class="exportbutton">
            <asp:Button ID="ExportButton" ResourceKey="ExportDataButton" CssClass="export-button" runat="server" />
            <asp:CheckBox ID="HeaderRowCheckBox" ResourceKey="HeaderRowCheckBox" CssClass="header-checkbox" runat="server" Checked="true" />
        </li>
    </ol>
</div>
