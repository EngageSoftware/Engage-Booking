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
            <div class="NumericTextBoxWrapper">
                <telerik:radnumerictextbox id="AppointmentsPerDayTextBox" runat="server" maxlength="3" maxvalue="100" minvalue="1" showspinbuttons="True"> 
                    <NumberFormat AllowRounding="True" DecimalDigits="0"/>
                </telerik:radnumerictextbox>
            </div>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="AppointmentsPerDayTextBox" ResourceKey="AppointmentsPerDayRequiredValidator" CssClass="NormalRed" Display="None" />
        </div>
        <div class="booking-setting">
            <dnn:label runat="server" ControlName="RecordsPerPageTextBox" ResourceKey="PagingLabel" CssClass="SubHead" />
            <div class="NumericTextBoxWrapper">
                <telerik:radnumerictextbox id="RecordsPerPageTextBox" runat="server" maxlength="3" maxvalue="100" minvalue="1" showspinbuttons="True"> 
                    <NumberFormat AllowRounding="True" DecimalDigits="0"/>
                </telerik:radnumerictextbox>
            </div>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="RecordsPerPageTextBox" ResourceKey="RecordsPerPageRequiredValidator" CssClass="NormalRed" Display="None" />
        </div>
        <div class="booking-setting">
            <dnn:label runat="server" controlname="NotificationEmailsList" ResourceKey="NotificationEmailsListLabel" CssClass="SubHead"/>
            <asp:TextBox id="NotificationEmailsListTextBox" runat="server" />
            <asp:RegularExpressionValidator ID="NotificationEmailsListValidator" runat="server" ControlToValidate="NotificationEmailsListTextBox" ResourceKey="NotificationEmailsListValidator" CssClass="NormalRed" Display="None" />
        </div>
        <div class="booking-setting">
            <dnn:label runat="server" controlname="DefaultAppointmentDuration" ResourceKey="DefaultAppointmentDurationLabel" CssClass="SubHead"/>
            <div class="NumericTextBoxWrapper">
                <telerik:radnumerictextbox id="DefaultAppointmentDurationHoursTextBox" runat="server" maxlength="3" maxvalue="200" minvalue="0" showspinbuttons="True"> 
                    <NumberFormat AllowRounding="True" DecimalDigits="0"/>
                </telerik:radnumerictextbox>
                <asp:Label runat="server" ResourceKey="DefaultAppointmentHoursLabel" AssociatedControlID="DefaultAppointmentDurationHoursTextBox" />

                <telerik:radnumerictextbox id="DefaultAppointmentDurationMinutesTextBox" runat="server" maxlength="2" maxvalue="59" minvalue="0" showspinbuttons="True"> 
                    <NumberFormat AllowRounding="True" DecimalDigits="0"/>
                </telerik:radnumerictextbox>
                <asp:Label runat="server" ResourceKey="DefaultAppointmentMinutesLabel" AssociatedControlID="DefaultAppointmentDurationMinutesTextBox" />
            </div>
            <asp:CustomValidator ID="DefaultAppointmentDurationValidator" runat="server" ResourceKey="DefaultAppointmentDurationValidator" CssClass="NormalRed" Display="None" />
        </div>
        <div class="booking-setting">
            <dnn:label runat="server" controlname="MinimumAppointmentDuration" ResourceKey="MinimumAppointmentDurationLabel" CssClass="SubHead"/>
            <div class="NumericTextBoxWrapper">
                <telerik:radnumerictextbox ID="MinimumAppointmentDurationHoursTextBox" runat="server" MaxLength="3" MinValue="0" MaxValue="200" ShowSpinButtons="True"> 
                    <NumberFormat AllowRounding="True" DecimalDigits="0"/>
                </telerik:radnumerictextbox>
                <asp:Label runat="server" ResourceKey="MinimumAppointmentHoursLabel" AssociatedControlID="MinimumAppointmentDurationHoursTextBox" />

                <telerik:radnumerictextbox ID="MinimumAppointmentDurationMinutesTextBox" runat="server" MinValue="0" MaxValue="59" ShowSpinButtons="True"> 
                    <NumberFormat AllowRounding="True" DecimalDigits="0"/>
                </telerik:radnumerictextbox>
                <asp:Label runat="server" ResourceKey="MinimumAppointmentMinutesLabel" AssociatedControlID="MinimumAppointmentDurationMinutesTextBox" />
            </div>
            <asp:CustomValidator ID="MinimumAppointmentDurationValidator" runat="server" ResourceKey="MinimumAppointmentDurationValidator" CssClass="NormalRed" Display="None" />
        </div>
        <div class="booking-setting">
            <dnn:label runat="server" controlname="MaximumAppointmentDuration" ResourceKey="MaximumAppointmentDurationLabel" CssClass="SubHead"/>
            <div class="NumericTextBoxWrapper">
                <telerik:radnumerictextbox id="MaximumAppointmentDurationHoursTextBox" runat="server" maxlength="3" minvalue="0" maxvalue="200" showspinbuttons="True"> 
                    <NumberFormat AllowRounding="True" DecimalDigits="0"/>
                </telerik:radnumerictextbox>
                <asp:Label runat="server" ResourceKey="MaximumAppointmentHoursLabel" AssociatedControlID="MaximumAppointmentDurationHoursTextBox" />

                <telerik:radnumerictextbox id="MaximumAppointmentDurationMinutesTextBox" runat="server" minvalue="0" maxvalue="59" showspinbuttons="True"> 
                    <NumberFormat AllowRounding="True" DecimalDigits="0"/>
                </telerik:radnumerictextbox>
                <asp:Label runat="server" ResourceKey="MaximumAppointmentMinutesLabel" AssociatedControlID="MaximumAppointmentDurationMinutesTextBox" />
            </div>
            <asp:CustomValidator ID="MaximumAppointmentDurationValidator" runat="server" ResourceKey="MaximumAppointmentDurationValidator" CssClass="NormalRed" Display="None" />
            <asp:CustomValidator ID="DurationCompareValidator" runat="server" ResourceKey="DurationCompareValidator" Display="None" />
        </div>
        <div class="booking-setting">
             <dnn:label runat="server" controlname="MaximumConcurrentAppointments" ResourceKey="MaximumConcurrentAppointmentsLabel" CssClass="SubHead"/>
             <div class="NumericTextBoxWrapper">
                <telerik:radnumerictextbox id="MaximumConcurrentAppointmentsTextBox" runat="server" minvalue="0" maxvalue="999" showspinbuttons="True">
                    <NumberFormat AllowRounding="True" DecimalDigits="0"/>
                </telerik:radnumerictextbox>
             </div>
             <asp:CustomValidator ID="MaximumConcurrentAppointmentsValidator" runat="server" ResourceKey="MaximumConcurrentAppointmentsValidator" CssClass="NormalRed" Display="None" />
        </div>
        <asp:ValidationSummary runat="server" ShowMessageBox="false" ShowSummary="true" CssClass="NormalRed" />
    </ContentTemplate>
</asp:UpdatePanel>