// <copyright file="AppointmentCalendar.ascx.cs" company="Engage Software">
// Engage: Booking
// Copyright (c) 2004-2009
// by Engage Software ( http://www.engagesoftware.com )
// </copyright>
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.

namespace Engage.Dnn.Booking
{
    using System;
    using System.Globalization;
    using System.Web.UI;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.Localization;
    using Telerik.Web.UI;

    /// <summary>
    /// A calendar view of appointments, with optional approval control for editors of the module
    /// </summary>
    public partial class AppointmentCalendar : ModuleBase
    {
        /// <summary>
        /// Gets the URL template (in <see cref="string.Format(string,object)"/> notation) to use to get to the new appointment page (for a specific time range).
        /// </summary>
        /// <value>The new appointment URL template.</value>
        protected string NewAppointmentUrlTemplate
        {
            get
            {
                // We can't just send {0} to BuildLinkUrl, because it will get "special treatment" by the friendly URL provider for its special characters
                return this.BuildLinkUrl(this.ModuleId, "AppointmentRequest", "startTime=__--0--__", "endTime=__--1--__").Replace("__--", "{").Replace("--__", "}");
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.Load += this.Page_Load;
            this.AppointmentsCalendar.AppointmentCreated += this.EventsCalendarDisplay_AppointmentCreated;
            this.AppointmentsCalendar.AppointmentDataBound += this.EventsCalendarDisplay_AppointmentDataBound;
            this.CalendarToolTipManager.AjaxUpdate += this.EventsCalendarToolTipManager_AjaxUpdate;
        }

        /// <summary>
        /// Handles the <see cref="Control.Load"/> event of this control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.AddJQueryReference();
                this.LocalizeCalendar();
                this.SetupAdminView();
                this.AppointmentsCalendar.SelectedView = this.IsEditable ? SchedulerViewType.DayView : SchedulerViewType.MonthView;
                if (Booking.ModuleSettings.AppointmentRequestsRole.GetValueAsStringFor(this) == "All Users" || this.UserInfo.IsInRole(Booking.ModuleSettings.AppointmentRequestsRole.GetValueAsStringFor(this)))
                {
                    this.NewAppointmentToolTip.Visible = true;
                    this.RequestAppointmentLink.Visible = true;
                    this.RequestAppointmentLink.NavigateUrl = this.BuildLinkUrl(this.ModuleId, "AppointmentRequest");
                }

                this.BindData();
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// <summary>
        /// Sets up the <see cref="ApprovalControl"/>
        /// </summary>
        private void SetupAdminView()
        {
            this.ApprovalControl.Visible = /*this.CalendarHeader.Visible =*/ this.IsEditable;
            ////this.CalendarHeader.IsExpanded = !this.CalendarHeader.Visible;
            this.ApprovalControl.ModuleConfiguration = this.ModuleConfiguration;
        }

        /// <summary>
        /// Localizes the calendar.
        /// </summary>
        private void LocalizeCalendar()
        {
            this.AppointmentsCalendar.Culture = CultureInfo.CurrentCulture;
            this.AppointmentsCalendar.FirstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;

            this.AppointmentsCalendar.Localization.HeaderToday = Localization.GetString("HeaderToday.Text", this.LocalResourceFile);
            this.AppointmentsCalendar.Localization.HeaderPrevDay = Localization.GetString("HeaderPrevDay.Text", this.LocalResourceFile);
            this.AppointmentsCalendar.Localization.HeaderNextDay = Localization.GetString("HeaderNextDay.Text", this.LocalResourceFile);
            this.AppointmentsCalendar.Localization.HeaderDay = Localization.GetString("HeaderDay.Text", this.LocalResourceFile);
            this.AppointmentsCalendar.Localization.HeaderWeek = Localization.GetString("HeaderWeek.Text", this.LocalResourceFile);
            this.AppointmentsCalendar.Localization.HeaderMonth = Localization.GetString("HeaderMonth.Text", this.LocalResourceFile);

            this.AppointmentsCalendar.Localization.AllDay = Localization.GetString("AllDay.Text", this.LocalResourceFile);
            this.AppointmentsCalendar.Localization.Show24Hours = Localization.GetString("Show24Hours.Text", this.LocalResourceFile);
            this.AppointmentsCalendar.Localization.ShowBusinessHours = Localization.GetString("ShowBusinessHours.Text", this.LocalResourceFile);

            this.AppointmentsCalendar.Localization.ShowMore = Localization.GetString("ShowMore.Text", this.LocalResourceFile);
        }

        /// <summary>
        /// Handles the <see cref="RadScheduler.AppointmentCreated"/> event of the <see cref="AppointmentCalendar"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.AppointmentCreatedEventArgs"/> instance containing the event data.</param>
        private void EventsCalendarDisplay_AppointmentCreated(object sender, AppointmentCreatedEventArgs e)
        {
            if (e.Appointment.Visible && !this.IsAppointmentRegisteredForTooltip(e.Appointment))
            {
                foreach (AppointmentControl appointmentControl in e.Appointment.AppointmentControls)
                {
                    this.CalendarToolTipManager.TargetControls.Add(appointmentControl.ClientID, e.Appointment.ID.ToString(), true);                    
                }
            }
        }

        /// <summary>
        /// Handles the <see cref="RadScheduler.AppointmentDataBound"/> event of the <see cref="AppointmentCalendar"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.SchedulerEventArgs"/> instance containing the event data.</param>
        private void EventsCalendarDisplay_AppointmentDataBound(object sender, SchedulerEventArgs e)
        {
            this.CalendarToolTipManager.TargetControls.Clear();
            ScriptManager.RegisterStartupScript(this, typeof(AppointmentCalendar), "HideToolTip", "hideActiveToolTip();", true);
        }

        /// <summary>
        /// Handles the <see cref="RadToolTipManager.AjaxUpdate"/> event of the <see cref="CalendarToolTipManager"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.ToolTipUpdateEventArgs"/> instance containing the event data.</param>
        private void EventsCalendarToolTipManager_AjaxUpdate(object sender, ToolTipUpdateEventArgs e)
        {
            int eventId;
            if (int.TryParse(e.Value.Split('_')[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out eventId))
            {
                Appointment appointment = Appointment.Load(eventId);
                AppointmentToolTip toolTip = (AppointmentToolTip)this.LoadControl("AppointmentToolTip.ascx");

                toolTip.ModuleConfiguration = this.ModuleConfiguration;
                toolTip.SetAppointment(appointment);
                e.UpdatePanel.ContentTemplateContainer.Controls.Add(toolTip);
            }
        }

        /// <summary>
        /// Determines whether the specified appointment is registered with the tooltip manager.
        /// </summary>
        /// <param name="apt">The appointment</param>
        /// <returns>
        /// <c>true</c> if the specified appointment is registered with the tooltip manager; otherwise, <c>false</c>.
        /// </returns>
        private bool IsAppointmentRegisteredForTooltip(Telerik.Web.UI.Appointment apt)
        {
            foreach (ToolTipTargetControl targetControl in this.CalendarToolTipManager.TargetControls)
            {
                if (targetControl.TargetControlID == apt.ClientID)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            var appointments = AppointmentCollection.Load(this.ModuleId, true);
            if (!this.IsEditable)
            {
                this.CalendarToolTipManager.Visible = false;

                foreach (var appointment in appointments)
                {
                    appointment.Title = Localization.GetString("Timeslot Taken", this.LocalResourceFile);
                }
            }

            this.AppointmentsCalendar.DataSource = appointments;
            this.AppointmentsCalendar.DataEndField = "EndDateTime";
            this.AppointmentsCalendar.DataKeyField = "AppointmentId";
            this.AppointmentsCalendar.DataStartField = "StartDateTime";
            this.AppointmentsCalendar.DataSubjectField = "Title";
            this.AppointmentsCalendar.DataBind();

            this.AppointmentsCalendar.Skin = this.CalendarToolTipManager.Skin = ModuleSettings.CalendarSkin.GetValueAsStringFor(this);
            this.AppointmentsCalendar.MonthView.VisibleAppointmentsPerDay = ModuleSettings.AppointmentsToDisplayPerDay.GetValueAsInt32For(this).Value;
        }
    }
}
