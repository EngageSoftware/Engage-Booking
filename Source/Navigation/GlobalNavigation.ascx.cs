// <copyright file="GlobalNavigation.ascx.cs" company="Engage Software">
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
    using System.ComponentModel;
    using System.Globalization;
    using DotNetNuke.Common;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Security.Permissions;
    using DotNetNuke.Services.Exceptions;

    /// <summary>
    /// A navigation control that is always displayed at the top of the module.  Currently only for administrators.
    /// </summary>
    public partial class GlobalNavigation : ModuleBase
    {
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            try
            {
                base.OnInit(e);

                // since the global navigation control is not loaded using DNN mechanisms we need to set it here so that calls to 
                // module related information will appear the same as the actual control this navigation is sitting on.hk
                this.ModuleConfiguration = Engage.Utility.FindParentControl<PortalModuleBase>(this).ModuleConfiguration;

                this.Load += this.Page_Load;
            }
            catch (LicenseException)
            {
                // swallow this exception so that MainContainer can handle it
                this.Visible = false;
            }
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.SetupLinks();
                this.LocalizeMenu();
                this.SetVisibility();
                this.SetCurrentlySelectedMenu();
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// <summary>
        /// Highlights the path of the menu item for the current page.
        /// </summary>
        private void SetCurrentlySelectedMenu()
        {
            var controlKey = this.GetCurrentControlKey();
            var currentItem = this.NavigationMenu.FindItemByValue(controlKey);
            if (!string.IsNullOrEmpty(controlKey) && currentItem != null)
            {
                // Highlight the current item and his parents
                currentItem.HighlightPath();
            }
            else
            {
                this.NavigationMenu.Items[0].HighlightPath();
            }
        }

        /// <summary>
        /// Localizes the menu items. It localizes the universe!
        /// </summary>
        private void LocalizeMenu()
        {
            this.HomeItem.Text = this.Localize("Home");
            this.AddEventItem.Text = this.Localize("Add Appointment");
            this.ExportDataItem.Text = this.Localize("Export");
            this.SettingsItem.Text = this.Localize("Settings");
            this.ModuleSettingsItem.Text = this.Localize("Module Settings");
            this.ManageAppointmentTypesItem.Text = this.Localize("Appointment Types");
            this.ManageItem.Text = this.Localize("Manage");
        }

        /// <summary>
        /// Sets up the URLs for each of the links.
        /// </summary>
        private void SetupLinks()
        {
            this.HomeItem.NavigateUrl = Globals.NavigateURL();

            this.AddEventItem.Value = "AppointmentRequest";
            this.AddEventItem.NavigateUrl = this.BuildLinkUrl(this.ModuleId, ControlKey.AppointmentRequest);

            this.ExportDataItem.Value = "ExportData";
            this.ExportDataItem.NavigateUrl = this.BuildLinkUrl(this.ModuleId, ControlKey.ExportData);

            this.ManageAppointmentTypesItem.Value = "ManageAppointmentTypes";
            this.ManageAppointmentTypesItem.NavigateUrl = this.BuildLinkUrl(this.ModuleId, ControlKey.ManageAppointmentTypes);

            this.ModuleSettingsItem.NavigateUrl = this.EditUrl("ModuleId", this.ModuleId.ToString(CultureInfo.InvariantCulture), "Module");
        }

        /// <summary>
        /// Sets the visibility.
        /// </summary>
        private void SetVisibility()
        {
            this.Visible = this.IsAdmin;
            this.ModuleSettingsItem.Visible = TabPermissionController.HasTabPermission("EDIT");
        }
    }
}