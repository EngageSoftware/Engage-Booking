// <copyright file="Settings.ascx.cs" company="Engage Software">
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
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using DotNetNuke.Common;

    using DotNetNuke.Security.Roles;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.Localization;

    /// <summary>
    /// This is the settings code behind for Event related settings.
    /// </summary>
    public partial class Settings : SettingsBase
    {
        /// <summary>
        /// Loads the settings.
        /// </summary>
        public override void LoadSettings()
        {
            base.LoadSettings();
            try
            {
                if (!this.IsPostBack)
                {
                    this.NotificationEmailsListValidator.ValidationExpression = Engage.Utility.EmailsRegEx;
                    this.SetOptions();
                }
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// <summary>
        /// Updates the settings.
        /// </summary>
        public override void UpdateSettings()
        {
            if (this.Page.IsValid)
            {
                try
                {
                    Booking.ModuleSettings.AppointmentRequestsRole.Set(this, this.AppointmentRequestsRoleDropDownList.SelectedValue);
                    Booking.ModuleSettings.CalendarSkin.Set(this, this.SkinDropDownList.SelectedValue);
                    Booking.ModuleSettings.AppointmentsPerPage.Set(this, (int)this.RecordsPerPageTextBox.Value.Value);
                    Booking.ModuleSettings.NotificationEmailAddresses.Set(this, this.NotificationEmailsListTextBox.Text);

                    var defaultAppointmentDuration = new TimeSpan(
                        (int)this.DefaultAppointmentDurationHoursTextBox.Value.Value,
                        (int)this.DefaultAppointmentDurationMinutesTextBox.Value.Value,
                        0);
                    Booking.ModuleSettings.DefaultAppointmentDuration.Set(this, defaultAppointmentDuration.TotalMinutes);

                    var minimumAppointmentDuration = new TimeSpan(
                        (int)this.MinimumAppointmentDurationHoursTextBox.Value.Value,
                        (int)this.MinimumAppointmentDurationMinutesTextBox.Value.Value,
                        0);
                    Booking.ModuleSettings.MinimumAppointmentDuration.Set(this, minimumAppointmentDuration.TotalMinutes);

                    var maximumAppointmentDuration = new TimeSpan(
                        (int)this.MaximumAppointmentDurationHoursTextBox.Value.Value,
                        (int)this.MaximumAppointmentDurationMinutesTextBox.Value.Value,
                        0);
                    Booking.ModuleSettings.MaximumAppointmentDuration.Set(this, maximumAppointmentDuration.TotalMinutes);

                    if (this.AppointmentsPerDayTextBox.Value.HasValue)
                    {
                        Booking.ModuleSettings.AppointmentsToDisplayPerDay.Set(this, (int)this.AppointmentsPerDayTextBox.Value.Value);
                    }
                }
                catch (Exception exc)
                {
                    Exceptions.ProcessModuleLoadException(this, exc);
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="Control.Init"/> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.DefaultAppointmentDurationValidator.ServerValidate += this.DefaultAppointmentDurationValidatator_ServerValidate;
            this.MinimumAppointmentDurationValidator.ServerValidate += this.MinimumAppointmentDurationValidator_ServerValidate;
            this.MaximumAppointmentDurationValidator.ServerValidate += this.MaximumAppointmentDurationValidator_ServerValidate;
            this.DurationCompareValidator.ServerValidate += this.DurationCompareValidator_ServerValidate;
        }

        /// <summary>
        /// Determines whether either value is non-null and greater than zero.
        /// </summary>
        /// <param name="value1">The first value.</param>
        /// <param name="value2">The second value.</param>
        /// <returns><c>true</c> if either <paramref name="value1"/> or <paramref name="value2"/> has a positive value; otherwise <c>false</c></returns>
        private static bool EitherHasValue(double? value1, double? value2)
        {
            return value1 > 0 || value2 > 0;
        }

        /// <summary>
        /// Validates that the maximum and minimum values are respectively greater and less then one another.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="args">The <see cref="ServerValidateEventArgs"/> instance containing the event data.</param>
        private void DurationCompareValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var minDuration =
                new TimeSpan(
                    (int)this.MinimumAppointmentDurationHoursTextBox.Value.Value, (int)this.MinimumAppointmentDurationMinutesTextBox.Value.Value, 0).
                    TotalMinutes;

            var maxDuration =
                new TimeSpan(
                    (int)this.MaximumAppointmentDurationHoursTextBox.Value.Value, (int)this.MaximumAppointmentDurationMinutesTextBox.Value.Value, 0).
                    TotalMinutes;

            args.IsValid = minDuration <= maxDuration;
        }

        /// <summary>
        /// Validates that the maximum appointment length has a value.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="args">The <see cref="ServerValidateEventArgs"/> instance containing the event data.</param>
        private void MaximumAppointmentDurationValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = EitherHasValue(this.MaximumAppointmentDurationHoursTextBox.Value, this.MaximumAppointmentDurationMinutesTextBox.Value);
        }

        /// <summary>
        /// Validates that the minimum appointment length has a value.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="args">The <see cref="ServerValidateEventArgs"/> instance containing the event data.</param>
        private void MinimumAppointmentDurationValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = EitherHasValue(this.MinimumAppointmentDurationHoursTextBox.Value, this.MinimumAppointmentDurationMinutesTextBox.Value);
        }

        /// <summary>
        /// Validates that the default appointment duration has a value.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="args">The <see cref="ServerValidateEventArgs"/> instance containing the event data.</param>
        private void DefaultAppointmentDurationValidatator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = EitherHasValue(this.DefaultAppointmentDurationHoursTextBox.Value, this.DefaultAppointmentDurationMinutesTextBox.Value);
        }

        /// <summary>
        /// Sets the options.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1302:DoNotHardcodeLocaleSpecificStrings", MessageId = "All Users", Justification = "'All Users' (glbRoleAllUsersName) does not refer to the system folder")]
        private void SetOptions()
        {
            this.SkinDropDownList.DataSource = Enum.GetNames(typeof(TelerikSkin));
            this.SkinDropDownList.DataBind();
            Dnn.Utility.LocalizeListControl(this.SkinDropDownList, this.LocalResourceFile);

            var li = this.SkinDropDownList.Items.FindByValue(Booking.ModuleSettings.CalendarSkin.GetValueAsStringFor(this));
            if (li != null)
            {
                li.Selected = true;
            }

            this.AppointmentRequestsRoleDropDownList.DataSource = new RoleController().GetRoleNames(this.PortalId);
            this.AppointmentRequestsRoleDropDownList.DataBind();
            this.AppointmentRequestsRoleDropDownList.Items.Insert(0, new ListItem(Localization.GetString("AllUsers.Text", this.LocalSharedResourceFile), Globals.glbRoleAllUsersName));

            li = this.AppointmentRequestsRoleDropDownList.Items.FindByValue(Booking.ModuleSettings.AppointmentRequestsRole.GetValueAsStringFor(this));
            if (li != null)
            {
                li.Selected = true;
            }

            this.AppointmentsPerDayTextBox.Value = Booking.ModuleSettings.AppointmentsToDisplayPerDay.GetValueAsInt32For(this).Value;
            this.RecordsPerPageTextBox.Value = Booking.ModuleSettings.AppointmentsPerPage.GetValueAsInt32For(this).Value;
            this.NotificationEmailsListTextBox.Text = Booking.ModuleSettings.NotificationEmailAddresses.GetValueAsStringFor(this);

            var defaultAppointmentDuration = TimeSpan.FromMinutes(Booking.ModuleSettings.DefaultAppointmentDuration.GetValueAsInt32For(this).Value);
            this.DefaultAppointmentDurationHoursTextBox.Value = defaultAppointmentDuration.Hours;
            this.DefaultAppointmentDurationMinutesTextBox.Value = defaultAppointmentDuration.Minutes;

            var minimumAppointmentDuration = TimeSpan.FromMinutes(Booking.ModuleSettings.MinimumAppointmentDuration.GetValueAsInt32For(this).Value);
            this.MinimumAppointmentDurationHoursTextBox.Value = minimumAppointmentDuration.Hours;
            this.MinimumAppointmentDurationMinutesTextBox.Value = minimumAppointmentDuration.Minutes;

            var maximumAppointmentDuration = TimeSpan.FromMinutes(Booking.ModuleSettings.MaximumAppointmentDuration.GetValueAsInt32For(this).Value);
            this.MaximumAppointmentDurationHoursTextBox.Value = maximumAppointmentDuration.Hours;
            this.MaximumAppointmentDurationMinutesTextBox.Value = maximumAppointmentDuration.Minutes;
        }
    }
}