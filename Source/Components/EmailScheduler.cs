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
    using System.Text;
    using DotNetNuke.Common;
    using DotNetNuke.Entities.Host;
    using DotNetNuke.Entities.Portals;
    using DotNetNuke.Services.Log.EventLog;
    using DotNetNuke.Services.Mail;
    using DotNetNuke.Services.Scheduling;
    using Routing;
    using DotNetNuke.Services.Exceptions;

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
        /// Does the EmailScheduler work.
        /// </summary>
        public override void DoWork()
        {
            try
            {
                int successCount = 0;
                using (IDataReader queuedEmails = AppointmentSqlDataProvider.GetQueuedEmails())
                {
                    while(queuedEmails.Read())
                    {
                        string result = Mail.SendMail(
                            new PortalController().GetPortal((int)queuedEmails["PortalId"]).Email,
                            queuedEmails["EmailAddressList"].ToString(),
                            string.Empty,
                            string.Empty,
                            MailPriority.Normal,
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

                        if (result == string.Empty)
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
            catch (Exception e)
            {
                this.ScheduleHistoryItem.Succeeded = false;
                this.ScheduleHistoryItem.AddLogNote(e.Message + "<br />");
            }
        }
    }
}