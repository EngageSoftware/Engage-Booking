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

namespace Engage.Dnn.Booking
{
    using System;
    using System.Globalization;
    using System.Web.UI.WebControls;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.Localization;

    /// <summary>
    /// Displays a list of the <see cref="Appointment"/>s waiting to be approved
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
            this.AppointmentsGrid.RowCommand += this.AppointmentsGrid_RowCommand;
            this.AppointmentsGrid.SelectedIndexChanging += this.AppointmentsGrid_SelectedIndexChanging;
        }

        /// <summary>
        /// Localizes the <see cref="AppointmentsGrid"/>.
        /// </summary>
        private void LocalizeGrid()
        {
            if (!this.IsPostBack)
            {
                Localization.LocalizeGridView(ref this.AppointmentsGrid, this.LocalResourceFile);

                var startDateField = this.AppointmentsGrid.Columns[3] as BoundField;
                if (startDateField != null)
                {
                    startDateField.DataFormatString = Localization.GetString("DateAndTime.Format.Text", this.LocalResourceFile);
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
                this.SetupSelectAllPlugin();
                this.LocalizeGrid();
                this.BindData();
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// <summary>
        /// Handles the <see cref="GridView.RowCommand"/> event of the <see cref="AppointmentsGrid"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCommandEventArgs"/> instance containing the event data.</param>
        private void AppointmentsGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int appointmentId = int.Parse((string)e.CommandArgument, NumberStyles.Integer, CultureInfo.InvariantCulture);
            switch (e.CommandName)
            {
                case "Accept":
                    Appointment.Accept(appointmentId, this.UserId);
                    break;
                case "Decline":
                    Appointment.Decline(appointmentId, this.UserId);
                    break;
            }

            this.BindData();
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
            this.FillDetailSection(appointment);
        }

        /// <summary>
        /// Fills the controls in <see cref="AppointmentDetailsPlaceHolder"/> with the information about the given <paramref name="appointment"/>.
        /// </summary>
        /// <param name="appointment">The appointment.</param>
        private void FillDetailSection(Appointment appointment)
        {
            this.DetailDateAndTimeLabel.Text = string.Format(
                    CultureInfo.CurrentCulture,
                    Localization.GetString("DetailDateAndTime.Format.Text", this.LocalResourceFile),
                    appointment.StartDateTime,
                    appointment.EndDateTime);
            this.DetailFullNameLabel.Text = appointment.RequestorName;
            this.DetailPhoneTypeLabel.Text = appointment.RequestorPhoneType;
            this.DetailPhoneNumberLabel.Text = appointment.RequestorPhone;
            this.DetailNumberOfParticipantsLabel.Text = this.GetNumberOfParticipantsText(appointment);
            ////this.DetailNamesLabel.Text = 
        }

        /// <summary>
        /// Gets the ID of the <see cref="Appointment"/> represented by the row in the <see cref="AppointmentsGrid"/> with the given <paramref name="rowIndex"/>.
        /// </summary>
        /// <param name="rowIndex">Index of the row in the <see cref="AppointmentsGrid"/>.</param>
        /// <returns>The Appointment ID</returns>
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

        /// <summary>
        /// Gets the text for <see cref="DetailNumberOfParticipantsLabel"/>.
        /// </summary>
        /// <param name="appointment">The appointment.</param>
        /// <returns>A formatted string displaying the number of participants compared with the number of special participants</returns>
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
            string initScriptKey = string.Format(CultureInfo.InvariantCulture, "SelectAllPlugin {0} : {1}", SelectAllCheckBoxCssClass, CheckBoxesCssClass);

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
            this.PagingControl.PageSize = ModuleSettings.AppointmentsPerPage.GetValueAsInt32For(this).Value;

            var appointments = AppointmentCollection.Load(this.ModuleId, null, null, this.CurrentPageIndex - 1, this.PagingControl.PageSize);
            this.AppointmentsGrid.DataSource = appointments;
            this.AppointmentsGrid.DataBind();

            this.SetupPagingControl(this.PagingControl, appointments.TotalRecords);
            if (this.AppointmentsGrid.HeaderRow != null)
            {
                this.AppointmentsGrid.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
    }
}