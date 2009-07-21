// <copyright file="EmailService.cs" company="Engage Software">
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
    using System.Text;
    using DotNetNuke.Common;
    using DotNetNuke.Entities.Host;
    using DotNetNuke.Entities.Users;
    using DotNetNuke.Services.Localization;
    using DotNetNuke.Services.Mail;

    /// <summary>
    /// Helps sending emails
    /// </summary>
    public static class EmailService
    {
        /// <summary>
        /// Sends the email indicating to an appointment requestor that their request has been approved.
        /// </summary>
        /// <param name="appointment">The appointment which was approved.</param>
        public static void SendAcceptanceEmail(Appointment appointment)
        {
            SendEmail(
                    appointment.RequestorEmail,
                    GetLocalizedFormattedText("AcceptanceSubject.Format", appointment),
                    GetLocalizedFormattedText("AcceptanceBody.Format", appointment));
        }

        /// <summary>
        /// Sends the email indicating to an appointment requestor that their request has been declined.
        /// </summary>
        /// <param name="appointment">The appointment which was approved.</param>
        public static void SendDeclineEmail(Appointment appointment)
        {
            SendEmail(
                    appointment.RequestorEmail,
                    GetLocalizedFormattedText("DeclineSubject.Format", appointment),
                    GetLocalizedFormattedText("DeclineBody.Format", appointment));
        }

        /// <summary>
        /// Sends an email to the given email address with the given <paramref name="subject"/> and HTML <paramref name="body"/>.
        /// </summary>
        /// <param name="to">The comma-or-semicolon-delimited list of email address(es) to which the email should be sent.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The HTML body.</param>
        private static void SendEmail(string to, string subject, string body)
        {
            Mail.SendMail(
                    Globals.GetPortalSettings().Email,
                    to,
                    string.Empty,
                    string.Empty,
                    MailPriority.Normal,
                    subject,
                    MailFormat.Html,
                    Encoding.UTF8,
                    body,
                    new string[] { },
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    "Y".Equals(HostSettings.GetHostSetting("SMTPEnableSSL"), StringComparison.Ordinal));
        }

        /// <summary>
        /// Gets the text for the given <paramref name="localizationKey"/> (from the <see cref="Utility.LocalSharedResourceFile"/>),
        /// and fills in the <see cref="string.Format(System.IFormatProvider,string,object[])"/> placeholders in that text with appointment information.
        /// </summary>
        /// <param name="localizationKey">The localization key.</param>
        /// <param name="appointment">The appointment.</param>
        /// <returns>The localized text with appointment-specific information inserted</returns>
        private static string GetLocalizedFormattedText(string localizationKey, Appointment appointment)
        {
            return string.Format(
                    CultureInfo.CurrentCulture,
                    Localization.GetString(localizationKey, Utility.LocalSharedResourceFile),
                    appointment.Title,
                    appointment.Description,
                    null, ////appointment.AppointmentType,
                    appointment.Notes,
                    appointment.Address1,
                    appointment.Address2,
                    appointment.City,
                    appointment.Region,
                    appointment.ContactStreet,
                    appointment.ContactPhone,
                    appointment.RequestorName,
                    appointment.RequestorPhoneType,
                    appointment.RequestorPhone,
                    appointment.RequestorAltPhoneType,
                    appointment.RequestorAltPhone,
                    appointment.RequestorEmail,
                    appointment.StartDateTime,
                    appointment.EndDateTime,
                    appointment.NumberOfSpecialParticipants,
                    appointment.NumberOfParticipants,
                    appointment.ParticipantGender,
                    appointment.ParticipantFlag,
                    appointment.ParticipantInstructions,
                    UserController.GetCurrentUserInfo().DisplayName,
                    Globals.GetPortalSettings().PortalName);
        }
    }
}
