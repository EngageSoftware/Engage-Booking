// <copyright file="AppointmentCalendar.ascx.cs" company="Engage Software">
// Engage: Booking - http://www.EngageSoftware.com
// Copyright (c) 2004-2009
// by Engage Software ( http://www.engagesoftware.com )
// </copyright>
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.

namespace Engage.Dnn.Booking.Display
{
    using System;
    using System.Globalization;
    using System.Web.UI;
    using Booking;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.Localization;
    using Telerik.Web.UI;
    using Setting = Setting;
    using Utility = Utility;

    /// <summary>
    /// Control to display the events calendar view
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
            this.EventsCalendarDisplay.AppointmentCreated += this.EventsCalendarDisplay_AppointmentCreated;
            this.EventsCalendarDisplay.AppointmentDataBound += this.EventsCalendarDisplay_AppointmentDataBound;
            this.EventsCalendarToolTipManager.AjaxUpdate += this.EventsCalendarToolTipManager_AjaxUpdate;
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.AddJQueryReference();
                this.LocalizeCalendar();
                this.SetupApprovalControl();
                this.RequestAppointmentLink.NavigateUrl = this.BuildLinkUrl(this.ModuleId, "AppointmentRequest");
                this.BindData();
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// <summary>
        /// Sets up the approval control.
        /// </summary>
        private void SetupApprovalControl()
        {
            this.ApprovalControl.Visible = this.IsEditable;
            this.ApprovalControl.ModuleConfiguration = this.ModuleConfiguration;
        }

        /// <summary>
        /// Localizes the calendar.
        /// </summary>
        private void LocalizeCalendar()
        {
            this.EventsCalendarDisplay.Localization.HeaderToday = Localization.GetString("HeaderToday.Text", this.LocalResourceFile);
            this.EventsCalendarDisplay.Localization.HeaderPrevDay = Localization.GetString("HeaderPrevDay.Text", this.LocalResourceFile);
            this.EventsCalendarDisplay.Localization.HeaderNextDay = Localization.GetString("HeaderNextDay.Text", this.LocalResourceFile);
            this.EventsCalendarDisplay.Localization.HeaderDay = Localization.GetString("HeaderDay.Text", this.LocalResourceFile);
            this.EventsCalendarDisplay.Localization.HeaderWeek = Localization.GetString("HeaderWeek.Text", this.LocalResourceFile);
            this.EventsCalendarDisplay.Localization.HeaderMonth = Localization.GetString("HeaderMonth.Text", this.LocalResourceFile);

            this.EventsCalendarDisplay.Localization.AllDay = Localization.GetString("AllDay.Text", this.LocalResourceFile);
            this.EventsCalendarDisplay.Localization.Show24Hours = Localization.GetString("Show24Hours.Text", this.LocalResourceFile);
            this.EventsCalendarDisplay.Localization.ShowBusinessHours = Localization.GetString("ShowBusinessHours.Text", this.LocalResourceFile);

            this.EventsCalendarDisplay.Localization.ShowMore = Localization.GetString("ShowMore.Text", this.LocalResourceFile);
        }

        /// <summary>
        /// Handles the AppointmentCreated event of the EventsCalendarDisplay control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.AppointmentCreatedEventArgs"/> instance containing the event data.</param>
        private void EventsCalendarDisplay_AppointmentCreated(object sender, AppointmentCreatedEventArgs e)
        {
            if (e.Appointment.Visible && !this.IsAppointmentRegisteredForTooltip(e.Appointment))
            {
                foreach (AppointmentControl appointmentControl in e.Appointment.AppointmentControls)
                {
                    this.EventsCalendarToolTipManager.TargetControls.Add(appointmentControl.ClientID, e.Appointment.ID.ToString(), true);                    
                }
            }
        }

        /// <summary>
        /// Handles the AppointmentDataBound event of the EventsCalendarDisplay control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.SchedulerEventArgs"/> instance containing the event data.</param>
        private void EventsCalendarDisplay_AppointmentDataBound(object sender, SchedulerEventArgs e)
        {
            this.EventsCalendarToolTipManager.TargetControls.Clear();
            ScriptManager.RegisterStartupScript(this, typeof(AppointmentCalendar), "HideToolTip", "hideActiveToolTip();", true);
        }

        /// <summary>
        /// Handles the AjaxUpdate event of the EventsCalendarToolTipManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.ToolTipUpdateEventArgs"/> instance containing the event data.</param>
        private void EventsCalendarToolTipManager_AjaxUpdate(object sender, ToolTipUpdateEventArgs e)
        {
            int eventId;
            if (int.TryParse(e.Value.Split('_')[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out eventId))
            {
                Booking.Appointment appointment = Booking.Appointment.Load(eventId);
                EventToolTip toolTip = (EventToolTip)this.LoadControl("EventToolTip.ascx");

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
            foreach (ToolTipTargetControl targetControl in this.EventsCalendarToolTipManager.TargetControls)
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
            this.EventsCalendarDisplay.Culture = CultureInfo.CurrentCulture;
            this.EventsCalendarDisplay.DataSource = Booking.AppointmentCollection.Load(this.ModuleId);
            this.EventsCalendarDisplay.DataEndField = "EndDateTime";
            this.EventsCalendarDisplay.DataKeyField = "AppointmentId";
            this.EventsCalendarDisplay.DataStartField = "StartDateTime";
            this.EventsCalendarDisplay.DataSubjectField = "Title";
            this.EventsCalendarDisplay.DataBind();

            string skinSetting = Utility.GetStringSetting(this.Settings, Setting.SkinSelection.PropertyName);
            if (skinSetting != null)
            {
                this.EventsCalendarDisplay.Skin = this.EventsCalendarToolTipManager.Skin = skinSetting;
            }

            this.EventsCalendarDisplay.MonthView.VisibleAppointmentsPerDay = Utility.GetIntSetting(this.Settings, Setting.EventsPerDay.PropertyName, 3);
        }
    }
}