// <copyright file="EmailScheduler.cs" company="Engage Software">
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
    using System.Data;
    using System.Globalization;
    using System.Net;
    using System.Net.Mail;
    using System.Text;
    using DotNetNuke.Common;
    using DotNetNuke.Common.Utilities;
    using DotNetNuke.Entities.Host;
    using DotNetNuke.Entities.Portals;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.Localization;
    using DotNetNuke.Services.Mail;
    using DotNetNuke.Services.Scheduling;

    /// <summary>
    /// A scheduler client for running scheduled email tasks.
    /// </summary>
    public class EmailScheduler : SchedulerClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailScheduler"/> class.
        /// </summary>
        /// <param name="objScheduleHistoryItem">The obj schedule history item.</param>
        public EmailScheduler(ScheduleHistoryItem objScheduleHistoryItem)
        {
            this.ScheduleHistoryItem = objScheduleHistoryItem;
        }

        /// <summary>
        /// Sends an email message.
        /// </summary>
        /// <remarks>
        /// This is a modified version of the DNN Core's <see cref="Mail.SendMail(string,string,string,string,DotNetNuke.Services.Mail.MailPriority,string,DotNetNuke.Services.Mail.MailFormat,System.Text.Encoding,string,string[],string,string,string,string,bool)"/> method.
        /// It does not provide a 
        /// </remarks>
        /// <param name="from">The email address from which the email is being sent.</param>
        /// <param name="to">The email address(es) to which the email is being sent.</param>
        /// <param name="cc">The email address(es) on the carbon copy line.</param>
        /// <param name="bcc">The email address(es) on the blind carbon copy line.</param>
        /// <param name="replyTo">The email address to which replies should be directed, if not <paramref name="from"/>.</param>
        /// <param name="priority">The priority of the email message.</param>
        /// <param name="subject">The subject of the email message.</param>
        /// <param name="bodyFormat">The format of <paramref name="body"/>.</param>
        /// <param name="bodyEncoding">The encoding of <paramref name="body"/>.</param>
        /// <param name="body">The body of the email message.</param>
        /// <param name="attachment">A list of the filename to attach to this email message.</param>
        /// <param name="smtpServer">The SMTP server to use to send the email, if not the server defined in the Host Settings of the website.</param>
        /// <param name="smtpAuthentication">The SMTP authentication type to use to send the email, if not the type defined in the Host Settings of the website.</param>
        /// <param name="smtpUserName">The SMTP username to use to send the email, if not the username defined in the Host Settings of the website.</param>
        /// <param name="smtpPassword">The SMTP password to use to send the email, if not the password defined in the Host Settings of the website.</param>
        /// <param name="smtpEnableSsl">if set to <c>true</c> sends the message to the SMTP server over SSL.</param>
        /// <returns><see cref="string.Empty"/>, or an error message if an error occurs.</returns>
        public static string SendMail(
                string from,
                string to,
                string cc,
                string bcc,
                string replyTo,
                DotNetNuke.Services.Mail.MailPriority priority,
                string subject,
                MailFormat bodyFormat,
                Encoding bodyEncoding,
                string body,
                string[] attachment,
                string smtpServer,
                string smtpAuthentication,
                string smtpUserName,
                string smtpPassword,
                bool smtpEnableSsl)
        {
            var returnMessage = string.Empty;

            // SMTP server configuration
            if (string.IsNullOrEmpty(smtpServer))
            {
                if (!string.IsNullOrEmpty((string)Globals.HostSettings["SMTPServer"]))
                {
                    smtpServer = (string)Globals.HostSettings["SMTPServer"];
                }
            }

            if (string.IsNullOrEmpty(smtpAuthentication))
            {
                if (!string.IsNullOrEmpty((string)Globals.HostSettings["SMTPAuthentication"]))
                {
                    smtpAuthentication = (string)Globals.HostSettings["SMTPAuthentication"];
                }
            }

            if (string.IsNullOrEmpty(smtpUserName))
            {
                if (!string.IsNullOrEmpty((string)Globals.HostSettings["SMTPUsername"]))
                {
                    smtpUserName = (string)Globals.HostSettings["SMTPUsername"];
                }
            }

            if (string.IsNullOrEmpty(smtpPassword))
            {
                if (!string.IsNullOrEmpty((string)Globals.HostSettings["SMTPPassword"]))
                {
                    smtpPassword = (string)Globals.HostSettings["SMTPPassword"];
                }
            }

            // translate semi-colon delimiters to commas as ASP.NET 2.0 does not support semi-colons
            MailMessage objMail = null;
            try
            {
                objMail = new MailMessage(from, to.Replace(";", ","));
                if (!string.IsNullOrEmpty(cc))
                {
                    objMail.CC.Add(cc.Replace(";", ","));
                }

                if (!string.IsNullOrEmpty(bcc))
                {
                    objMail.Bcc.Add(bcc.Replace(";", ","));
                }

                if (!string.IsNullOrEmpty(replyTo))
                {
                    objMail.ReplyTo = new MailAddress(replyTo);
                }

                objMail.Priority = (System.Net.Mail.MailPriority)priority;
                objMail.IsBodyHtml = bodyFormat == MailFormat.Html;

                foreach (string myAtt in attachment)
                {
                    if (!string.IsNullOrEmpty(myAtt))
                    {
                        objMail.Attachments.Add(new Attachment(myAtt));
                    }
                }

                // message
                objMail.SubjectEncoding = bodyEncoding;
                objMail.Subject = HtmlUtils.StripWhiteSpace(subject, true);
                objMail.BodyEncoding = bodyEncoding;

                objMail.Body = body;

                // added support for multipart html messages - removed in this modified version
                // add text part as alternate view - removed in this modified version
                ////var PlainView = AlternateView.CreateAlternateViewFromString(ConvertToText(body), null, "text/plain");
                ////objMail.AlternateViews.Add(PlainView);

                // if body contains html, add html part
                ////if (IsHTMLMail(body))
                ////{
                ////    var HTMLView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                ////    objMail.AlternateViews.Add(HTMLView);
                ////}
            }
            catch (Exception objException)
            {
                // Problem creating Mail Object
                returnMessage = to + ": " + objException.Message;
                Exceptions.LogException(objException);
            }

            if (objMail != null)
            {
                // external SMTP server alternate port
                int smtpPort = Null.NullInteger;
                int portPos = smtpServer.IndexOf(":", StringComparison.Ordinal);
                if (portPos > -1)
                {
                    smtpPort = int.Parse(smtpServer.Substring(portPos + 1, smtpServer.Length - portPos - 1), CultureInfo.InvariantCulture);
                    smtpServer = smtpServer.Substring(0, portPos);
                }

                var smtpClient = new SmtpClient();

                try
                {
                    if (!string.IsNullOrEmpty(smtpServer))
                    {
                        smtpClient.Host = smtpServer;
                        if (smtpPort > Null.NullInteger)
                        {
                            smtpClient.Port = smtpPort;
                        }

                        switch (smtpAuthentication)
                        {
                            // basic
                            case "1":
                                if (!string.IsNullOrEmpty(smtpUserName) && !string.IsNullOrEmpty(smtpPassword))
                                {
                                    smtpClient.UseDefaultCredentials = false;
                                    smtpClient.Credentials = new NetworkCredential(smtpUserName, smtpPassword);
                                }

                                break;

                            // NTLM
                            case "2":
                                smtpClient.UseDefaultCredentials = true;
                                break;
                        }
                    }

                    smtpClient.EnableSsl = smtpEnableSsl;

                    smtpClient.Send(objMail);
                    returnMessage = string.Empty;
                }
                catch (SmtpFailedRecipientException exc)
                {
                    returnMessage = string.Format(CultureInfo.CurrentCulture, Localization.GetString("FailedRecipient"), exc.FailedRecipient);
                    Exceptions.LogException(exc);
                }
                catch (SmtpException exc)
                {
                    returnMessage = Localization.GetString("SMTPConfigurationProblem");
                    Exceptions.LogException(exc);
                }
                catch (Exception objException)
                {
                    // mail configuration problem
                    if (objException.InnerException != null)
                    {
                        returnMessage = string.Concat(objException.Message, Environment.NewLine, objException.InnerException.Message);
                        Exceptions.LogException(objException.InnerException);
                    }
                    else
                    {
                        returnMessage = objException.Message;
                        Exceptions.LogException(objException);
                    }
                }
                finally
                {
                    objMail.Dispose();
                }
            }

            return returnMessage;
        }

        /// <summary>
        /// Sends all queued emails
        /// </summary>
        public override void DoWork()
        {
            try
            {
                int successCount = 0;
                using (IDataReader queuedEmails = AppointmentSqlDataProvider.GetQueuedEmails())
                {
                    while (queuedEmails.Read())
                    {
                        string result = SendMail(
                            new PortalController().GetPortal((int)queuedEmails["PortalId"]).Email,
                            queuedEmails["RecipientList"].ToString(),
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            DotNetNuke.Services.Mail.MailPriority.Normal,
                            queuedEmails["Subject"].ToString(),
                            MailFormat.Html,
                            Encoding.UTF8,
                            queuedEmails["Body"].ToString(),
                            new string[] { },
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            "Y".Equals(HostSettings.GetHostSetting("SMTPEnableSSL"), StringComparison.Ordinal));

                        if (string.IsNullOrEmpty(result))
                        {
                            AppointmentSqlDataProvider.ClearQueuedEmail((int)queuedEmails["QueueID"]);
                            successCount++;
                        }
                        else
                        {
                            this.ScheduleHistoryItem.AddLogNote("QueueID " + queuedEmails["QueueID"].ToString() + " failed to send.  SendMail error: " + result + "<br />");
                        }
                    }
                }

                this.ScheduleHistoryItem.Succeeded = true;
                this.ScheduleHistoryItem.AddLogNote("Email Scheduler completed successfully. " + successCount + " emails sent.<br />");
            }
            catch (Exception exc)
            {
                this.ScheduleHistoryItem.Succeeded = false;
                this.ScheduleHistoryItem.AddLogNote("Email Scheduler failed with the following error message: <br/>" + exc.Message + "<br />");
                this.Errored(ref exc);
            }
        }
    }
}