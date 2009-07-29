<%@ Control Language="c#" AutoEventWireup="false" Inherits="Engage.Dnn.Booking.AppointmentCalendar" CodeBehind="AppointmentCalendar.ascx.cs" %>
<%@ Import Namespace="DotNetNuke.UI.Utilities" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.UI.WebControls" Assembly="DotNetNuke" %>
<%@ Register TagPrefix="dnn" TagName="SectionHead" Src="~/controls/sectionheadcontrol.ascx" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="engage" TagName="approval" Src="Approval.ascx" %>

<script type="text/javascript">
//<![CDATA[
    var pageRequestManager = Sys.WebForms.PageRequestManager.getInstance();
    pageRequestManager.add_beginRequest(function(sender, args) {
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (args.get_postBackElement().id.indexOf('AppointmentsCalendar') != -1) {
            hideActiveToolTip();
        }
    });

    function hideActiveToolTip() {
        if (Telerik.Web.UI.ReadToolTipController) {
            var controller = Telerik.Web.UI.RadToolTipController.getInstance();
            if (controller) {
                var tooltip = controller.get_activeToolTip();
                if (tooltip) {
                    tooltip.hide();
                }
            }
        }
    }
    
    (function($) {
        var newAppointmentUrl = '<%=ClientAPI.GetSafeJSString(NewAppointmentUrlTemplate) %>',
            radSchedulerId = '<%=this.AppointmentsCalendar.ClientID %>';

        function formatDateUrlParameter(date) {
            return encodeURIComponent(date.format("yyyy-MM-dd-HH-mm"));
        }

        function wireupCalendarHover() {
            var otherToolTipManager = $find('<%=AppointmentToolTipManager.ClientID %>'),
                appointmentControls = [];
            if (otherToolTipManager) {
                appointmentControls = otherToolTipManager.get_targetControls();
            }
            
            $('.appointments-calendar .rsContentTable td').one('mouseenter', function(event) {
                var toolTipManager = $find('<%=NewRequestToolTipManager.ClientID %>'),
                    hasAppointments = false,
                    $this = $(this);

                $.each(appointmentControls, function(i, targetControl) {
                    hasAppointments = $this.find('#' + targetControl[0]).length;

                    // returning false is like 'break', true is like 'continue'
                    // so, if it finds that an appointment is a child of this control, it sets hasAppointments to true and exits the loop
                    return !hasAppointments;
                });

                if (toolTipManager && !hasAppointments) {
                    var tooltip = toolTipManager.getToolTipByElement(this);

                    if (!tooltip) {
                        tooltip = toolTipManager.createToolTip(this);

                        var timeSlot = $find(radSchedulerId).get_activeModel().getTimeSlotFromDomElement(this);
                        tooltip.set_content("<a href='" + String.format(newAppointmentUrl, formatDateUrlParameter(timeSlot.get_startTime()), formatDateUrlParameter(timeSlot.get_endTime())) + "'><%=ClientAPI.GetSafeJSString(Localize("Request Appointment.Text")) %></a>");
                    }

                    tooltip.show();
                }
            });
        }

        pageRequestManager.add_endRequest(function() {
            wireupCalendarHover();
        });

        $(document).ready(function() {
            wireupCalendarHover();
        });
    })(jQuery);
//]]>
</script>

<engage:approval ID="ApprovalControl" runat="server" />

<%-- <dnn:sectionhead ID="CalendarHeader" runat="server" CssClass="approval-calendar-header" ResourceKey="CalendarHeader" Section="CalendarWrapper"  /> --%>
<%--<div id="CalendarWrapper" runat="server" class="appointments-calendar">--%>
<div class="appointments-calendar">
    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <telerik:radscheduler id="AppointmentsCalendar" runat="server" CssClass="booking-calendar" ReadOnly="True" 
                TimelineView-UserSelectable="False" OverflowBehavior="Expand" ShowAllDayRow="False" />
            <telerik:radtooltipmanager runat="server" id="AppointmentToolTipManager" width="300" height="150"
                animation="None" position="BottomRight" HideEvent="LeaveTooltip" text="Loading..." AutoTooltipify="false" />
            <telerik:radtooltipmanager runat="server" id="NewRequestToolTipManager" width="150" height="75"
                animation="None" position="BottomRight" HideEvent="LeaveTooltip" text="Loading..." AutoTooltipify="false" Visible="false" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Hyperlink ID="RequestAppointmentLink" Visible="false" runat="server" CssClass="RequestAppointmentLink" ResourceKey="Request Appointment.Text" />
</div>