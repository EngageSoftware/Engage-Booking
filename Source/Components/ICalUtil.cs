﻿// <copyright file="ICalUtil.cs" company="Engage Software">
// Engage.Events - http://www.engagemodules.com
// Copyright (c) 2004-2008
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
    using System.Text;
    using Telerik.Web.UI;

    /// <summary>
    /// Utilities for creating iCalendar files
    /// </summary>
    /// <remarks>
    /// Based off of class from Telerik's RadControl for ASP.NET AJAX
    /// </remarks>
    internal static class ICalUtil
    {
        /// <summary>
        /// Format to use for dates within the iCalendar file
        /// </summary>
        private const string DateFormat = "yyyyMMddTHHmmssZ";

        /// <summary>
        /// Exports the specified appointment as an iCalendar file.
        /// </summary>
        /// <param name="description">The appointment's description.</param>
        /// <param name="location">The appointment's location.</param>
        /// <param name="app">The appointment to export.</param>
        /// <param name="timeZoneOffset">The time zone offset.</param>
        /// <returns>The given appointment in an iCalendar format</returns>
        public static string Export(string description, string location, Appointment app, TimeSpan timeZoneOffset)
        {
            StringBuilder output = new StringBuilder();
            WriteFileHeader(output);

            ////if (app.RecurrenceState != RecurrenceState.Occurrence)
            ////{
            ////    if (outlookCompatibleMode)
            ////    {
            ////        ValidateOutlookCompatibility(app);
            ////    }
            ////}

            WriteTask(description, location, output, app, timeZoneOffset);

            WriteFileFooter(output);

            return output.ToString();
        }

        /// <summary>
        /// Writes the the entry for an event.
        /// </summary>
        /// <param name="description">The event's description.</param>
        /// <param name="location">The event's location.</param>
        /// <param name="output">The <see cref="StringBuilder"/> into which the output should be appended.</param>
        /// <param name="app">The appointment to export.</param>
        /// <param name="timeZoneOffset">The time zone offset.</param>
        /// <exception cref="InvalidOperationException">Invalid recurrence rule.</exception>
        private static void WriteTask(string description, string location, StringBuilder output, Appointment app, TimeSpan timeZoneOffset)
        {
            output.AppendFormat("DTSTART:{0}\r\n", FormatDate(ClientToUtc(app.StartDateTime, timeZoneOffset)));
            output.AppendFormat("DTEND:{0}\r\n", FormatDate(ClientToUtc(app.EndDateTime, timeZoneOffset)));

            string title = app.Title.Replace("\r\n", "\\n");
            title = title.Replace("\n", "\\n");
            output.AppendFormat("SUMMARY:{0}\r\n", title);
            output.AppendLine("END:VEVENT");
        }

        /// <summary>
        /// Writes the file header.
        /// </summary>
        /// <param name="output">The <see cref="StringBuilder"/> into which the output should be appended.</param>
        private static void WriteFileHeader(StringBuilder output)
        {
            output.AppendLine("BEGIN:VCALENDAR");
            output.AppendLine("VERSION:2.0");
            ////output.AppendLine("PRODID:-//Telerik Inc.//NONSGML RadScheduler//EN");
            output.AppendLine("PRODID:-//Microsoft Corporation//Outlook 12.0 MIMEDIR//EN");
        }

        /// <summary>
        /// Writes the file footer.
        /// </summary>
        /// <param name="output">The <see cref="StringBuilder"/> into which the output should be appended.</param>
        private static void WriteFileFooter(StringBuilder output)
        {
            output.AppendLine("END:VCALENDAR");
        }

        /// <summary>
        /// Adjusts the given date from client time to UTC.
        /// </summary>
        /// <param name="date">The date to be adjusted.</param>
        /// <param name="offset">The time zone offset.</param>
        /// <returns>The given date adjusted to UTC</returns>
        private static DateTime ClientToUtc(DateTime date, TimeSpan offset)
        {
            return new DateTime(date.Add(-offset).Ticks, DateTimeKind.Utc);
        }

        /// <summary>
        /// Formats the given date to the iCalendar date format.
        /// </summary>
        /// <param name="date">The date to be formatted.</param>
        /// <returns>The given date formatted for an iCalendar file.</returns>
        private static string FormatDate(DateTime date)
        {
            return date.ToString(DateFormat, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts the recurrence rule from client time to UTC.
        /// </summary>
        /// <param name="rrule">The recurrence rule.</param>
        /// <param name="offset">The time zone offset.</param>
        private static void ConvertRecurrenceRuleToUtc(RecurrenceRule rrule, TimeSpan offset)
        {
            rrule.Range.Start = ClientToUtc(rrule.Range.Start, offset);

            if (rrule.Range.RecursUntil < DateTime.MaxValue)
            {
                rrule.Range.RecursUntil = ClientToUtc(rrule.Range.RecursUntil, offset);
            }

            for (int i = 0; i < rrule.Exceptions.Count; i++)
            {
                rrule.Exceptions[i] = ClientToUtc(rrule.Exceptions[i], offset);
            }
        }
    }
}
