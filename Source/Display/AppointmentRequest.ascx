<%@ Control Language="c#" AutoEventWireup="false" Inherits="Engage.Dnn.Booking.AppointmentRequest" CodeBehind="AppointmentRequest.ascx.cs" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
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
                    <asp:Label ID="AppointmentType" CssClass="event-type-label" runat="server" Text="Type of Event" AssociateControlId="EventType" />
                    <asp:DropDownList ID="AppointmentTypeDropDownList" CssClass="event-type-box" runat="server" />
                </div>
                <div class="Title long">
                    <asp:Label ID="TitleLabel" CssClass="title-label" runat="server" Text="Title" AssociateControlId="TitleTextBox" />
                    <asp:TextBox ID="TitleTextBox" CssClass="title-box" runat="server" />
                </div>
                <div class="description full">
                    <asp:Label ID="DescriptionLabel" CssClass="description-label" runat="server" Text="Description of Event" AssociateControlId="DescriptionTextBox" />
                    <asp:TextBox ID="DescriptionTextBox" CssClass="description-box" runat="server" />
                </div>
                <div class="notes full">
                    <asp:Label ID="NotesLabel" CssClass="considerations-label" runat="server" Text="Special Considerations" AssociateControlId="NotesTextBox" />
                    <asp:TextBox ID="NotesTextBox" CssClass="notes-box" runat="server" />
                </div>
            </div>
            <h4>
                Address</h4>
            <div class="interpreting-form-b">
                <div class="street long">
                    <asp:Label ID="StreetLabel" CssClass="street-label" runat="server" Text="Street" AssociateControlId="StreetTextBox" />
                    <asp:TextBox ID="StreetTextBox" CssClass="street-box" runat="server" />
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
                    <asp:DropDownList ID="StateDropDownList" runat="server" CssClass="state-list">
                    </asp:DropDownList>
                </div>
                <div class="zip short">
                    <asp:TextBox ID="ZipTextBox" CssClass="zip-box" runat="server" />
                </div>
                <div class="nearest long">
                    <asp:Label ID="AdditionaAddressInfoLabel" CssClass="nearest-label" runat="server" Text="If DC/MD/VA, nearest Metro and nearest cross street" AssociateControlId="AdditionaAddressInfoTextBox" />
                    <asp:TextBox ID="AdditionaAddressInfoTextBox" CssClass="nearest-box" runat="server" />
                </div>
            </div>
            <h4>
                On-site Contact</h4>
            <div class="interpreting-form-c">
                <div class="street long">
                    <asp:Label ID="OnSiteStreetLabel" CssClass="street-label" runat="server" Text="Street" AssociateControlId="StreetBox" />
                    <asp:TextBox ID="OnSiteStreetTextBox" CssClass="street-box" runat="server" />
                </div>
                <div class="phone">
                    <asp:Label ID="OnSitePhoneLabel" CssClass="phone-label" runat="server" Text="Phone" AssociateControlId="OnSitePhoneTextBox" />
                    <asp:TextBox ID="OnSitePhoneTextBox" CssClass="phone-box" runat="server" />
                </div>
            </div>
        </fieldset>
        <fieldset>
            <legend>Person Requesting Interpreting Services</legend>
            <div class="requesting-form">
                <div class="full-name long">
                    <asp:Label ID="RequestorNameLabel" CssClass="full-name-label" runat="server" Text="Full Name" AssociateControlId="RequestorNameTextBox" />
                    <asp:TextBox ID="RequestorNameTextBox" CssClass="full-name-box" runat="server" />
                </div>
                <div class="phone">
                    <asp:DropDownList ID="RequestorPhoneTypeDropDownList" runat="server">
                        <asp:ListItem>Voice</asp:ListItem>
                        <asp:ListItem>TTY</asp:ListItem>
                        <asp:ListItem>Fax</asp:ListItem>
                        <asp:ListItem>WebCam</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="RequestorPhoneLabel" CssClass="phone-label" runat="server" Text="Phone" AssociateControlId="RequestorPhoneTextBox" />
                    <asp:TextBox ID="RequestorPhoneTextBox" CssClass="phone-box" runat="server" />
                </div>
                <div class="alt-phone">
                    <asp:DropDownList ID="RequestorAltPhoneTypeDropDownList" runat="server">
                        <asp:ListItem>Voice</asp:ListItem>
                        <asp:ListItem>TTY</asp:ListItem>
                        <asp:ListItem>Fax</asp:ListItem>
                        <asp:ListItem>WebCam</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="RequestorAltPhoneLabel" CssClass="alt-phone-label" runat="server" Text="Alternate Phone" AssociateControlId="RequestorAltPhoneTextBox" />
                    <asp:TextBox ID="RequestorAltPhoneTextBox" CssClass="alt-phone-box" runat="server" />
                </div>
                <div class="email long">
                    <asp:Label ID="RequestorEmailLabel" CssClass="email-label" runat="server" Text="Email Address" AssociateControlId="RequestorEmailTextBox" />
                    <asp:TextBox ID="RequestorEmailTextBox" CssClass="email-box" runat="server" />
                </div>
            </div>
        </fieldset>
        <fieldset>
            <legend>Date of Assignment</legend>
            <div class="date-assignment-form">
                <div class="start medium">
                    <asp:Label ID="Start" CssClass="start-label" runat="server" Text="Start" AssociateControlId="StartBox" />
                    <asp:TextBox ID="StartBox" CssClass="start-box" runat="server" />
                    <telerik:RadDateTimePicker runat="server" ID="StartDateTimePicker" Skin="WebBlue">
                        <TimeView Skin="WebBlue" />
                        <Calendar Skin="WebBlue" ShowRowHeaders="false" />
                        <DateInput InvalidStyleDuration="100" />
                        <ClientEvents OnDateSelected="StartDateTimePicker_DateSelected" />
                    </telerik:RadDateTimePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="StartDateTimePicker" ResourceKey="StartDateTimePickerRequired" Display="None" EnableClientScript="false" />
                </div>
                <div class="end medium">
                    <asp:Label ID="End" CssClass="end-label" runat="server" Text="End:" AssociateControlId="EndBox" />
                    <asp:TextBox ID="EndBox" CssClass="end-box" runat="server" />
                    <telerik:RadDateTimePicker runat="server" ID="EndDateTimePicker" Skin="WebBlue">
                        <TimeView Skin="WebBlue" />
                        <Calendar Skin="WebBlue" ShowRowHeaders="false" />
                        <DateInput InvalidStyleDuration="100" />
                    </telerik:RadDateTimePicker>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" Display="None" EnableClientScript="false" ControlToCompare="StartDateTimePicker" ControlToValidate="EndDateTimePicker" ResourceKey="EndDateCompareValidator" Operator="GreaterThan" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="EndDateTimePicker" ResourceKey="EndDateTimePickerRequired" Display="None" EnableClientScript="false" />
                </div>
                <div class="AEEventTimeZone">
                    <asp:Label ID="Label1" runat="server" ResourceKey="EventTimeZoneLabel" CssClass="NormalBold" AssociatedControlID="TimeZoneDropDownList" />
                    <div class="timezone_display">
                        <asp:DropDownList runat="server" ID="TimeZoneDropDownList" />
                        <div class="Normal">
                            <asp:CheckBox runat="server" ID="InDaylightTimeCheckBox" ResourceKey="EventTimeZoneCheckBox" /></div>
                    </div>
                </div>
            </div>
        </fieldset>
        <fieldset>
            <legend>Participants</legend>
            <div class="participants-form">
                <div class="number-special short">
                    <asp:Label ID="NumberOfSpecialParticipantsLabel" CssClass="number-special-label" runat="server" Text="Number of Deaf Participants" AssociateControlId="NumberOfSpecialParticipantsTextBox" />
                    <asp:TextBox ID="NumberOfSpecialParticipantsTextBox" CssClass="number-special-box" runat="server" />
                </div>
                <div class="total-number short">
                    <asp:Label ID="TotalNumberParticipantsLabel" CssClass="total-number-label" runat="server" Text="Total Number of Participants" AssociateControlId="TotalNumberParticipantsTextBox" />
                    <asp:TextBox ID="TotalNumberParticipantsTextBox" CssClass="total-number-box" runat="server" />
                </div>
                <div class="gender short">
                    <asp:Label ID="GenderLabel" CssClass="gender-label" runat="server" Text="Gender" AssociateControlId="GenderDropDownList" />
                    <asp:DropDownList ID="GenderDropDownList" runat="server" CssClass="gender-list" runat="server">
                        <asp:ListItem>Mixed Group</asp:ListItem>
                        <asp:ListItem>Men</asp:ListItem>
                        <asp:ListItem>Women</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="presenter short">
                    <asp:Label ID="PresenterLabel" CssClass="presenter-label" runat="server" Text="Deaf Presenter" AssociateControlId="PresenterDropDownList" />
                    <asp:DropDownList ID="PresenterDropDownList" CssClass="female-box" runat="server">
                        <asp:ListItem>N</asp:ListItem>
                        <asp:ListItem>Y</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="instructions full">
                    <asp:Label ID="InstructionsLabel" CssClass="instructions-label" runat="server" Text="Special Instructions" AssociateControlId="InstructionsTextBox" />
                    <asp:TextBox ID="InstructionsTextBox" CssClass="instructions-box" runat="server" />
                </div>
            </div>
        </fieldset>
    </fieldset>
</div>
<engage:ValidationSummary ID="ValidationSummary1" runat="server" />
<div class="AdminButtons FooterButtons">
    <asp:ImageButton ID="SaveAppointmentButton" runat="server" CssClass="Normal" ImageUrl="~/DesktopModules/EngageBooking/Images/save.gif" />
    <asp:ImageButton ID="CancelAppointmentButton" runat="server" CssClass="Normal" ImageUrl="~/DesktopModules/EngageBooking/Images/cancel_go_home.gif" onclick="CancelAppointmentButton_Click" />
    <asp:ImageButton ID="SaveAndCreateNewAppointmentButton" runat="server" CssClass="Normal" ImageUrl="~/DesktopModules/EngageBooking/Images/save_create_new.gif" />
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

