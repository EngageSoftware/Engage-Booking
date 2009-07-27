// <copyright file="DirectApproval.ascx.cs" company="Engage Software">
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
    using DotNetNuke.Services.Exceptions;

    /// <summary>
    /// Displays a list of the <see cref="Appointment"/>s waiting to be approved
    /// </summary>
    public partial class DirectApproval : ModuleBase
    {
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += this.Page_Load;
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string actionKeyString = this.Request.QueryString["ActionKey"];
                if (string.IsNullOrEmpty(actionKeyString))
                {
                    return;
                }

                Guid actionKey;
                try
                {
                    actionKey = new Guid(actionKeyString);
                }
                catch (FormatException)
                {
                    return;
                }
                catch (OverflowException)
                {
                    return;
                }

                Appointment appointment = Appointment.ApproveByKey(actionKey);
                if (appointment == null)
                {
                    return;
                }

                if (appointment.IsAccepted == null)
                {
                    this.ApprovalMessage.TextResourceKey = "UnsuccessfulAccept.Text";
                    return;
                }

                string resourceKey;
                if (appointment.IsAccepted.Value)
                {
                    EmailService.SendAcceptanceEmail(appointment);
                    resourceKey = "SuccessfulAccept.Text";
                }
                else
                {
                    EmailService.SendDeclineEmail(appointment, string.Empty);
                    resourceKey = "SuccessfulDecline.Text";
                }

                this.ApprovalMessage.MessageType = ModuleMessageType.Success;
                this.ApprovalMessage.TextResourceKey = resourceKey;
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }
    }
}