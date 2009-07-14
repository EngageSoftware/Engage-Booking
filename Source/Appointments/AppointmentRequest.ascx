<%@ Control Language="c#" AutoEventWireup="false" Inherits="Engage.Dnn.Booking.Appointments.AppointmentRequest" CodeBehind="AppointmentRequest.ascx.cs" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.UI.WebControls" Assembly="DotNetNuke" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx" %>
<%@ Register TagPrefix="engage" TagName="ModuleMessage" Src="../Controls/ModuleMessage.ascx" %>
<%@ Register TagPrefix="engage" Namespace="Engage.Controls" Assembly="Engage.Framework" %>

<engage:ModuleMessage runat="server" ID="SuccessModuleMessage" MessageType="Success" TextResourceKey="AddEventSuccess" CssClass="AddEventSuccessMessage"/>

<%--<div id="AddNewEvent" runat="server" class="AddNewEvent">

    <h2 class="SubHead">
        <asp:Label ID="AppointmentRequestLabel" runat="server" ResourceKey="AppointmentRequestLabel"/>
    </h2>
    
    <div class="AEEventTitle">
        <asp:Label ID="Label1" runat="server" ResourceKey="EventTitleLabel" CssClass="NormalBold" AssociatedControlID="EventTitleTextBox"/>
        <asp:TextBox ID="EventTitleTextBox" runat="server" CssClass="NormalTextBox" MaxLength="250"/>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="EventTitleTextBox" ResourceKey="EventTitleTextBoxRequired" Display="None" EnableClientScript="false"/>
    </div>
    
    <div class="AEEventStartDate">
        <asp:Label ID="Label2" runat="server" ResourceKey="EventStartDateLabel" CssClass="NormalBold" AssociatedControlID="StartDateTimePicker"/>
        <telerik:raddatetimepicker runat="server" id="StartDateTimePicker" skin="WebBlue">
            <timeview skin="WebBlue"/>
            <calendar skin="WebBlue" ShowRowHeaders="false"/>
            <DateInput InvalidStyleDuration="100"/>
            <ClientEvents OnDateSelected="StartDateTimePicker_DateSelected" />
        </telerik:raddatetimepicker>
        
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="StartDateTimePicker" ResourceKey="StartDateTimePickerRequired" Display="None" EnableClientScript="false"/>
    </div>
    
    <div class="AEEventEndDate">
        <asp:Label ID="Label3" runat="server" ResourceKey="EventEndDateLabel" CssClass="NormalBold" AssociatedControlID="EndDateTimePicker"/>
        <telerik:raddatetimepicker runat="server" id="EndDateTimePicker" skin="WebBlue">
            <timeview skin="WebBlue"/>
            <calendar skin="WebBlue" ShowRowHeaders="false"/>
            <DateInput InvalidStyleDuration="100"/>
        </telerik:raddatetimepicker>
        
        <asp:CompareValidator ID="CompareValidator1" 
            runat="server" Display="None" EnableClientScript="false"
            ControlToCompare="StartDateTimePicker"
            ControlToValidate="EndDateTimePicker" 
            ResourceKey="EndDateCompareValidator"
            Operator="GreaterThan"/>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="EndDateTimePicker" ResourceKey="EndDateTimePickerRequired" Display="None" EnableClientScript="false"/>
    </div>
    
    <div class="AEEventTimeZone">
        <asp:Label ID="Label4" runat="server" ResourceKey="EventTimeZoneLabel" CssClass="NormalBold" AssociatedControlID="TimeZoneDropDownList" />
        <div class="timezone_display">
        	<asp:DropDownList runat="server" ID="TimeZoneDropDownList" />
	        <div class="Normal"><asp:CheckBox runat="server" ID="InDaylightTimeCheckBox" ResourceKey="EventTimeZoneCheckBox" /></div>
        </div>
    </div>
    
    <div class="AEEventLocationAdd">
        <asp:Label ID="Label5" runat="server" ResourceKey="EventLocationLabel" CssClass="NormalBold" AssociatedControlID="EventLocationTextBox"/>
        <asp:TextBox ID="EventLocationTextBox" runat="server" CssClass="NormalTextBox"/>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="EventLocationTextBox" ResourceKey="EventLocationTextBoxRequired" Display="None" EnableClientScript="false"/>
    </div>

    <div class="AEEventEditor">
        <asp:Label ID="Label6" runat="server" ResourceKey="EventOverviewLabel" CssClass="NormalBold" AssociatedControlID="EventOverviewTextEditor"/>
        <asp:TextBox  ID="EventOverviewTextEditor" runat="server" Columns="30" Rows="5" Width="400" TextMode="MultiLine" Height="350" />
    </div>
    
    <div class="AEEventEditor">
        <asp:Label ID="Label7" runat="server" ResourceKey="EventDescriptionLabel" CssClass="NormalBold" AssociatedControlID="EventDescriptionTextEditor"/>
        <dnn:TextEditor ID="EventDescriptionTextEditor" runat="server" Width="400" TextRenderMode="Raw" HtmlEncode="False" DefaultMode="Rich" Height="350" ChooseMode="True" ChooseRender="False" />
        <asp:CustomValidator ID="EventDescriptionTextEditorValidator" runat="server" ResourceKey="EventDescriptionTextEditorRequired" Display="None"/>
    </div>
    
    <div class="AEEventFeature">
        <asp:Label ID="Label8" runat="server" ResourceKey="FeaturedEventLabel" CssClass="NormalBold" AssociatedControlID="FeaturedCheckBox"/>
        <asp:CheckBox ID="FeaturedCheckBox" runat="server" />        
    </div>
    <div class="AEEventRegister">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="AllowRegistrationsCheckBox" />
                <asp:AsyncPostBackTrigger ControlID="LimitRegistrationsCheckBox" />
                <asp:AsyncPostBackTrigger ControlID="CapacityMetMessageRadioButtonList" />
            </Triggers>
            <ContentTemplate>
                <asp:Label ID="Label9" runat="server" ResourceKey="AllowRegistrationsLabel" CssClass="NormalBold MainLabel" AssociatedControlID="AllowRegistrationsCheckBox"/>
                <asp:CheckBox ID="AllowRegistrationsCheckBox" runat="server" Checked="true" AutoPostBack="true" />
                <fieldset class="registration_fs">
                    <legend class="registrationoptions SubHead"><%=Localize("Registration Options:") %></legend>
                    <asp:Panel ID="LimitRegistrationsPanel" runat="server">
                        <asp:Label ID="LimitRegistrationsLabel" runat="server" ResourceKey="LimitRegistrationsLabel" CssClass="NormalBold RegCap" AssociatedControlID="LimitRegistrationsCheckBox" />
                        <asp:CheckBox ID="LimitRegistrationsCheckBox" runat="server" CssClass="NormalBold" AutoPostBack="true" />
                        <asp:Panel ID="RegistrationLimitPanel" runat="server" Visible="false">
                            <asp:Label ID="Label10" runat="server" ResourceKey="RegistrationLimitLabel" CssClass="NormalBold RegCap" AssociatedControlID="LimitRegistrationsCheckBox" />
                            <telerik:RadNumericTextBox ID="RegistrationLimitTextBox" runat="server" Value="25" MinValue="1" ShowSpinButtons="True"> 
                                <NumberFormat AllowRounding="True" DecimalDigits="0"/>
                            </telerik:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="RegistrationLimitTextBox" ResourceKey="RegistrationLimitRequiredValidator" CssClass="NormalRed" Display="None" />
                            <div>
                                <asp:Label ID="Label11" runat="server" ResourceKey="CapacityMetMessageLabel" CssClass="NormalBold RegCap" AssociatedControlID="CapacityMetMessageRadioButtonList" />
                                <asp:RadioButtonList ID="CapacityMetMessageRadioButtonList" runat="server" ResourceKey="CapacityMetMessageRadioButtonList" CssClass="NormalBold" AutoPostBack="true">
                                    <asp:ListItem ResourceKey="DefaultMessage" Value="False" Selected="True"/>
                                    <asp:ListItem ResourceKey="CustomMessage" Value="True"/>
                                </asp:RadioButtonList>
                            </div>
                            <asp:Panel ID="CustomCapacityMetMessagePanel" runat="server" Visible="false">
                                <asp:Label ID="Label12" runat="server" ResourceKey="CustomCapacityMetMessageLabel" CssClass="NormalBold RegCap" AssociatedControlID="CustomCapacityMetMessageTextEditor" />
                                <dnn:TextEditor ID="CustomCapacityMetMessageTextEditor" runat="server" Width="400" TextRenderMode="Raw" HtmlEncode="False" DefaultMode="Rich" Height="350" ChooseMode="True" ChooseRender="False" />
                            </asp:Panel>
                        </asp:Panel>
                    </asp:Panel>                
                </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="AEEventRecur">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="RecurringCheckbox" />
            </Triggers>
            <ContentTemplate>
                <asp:Label ID="RecurringEventLabel" runat="server" ResourceKey="RecurringEventLabel" CssClass="NormalBold" AssociatedControlID="RecurringCheckBox"/>
                <asp:CheckBox ID="RecurringCheckBox" runat="server" AutoPostBack="true" />
                <engage:RecurrenceEditor ID="RecurrenceEditor" runat="server" Visible="false" DatePickerSkin="WebBlue" />
                <asp:CustomValidator ID="RecurrenceEditorValidator" runat="server" ResourceKey="InvalidRecurrence" Display="None" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>--%>

<div>
    <fieldset>
    	<legend>Service Request Form</legend>
    	<p class="note"><strong>Note:</strong>Fields with asteriks (*) denote required fields.</p>
            <fieldset>
                <legend>Interpreting Event</legend>
                    <ol class="interpreting-form">
                        <li class="event-type medium">
                            <asp:Label ID="EventType" CssClass="event-type-label" runat="server" Text="Type of Event" AssociateControlId="EventType" />
                            <asp:TextBox ID="EventTypeBox" CssClass="event-type-box" runat="server" />
                        </li>
                         <li class="Title long">
                            <asp:Label ID="Title" CssClass="title-label" runat="server" Text="Title" AssociateControlId="TitleBox" />
                            <asp:TextBox ID="TitleBox" CssClass="title-box" runat="server" />
                         </li>
                         <li class="description full">
                            <asp:Label ID="Description" CssClass="description-label" runat="server" Text="Description of Event" AssociateControlId="DescriptionBox" />
                            <asp:TextBox ID="DescriptionBox" CssClass="description-box" runat="server" />
                        </li>
                        <li class="considerations full">
                            <asp:Label ID="Considerations" CssClass="considerations-label" runat="server" Text="Special Considerations" AssociateControlId="ConsiderationsBox" />
                            <asp:TextBox ID="ConsiderationsBox" CssClass="considerations-box" runat="server" />
                        </li>
                    </ol>
                    <h4>Address</h4>
                    <ol class="interpreting-form-b">
                        <li class="street long">
                            <asp:Label ID="Street" CssClass="street-label" runat="server" Text="Street" AssociateControlId="StreetBox" />
                            <asp:TextBox ID="StreetBox" CssClass="street-box" Visible="false" runat="server" />
                        </li>
                        <li class="room medium">
                            <asp:TextBox ID="RoomBox" CssClass="room-box" runat="server" />
                         </li>
                         <li class="city long">
                            <asp:Label ID="City" CssClass="city-label" runat="server" Text="City" AssociateControlId="CityBox" />
                            <asp:TextBox ID="CityBox" CssClass="city-box" runat="server" />
                         </li>
                         <li class="state medium">
                            <asp:TextBox ID="StateBox" CssClass="state-box" runat="server" />
                         </li>
                         <li class="zip short">
                            <asp:TextBox ID="ZipBox" CssClass="zip-box" runat="server" />
                         </li>
                         
                         <li class="nearest long">
                            <asp:Label ID="Nearest" CssClass="nearest-label" runat="server" Text="If DC/MD/VA, nearest Metro and nearest cross street" AssociateControlId="NearestBox" />
                            <asp:TextBox ID="NearestBox" CssClass="nearest-box" runat="server" />
                        </li>
                    </ol>
                    <h4>On-site Contact</h4>
                    <ol class="interpreting-form-c">
                        <li class="street long">
                            <asp:Label ID="OnSiteStreet" CssClass="street-label" runat="server" Text="Street" AssociateControlId="StreetBox" />
                            <asp:TextBox ID="OnSiteStreetBox" CssClass="street-box" Visible="false" runat="server" />
                        </li>
                        <li class="phone">
                                <asp:Label ID="Phone" CssClass="phone-label" Visible = "false" runat="server" Text="Phone" AssociateControlId="PhoneBox" />
                                <asp:TextBox ID="PhoneBox" CssClass="phone-box" Visible="false" runat="server" />
                        </li>
                    </ol>        
            </fieldset>
                    
            <fieldset>
                <legend>Person Requesting Interpreting Services</legend>
                    <ol class="requesting-form">
                        <li class="full-name long">
                            <asp:Label ID="FullName" CssClass="full-name-label" Visible = "false" runat="server" Text="Full Name" AssociateControlId="FullNameBox" />
                            <asp:TextBox ID="FullNameBox" CssClass="full-name-box" Visible="false" runat="server" />
                        </li>
                         <li class="phone">
                            <asp:Label ID="RequestorPhone" CssClass="phone-label" Visible = "false" runat="server" Text="Phone" AssociateControlId="PhoneBox" />
                            <asp:TextBox ID="RequestorPhoneBox" CssClass="phone-box" Visible="false" runat="server" />
                         </li>
                         <li class="alt-phone">
                            <asp:Label ID="AltPhone" CssClass="alt-phone-label" Visible = "false" runat="server" Text="Alternate Phone" AssociateControlId="AltPhoneBox" />
                            <asp:TextBox ID="AltPhoneBox" CssClass="alt-phone-box" Visible="false" runat="server" />
                        </li>
                        <li class="email long">
                            <asp:Label ID="Email" CssClass="email-label" Visible = "false" runat="server" Text="Email Address" AssociateControlId="EmailBox" />
                            <asp:TextBox ID="EmailBox" CssClass="email-box" Visible="false" runat="server" />
                        </li>
                    </ol>
            </fieldset>
        
            <fieldset>
                <legend>Date of Assignment</legend>
                    <ol class="date-assignment-form">
                        <li class="start medium">
                            <asp:Label ID="Start" CssClass="start-label" Visible = "false" runat="server" Text="Start" AssociateControlId="StartBox" />
                            <asp:TextBox ID="StartBox" CssClass="start-box" Visible="false" runat="server" />
                            <telerik:raddatetimepicker runat="server" id="StartDateTimePicker" skin="WebBlue">
                                <timeview skin="WebBlue"/>
                                <calendar skin="WebBlue" ShowRowHeaders="false"/>
                                <DateInput InvalidStyleDuration="100"/>
                                <ClientEvents OnDateSelected="StartDateTimePicker_DateSelected" />
                            </telerik:raddatetimepicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="StartDateTimePicker" ResourceKey="StartDateTimePickerRequired" Display="None" EnableClientScript="false"/>
                         </li>
                         <li class="at-time short">
                            <asp:Label ID="AtTime" CssClass="at-time-label" Visible = "false" runat="server" Text="at" AssociateControlId="AtTimeBox" />
                            <asp:TextBox ID="AtTimeBox" CssClass="at-time-box" Visible="false" runat="server" />                            
                           
                         </li>
                         <li class="end medium">
                            <asp:Label ID="End" CssClass="end-label" Visible = "false" runat="server" Text="End:" AssociateControlId="EndBox" />
                            <asp:TextBox ID="EndBox" CssClass="end-box" Visible="false" runat="server" />
                            <telerik:raddatetimepicker runat="server" id="EndDateTimePicker" skin="WebBlue">
                                <timeview skin="WebBlue"/>
                                <calendar skin="WebBlue" ShowRowHeaders="false"/>
                                <DateInput InvalidStyleDuration="100"/>
                            </telerik:raddatetimepicker>
                            
                            <asp:CompareValidator ID="CompareValidator1" 
                                runat="server" Display="None" EnableClientScript="false"
                                ControlToCompare="StartDateTimePicker"
                                ControlToValidate="EndDateTimePicker" 
                                ResourceKey="EndDateCompareValidator"
                                Operator="GreaterThan"/>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="EndDateTimePicker" ResourceKey="EndDateTimePickerRequired" Display="None" EnableClientScript="false"/>
                        </li>
                        <li class="at-time short">
                            <asp:Label ID="AtEndTime" CssClass="at-time-label" Visible = "false" runat="server" Text="at" AssociateControlId="AtTimeBox" />
                            <asp:TextBox ID="AtEndTimeBox" CssClass="at-time-box" Visible="false" runat="server" />
                         </li>
                    </ol>
            </fieldset>
        
            <fieldset>
                <legend>Participants</legend>
                    <ol class="participants-form">
                        <li class="number-deaf short">
                            <asp:Label ID="NumberDeaf" CssClass="number-deaf-label" Visible = "false" runat="server" Text="Number of Deaf Participants" AssociateControlId="NumberDeafBox" />
                            <asp:TextBox ID="NumberDeafBox" CssClass="number-deaf-box" Visible="false" runat="server" />
                            <=== insert you calendar img here ===>
                        </li>
                         <li class="total-number short">
                            <asp:Label ID="TotalNumber" CssClass="total-number-label" Visible = "false" runat="server" Text="Total Number of Participants" AssociateControlId="TotalNumberBox" />
                            <asp:TextBox ID="TotalNumberBox" CssClass="total-number-box" Visible="false" runat="server" />
                            <=== insert you calendar img here ===>
                         </li>
                         <li class="male short">
                            <asp:Label ID="Male" CssClass="male-label" Visible = "false" runat="server" Text="Male" AssociateControlId="MaleBox" />
                            <asp:TextBox ID="MaleBox" CssClass="male-box" Visible="false" runat="server" />
                        </li>
                        <li class="female short">
                            <asp:Label ID="Female" CssClass="female-label" Visible = "false" runat="server" Text="Female" AssociateControlId="FemaleBox" />
                            <asp:TextBox ID="FemaleBox" CssClass="female-box" Visible="false" runat="server" />
                        </li>
                        <li class="instructions full">
                            <asp:Label ID="Instructions" CssClass="instructions-label" Visible = "false" runat="server" Text="Email Address" AssociateControlId="InstructionsBox" />
                            <asp:TextBox ID="InstructionsBox" CssClass="instructions-box" Visible="false" runat="server" />
                        </li>
                    </ol>
			</fieldset>
	</fieldset> 
</div>	

<engage:ValidationSummary ID="ValidationSummary1" runat="server" />

<%--<asp:MultiView ID="FooterMultiview" runat="server" ActiveViewIndex="0">
    <asp:View ID="AddEventFooterView" runat="server">
        <div class="AddEventFooterButtons AdminButtons FooterButtons">
            <asp:ImageButton ID="SaveEventButton" runat="server" CssClass="Normal" ImageUrl="~/DesktopModules/EngageEvents/Images/save.gif" />
            <asp:HyperLink ID="CancelEventLink" runat="server" CssClass="Normal" ImageUrl="~/DesktopModules/EngageEvents/Images/cancel_go_home.gif" ResourceKey="Cancel.Alt" />
            <asp:ImageButton ID="SaveAndCreateNewEventButton" runat="server" CssClass="Normal" ImageUrl="~/DesktopModules/EngageEvents/Images/save_create_new.gif"/>
        </div>
    </asp:View>
    <asp:View ID="FinalFooterView" runat="server">
        <div class="FinalButtons AdminButtons FooterButtons">
            <asp:HyperLink ID="CreateAnotherEventLink" runat="server" CssClass="Normal" ImageUrl="~/DesktopModules/EngageEvents/Images/create_another_event.gif" ResourceKey="CreateAnother.Alt" />
            <%--<asp:LinkButton ID="CreateEventEmailButton" runat="server">Create E-Mail For This Event</asp:LinkButton>--%>
            <asp:HyperLink ID="HyperLink1" runat="server" CssClass="Normal" ImageUrl="~/DesktopModules/EngageEvents/Images/home.gif" ResourceKey=CancelGoHome.Alt" />
        </div>
    </asp:View>
</asp:MultiView>--%>
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
