// <copyright file="AppointmentRequest.ascx.cs" company="Engage Software">
// Engage: Events - http://www.EngageSoftware.com
// Copyright (c) 2004-2009
// by Engage Software ( http://www.engagesoftware.com )
// </copyright>
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.

namespace Engage.Dnn.Booking.Appointments
{
    using System;
    using System.Globalization;
    using System.Web;
    using Booking;
    using DotNetNuke.Services.Exceptions;

    /// <summary>
    /// Appointment Request Form
    /// </summary>
    public partial class AppointmentRequest : ModuleBase
    {
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += this.Page_Load;
            ////this.SaveAppointmentButton.Click += this.SaveAppointmentButton_Click;
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
                if (!this.IsPostBack)
                {
                    this.StartDateTimePicker.SelectedDate = this.GetDateFromQueryString("startTime");
                    this.EndDateTimePicker.SelectedDate = this.GetDateFromQueryString("endTime");
                    //// this.SetupControl();
                    //// this.BindData();
                }

                this.SuccessModuleMessage.Visible = false;
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /////// <summary>
        /////// Handles the OnClick event of the SaveAppointmentButton control.
        /////// </summary>
        /////// <param name="sender">The source of the event.</param>
        /////// <param name="e">The <see cref="System.EventArgs"/> instance containing the appointment data.</param>
        ////private void SaveAppointmentButton_OnClick(object sender, EventArgs e)
        ////{
        ////    try
        ////    {
        ////        if (this.Page.IsValid)
        ////        {
        ////            this.Save();
        ////            this.DisplayFinalSuccess();
        ////        }
        ////    }
        ////    catch (Exception exc)
        ////    {
        ////        Exceptions.ProcessModuleLoadException(this, exc);
        ////    }
        ////}

        /////// <summary>
        /////// Displays the final success.
        /////// </summary>
        ////private void DisplayFinalSuccess()
        ////{
        ////    this.SuccessModuleMessage.Visible = true;
        ////    ////this.AddNewEvent.Visible = false;
        ////    ////this.FooterMultiview.SetActiveView(this.FinalFooterView);
        ////}

        /////// <summary>
        /////// This method will either update or create an event based on the current context of EventId
        /////// </summary>
        ////private void Save()
        ////{
        ////    if (this.AppointmentId.HasValue)
        ////    {
        ////        this.Update();
        ////    }
        ////    else
        ////    {
        ////        this.Insert();
        ////    }
        ////}

        /////// <summary>
        /////// This method is responsible for updating an event which already exists in the data store.
        /////// </summary>
        ////private void Update()
        ////{
        ////    ////int timeZoneOffsetMinutes;
        ////    int? appointmentId = this.AppointmentId;
        ////    ////if (appointmentId.HasValue && int.TryParse(this.TimeZoneDropDownList.SelectedValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out timeZoneOffsetMinutes))
        ////    ////{
        ////    ////    TimeSpan timeZoneOffset = new TimeSpan(0, timeZoneOffsetMinutes, 0);
        ////    ////    if (this.InDaylightTimeCheckBox.Checked)
        ////    ////    {
        ////    ////        timeZoneOffset = timeZoneOffset.Add(new TimeSpan(1, 0, 0));
        ////    ////    }

        ////        Appointment appointment = Appointment.Load(appointmentId.Value);
        ////        ////appointment.StartDateTime = this.StartDateTimePicker.SelectedDate.Value;
        ////        ////appointment.EndDateTime = this.EndDateTimePicker.SelectedDate.Value;
        ////        ////appointment.Title = this.EventTitleTextBox.Text;
        ////        ////appointment.Description = this.EventDescriptionTextEditor.Text;
        ////        ////...
        ////        appointment.Save(this.UserId);
        ////    ////}
        ////}

        /////// <summary>
        /////// Based on the values entered by the user for the event, this method will populate an event object and call the Event object's save method.
        /////// </summary>
        ////private void Insert()
        ////{
            ////int timeZoneOffsetMinutes;
            ////if (int.TryParse(this.TimeZoneDropDownList.SelectedValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out timeZoneOffsetMinutes))
            ////{
            ////    TimeSpan timeZoneOffset = new TimeSpan(0, timeZoneOffsetMinutes, 0);
            ////    if (this.InDaylightTimeCheckBox.Checked)
            ////    {
            ////        timeZoneOffset = timeZoneOffset.Add(new TimeSpan(1, 0, 0));
            ////    }

            ////    DateTime eventStart = this.StartDateTimePicker.SelectedDate.Value;
            ////    DateTime eventEnd = this.EndDateTimePicker.SelectedDate.Value;
            ////    Appointment appointment = Appointment.Create(
            ////            this.PortalId,
            ////            this.ModuleId,
            ////            this.UserInfo.Email,
            ////            this.EventTitleTextBox.Text,
            ////            this.EventOverviewTextEditor.Text,
            ////            this.EventDescriptionTextEditor.Text,
            ////            eventStart,
            ////            eventEnd,
            ////            timeZoneOffset,
            ////            this.EventLocationTextBox.Text,
            ////            this.FeaturedCheckBox.Checked,
            ////            this.AllowRegistrationsCheckBox.Checked,
            ////            this.RecurrenceEditor.GetRecurrenceRule(eventStart, eventEnd),
            ////            this.LimitRegistrationsCheckBox.Checked && this.LimitRegistrationsCheckBox.Visible ? (int?)this.RegistrationLimitTextBox.Value : null,
            ////            this.InDaylightTimeCheckBox.Checked, String.Empty
            ////            );

            ////    appointment.Save(this.UserId);
        ////}

        /// <summary>
        /// Gets a date from the <see cref="HttpRequest.QueryString"/> (handling specific formatting requirements).
        /// </summary>
        /// <param name="parameterName">Name of the <see cref="HttpRequest.QueryString"/> parameter with the date value.</param>
        /// <returns>The date value of the given <paramref name="parameterName"/>, or <c>null</c> if there is not parameter with that name, or the parameter's
        /// value is not in the correct format.</returns>
        private DateTime? GetDateFromQueryString(string parameterName)
        {
            DateTime dateValue;
            if (DateTime.TryParseExact(this.Request.QueryString[parameterName], "yyyy-MM-dd-HH-mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
            {
                return dateValue;
            }

            return null;
        }
    }
}