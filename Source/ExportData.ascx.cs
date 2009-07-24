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
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// ExportData class
    /// </summary>
    public partial class ExportData : ModuleBase
    {
        /// <summary>
        /// Gets the base filename (no extension) for the AppointmentsData export.
        /// </summary>
        /// <value>The base filename with the current ISO 8601 date appended.</value>
        private static string BaseFileName
        {
            get
            {
                return "AppointmentsData-" + DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Handles the <see cref="Control.Load"/> event of this control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.StartDatePicker.SelectedDate = DateTime.Now;
            this.EndDatePicker.SelectedDate = DateTime.Now.AddDays(15);
        }

        /// <summary>
        /// Handles the Click event of the ExportButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void ExportButton_Click(object sender, EventArgs e)
        {
            var dt = new DataTable();
            dt.Locale = CultureInfo.InvariantCulture;
            dt.Load(AppointmentSqlDataProvider.GetAppointmentsByDateRange(this.ModuleId, this.StartDatePicker.SelectedDate, this.EndDatePicker.SelectedDate));

            // var outputStream = new StreamWriter(DotNetNuke.Common.Globals.ApplicationMapPath + "\\DesktopModules\\EngageBooking\\" + BaseFileName + ".csv");
            // outputStream.Write(CsvWriter.WriteToString(dt, false, false));
            File.WriteAllText(DotNetNuke.Common.Globals.ApplicationMapPath + "\\DesktopModules\\EngageBooking\\" + BaseFileName + ".csv", CsvWriter.WriteToString(dt, false, false));

            Response.Redirect(this.ModulePath + BaseFileName + ".csv");
        }
    }
}