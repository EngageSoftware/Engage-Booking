// <copyright file="Approval.ascx.cs" company="Engage Software">
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
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using System.Web.UI;
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
            this.AcceptAppointmentsButton.Click += this.AcceptAppointmentsButton_Click;
            this.DeclineAppointmentsButton.Click += this.DeclineAppointmentsButton_Click;
            this.DeclineReasonRepeater.ItemDataBound += this.DeclineReasonRepeater_ItemDataBound;
        }

        /// <summary>
        /// Handles the <see cref="LinkButton.Click"/> event of the <c>CancelDeclineButton</c> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void CancelDeclineButton_Click(object sender, EventArgs e)
        {
            this.BindData(true);
            this.ApprovalMultiview.SetActiveView(this.ApprovalsListView);
        }

        /// <summary>
        /// Handles the <see cref="LinkButton.Click"/> event of the <c>SubmitDeclineReasonButton</c> control in the footer of the <see cref="DeclineReasonRepeater"/>.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void SubmitDeclineReasonButton_Click(object sender, EventArgs e)
        {
            var declinedAppointments = new List<Appointment>();
            foreach (RepeaterItem repeaterItem in this.DeclineReasonRepeater.Items)
            {
                var declinedAppointmentIdHiddenField = (HiddenField)repeaterItem.FindControl("DeclinedAppointmentIdHiddenField");
                var declineReasonTextBox = (TextBox)repeaterItem.FindControl("DeclineReasonTextBox");

                int appointmentId = int.Parse(declinedAppointmentIdHiddenField.Value, NumberStyles.Integer, CultureInfo.InvariantCulture);
                var appointment = Appointment.Load(appointmentId);
                if (appointment != null)
                {
                    appointment.Decline(this.UserId);
                    declinedAppointments.Add(appointment);
                    EmailService.SendDeclineEmail(appointment, declineReasonTextBox.Text);
                }
            }

            this.ApprovalMessage.Visible = true;
            this.ApprovalMessage.Text = GenerateAppointmentApprovalMessage(declinedAppointments, "DeclinedAppointments.Text");

            this.BindData(true);
            this.ApprovalMultiview.SetActiveView(this.ApprovalsListView);
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
                this.BindData(false);
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
                    if (!this.AcceptAppointment(appointmentId))
                    {
                        this.ConflictingAppointmentsMessage.Visible = true;
                        this.ConflictingAppointmentsMessage.TextResourceKey = "ConflictAcceptingAppointment.Text";
                    }
                    else
                    {
                        this.ApprovalMessage.Visible = true;
                        this.ApprovalMessage.TextResourceKey = "SuccessfulAccept.Text";
                    }

                    break;
                case "Decline":
                    this.ApprovalMultiview.SetActiveView(this.ProvideDeclineReasonView);
                    this.DeclineReasonRepeater.DataSource = new[] { Appointment.Load(appointmentId) };
                    this.DeclineReasonRepeater.DataBind();
                    break;
            }

            this.BindData(true);
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

            this.AppointmentDetailsPlaceholder.Visible = true;
            this.FillDetailSection(appointment);
        }

        /// <summary>
        /// Handles the <see cref="Button.Click"/> event of the <see cref="AcceptAppointmentsButton"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void AcceptAppointmentsButton_Click(object sender, EventArgs e)
        {
            var conflictingAppointments = new List<Appointment>();
            var acceptedAppointments = new List<Appointment>();
            var appointmentIds = this.GetSelectedAppointmentIds();
            foreach (var appointmentId in appointmentIds)
            {
                var appointment = Appointment.Load(appointmentId);
                if (appointment != null)
                {
                    if (appointment.Accept(this.UserId))
                    {
                        EmailService.SendAcceptanceEmail(appointment);
                        acceptedAppointments.Add(appointment);
                    }
                    else
                    {
                        conflictingAppointments.Add(appointment);
                    }
                }

                if (acceptedAppointments.Count > 0)
                {
                    this.ApprovalMessage.Visible = true;
                    this.ApprovalMessage.Text = this.GenerateAppointmentApprovalMessage(acceptedAppointments, "AcceptedAppointments.Text");
                }
            }

            if (conflictingAppointments.Count > 0)
            {
                this.ConflictingAppointmentsMessage.Visible = true;
                this.ConflictingAppointmentsMessage.Text = this.GenerateAppointmentApprovalMessage(conflictingAppointments, "ConflictAcceptingAppointments.Text");
            }

            this.BindData(true);
        }

        /// <summary>
        /// Generates the success message when appointments are accepted.
        /// </summary>
        /// <param name="acceptedAppointments">The accepted appointments.</param>
        /// <returns>The success message</returns>
        private string GenerateAppointmentApprovalMessage(IEnumerable<Appointment> acceptedAppointments, string headerTextLocalizationKey)
        {
            StringBuilder successMessageBuilder = new StringBuilder(Localization.GetString(headerTextLocalizationKey, this.LocalResourceFile)).Append("<ul>");
            foreach (var appointment in acceptedAppointments)
            {
                successMessageBuilder.Append("<li>").Append(appointment.Title).Append("</li>");
            }

            return successMessageBuilder.Append("</ul>").ToString();
        }

        /// <summary>
        /// Handles the <see cref="Button.Click"/> event of the <see cref="DeclineAppointmentsButton"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DeclineAppointmentsButton_Click(object sender, EventArgs e)
        {
            this.ApprovalMultiview.SetActiveView(this.ProvideDeclineReasonView);
            List<Appointment> selectedAppointments = this.GetSelectedAppointmentIds().ConvertAll(appointmentId => Appointment.Load(appointmentId));
            this.DeclineReasonRepeater.DataSource = selectedAppointments;
            this.DeclineReasonRepeater.DataBind();
        }

        /// <summary>
        /// Handles the <see cref="Repeater.ItemDataBound"/> event of the <see cref="DeclineReasonRepeater"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.RepeaterItemEventArgs"/> instance containing the event data.</param>
        private void DeclineReasonRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var declineReasonTextBox = (TextBox)e.Item.FindControl("DeclineReasonTextBox");
                declineReasonTextBox.Text = Localization.GetString("Default Reason for Decline", this.LocalResourceFile);
            }
        }

        /// <summary>
        /// Accepts the <see cref="Appointment"/> with the given <paramref name="appointmentId"/>,
        /// and sends an email to the requestor of the <see cref="Appointment"/>.
        /// </summary>
        /// <param name="appointmentId">The ID of the appointment to accept.</param>
        /// <returns>Whether the appointment was able to be successfully accepted</returns>
        private bool AcceptAppointment(int appointmentId)
        {
            var appointment = Appointment.Load(appointmentId);
            if (appointment != null)
            {
                if (appointment.Accept(this.UserId))
                {
                    EmailService.SendAcceptanceEmail(appointment);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Binds the data.
        /// </summary>
        /// <param name="rebindInPostback">if set to <c>true</c> rebinds to the <see cref="AppointmentCollection"/> during a postback.</param>
        private void BindData(bool rebindInPostback)
        {
            this.PagingControl.PageSize = ModuleSettings.AppointmentsPerPage.GetValueAsInt32For(this).Value;

            var appointments = AppointmentCollection.Load(this.ModuleId, null, null, this.CurrentPageIndex - 1, this.PagingControl.PageSize);
            this.SetupPagingControl(this.PagingControl, appointments.TotalRecords);

            if (!this.IsPostBack || rebindInPostback)
            {
                this.AppointmentsGrid.DataSource = appointments;
                this.AppointmentsGrid.DataBind();
            }

            if (this.AppointmentsGrid.HeaderRow != null)
            {
                this.AppointmentsGrid.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        /// <summary>
        /// Fills the controls in <see cref="AppointmentDetailsPlaceholder"/> with the information about the given <paramref name="appointment"/>.
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
        /// Gets the IDs of the <see cref="Appointment"/>s whose rows are selected in <see cref="AppointmentsGrid"/>.
        /// </summary>
        /// <returns>A list of the selected appointment IDs</returns>
        private List<int> GetSelectedAppointmentIds()
        {
            List<int> selectedAppointmentIds = new List<int>(this.AppointmentsGrid.Rows.Count);
            foreach (GridViewRow row in this.AppointmentsGrid.Rows)
            {
                var selectionCheckBox = (CheckBox)row.FindControl("SelectionCheckBox");
                if (selectionCheckBox.Checked)
                {
                    var appointmentIdHiddenField = (HiddenField)row.FindControl("AppointmentIdHiddenField");
                    selectedAppointmentIds.Add(int.Parse(appointmentIdHiddenField.Value, NumberStyles.Integer, CultureInfo.InvariantCulture));
                }
            }

            return selectedAppointmentIds;
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
        /// Sets up the Select All jQuery plugin (to allow the header checkbox to select all other checkboxes).
        /// </summary>
        private void SetupSelectAllPlugin()
        {
            const string SelectAllCheckBoxCssClass = "header-checkbox";
            const string CheckBoxesCssClass = "select-checkbox";
            string initScriptKey = string.Format(CultureInfo.InvariantCulture, "SelectAllPlugin {0} : {1}", SelectAllCheckBoxCssClass, CheckBoxesCssClass);

            this.AddJQueryReference();
            ScriptManager.RegisterClientScriptResource(this, typeof(Approval), "Engage.Dnn.Booking.JavaScript.SelectAllPlugin.js");
            ScriptManager.RegisterStartupScript(
                    this,
                    typeof(Approval),
                    initScriptKey,
                    "jQuery(function($) { $('." + SelectAllCheckBoxCssClass + "').selectAll($('." + CheckBoxesCssClass + "')); });",
                    true);
        }
    }
}