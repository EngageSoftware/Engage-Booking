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
    using System.Web.UI.WebControls;
    using DotNetNuke.Services.Exceptions;

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
                    Booking.ModuleSettings.AllowAppointmentRequests.Set(this, this.AllowAppointmentRequestsCheckBox.Checked);
                    Booking.ModuleSettings.CalendarSkin.Set(this, this.SkinDropDownList.SelectedValue);
                    Booking.ModuleSettings.AppointmentsPerPage.Set(this, (int)this.RecordsPerPageTextBox.Value.Value);
                    Booking.ModuleSettings.NotificationEmailAddresses.Set(this, this.NotificationEmailsListTextBox.Text);

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
        /// Sets the options.
        /// </summary>
        private void SetOptions()
        {
            this.SkinDropDownList.DataSource = Enum.GetNames(typeof(TelerikSkin));
            this.SkinDropDownList.DataBind();
            Dnn.Utility.LocalizeListControl(this.SkinDropDownList, this.LocalResourceFile);

            ListItem li = this.SkinDropDownList.Items.FindByValue(Booking.ModuleSettings.CalendarSkin.GetValueAsStringFor(this));
            if (li != null)
            {
                li.Selected = true;
            }

            this.AllowAppointmentRequestsCheckBox.Checked = Booking.ModuleSettings.AllowAppointmentRequests.GetValueAsBooleanFor(this).Value;
            this.AppointmentsPerDayTextBox.Value = Booking.ModuleSettings.AppointmentsToDisplayPerDay.GetValueAsInt32For(this).Value;
            this.RecordsPerPageTextBox.Value = Booking.ModuleSettings.AppointmentsPerPage.GetValueAsInt32For(this).Value;
            this.NotificationEmailsListTextBox.Text = Booking.ModuleSettings.NotificationEmailAddresses.GetValueAsStringFor(this);
        }
    }
}