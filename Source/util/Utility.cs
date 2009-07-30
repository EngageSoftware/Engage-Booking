// <copyright file="Utility.cs" company="Engage Software">
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
    using System.Text;
    using DotNetNuke.Common;
    using DotNetNuke.Services.Localization;
    using Telerik.Web.UI;

    /// <summary>
    /// All common, shared functionality for the Engage: Booking module.
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// The friendly name of this module.
        /// </summary>
        public const string DesktopModuleName = "Engage: Booking";

        /// <summary>
        /// The friendly name of this module's definition.
        /// </summary>
        public const string ModuleDefinitionFriendlyName = "Engage: Booking";

        /// <summary>
        /// Gets the name of the desktop module folder.
        /// </summary>
        /// <value>The name of the desktop module folder.</value>
        public static string DesktopModuleFolderName
        {
            get
            {
                StringBuilder sb = new StringBuilder(128);
                sb.Append("/DesktopModules/");
                sb.Append(Globals.GetDesktopModuleByName(DesktopModuleName).FolderName);
                sb.Append("/");
                return sb.ToString();
            }
        }

        /// <summary>
        /// Gets the relative path to the resource file holding resources that are shared by multiple controls within the module.
        /// </summary>
        /// <value>The relative path to the resource file holding resources that are shared by multiple controls within the module</value>
        public static string LocalSharedResourceFile
        {
            get { return "~" + DesktopModuleFolderName + Localization.LocalResourceDirectory + "/" + Localization.LocalSharedResourceFile; }
        }

        /// <summary>
        /// Localizes the text within the given <paramref name="dateTimePicker"/>.
        /// </summary>
        /// <param name="dateTimePicker">The <see cref="RadDateTimePicker"/> to localize the text for.</param>
        public static void LocalizeDateTimePicker(RadDateTimePicker dateTimePicker)
        {
            dateTimePicker.TimeView.HeaderText = Localization.GetString("Time Picker", LocalSharedResourceFile);
            dateTimePicker.TimePopupButton.ToolTip = Localization.GetString("Time Picker ToolTip", LocalSharedResourceFile);
            dateTimePicker.DatePopupButton.ToolTip = Localization.GetString("Date Picker ToolTip", LocalSharedResourceFile);
            LocalizeCalendar(dateTimePicker.Calendar);
        }

        /// <summary>
        /// Localizes the calendar.
        /// </summary>
        /// <param name="calendar">The calendar.</param>
        public static void LocalizeCalendar(RadCalendar calendar)
        {
            if (calendar != null)
            {
                calendar.FastNavigationSettings.CancelButtonCaption = Localization.GetString("Date Picker Cancel", LocalSharedResourceFile);
                calendar.FastNavigationSettings.OkButtonCaption = Localization.GetString("Date Picker OK", LocalSharedResourceFile);
                calendar.FastNavigationSettings.TodayButtonCaption = Localization.GetString("Date Picker Today", LocalSharedResourceFile);
            }
        }

        /// <summary>
        /// Converts to given value to an enum value of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the enum to which the value is being converted</typeparam>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value to return if <paramref name="value"/> is not a value of <typeparamref name="T"/>.</param>
        /// <returns>
        /// The value converted to an enum value of type <typeparamref name="T"/>
        /// </returns>
        public static T ConvertToEnum<T>(string value, T defaultValue)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), value);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
    }
}