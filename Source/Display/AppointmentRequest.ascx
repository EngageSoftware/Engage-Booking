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
            <ol class="interpreting-form">
                <li class="event-type">
                    <asp:Label ID="AppointmentType" CssClass="event-type-label" runat="server" Text="Type of Event" AssociatedControlId="AppointmentTypeDropDownList" />
                    <asp:DropDownList ID="AppointmentTypeDropDownList" CssClass="event-type-box medium" runat="server" />
                </li>
                <li class="title">
                    <asp:Label ID="TitleLabel" CssClass="title-label" runat="server" Text="Title" AssociatedControlId="TitleTextBox" />
                    <asp:TextBox ID="TitleTextBox" CssClass="title-box long" runat="server" />
                </li>
                <li class="description">
                    <asp:Label ID="DescriptionLabel" CssClass="description-label" runat="server" Text="Description of Event" AssociatedControlId="DescriptionTextBox" />
                    <asp:TextBox ID="DescriptionTextBox" CssClass="description-box full" runat="server" TextMode="MultiLine" Rows="4" Columns="40" />
                </li>
                <li class="notes">
                    <asp:Label ID="NotesLabel" CssClass="considerations-label" runat="server" Text="Special Considerations" AssociatedControlId="NotesTextBox" />
                    <asp:TextBox ID="NotesTextBox" CssClass="notes-box full" runat="server" TextMode="MultiLine" Rows="4" Columns="40" />
                </li>
            </ol>
            <h4>Address</h4>
            <ol class="interpreting-form-b">
                <li class="street">
                    <asp:Label ID="StreetLabel" CssClass="street-label" runat="server" Text="Street" AssociatedControlId="StreetTextBox" />
                    <asp:TextBox ID="StreetTextBox" CssClass="street-box long" runat="server" />
                    <asp:TextBox ID="RoomTextBox" CssClass="room-box short" runat="server" />
                </li>
               
                <li class="city">
                    <asp:Label ID="CityLabel" CssClass="city-label" runat="server" Text="City" AssociatedControlId="CityTextBox" />
                    <asp:TextBox ID="CityTextBox" CssClass="city-box long" runat="server" />
                </li>
                <li class="state">
                    <asp:Label ID="StateLabel" CssClass="state-label" runat="server" />
                    <asp:DropDownList ID="RegionDropDownList" runat="server" CssClass="state-list medium"></asp:DropDownList>
                </li>
                <li class="zip">
                    <asp:TextBox ID="ZipTextBox" CssClass="zip-box short" runat="server" />
                </li>
                <li class="nearest">
                    <asp:Label ID="AdditionalAddressInfoLabel" CssClass="nearest-label" runat="server" Text="If DC/MD/VA, nearest Metro and nearest cross street" AssociatedControlId="AdditionalAddressInfoTextBox" />
                    <asp:TextBox ID="AdditionalAddressInfoTextBox" CssClass="nearest-box long" runat="server" />
                </li>
            </ol>
            <h4>On-site Contact</h4>
            <ol class="interpreting-form-c">
                <li class="street">
                    <asp:Label ID="OnsiteStreetLabel" CssClass="street-label" runat="server" Text="Street" AssociatedControlId="OnsiteStreetTextBox" />
                    <asp:TextBox ID="OnsiteStreetTextBox" CssClass="street-box long" runat="server" />
                </li>
                <li class="phone">
                    <asp:Label ID="OnsitePhoneLabel" CssClass="phone-label" runat="server" Text="Phone" AssociatedControlId="OnsitePhoneTextBox" />
                    <asp:TextBox ID="OnsitePhoneTextBox" CssClass="phone-box medium" runat="server" />
                </li>
            </ol>
        </fieldset>
        <fieldset>
            <legend>Person Requesting Interpreting Services</legend>
            <ol class="requesting-form">
                <li class="full-name">
                    <asp:Label ID="RequestorNameLabel" CssClass="full-name-label" runat="server" Text="Full Name" AssociatedControlId="RequestorNameTextBox" />
                    <asp:TextBox ID="RequestorNameTextBox" CssClass="full-name-box long" runat="server" />
                </li>
                <li class="phone">
                    <asp:DropDownList ID="RequestorPhoneTypeDropDownList" runat="server">
                        <asp:ListItem>Voice</asp:ListItem>
                        <asp:ListItem>TTY</asp:ListItem>
                        <asp:ListItem>Fax</asp:ListItem>
                        <asp:ListItem>WebCam</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="RequestorPhoneLabel" CssClass="phone-label" runat="server" Text="Phone" AssociatedControlId="RequestorPhoneTextBox" />
                    <asp:TextBox ID="RequestorPhoneTextBox" CssClass="phone-box long" runat="server" />
                </li>
                <li class="alt-phone">
                    <asp:DropDownList ID="RequestorAltPhoneTypeDropDownList" runat="server">
                        <asp:ListItem>Voice</asp:ListItem>
                        <asp:ListItem>TTY</asp:ListItem>
                        <asp:ListItem>Fax</asp:ListItem>
                        <asp:ListItem>WebCam</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="RequestorAltPhoneLabel" CssClass="alt-phone-label" runat="server" Text="Alternate Phone" AssociatedControlId="RequestorAltPhoneTextBox" />
                    <asp:TextBox ID="RequestorAltPhoneTextBox" CssClass="alt-phone-box" runat="server" />
                </li>
                <li class="email">
                    <asp:Label ID="RequestorEmailLabel" CssClass="email-label" runat="server" Text="Email Address" AssociatedControlId="RequestorEmailTextBox" />
                    <asp:TextBox ID="RequestorEmailTextBox" CssClass="email-box long" runat="server" />
                </li>
            </ol>
        </fieldset>
        <fieldset>
            <legend>Date of Assignment</legend>
            <ol class="date-assignment-form">
                <li class="start">
                    <asp:Label ID="Start" CssClass="start-label" runat="server" Text="Start" AssociatedControlId="StartBox" />
                    <asp:TextBox ID="StartBox" CssClass="start-box medium" runat="server" />
                    <telerik:RadDateTimePicker runat="server" ID="StartDateTimePicker" Skin="WebBlue">
                        <TimeView Skin="WebBlue" />
                        <Calendar Skin="WebBlue" ShowRowHeaders="false" />
                        <DateInput InvalidStyleDuration="100" />
                        <ClientEvents OnDateSelected="StartDateTimePicker_DateSelected" />
                    </telerik:RadDateTimePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="StartDateTimePicker" ResourceKey="StartDateTimePickerRequired" Display="None" EnableClientScript="false" />
                </li>
                <li class="end">
                    <asp:Label ID="End" CssClass="end-label" runat="server" Text="End:" AssociatedControlId="EndBox" />
                    <asp:TextBox ID="EndBox" CssClass="end-box medium" runat="server" />
                    <telerik:RadDateTimePicker runat="server" ID="EndDateTimePicker" Skin="WebBlue">
                        <TimeView Skin="WebBlue" />
                        <Calendar Skin="WebBlue" ShowRowHeaders="false" />
                        <DateInput InvalidStyleDuration="100" />
                    </telerik:RadDateTimePicker>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" Display="None" EnableClientScript="false" ControlToCompare="StartDateTimePicker" ControlToValidate="EndDateTimePicker" ResourceKey="EndDateCompareValidator" Operator="GreaterThan" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="EndDateTimePicker" ResourceKey="EndDateTimePickerRequired" Display="None" EnableClientScript="false" />
                </li>
                <li class="AEEventTimeZone">
                    <asp:Label ID="Label1" runat="server" ResourceKey="EventTimeZoneLabel" CssClass="NormalBold" AssociatedControlID="TimeZoneDropDownList" />
                    <div class="timezone_display">
                        <asp:DropDownList runat="server" ID="TimeZoneDropDownList" />
                        <div class="Normal">
                            <asp:CheckBox runat="server" ID="InDaylightTimeCheckBox" ResourceKey="EventTimeZoneCheckBox" />
                        </div>
                    </div>
                </li>
            </ol>
        </fieldset>
        <fieldset>
            <legend>Participants</legend>
            <ol class="participants-form">
                <li class="number-special">
                    <asp:Label ID="NumberOfSpecialParticipantsLabel" CssClass="number-special-label" runat="server" Text="Number of Deaf Participants" AssociatedControlId="NumberOfSpecialParticipantsTextBox" />
                    <asp:TextBox ID="NumberOfSpecialParticipantsTextBox" CssClass="number-special-box short" runat="server" />
                </li>
                <li class="total-number">
                    <asp:Label ID="TotalNumberParticipantsLabel" CssClass="total-number-label" runat="server" Text="Total Number of Participants" AssociatedControlId="TotalNumberParticipantsTextBox" />
                    <asp:TextBox ID="TotalNumberParticipantsTextBox" CssClass="total-number-box short" runat="server" />
                </li>
                <li class="gender">
                    <asp:Label ID="GenderLabel" CssClass="gender-label" runat="server" Text="Gender" AssociatedControlId="GenderDropDownList" />
                    <asp:DropDownList ID="GenderDropDownList" runat="server" CssClass="gender-list short" runat="server">
                        <asp:ListItem>Mixed Group</asp:ListItem>
                        <asp:ListItem>Men</asp:ListItem>
                        <asp:ListItem>Women</asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li class="presenter">
                    <asp:Label ID="PresenterLabel" CssClass="presenter-label" runat="server" Text="Deaf Presenter" AssociatedControlId="PresenterDropDownList" />
                    <asp:DropDownList ID="PresenterDropDownList" CssClass="female-box short" runat="server">
                        <asp:ListItem>N</asp:ListItem>
                        <asp:ListItem>Y</asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li class="instructions">
                    <asp:Label ID="InstructionsLabel" CssClass="instructions-label" runat="server" Text="Special Instructions" AssociatedControlId="InstructionsTextBox" />
                    <asp:TextBox ID="InstructionsTextBox" CssClass="instructions-box full" runat="server" />
                </li>
            </ol>
        </fieldset>
    </fieldset>
</div>
<engage:ValidationSummary ID="ValidationSummary1" runat="server" />
<div class="AdminButtons FooterButtons">
    <asp:ImageButton ID="SaveAppointmentButton" runat="server" CssClass="Normal" ImageUrl="~/DesktopModules/EngageBooking/Images/save.gif" />
    <asp:HyperLink ID="CancelAppointmentLink" runat="server" CssClass="Normal" ImageUrl="~/DesktopModules/EngageBooking/Images/cancel_go_home.gif" />
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

