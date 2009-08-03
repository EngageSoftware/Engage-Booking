<%@ Control Language="c#" AutoEventWireup="false" Inherits="Engage.Dnn.Booking.AppointmentRequest" CodeBehind="AppointmentRequest.ascx.cs" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="engage" TagName="ModuleMessage" Src="../Controls/ModuleMessage.ascx" %>
<%@ Register TagPrefix="engage" Namespace="Engage.Controls" Assembly="Engage.Framework" %>
<engage:ModuleMessage runat="server" ID="SuccessModuleMessage" MessageType="Success" TextResourceKey="AddEventSuccess" CssClass="RequestAppointmentSuccessMessage" />
<div class="approval-request Normal">
    <fieldset>
        <legend><%=Localize("Service Request Form")%></legend>
        <p class="note">
            <%=Localize("RequiredLabelStart.Text") %><asp:Label runat="server" ResourceKey="Required Label" CssClass="required-label" /><%=Localize("RequiredLabelEnd.Text") %>
        </p>
        <fieldset>
            <legend><%=Localize("Interpreting Event")%></legend>
            <ol class="interpreting-form">
                <li class="event-type">
                    <asp:Label runat="server" ResourceKey="Required Label" CssClass="required-label" />
                    <asp:Label CssClass="event-type-label" ResourceKey="EventTypeLabel" runat="server" AssociatedControlId="AppointmentTypeDropDownList" />
                    <asp:DropDownList ID="AppointmentTypeDropDownList" CssClass="event-type-box short" runat="server" />
                    
                    <asp:Label runat="server" ResourceKey="Required Label" CssClass="required-label" /><asp:Label CssClass="title-label" runat="server" ResourceKey="TitleLabel" AssociatedControlId="TitleTextBox" />
                    <asp:TextBox ID="TitleTextBox" CssClass="title-box short" runat="server" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="TitleTextBox" ResourceKey="TitleRequiredValidator" Display="None" EnableClientScript="false" />
                </li>

                <li class="description">
                    <asp:Label CssClass="description-label" runat="server" ResourceKey="DescriptionLabel" AssociatedControlId="DescriptionTextBox" />
                    <asp:TextBox ID="DescriptionTextBox" CssClass="description-box full" runat="server" TextMode="MultiLine" Rows="4" Columns="40" />
                </li>
                <li class="notes">
                    <asp:Label CssClass="considerations-label" runat="server" ResourceKey="ConsiderationsLabel" AssociatedControlId="NotesTextBox" />
                    <asp:TextBox ID="NotesTextBox" CssClass="notes-box full" runat="server" TextMode="MultiLine" Rows="4" Columns="40" />
                </li>
            </ol>
            
            <h4><asp:Label ID="Label1" ResourceKey="AddressLabel" runat="server" /></h4>
            
            <ol class="interpreting-form-b">
                <li class="street">
                    <asp:Label CssClass="street-label" runat="server" ResourceKey="StreetLabel" AssociatedControlId="StreetTextBox" />
                    <asp:TextBox ID="StreetTextBox" CssClass="street-box long" runat="server" />
                    <asp:TextBox ID="RoomTextBox" CssClass="room-box small inline-label inline-label-gray" runat="server" />
                </li>
                <li class="city">
                    <asp:Label CssClass="city-label" runat="server" ResourceKey="CityLabel" AssociatedControlId="CityTextBox" />
                    <asp:TextBox ID="CityTextBox" CssClass="city-box long" runat="server" />
                    <asp:DropDownList ID="RegionDropDownList" runat="server" CssClass="state-list short inline-label inline-label-gray"></asp:DropDownList>
                    <asp:TextBox ID="PostalCodeTextBox" CssClass="zip-box small inline-label inline-label-gray" runat="server" />
                </li>
                <li class="nearest">
                    <asp:Label CssClass="nearest-label" runat="server" ResourceKey="NearestLabel" AssociatedControlId="AdditionalAddressInfoTextBox" />
                    <asp:TextBox ID="AdditionalAddressInfoTextBox" CssClass="nearest-box long" runat="server" />
                </li>
            </ol>
            
            <h4><asp:Label ResourceKey="OnSiteContactLabel" runat="server" /></h4>
            
            <ol class="interpreting-form-c">
                <li class="street">
                    <asp:Label CssClass="street-label" runat="server" ResourceKey="StreetLabel" AssociatedControlId="OnsiteStreetTextBox" />
                    <asp:TextBox ID="OnsiteStreetTextBox" CssClass="street-box long" runat="server" />
                </li>
                <li class="phone">
                    <asp:Label CssClass="phone-label" runat="server" ResourceKey="PhoneLabel" AssociatedControlId="OnsitePhoneTextBox" />
                    <asp:TextBox ID="OnsitePhoneTextBox" CssClass="phone-box long" runat="server" />
                </li>
            </ol>
        </fieldset>
        <fieldset>
            <legend><asp:Label ResourceKey="InterpretingServicesLabel.Text" runat="server" /></legend>
            <ol class="requesting-form">
                <li class="full-name">
                    <asp:Label runat="server" ResourceKey="Required Label" CssClass="required-label" /><asp:Label ID="RequestorNameLabel" CssClass="full-name-label" runat="server" ResourceKey="FullNameLabel.Text" AssociatedControlId="RequestorNameTextBox" />
                    <asp:TextBox ID="RequestorNameTextBox" CssClass="full-name-box long" runat="server" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="RequestorNameTextBox" ResourceKey="FullNameRequiredValidator" Display="None" EnableClientScript="false" />
                </li>
                <li class="phone">
                    <asp:DropDownList ID="RequestorPhoneTypeDropDownList" runat="server"/>
                    <asp:Label runat="server" ResourceKey="Required Label" CssClass="required-label" /><asp:Label CssClass="phone-label" runat="server" ResourceKey="RequestorPhoneLabel.Text" AssociatedControlId="RequestorPhoneTextBox" />
                    <asp:TextBox ID="RequestorPhoneTextBox" CssClass="phone-box long" runat="server" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="RequestorPhoneTextBox" ResourceKey="RequestorPhoneRequiredValidator" Display="None" EnableClientScript="false" />
                </li>
                <li class="alt-phone">
                    <asp:DropDownList ID="RequestorAltPhoneTypeDropDownList" runat="server"/>
                    <asp:Label CssClass="alt-phone-label" runat="server" ResourceKey="AltPhoneLabel" AssociatedControlId="RequestorAltPhoneTextBox" />
                    <asp:TextBox ID="RequestorAltPhoneTextBox" CssClass="alt-phone-box long" runat="server" />
                </li>
                <li class="email">
                    <asp:Label runat="server" ResourceKey="Required Label" CssClass="required-label" /><asp:Label CssClass="email-label" runat="server" ResourceKey="EmailAddressLabel" AssociatedControlId="RequestorEmailTextBox" />
                    <asp:TextBox ID="RequestorEmailTextBox" CssClass="email-box full" runat="server" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="RequestorEmailTextBox" ResourceKey="RequestorEmailRequiredValidator" Display="None" EnableClientScript="false" />
                    <asp:RegularExpressionValidator ID="RequestorEmailFormatValidator" runat="server" ControlToValidate="RequestorEmailTextBox" ResourceKey="RequestorEmailFormatValidator" Display="None" EnableClientScript="false" />
                </li>
            </ol>
        </fieldset>
        <fieldset>
            <legend><asp:Label ResourceKey="AssignmentDateLabel.Text" runat="server" /></legend>
            <ol class="date-assignment-form">
                <li class="start">
                    <asp:Label runat="server" ResourceKey="Required Label" CssClass="required-label" /><asp:Label CssClass="start-label" runat="server" ResourceKey="StartLabel" AssociatedControlId="StartDateTimePicker" />
                    <telerik:RadDateTimePicker runat="server" ID="StartDateTimePicker">
                        <Calendar ShowRowHeaders="false" />
                        <DateInput InvalidStyleDuration="100" />
                        <ClientEvents OnDateSelected="StartDateTimePicker_DateSelected" />
                    </telerik:RadDateTimePicker>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="StartDateTimePicker" ResourceKey="StartDateTimePickerRequired" Display="None" EnableClientScript="false" />
                </li>
                <li class="end">
                    <asp:Label runat="server" ResourceKey="Required Label" CssClass="required-label" /><asp:Label CssClass="end-label" runat="server" ResourceKey="EndLabel" AssociatedControlId="EndDateTimePicker" />
                    <telerik:RadDateTimePicker runat="server" ID="EndDateTimePicker">
                        <Calendar ShowRowHeaders="false" />
                        <DateInput InvalidStyleDuration="100" />
                    </telerik:RadDateTimePicker>
                    <asp:CompareValidator runat="server" Display="None" EnableClientScript="false" ControlToCompare="StartDateTimePicker" ControlToValidate="EndDateTimePicker" ResourceKey="EndDateCompareValidator" Operator="GreaterThan" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="EndDateTimePicker" ResourceKey="EndDateTimePickerRequired" Display="None" EnableClientScript="false" />
                </li>
                <li class="timezone">
                    <asp:Label runat="server" ResourceKey="EventTimeZoneLabel" CssClass="timezone-label" AssociatedControlID="TimeZoneDropDownList" />
                    <div class="timezone_display">
                        <asp:DropDownList runat="server" ID="TimeZoneDropDownList" CssClass="timezone-dropdown full" />
                        <div class="Normal">
                            <asp:CheckBox runat="server" ID="InDaylightTimeCheckBox" CssClass="timezone-checkbox" ResourceKey="EventTimeZoneCheckBox" />
                        </div>
                    </div>
                </li>
            </ol>
        </fieldset>
        <fieldset>
            <legend><asp:Label ResourceKey="ParticipantsLabel.Text" runat="server" /></legend>
            <ol class="participants-form">
                <li class="number-special">
                    <asp:Label CssClass="number-special-label" runat="server" ResourceKey="NumberSpecialLabel" AssociatedControlId="NumberOfSpecialParticipantsTextBox" />
                    <asp:TextBox ID="NumberOfSpecialParticipantsTextBox" CssClass="number-special-box short" runat="server" />
                    <asp:CompareValidator runat="server" ControlToValidate="NumberOfSpecialParticipantsTextBox" ResourceKey="NumberOfSpecialParticipantsFormatValidator" Operator="DataTypeCheck" Type="Integer" Display="None" EnableClientScript="false" />
                    
                    <asp:Label CssClass="total-number-label" runat="server" ResourceKey="TotalNumberLabel" AssociatedControlId="TotalNumberParticipantsTextBox" />
                    <asp:TextBox ID="TotalNumberParticipantsTextBox" CssClass="total-number-box short" runat="server" />
                    <asp:CompareValidator runat="server" ControlToValidate="TotalNumberParticipantsTextBox" ResourceKey="TotalNumberOfParticipantsFormatValidator" Operator="DataTypeCheck" Type="Integer" Display="None" EnableClientScript="false" />
                    <asp:CompareValidator runat="server" ControlToValidate="TotalNumberParticipantsTextBox" ControlToCompare="NumberOfSpecialParticipantsTextBox" ResourceKey="TotalNumberOfParticipantsCompareValidator.Text" Operator="GreaterThanEqual" Display="None" EnableClientScript="false" />
                </li>
                <li class="gender">
                    <asp:Label CssClass="gender-label" runat="server" ResourceKey="GenderLabel" AssociatedControlId="GenderDropDownList" />
                    <asp:DropDownList ID="GenderDropDownList" runat="server" CssClass="gender-list short" runat="server"/>
                    <asp:Label CssClass="presenter-label" runat="server" ResourceKey="PresenterLabel" AssociatedControlId="PresenterDropDownList" />
                    <asp:DropDownList ID="PresenterDropDownList" CssClass="presenter-box short" runat="server"/>
                </li>
                <li class="instructions">
                    <asp:Label CssClass="instructions-label" runat="server" ResourceKey="InstructionsLabel" AssociatedControlId="InstructionsTextBox" />
                    <asp:TextBox ID="InstructionsTextBox" CssClass="instructions-box full" runat="server" TextMode="MultiLine" Rows="4" Columns="40" />
                </li>
            </ol>
        </fieldset>
        <fieldset class="customfields-fieldset">
            <legend><asp:Label ResourceKey="CustomFieldsLabel.Text" runat="server" /></legend>
            <ol class="customfields-form">
                <li class="customfield1">
                    <asp:Label CssClass="customfield1-label" runat="server" ResourceKey="CustomField1Label.Text" AssociatedControlId="CustomField1TextBox" />
                    <asp:TextBox ID="CustomField1TextBox" CssClass="customfield1-box full" runat="server" />
                </li>
                <li class="customfield2">
                    <asp:Label CssClass="customfield2-label" runat="server" ResourceKey="CustomField2Label.Text" AssociatedControlId="CustomField2TextBox" />
                    <asp:TextBox ID="CustomField2TextBox" CssClass="customfield2-box full" runat="server" />
                </li>
                <li class="customfield3">
                    <asp:Label CssClass="customfield3-label" runat="server" ResourceKey="CustomField3Label.Text" AssociatedControlId="CustomField3TextBox" />
                    <asp:TextBox ID="CustomField3TextBox" CssClass="customfield3-box full" runat="server" />
                </li>
                <li class="customfield4">
                    <asp:Label CssClass="customfield4-label" runat="server" ResourceKey="CustomField4Label.Text" AssociatedControlId="CustomField4TextBox" />
                    <asp:TextBox ID="CustomField4TextBox" CssClass="customfield4-box full" runat="server" />
                </li>
                <li class="customfield5">
                    <asp:Label CssClass="customfield5-label" runat="server" ResourceKey="CustomField5Label.Text" AssociatedControlId="CustomField5TextBox" />
                    <asp:TextBox ID="CustomField5TextBox" CssClass="customfield5-box full" runat="server" />
                </li>
                <li class="customfield6">
                    <asp:Label CssClass="customfield6-label" runat="server" ResourceKey="CustomField6Label.Text" AssociatedControlId="CustomField6TextBox" />
                    <asp:TextBox ID="CustomField6TextBox" CssClass="customfield6-box full" runat="server" />
                </li>
                <li class="customfield7">
                    <asp:Label CssClass="customfield7-label" runat="server" ResourceKey="CustomField7Label.Text" AssociatedControlId="CustomField7TextBox" />
                    <asp:TextBox ID="CustomField7TextBox" CssClass="customfield7-box full" runat="server" />
                </li>
                <li class="customfield8">
                    <asp:Label CssClass="customfield8-label" runat="server" ResourceKey="CustomField8Label.Text" AssociatedControlId="CustomField8TextBox" />
                    <asp:TextBox ID="CustomField8TextBox" CssClass="customfield8-box full" runat="server" />
                </li>
                <li class="customfield9">
                    <asp:Label CssClass="customfield9-label" runat="server" ResourceKey="CustomField9Label.Text" AssociatedControlId="CustomField9TextBox" />
                    <asp:TextBox ID="CustomField9TextBox" CssClass="customfield9-box full" runat="server" />
                </li>
                <li class="customfield10">
                    <asp:Label CssClass="customfield10-label" runat="server" ResourceKey="CustomField10Label.Text" AssociatedControlId="CustomField10TextBox" />
                    <asp:TextBox ID="CustomField10TextBox" CssClass="customfield10-box full" runat="server" />
                </li>
            </ol>
        </fieldset>
    </fieldset>
</div>
<engage:ValidationSummary runat="server" CssClass="NormalRed" />
<asp:CustomValidator ID="UniqueTimeslotValidator" runat="server" Display="None" ResourceKey="UniqueTimeslotValidator.Text" />
<div class="AdminButtons FooterButtons">
    <asp:ImageButton ID="SaveAppointmentButton" runat="server" CssClass="Normal" ImageUrl="~/DesktopModules/EngageBooking/Images/save.gif" />
    <asp:HyperLink ID="CancelAppointmentLink" runat="server" CssClass="Normal" ImageUrl="~/DesktopModules/EngageBooking/Images/cancel_go_home.gif" />
    <asp:ImageButton ID="SaveAndCreateNewAppointmentButton" runat="server" CssClass="Normal" ImageUrl="~/DesktopModules/EngageBooking/Images/save_create_new.gif" />
</div>

<script type="text/javascript">
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

    (function($) {
        $(function() {
            $('.inline-label').each(function() {
                var $this = $(this);
                $this.data('original-text', $this.val());
            }).focus(function() {
                var $this = $(this);
                if ($this.val() === $this.data('original-text')) {
                    $this.val('')
                    $this.removeClass('inline-label-gray');
                }
            }).blur(function() {
                var $this = $(this);
                if (!$this.val() || $this.val() === $this.data('original-text')) {
                    $this.val($this.data('original-text'));
                    $this.addClass('inline-label-gray');
                }
            });
        });
    })(jQuery);
</script>