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
#if TRIAL
    using Engage.Licensing;
#endif

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
        /// Gets a value indicating whether the currently logged-in user can request appointments.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance the currently logged-in user can submit appointment requests; otherwise, <c>false</c>.
        /// </value>
        protected bool CanRequestAppointments
        {
            get
            {
                return this.UserInfo.IsInRole(ModuleSettings.AppointmentRequestsRole.GetValueAsStringFor(this)) 
                    || this.UserInfo.IsSuperUser
                    || this.UserInfo.IsInRole(this.PortalSettings.AdministratorRoleName);
            }
        }

        /// <summary>
        /// Sends a file to the client to download.
        /// </summary>
        /// <param name="response">The response to use to send the content.</param>
        /// <param name="contentType">The MIME content type of the <paramref name="content"/>.</param>
        /// <param name="content">The content of the file.</param>
        /// <param name="fileName">The name of the file.</param>
        protected static void SendContentToClient(HttpResponse response, string contentType, string content, string fileName)
        {
            response.Clear();

            response.Buffer = true;

            response.ContentType = contentType;
            response.ContentEncoding = Encoding.UTF8;
            response.Charset = "utf-8";

            response.AddHeader("Content-Disposition", "attachment;filename=\"" + HttpUtility.UrlEncode(fileName) + "\"");

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
        /// Builds a URL for this TabId, using the given queryString parameters.
        /// </summary>
        /// <param name="moduleId">The module id of the module for which the control key is being used.</param>
        /// <param name="controlKey">The control key to determine which control to load.</param>
        /// <returns>
        /// A URL to the current TabId, with the given queryString parameters
        /// </returns>
        protected string BuildLinkUrl(int moduleId, ControlKey controlKey)
        {
            return this.BuildLinkUrl(moduleId, controlKey.ToString());
        }

        /// <summary>
        /// Builds a URL for this TabId, using the given queryString parameters.
        /// </summary>
        /// <param name="moduleId">The module id of the module for which the control key is being used.</param>
        /// <param name="controlKey">The control key to determine which control to load.</param>
        /// <param name="queryStringParameters">Any other queryString parameters.</param>
        /// <returns>
        /// A URL to the current TabId, with the given queryString parameters
        /// </returns>
        protected string BuildLinkUrl(int moduleId, ControlKey controlKey, params string[] queryStringParameters)
        {
            return this.BuildLinkUrl(moduleId, controlKey.ToString(), queryStringParameters);
        }

        /// <summary>
        /// Builds a URL for the given <paramref name="tabId"/>, using the given queryString parameters.
        /// </summary>
        /// <param name="tabId">The tab id of the page to navigate to.</param>
        /// <param name="moduleId">The module id of the module for which the control key is being used.</param>
        /// <param name="controlKey">The control key to determine which control to load.</param>
        /// <param name="queryStringParameters">Any other queryString parameters.</param>
        /// <returns>
        /// A URL to the given <paramref name="tabId"/>, with the given queryString parameters
        /// </returns>
        protected string BuildLinkUrl(int tabId, int moduleId, ControlKey controlKey, params string[] queryStringParameters)
        {
            return this.BuildLinkUrl(tabId, moduleId, controlKey.ToString(), queryStringParameters);
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
#if TRIAL
            this.LicenseProvider = new TrialLicenseProvider(FeaturesController.ModuleLicenseKey);
#endif

            base.OnInit(e);
            this.LocalResourceFile = this.AppRelativeTemplateSourceDirectory + Localization.LocalResourceDirectory + "/" + Path.GetFileNameWithoutExtension(this.TemplateControl.AppRelativeVirtualPath);
        }
    }
}
