<%@ Control Language="c#" AutoEventWireup="false" Inherits="Engage.Dnn.Booking.AppointmentDetails" CodeBehind="AppointmentDetails.ascx.cs" %>
<div class="appointment-details Normal">
    <fieldset>
        <legend><%=Localize("Service Request Form")%></legend>
        <fieldset>
            <legend><%=Localize("Interpreting Event")%></legend>
            <ol class="interpreting-form">
                <li class="event-type">
                    <asp:Label CssClass="event-type-label" ResourceKey="EventTypeLabel" runat="server" AssociatedControlId="AppointmentTypeLabel" />
                    <asp:Label ID="AppointmentTypeLabel" CssClass="event-type-box short" runat="server" />
                    
                    <asp:Label CssClass="title-label" runat="server" ResourceKey="TitleLabel" AssociatedControlId="TitleLabel" />
                    <asp:Label ID="TitleLabel" CssClass="title-box short" runat="server" />
                </li>

                <li class="description">
                    <asp:Label CssClass="description-label" runat="server" ResourceKey="DescriptionLabel" AssociatedControlId="DescriptionLabel" />
                    <asp:Label ID="DescriptionLabel" CssClass="description-box full" runat="server"/>
                </li>
                <li class="notes">
                    <asp:Label CssClass="considerations-label" runat="server" ResourceKey="ConsiderationsLabel" AssociatedControlId="NotesLabel" />
                    <asp:Label ID="NotesLabel" CssClass="notes-box full" runat="server"/>
                </li>
            </ol>
            
            <h4><asp:Label ResourceKey="AddressLabel" runat="server" /></h4>
            
            <ol class="interpreting-form-b">
                <li class="street">
                    <asp:Label CssClass="street-label" runat="server" ResourceKey="StreetLabel" AssociatedControlId="StreetLabel" />
                    <asp:Label ID="StreetLabel" CssClass="street-box long" runat="server" />
                    <asp:Label ID="RoomLabel" CssClass="room-box small" runat="server" />
                </li>
                <li class="city">
                    <asp:Label CssClass="city-label" runat="server" ResourceKey="CityLabel" AssociatedControlId="CityLabel" />
                    <asp:Label ID="CityLabel" CssClass="city-box long" runat="server" />
                    <asp:Label ID="RegionLabel" runat="server" CssClass="state-list short"/>
                    <asp:Label ID="PostalCodeLabel" CssClass="zip-box small" runat="server" />
                </li>
                <li class="nearest">
                    <asp:Label CssClass="nearest-label" runat="server" ResourceKey="NearestLabel" AssociatedControlId="AdditionalAddressInfoLabel" />
                    <asp:Label ID="AdditionalAddressInfoLabel" CssClass="nearest-box long" runat="server" />
                </li>
            </ol>
            
            <h4><asp:Label ResourceKey="OnSiteContactLabel" runat="server" /></h4>
            
            <ol class="interpreting-form-c">
                <li class="street">
                    <asp:Label CssClass="street-label" runat="server" ResourceKey="StreetLabel" AssociatedControlId="OnsiteStreetLabel" />
                    <asp:Label ID="OnsiteStreetLabel" CssClass="street-box long" runat="server" />
                </li>
                <li class="phone">
                    <asp:Label CssClass="phone-label" runat="server" ResourceKey="PhoneLabel" AssociatedControlId="OnsitePhoneLabel" />
                    <asp:Label ID="OnsitePhoneLabel" CssClass="phone-box long" runat="server" />
                </li>
            </ol>
        </fieldset>
        <fieldset>
            <legend><asp:Label ResourceKey="InterpretingServicesLabel.Text" runat="server" /></legend>
            <ol class="requesting-form">
                <li class="full-name">
                    <asp:Label CssClass="full-name-label" runat="server" ResourceKey="FullNameLabel.Text" AssociatedControlId="RequestorNameLabel" />
                    <asp:Label ID="RequestorNameLabel" CssClass="full-name-box long" runat="server" />
                </li>
                <li class="phone">
                    <asp:Label ID="RequestorPhoneTypeLabel" runat="server"/>
                    <asp:Label CssClass="phone-label" runat="server" ResourceKey="RequestorPhoneLabel.Text" AssociatedControlId="RequestorPhoneLabel" />
                    <asp:Label ID="RequestorPhoneLabel" CssClass="phone-box long" runat="server" />
                </li>
                <li class="alt-phone">
                    <asp:Label ID="RequestorAltPhoneTypeLabel" runat="server"/>
                    <asp:Label CssClass="alt-phone-label" runat="server" ResourceKey="AltPhoneLabel" AssociatedControlId="RequestorAltPhoneLabel" />
                    <asp:Label ID="RequestorAltPhoneLabel" CssClass="alt-phone-box long" runat="server" />
                </li>
                <li class="email">
                    <asp:Label CssClass="email-label" runat="server" ResourceKey="EmailAddressLabel" AssociatedControlId="RequestorEmailLabel" />
                    <asp:Label ID="RequestorEmailLabel" CssClass="email-box full" runat="server" />
                </li>
            </ol>
        </fieldset>
        <fieldset>
            <legend><asp:Label ResourceKey="AssignmentDateLabel.Text" runat="server" /></legend>
            <ol class="date-assignment-form">
                <li class="start">
                    <asp:Label CssClass="start-label" runat="server" ResourceKey="StartLabel" AssociatedControlId="StartDateTimeLabel" />
                    <asp:Label runat="server" ID="StartDateTimeLabel"/>
                </li>
                <li class="end">
                    <asp:Label CssClass="end-label" runat="server" ResourceKey="EndLabel" AssociatedControlId="EndDateTimeLabel" />
                    <asp:Label runat="server" ID="EndDateTimeLabel"/>
                </li>
            </ol>
        </fieldset>
        <fieldset>
            <legend><asp:Label ResourceKey="ParticipantsLabel.Text" runat="server" /></legend>
            <ol class="participants-form">
                <li class="number-special">
                    <asp:Label CssClass="number-special-label" runat="server" ResourceKey="NumberSpecialLabel" AssociatedControlId="NumberOfSpecialParticipantsLabel" />
                    <asp:Label ID="NumberOfSpecialParticipantsLabel" CssClass="number-special-box short" runat="server" />
                    
                    <asp:Label CssClass="total-number-label" runat="server" ResourceKey="TotalNumberLabel" AssociatedControlId="TotalNumberParticipantsLabel" />
                    <asp:Label ID="TotalNumberParticipantsLabel" CssClass="total-number-box short" runat="server" />
                </li>
                <li class="gender">
                    <asp:Label CssClass="gender-label" runat="server" ResourceKey="GenderLabel" AssociatedControlId="GenderLabel" />
                    <asp:Label ID="GenderLabel" runat="server" CssClass="gender-list short" runat="server"/>
                    <asp:Label CssClass="presenter-label" runat="server" ResourceKey="PresenterLabel" AssociatedControlId="PresenterLabel" />
                    <asp:Label ID="PresenterLabel" CssClass="presenter-box short" runat="server"/>
                </li>
                <li class="instructions">
                    <asp:Label CssClass="instructions-label" runat="server" ResourceKey="InstructionsLabel" AssociatedControlId="InstructionsLabel" />
                    <asp:Label ID="InstructionsLabel" CssClass="instructions-box full" runat="server"/>
                </li>
            </ol>
        </fieldset>
        <fieldset class="customfields-fieldset">
            <legend><asp:Label ResourceKey="CustomFieldsLabel.Text" runat="server" /></legend>
            <ol class="customfields-form">
                <li class="customfield1">
                    <asp:Label CssClass="customfield1-label" runat="server" ResourceKey="CustomField1Label.Text" AssociatedControlId="CustomField1Label" />
                    <asp:Label ID="CustomField1Label" CssClass="customfield1-box full" runat="server" />
                </li>
                <li class="customfield2">
                    <asp:Label CssClass="customfield2-label" runat="server" ResourceKey="CustomField2Label.Text" AssociatedControlId="CustomField2Label" />
                    <asp:Label ID="CustomField2Label" CssClass="customfield2-box full" runat="server" />
                </li>
                <li class="customfield3">
                    <asp:Label CssClass="customfield3-label" runat="server" ResourceKey="CustomField3Label.Text" AssociatedControlId="CustomField3Label" />
                    <asp:Label ID="CustomField3Label" CssClass="customfield3-box full" runat="server" />
                </li>
                <li class="customfield4">
                    <asp:Label CssClass="customfield4-label" runat="server" ResourceKey="CustomField4Label.Text" AssociatedControlId="CustomField4Label" />
                    <asp:Label ID="CustomField4Label" CssClass="customfield4-box full" runat="server" />
                </li>
                <li class="customfield5">
                    <asp:Label CssClass="customfield5-label" runat="server" ResourceKey="CustomField5Label.Text" AssociatedControlId="CustomField5Label" />
                    <asp:Label ID="CustomField5Label" CssClass="customfield5-box full" runat="server" />
                </li>
                <li class="customfield6">
                    <asp:Label CssClass="customfield6-label" runat="server" ResourceKey="CustomField6Label.Text" AssociatedControlId="CustomField6Label" />
                    <asp:Label ID="CustomField6Label" CssClass="customfield6-box full" runat="server" />
                </li>
                <li class="customfield7">
                    <asp:Label CssClass="customfield7-label" runat="server" ResourceKey="CustomField7Label.Text" AssociatedControlId="CustomField7Label" />
                    <asp:Label ID="CustomField7Label" CssClass="customfield7-box full" runat="server" />
                </li>
                <li class="customfield8">
                    <asp:Label CssClass="customfield8-label" runat="server" ResourceKey="CustomField8Label.Text" AssociatedControlId="CustomField8Label" />
                    <asp:Label ID="CustomField8Label" CssClass="customfield8-box full" runat="server" />
                </li>
                <li class="customfield9">
                    <asp:Label CssClass="customfield9-label" runat="server" ResourceKey="CustomField9Label.Text" AssociatedControlId="CustomField9Label" />
                    <asp:Label ID="CustomField9Label" CssClass="customfield9-box full" runat="server" />
                </li>
                <li class="customfield10">
                    <asp:Label CssClass="customfield10-label" runat="server" ResourceKey="CustomField10Label.Text" AssociatedControlId="CustomField10Label" />
                    <asp:Label ID="CustomField10Label" CssClass="customfield10-box full" runat="server" />
                </li>
            </ol>
        </fieldset>
    </fieldset>
</div>