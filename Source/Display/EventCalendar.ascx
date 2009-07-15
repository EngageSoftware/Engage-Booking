<%@ Control Language="c#" AutoEventWireup="false" Inherits="Engage.Dnn.Booking.Display.EventCalendar" CodeBehind="EventCalendar.ascx.cs" %>
<%@ Import Namespace="DotNetNuke.UI.Utilities" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.UI.WebControls" Assembly="DotNetNuke" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<script type="text/javascript">
//<![CDATA[
    (function($) {
        var newAppointmentUrl = '<%=ClientAPI.GetSafeJSString(NewAppointmentUrlTemplate) %>',
            radSchedulerId = '<%=EventsCalendarDisplay.ClientID %>',
            formatDateUrlParameter = function(date) {
                return encodeURIComponent(date.format("yyyy-MM-dd-HH-mm"));
            }
        timeoutValue = null;

        var pageRequestManager = Sys.WebForms.PageRequestManager.getInstance();
        pageRequestManager.add_beginRequest(function(sender, args) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (args.get_postBackElement().id.indexOf('EventsCalendarDisplay') != -1) {
                hideActiveToolTip();
            }
        });
        pageRequestManager.add_endRequest(function() {
            wireupCalendarHover();
        });

        function hideActiveToolTip() {
            var controller = Telerik.Web.UI.RadToolTipController.getInstance();
            var tooltip = controller.get_activeToolTip();
            if (tooltip) {
                tooltip.hide();
            }
        }

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

<div class="EventHeader">
    <h2 class="NormalBold">
        <asp:Label runat="server" ResourceKey="EventsTitle" />
    </h2>
</div>
<div class="EventCalendar">
    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <telerik:radscheduler id="EventsCalendarDisplay" runat="server" CssClass="booking-calendar" ReadOnly="True" 
                TimelineView-UserSelectable="False" OverflowBehavior="Expand" ShowAllDayRow="False" />
            <telerik:radtooltipmanager runat="server" id="EventsCalendarToolTipManager" width="300" height="150"
                animation="None" position="BottomRight" HideEvent="LeaveTooltip" text="Loading..." AutoTooltipify="false" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Hyperlink ID="RequestAppointmentLink" runat="server" CssClass="RequestAppointmentLink" ResourceKey="RequestAppointment.Text" />
    <div class="NewAppointmentTooltip Normal">
        <asp:Hyperlink runat="server" ResourceKey="RequestAppointment.Text" />
    </div>
</div>