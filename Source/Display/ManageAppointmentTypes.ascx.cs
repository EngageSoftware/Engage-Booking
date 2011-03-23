// <copyright file="ManageAppointmentTypes.ascx.cs" company="Engage Software">
// Engage: Booking - http://www.EngageSoftware.com
// Copyright (c) 2004-2011
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
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.UI.Utilities;

    using Telerik.Web.UI;

    /// <summary>
    /// Control to allow module editors to manage the categories for this module
    /// </summary>
    public partial class ManageAppointmentTypes : ModuleBase
    {
        /// <summary>
        /// Raises the <see cref="Control.Init"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.Load += this.Page_Load;
            this.AppointmentTypesGrid.DeleteCommand += this.AppointmentTypesGridDeleteCommand;
            this.AppointmentTypesGrid.ItemCreated += AppointmentTypesGridItemCreated;
            this.AppointmentTypesGrid.NeedDataSource += this.AppointmentTypesGridNeedDataSource;
            this.AppointmentTypesGrid.InsertCommand += this.AppointmentTypesGridInsertCommand;
            this.AppointmentTypesGrid.UpdateCommand += this.AppointmentTypesGridUpdateCommand;
        }

        /// <summary>
        /// Handles the <see cref="CustomValidator.ServerValidate"/> event of the <c>UniqueNameValidator</c> control.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="args">The <see cref="ServerValidateEventArgs"/> instance containing the event data.</param>
        protected void UniqueNameValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            var gridItem = Engage.Utility.FindParentControl<GridDataItem>((CustomValidator)source);
            var appointmentTypeId = gridItem.ItemIndex >= 0 ? (int)gridItem.OwnerTableView.DataKeyValues[gridItem.ItemIndex]["Id"] : -1;
            args.IsValid = !AppointmentTypeCollection.Load(this.ModuleId).Any(appointmentType => appointmentType.Id != appointmentTypeId && appointmentType.Name.Equals(args.Value, StringComparison.CurrentCultureIgnoreCase));
        }

        /// <summary>
        /// Handles the ItemCreated event of the AppointmentTypesGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridItemEventArgs"/> instance containing the event data.</param>
        private static void AppointmentTypesGridItemCreated(object sender, GridItemEventArgs e)
        {
            var commandItem = e.Item as GridCommandItem;
            if (commandItem != null)
            {
                // control names from http://www.telerik.com/help/aspnet-ajax/grddefaultbehavior.html
                commandItem.FindControl("RefreshButton").Visible = false;
                commandItem.FindControl("RebindGridButton").Visible = false;
            }
            else
            {
                var editableItem = e.Item as GridEditableItem;
                if (editableItem != null && e.Item.IsInEditMode)
                {
                    const int EnterKey = 13;
                    ClientAPI.RegisterKeyCapture(
                        editableItem["Name"].Controls.OfType<TextBox>().Single(),
                        editableItem["EditButtons"].Controls[0],
                        EnterKey);
                }
                else
                {
                    var normalItem = e.Item as GridDataItem;
                    if (normalItem != null && e.Item.DataItem != null)
                    {
                        ////var category = (AppointmentType)e.Item.DataItem;
                        ////normalItem["Delete"].Controls.OfType<LinkButton>().Single().Visible = category.EventCount == 0;
                    }
                }
            }
        }

        /// <summary>
        /// Handles the <see cref="RadGrid.DeleteCommand"/> event of the <see cref="AppointmentTypesGrid"/> control.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="e">The <see cref="GridCommandEventArgs"/> instance containing the event data.</param>
        private void AppointmentTypesGridDeleteCommand(object source, GridCommandEventArgs e)
        {
            var appointmentTypeId = (int)e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"];
            AppointmentType.Delete(appointmentTypeId);
        }

        /// <summary>
        /// Handles the NeedDataSource event of the AppointmentTypesGrid control.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridNeedDataSourceEventArgs"/> instance containing the event data.</param>
        private void AppointmentTypesGridNeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            this.AppointmentTypesGrid.DataSource = AppointmentTypeCollection.Load(this.ModuleId).Select(
                            appointmentType => new
                                {
                                    appointmentType.Id,
                                    Name = Utility.GetLocalizedAppointmentTypeName(appointmentType.Name)
                                });
        }

        /// <summary>
        /// Handles the InsertCommand event of the AppointmentTypesGrid control.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        private void AppointmentTypesGridInsertCommand(object source, GridCommandEventArgs e)
        {
            if (!this.Page.IsValid)
            {
                e.Canceled = true;
                return;
            }

            var newValues = new Dictionary<string, string>(2);
            e.Item.OwnerTableView.ExtractValuesFromItem(newValues, (GridEditableItem)e.Item);
            var appointmentType = AppointmentType.Create(newValues["Name"]);
            appointmentType.Save(this.UserId, this.ModuleId);
            
            this.SuccessModuleMessage.Visible = true;
            this.SuccessModuleMessage.Text = this.Localize("AppointmentTypeInsertSuccess");
        }

        /// <summary>
        /// Handles the UpdateCommand event of the AppointmentTypesGrid control.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        private void AppointmentTypesGridUpdateCommand(object source, GridCommandEventArgs e)
        {
            if (!this.Page.IsValid)
            {
                e.Canceled = true;
                return;
            }

            var appointmentTypeId = (int)e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"];
            var appointmentType = AppointmentType.Load(appointmentTypeId, this.ModuleId);
            if (appointmentType == null)
            {
                e.Canceled = true;
                return;
            }

            var newValues = new Dictionary<string, string>(2);
            e.Item.OwnerTableView.ExtractValuesFromItem(newValues, (GridEditableItem)e.Item);
            appointmentType.Name = newValues["Name"];

            appointmentType.Save(this.UserId, this.ModuleId);

            this.SuccessModuleMessage.Visible = true;
            this.SuccessModuleMessage.Text = this.Localize("AppointmentTypeUpdateSuccess");
        }

        /// <summary>
        /// Raises the <see cref="Control.Load"/> event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> object that contains the event data.</param>
        private void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    this.LocalizeGrid();
                }

                this.SuccessModuleMessage.Visible = false;
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// <summary>
        /// Localizes the <see cref="AppointmentTypesGrid"/>.
        /// </summary>
        private void LocalizeGrid()
        {
            this.AppointmentTypesGrid.MasterTableView.NoMasterRecordsText = this.Localize("NoAppointmentTypes.Text");
            this.AppointmentTypesGrid.MasterTableView.CommandItemSettings.AddNewRecordText = this.Localize("AddAppointmentType.Text");

            var editColumn = (GridEditCommandColumn)this.AppointmentTypesGrid.Columns.FindByUniqueName("EditButtons");
            editColumn.EditText = this.Localize("EditAppointmentType.Text");
            editColumn.CancelText = this.Localize("CancelEdit.Text");
            editColumn.UpdateText = this.Localize("UpdateAppointmentType.Text");
            editColumn.InsertText = this.Localize("CreateAppointmentType.Text");

            var appointmentTypeNameColumn = (GridTemplateColumn)this.AppointmentTypesGrid.Columns.FindByUniqueName("Name");
            appointmentTypeNameColumn.HeaderText = this.Localize("Name.Header");

            var deleteColumn = (GridButtonColumn)this.AppointmentTypesGrid.Columns.FindByUniqueName("Delete");
            deleteColumn.Text = this.Localize("DeleteAppointmentType.Text");
            deleteColumn.ConfirmText = this.Localize("DeleteConfirmation.Text");
        }
    }
}