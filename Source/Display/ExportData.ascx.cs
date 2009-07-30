// <copyright file="ExportData.ascx.cs" company="Engage Software">
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
    using System.Globalization;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using DotNetNuke.Framework;
    using DotNetNuke.Services.Localization;

    /// <summary>
    /// ExportData class
    /// </summary>
    public partial class ExportData : ModuleBase
    {
        /// <summary>
        /// Raises the <see cref="Control.Init"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            this.Load += this.Page_Load;
            this.ExportButton.Click += this.ExportButton_Click;
            base.OnInit(e);
        }

        /// <summary>
        /// Handles the <see cref="Control.Load"/> event of this control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Page_Load(object sender, EventArgs e)
        {
            ((ScriptManager)AJAX.ScriptManagerControl(this.Page)).RegisterPostBackControl(this.ExportButton);

            this.StartDatePicker.SelectedDate = DateTime.Today;
            this.EndDatePicker.SelectedDate = DateTime.Today.AddDays(15);

            this.StartDatePicker.Skin = this.EndDatePicker.Skin = ModuleSettings.CalendarSkin.GetValueAsStringFor(this);
        }

        /// <summary>
        /// Handles the <see cref="Button.Click"/> event of the <see cref="ExportButton"/> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ExportButton_Click(object sender, EventArgs e)
        {
            var appointmentsTable = AppointmentSqlDataProvider.GetAppointmentsByDateRange(this.ModuleId, this.StartDatePicker.SelectedDate, this.EndDatePicker.SelectedDate);
            SendContentToClient(this.Response, "text/csv", CsvWriter.WriteToString(appointmentsTable, this.HeaderRowCheckBox.Checked, false), this.GetFileName());
        }

        /// <summary>
        /// Gets the base filename (no extension) for the AppointmentsData export.
        /// </summary>
        /// <returns>The filename with the current ISO 8601 date appended.</returns>
        private string GetFileName()
        {
            return string.Format(CultureInfo.CurrentCulture, Localization.GetString("BaseFileName.Text", this.LocalResourceFile), DateTime.Today, this.StartDatePicker.SelectedDate, this.EndDatePicker.SelectedDate);
        }
    }
}