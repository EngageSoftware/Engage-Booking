<%@ Import Namespace="System.Globalization"%>
<%@ Control Language="c#" AutoEventWireup="True" Inherits="Engage.Dnn.Booking.Settings" CodeBehind="Settings.ascx.cs" %>
<%@ Register TagPrefix="dnn" TagName="label" Src="~/controls/labelControl.ascx" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<style type="text/css">
    @import url(<%=Engage.Dnn.Framework.ModuleBase.ApplicationUrl %><%=Engage.Dnn.Framework.Utility.GetDesktopModuleFolderName(Engage.Dnn.Booking.Utility.DesktopModuleName) %>Module.css);
</style>

<asp:UpdatePanel runat="server" UpdateMode="Conditional">
    <ContentTemplate>        
        <div class="booking-setting">
            <dnn:label ResourceKey="AppointmentRequestsRoleLabel" runat="server" CssClass="SubHead" ControlName="AppointmentRequestsRoleCheckBox" />
            <span>
                <asp:DropDownList ID="AppointmentRequestsRoleDropDownList" runat="server" />
            </span>
        </div>
        <div class="booking-setting">
            <dnn:label runat="server" controlname="SkinDropDownList" ResourceKey="SkinLabel" CssClass="SubHead"/>
            <asp:dropdownlist id="SkinDropDownList" Runat="server" />
        </div>
        <div class="booking-setting">
            <dnn:label runat="server" controlname="AppointmentsPerDayTextBox" ResourceKey="AppointmentsPerDayLabel" CssClass="SubHead"/>
            <span class="NumericTextBoxWrapper">
                <telerik:radnumerictextbox id="AppointmentsPerDayTextBox" runat="server" maxlength="3" maxvalue="100" minvalue="1" showspinbuttons="True"> 
                    <NumberFormat AllowRounding="True" DecimalDigits="0"/>
                </telerik:radnumerictextbox>
            </span>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="AppointmentsPerDayTextBox" ResourceKey="AppointmentsPerDayRequiredValidator" CssClass="NormalRed" Display="None" />
        </div>
        <div class="booking-setting">
            <dnn:label runat="server" ControlName="RecordsPerPageTextBox" ResourceKey="PagingLabel" CssClass="SubHead" />
            <span class="NumericTextBoxWrapper">
                <telerik:radnumerictextbox id="RecordsPerPageTextBox" runat="server" maxlength="3" maxvalue="100" minvalue="1" showspinbuttons="True"> 
                    <NumberFormat AllowRounding="True" DecimalDigits="0"/>
                </telerik:radnumerictextbox>
            </span>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="RecordsPerPageTextBox" ResourceKey="RecordsPerPageRequiredValidator" CssClass="NormalRed" Display="None" />
        </div>
        <div class="booking-setting">
            <dnn:label runat="server" controlname="NotificationEmailsList" ResourceKey="NotificationEmailsListLabel" CssClass="SubHead"/>
            <asp:TextBox id="NotificationEmailsListTextBox" runat="server" />
            <asp:RegularExpressionValidator ID="NotificationEmailsListValidator" runat="server" ControlToValidate="NotificationEmailsListTextBox" ResourceKey="NotificationEmailsListValidator" CssClass="NormalRed" Display="None" />
        </div>

        <asp:ValidationSummary runat="server" ShowMessageBox="false" ShowSummary="true" CssClass="NormalRed" />
    </ContentTemplate>
</asp:UpdatePanel>