// <copyright file="MainContainer.ascx.cs" company="Engage Software">
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
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Web;
    using System.Web.UI;
    using DotNetNuke.Common;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Security;
    using DotNetNuke.Services.Exceptions;
    using Framework;

    /// <summary>
    /// The main container that is used by this module.  
    /// This control is registered with DNN, and is in charge of loading other requested control.
    /// </summary>
    public partial class MainContainer : ModuleBase
    {
        /// <summary>
        /// The control key for the <see cref="DefaultSubControl"/>
        /// </summary>
        protected internal const ControlKey DefaultControlKey = ControlKey.Home;

        /// <summary>
        /// The default sub-control to load when no control key is provided
        /// </summary>
        private static readonly SubControlInfo DefaultSubControl = new SubControlInfo("Display/AppointmentCalendar.ascx", false);

        /// <summary>
        /// The sub-control to load when there is an error with the license
        /// </summary>
        private static readonly SubControlInfo LicenseErrorControl = new SubControlInfo("Admin/LicenseError.ascx", false);

        /// <summary>
        /// A dictionary mapping control keys to user controls.
        /// </summary>
        private static readonly IDictionary<ControlKey, SubControlInfo> ControlKeys = FillControlKeys();

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            if (!this.IsEditable)
            {
                this.ModuleConfiguration.DisplayPrint = false;
            }

            SubControlInfo controlToLoad;
            try
            {
                base.OnInit(e);

                controlToLoad = this.GetControlToLoad();
            }
            catch (LicenseException)
            {
                controlToLoad = LicenseErrorControl;
            }

            if (!controlToLoad.RequiresEditPermission || PortalSecurity.HasNecessaryPermission(SecurityAccessLevel.Edit, this.PortalSettings, this.ModuleConfiguration, this.UserInfo.Username))
            {
                this.LoadChildControl(controlToLoad);
            }
            else if (Engage.Utility.IsLoggedIn)
            {
                this.Response.Redirect(Globals.NavigateURL(this.TabId), true);
            }
            else
            {
                this.Response.Redirect(Dnn.Utility.GetLoginUrl(this.PortalSettings, this.Request), true);
            }
        }

        /// <summary>
        /// Fills <see cref="ControlKeys"/>.
        /// </summary>
        /// <returns>A dictionary mapping control keys to user controls.</returns>
        private static IDictionary<ControlKey, SubControlInfo> FillControlKeys()
        {
            return new Dictionary<ControlKey, SubControlInfo>(6)
                       {
                               { DefaultControlKey, DefaultSubControl },
                               { ControlKey.AppointmentRequest, new SubControlInfo("Display/AppointmentRequest.ascx", false) },

                               // The Approval control just causes the user to login before showing the default page, so that they see pending appointments
                               { ControlKey.Approval, new SubControlInfo("Display/AppointmentCalendar.ascx", true) },
                               { ControlKey.DirectApproval, new SubControlInfo("Display/DirectApproval.ascx", false) },
                               { ControlKey.ExportData, new SubControlInfo("Display/ExportData.ascx", true) },
                               { ControlKey.AppointmentDetails, new SubControlInfo("Display/AppointmentDetails.ascx", true) }
                       };
        }

        /// <summary>
        /// Gets the control to load, based on the key (or lack thereof) that is passed on the querystring.
        /// </summary>
        /// <returns>A relative path to the control that should be loaded into this container</returns>
        private SubControlInfo GetControlToLoad()
        {
            if (!this.IsConfigured)
            {
                return new SubControlInfo("Admin/NotConfigured.ascx", false);
            }

            ControlKey? keyParam = this.GetControlKey();
            return keyParam.HasValue ? ControlKeys[keyParam.Value] : DefaultSubControl;
        }

        /// <summary>
        /// Gets the <see cref="HttpRequest.QueryString"/> control key for the current <see cref="Page.Request"/>.
        /// </summary>
        /// <returns>The <see cref="HttpRequest.QueryString"/> control key for the current <see cref="Page.Request"/></returns>
        private ControlKey? GetControlKey()
        {
            string currentControlKey = this.GetCurrentControlKey();
            if (!string.IsNullOrEmpty(currentControlKey))
            {
                try
                {
                    return (ControlKey)Enum.Parse(typeof(ControlKey), currentControlKey, true);
                }
                catch (ArgumentException)
                {
                }
                catch (OverflowException)
                {
                }
            }

            return null;
        }

        /// <summary>
        /// Loads the child control to be displayed in this container.
        /// </summary>
        /// <param name="controlToLoad">The control to load.</param>
        private void LoadChildControl(SubControlInfo controlToLoad)
        {
            try
            {
                PortalModuleBase mb = (PortalModuleBase)this.LoadControl(controlToLoad.ControlPath);
                mb.ModuleConfiguration = this.ModuleConfiguration;
                mb.ID = Path.GetFileNameWithoutExtension(controlToLoad.ControlPath);
                this.phControls.Controls.Add(mb);
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }
    }
}