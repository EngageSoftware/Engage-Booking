// <copyright file="Approval.ascx.cs" company="Engage Software">
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
    using System.Web.UI.WebControls;
    using Booking;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.Localization;

    /// <summary>
    /// Control to display the events calendar view
    /// </summary>
    public partial class Approval : ModuleBase
    {
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += this.Page_Load;
            this.AppointmentsGrid.SelectedIndexChanging += this.AppointmentsGrid_SelectedIndexChanging;
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
                this.SetupSelectAllPlugin();
                this.BindData();
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// <summary>
        /// Handles the <see cref="GridView.SelectedIndexChanging"/> event of the <see cref="AppointmentsGrid"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewSelectEventArgs"/> instance containing the event data.</param>
        private void AppointmentsGrid_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            var appointmentId = this.GetAppointmentIdFromRowIndex(e.NewSelectedIndex);
            if (appointmentId == null)
            {
                return;
            }

            var appointment = Appointment.Load(appointmentId.Value);
            if (appointment == null)
            {
                return;
            }

            this.AppointmentDetailsPlaceHolder.Visible = true;
            this.DetailDateAndTimeLabel.Text = string.Format(CultureInfo.CurrentCulture, "{0:ddd, MMM dd, yyyy} at {0:h:mm tt}", appointment.StartDateTime);
            this.DetailFullNameLabel.Text = appointment.RequestorName;
            this.DetailPhoneTypeLabel.Text = appointment.RequestorPhoneType;
            this.DetailPhoneNumberLabel.Text = appointment.RequestorPhone;
            this.DetailNumberOfParticipantsLabel.Text = this.GetNumberOfParticipantsText(appointment);
            ////this.DetailNamesLabel.Text = 
        }

        private int? GetAppointmentIdFromRowIndex(int rowIndex)
        {
            int appointmentId;
            var selectLink = (LinkButton)this.AppointmentsGrid.Rows[rowIndex].FindControl("SelectLink");
            if (int.TryParse(selectLink.CommandArgument, NumberStyles.Integer, CultureInfo.InvariantCulture, out appointmentId))
            {
                return appointmentId;
            }

            return null;
        }

        private string GetNumberOfParticipantsText(Appointment appointment)
        {
            return string.Format(
                    CultureInfo.CurrentCulture,
                    Localization.GetString("NumberOfParticipants.Format.Text", this.LocalResourceFile),
                    appointment.NumberOfParticipants,
                    appointment.NumberOfSpecialParticipants);
        }

        /// <summary>
        /// Sets up the Select All jQuery plugin (to allow the header checkbox to select all other checkboxes).
        /// </summary>
        private void SetupSelectAllPlugin()
        {
            const string SelectAllCheckBoxCssClass = "header-checkbox";
            const string CheckBoxesCssClass = "select-checkbox";
            string initScriptKey = string.Format("SelectAllPlugin {0} : {1}", SelectAllCheckBoxCssClass, CheckBoxesCssClass);

            this.AddJQueryReference();
            this.Page.ClientScript.RegisterClientScriptResource(typeof(Approval), "Engage.Dnn.Booking.JavaScript.SelectAllPlugin.js");
            this.Page.ClientScript.RegisterStartupScript(
                    typeof(Approval),
                    initScriptKey,
                    "jQuery(function($) { $('." + SelectAllCheckBoxCssClass + "').selectAll($('." + CheckBoxesCssClass + "')); });",
                    true);
        }

        /// <summary>
        /// Binds the data.
        /// </summary>
        private void BindData()
        {
            var appointments = AppointmentCollection.Load(this.ModuleId, null, this.CurrentPageIndex - 1, this.PagingControl.PageSize);
            this.AppointmentsGrid.DataSource = appointments;
            this.AppointmentsGrid.DataBind();

            this.SetupPagingControl(this.PagingControl, appointments.TotalRecords);
        }
    }
}