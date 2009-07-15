<%@ Control Language="c#" AutoEventWireup="false" Inherits="Engage.Dnn.Booking.Appointments.AppointmentRequest" CodeBehind="AppointmentRequest.ascx.cs" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.UI.WebControls" Assembly="DotNetNuke" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx" %>
<%@ Register TagPrefix="engage" TagName="ModuleMessage" Src="../Controls/ModuleMessage.ascx" %>
<%@ Register TagPrefix="engage" Namespace="Engage.Controls" Assembly="Engage.Framework" %>
<engage:ModuleMessage runat="server" ID="SuccessModuleMessage" MessageType="Success" TextResourceKey="AddEventSuccess" CssClass="AddEventSuccessMessage" />

<div>
    <fieldset>
        <legend>Service Request Form</legend>
        <p class="note">
            <strong>Note: </strong>Fields with asteriks (*) denote required fields.</p>
        <fieldset>
            <legend>Interpreting Event</legend>
            <div class="interpreting-form">
                <div class="event-type medium">
                    <asp:Label ID="EventType" CssClass="event-type-label" runat="server" Text="Type of Event" AssociateControlId="EventType" />
                    <asp:TextBox ID="EventTypeBox" CssClass="event-type-box" runat="server" />
                </div>
                <div class="Title long">
                    <asp:Label ID="TitleLabel" CssClass="title-label" runat="server" Text="Title" AssociateControlId="TitleTextBox" />
                    <asp:TextBox ID="TitleTextBox" CssClass="title-box" runat="server" />
                </div>
                <div class="description full">
                    <asp:Label ID="DescriptionLabel" CssClass="description-label" runat="server" Text="Description of Event" AssociateControlId="DescriptionTextBox" />
                    <asp:TextBox ID="DescriptionTextBox" CssClass="description-box" runat="server" />
                </div>
                <div class="considerations full">
                    <asp:Label ID="ConsiderationsLabel" CssClass="considerations-label" runat="server" Text="Special Considerations" AssociateControlId="ConsiderationsTextBox" />
                    <asp:TextBox ID="ConsiderationsTextBox" CssClass="considerations-box" runat="server" />
                </div>
            </div>
            <h4>
                Address</h4>
            <div class="interpreting-form-b">
                <div class="street long">
                    <asp:Label ID="StreetLabel" CssClass="street-label" runat="server" Text="Street" AssociateControlId="StreetTextBox" />
                    <asp:TextBox ID="StreetTextBox" CssClass="street-box"  runat="server" />
                </div>
                <div class="room medium">
                    <asp:TextBox ID="RoomTextBox" CssClass="room-box" runat="server" />
                </div>
                <div class="city long">
                    <asp:Label ID="CityLabel" CssClass="city-label" runat="server" Text="City" AssociateControlId="CityTextBox" />
                    <asp:TextBox ID="CityTextBox" CssClass="city-box" runat="server" />
                </div>
                <div class="state medium">
                    <asp:Label ID="StateLabel" CssClass="state-label" runat="server" />
                    <asp:DropDownList ID="StateDropDownList" runat="server" CssClass="state-list"></asp:DropDownList>
                </div>
                <div class="zip short">
                    <asp:TextBox ID="ZipBox" CssClass="zip-box" runat="server" />
                </div>
                <div class="nearest long">
                    <asp:Label ID="Nearest" CssClass="nearest-label" runat="server" Text="If DC/MD/VA, nearest Metro and nearest cross street" AssociateControlId="NearestBox" />
                    <asp:TextBox ID="NearestBox" CssClass="nearest-box" runat="server" />
                </div>
            </div>
            <h4>
                On-site Contact</h4>
            <div class="interpreting-form-c">
                <div class="street long">
                    <asp:Label ID="OnSiteStreet" CssClass="street-label" runat="server" Text="Street" AssociateControlId="StreetBox" />
                    <asp:TextBox ID="OnSiteStreetBox" CssClass="street-box"  runat="server" />
                </div>
                <div class="phone">
                    <asp:Label ID="Phone" CssClass="phone-label"  runat="server" Text="Phone" AssociateControlId="PhoneBox" />
                    <asp:TextBox ID="PhoneBox" CssClass="phone-box"  runat="server" />
                </div>
            </div>
        </fieldset>
        <fieldset>
            <legend>Person Requesting Interpreting Services</legend>
            <div class="requesting-form">
                <div class="full-name long">
                    <asp:Label ID="FullName" CssClass="full-name-label"  runat="server" Text="Full Name" AssociateControlId="FullNameBox" />
                    <asp:TextBox ID="FullNameBox" CssClass="full-name-box"  runat="server" />
                </div>
                <div class="phone">
                    <asp:Label ID="RequestorPhone" CssClass="phone-label"  runat="server" Text="Phone" AssociateControlId="PhoneBox" />
                    <asp:TextBox ID="RequestorPhoneBox" CssClass="phone-box"  runat="server" />
                </div>
                <div class="alt-phone">
                    <asp:Label ID="AltPhone" CssClass="alt-phone-label"  runat="server" Text="Alternate Phone" AssociateControlId="AltPhoneBox" />
                    <asp:TextBox ID="AltPhoneBox" CssClass="alt-phone-box"  runat="server" />
                </div>
                <div class="email long">
                    <asp:Label ID="Email" CssClass="email-label"  runat="server" Text="Email Address" AssociateControlId="EmailBox" />
                    <asp:TextBox ID="EmailBox" CssClass="email-box"  runat="server" />
                </div>
            </div>
        </fieldset>
        <fieldset>
            <legend>Date of Assignment</legend>
            <div class="date-assignment-form">
                <div class="start medium">
                    <asp:Label ID="Start" CssClass="start-label"  runat="server" Text="Start" AssociateControlId="StartBox" />
                    <asp:TextBox ID="StartBox" CssClass="start-box"  runat="server" />
                    <telerik:RadDateTimePicker runat="server" ID="StartDateTimePicker" Skin="WebBlue">
                        <TimeView Skin="WebBlue" />
                        <Calendar Skin="WebBlue" ShowRowHeaders="false" />
                        <DateInput InvalidStyleDuration="100" />
                        <ClientEvents OnDateSelected="StartDateTimePicker_DateSelected" />
                    </telerik:RadDateTimePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="StartDateTimePicker" ResourceKey="StartDateTimePickerRequired" Display="None" EnableClientScript="false" />
                </div>
                <div class="at-time short">
                    <asp:Label ID="AtTime" CssClass="at-time-label"  runat="server" Text="at" AssociateControlId="AtTimeBox" />
                    <asp:TextBox ID="AtTimeBox" CssClass="at-time-box"  runat="server" />
                </div>
                <div class="end medium">
                    <asp:Label ID="End" CssClass="end-label"  runat="server" Text="End:" AssociateControlId="EndBox" />
                    <asp:TextBox ID="EndBox" CssClass="end-box"  runat="server" />
                    <telerik:RadDateTimePicker runat="server" ID="EndDateTimePicker" Skin="WebBlue">
                        <TimeView Skin="WebBlue" />
                        <Calendar Skin="WebBlue" ShowRowHeaders="false" />
                        <DateInput InvalidStyleDuration="100" />
                    </telerik:RadDateTimePicker>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" Display="None" EnableClientScript="false" ControlToCompare="StartDateTimePicker" ControlToValidate="EndDateTimePicker" ResourceKey="EndDateCompareValidator" Operator="GreaterThan" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="EndDateTimePicker" ResourceKey="EndDateTimePickerRequired" Display="None" EnableClientScript="false" />
                </div>
                <div class="at-time short">
                    <asp:Label ID="AtEndTime" CssClass="at-time-label"  runat="server" Text="at" AssociateControlId="AtTimeBox" />
                    <asp:TextBox ID="AtEndTimeBox" CssClass="at-time-box"  runat="server" />
                </div>
            </div>
        </fieldset>
        <fieldset>
            <legend>Participants</legend>
            <div class="participants-form">
                <div class="number-deaf short">
                    <asp:Label ID="NumberDeaf" CssClass="number-deaf-label"  runat="server" Text="Number of Deaf Participants" AssociateControlId="NumberDeafBox" />
                    <asp:TextBox ID="NumberDeafBox" CssClass="number-deaf-box"  runat="server" />
                </div>
                <div class="total-number short">
                    <asp:Label ID="TotalNumber" CssClass="total-number-label"  runat="server" Text="Total Number of Participants" AssociateControlId="TotalNumberBox" />
                    <asp:TextBox ID="TotalNumberBox" CssClass="total-number-box"  runat="server" />
                </div>
                <div class="male short">
                    <asp:Label ID="Male" CssClass="male-label"  runat="server" Text="Male" AssociateControlId="MaleBox" />
                    <asp:TextBox ID="MaleBox" CssClass="male-box"  runat="server" />
                </div>
                <div class="female short">
                    <asp:Label ID="Female" CssClass="female-label"  runat="server" Text="Female" AssociateControlId="FemaleBox" />
                    <asp:TextBox ID="FemaleBox" CssClass="female-box"  runat="server" />
                </div>
                <div class="instructions full">
                    <asp:Label ID="Instructions" CssClass="instructions-label"  runat="server" Text="Email Address" AssociateControlId="InstructionsBox" />
                    <asp:TextBox ID="InstructionsBox" CssClass="instructions-box"  runat="server" />
                </div>
            </div>
        </fieldset>
    </fieldset>
</div>
<engage:ValidationSummary ID="ValidationSummary1" runat="server" />
<div class="AddEventFooterButtons AdminButtons FooterButtons">
    <asp:ImageButton ID="SaveEventButton" runat="server" CssClass="Normal" ImageUrl="~/DesktopModules/EngageBooking/Images/save.gif" />
    <asp:HyperLink ID="CancelEventLink" runat="server" CssClass="Normal" ImageUrl="~/DesktopModules/EngageBooking/Images/cancel_go_home.gif" ResourceKey="Cancel.Alt" />
    <asp:ImageButton ID="SaveAndCreateNewEventButton" runat="server" CssClass="Normal" ImageUrl="~/DesktopModules/EngageBooking/Images/save_create_new.gif"/>
</div>

<script type="text/ecmascript">
    function StartDateTimePicker_DateSelected(sender, eventArgs) {
        var EndDateTimePicker = $find("<%= EndDateTimePicker.ClientID %>");
        // don't update end date if there's already an end date but not an old start date
        if (EndDateTimePicker.isEmpty() || eventArgs.get_oldDate() || EndDateTimePicker.get_selectedDate() <= eventArgs.get_newDate()) {
            var selectedDateSpan = 1800000; // 30 minutes
            if (!EndDateTimePicker.isEmpty() && EndDateTimePicker.get_selectedDate() > eventArgs.get_newDate()) {
                selectedDateSpan = EndDateTimePicker.get_selectedDate() - eventArgs.get_oldDate();
            }

            EndDateTimePicker.set_selectedDate(new Date(eventArgs.get_newDate().getTime() + selectedDateSpan));
        }
    }
</script>

