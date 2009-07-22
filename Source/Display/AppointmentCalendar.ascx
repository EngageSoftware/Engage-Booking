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
        var controller = Telerik.Web.UI.RadToolTipController.getInstance();
        var tooltip = controller.get_activeToolTip();
        if (tooltip) {
            tooltip.hide();
        }
    }
    
    (function($) {
        var newAppointmentUrl = '<%=ClientAPI.GetSafeJSString(NewAppointmentUrlTemplate) %>',
            radSchedulerId = '<%=this.AppointmentsCalendar.ClientID %>',
            timeoutValue = null;
        
        function formatDateUrlParameter(date) {
            return encodeURIComponent(date.format("yyyy-MM-dd-HH-mm"));
        }

        pageRequestManager.add_endRequest(function() {
            wireupCalendarHover();
        });

        function wireupCalendarHover() {
            $('.EventCalendar .rsContentTable td')
                .unbind('mouseenter').unbind('mouseleave')
                .hover(function(event) {
                    clearTimeout(timeoutValue);

                    if (!$('.NewAppointmentToolip').is(':visible')) {
                        var timeSlotPosition = $(this).position(),
                        timeSlot = $find(radSchedulerId).get_activeModel().getTimeSlotFromDomElement(this);
                        
                        $('.NewAppointmentTooltip')
                            .show()
                            .css('top', event.clientY)
                            .css('left', event.clientX)
                        .find('a')
                            .attr('href', String.format(newAppointmentUrl, formatDateUrlParameter(timeSlot.get_startTime()), formatDateUrlParameter(timeSlot.get_endTime())));
                    }
                }, function() {
                    if ($('.NewAppointmentToolip').is(':visible')) {
                        timeoutValue = setTimeout(function() {
                            $('.NewAppointmentTooltip')
                                .hide();
                        }
                            , 100);
                    }
                    else {
                        clearTimeout(timeoutValue);
                    }
                });
        }

        $(function() {
            wireupCalendarHover();
            $('.NewAppointmentTooltip').hover(function() {
                clearTimeout(timeoutValue);
            }, function() {
                clearTimeout(timeoutValue);
                timeoutValue = setTimeout(function() {
                    $('.NewAppointmentTooltip')
                        .hide();
                }
                    , 100);
            });
        });
    })(jQuery);
//]]>
</script>

<engage:approval ID="ApprovalControl" runat="server" />

<%-- <dnn:sectionhead ID="CalendarHeader" runat="server" CssClass="approval-calendar-header" ResourceKey="CalendarHeader" Section="CalendarWrapper"  /> --%>
<%--<div id="CalendarWrapper" runat="server" class="EventCalendar">--%>
<div class="EventCalendar">
    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <telerik:radscheduler id="AppointmentsCalendar" runat="server" CssClass="booking-calendar" ReadOnly="True" 
                TimelineView-UserSelectable="False" OverflowBehavior="Expand" ShowAllDayRow="False" />
            <telerik:radtooltipmanager runat="server" id="CalendarToolTipManager" width="300" height="150"
                animation="None" position="BottomRight" HideEvent="LeaveTooltip" text="Loading..." AutoTooltipify="false" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Hyperlink ID="RequestAppointmentLink" runat="server" CssClass="RequestAppointmentLink" ResourceKey="RequestAppointment.Text" />
    <div class="NewAppointmentTooltip Normal">
        <asp:Hyperlink runat="server" ResourceKey="RequestAppointment.Text" />
    </div>
</div>