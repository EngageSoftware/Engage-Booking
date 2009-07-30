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
    using DotNetNuke.Entities.Portals;
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
        /// <param name="appointment">The appointment which was declined.</param>
        /// <param name="declineReason">The reason for declining the appointment, or <c>null</c>.</param>
        public static void SendDeclineEmail(Appointment appointment, string declineReason)
        {
            string body = string.IsNullOrEmpty(declineReason) || string.IsNullOrEmpty(declineReason.Trim())
                                  ? GetLocalizedFormattedText("DeclineBody.Format", appointment)
                                  : GetLocalizedFormattedText("DeclineBodyWithReason.Format", appointment, declineReason);
            SendEmail(
                appointment.RequestorEmail, 
                GetLocalizedFormattedText("DeclineSubject.Format", appointment), 
                body);
        }

        /// <summary>
        /// Sends the email indicated to the module administrator that a new request has been made.
        /// </summary>
        /// <param name="appointment">The appointment.</param>
        /// <param name="toEmailAddresses">To email addresses.</param>
        /// <param name="approvalUrl">The approval URL.</param>
        /// <param name="declineUrl">The decline URL.</param>
        /// <param name="loginUrl">The login URL.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "login", Justification = "No Thanks")]
        public static void SendNewRequestEmail(Appointment appointment, string toEmailAddresses, string approvalUrl, string declineUrl, string loginUrl)
        {
            SendEmail(
                toEmailAddresses,
                GetLocalizedFormattedText("NewRequestSubject.Format", appointment),
                GetLocalizedFormattedText("NewRequestBody.Format", appointment, approvalUrl, declineUrl, loginUrl));
        }

        /// <summary>
        /// Sends an email to the given email address with the given <paramref name="subject"/> and HTML <paramref name="body"/>.
        /// </summary>
        /// <param name="portalId">The current portalId.</param>
        /// <param name="toList">The comma-or-semicolon-delimited list of email address(es) to which the email should be sent.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The HTML body.</param>
        private static void SendEmail(string toList, string subject, string body)
        {
            AppointmentSqlDataProvider.QueueEmail(PortalController.GetCurrentPortalSettings().PortalId, toList, subject, body);
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
            return GetLocalizedFormattedText(localizationKey, appointment, string.Empty);
        }

        /// <summary>
        /// Gets the text for the given <paramref name="localizationKey"/> (from the <see cref="Utility.LocalSharedResourceFile"/>),
        /// and fills in the <see cref="string.Format(System.IFormatProvider,string,object[])"/> placeholders in that text with appointment information.
        /// </summary>
        /// <param name="localizationKey">The localization key.</param>
        /// <param name="appointment">The appointment.</param>
        /// <param name="declineReason">The reason for declining <paramref name="appointment"/>.</param>
        /// <returns>
        /// The localized text with appointment-specific information inserted
        /// </returns>
        private static string GetLocalizedFormattedText(string localizationKey, Appointment appointment, string declineReason)
        {
            return GetLocalizedFormattedText(localizationKey, appointment, declineReason, string.Empty, string.Empty, string.Empty);
        }

        /// <summary>
        /// Gets the text for the given <paramref name="localizationKey"/> (from the <see cref="Utility.LocalSharedResourceFile"/>),
        /// and fills in the <see cref="string.Format(System.IFormatProvider,string,object[])"/> placeholders in that text with appointment information.
        /// </summary>
        /// <param name="localizationKey">The localization key.</param>
        /// <param name="appointment">The appointment.</param>
        /// <param name="approvalUrl">The URL to use to approve the appointment.</param>
        /// <param name="declineUrl">The URL to use to decline the appointment.</param>
        /// <param name="loginUrl">The URL to use to log into the module.</param>
        /// <returns>
        /// The localized text with appointment-specific information inserted
        /// </returns>
        private static string GetLocalizedFormattedText(string localizationKey, Appointment appointment, string approvalUrl, string declineUrl, string loginUrl)
        {
            return GetLocalizedFormattedText(localizationKey, appointment, string.Empty, approvalUrl, declineUrl, loginUrl);
        }

        /// <summary>
        /// Gets the text for the given <paramref name="localizationKey"/> (from the <see cref="Utility.LocalSharedResourceFile"/>),
        /// and fills in the <see cref="string.Format(System.IFormatProvider,string,object[])"/> placeholders in that text with appointment information.
        /// </summary>
        /// <param name="localizationKey">The localization key.</param>
        /// <param name="appointment">The appointment.</param>
        /// <param name="declineReason">The reason for declining <paramref name="appointment"/>.</param>
        /// <param name="approvalUrl">The URL to use to approve the appointment.</param>
        /// <param name="declineUrl">The URL to use to decline the appointment.</param>
        /// <param name="loginUrl">The URL to use to log into the module.</param>
        /// <returns>
        /// The localized text with appointment-specific information inserted
        /// </returns>
        private static string GetLocalizedFormattedText(string localizationKey, Appointment appointment, string declineReason, string approvalUrl, string declineUrl, string loginUrl)
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
                    appointment.IsPresenterSpecialYOrN,
                    appointment.ParticipantInstructions,
                    UserController.GetCurrentUserInfo().DisplayName,
                    Globals.GetPortalSettings().PortalName,
                    declineReason,
                    approvalUrl,
                    declineUrl,
                    loginUrl);
        }
    }
}
