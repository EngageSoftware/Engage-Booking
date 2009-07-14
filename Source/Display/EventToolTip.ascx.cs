// <copyright file="EventToolTip.ascx.cs" company="Engage Software">
// Engage: Events - http://www.EngageSoftware.com
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
    using System.Diagnostics;
    using System.Web.UI;
    using DotNetNuke.Framework;

    /// <summary>
    /// Used to display a "tool tip" for an appointment.
    /// </summary>
    public partial class EventToolTip : ModuleBase
    {
        /// <summary>
        /// The backing field for <see cref="SetEvent"/>.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Appointment currentAppointment;

        /// <summary>
        /// Sets the appointment to be displayed in the appointment tooltip.
        /// </summary>
        /// <param name="tooltipEvent">The appointment to display in the tooltip.</param>
        internal void SetEvent(Appointment tooltipEvent)
        {
            this.currentAppointment = tooltipEvent;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.PreRender += this.Page_PreRender;
            this.AddToCalendarButton.Click += this.AddToCalendarButton_Click;
            this.EditButton.Click += this.EditButton_Click;
            this.RequestAppointmentButton.Click += this.RequestAppointmentButton_Click;

            this.RegisterButton.CurrentAppointment = this.currentAppointment;
            this.RegisterButton.ModuleConfiguration = this.ModuleConfiguration;
            this.RegisterButton.LocalResourceFile = this.LocalResourceFile;

            AJAX.RegisterPostBackControl(this.AddToCalendarButton);
            AJAX.RegisterPostBackControl(this.RequestAppointmentButton);
        }

        /// <summary>
        /// Handles the <see cref="Control.PreRender"/> event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Page_PreRender(object sender, EventArgs e)
        {
            this.EventDate.Text = Util.Utility.GetFormattedEventDate(this.currentAppointment.StartDateTime, this.currentAppointment.EndDateTime, this.LocalResourceFile);
            //this.EventOverview.Text = this.currentAppointment.Overview;
            //this.EventTitle.Text = this.currentAppointment.Title;
            //this.RegisterButton.Visible = this.currentAppointment.AllowRegistrations;
            this.EditButton.Visible = this.IsAdmin;
        }

        /// <summary>
        /// Handles the Click event of the AddToCalendarButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void AddToCalendarButton_Click(object sender, EventArgs e)
        {
            SendICalendarToClient(this.Response, this.currentAppointment.ToICal(), this.currentAppointment.Title);
        }

        /// <summary>
        /// Handles the Click event of the EditButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void EditButton_Click(object sender, EventArgs e)
        {
            this.Response.Redirect(this.BuildLinkUrl(this.ModuleId, "EventEdit", Util.Utility.GetEventParameters(this.currentAppointment)), true);
        }

        private void RequestAppointmentButton_Click(object sender, EventArgs e)
        {
            this.Response.Redirect(this.BuildLinkUrl(this.ModuleId, "AppointmentRequest"));
        }
    }
}