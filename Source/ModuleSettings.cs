// <copyright file="ModuleSettings.cs" company="Engage Software">
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
    using DotNetNuke.Common;
    using Framework;

    /// <summary>
    /// A collection of the <see cref="Engage.Dnn.Framework.Setting{T}"/>s for this module
    /// </summary>
    public static class ModuleSettings
    {
        /// <summary>
        /// The skin used for the Calendar display
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Setting<T> is immutable")]
        public static readonly Setting<TelerikSkin> CalendarSkin = new Setting<TelerikSkin>("CalendarSkin", SettingScope.TabModule, TelerikSkin.Default);

        /// <summary>
        /// The number of appointments to display on a single day in the calendar's month view
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Setting<T> is immutable")]
        public static readonly Setting<int> AppointmentsToDisplayPerDay = new Setting<int>("AppointmentsToDisplayPerDay", SettingScope.TabModule, 3);

        /// <summary>
        /// The number of appointments to display on a page in the approval control
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Setting<T> is immutable")]
        public static readonly Setting<int> AppointmentsPerPage = new Setting<int>("AppointmentsPerPage", SettingScope.TabModule, 10);

        /// <summary>
        /// A comma-delimited list of the email addresses to send notification to when a new appointment request is submitted
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Setting<T> is immutable")]
        public static readonly Setting<string> NotificationEmailAddresses = new Setting<string>("NotificationEmailAddresses", SettingScope.TabModule, string.Empty);

        /// <summary>
        /// Which roles users must be in order to submit requests for appointments on the calendar of this instance of the module
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Setting<T> is immutable")]
        public static readonly Setting<string> AppointmentRequestsRole = new Setting<string>("AppointmentRequestsRole", SettingScope.TabModule, Globals.glbRoleAllUsersName);

        /// <summary>
        /// The default appointment duration in minutes
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Setting<T> is immutable")]
        public static readonly Setting<int> DefaultAppointmentDuration = new Setting<int>("DefaultAppointmentDuration", SettingScope.TabModule, 30);

        /// <summary>
        /// The minimum appointment duration a user can request in minutes
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Setting<T> is immutable")]
        public static readonly Setting<int> MinimumAppointmentDuration = new Setting<int>("MinimumAppointmentDuration", SettingScope.TabModule, 5);

        /// <summary>
        /// The maximum appointment duration a user can request in minutes
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Setting<T> is immutable")]
        public static readonly Setting<int> MaximumAppointmentDuration = new Setting<int>("MaximumAppointmentDuration", SettingScope.TabModule, 24 * 60);
    }
}