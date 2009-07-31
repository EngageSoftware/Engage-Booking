// <copyright file="AppointmentRequest.ascx.cs" company="Engage Software">
// Engage: Events
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
    using System.Web.UI.WebControls;
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
        /// The default duration of an appointment in minutes
        /// </summary>
        private const int DefaultDurationInMinutes = 30;

        /// <summary>
        /// Gets the URL to navigate to in order to add a new <see cref="Appointment"/>.
        /// </summary>
        /// <value>The URL to navigate to in order to add a new <see cref="Appointment"/></value>
        private string NewAppintmentUrl
        {
            get { return this.BuildLinkUrl(this.ModuleId, ControlKey.AppointmentRequest); }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            this.KickOutUnauthorizedUsers();

            base.OnInit(e);
            this.Load += this.Page_Load;
            this.SaveAppointmentButton.Click += this.SaveAppointmentButton_OnClick;
            this.SaveAndCreateNewAppointmentButton.Click += this.SaveAndCreateNewAppointmentButton_OnClick;
            this.UniqueTimeslotValidator.ServerValidate += this.UniqueTimeslotValidator_ServerValidate;
        }

        /// <summary>
        /// Gets the next time that is a full duration (currently half hour) from now (i.e. if the time is currently 4:13, returns 4:30).
        /// </summary>
        /// <returns>The next full duration from <see cref="DateTime.Now"/></returns>
        private static DateTime GetNextDuration()
        {
            var now = DateTime.Now;
            return now.Subtract(now.TimeOfDay).AddHours(now.Hour).AddMinutes(now.Minute + DefaultDurationInMinutes - (now.Minute % DefaultDurationInMinutes));
        }

        /// <summary>
        /// Verifies that the current user can submit a request for an appointment.  
        /// If not, redirects anonymous users to the login page, and other users to the home page of this module
        /// </summary>
        private void KickOutUnauthorizedUsers()
        {
            if (!this.UserInfo.IsInRole(ModuleSettings.AppointmentRequestsRole.GetValueAsStringFor(this)))
            {
                if (Engage.Utility.IsLoggedIn)
                {
                    this.Response.Redirect(Globals.NavigateURL(), true);
                }
                else
                {
                    this.Response.Redirect(Dnn.Utility.GetLoginUrl(this.PortalSettings, this.Request));
                }
            }
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
                    this.StartDateTimePicker.SelectedDate = this.GetDateFromQueryString("startTime") ?? GetNextDuration();
                    this.EndDateTimePicker.SelectedDate = this.GetDateFromQueryString("endTime") ?? this.StartDateTimePicker.SelectedDate.Value.AddMinutes(DefaultDurationInMinutes);

                    if (this.UserInfo.UserID > 0)
                    {
                        this.RequestorNameTextBox.Text = this.UserInfo.DisplayName;
                        this.RequestorEmailTextBox.Text = this.UserInfo.Email;
                    }

                    this.SetupControl();
                }

                this.SetButtonLinks();
                this.SuccessModuleMessage.Visible = false;
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// <summary>
        /// Handles the <see cref="ImageButton.Click"/> event of the <see cref="SaveAndCreateNewAppointmentButton"/> control.
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
                    this.Response.Redirect(this.NewAppintmentUrl, true);
                }
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// <summary>
        /// Handles the <see cref="ImageButton.Click"/> event of the <see cref="SaveAppointmentButton"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the appointment data.</param>
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
        /// Handles the <see cref="CustomValidator.ServerValidate"/> event of the <see cref="UniqueTimeslotValidator"/> control.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="args">The <see cref="System.Web.UI.WebControls.ServerValidateEventArgs"/> instance containing the event data.</param>
        private void UniqueTimeslotValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!this.StartDateTimePicker.SelectedDate.HasValue || !this.EndDateTimePicker.SelectedDate.HasValue)
            {
                return;
            }

            args.IsValid = Appointment.CanCreateAt(this.ModuleId, this.StartDateTimePicker.SelectedDate.Value, this.EndDateTimePicker.SelectedDate.Value);
        }

        /// <summary>
        /// Displays the final success.
        /// </summary>
        private void DisplayFinalSuccess()
        {
            this.SuccessModuleMessage.Visible = true;
        }

        /// <summary>
        /// Sets the <c>NavigateUrl</c> property for the button links.
        /// </summary>
        private void SetButtonLinks()
        {
            this.CancelAppointmentLink.NavigateUrl = Globals.NavigateURL();
        }

        /// <summary>
        /// Based on the values entered by the user for the event, this method will populate an <see cref="Appointment"/> and save it
        /// </summary>
        private void Insert()
        {
            int appointmentTypeId = int.Parse(this.AppointmentTypeDropDownList.SelectedValue, NumberStyles.Integer, CultureInfo.InvariantCulture);
            int regionId = int.Parse(this.RegionDropDownList.SelectedValue, NumberStyles.Integer, CultureInfo.InvariantCulture);
            int totalNumberOfParticipants; 
            if (!int.TryParse(this.TotalNumberParticipantsTextBox.Text, NumberStyles.Integer, CultureInfo.CurrentCulture, out totalNumberOfParticipants))
            {
                totalNumberOfParticipants = 0;
            }

            int numberOfSpecialParticipants;
            if (!int.TryParse(this.NumberOfSpecialParticipantsTextBox.Text, NumberStyles.Integer, CultureInfo.CurrentCulture, out numberOfSpecialParticipants))
            {
                numberOfSpecialParticipants = 0;
            }

            int timeZoneOffsetMinutes = int.Parse(this.TimeZoneDropDownList.SelectedValue, NumberStyles.Integer, CultureInfo.InvariantCulture);
            TimeSpan timeZoneOffset = new TimeSpan(0, timeZoneOffsetMinutes, 0);
            if (this.InDaylightTimeCheckBox.Checked)
            {
                timeZoneOffset = timeZoneOffset.Add(new TimeSpan(1, 0, 0));
            }

            Appointment appointment = Appointment.Create(
                    this.ModuleId,
                    appointmentTypeId,
                    this.TitleTextBox.Text,
                    this.DescriptionTextBox.Text,
                    this.NotesTextBox.Text,
                    this.StreetTextBox.Text,
                    this.RoomTextBox.Text,
                    this.CityTextBox.Text,
                    regionId,
                    this.PostalCodeTextBox.Text,
                    this.OnsitePhoneTextBox.Text,
                    this.AdditionalAddressInfoTextBox.Text,
                    this.OnsiteStreetTextBox.Text,
                    this.OnsitePhoneTextBox.Text,
                    this.RequestorNameTextBox.Text,
                    Utility.ConvertToEnum(this.RequestorPhoneTypeDropDownList.SelectedValue, PhoneType.Voice),
                    this.RequestorPhoneTextBox.Text,
                    this.RequestorEmailTextBox.Text,
                    Utility.ConvertToEnum(this.RequestorAltPhoneTypeDropDownList.SelectedValue, PhoneType.None),
                    this.RequestorAltPhoneTextBox.Text,
                    this.StartDateTimePicker.SelectedDate.Value,
                    this.EndDateTimePicker.SelectedDate.Value,
                    timeZoneOffset,
                    totalNumberOfParticipants,
                    Utility.ConvertToEnum(this.GenderDropDownList.SelectedValue, GroupGender.MixedGroup),
                    bool.Parse(this.PresenterDropDownList.SelectedValue),
                    this.InstructionsTextBox.Text,
                    numberOfSpecialParticipants,
                    this.CustomField1TextBox.Text,
                    this.CustomField2TextBox.Text,
                    this.CustomField3TextBox.Text,
                    this.CustomField4TextBox.Text,
                    this.CustomField5TextBox.Text,
                    this.CustomField6TextBox.Text,
                    this.CustomField7TextBox.Text,
                    this.CustomField8TextBox.Text,
                    this.CustomField9TextBox.Text,
                    this.CustomField10TextBox.Text,
                    null);

            appointment.Save(this.UserId);

            EmailService.SendNewRequestEmail(
                    appointment,
                    ModuleSettings.NotificationEmailAddresses.GetValueAsStringFor(this),
                    this.BuildLinkUrl(this.ModuleId, ControlKey.DirectApproval, "actionKey=" + appointment.AcceptKey),
                    this.BuildLinkUrl(this.ModuleId, ControlKey.DirectApproval, "actionKey=" + appointment.DeclineKey),
                    this.BuildLinkUrl(this.ModuleId, ControlKey.Approval));
        }

        /// <summary>
        /// This method will either update or create an <see cref="Appointment"/>
        /// </summary>
        private void Save()
        {
            ////if (this.AppointmentId.HasValue)
            ////{
            ////    this.Update();
            ////}
            ////else
            ////{
            this.Insert();
            ////}
        }

        /// <summary>
        /// Fills the list controls on the form.
        /// </summary>
        private void SetupControl()
        {
            this.RequestorEmailFormatValidator.ValidationExpression = Engage.Utility.EmailRegEx;

            this.StartDateTimePicker.Skin = this.EndDateTimePicker.Skin = ModuleSettings.CalendarSkin.GetValueAsStringFor(this);
            Utility.LocalizeDateTimePicker(this.StartDateTimePicker);
            Utility.LocalizeDateTimePicker(this.EndDateTimePicker);

            this.RegionDropDownList.DataTextField = "Text";
            this.RegionDropDownList.DataValueField = "EntryId";
            this.RegionDropDownList.DataSource = new ListController().GetListEntryInfoCollection("Region");
            this.RegionDropDownList.DataBind();

            this.AppointmentTypeDropDownList.DataSource = AppointmentTypeCollection.Load();
            this.AppointmentTypeDropDownList.DataTextField = "Name";
            this.AppointmentTypeDropDownList.DataValueField = "Id";
            this.AppointmentTypeDropDownList.DataBind();
            Dnn.Utility.LocalizeListControl(this.AppointmentTypeDropDownList, this.LocalResourceFile);

            this.RequestorPhoneTypeDropDownList.DataSource = Enum.GetNames(typeof(PhoneType));
            this.RequestorPhoneTypeDropDownList.DataBind();
            this.RequestorPhoneTypeDropDownList.Items.Remove(PhoneType.None.ToString());
            Dnn.Utility.LocalizeListControl(this.RequestorPhoneTypeDropDownList, Utility.LocalSharedResourceFile);

            this.RequestorAltPhoneTypeDropDownList.DataSource = Enum.GetNames(typeof(PhoneType));
            this.RequestorAltPhoneTypeDropDownList.DataBind();
            this.RequestorAltPhoneTypeDropDownList.SelectedValue = PhoneType.None.ToString();
            Dnn.Utility.LocalizeListControl(this.RequestorAltPhoneTypeDropDownList, Utility.LocalSharedResourceFile);

            this.GenderDropDownList.DataSource = Enum.GetNames(typeof(GroupGender));
            this.GenderDropDownList.DataBind();
            Dnn.Utility.LocalizeListControl(this.GenderDropDownList, Utility.LocalSharedResourceFile);

            this.PresenterDropDownList.Items.Clear();
            this.PresenterDropDownList.Items.Add(new ListItem(Localization.GetString(true.ToString(CultureInfo.InvariantCulture), this.LocalResourceFile), true.ToString(CultureInfo.InvariantCulture)));
            this.PresenterDropDownList.Items.Add(new ListItem(Localization.GetString(false.ToString(CultureInfo.InvariantCulture), this.LocalResourceFile), false.ToString(CultureInfo.InvariantCulture)));

            // TODO: Once we support .NET 3.5, replace this with TimeZoneInfo.GetSystemTimeZones
            Localization.LoadTimeZoneDropDownList(
                    this.TimeZoneDropDownList,
                    CultureInfo.CurrentCulture.Name,
                    ((int)Dnn.Utility.GetUserTimeZoneOffset(this.UserInfo, this.PortalSettings).TotalMinutes).ToString(CultureInfo.InvariantCulture));
        }

        /////// <summary>
        /////// This method is responsible for updating an <see cref="Appointment"/> which already exists.
        /////// </summary>
        ////private void Update()
        ////{
        ////    int? appointmentId = this.AppointmentId;
        ////    if (appointmentId.HasValue)
        ////    {
        ////        int appointmentTypeId = int.Parse(this.AppointmentTypeDropDownList.SelectedValue, NumberStyles.Integer, CultureInfo.InvariantCulture);
        ////        int regionId = int.Parse(this.RegionDropDownList.SelectedValue, NumberStyles.Integer, CultureInfo.InvariantCulture);
        ////        int total = int.Parse(this.TotalNumberParticipantsTextBox.Text, NumberStyles.Integer, CultureInfo.CurrentCulture);
        ////        int special = int.Parse(this.NumberOfSpecialParticipantsTextBox.Text, NumberStyles.Integer, CultureInfo.CurrentCulture);
        ////        int timeZoneOffsetMinutes = int.Parse(this.TimeZoneDropDownList.SelectedValue, NumberStyles.Integer, CultureInfo.InvariantCulture);
        ////        TimeSpan timeZoneOffset = new TimeSpan(0, timeZoneOffsetMinutes, 0);
        ////        if (this.InDaylightTimeCheckBox.Checked)
        ////        {
        ////            timeZoneOffset = timeZoneOffset.Add(new TimeSpan(1, 0, 0));
        ////        }

        ////        Appointment appointment = Appointment.Load(appointmentId.Value);
        ////        appointment.StartDateTime = this.StartDateTimePicker.SelectedDate.Value;
        ////        appointment.EndDateTime = this.EndDateTimePicker.SelectedDate.Value;
        ////        appointment.Title = this.TitleTextBox.Text;
        ////        appointment.Description = this.DescriptionTextBox.Text;
        ////        ////...
        ////        appointment.Save(this.UserId);
        ////    }
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
