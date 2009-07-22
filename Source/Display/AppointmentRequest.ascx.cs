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

namespace Engage.Dnn.Booking
{
    using System;
    using System.Globalization;
    using System.Web;
    using DotNetNuke.Common;
    using DotNetNuke.Common.Lists;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.Localization;

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
            this.SaveAppointmentButton.Click += this.SaveAppointmentButton_OnClick;
            this.SaveAndCreateNewAppointmentButton.Click += SaveAndCreateNewAppointmentButton_OnClick;
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
                    this.SetupControl();
                    //// this.BindData();
                }

                this.SetButtonLinks();
                this.SuccessModuleMessage.Visible = false;
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        private void SetupControl()
        {
            ListController controller = new ListController();
            ListEntryInfoCollection regions = controller.GetListEntryInfoCollection("Region");

            StateDropDownList.DataTextField = "Text";
            StateDropDownList.DataValueField = "EntryId";
            StateDropDownList.DataSource = regions;
            StateDropDownList.DataBind();

            AppointmentTypeDropDownList.DataSource = AppointmentTypeCollection.Load();
            AppointmentTypeDropDownList.DataTextField = "Name";
            AppointmentTypeDropDownList.DataValueField = "Id";
            AppointmentTypeDropDownList.DataBind();
            
            // TODO: Once we support .NET 3.5, replace this with TimeZoneInfo.GetSystemTimeZones
            Localization.LoadTimeZoneDropDownList(this.TimeZoneDropDownList, CultureInfo.CurrentCulture.Name, ((int)Dnn.Utility.GetUserTimeZoneOffset(this.UserInfo, this.PortalSettings).TotalMinutes).ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Gets the URL to navigate to in order to add a new event.
        /// </summary>
        /// <value>The URL to navigate to in order to add a new event</value>
        private string AddAppintmentUrl
        {
            get { return this.BuildLinkUrl(this.ModuleId, "AppointmentRequest"); }
        }

        /// <summary>
        /// Sets the <c>NavigateUrl</c> property for the button links.
        /// </summary>
        private void SetButtonLinks()
        {
            //this.CancelAppointmentLink.NavigateUrl = this.CancelEventLink.NavigateUrl = Globals.NavigateURL();
            //this.SaveCreateAnotherEventLink.NavigateUrl = this.AddAppintmentUrl;
        }


        ///// <summary>
        ///// Handles the OnClick event of the SaveAppointmentButton control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="System.EventArgs"/> instance containing the appointment data.</param>
        private void SaveAppointmentButton_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (this.Page.IsValid)
                {
                    this.Save();
                    this.DisplayFinalSuccess();
                }
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// <summary>
        /// Handles the OnClick event of the SaveAndCreateNewAppointmentButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void SaveAndCreateNewAppointmentButton_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (this.Page.IsValid)
                {
                    this.Save();
                    this.Response.Redirect(this.AddAppintmentUrl, true);
                }
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        protected void CancelAppointmentButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect(Globals.NavigateURL());
        }

        ///// <summary>
        ///// Displays the final success.
        ///// </summary>
        private void DisplayFinalSuccess()
        {
            this.SuccessModuleMessage.Visible = true;
            ////this.AddNewEvent.Visible = false;
            ////this.FooterMultiview.SetActiveView(this.FinalFooterView);
        }

        ///// <summary>
        ///// This method will either update or create an event based on the current context of EventId
        ///// </summary>
        private void Save()
        {
            if (this.AppointmentId.HasValue)
            {
                this.Update();
            }
            else
            {
                this.Insert();
            }
        }

        ///// <summary>
        ///// This method is responsible for updating an event which already exists in the data store.
        ///// </summary>
        private void Update()
        {
            int timeZoneOffsetMinutes;
            int? appointmentId = this.AppointmentId;
            if (appointmentId.HasValue && int.TryParse(this.TimeZoneDropDownList.SelectedValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out timeZoneOffsetMinutes))
            {
                TimeSpan timeZoneOffset = new TimeSpan(0, timeZoneOffsetMinutes, 0);
                if (this.InDaylightTimeCheckBox.Checked)
                {
                    timeZoneOffset = timeZoneOffset.Add(new TimeSpan(1, 0, 0));
                }

                Appointment appointment = Appointment.Load(appointmentId.Value);
                appointment.StartDateTime = this.StartDateTimePicker.SelectedDate.Value;
                appointment.EndDateTime = this.EndDateTimePicker.SelectedDate.Value;
                appointment.Title = this.TitleTextBox.Text;
                appointment.Description = this.DescriptionTextBox.Text;
                ////...
                appointment.Save(this.UserId);
            }
        }

        ///// <summary>
        ///// Based on the values entered by the user for the event, this method will populate an event object and call the Event object's save method.
        ///// </summary>
        private void Insert()
        {
            int timeZoneOffsetMinutes;
            if (int.TryParse(this.TimeZoneDropDownList.SelectedValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out timeZoneOffsetMinutes))
            {
                TimeSpan timeZoneOffset = new TimeSpan(0, timeZoneOffsetMinutes, 0);
                if (this.InDaylightTimeCheckBox.Checked)
                {
                    timeZoneOffset = timeZoneOffset.Add(new TimeSpan(1, 0, 0));
                }

                DateTime eventStart = this.StartDateTimePicker.SelectedDate.Value;
                DateTime eventEnd = this.EndDateTimePicker.SelectedDate.Value;
                int appointmentTypeId = Convert.ToInt32(this.AppointmentTypeDropDownList.SelectedValue);
                int regionId = Convert.ToInt32(this.StateDropDownList.SelectedValue);
                int total = Convert.ToInt32(TotalNumberParticipantsTextBox.Text);
                int special = Convert.ToInt32(TotalNumberParticipantsTextBox.Text);

                Appointment appointment = Appointment.Create(
                        this.ModuleId, appointmentTypeId, this.TitleTextBox.Text, DescriptionTextBox.Text,
                        this.NotesTextBox.Text, StreetTextBox.Text, RoomTextBox.Text, CityTextBox.Text,
                        regionId, this.ZipTextBox.Text, this.OnSitePhoneTextBox.Text, AdditionaAddressInfoTextBox.Text,
                        this.OnSiteStreetTextBox.Text, OnSitePhoneTextBox.Text, RequestorNameTextBox.Text, RequestorPhoneDropDownList.SelectedValue,
                        RequestorPhoneTextBox.Text,
                        RequestorEmailTextBox.Text, this.RequestorAltPhoneDropDownList.SelectedValue, this.RequestorAltPhoneDropDownList.Text,
                        eventStart, eventEnd, timeZoneOffset, total,
                        GenderDropDownList.SelectedValue,
                        Convert.ToChar(PresenterDropDownList.SelectedValue), InstructionsTextBox.Text,
                        special, false);

                appointment.Save(this.UserId);
            }
        }

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
            if (parameterName == "startTime")
            {
                return DateTime.Now;
            }
            return DateTime.Now.AddMinutes(30);
        }
    }
}
