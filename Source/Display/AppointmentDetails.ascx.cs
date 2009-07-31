// <copyright file="AppointmentDetails.ascx.cs" company="Engage Software">
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
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.Localization;

    /// <summary>
    /// Displays the details of a specific appointment
    /// </summary>
    public partial class AppointmentDetails : ModuleBase
    {
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += this.Page_Load;
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
                if (this.AppointmentId.HasValue)
                {
                    var appointment = Appointment.Load(this.AppointmentId.Value);
                    if (appointment != null && appointment.ModuleId == this.ModuleId)
                    {
                        this.FillAppointmentInformation(appointment);
                    }
                }
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// <summary>
        /// Fills the control with the information about the given <paramref name="appointment"/>.
        /// </summary>
        /// <param name="appointment">The appointment for which to display information.</param>
        private void FillAppointmentInformation(Appointment appointment)
        {
            this.AppointmentTypeLabel.Text = appointment.AppointmentType.Name;
            this.TitleLabel.Text = appointment.Title;
            this.DescriptionLabel.Text = appointment.Description;
            this.NotesLabel.Text = appointment.Notes;
            this.StreetLabel.Text = appointment.Address1;
            this.RoomLabel.Text = appointment.Address2;
            this.CityLabel.Text = appointment.City;
            this.RegionLabel.Text = appointment.RegionName;
            this.PostalCodeLabel.Text = appointment.PostalCode;
            this.AdditionalAddressInfoLabel.Text = appointment.AdditionalAddressInfo;
            this.OnsiteStreetLabel.Text = appointment.ContactStreet;
            this.OnsitePhoneLabel.Text = appointment.ContactPhone;
            this.RequestorNameLabel.Text = appointment.RequestorName;
            this.RequestorPhoneTypeLabel.Text = Localization.GetString(appointment.RequestorPhoneType.ToString(), Utility.LocalSharedResourceFile);
            this.RequestorPhoneLabel.Text = appointment.RequestorPhone;
            this.RequestorAltPhoneTypeLabel.Text = Localization.GetString(appointment.RequestorAltPhoneType.ToString(), Utility.LocalSharedResourceFile);
            this.RequestorAltPhoneLabel.Text = appointment.RequestorAltPhone; 
            this.RequestorEmailLabel.Text = appointment.RequestorEmail;
            this.StartDateTimeLabel.Text = appointment.StartDateTime.ToString("g", CultureInfo.CurrentCulture);
            this.EndDateTimeLabel.Text = appointment.EndDateTime.ToString("g", CultureInfo.CurrentCulture);
            this.NumberOfSpecialParticipantsLabel.Text = appointment.NumberOfSpecialParticipants.ToString(CultureInfo.CurrentCulture);
            this.TotalNumberParticipantsLabel.Text = appointment.NumberOfParticipants.ToString(CultureInfo.CurrentCulture);
            this.GenderLabel.Text = Localization.GetString(appointment.ParticipantGender.ToString(), Utility.LocalSharedResourceFile);
            this.PresenterLabel.Text = Localization.GetString(appointment.IsPresenterSpecial.ToString(), this.LocalResourceFile);
            this.InstructionsLabel.Text = appointment.ParticipantInstructions;
            this.CustomField1Label.Text = appointment.Custom1;
            this.CustomField2Label.Text = appointment.Custom2;
            this.CustomField3Label.Text = appointment.Custom3;
            this.CustomField4Label.Text = appointment.Custom4;
            this.CustomField5Label.Text = appointment.Custom5;
            this.CustomField6Label.Text = appointment.Custom6;
            this.CustomField7Label.Text = appointment.Custom7;
            this.CustomField8Label.Text = appointment.Custom8;
            this.CustomField9Label.Text = appointment.Custom9;
            this.CustomField10Label.Text = appointment.Custom10;
        }
    }
}
