// <copyright file="ModuleBase.cs" company="Engage Software">
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
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using DotNetNuke.Services.Localization;
    using DotNetNuke.UI.WebControls;

    /// <summary>
    /// This class extends the framework version in order for developers to add on any specific methods/behavior.
    /// </summary>
    public class ModuleBase : Framework.ModuleBase
    {
        /// <summary>
        /// Gets the name of the this module's desktop module record in DNN.
        /// </summary>
        /// <value>The name of this module's desktop module record in DNN.</value>
        public override string DesktopModuleName
        {
            get { return Utility.DesktopModuleName; }
        }

        /// <summary>
        /// Gets the appointmentId id.
        /// </summary>
        /// <value>The appointmentId id.</value>
        protected int? AppointmentId
        {
            get
            {
                if (this.Request.QueryString["appointmentId"] != null)
                {
                    int id;
                    if (int.TryParse(this.Request.QueryString["appointmentId"], NumberStyles.Integer, CultureInfo.InvariantCulture, out id))
                    {
                        return id;
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is configured.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is configured; otherwise, <c>false</c>.
        /// </value>
        protected override bool IsConfigured
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance of the module allows users to submit appointment requests.
        /// </summary>
        /// <value><c>true</c> if this instance of the module allows users to submit appointment requests; otherwise, <c>false</c>.</value>
        protected bool AllowAppointments
        {
            get
            {
                return ModuleSettings.AppointmentRequestsRole.GetValueAsBooleanFor(this).Value;
            }
        }
     
        /// <summary>
        /// Gets the (one-based) index of the current page from the <see cref="HttpRequest.QueryString"/>.
        /// </summary>
        /// <value>The one-based index of the current page.</value>
        protected int CurrentPageIndex
        {
            get
            {
                int index;
                if (!int.TryParse(this.Request.QueryString["currentpage"], NumberStyles.Integer, CultureInfo.InvariantCulture, out index))
                {
                    index = 1;
                }

                return index;
            }
        }

        /// <summary>
        /// Sends an <c>iCalendar</c> to the client to download.
        /// </summary>
        /// <param name="response">The response to use to send the <c>iCalendar</c>.</param>
        /// <param name="content">The content of the <c>iCalendar</c>.</param>
        /// <param name="name">The name of the file.</param>
        protected static void SendICalendarToClient(HttpResponse response, string content, string name)
        {
            response.Clear();

            // Stream The ICalendar 
            response.Buffer = true;

            response.ContentType = "text/calendar";
            response.ContentEncoding = Encoding.UTF8;
            response.Charset = "utf-8";

            response.AddHeader("Content-Disposition", "attachment;filename=\"" + HttpUtility.UrlEncode(name) + ".ics" + "\"");

            response.Write(content);
            response.End();
        }

        /// <summary>
        /// Generates a list of QueryString parameters for the given list of <paramref name="queryStringKeys"/>.
        /// </summary>
        /// <param name="request">The current request.</param>
        /// <param name="queryStringKeys">The keys for which to generate parameters.</param>
        /// <returns>
        /// A list of QueryString parameters for the given list of <paramref name="queryStringKeys"/>
        /// </returns>
        protected static string GenerateQueryStringParameters(HttpRequest request, params string[] queryStringKeys)
        {
            StringBuilder queryString = new StringBuilder(64);
            foreach (string key in queryStringKeys)
            {
                if (Engage.Utility.HasValue(request.QueryString[key]))
                {
                    if (queryString.Length > 0)
                    {
                        queryString.Append("&");
                    }

                    queryString.Append(key).Append("=").Append(request.QueryString[key]);
                }
            }

            return queryString.ToString();
        }

        /// <summary>
        /// Sets up a DNN <see cref="PagingControl"/>.
        /// </summary>
        /// <param name="pagingControl">The pager control.</param>
        /// <param name="totalRecords">The total records.</param>
        /// <param name="queryStringKeys">The QueryString keys which should be persisted when the paging links are clicked.</param>
        protected void SetupPagingControl(PagingControl pagingControl, int totalRecords, params string[] queryStringKeys)
        {
            pagingControl.Visible = totalRecords != 0;
            pagingControl.TotalRecords = totalRecords;
            pagingControl.CurrentPage = this.CurrentPageIndex;
            pagingControl.TabID = this.TabId;
            pagingControl.QuerystringParams = GenerateQueryStringParameters(this.Request, queryStringKeys);
        }

        /// <summary>
        /// Gets localized text for the given resource key using this control's <see cref="DotNetNuke.Entities.Modules.PortalModuleBase.LocalResourceFile"/>.
        /// </summary>
        /// <param name="resourceKey">The resource key.</param>
        /// <returns>Localized text for the given resource key</returns>
        protected string Localize(string resourceKey)
        {
            return Localization.GetString(resourceKey, this.LocalResourceFile);
        }

        /// <summary>
        /// Raises the <see cref="Control.Init"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.LocalResourceFile = this.AppRelativeTemplateSourceDirectory + Localization.LocalResourceDirectory + "/" + Path.GetFileNameWithoutExtension(this.TemplateControl.AppRelativeVirtualPath);
        }
    }
}
