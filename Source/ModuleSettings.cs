// <copyright file="ModuleSettings.cs" company="Engage Software">
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
    using Framework;

    /// <summary>
    /// A collection of the <see cref="Engage.Dnn.Framework.Setting{T}"/>s for this module
    /// </summary>
    public static class ModuleSettings
    {
        /// <summary>
        /// The skin used for the Calendar display
        /// </summary>
        public static readonly Setting<TelerikSkin> CalendarSkin = new Setting<TelerikSkin>("CalendarSkin", SettingScope.TabModule, TelerikSkin.Default);

        /// <summary>
        /// The number of appointments to display on a single day in the calendar's month view
        /// </summary>
        public static readonly Setting<int> AppointmentsToDisplayPerDay = new Setting<int>("AppointmentsToDisplayPerDay", SettingScope.TabModule, 3);

        /// <summary>
        /// The number of appointments to display on a page in the approval control
        /// </summary>
        public static readonly Setting<int> AppointmentsPerPage = new Setting<int>("AppointmentsPerPage", SettingScope.TabModule, 10);
    }
}